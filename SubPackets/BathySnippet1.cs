using System;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Represent the beam part of the bathy data attached to a PingHeader packet.
    /// </summary>
    public class BathySnippet1
    {
        #region private properties

        private UInt32 MagicId = 0x534E5031;
        private UInt16 _HeaderSize;
        private UInt16 _DataSize;
        private UInt32 _PingNumber;
        private UInt16 _Beam;
        private UInt16 _SnipSamples;
        private UInt16 _GainStart;
        private UInt16 _GainEnd;
        private UInt16 _FragOffset;
        private UInt16 _FragSamples;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Identifier code. SNP1= 0x534E5031
        /// </summary>
        public UInt32 IdentifierCode { get { return MagicId; } }

        /// <summary>
        /// Gets the size, in bytes, of this header packet.
        /// </summary>
        public UInt16 HeaderSize { get { return _HeaderSize; } }

        /// <summary>
        /// Gets the size, in bytes, of the following data packets.
        /// </summary>
        public UInt16 DataSize { get { return _DataSize; } }

        /// <summary>
        /// Gets the sequential number of the ping.
        /// </summary>
        public UInt32 PingNumber { get { return _PingNumber; } }

        /// <summary>
        /// Gets the beam number, from 0 to total number minus 1.
        /// </summary>
        public UInt16 Beam { get { return _Beam; } }

        /// <summary>
        /// Gets the snippet size, samples.
        /// </summary>
        public UInt16 SnipSamples { get { return _SnipSamples; } }

        /// <summary>
        /// Gets the gain at start of snippet, 0.01 dB steps.
        /// </summary>
        public UInt16 GainStart { get { return _GainStart; } }

        /// <summary>
        /// Gets the gain at end of snippet, 0.01 dB steps.
        /// </summary>
        public UInt16 GainEnd { get { return _GainEnd; } }

        /// <summary>
        /// Gets the fragment offset, samples from ping.
        /// </summary>
        public UInt16 FragOffset { get { return _FragOffset; } }

        /// <summary>
        /// Gets the fragment size, samples.
        /// </summary>
        public UInt16 FragSamples { get { return _FragSamples; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the BathySnippet1 class that has default zero values.
        /// </summary>
        public BathySnippet1()
        {
            _HeaderSize = 0;
            _DataSize = 0;
            _PingNumber = 0;
            _Beam = 0;
            _SnipSamples = 0;
            _GainStart = 0;
            _GainEnd = 0;
            _FragOffset = 0;
            _FragSamples = 0;
        }

        /// <summary>
        /// Initializes a new instance of the BathySnippet1 class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 24 bytes.</param>
        public BathySnippet1(Byte[] byteArray)
        {
            _HeaderSize = 0;
            _DataSize = 0;
            _PingNumber = 0;
            _Beam = 0;
            _SnipSamples = 0;
            _GainStart = 0;
            _GainEnd = 0;
            _FragOffset = 0;
            _FragSamples = 0;

            UInt32 chkNumber;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 24)
                {
                    chkNumber = dp.ReadUInt32(); // 0-1-2-3
                    if (chkNumber == MagicId)
                    {
                        _HeaderSize = dp.ReadUInt16(); // 4-5
                        _DataSize = dp.ReadUInt16(); // 6-7
                        _PingNumber = dp.ReadUInt32(); // 8-9-10-11
                        _Beam = dp.ReadUInt16(); // 12-13
                        _SnipSamples = dp.ReadUInt16(); // 14-15
                        _GainStart = dp.ReadUInt16(); // 16-17
                        _GainEnd = dp.ReadUInt16(); // 18-19
                        _FragOffset = dp.ReadUInt16(); // 20-21
                        _FragSamples = dp.ReadUInt16(); // 22-23
                    }
                }
            }
        }

        #endregion constructors
    }
}