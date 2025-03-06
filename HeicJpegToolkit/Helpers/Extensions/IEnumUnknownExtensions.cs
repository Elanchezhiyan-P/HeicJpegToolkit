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
    public static class IEnumUnknownExtensions
    {
        public static IEnumerable<object> AsEnumerable(this IEnumUnknown enumUnknown)
        {
            var buffer = new object[1];
            for (; ; )
            {
                int length;
                enumUnknown.Next(1, buffer, out length);
                if (length != 1) break;
                yield return buffer[0];
            }
        }
    }
}
