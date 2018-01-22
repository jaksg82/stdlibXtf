namespace stdlibXtf.Enums
{
    /// <summary>
    /// Stored as Byte in the xtf file - Not used after X26 version
    /// </summary>
    public enum ChannelSampleFormats
    {
        Legacy = 0,
        IBMFloat = 1,
        Integer4Byte = 2,
        Integer2Byte = 3,
        Unused4 = 4,
        IEEEFloat = 5,
        Unused6 = 6,
        Unused7 = 7,
        Integer1Byte = 8
    }
}