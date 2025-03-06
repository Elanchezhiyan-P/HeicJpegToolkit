using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICBitmapAlphaChannelOption : int
    {
        WICBitmapUseAlpha = 0x00000000,
        WICBitmapUsePremultipliedAlpha = 0x00000001,
        WICBitmapIgnoreAlpha = 0x00000002,
    }
}
