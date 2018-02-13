using System;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Define the base data packet.
    /// </summary>
    public class QpsMultiTxEntry
    {
        #region private properties

        private Int32 _Id;
        private Single _Intensity;
        private Int32 _Quality;
        private Single _TwoWayTravelTime;
        private Single _DeltaTime;
        private Single _OffsetX;
        private Single _OffsetY;
        private Single _OffsetZ;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the beam ID.
        /// </summary>
        public Int32 Id { get { return _Id; } }

        /// <summary>
        /// Gets the signal strength.
        /// </summary>
        public Single Intensity { get { return _Intensity; } }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        public Int32 Quality { get { return _Quality; } }

        /// <summary>
        /// Gets the two way travel time, in seconds.
        /// </summary>
        public Single TwoWayTravelTime { get { return _TwoWayTravelTime; } }

        /// <summary>
        /// Gets the difference between header, in seconds.
        /// </summary>
        public Single DeltaTime { get { return _DeltaTime; } }

        /// <summary>
        /// Gets the location of ship's reference frame.
        /// </summary>
        public Single OffsetX { get { return _OffsetX; } }

        /// <summary>
        /// Gets the location of ship's reference frame.
        /// </summary>
        public Single OffsetY { get { return _OffsetY; } }

        /// <summary>
        /// Gets the location of ship's reference frame.
        /// </summary>
        public Single OffsetZ { get { return _OffsetZ; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the QpsMultiTxEntry class that has default zero values.
        /// </summary>
        public QpsMultiTxEntry()
        {
            _Id = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;
            _DeltaTime = 0;
            _OffsetX = 0;
            _OffsetY = 0;
            _OffsetZ = 0;
        }

        /// <summary>
        /// Initializes a new instance of the QpsMultiTxEntry class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public QpsMultiTxEntry(Byte[] byteArray)
        {
            _Id = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;
            _DeltaTime = 0;
            _OffsetX = 0;
            _OffsetY = 0;
            _OffsetZ = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 32)
                {
                    _Id = dp.ReadInt32(); // 0-1-2-3
                    _Intensity = dp.ReadSingle(); // 4-5-6-7
                    _Quality = dp.ReadInt32(); // 8-9-10-11
                    _TwoWayTravelTime = dp.ReadSingle(); // 12-13-14-15
                    _DeltaTime = dp.ReadSingle(); // 16-17-18-19
                    _OffsetX = dp.ReadSingle(); // 20-21-22-23
                    _OffsetY = dp.ReadSingle(); // 24-25-26-27
                    _OffsetZ = dp.ReadSingle(); // 28-29-30-31
                }
            }
        }

        #endregion constructors
    }
}