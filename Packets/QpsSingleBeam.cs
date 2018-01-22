using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class QpsSingleBeam : IPacket 
    {
        #region private properties

        private Byte _HeaderType = 26; // XTF_HEADER_Q_SINGLEBEAM
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
        public Int32 Id { get; set; }
        public Single SoundVelocity { get; set; }
        public Single Intensity { get; set; }
        public Int32 Quality { get; set; }
        public Single TwoWayTravelTime { get; set; }

        #endregion

        #region constructors

        public QpsSingleBeam()
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            TimeTag = 0;
            Id = 0;
            SoundVelocity = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;

        }

        public QpsSingleBeam(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            TimeTag = 0;
            Id = 0;
            SoundVelocity = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;

            UInt16 chkNumber;
            UInt16 Year;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            UInt16 MSeconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 53)
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

                        TimeTag = dp.ReadUInt32(); // 14-15-16-17
                        Id = dp.ReadInt32(); // 18-19-20-21
                        SoundVelocity = dp.ReadSingle(); // 22-23-24-25
                        Intensity = dp.ReadSingle(); // 26-27-28-29
                        Quality = dp.ReadInt32(); // 30-31-32-33
                        TwoWayTravelTime = dp.ReadSingle(); // 34-35-36-37

                        //Read the ping time values
                        Year = dp.ReadUInt16(); // 38-39
                        Month = dp.ReadByte(); // 40
                        Day = dp.ReadByte(); // 41
                        Hour = dp.ReadByte(); // 42
                        Minutes = dp.ReadByte(); // 43
                        Seconds = dp.ReadByte(); // 44
                        MSeconds = dp.ReadUInt16(); // 45-46
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + MSeconds.ToString("000", CultureInfo.InvariantCulture);
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
