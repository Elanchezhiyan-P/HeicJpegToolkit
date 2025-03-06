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
    public static class IWICBitmapLockExtensions
    {
        public static WICSize GetSize(this IWICBitmapLock bitmapLock)
        {
            int width, height;
            bitmapLock.GetSize(out width, out height);
            return new WICSize(width, height);
        }
    }
}
