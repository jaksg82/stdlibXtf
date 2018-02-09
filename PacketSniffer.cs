using System;
using System.IO;
using stdlibXtf.Enums;
using stdlibXtf.Packets;
using stdlibXtf.Common;

namespace stdlibXtf
{
    /// <summary>
    /// Extract the packet basic information from the given array of bytes.
    /// </summary>
    public class PacketSniffer : IPacket
    {
        #region private properties

        private Byte _HeaderType;
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;
        private UInt32 _TimeTag;

        #endregion private properties

        #region public properties

        // IPacket implementation
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

        // Other Properties
        /// <summary>
        /// Gets the system time reference in milliseconds. Available only in specific packet types.
        /// </summary>
        public UInt32 TimeTag { get { return _TimeTag; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the PacketSniffer class that has default zero values.
        /// </summary>
        public PacketSniffer()
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;
            _TimeTag = 0;
        }

        /// <summary>
        /// Initializes a new instance of the PacketSniffer class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">14 bytes array for basic information OR 256 byte for packet time.</param>
        public PacketSniffer(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _HeaderType = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 0;
            _PacketTime = DateTime.MinValue;
            _TimeTag = 0;

            using (BinaryReader pd = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 14)
                {
                    // Read the basic information of the packet
                    XtfBasePacket basePkt = new XtfBasePacket(byteArray);

                    _MagicNumber = basePkt.MagicNumber;
                    _HeaderType = basePkt.HeaderType;
                    _SubChannelNumber = basePkt.SubChannelNumber;
                    _NumberChannelsToFollow = basePkt.NumberChannelsToFollow;
                    _NumberBytesThisRecord = basePkt.NumberBytesThisRecord;

                    if (byteArray.Length >= 256)
                    {
                        // Extract packet time from each packet type known
                        switch (HeaderType)
                        {
                            // Ping header packet types
                            case 0:
                            case 2:
                            case 4:
                            case 5:
                            case 8:
                            case 9:
                            case 10:
                            case 14:
                            case 16:
                            case 17:
                            case 18:
                            case 19:
                            case 22:
                            case 25:
                            case 27:
                            case 28:
                            case 60:
                            case 61:
                            case 62:
                            case 65:
                            case 66:
                            case 68:
                            case 69:
                            case 73:
                                PingHeader tmpHdr0 = new PingHeader(byteArray);
                                _PacketTime = tmpHdr0.PacketTime;
                                break;

                            // Notes - text annotation packet types
                            case 1:
                                Notes tmpHdr1 = new Notes(byteArray);
                                _PacketTime = tmpHdr1.PacketTime;
                                break;

                            // Attitude Data packet types
                            case 3:
                            case 103:
                                AttitudeData tmpHdr2 = new AttitudeData(byteArray);
                                _PacketTime = tmpHdr2.PacketTime;
                                _TimeTag = tmpHdr2.TimeTag;
                                break;

                            // Raw Serial packet types
                            case 6:
                                RawSerialHeader tmpHdr3 = new RawSerialHeader(byteArray);
                                _PacketTime = tmpHdr3.PacketTime;
                                break;

                            // High speed sensor packet types
                            case 11:
                            case 15:
                                HighSpeedSensor tmpHdr4 = new HighSpeedSensor(byteArray);
                                _PacketTime = tmpHdr4.PacketTime;
                                break;

                            // Gyro packet types
                            case 23:
                            case 84:
                                Gyro tmpHdr5 = new Gyro(byteArray);
                                _PacketTime = tmpHdr5.PacketTime;
                                _TimeTag = tmpHdr5.TimeTag;
                                break;

                            // QPS Singlebeam packet types
                            case 26:
                                QpsSingleBeam tmpHdr6 = new QpsSingleBeam(byteArray);
                                _PacketTime = tmpHdr6.PacketTime;
                                break;

                            // Navigation packet types
                            case 42:
                            case 100:
                                Navigation tmpHdr7 = new Navigation(byteArray);
                                _PacketTime = tmpHdr7.PacketTime;
                                _TimeTag = tmpHdr7.TimeTag;
                                break;

                            // Pos Raw Navigation packet types
                            case 107:
                                PosRawNavigation tmpHdr8 = new PosRawNavigation(byteArray);
                                _PacketTime = tmpHdr8.PacketTime;
                                break;

                            // Navigation packet types
                            case 199:
                                RawCustomHeader tmpHdr9 = new RawCustomHeader(byteArray);
                                _PacketTime = tmpHdr9.PacketTime;
                                _TimeTag = tmpHdr9.TimeTag;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        #endregion constructors
    }
}