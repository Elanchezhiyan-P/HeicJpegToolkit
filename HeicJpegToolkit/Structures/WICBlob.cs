using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Structures
{
    public class WICBlob
    {
        public byte[] Bytes { get; }

        public WICBlob(byte[] bytes)
        {
            Bytes = bytes;
        }
    }
}
