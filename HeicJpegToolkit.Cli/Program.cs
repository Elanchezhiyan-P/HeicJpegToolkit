using HeicJpegToolkit.Helpers.Enums;

if (args.Length < 2)
{
    Console.WriteLine("Usage: heicjpeg <inputFile> <outputFolder> [format]");
    Console.WriteLine("Example: heicjpeg photo.heic ./output jpeg");
    return;
}

var input = args[0];
var outputFolder = args[1];
var format = ImageFormat.JPEG;

if (args.Length >= 3 && Enum.TryParse<ImageFormat>(args[2], true, out var parsed))
{
    format = parsed;
}

var result = HeicJpegToolkit.HeicJpegToolkit.ConvertFile(input, outputFolder, format);

if (result.Success)
{
    Console.WriteLine($"Converted successfully: {result.OutputFilePath}");
}
else
{
    Console.WriteLine($"Conversion failed: {result.Reason}");
}

