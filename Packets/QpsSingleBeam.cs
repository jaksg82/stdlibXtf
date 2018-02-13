using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define a QPS single beam data packet.
    /// </summary>
    public class QpsSingleBeam : IPacket
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
        private Int32 _Id;
        private Single _SoundVelocity;
        private Single _Intensity;
        private Int32 _Quality;
        private Single _TwoWayTravelTime;

        #endregion private properties

        #region public properties

        /// <summary>
        /// System time reference in milliseconds.
        /// </summary>
        public UInt32 TimeTag { get { return _TimeTag; } }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public Int32 Id { get { return _Id; } }

        /// <summary>
        /// Gets the speed of sound in m/s.
        /// </summary>
        public Single SoundVelocity { get { return _SoundVelocity; } }

        /// <summary>
        /// Gets the signal strength.
        /// </summary>
        public Single Intensity { get { return _Intensity; } }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        public Int32 Quality { get { return _Quality; } }

        /// <summary>
        /// Gets the two way travel time in seconds.
        /// </summary>
        public Single TwoWayTravelTime { get { return _TwoWayTravelTime; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the QpsSingleBeam class that has default zero values.
        /// </summary>
        public QpsSingleBeam()
        {
            _HeaderType = 26;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _TimeTag = 0;
            _Id = 0;
            _SoundVelocity = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;
        }

        /// <summary>
        /// Initializes a new instance of the QpsSingleBeam class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public QpsSingleBeam(Byte[] byteArray)
        {
            _HeaderType = 26;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _TimeTag = 0;
            _Id = 0;
            _SoundVelocity = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;

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
                    if (chkNumber == XtfDocument.MagicNumber)
                    {
                        dp.ReadByte(); //HeaderType 2
                        _SubChannelNumber = dp.ReadByte(); // 3
                        _NumberChannelsToFollow = dp.ReadUInt16(); // 4-5
                        dp.ReadUInt16(); //Unused 6-7
                        dp.ReadUInt16(); //Unused 8-9
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13

                        _TimeTag = dp.ReadUInt32(); // 14-15-16-17
                        _Id = dp.ReadInt32(); // 18-19-20-21
                        _SoundVelocity = dp.ReadSingle(); // 22-23-24-25
                        _Intensity = dp.ReadSingle(); // 26-27-28-29
                        _Quality = dp.ReadInt32(); // 30-31-32-33
                        _TwoWayTravelTime = dp.ReadSingle(); // 34-35-36-37

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

        #endregion constructors
    }
}