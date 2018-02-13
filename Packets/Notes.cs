using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define an annotation data packet.
    /// </summary>
    public class Notes : IPacket
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

        private String _Text;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the annotation text.
        /// The maximum size is of 200 characters.
        /// </summary>
        public String Text
        {
            get { return _Text; }
            //set
            //{
            //    if (String.IsNullOrEmpty(value)) { value = " "; }
            //    if (value.Length > 200) { _Text = value.Substring(0, 200); }
            //}
        }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the Notes class that has default zero values.
        /// </summary>
        public Notes()
        {
            _HeaderType = 1;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _Text = " ";
        }

        /// <summary>
        /// Initializes a new instance of the Notes class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 256 bytes.</param>
        public Notes(Byte[] byteArray)
        {
            _HeaderType = 1;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _Text = " ";

            UInt16 chkNumber;
            UInt16 Year;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            //UInt32 MSeconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 256)
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
                        //MSeconds = dp.ReadUInt32();
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        //TimeString = TimeString + "-" + MSeconds.ToString("000000", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        dp.ReadBytes(35); // Unused 21 to 55
                        _Text = new String(dp.ReadChars(200)); // 56 to 255
                    }
                }
            }
        }

        #endregion constructors
    }
}