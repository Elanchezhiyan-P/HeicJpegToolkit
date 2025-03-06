using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeicJpegToolkit.Structures
{
    public struct Resolution
    {
        public Resolution(double dpiX, double dpiY)
        {
            DpiX = dpiX;
            DpiY = dpiY;
        }

        public double DpiX { get; }
        public double DpiY { get; }
    }
}
