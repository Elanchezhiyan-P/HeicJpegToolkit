namespace SampleProject
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string inputFilePath = "";
            string outputFolderPath = string.Empty;

            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("File doesn't exists");
                Exit();
                return;
            }

            if(!Path.GetExtension(inputFilePath).Equals(".heic", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("The file is not in HEIC format");
                Exit();
                return;
            }

            if (!Directory.Exists(outputFolderPath))
            {
                Directory.CreateDirectory(outputFolderPath);
            }
            
            var result = await HeicJpegToolkit.HeicJpegToolkit.ConvertFile(inputFilePath, outputFolderPath);

            if (result.IsConverted)
            {
                Console.WriteLine($"Image has been converterd and placed in the folder {result.OutputFilePath}");
            }
            else
            {
                Console.WriteLine($"Image conversion failed. Reason: {result.Reason}");
            }
            Exit();
        }

        static void Exit()
        {
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
