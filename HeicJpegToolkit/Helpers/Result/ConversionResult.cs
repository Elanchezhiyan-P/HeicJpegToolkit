using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Result
{
    public class ConversionResult
    {
        public bool IsConverted { get; set; }  // Indicates whether the conversion was successful or not
        public string OutputFilePath { get; set; }  // Path of the output file (if conversion is successful)
        public string ErrorMessage { get; set; }  // Error message if something went wrong during the conversion
        public string Reason { get; set; }  // Reason for failure or additional information

        // Constructor for success result
        public ConversionResult(string outputFilePath)
        {
            IsConverted = true;
            OutputFilePath = outputFilePath;
            ErrorMessage = string.Empty;
            Reason = "Conversion successful";
        }

        // Constructor for failure result
        public ConversionResult(string errorMessage, string reason = "")
        {
            IsConverted = false;
            OutputFilePath = string.Empty;
            ErrorMessage = errorMessage;
            Reason = reason;
        }
    }
}
