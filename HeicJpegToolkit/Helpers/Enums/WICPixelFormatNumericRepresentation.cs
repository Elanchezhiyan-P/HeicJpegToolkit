using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICPixelFormatNumericRepresentation
    {
        WICPixelFormatNumericRepresentationUnspecified = 0x00000000,
        WICPixelFormatNumericRepresentationIndexed = 0x00000001,
        WICPixelFormatNumericRepresentationUnsignedInteger = 0x00000002,
        WICPixelFormatNumericRepresentationSignedInteger = 0x00000003,
        WICPixelFormatNumericRepresentationFixed = 0x00000004,
        WICPixelFormatNumericRepresentationFloat = 0x00000005,
        WICPixelFormatNumericRepresentation_FORCE_DWORD = 2147483647
    }
}
