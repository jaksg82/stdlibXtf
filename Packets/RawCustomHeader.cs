using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define a custom raw data packet.
    /// </summary>
    public class RawCustomHeader : IPacket
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

        private Byte _ManufacturerId;
        private UInt16 _SonarId;
        private UInt16 _PacketId;
        private UInt32 _PingNumber;
        private UInt32 _TimeTag;
        private UInt32 _NumberCustomerBytes;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the manufacturer ID number.
        /// </summary>
        public Byte ManufacturerId { get { return _ManufacturerId; } }

        /// <summary>
        /// Gets the sonar ID number.
        /// </summary>
        public UInt16 SonarId { get { return _SonarId; } }

        /// <summary>
        /// Gets the packet ID number.
        /// </summary>
        public UInt16 PacketId { get { return _PacketId; } }

        /// <summary>
        /// Counts consecutively (usually from 0) and increments for each update.
        /// </summary>
        public UInt32 PingNumber { get { return _PingNumber; } }

        /// <summary>
        /// Get the system time reference in milliseconds.
        /// </summary>
        public UInt32 TimeTag { get { return _TimeTag; } }

        /// <summary>
        /// Gets the number of bytes that follow this header.
        /// </summary>
        public UInt32 NumberCustomerBytes { get { return _NumberCustomerBytes; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the RawCustomHeader class that has default zero values.
        /// </summary>
        public RawCustomHeader()
        {
            _HeaderType = 199;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            //_NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _ManufacturerId = 0;
            _SonarId = 0;
            _PacketId = 0;
            _PingNumber = 0;
            _TimeTag = 0;
            _NumberCustomerBytes = 0;
            _NumberBytesThisRecord = NumberCustomerBytes + 64;
        }

        /// <summary>
        /// Initializes a new instance of the RawCustomHeader class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public RawCustomHeader(Byte[] byteArray)
        {
            _HeaderType = 199;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            //_NumberBytesThisRecord = 64;
            _PacketTime = DateTime.MinValue;

            _ManufacturerId = 0;
            _SonarId = 0;
            _PacketId = 0;
            _PingNumber = 0;
            _TimeTag = 0;
            _NumberCustomerBytes = 0;
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
                    if (chkNumber == XtfDocument.MagicNumber)
                    {
                        dp.ReadByte(); //HeaderType 2
                        _ManufacturerId = dp.ReadByte(); // 3
                        _SonarId = dp.ReadUInt16(); // 4-5
                        _PacketId = dp.ReadUInt16(); // 6-7
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
                        _PingNumber = dp.ReadUInt32(); // 28-29-30-31
                        _TimeTag = dp.ReadUInt32(); // 32-33-34-35
                        _NumberCustomerBytes = dp.ReadUInt32(); // 36-37-38-39
                    }
                }
            }
        }

        #endregion constructors
    }
}