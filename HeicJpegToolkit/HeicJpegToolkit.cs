using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Helpers.Extensions;
using HeicJpegToolkit.Helpers.Result;
using HeicJpegToolkit.Helpers.Utils;
using HeicJpegToolkit.Interfaces;
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

            try
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
                    progressCallback?.Invoke($"Processing frame {i + 1}/{decoder.GetFrameCount()}");

                    var frame = decoder.GetFrame(i);
                    encoder.CreateNewFrame(out var frameTarget, null);
                    frameTarget.Initialize(null);
                    frameTarget.SetSize(frame.GetSize());
                    frameTarget.SetResolution(frame.GetResolution());
                    frameTarget.SetPixelFormat(frame.GetPixelFormat());

                    CopyMetadata(frame, frameTarget, metadataFilter);

                    frameTarget.WriteSource(frame);
                    frameTarget.Commit();
                }

                encoder.Commit();
                output.Commit(STGC.STGC_DEFAULT);

                return new ConversionResult(outputFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Conversion failed.");
                return new ConversionResult($"Conversion error: {ex.Message}", ex.ToString());
            }
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
    }
}