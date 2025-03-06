using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Structures
{
    public sealed class WICBitmapPattern
    {
        public long Position;
        public int Length;
        public byte[]? Pattern;
        public byte[]? Mask;
        public bool EndOfStream;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class WICBitmapPatternRaw
    {
        public long Position;
        public int Length;
        public IntPtr Pattern;
        public IntPtr Mask;
        public bool EndOfStream;
    }
}
