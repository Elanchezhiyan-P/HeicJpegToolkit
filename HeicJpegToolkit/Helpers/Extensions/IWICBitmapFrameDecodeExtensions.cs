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
    public static class IWICBitmapFrameDecodeExtensions
    {
        public static IWICMetadataBlockReader AsMetadataBlockReader(this IWICBitmapFrameDecode bitmapFrameDecode)
        {
            return (IWICMetadataBlockReader)bitmapFrameDecode;
        }

        public static IWICColorContext[] GetColorContexts(this IWICBitmapFrameDecode bitmapFrameDecode)
        {
            var wic = new WICImagingFactory();

            bitmapFrameDecode.GetColorContexts(0, null, out int length);

            var colorContexts = new IWICColorContext[length];

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    colorContexts[i] = wic.CreateColorContext();
                }

                bitmapFrameDecode.GetColorContexts(length, colorContexts, out _);
            }

            return colorContexts;
        }

    }
}
