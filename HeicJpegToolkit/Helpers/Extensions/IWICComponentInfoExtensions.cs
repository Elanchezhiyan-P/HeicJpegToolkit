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
    public static class IWICComponentInfoExtensions
    {
        public static string GetAuthor(this IWICComponentInfo componentInfo)
        {
            return FetchIntoBufferHelper.FetchString(componentInfo.GetAuthor);
        }

        public static string GetFriendlyName(this IWICComponentInfo componentInfo)
        {
            return FetchIntoBufferHelper.FetchString(componentInfo.GetFriendlyName);
        }

        public static string GetVersion(this IWICComponentInfo componentInfo)
        {
            return FetchIntoBufferHelper.FetchString(componentInfo.GetVersion);
        }

        public static string GetSpecVersion(this IWICComponentInfo componentInfo)
        {
            return FetchIntoBufferHelper.FetchString(componentInfo.GetSpecVersion);
        }
    }
}

