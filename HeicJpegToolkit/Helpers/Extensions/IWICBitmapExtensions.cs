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
    public static class IWICBitmapExtensions
    {
        public static IWICBitmapLock Lock(this IWICBitmap bitmap, WICBitmapLockFlags flags, WICRect? prcLock = null)
        {
            using (var prcLockPtr = CoTaskMemPtr.From(prcLock))
            {
                return bitmap.Lock(prcLockPtr, flags);
            }
        }

        public static void SetResolution(this IWICBitmap bitmap, Resolution resolution)
        {
            bitmap.SetResolution(resolution.DpiX, resolution.DpiY);
        }
    }
}

