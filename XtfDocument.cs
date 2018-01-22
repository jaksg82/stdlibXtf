using stdlibXtf.SubPackets;
using stdlibXtf.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stdlibXtf
{
    public class XtfDocument
    {
        #region private properties

        private XtfMainHeader _MainHeader;
        private List<ChannelInfo> _Channels;
        private List<IndexEntry> _PacketIndexes;

        #endregion private properties

        #region public properties

        /// <summary>
        /// This is the number that identify the start of each packet header.
        /// </summary>
        static public UInt16 MagicNumber { get { return 64206; } }

        /// <summary>
        /// Represent the information contained in the file header.
        /// </summary>
        public XtfMainHeader MainHeader { get { return _MainHeader; } }

        /// <summary>
        /// Informations, that don't change through the file, relative to each channel recorded.
        /// All sidescan channels will always precede the bathymetry channels.
        /// </summary>
        public List<ChannelInfo> Channels { get { return _Channels; } }

        /// <summary>
        /// All the packets recorded in this file.
        /// </summary>
        public List<IndexEntry> Packets { get { return _PacketIndexes; } }  // TODO: Create an entry in order to store the byte position of each packet.

        /// <summary>
        /// A collection with some statistics for each type of packets in this file.
        /// </summary>
        public StatCollection Statistics { get; set; }

        #endregion public properties

        #region constructor

        public XtfDocument()
        {
            _MainHeader = new XtfMainHeader();
            _Channels = new List<ChannelInfo> { };
            _PacketIndexes = new List<IndexEntry> { };
            Statistics = new StatCollection();
        }


        public XtfDocument(Byte[] byteArray)
        {
            _MainHeader = new XtfMainHeader();
            _Channels = new List<ChannelInfo> { };
            _PacketIndexes = new List<IndexEntry> { };
            Statistics = new StatCollection();

            if (byteArray.Length >= 1024)
            {
                using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
                {
                    // Make sure to start from the first byte
                    dp.BaseStream.Seek(0, SeekOrigin.Begin);

                    // Read the header of the file.
                    _MainHeader = new XtfMainHeader(dp.ReadBytes(256));

                    // Read the channel data attacched to the header of the file.
                    _Channels.Clear();
                    int chnum = _MainHeader.NumberOfSonarChannels + _MainHeader.NumberOfBathymetryChannels - 1;
                    if (chnum >= 0)
                    {
                        if (chnum <= 6)
                        {
                            for (int i = 0; i <= chnum; i++)
                            {
                                _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                            }
                        }
                        else
                        {
                            if (byteArray.Length >= 2048)
                            {
                                for (int i = 0; i <= chnum; i++)
                                {
                                    _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                                }
                            }
                            else
                            {
                                for (int i = 0; i <= 6; i++)
                                {
                                    _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                                }
                            }
                        }

                    }

                    UInt32 actualByte;
                    UInt32 totalBytes = (UInt32)byteArray.Length;

                    // Compute the dimension of the header.
                    if (_Channels.Count <= 6) { actualByte = 1024; }
                    else
                    {
                        if (_Channels.Count <= 14) { actualByte = 2048; }
                        else { actualByte = 3072; } // Up to 22 channels seem to be more than enough.
                    }
                    // Make sure to start from the first packet after the header.
                    dp.BaseStream.Seek(actualByte - 1, SeekOrigin.Begin);

                    // read all the data packets
                    while ((totalBytes - actualByte) < 257)
                    {
                        PacketSniffer snif = new PacketSniffer(dp.ReadBytes(256));
                        if (snif.MagicNumber == MagicNumber)
                        {
                            _PacketIndexes.Add(new IndexEntry(snif.HeaderType, actualByte));
                            Statistics.AddPacket(snif);
                            // Update the reading position
                            actualByte = actualByte + snif.NumberBytesThisRecord;
                            dp.BaseStream.Seek(actualByte - 1, SeekOrigin.Begin);
                        }
                        else
                        {
                            // Update the reading position
                            actualByte = actualByte + 1;
                            dp.BaseStream.Seek(actualByte - 1, SeekOrigin.Begin);
                        }
                    }
                }
            }

        }

        #endregion constructor

        #region functions


        #endregion functions

    }
}
