using HeicJpegToolkit.Helpers.Utils;
using HeicJpegToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Helpers.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICPaletteExtensions
    {
        public static int[] GetColors(this IWICPalette palette)
        {
            return FetchIntoBufferHelper.FetchArray<int>(palette.GetColors);
        }

        public static void InitializeCustom(this IWICPalette palette, int[] pColors)
        {
            palette.InitializeCustom(pColors, pColors.Length);
        }
    }
}
