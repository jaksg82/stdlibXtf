namespace stdlibXtf.Enums
{
    /// <summary>
    /// This class contain some methods that extend the enums.
    /// </summary>
    public static class EnumExtensions
    {
        #region "Enums stored as byte"

        /// <summary>
        /// Return a byte that represent the value of the enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(this ChannelSampleFormats value)
        {
            return (byte)value;
        }

        /// <summary>
        /// Return a byte that represent the value of the enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(this ChannelTypes value)
        {
            return (byte)value;
        }

        /// <summary>
        /// Return a byte that represent the value of the enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte ToByte(this NoteSubChannels value)
        {
            return (byte)value;
        }

        //public static System.Enum FromByte(this byte value)
        //{
        //    // Add check for the type of enum to parse
        //    if (!System.Enum.IsDefined(typeof(ChannelSampleFormats), value))
        //        return ChannelSampleFormats.Legacy;
        //    return (ChannelSampleFormats)value;
        //}

        #endregion "Enums stored as byte"

        #region "Enums stored as 16 bit unsigned integers"

        /// <summary>
        /// Return an unsigned integer of 16 bit that represent the value of the enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this CoordinateUnits value)
        {
            return (ushort)value;
        }

        /// <summary>
        /// Return an unsigned integer of 16 bit that represent the value of the enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ushort ToUInt16(this CorrectionFlags value)
        {
            return (ushort)value;
        }

        #endregion "Enums stored as 16 bit unsigned integers"
    }
}