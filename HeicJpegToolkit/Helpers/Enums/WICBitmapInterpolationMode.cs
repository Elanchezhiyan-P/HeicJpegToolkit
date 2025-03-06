using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Enums
{
    public enum WICBitmapInterpolationMode : int
    {
        WICBitmapInterpolationModeNearestNeighbor = 0x00000000,
        WICBitmapInterpolationModeLinear = 0x00000001,
        WICBitmapInterpolationModeCubic = 0x00000002,
        WICBitmapInterpolationModeFant = 0x00000003,
        WICBitmapInterpolationModeHighQualityCubic = 0x00000004,
    }
}
