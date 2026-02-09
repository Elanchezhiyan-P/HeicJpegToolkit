namespace HeicJpegToolkit.Helpers.Options
{
    /// <summary>
    /// Options to control conversion behavior for web/API, privacy, and quality scenarios.
    /// This type is kept simple and POCO-friendly so it can be bound from configuration.
    /// </summary>
    public sealed class ConversionOptions
    {
        /// <summary>
        /// Optional preset that configures sensible defaults for quality and resizing.
        /// </summary>
        public ConversionPreset Preset { get; set; } = ConversionPreset.Original;

        /// <summary>
        /// JPEG quality (1-100). If null, a sensible default is chosen based on <see cref="Preset"/>.
        /// </summary>
        public int? Quality { get; set; }

        /// <summary>
        /// Optional maximum width in pixels. Images larger than this will be resized while preserving aspect ratio.
        /// </summary>
        public int? MaxWidth { get; set; }

        /// <summary>
        /// Optional maximum height in pixels. Images larger than this will be resized while preserving aspect ratio.
        /// </summary>
        public int? MaxHeight { get; set; }

        /// <summary>
        /// When true, EXIF orientation is applied so the output is visually correct.
        /// </summary>
        public bool AutoRotate { get; set; } = true;

        /// <summary>
        /// When true, metadata such as EXIF, IPTC, and XMP will be preserved where possible.
        /// </summary>
        public bool PreserveMetadata { get; set; } = true;

        /// <summary>
        /// When true, GPS and other sensitive location metadata is removed from the output.
        /// </summary>
        public bool StripGpsMetadata { get; set; } = false;

        /// <summary>
        /// Optional maximum allowed input size in bytes. Larger inputs will fail fast.
        /// Useful in web/API scenarios to guard against very large uploads.
        /// </summary>
        public long? MaxInputBytes { get; set; }

        /// <summary>
        /// Optional maximum decoded pixel count (width * height). Inputs that exceed this will fail.
        /// This is another safety mechanism for web/API scenarios.
        /// </summary>
        public long? MaxPixelCount { get; set; }
    }
}

