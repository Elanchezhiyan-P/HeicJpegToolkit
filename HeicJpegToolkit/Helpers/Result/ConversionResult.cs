namespace HeicJpegToolkit.Helpers.Result
{
    public class ConversionResult
    {
        public string? OutputFilePath { get; set; }
        public string? Reason { get; set; }
        public bool Success => string.IsNullOrEmpty(Reason);

        public ConversionResult(string path) => OutputFilePath = path;
        public ConversionResult(string message, string? reason = null)
        {
            OutputFilePath = null;
            Reason = reason ?? message;
        }
    }
}
