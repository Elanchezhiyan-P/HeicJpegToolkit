using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    [Flags]
    public enum WICComponentType : int
    {
        WICDecoder = 0x00000001,
        WICEncoder = 0x00000002,
        WICPixelFormatConverter = 0x00000004,
        WICMetadataReader = 0x00000008,
        WICMetadataWriter = 0x00000010,
        WICPixelFormat = 0x00000020,
        WICAllComponents = 0x0000003F,
    }
}
