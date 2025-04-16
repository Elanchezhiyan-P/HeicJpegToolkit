using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Helpers.Extensions;
using HeicJpegToolkit.Helpers.Result;
using HeicJpegToolkit.Helpers.Utils;
using HeicJpegToolkit.Interfaces;

namespace HeicJpegToolkit
{
    public class HeicJpegToolkit
    {
        // Keep system properties private, as they're meant for internal use
        private static readonly string[] SystemProperties = {
                "System.ApplicationName",
                "System.Author",
                "System.Comment",
                "System.Copyright",
                "System.DateAcquired",
                "System.Keywords",
                "System.Rating",
                "System.SimpleRating",
                "System.Subject",
                "System.Title",
                "System.FileSize",                // Added file size property
                "System.DateCreated",             // Added creation date
                "System.DateModified",            // Added modified date
                "System.OriginalFileName",        // Added original file name
                "System.FileExtension",           // Added file extension
                "System.GPSLatitude",            // GPS latitude if it's an image or geotagged file
                "System.GPSLongitude",           // GPS longitude if it's an image or geotagged file
                "System.CameraModel",             // Camera model for images (EXIF)
                "System.CameraMake",              // Camera make for images (EXIF)
                "System.FlashUsed",               // Flash used for images (EXIF)
                "System.ImageWidth",              // Image width for images (EXIF)
                "System.ImageHeight"              // Image height for images (EXIF)
            };

        private static readonly string[] SystemPhotoProperties = {
                "System.Photo.ApertureDenominator",
                "System.Photo.ApertureNumerator",
                "System.Photo.BrightnessDenominator",
                "System.Photo.BrightnessNumerator",
                "System.Photo.CameraManufacturer",
                "System.Photo.CameraModel",
                "System.Photo.CameraSerialNumber",
                "System.Photo.Contrast",
                "System.Photo.DateTaken",
                "System.Photo.DigitalZoomDenominator",
                "System.Photo.DigitalZoomNumerator",
                "System.Photo.Event",
                "System.Photo.EXIFVersion",
                "System.Photo.ExposureBiasDenominator",
                "System.Photo.ExposureBiasNumerator",
                "System.Photo.ExposureIndexDenominator",
                "System.Photo.ExposureIndexNumerator",
                "System.Photo.ExposureProgram",
                "System.Photo.ExposureTimeDenominator",
                "System.Photo.ExposureTimeNumerator",
                "System.Photo.Flash",
                "System.Photo.FlashEnergyDenominator",
                "System.Photo.FlashEnergyNumerator",
                "System.Photo.FlashManufacturer",
                "System.Photo.FlashModel",
                "System.Photo.FNumberDenominator",
                "System.Photo.FNumberNumerator",
                "System.Photo.FocalLengthDenominator",
                "System.Photo.FocalLengthInFilm",
                "System.Photo.FocalLengthNumerator",
                "System.Photo.FocalPlaneXResolutionDenominator",
                "System.Photo.FocalPlaneXResolutionNumerator",
                "System.Photo.FocalPlaneYResolutionDenominator",
                "System.Photo.FocalPlaneYResolutionNumerator",
                "System.Photo.GainControlDenominator",
                "System.Photo.GainControlNumerator",
                "System.Photo.ISOSpeed",
                "System.Photo.LensManufacturer",
                "System.Photo.LensModel",
                "System.Photo.LightSource",
                "System.Photo.MakerNote",
                "System.Photo.MakerNoteOffset",
                "System.Photo.MaxApertureDenominator",
                "System.Photo.MaxApertureNumerator",
                "System.Photo.MeteringMode",
                "System.Photo.Orientation",
                "System.Photo.ProgramMode",
                "System.Photo.Saturation",
                "System.Photo.Sharpness",
                "System.Photo.ShutterSpeedDenominator",
                "System.Photo.ShutterSpeedNumerator",
                "System.Photo.SubjectDistanceDenominator",
                "System.Photo.SubjectDistanceNumerator",
                "System.Photo.TranscodedForSync",
                "System.Photo.WhiteBalance",
                "System.Photo.ShutterSpeed",
                "System.Photo.FocalLength",
                "System.Photo.DateTimeOriginal"
            };

        private static readonly string[] SystemGpsProperties = {
                "System.GPS.AltitudeDenominator",
                "System.GPS.AltitudeNumerator",
                "System.GPS.AltitudeRef",
                "System.GPS.AreaInformation",
                "System.GPS.Date",
                "System.GPS.DestBearingDenominator",
                "System.GPS.DestBearingNumerator",
                "System.GPS.DestBearingRef",
                "System.GPS.DestDistanceDenominator",
                "System.GPS.DestDistanceNumerator",
                "System.GPS.DestDistanceRef",
                "System.GPS.DestLatitudeDenominator",
                "System.GPS.DestLatitudeNumerator",
                "System.GPS.DestLatitudeRef",
                "System.GPS.DestLongitudeDenominator",
                "System.GPS.DestLongitudeNumerator",
                "System.GPS.DestLongitudeRef",
                "System.GPS.Differential",
                "System.GPS.DOPDenominator",
                "System.GPS.DOPNumerator",
                "System.GPS.ImgDirectionDenominator",
                "System.GPS.ImgDirectionNumerator",
                "System.GPS.ImgDirectionRef",
                "System.GPS.LatitudeDenominator",
                "System.GPS.LatitudeNumerator",
                "System.GPS.LatitudeRef",
                "System.GPS.LongitudeDenominator",
                "System.GPS.LongitudeNumerator",
                "System.GPS.LongitudeRef",
                "System.GPS.MapDatum",
                "System.GPS.MeasureMode",
                "System.GPS.ProcessingMethod",
                "System.GPS.Satellites",
                "System.GPS.SpeedDenominator",
                "System.GPS.SpeedNumerator",
                "System.GPS.SpeedRef",
                "System.GPS.Status",
                "System.GPS.TrackDenominator",
                "System.GPS.TrackNumerator",
                "System.GPS.TrackRef",
                "System.GPS.VersionID",
                "System.GPS.LatitudeDecimal",
                "System.GPS.LongitudeDecimal",
                "System.GPS.Time"
            };

        // Public method that initiates the conversion and accepts input file and output folder
        public static ConversionResult ConvertFile(string inputFilePath, string outputFolderPath, ImageFormat targetFormat = ImageFormat.JPEG)
        {
            if (!OperatingSystem.IsWindows())
            {
                return new ConversionResult("This tool is designed to work only on Windows OS. You can run it on a Windows PC, or alternatively, search for a similar tool available for your operating system.");
            }

            // Check for valid file paths
            if (string.IsNullOrWhiteSpace(inputFilePath) || !File.Exists(inputFilePath))
            {
                return new ConversionResult("Invalid input file path.", "Input file does not exist or is invalid.");
            }

            if (string.IsNullOrWhiteSpace(outputFolderPath))
            {
                return new ConversionResult("Invalid output folder path.", "Output folder path is empty or invalid.");
            }

            try
            {
                // Ensure the output folder exists
                Directory.CreateDirectory(outputFolderPath);

                // Get the full path of the input file
                inputFilePath = Path.GetFullPath(inputFilePath);

                var imagingFactory = new WICImagingFactory();
                var decoder = imagingFactory.CreateDecoderFromFilename(inputFilePath, Guid.Empty, StreamAccessMode.GENERIC_READ,
                    WICDecodeOptions.WICDecodeMetadataCacheOnLoad);

                var formatGuid = GetFormatGuid(targetFormat);
                string outputFilePath = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(inputFilePath) + "." + targetFormat.ToString().ToLower());

                // Skip conversion if the input format matches the target format
                if (decoder.GetDecoderInfo().GetCLSID() == formatGuid)
                {
                    return new ConversionResult(outputFilePath)
                    {
                        Reason = $"File is already in {targetFormat.ToString().ToUpper()} format, no conversion needed."
                    };
                }

                IWICStream output = imagingFactory.CreateStream();
                output.InitializeFromFilename(outputFilePath, StreamAccessMode.GENERIC_WRITE);
                var encoder = imagingFactory.CreateEncoder(formatGuid);
                encoder.Initialize(output, WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

                for (int i = 0; i < decoder.GetFrameCount(); i++)
                {
                    var frame = decoder.GetFrame(i);
                    encoder.CreateNewFrame(out var frameTarget, null);
                    frameTarget.Initialize(null);
                    frameTarget.SetSize(frame.GetSize());
                    frameTarget.SetResolution(frame.GetResolution());
                    frameTarget.SetPixelFormat(frame.GetPixelFormat());

                    // Copy metadata
                    CopyMetadata(frame, frameTarget);

                    // Write the frame to the output
                    frameTarget.WriteSource(frame);
                    frameTarget.Commit();
                }

                encoder.Commit();
                output.Commit(STGC.STGC_DEFAULT);

                return new ConversionResult(outputFilePath);
            }
            catch (FileNotFoundException fnfEx)
            {
                return new ConversionResult($"File not found: {fnfEx.Message}", fnfEx.ToString());
            }
            catch (UnauthorizedAccessException uaEx)
            {
                return new ConversionResult($"Access denied: {uaEx.Message}", uaEx.ToString());
            }
            catch (Exception ex)
            {
                return new ConversionResult($"Error converting file '{inputFilePath}': {ex.Message}", ex.ToString());
            }
        }

        // Helper method to copy metadata from the source frame to the target frame
        private static void CopyMetadata(IWICBitmapFrameDecode frame, IWICBitmapFrameEncode frameTarget)
        {
            var metadataReader = frame.GetMetadataQueryReader();
            var metadataWriter = frameTarget.GetMetadataQueryWriter();

            if (metadataReader == null || metadataWriter == null)
            {
                System.Diagnostics.Trace.WriteLine("Metadata reader or writer is null.");
                return;
            }

            try
            {
                foreach (var name in metadataReader.GetNamesRecursive())
                {
                    try
                    {
                        var val = metadataReader.GetMetadataByName(name);
                        string newName = TransformMetadataName(name);
                        metadataWriter.SetMetadataByName(newName, val);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine($"Error setting metadata '{name}': {ex.Message}");
                    }
                }

                // Handle SystemProperties, SystemPhotoProperties, and SystemGpsProperties
                var photoProperties = SystemProperties.Concat(SystemPhotoProperties).Concat(SystemGpsProperties);
                foreach (var photoProp in photoProperties)
                {
                    try
                    {
                        var val = metadataReader.GetMetadataByName(photoProp);
                        metadataWriter.SetMetadataByName(photoProp, val);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Trace.WriteLine($"Error setting '{photoProp}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Error during metadata copying: {ex.Message}");
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
                _ => throw new ArgumentException("Unsupported format"),
            };
        }
    }
}