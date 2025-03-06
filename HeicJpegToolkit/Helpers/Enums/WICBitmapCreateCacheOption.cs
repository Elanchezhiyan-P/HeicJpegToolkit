using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICBitmapCreateCacheOption : int
    {
        WICBitmapNoCache = 0x00000000,
        WICBitmapCacheOnDemand = 0x00000001,
        WICBitmapCacheOnLoad = 0x00000002,
    }
}
