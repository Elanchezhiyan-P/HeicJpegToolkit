using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Helpers.Extensions;
using HeicJpegToolkit.Helpers.Result;
using HeicJpegToolkit.Helpers.Utils;
using Decoder = HeicJpegToolkit.Helpers.Constants.Decoder;

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
            "System.Title"
    };

        private static readonly string[] SystemPhotoProperties = {
            //"System.Photo.Aperture",
            "System.Photo.ApertureDenominator",
            "System.Photo.ApertureNumerator",
            //"System.Photo.Brightness",
            "System.Photo.BrightnessDenominator",
            "System.Photo.BrightnessNumerator",
            "System.Photo.CameraManufacturer",
            "System.Photo.CameraModel",
            "System.Photo.CameraSerialNumber",
            "System.Photo.Contrast",
            //"System.Photo.ContrastText",
            "System.Photo.DateTaken",
            //"System.Photo.DigitalZoom",
            "System.Photo.DigitalZoomDenominator",
            "System.Photo.DigitalZoomNumerator",
            "System.Photo.Event",
            "System.Photo.EXIFVersion",
            //"System.Photo.ExposureBias",
            "System.Photo.ExposureBiasDenominator",
            "System.Photo.ExposureBiasNumerator",
            //"System.Photo.ExposureIndex",
            "System.Photo.ExposureIndexDenominator",
            "System.Photo.ExposureIndexNumerator",
            "System.Photo.ExposureProgram",
            //"System.Photo.ExposureProgramText",
            //"System.Photo.ExposureTime",
            "System.Photo.ExposureTimeDenominator",
            "System.Photo.ExposureTimeNumerator",
            "System.Photo.Flash",
            //"System.Photo.FlashEnergy",
            "System.Photo.FlashEnergyDenominator",
            "System.Photo.FlashEnergyNumerator",
            "System.Photo.FlashManufacturer",
            "System.Photo.FlashModel",
            //"System.Photo.FlashText",
            //"System.Photo.FNumber",
            "System.Photo.FNumberDenominator",
            "System.Photo.FNumberNumerator",
            //"System.Photo.FocalLength",
            "System.Photo.FocalLengthDenominator",
            "System.Photo.FocalLengthInFilm",
            "System.Photo.FocalLengthNumerator",
            //"System.Photo.FocalPlaneXResolution",
            "System.Photo.FocalPlaneXResolutionDenominator",
            "System.Photo.FocalPlaneXResolutionNumerator",
            //"System.Photo.FocalPlaneYResolution",
            "System.Photo.FocalPlaneYResolutionDenominator",
            "System.Photo.FocalPlaneYResolutionNumerator",
            //"System.Photo.GainControl",
            "System.Photo.GainControlDenominator",
            "System.Photo.GainControlNumerator",
            //"System.Photo.GainControlText",
            "System.Photo.ISOSpeed",
            "System.Photo.LensManufacturer",
            "System.Photo.LensModel",
            "System.Photo.LightSource",
            "System.Photo.MakerNote",
            "System.Photo.MakerNoteOffset",
            //"System.Photo.MaxAperture",
            "System.Photo.MaxApertureDenominator",
            "System.Photo.MaxApertureNumerator",
            "System.Photo.MeteringMode",
            //"System.Photo.MeteringModeText",
            "System.Photo.Orientation",
            //"System.Photo.OrientationText",
            //"System.Photo.PeopleNames",
            //"System.Photo.PhotometricInterpretation",
            //"System.Photo.PhotometricInterpretationText",
            "System.Photo.ProgramMode",
            //"System.Photo.ProgramModeText",
            //"System.Photo.RelatedSoundFile",
            "System.Photo.Saturation",
            //"System.Photo.SaturationText",
            "System.Photo.Sharpness",
            //"System.Photo.SharpnessText",
            //"System.Photo.ShutterSpeed",
            "System.Photo.ShutterSpeedDenominator",
            "System.Photo.ShutterSpeedNumerator",
            //"System.Photo.SubjectDistance",
            "System.Photo.SubjectDistanceDenominator",
            "System.Photo.SubjectDistanceNumerator",
            //"System.Photo.TagViewAggregate",
            "System.Photo.TranscodedForSync",
            "System.Photo.WhiteBalance",
            //"System.Photo.WhiteBalanceText"
    };

        private static readonly string[] SystemGpsProperties = {
            //"System.GPS.Altitude",
            "System.GPS.AltitudeDenominator",
            "System.GPS.AltitudeNumerator",
            "System.GPS.AltitudeRef",
            "System.GPS.AreaInformation",
            "System.GPS.Date",
            //"System.GPS.DestBearing",
            "System.GPS.DestBearingDenominator",
            "System.GPS.DestBearingNumerator",
            "System.GPS.DestBearingRef",
            //"System.GPS.DestDistance",
            "System.GPS.DestDistanceDenominator",
            "System.GPS.DestDistanceNumerator",
            "System.GPS.DestDistanceRef",
            //"System.GPS.DestLatitude",
            "System.GPS.DestLatitudeDenominator",
            "System.GPS.DestLatitudeNumerator",
            "System.GPS.DestLatitudeRef",
            //"System.GPS.DestLongitude",
            "System.GPS.DestLongitudeDenominator",
            "System.GPS.DestLongitudeNumerator",
            "System.GPS.DestLongitudeRef",
            "System.GPS.Differential",
            //"System.GPS.DOP",
            "System.GPS.DOPDenominator",
            "System.GPS.DOPNumerator",
            //"System.GPS.ImgDirection",
            "System.GPS.ImgDirectionDenominator",
            "System.GPS.ImgDirectionNumerator",
            "System.GPS.ImgDirectionRef",
            //"System.GPS.Latitude",
            //"System.GPS.LatitudeDecimal",
            "System.GPS.LatitudeDenominator",
            "System.GPS.LatitudeNumerator",
            "System.GPS.LatitudeRef",
            //"System.GPS.Longitude",
            //"System.GPS.LongitudeDecimal",
            "System.GPS.LongitudeDenominator",
            "System.GPS.LongitudeNumerator",
            "System.GPS.LongitudeRef",
            "System.GPS.MapDatum",
            "System.GPS.MeasureMode",
            "System.GPS.ProcessingMethod",
            "System.GPS.Satellites",
            //"System.GPS.Speed",
            "System.GPS.SpeedDenominator",
            "System.GPS.SpeedNumerator",
            "System.GPS.SpeedRef",
            "System.GPS.Status",
            //"System.GPS.Track",
            "System.GPS.TrackDenominator",
            "System.GPS.TrackNumerator",
            "System.GPS.TrackRef",
            "System.GPS.VersionID"
    };

        // Public method that initiates the conversion and accepts input file and output folder
        public static async Task<ConversionResult> ConvertFile(string inputFilePath, string outputFolderPath)
        {
            if (!OperatingSystem.IsWindows())
            {
                return new ConversionResult("This tool is designed to work only on Windows OS. You can run it on a Windows PC, or alternatively, search for a similar tool available for your operating system.");
            }

            try
            {
                // Ensure the input file is valid
                if (string.IsNullOrWhiteSpace(inputFilePath) || !File.Exists(inputFilePath))
                {
                    return new ConversionResult("Invalid input file path.", "Input file does not exist or is invalid.");
                }

                // Ensure the output folder exists, or create it if necessary
                if (string.IsNullOrWhiteSpace(outputFolderPath))
                {
                    return new ConversionResult("Invalid output folder path.", "Output folder path is empty or invalid.");
                }

                if (!Directory.Exists(outputFolderPath))
                {
                    Directory.CreateDirectory(outputFolderPath);
                }

                // Get the full path of the input file
                inputFilePath = Path.GetFullPath(inputFilePath);

                // Prepare the output file path by combining the output folder with the desired file name
                string outputFilePath = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(inputFilePath) + ".jpg");

                var imagingFactory = new WICImagingFactory();
                var decoder = imagingFactory.CreateDecoderFromFilename(inputFilePath, Guid.Empty, StreamAccessMode.GENERIC_READ,
                    WICDecodeOptions.WICDecodeMetadataCacheOnLoad);

                // If the file is already a JPEG, skip the conversion
                if (decoder.GetDecoderInfo().GetCLSID() == Decoder.Jpeg)
                {
                    return new ConversionResult(outputFilePath)
                    {
                        Reason = "File is already in JPEG format, no conversion needed."
                    };
                }

                var output = imagingFactory.CreateStream();
                output.InitializeFromFilename(outputFilePath, StreamAccessMode.GENERIC_WRITE);
                var encoder = imagingFactory.CreateEncoder(ContainerFormat.Jpeg);
                encoder.Initialize(output, WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

                for (int i = 0; i < decoder.GetFrameCount(); i++)
                {
                    var frame = decoder.GetFrame(i);
                    encoder.CreateNewFrame(out var frameJpg, null);
                    frameJpg.Initialize(null);
                    frameJpg.SetSize(frame.GetSize());
                    frameJpg.SetResolution(frame.GetResolution());
                    frameJpg.SetPixelFormat(frame.GetPixelFormat());

                    var reader = frame.AsMetadataBlockReader();

                    // Get the EXIF data from the original photo.
                    var metadataReader = frame.GetMetadataQueryReader();
                    var metadataWriter = frameJpg.GetMetadataQueryWriter();
                    foreach (var name in metadataReader.GetNamesRecursive())
                    {
                        try
                        {
                            var val = metadataReader.GetMetadataByName(name);
                            if (name.StartsWith("/ifd/"))
                                metadataWriter.SetMetadataByName("/app1" + name.Replace("/ifd/{ushort=34665}/", "/ifd/exif/").Replace("/ifd/{ushort=34853}/", "/ifd/gps/"), val);
                            else if (name.StartsWith("/xmp/"))
                                metadataWriter.SetMetadataByName(name, val);
                        }
                        catch
                        {
                            System.Diagnostics.Trace.WriteLine($"Error setting '{name}'");
                        }
                    }

                    // Handle SystemProperties, SystemPhotoProperties, and SystemGpsProperties
                    var photoProperties = SystemProperties.Concat(SystemPhotoProperties.Concat(SystemGpsProperties));
                    foreach (var photoProp in photoProperties)
                    {
                        var action = "getting";
                        try
                        {
                            var val = metadataReader.GetMetadataByName(photoProp);
                            action = "setting";
                            metadataWriter.SetMetadataByName(photoProp, val);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Trace.WriteLine($"Error {action} '{photoProp}': " + ex.Message);
                        }
                    }

                    // Write the frame to the output
                    frameJpg.WriteSource(frame);
                    frameJpg.Commit();
                    frame = null;
                    frameJpg = null;
                }

                encoder.Commit();
                output.Commit(STGC.STGC_DEFAULT);
                encoder = null;
                output = null;

                return new ConversionResult(outputFilePath);
            }
            catch (Exception ex)
            {
                return new ConversionResult($"Error converting file '{inputFilePath}': {ex.Message}", ex.ToString());
            }
        }
    }
}
