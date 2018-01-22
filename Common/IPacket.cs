using System;

namespace stdlibXtf.Common
{
    public interface IPacket
    {
        #region Properties

        UInt16 MagicNumber { get; }
        Byte HeaderType { get; }
        Byte SubChannelNumber { get; }
        UInt16 NumberChannelsToFollow { get; }
        UInt32 NumberBytesThisRecord { get; }
        DateTime PacketTime { get; }
        //UInt32 TimeTag { get; }

        #endregion


    }
}
