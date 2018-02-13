using System;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Interface that provide the base properties of all the packages.
    /// </summary>
    public interface IPacket
    {
        #region Properties

        /// <summary>
        /// Gets the number that identify the correct start of the packet.
        /// </summary>
        UInt16 MagicNumber { get; }

        /// <summary>
        /// Gets the type of the packet header.
        /// </summary>
        Byte HeaderType { get; }

        /// <summary>
        /// Gets the index number of which channels this packet are referred.
        /// </summary>
        Byte SubChannelNumber { get; }

        /// <summary>
        /// Gets the number of channels that follow this packet.
        /// </summary>
        UInt16 NumberChannelsToFollow { get; }

        /// <summary>
        /// Total byte count for this packet, including the header and the data if available.
        /// </summary>
        UInt32 NumberBytesThisRecord { get; }

        /// <summary>
        /// Gets the packet recording time.
        /// </summary>
        DateTime PacketTime { get; }

        #endregion Properties
    }
}