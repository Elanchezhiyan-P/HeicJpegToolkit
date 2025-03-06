using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    [Flags]
    public enum WICBitmapLockFlags : int
    {
        WICBitmapLockRead = 0x00000001,
        WICBitmapLockWrite = 0x00000002,
    }
}
