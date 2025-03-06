using HeicJpegToolkit.Helpers.Enums;
using HeicJpegToolkit.Interfaces;
using HeicJpegToolkit.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICBitmapScalerExtensions
    {
        public static void Initialize(this IWICBitmapScaler bitmapScaler, IWICBitmapSource pISource, WICSize size, WICBitmapInterpolationMode mode)
        {
            bitmapScaler.Initialize(pISource, size.Width, size.Height, mode);
        }
    }
}
