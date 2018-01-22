using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class PosRawNavigation : IPacket 
    {
        #region private properties

        private Byte _HeaderType = 107; // XTF_HEADER_POS_RAW_NAVIGATION
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
        public Double RawCoordinateX { get; set; }
        public Double RawCoordinateY { get; set; }
        public Double RawAltitude { get; set; }
        public Single Pitch { get; set; }
        public Single Roll { get; set; }
        public Single Heave { get; set; }
        public Single Heading { get; set; }

        #endregion

        #region constructors

        public PosRawNavigation()
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            RawCoordinateY = 0;
            RawCoordinateX = 0;
            RawAltitude = 0;
            Pitch = 0;
            Roll = 0;
            Heave = 0;
            Heading = 0;

        }

        public PosRawNavigation(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            RawCoordinateY = 0;
            RawCoordinateX = 0;
            RawAltitude = 0;
            Pitch = 0;
            Roll = 0;
            Heave = 0;
            Heading = 0;

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
                if (byteArray.Length >= 64)
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
                        MSeconds = dp.ReadUInt16(); // 21-22
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + MSeconds.ToString("0000", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ffff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        RawCoordinateY = dp.ReadDouble(); // 23-24-25-26-27-28-29-30
                        RawCoordinateX = dp.ReadDouble(); // 31-32-33-34-35-36-37-38
                        RawAltitude = dp.ReadDouble(); // 39-40-41-42-43-44-45-46
                        Pitch = dp.ReadSingle(); // 47-48-49-50
                        Roll = dp.ReadSingle(); // 51-52-53-54
                        Heave = dp.ReadSingle(); // 55-56-57-58
                        Heading = dp.ReadSingle(); // 59-60-61-62

                    }
                }
            }
        }

        #endregion

    }
}
