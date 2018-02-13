using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define a raw position navigation data packet.
    /// </summary>
    public class PosRawNavigation : IPacket
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

        private Double _RawCoordinateX;
        private Double _RawCoordinateY;
        private Double _RawAltitude;
        private Single _Pitch;
        private Single _Roll;
        private Single _Heave;
        private Single _Heading;

        #endregion private properties

        #region public properties

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
        /// Positive value is nose up.
        /// </summary>
        public Single Pitch { get { return _Pitch; } }

        /// <summary>
        /// Positive value is roll to starboard.
        /// </summary>
        public Single Roll { get { return _Roll; } }

        /// <summary>
        /// Positive value is sensor up.
        /// Isis Note: The TSS sends heave positive up. The MRU sends heave positive down.
        /// In order to make the data logging consistent, the sign of the MRU's heave is reversed before being stored in this field.
        /// </summary>
        public Single Heave { get { return _Heave; } }

        /// <summary>
        /// In degrees, as reported by MRU. TSS doesn't report heading, so when using a TSS this value will be the most recent ship gyro
        /// value as received from GPS or from any serial port using 'G' in the template.
        /// </summary>
        public Single Heading { get { return _Heading; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the PosRawNavigation class that has default zero values.
        /// </summary>
        public PosRawNavigation()
        {
            _HeaderType = 107;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _RawCoordinateY = 0;
            _RawCoordinateX = 0;
            _RawAltitude = 0;
            _Pitch = 0;
            _Roll = 0;
            _Heave = 0;
            _Heading = 0;
        }

        /// <summary>
        /// Initializes a new instance of the PosRawNavigation class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public PosRawNavigation(Byte[] byteArray)
        {
            _HeaderType = 107;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _RawCoordinateY = 0;
            _RawCoordinateX = 0;
            _RawAltitude = 0;
            _Pitch = 0;
            _Roll = 0;
            _Heave = 0;
            _Heading = 0;

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

                        _RawCoordinateY = dp.ReadDouble(); // 23-24-25-26-27-28-29-30
                        _RawCoordinateX = dp.ReadDouble(); // 31-32-33-34-35-36-37-38
                        _RawAltitude = dp.ReadDouble(); // 39-40-41-42-43-44-45-46
                        _Pitch = dp.ReadSingle(); // 47-48-49-50
                        _Roll = dp.ReadSingle(); // 51-52-53-54
                        _Heave = dp.ReadSingle(); // 55-56-57-58
                        _Heading = dp.ReadSingle(); // 59-60-61-62
                    }
                }
            }
        }

        #endregion constructors
    }
}