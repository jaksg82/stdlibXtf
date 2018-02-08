namespace stdlibXtf.Enums
{
    public static class EnumExtensions
    {

        #region "Enums stored as byte"

        public static byte ToByte(this ChannelSampleFormats value)
        {
            return (byte)value;
        }

        public static byte ToByte(this ChannelTypes value)
        {
            return (byte)value;
        }

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

        #endregion

        #region "Enums stored as 16 bit unsigned integers"

        public static ushort ToUInt16(this CoordinateUnits value)
        {
            return (ushort)value;
        }

        public static ushort ToUInt16(this CorrectionFlags value)
        {
            return (ushort)value;
        }

        #endregion

    }

}