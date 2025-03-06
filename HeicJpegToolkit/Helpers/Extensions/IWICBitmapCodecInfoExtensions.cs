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
    public static class IWICBitmapCodecInfoExtensions
    {
        public static string GetColorManagementVersion(this IWICBitmapCodecInfo bitmapCodecInfo)
        {
            return FetchIntoBufferHelper.FetchString(bitmapCodecInfo.GetColorManagementVersion);
        }

        public static string GetDeviceManufacturer(this IWICBitmapCodecInfo bitmapCodecInfo)
        {
            return FetchIntoBufferHelper.FetchString(bitmapCodecInfo.GetDeviceManufacturer);
        }

        public static string GetDeviceModels(this IWICBitmapCodecInfo bitmapCodecInfo)
        {
            return FetchIntoBufferHelper.FetchString(bitmapCodecInfo.GetDeviceModels);
        }

        public static string[] GetMimeTypes(this IWICBitmapCodecInfo bitmapCodecInfo)
        {
            return FetchIntoBufferHelper.FetchString(bitmapCodecInfo.GetMimeTypes).Split(',');
        }

        public static string[] GetFileExtensions(this IWICBitmapCodecInfo bitmapCodecInfo)
        {
            return FetchIntoBufferHelper.FetchString(bitmapCodecInfo.GetFileExtensions).Split(',');
        }
    }
}
