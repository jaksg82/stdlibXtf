using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    class AttitudeData : IPacket 
    {
        #region private properties

        private Byte _HeaderType;
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

        public Single Pitch { get; set; }
        public Single Roll { get; set; }
        public Single Heave { get; set; }
        public Single Yaw { get; set; }
        public Single Heading { get; set; }
        public UInt32 TimeTag { get; set; }
        public UInt32 SourceEpoch { get; set; }
        public UInt32 SourceEpochMicroseconds { get; set; }

        #endregion

        #region constructors

        public AttitudeData()
        {
            _MagicNumber = 0;
            _HeaderType = 3; // XTF_HEADER_ATTITUDE
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;

            Pitch = 0;
            Roll = 0;
            Heave = 0;
            Yaw = 0;
            Heading = 0;
            TimeTag = 0;
            SourceEpoch = 0;
            SourceEpochMicroseconds = 0;
        }

        /// <summary>
        /// Create an Attitude data packet from the given bytes.
        /// </summary>
        /// <param name="byteArray">64 byte array that contain the informations.</param>
        public AttitudeData(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;

            Pitch = 0;
            Roll = 0;
            Heave = 0;
            Yaw = 0;
            Heading = 0;
            TimeTag = 0;
            SourceEpoch = 0;
            SourceEpochMicroseconds = 0;

            UInt16 Year;
            UInt16 MSecons;
            UInt16 chkNumber;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 63)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfMainHeader.MagicNumber)
                    {
                        dp.ReadByte(); // 2
                        _SubChannelNumber = dp.ReadByte(); // 3
                        _NumberChannelsToFollow = dp.ReadUInt16(); // 4-5
                        dp.ReadUInt16(); // 6-7
                        dp.ReadUInt16(); // 8-9
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13
                        dp.ReadUInt32(); // 14-15-16-17
                        dp.ReadUInt32(); // 18-19-20-21

                        SourceEpochMicroseconds = dp.ReadUInt32(); // 22-23-24-25
                        SourceEpoch = dp.ReadUInt32(); // 26-27-28-29
                        Pitch = dp.ReadSingle(); // 30-31-32-33
                        Roll = dp.ReadSingle(); // 34-35-36-37
                        Heave = dp.ReadSingle(); // 38-39-40-41
                        Yaw = dp.ReadSingle(); // 42-43-44-45
                        TimeTag = dp.ReadUInt32(); // 46-47-48-49
                        Heading = dp.ReadSingle(); // 50-51-52-53

                        //Read the packet time values
                        Year = dp.ReadUInt16(); // 54-55
                        Month = dp.ReadByte(); // 56
                        Day = dp.ReadByte(); // 57
                        Hour = dp.ReadByte(); // 58
                        Minutes = dp.ReadByte(); // 59
                        Seconds = dp.ReadByte(); // 60
                        MSecons = dp.ReadUInt16(); // 61-62
                        //Compose the packet time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + MSecons.ToString("000", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                    }

                }
            }

        }
        
        #endregion
        
    }
}
