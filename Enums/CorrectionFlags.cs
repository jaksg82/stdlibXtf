namespace stdlibXtf.Enums
{
    /// <summary>
    /// Specify if the data is stored raw or corrected.
    /// Range = 1 = Slant-range (Raw)
    /// Depth = 2 = Ground-range (Corrected)
    /// Stored as 16 bit Unsigend Integer in the xtf file
    /// </summary>
    public enum CorrectionFlags
    {
        /// <summary>
        /// Not specific way of storage. This is a default value that can be considered equivalent to Range.
        /// </summary>
        None = 0,

        /// <summary>
        /// Sonar imagery stored as raw, range or slant-range.
        /// </summary>
        Range = 1,

        /// <summary>
        /// Sonar imagery stored as corrected, depth or ground-range.
        /// </summary>
        Depth = 2
    }
}