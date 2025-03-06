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
    public static class IWICMetadataHandlerInfoExtensions
    {
        public static Guid[] GetContainerFormats(this IWICMetadataHandlerInfo metadataHandlerInfo)
        {
            return FetchIntoBufferHelper.FetchArray<Guid>(metadataHandlerInfo.GetContainerFormats);
        }

        public static string GetDeviceManufacturer(this IWICMetadataHandlerInfo metadataHandlerInfo)
        {
            return FetchIntoBufferHelper.FetchString(metadataHandlerInfo.GetDeviceManufacturer);
        }

        public static string GetDeviceModels(this IWICMetadataHandlerInfo metadataHandlerInfo)
        {
            return FetchIntoBufferHelper.FetchString(metadataHandlerInfo.GetDeviceModels);
        }
    }
}
