using System;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Define the base data packet.
    /// </summary>
    public class QpsMbeEntry
    {
        #region private properties

        private Int32 _Id;
        private Double _Intensity;
        private Int32 _Quality;
        private Double _TwoWayTravelTime;
        private Double _DeltaTime;
        private Double _BeamAngle;
        private Double _TiltAngle;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the beam ID.
        /// </summary>
        public Int32 Id { get { return _Id; } }

        /// <summary>
        /// Gets the signal strength.
        /// </summary>
        public Double Intensity { get { return _Intensity; } }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        public Int32 Quality { get { return _Quality; } }

        /// <summary>
        /// Gets the two way travel time, in seconds.
        /// </summary>
        public Double TwoWayTravelTime { get { return _TwoWayTravelTime; } }

        /// <summary>
        /// Gets the beam time offset. DeltaTime can be used for profilers to calculate the ping time per beam.
        /// </summary>
        public Double DeltaTime { get { return _DeltaTime; } }

        /// <summary>
        /// Gets the beam angle. BeamAngle convention: negative to port side, nadir beam 0 degrees, positive to starboard side.
        /// </summary>
        public Double BeamAngle { get { return _BeamAngle; } }

        /// <summary>
        /// Gets the tilt angle. TiltAngle convention: positive forward, negative backward (used for pitch steering).
        /// </summary>
        public Double TiltAngle { get { return _TiltAngle; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the QpsMbeEntry class that has default zero values.
        /// </summary>
        public QpsMbeEntry()
        {
            _Id = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;
            _DeltaTime = 0;
            _BeamAngle = 0;
            _TiltAngle = 0;
        }

        /// <summary>
        /// Initializes a new instance of the QpsMbeEntry class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public QpsMbeEntry(Byte[] byteArray)
        {
            _Id = 0;
            _Intensity = 0;
            _Quality = 0;
            _TwoWayTravelTime = 0;
            _DeltaTime = 0;
            _BeamAngle = 0;
            _TiltAngle = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 48)
                {
                    _Id = dp.ReadInt32(); // 0-1-2-3
                    _Intensity = dp.ReadDouble(); // 4-5-6-7-8-9-10-11
                    _Quality = dp.ReadInt32(); // 12-13-14-15
                    _TwoWayTravelTime = dp.ReadDouble(); // 16-17-18-19-20-21-22-23
                    _DeltaTime = dp.ReadDouble(); // 24-25-26-27-28-29-30-31
                    _BeamAngle = dp.ReadDouble(); // 32-33-34-35-36-37-38-39
                    _TiltAngle = dp.ReadDouble(); // 40-41-42-43-44-45-46-47
                }
            }
        }

        #endregion constructors
    }
}