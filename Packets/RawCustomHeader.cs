using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class RawCustomHeader : IPacket 
    {
        #region private properties

        private Byte _HeaderType = 199; // XTF_HEADER_CUSTOM
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;

        #endregion

        #region public properties

        // IPacket implementation
        public Byte HeaderType { get { return _HeaderType; } }
        public ushort MagicNumber { get { return _MagicNumber; } }
        public byte SubChannelNumber { get { return _SubChannelNumber; } }
        public ushort NumberChannelsToFollow { get { return _NumberChannelsToFollow; } }
        public uint NumberBytesThisRecord { get { return _NumberBytesThisRecord; } }
        public DateTime PacketTime { get { return _PacketTime; } }

        // Other properties
        public Byte ManufacturerId { get; set; }
        public UInt16 SonarId { get; set; }
        public UInt16 PacketId { get; set; }
        public UInt32 PingNumber { get; set; }
        public UInt32 TimeTag { get; set; }
        public UInt32 NumberCustomerBytes { get; set; }

        #endregion

        #region constructors

        public RawCustomHeader()
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            //_NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            ManufacturerId = 0;
            SonarId = 0;
            PacketId = 0;
            PingNumber = 0;
            TimeTag = 0;
            NumberCustomerBytes = 0;
            _NumberBytesThisRecord = NumberCustomerBytes + 64;

        }

        public RawCustomHeader(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            //_NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            ManufacturerId = 0;
            SonarId = 0;
            PacketId = 0;
            PingNumber = 0;
            TimeTag = 0;
            NumberCustomerBytes = 0;
            _NumberBytesThisRecord = NumberCustomerBytes + 64;

            UInt16 chkNumber;
            UInt16 Year;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            Byte MSeconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 64)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfMainHeader.MagicNumber)
                    {
                        dp.ReadByte(); //HeaderType 2
                        ManufacturerId = dp.ReadByte(); // 3
                        SonarId = dp.ReadUInt16(); // 4-5
                        PacketId = dp.ReadUInt16(); // 6-7
                        dp.ReadUInt16(); // 8-9 Unused
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13

                        //Read the ping time values
                        Year = dp.ReadUInt16(); // 14-15
                        Month = dp.ReadByte(); // 16
                        Day = dp.ReadByte(); // 17
                        Hour = dp.ReadByte(); // 18
                        Minutes = dp.ReadByte(); // 19
                        Seconds = dp.ReadByte(); // 20
                        MSeconds = dp.ReadByte(); // 21
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + MSeconds.ToString("00", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        dp.ReadUInt16(); // 22-23 Julian Day
                        dp.ReadUInt16(); // 24-25 Unused
                        dp.ReadUInt16(); // 26-27 Unused
                        PingNumber = dp.ReadUInt32(); // 28-29-30-31
                        TimeTag = dp.ReadUInt32(); // 32-33-34-35
                        NumberCustomerBytes = dp.ReadUInt32(); // 36-37-38-39

                    }
                }
            }
        }

        #endregion

    }
}
