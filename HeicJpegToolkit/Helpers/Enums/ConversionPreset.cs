namespace HeicJpegToolkit.Helpers.Enums
{
    /// <summary>
    /// High-level presets that control reasonable defaults for quality and sizing.
    /// </summary>
    public enum ConversionPreset
    {
        /// <summary>
        /// Keep original dimensions, high quality JPEG.
        /// </summary>
        Original = 0,

        /// <summary>
        /// Good balance between quality and size, suitable for web use.
        /// </summary>
        WebOptimized = 1,

        /// <summary>
        /// Smaller thumbnails, typically used for previews.
        /// </summary>
        Thumbnail = 2,

        /// <summary>
        /// Higher quality settings, intended for archival or high-detail scenarios.
        /// </summary>
        Archive = 3
    }
}

