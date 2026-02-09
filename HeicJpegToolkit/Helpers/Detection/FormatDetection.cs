using System;
using System.IO;

namespace HeicJpegToolkit.Helpers.Detection
{
    /// <summary>
    /// Simple, fast format detection helpers suitable for guarding web/API endpoints.
    /// These rely on header signatures and are not a full parser.
    /// </summary>
    public static class FormatDetection
    {
        /// <summary>
        /// Checks whether the buffer looks like HEIC/HEIF based on the ISO BMFF "ftyp" box and known brands.
        /// </summary>
        public static bool IsHeic(ReadOnlySpan<byte> buffer)
        {
            // Need at least enough bytes for "ftyp" + brand.
            if (buffer.Length < 16)
            {
                return false;
            }

            // HEIC/HEIF is based on ISO Base Media File Format with a "ftyp" box
            // and brands like "heic", "heix", "hevc", "hevx", "mif1", "msf1".
            // Offset 4-7: "ftyp"
            if (buffer[4] != (byte)'f' ||
                buffer[5] != (byte)'t' ||
                buffer[6] != (byte)'y' ||
                buffer[7] != (byte)'p')
            {
                return false;
            }

            // Major brand at offset 8-11.
            var brand = buffer.Slice(8, 4);
            return brand.SequenceEqual("heic"u8) ||
                   brand.SequenceEqual("heix"u8) ||
                   brand.SequenceEqual("hevc"u8) ||
                   brand.SequenceEqual("hevx"u8) ||
                   brand.SequenceEqual("mif1"u8) ||
                   brand.SequenceEqual("msf1"u8);
        }

        /// <summary>
        /// Reads up to <paramref name="maxBytesToRead"/> from the current position and restores it,
        /// then runs <see cref="IsHeic(ReadOnlySpan{byte})"/>.
        /// </summary>
        public static bool IsHeic(Stream stream, int maxBytesToRead = 32)
        {
            if (stream is null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new ArgumentException("Stream must be readable.", nameof(stream));

            Span<byte> buffer = stackalloc byte[Math.Min(maxBytesToRead, 32)];
            int read;

            if (stream.CanSeek)
            {
                long originalPosition = stream.Position;
                read = stream.Read(buffer);
                stream.Position = originalPosition;
            }
            else
            {
                // For non-seekable streams we copy to a temporary MemoryStream
                using var temp = new MemoryStream();
                stream.CopyTo(temp);
                var data = temp.ToArray().AsSpan();
                return IsHeic(data);
            }

            return IsHeic(buffer[..read]);
        }
    }
}

