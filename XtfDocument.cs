using stdlibXtf.SubPackets;
using stdlibXtf.Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace stdlibXtf
{
    /// <summary>
    /// Represents a document to read XTF files.
    /// </summary>
    public class XtfDocument
    {
        #region private properties

        private XtfMainHeader _MainHeader;
        private List<ChannelInfo> _Channels;
        private List<IndexEntry> _PacketIndexes;
        private StatCollection _Stats;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the number that identify the correct start of the packet.
        /// </summary>
        static public UInt16 MagicNumber { get { return 64206; } }

        /// <summary>
        /// Gets the XtfMainHeader object that contain the document header information.
        /// </summary>
        public XtfMainHeader MainHeader { get { return _MainHeader; } }

        /// <summary>
        /// Gets a list of the sonar channels, and the relative informations, contained in this document.
        /// </summary>
        public List<ChannelInfo> Channels { get { return _Channels; } }

        /// <summary>
        /// Gets a list of all the packets, and their position, stored inside this document.
        /// </summary>
        public List<IndexEntry> Packets { get { return _PacketIndexes; } }

        /// <summary>
        /// Gets a summary of the packets type and informations stored inside this document.
        /// </summary>
        public StatCollection Statistics { get { return _Stats; } }

        #endregion public properties

        #region constructor

        /// <summary>
        /// Initializes a new instance of the XtfDocument class that has default zero values.
        /// </summary>
        public XtfDocument()
        {
            _MainHeader = new XtfMainHeader();
            _Channels = new List<ChannelInfo> { };
            _PacketIndexes = new List<IndexEntry> { };
            _Stats = new StatCollection();
        }

        /// <summary>
        /// Initializes a new instance of the XtfDocument class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray"></param>
        public XtfDocument(Byte[] byteArray)
        {
            _MainHeader = new XtfMainHeader();
            _Channels = new List<ChannelInfo> { };
            _PacketIndexes = new List<IndexEntry> { };
            _Stats = new StatCollection();

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
                    bool IsEndOfStream = false;
                    long bytesToEnd;

                    while (!IsEndOfStream)
                    {
                        try
                        {
                            // Explicit cast to fix the difference operator with uint values
                            bytesToEnd = (long)totalBytes - (long)actualByte;

                            if (bytesToEnd > 256)
                            {
                                PacketSniffer snif = new PacketSniffer(dp.ReadBytes(256));
                                if (snif.MagicNumber == MagicNumber)
                                {
                                    _PacketIndexes.Add(new IndexEntry(snif.HeaderType, actualByte));
                                    _Stats.AddPacket(snif);
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

                                IsEndOfStream = false;
                            }
                            else
                            {
                                if (bytesToEnd >= 64)
                                {
                                    PacketSniffer snif = new PacketSniffer(dp.ReadBytes(14));
                                    if (snif.MagicNumber == MagicNumber)
                                    {
                                        _PacketIndexes.Add(new IndexEntry(snif.HeaderType, actualByte));
                                        _Stats.AddPacket(snif);
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
                                    IsEndOfStream = true;
                                }
                                else
                                {
                                    IsEndOfStream = true;
                                }
                            }
                        }
                        catch
                        {
                            // Error
                        }
                    }
                }
            }
        }

        #endregion constructor
    }
}