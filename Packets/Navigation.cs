using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define a navigation data packet.
    /// </summary>
    public class Navigation : IPacket
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

        private UInt32 _TimeTag;
        private UInt32 _SourceEpoch;
        private Double _RawCoordinateX;
        private Double _RawCoordinateY;
        private Double _RawAltitude;
        private Double _TimestampValidity;

        #endregion private properties

        #region public properties

        /// <summary>
        /// System time reference in milliseconds.
        /// </summary>
        public UInt32 TimeTag { get { return _TimeTag; } }

        /// <summary>
        /// Source Epoch Seconds since 1/1/1970, will be followed attitude data even to 64 bytes.
        /// </summary>
        public UInt32 SourceEpoch { get { return _SourceEpoch; } }

        /// <summary>
        /// Raw position from POSMV or other time stamped navigation source.
        /// </summary>
        public Double RawCoordinateX { get { return _RawCoordinateX; } }

        /// <summary>
        /// Raw position from POSMV or other time stamped navigation source.
        /// </summary>
        public Double RawCoordinateY { get { return _RawCoordinateY; } }

        /// <summary>
        /// Altitude, can hold real-time kinematics altitude.
        /// </summary>
        public Double RawAltitude { get { return _RawAltitude; } }

        /// <summary>
        /// Time stamp validity:
        /// 0 = only receive time valid
        /// 1 = only source time valid
        /// 3 = both valid
        /// </summary>
        public Double TimestampValidity { get { return _TimestampValidity; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the Navigation class that has default zero values.
        /// </summary>
        public Navigation()
        {
            _HeaderType = 42;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _TimeTag = 0;
            _SourceEpoch = 0;
            _RawCoordinateX = 0;
            _RawCoordinateY = 0;
            _RawAltitude = 0;
            _TimestampValidity = 0;
        }

        /// <summary>
        /// Initializes a new instance of the Navigation class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public Navigation(Byte[] byteArray)
        {
            _HeaderType = 42;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _TimeTag = 0;
            _SourceEpoch = 0;
            _RawCoordinateX = 0;
            _RawCoordinateY = 0;
            _RawAltitude = 0;
            _TimestampValidity = 0;

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
                if (byteArray.Length >= 58)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfDocument.MagicNumber)
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

                        _SourceEpoch = dp.ReadUInt32(); // 25-26-27-28
                        _TimeTag = dp.ReadUInt32(); // 29-30-31-32
                        _RawCoordinateY = dp.ReadDouble(); // 33-34-35-36-37-38-39-40
                        _RawCoordinateX = dp.ReadDouble(); // 41-42-43-44-45-46-47-48
                        _RawAltitude = dp.ReadDouble(); // 49-50-51-52-53-54-55-56
                        _TimestampValidity = dp.ReadByte(); // 57
                    }
                }
            }
        }

        #endregion constructors
    }
}