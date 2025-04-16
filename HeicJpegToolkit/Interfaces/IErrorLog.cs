using HeicJpegToolkit.Helpers.Constants;
using System.Runtime.InteropServices;

namespace HeicJpegToolkit.Interfaces
{
    [ComImport]
    [Guid(IID.IErrorLog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IErrorLog
    {
        void AddError(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            [In] nint pExcepInfo); // EXCEPINFO*
    }
}
