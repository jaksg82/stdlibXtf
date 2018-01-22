using System;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class XtfBasePacket : IPacket
    {
        #region private properties

        private Byte _HeaderType; // XTF_HEADER_SONAR
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;
        private ushort _Reserved1;
        private ushort _Reserved2;

        #endregion

        #region public properties

        // IPacket implementation
        public Byte HeaderType { get { return _HeaderType; } }
        public ushort MagicNumber { get { return _MagicNumber; } }
        public byte SubChannelNumber { get { return _SubChannelNumber; } }
        public ushort NumberChannelsToFollow { get { return _NumberChannelsToFollow; } }
        public uint NumberBytesThisRecord { get { return _NumberBytesThisRecord; } }
        public DateTime PacketTime { get { return _PacketTime; } }

        #endregion

        #region constructors

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


        #endregion

    }
}
