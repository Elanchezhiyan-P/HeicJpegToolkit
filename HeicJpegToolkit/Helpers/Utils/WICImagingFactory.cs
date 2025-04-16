using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Interfaces;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Helpers.Utils
{
    [ComImport]
    [Guid(IID.IWICImagingFactory)]
    [CoClass(typeof(WICImagingFactoryClass))]
    public interface WICImagingFactory : IWICImagingFactory { }

    [ComImport]
    [Guid(CLSID.WICImagingFactory)]
    [ComDefaultInterface(typeof(IWICImagingFactory))]
    public class WICImagingFactoryClass { }
}
