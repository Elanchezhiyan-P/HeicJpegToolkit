using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICBitmapEncoderCacheOption : int
    {
        WICBitmapEncoderCacheInMemory = 0x00000000,
        WICBitmapEncoderCacheTempFile = 0x00000001,
        WICBitmapEncoderNoCache = 0x00000002,
    }
}
