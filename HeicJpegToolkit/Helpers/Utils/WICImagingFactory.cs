using HeicJpegToolkit.Helpers.Constants;
using HeicJpegToolkit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
