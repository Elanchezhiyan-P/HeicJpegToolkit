using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICDecodeOptions : int
    {
        WICDecodeMetadataCacheOnDemand = 0x00000000,
        WICDecodeMetadataCacheOnLoad = 0x00000001,
    }
}
