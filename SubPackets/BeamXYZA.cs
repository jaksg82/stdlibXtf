using System;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Represent the processed beam of the bathy data attached to a PingHeader packet.
    /// </summary>
    public class BeamXYZA
    {
        #region private properties

        private Double _PositionOffsetX;
        private Double _PositionOffsetY;
        private Single _Depth;
        private Double _Time;
        private Int16 _Amplitude;
        private Byte _Quality;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the offset from fish.
        /// </summary>
        public Double PositionOffsetX { get { return _PositionOffsetX; } }

        /// <summary>
        /// Gets the offset from fish.
        /// </summary>
        public Double PositionOffsetY { get { return _PositionOffsetY; } }

        /// <summary>
        /// Gets the absolute depth.
        /// </summary>
        public Single Depth { get { return _Depth; } }

        /// <summary>
        /// Gets the two way travel time.
        /// </summary>
        public Double Time { get { return _Time; } }

        /// <summary>
        /// Gets the amplitude.
        /// </summary>
        public Int16 Amplitude { get { return _Amplitude; } }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        public Byte Quality { get { return _Quality; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the BeamXYZA class that has default zero values.
        /// </summary>
        public BeamXYZA()
        {
            _PositionOffsetX = 0;
            _PositionOffsetY = 0;
            _Depth = 0;
            _Time = 0;
            _Amplitude = 0;
            _Quality = 0;
        }

        /// <summary>
        /// Initializes a new instance of the BeamXYZA class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 31 bytes.</param>
        public BeamXYZA(Byte[] byteArray)
        {
            _PositionOffsetX = 0;
            _PositionOffsetY = 0;
            _Depth = 0;
            _Time = 0;
            _Amplitude = 0;
            _Quality = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 31)
                {
                    _PositionOffsetY = dp.ReadDouble(); // 0-1-2-3-4-5-6-7
                    _PositionOffsetX = dp.ReadDouble(); // 8-9-10-11-12-13-14-15
                    _Depth = dp.ReadSingle(); // 16-17-18-19
                    _Time = dp.ReadDouble(); // 20-21-22-23-24-25-26-27
                    _Amplitude = dp.ReadInt16(); // 28-29
                    _Quality = dp.ReadByte(); // 30
                }
            }
        }

        #endregion constructors
    }
}