namespace HeicJpegToolkit.Structures
{
    public readonly struct Resolution(double dpiX, double dpiY)
    {
        public double DpiX { get; } = dpiX;
        public double DpiY { get; } = dpiY;
    }
}
