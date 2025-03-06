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
    public static class IWICColorContextExtensions
    {
        public static void InitializeFromMemory(this IWICColorContext colorContext, byte[] pbBuffer)
        {
            colorContext.InitializeFromMemory(pbBuffer, pbBuffer.Length);
        }

        public static byte[] GetProfileBytes(this IWICColorContext colorContext)
        {
            return FetchIntoBufferHelper.FetchArray<byte>(colorContext.GetProfileBytes);
        }
    }
}
