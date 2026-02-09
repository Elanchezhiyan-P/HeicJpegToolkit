namespace HeicJpegToolkit.Structures
{
    public readonly struct Resolution
    {
        public double DpiX { get; }
        public double DpiY { get; }

        public Resolution(double dpiX, double dpiY)
        {
            DpiX = dpiX;
            DpiY = dpiY;
        }
    }
}
