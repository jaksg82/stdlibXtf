using System;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define the base data packet.
    /// </summary>
    public class XtfBasePacket : IPacket
    {
        #region IPacket implementation

        private Byte _HeaderType;
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;

        /// <summary>
        /// Gets the type of the packet header.
        /// </summary>
        public Byte HeaderType { get { return _HeaderType; } }

        /// <summary>
        /// Gets the number that identify the correct start of the packet.
        /// </summary>
        public ushort MagicNumber { get { return _MagicNumber; } }

        /// <summary>
        /// Gets the index number of which channels this packet are referred.
        /// </summary>
        public byte SubChannelNumber { get { return _SubChannelNumber; } }

        /// <summary>
        /// Gets the number of channels that follow this packet.
        /// </summary>
        public ushort NumberChannelsToFollow { get { return _NumberChannelsToFollow; } }

        /// <summary>
        /// Total byte count for this packet, including the header and the data if available.
        /// </summary>
        public uint NumberBytesThisRecord { get { return _NumberBytesThisRecord; } }

        /// <summary>
        /// Gets the packet recording time.
        /// </summary>
        public DateTime PacketTime { get { return _PacketTime; } }

        #endregion IPacket implementation

        #region private properties

        private ushort _Reserved1;
        private ushort _Reserved2;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the first unused value.
        /// </summary>
        public ushort Reserved1 { get { return _Reserved1; } }

        /// <summary>
        /// Gets the second unused value.
        /// </summary>
        public ushort Reserved2 { get { return _Reserved2; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the XtfBasePacket class that has default zero values.
        /// </summary>
        public XtfBasePacket()
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _Reserved1 = 0;
            _Reserved2 = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;
        }

        /// <summary>
        /// Initializes a new instance of the XtfBasePacket class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 14 bytes.</param>
        public XtfBasePacket(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _Reserved1 = 0;
            _Reserved2 = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;

            using (BinaryReader pd = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 14)
                {
                    // Read the basic information of the packet
                    _MagicNumber = pd.ReadUInt16(); // 0-1
                    _HeaderType = pd.ReadByte(); // 2
                    _SubChannelNumber = pd.ReadByte(); // 3
                    _NumberChannelsToFollow = pd.ReadUInt16(); // 4-5
                    _Reserved1 = pd.ReadUInt16(); // 6-7
                    _Reserved2 = pd.ReadUInt16(); // 8-9
                    _NumberBytesThisRecord = pd.ReadUInt32(); // 10-11-12-13
                }
            }
        }

        #endregion constructors
    }
}