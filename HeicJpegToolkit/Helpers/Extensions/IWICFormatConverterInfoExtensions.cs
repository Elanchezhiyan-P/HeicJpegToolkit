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
    public static class IWICFormatConverterInfoExtensions
    {
        public static Guid[] GetPixelFormats(this IWICFormatConverterInfo formatConverterInfo)
        {
            return FetchIntoBufferHelper.FetchArray<Guid>(formatConverterInfo.GetPixelFormats);
        }

    }
}