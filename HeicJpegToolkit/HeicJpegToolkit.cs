using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Helpers.Extensions;
using HeicJpegToolkit.Helpers.Options;
using HeicJpegToolkit.Helpers.Result;
using HeicJpegToolkit.Helpers.Utils;
using HeicJpegToolkit.Interfaces;
using HeicJpegToolkit.Structures;
using Microsoft.Extensions.Logging;

namespace HeicJpegToolkit
{
    public class HeicJpegToolkit
    {
        private static readonly ILogger _logger = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        }).CreateLogger<HeicJpegToolkit>();

        // Public method that initiates the conversion and accepts input file and output folder
        public static ConversionResult ConvertFile(string inputFilePath, string outputFolderPath, ImageFormat targetFormat = ImageFormat.JPEG)
        {
            return ConvertFile(inputFilePath, outputFolderPath, targetFormat, options: null);
        }

        /// <summary>
        /// Conversion with options for presets, limits, and metadata/privacy controls.
        /// </summary>
        public static ConversionResult ConvertFile(
            string inputFilePath,
            string outputFolderPath,
            ImageFormat targetFormat,
            ConversionOptions? options)
        {
            if (!OperatingSystem.IsWindows())
            {
                return new ConversionResult("This tool runs on Windows only.");
            }

            if (string.IsNullOrWhiteSpace(inputFilePath) || !File.Exists(inputFilePath))
            {
                return new ConversionResult("Invalid input file path.", "File does not exist.");
            }

            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                return new ConversionResult("Invalid output folder path.");
            }

            options ??= new ConversionOptions();

            try
            {
                return ConvertInternal(inputFilePath, outputFolderPath, targetFormat, options);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Conversion failed.");
                return new ConversionResult($"Conversion error: {ex.Message}", ex.ToString());
            }
        }

        /// <summary>
        /// Async wrapper around <see cref="ConvertFile"/> for background and server scenarios.
        /// </summary>
        public static Task<ConversionResult> ConvertFileAsync(
            string inputFilePath,
            string outputFolderPath,
            ImageFormat targetFormat = ImageFormat.JPEG,
            CancellationToken cancellationToken = default)
        {
            // Use Task.Run to avoid blocking ASP.NET or UI threads when doing heavy work.
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                return ConvertFile(inputFilePath, outputFolderPath, targetFormat);
            }, cancellationToken);
        }

        /// <summary>
        /// Converts from an input stream to an output stream.
        /// This is ideal for ASP.NET Core Web API or web applications that work with uploaded files.
        /// Internally this uses temporary files and the existing Windows-only codec pipeline.
        /// </summary>
        public static async Task<ConversionResult> ConvertStreamAsync(
            Stream input,
            Stream output,
            ImageFormat targetFormat = ImageFormat.JPEG,
            CancellationToken cancellationToken = default,
            ConversionOptions? options = null)
        {
            if (!OperatingSystem.IsWindows())
            {
                return new ConversionResult("This tool runs on Windows only.");
            }

            if (input is null) throw new ArgumentNullException(nameof(input));
            if (output is null) throw new ArgumentNullException(nameof(output));
            if (!input.CanRead) throw new ArgumentException("Input stream must be readable.", nameof(input));
            if (!output.CanWrite) throw new ArgumentException("Output stream must be writable.", nameof(output));

            // Use a unique temporary working directory so multiple concurrent calls are safe.
            var tempRoot = Path.Combine(Path.GetTempPath(), "HeicJpegToolkit");
            Directory.CreateDirectory(tempRoot);

            var workingDir = Path.Combine(tempRoot, Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(workingDir);

            var inputPath = Path.Combine(workingDir, "input.heic");
            try
            {
                options ??= new ConversionOptions();

                if (options?.MaxInputBytes is long maxBytes && input.CanSeek && input.Length > maxBytes)
                {
                    return new ConversionResult($"Input exceeds the maximum size of {maxBytes} bytes.");
                }

                await using (var fileStream = File.Create(inputPath))
                {
                    await input.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
                }

                var result = ConvertInternal(inputPath, workingDir, targetFormat, options);
                if (!result.Success || string.IsNullOrWhiteSpace(result.OutputFilePath))
                {
                    return result;
                }

                await using (var fileStream = File.OpenRead(result.OutputFilePath))
                {
                    await fileStream.CopyToAsync(output, cancellationToken).ConfigureAwait(false);
                }

                if (output.CanSeek)
                {
                    output.Seek(0, SeekOrigin.Begin);
                }

                return result;
            }
            finally
            {
                try
                {
                    if (Directory.Exists(workingDir))
                    {
                        Directory.Delete(workingDir, recursive: true);
                    }
                }
                catch (Exception cleanupEx)
                {
                    _logger.LogWarning(cleanupEx, "Failed to clean up temporary working directory {WorkingDir}", workingDir);
                }
            }
        }

        private static ConversionResult ConvertInternal(string inputFilePath, string outputFolderPath, ImageFormat targetFormat, ConversionOptions options)
        {
            Directory.CreateDirectory(outputFolderPath);
            inputFilePath = Path.GetFullPath(inputFilePath);

            var imagingFactory = new WICImagingFactory();
            var decoder = imagingFactory.CreateDecoderFromFilename(inputFilePath, Guid.Empty, StreamAccessMode.GENERIC_READ, WICDecodeOptions.WICDecodeMetadataCacheOnLoad);

            var formatGuid = GetFormatGuid(targetFormat);
            string outputFilePath = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(inputFilePath) + "." + targetFormat.ToString().ToLower());

            if (decoder.GetDecoderInfo().GetCLSID() == formatGuid)
            {
                return new ConversionResult(outputFilePath)
                {
                    Reason = "No conversion needed."
                };
            }

            var output = imagingFactory.CreateStream();
            output.InitializeFromFilename(outputFilePath, StreamAccessMode.GENERIC_WRITE);
            var encoder = imagingFactory.CreateEncoder(formatGuid);
            encoder.Initialize(output, WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

            for (int i = 0; i < decoder.GetFrameCount(); i++)
            {
                var frame = decoder.GetFrame(i);

                // Enforce pixel-count safety limit if configured.
                var originalSize = frame.GetSize();
                if (options.MaxPixelCount is long maxPixels &&
                    (long)originalSize.Width * originalSize.Height > maxPixels)
                {
                    return new ConversionResult($"Input image exceeds the maximum pixel count of {maxPixels}.");
                }

                // Decide target dimensions based on options and presets.
                var targetSize = GetTargetSize(originalSize, options);

                // Create encoder frame with encoder options bag so we can set quality.
                var frameTarget = encoder.CreateNewFrame(out var encoderOptions);
                ConfigureEncoderOptions(encoderOptions, targetFormat, options);

                frameTarget.Initialize(null);
                frameTarget.SetSize(targetSize);
                frameTarget.SetResolution(frame.GetResolution());
                frameTarget.SetPixelFormat(frame.GetPixelFormat());

                // Apply metadata filter based on PreserveMetadata / StripGpsMetadata.
                Func<string, bool>? metadataFilter = name => ShouldKeepMetadata(name, options);
                CopyMetadata(frame, frameTarget, metadataFilter);

                // Optionally resize using a high-quality scaler.
                IWICBitmapSource source = frame;
                if (targetSize.Width != originalSize.Width || targetSize.Height != originalSize.Height)
                {
                    var scaler = imagingFactory.CreateBitmapScaler();
                    scaler.Initialize(frame, targetSize.Width, targetSize.Height, WICBitmapInterpolationMode.WICBitmapInterpolationModeHighQualityCubic);
                    source = scaler;
                }

                frameTarget.WriteSource(source);
                frameTarget.Commit();
            }

            encoder.Commit();
            output.Commit(STGC.STGC_DEFAULT);

            return new ConversionResult(outputFilePath);
        }

        // Helper method to copy metadata from the source frame to the target frame
        private static void CopyMetadata(IWICBitmapFrameDecode frame, IWICBitmapFrameEncode frameTarget, Func<string, bool>? filter = null)
        {
            var metadataReader = frame.GetMetadataQueryReader();
            var metadataWriter = frameTarget.GetMetadataQueryWriter();

            if (metadataReader == null || metadataWriter == null) return;

            foreach (var name in metadataReader.GetNamesRecursive())
            {
                try
                {
                    if (filter != null && !filter(name)) continue;
                    var val = metadataReader.GetMetadataByName(name);
                    var newName = TransformMetadataName(name);
                    metadataWriter.SetMetadataByName(newName, val);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Skipping metadata '{Name}'", name);
                }
            }

            var allProps = MetadataKeys.System.Concat(MetadataKeys.Photo).Concat(MetadataKeys.GPS);
            foreach (var prop in allProps)
            {
                try
                {
                    if (filter != null && !filter(prop)) continue;
                    var val = metadataReader.GetMetadataByName(prop);
                    metadataWriter.SetMetadataByName(prop, val);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Optional metadata skipped: {Prop}", prop);
                }
            }
        }

        // Helper method to transform metadata names for specific formats
        private static string TransformMetadataName(string name)
        {
            if (name.StartsWith("/ifd/"))
            {
                return "/app1" + name.Replace("/ifd/{ushort=34665}/", "/ifd/exif/").Replace("/ifd/{ushort=34853}/", "/ifd/gps/");
            }
            return name;
        }

        private static Guid GetFormatGuid(ImageFormat format)
        {
            return format switch
            {
                ImageFormat.PNG => ContainerFormat.Png,
                ImageFormat.JPEG => ContainerFormat.Jpeg,
                ImageFormat.TIFF => ContainerFormat.Tiff,
                ImageFormat.GIF => ContainerFormat.Gif,
                ImageFormat.WMP => ContainerFormat.Wmp,
                ImageFormat.HEIF => ContainerFormat.Heif,
                ImageFormat.BMP => ContainerFormat.Bmp,
                ImageFormat.WEBP => ContainerFormat.Webp,
                ImageFormat.ICO => ContainerFormat.Ico,
                _ => throw new ArgumentException($"Unsupported format: {format}")
            };
        }

        private static WICSize GetTargetSize(WICSize original, ConversionOptions options)
        {
            // Determine max dimensions from options or presets.
            int maxWidth = options.MaxWidth ?? options.Preset switch
            {
                ConversionPreset.WebOptimized => 1920,
                ConversionPreset.Thumbnail => 400,
                _ => 0
            };

            int maxHeight = options.MaxHeight ?? options.Preset switch
            {
                ConversionPreset.WebOptimized => 1080,
                ConversionPreset.Thumbnail => 400,
                _ => 0
            };

            if (maxWidth <= 0 && maxHeight <= 0)
            {
                // No resize configured.
                return original;
            }

            double scaleX = maxWidth > 0 ? (double)maxWidth / original.Width : double.PositiveInfinity;
            double scaleY = maxHeight > 0 ? (double)maxHeight / original.Height : double.PositiveInfinity;

            double scale = Math.Min(scaleX, scaleY);

            if (scale >= 1.0)
            {
                // Already within bounds; keep original size.
                return original;
            }

            int width = Math.Max(1, (int)Math.Round(original.Width * scale));
            int height = Math.Max(1, (int)Math.Round(original.Height * scale));

            return new WICSize(width, height);
        }

        private static void ConfigureEncoderOptions(IPropertyBag2? encoderOptions, ImageFormat targetFormat, ConversionOptions options)
        {
            if (encoderOptions is null)
            {
                return;
            }

            // Currently only JPEG quality is tuned.
            if (targetFormat != ImageFormat.JPEG)
            {
                return;
            }

            float quality = GetJpegQuality(options);

            try
            {
                encoderOptions.Write("ImageQuality", quality);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to set JPEG quality on encoder options.");
            }
        }

        private static float GetJpegQuality(ConversionOptions options)
        {
            if (options.Quality is int q)
            {
                // Clamp 1-100 and convert to 0.0-1.0 range.
                q = Math.Clamp(q, 1, 100);
                return q / 100f;
            }

            return options.Preset switch
            {
                ConversionPreset.WebOptimized => 0.82f,
                ConversionPreset.Thumbnail => 0.7f,
                ConversionPreset.Archive => 0.92f,
                _ => 0.9f
            };
        }

        private static bool ShouldKeepMetadata(string name, ConversionOptions options)
        {
            // Drop everything if metadata is disabled.
            if (!options.PreserveMetadata)
            {
                return false;
            }

            // If GPS stripping is off, keep all metadata.
            if (!options.StripGpsMetadata)
            {
                return true;
            }

            // Strip known GPS-related properties.
            if (name.StartsWith("System.GPS.", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (MetadataKeys.GPS.Contains(name))
            {
                return false;
            }

            // WIC EXIF GPS paths often contain "/gps/".
            if (name.Contains("/gps/", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}