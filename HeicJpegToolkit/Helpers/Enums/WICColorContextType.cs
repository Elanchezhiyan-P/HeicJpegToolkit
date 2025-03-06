using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICColorContextType : int
    {
        WICColorContextUninitialized = 0x00000000,
        WICColorContextProfile = 0x00000001,
        WICColorContextExifColorSpace = 0x00000002,
    }
}
