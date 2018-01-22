namespace stdlibXtf.Enums
{
    /// <summary>
    /// Sonar imagery stored as:
    /// Range = 1 = Slant-range (Raw)
    /// Depth = 2 = Ground-range (Corrected)
    /// Stored as 16 bit Unsigend Integer in the xtf file
    /// </summary>
    public enum CorrectionFlags
    {
        None = 0,
        Range = 1,
        Depth = 2
    }
}