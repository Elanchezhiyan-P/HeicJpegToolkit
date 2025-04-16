namespace HeicJpegToolkit.Structures
{
    public readonly struct WICSize(int width, int height) : IEquatable<WICSize>
    {
        public int Width { get; } = width;
        public int Height { get; } = height;

        public override bool Equals(object? obj)
        {
            return obj is WICSize size && Equals(size);
        }

        public bool Equals(WICSize other)
        {
            return Width == other.Width
                && Height == other.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Width, Height);
        }

        public static bool operator ==(WICSize obj1, WICSize obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(WICSize obj1, WICSize obj2)
        {
            return !obj1.Equals(obj2);
        }
    }
}
