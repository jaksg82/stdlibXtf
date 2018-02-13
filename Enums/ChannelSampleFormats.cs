namespace stdlibXtf.Enums
{
    /// <summary>
    /// Specify the recording format of the sensor data in a channel.
    /// Stored as Byte in the xtf file - Not used after X26 version
    /// </summary>
    public enum ChannelSampleFormats
    {
        /// <summary>
        /// Data stored with legacy recording format.
        /// </summary>
        Legacy = 0,

        /// <summary>
        /// Data stored as 4 bytes IBM float format.
        /// </summary>
        IBMFloat = 1,

        /// <summary>
        /// Data stored as 4 bytes integer format.
        /// </summary>
        Integer4Byte = 2,

        /// <summary>
        /// Data stored as 2 bytes integer format.
        /// </summary>
        Integer2Byte = 3,

        ///// <summary>
        ///// Unused data format.
        ///// </summary>
        //Unused4 = 4,

        /// <summary>
        /// Data stored as 4 bytes IEEE float format.
        /// </summary>
        IEEEFloat = 5,

        ///// <summary>
        ///// Unused data format.
        ///// </summary>
        //Unused6 = 6,

        ///// <summary>
        ///// Unused data format.
        ///// </summary>
        //Unused7 = 7,

        /// <summary>
        /// Data stored as 1 byte integer format.
        /// </summary>
        Integer1Byte = 8
    }
}