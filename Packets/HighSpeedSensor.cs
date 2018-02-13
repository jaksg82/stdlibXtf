using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define the high speed sensor data packet.
    /// </summary>
    public class HighSpeedSensor : IPacket
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

        private UInt32 _RelativeBathyPingNumber;
        private UInt32 _NumberSensorBytes;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Bathymetry ping number belonging to this sensor data.
        /// </summary>
        public UInt32 RelativeBathyPingNumber { get { return _RelativeBathyPingNumber; } }

        /// <summary>
        /// Number of bytes of sensor data following this structure.
        /// </summary>
        public UInt32 NumberSensorBytes { get { return _NumberSensorBytes; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the HighSpeedSensor class that has default zero values.
        /// </summary>
        public HighSpeedSensor()
        {
            _HeaderType = 15;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _NumberSensorBytes = 0;
            _RelativeBathyPingNumber = 0;
        }

        /// <summary>
        /// Initializes a new instance of the HighSpeedSensor class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public HighSpeedSensor(Byte[] byteArray)
        {
            _HeaderType = 15;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _NumberSensorBytes = 0;
            _RelativeBathyPingNumber = 0;

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
                if (byteArray.Length >= 31)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfDocument.MagicNumber)
                    {
                        _HeaderType = dp.ReadByte(); //HeaderType 2
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
                        TimeString = TimeString + "-" + MSeconds.ToString("00", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        _NumberSensorBytes = dp.ReadUInt32(); // 23-24-25-26
                        _RelativeBathyPingNumber = dp.ReadUInt32(); // 27-28-29-30
                    }
                }
            }
        }

        #endregion constructors
    }
}