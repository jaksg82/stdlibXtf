using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class Gyro : IPacket 
    {
        #region private properties

        private Byte _HeaderType = 84; // XTF_HEADER_SOURCETIME_GYRO
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
        public UInt32 TimeTag { get; set; }
        public UInt32 SourceEpoch { get; set; }
        public Single GyroValue { get; set; }
        public Byte TimestampValidity { get; set; }

        #endregion

        #region constructors

        public Gyro()
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            TimeTag = 0;
            SourceEpoch = 0;
            GyroValue = 0;
            TimestampValidity = 0;

        }

        public Gyro(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            TimeTag = 0;
            SourceEpoch = 0;
            GyroValue = 0;
            TimestampValidity = 0;

            UInt16 chkNumber;
            UInt16 Year;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            UInt32 MSeconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 38)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfMainHeader.MagicNumber)
                    {
                        dp.ReadByte(); //HeaderType 2
                        _SubChannelNumber = dp.ReadByte(); // 3
                        _NumberChannelsToFollow = dp.ReadUInt16(); // 4-5
                        dp.ReadUInt16(); //Unused 6-7
                        dp.ReadUInt16(); //Unused 8-9
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13
                        //Read the ping time values
                        Year = dp.ReadUInt16(); // 14-15
                        Month = dp.ReadByte(); // 16
                        Day = dp.ReadByte(); // 17
                        Hour = dp.ReadByte(); // 18
                        Minutes = dp.ReadByte(); // 19
                        Seconds = dp.ReadByte(); // 20
                        MSeconds = dp.ReadUInt32(); // 21-22-23-24
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + MSeconds.ToString("000000", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ffffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        SourceEpoch = dp.ReadUInt32(); // 25-26-27-28
                        TimeTag = dp.ReadUInt32(); // 29-30-31-32
                        GyroValue = dp.ReadSingle(); // 33-34-35-36
                        TimestampValidity = dp.ReadByte(); // 37

                    }
                }
            }
        }

        #endregion

    }
}
