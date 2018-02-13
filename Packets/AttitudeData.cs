using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define the attitude data packet.
    /// </summary>
    public class AttitudeData : IPacket
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

        private Single _Pitch;
        private Single _Roll;
        private Single _Heave;
        private Single _Yaw;
        private Single _Heading;
        private UInt32 _TimeTag;
        private UInt32 _SourceEpoch;
        private UInt32 _SourceEpochMicroseconds;

        #endregion private properties

        #region public properties

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
        /// Positive value is turn right.
        /// </summary>
        public Single Yaw { get { return _Yaw; } }

        /// <summary>
        /// In degrees, as reported by MRU. TSS doesn't report heading, so when using a TSS this value will be the most recent ship gyro value
        /// as received from GPS or from any serial port using 'G' in the template.
        /// </summary>
        public Single Heading { get { return _Heading; } }

        /// <summary>
        /// System time reference in milliseconds.
        /// </summary>
        public UInt32 TimeTag { get { return _TimeTag; } }

        /// <summary>
        /// Source Epoch Seconds since 1/1/1970, will be followed attitude data even to 64 bytes.
        /// </summary>
        public UInt32 SourceEpoch { get { return _SourceEpoch; } }

        /// <summary>
        /// The Microsecond part of SourceEpoch. Range: 0 to 999999
        /// </summary>
        public UInt32 SourceEpochMicroseconds { get { return _SourceEpochMicroseconds; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the AttitudeData class that has default zero values.
        /// </summary>
        public AttitudeData()
        {
            _MagicNumber = 0;
            _HeaderType = 3; // XTF_HEADER_ATTITUDE
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;

            _Pitch = 0;
            _Roll = 0;
            _Heave = 0;
            _Yaw = 0;
            _Heading = 0;
            _TimeTag = 0;
            _SourceEpoch = 0;
            _SourceEpochMicroseconds = 0;
        }

        /// <summary>
        /// Initializes a new instance of the AttitudeData class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public AttitudeData(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;

            _Pitch = 0;
            _Roll = 0;
            _Heave = 0;
            _Yaw = 0;
            _Heading = 0;
            _TimeTag = 0;
            _SourceEpoch = 0;
            _SourceEpochMicroseconds = 0;

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
                    if (chkNumber == XtfDocument.MagicNumber)
                    {
                        _HeaderType = dp.ReadByte(); // 2
                        _SubChannelNumber = dp.ReadByte(); // 3
                        _NumberChannelsToFollow = dp.ReadUInt16(); // 4-5
                        dp.ReadUInt16(); // 6-7
                        dp.ReadUInt16(); // 8-9
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13
                        dp.ReadUInt32(); // 14-15-16-17
                        dp.ReadUInt32(); // 18-19-20-21

                        _SourceEpochMicroseconds = dp.ReadUInt32(); // 22-23-24-25
                        _SourceEpoch = dp.ReadUInt32(); // 26-27-28-29
                        _Pitch = dp.ReadSingle(); // 30-31-32-33
                        _Roll = dp.ReadSingle(); // 34-35-36-37
                        _Heave = dp.ReadSingle(); // 38-39-40-41
                        _Yaw = dp.ReadSingle(); // 42-43-44-45
                        _TimeTag = dp.ReadUInt32(); // 46-47-48-49
                        _Heading = dp.ReadSingle(); // 50-51-52-53

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

        #endregion constructors
    }
}