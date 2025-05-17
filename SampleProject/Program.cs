using HeicJpegToolkit.Helpers.Enums;

namespace SampleProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = ""; //Enter the input file path
            string outputFolderPath = string.Empty;

            var result = HeicJpegToolkit.HeicJpegToolkit.ConvertFile(inputFilePath, outputFolderPath, ImageFormat.JPEG);

            if (result.IsConverted)
            {
                Console.WriteLine($"Image has been converterd and placed in the folder {result.OutputFilePath}");
            }
            else
            {
                Console.WriteLine($"Image conversion failed. Reason: {result.Reason}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
