using System;
using System.IO;
using stdlibXtf.Enums;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Channel information structure (contained in the file header).
    /// One-time information describing each channel.
    /// This is data pertaining to each channel that will NOT change during the course of a run.
    /// 128 bytes long.
    /// </summary>
    public class ChannelInfo
    {
        #region private properties

        private ChannelTypes _TypeOfChannel;
        private Byte _SubChannelNumber;
        private CorrectionFlags _CorrectionFlag;
        private DataPolarity _Polarity;
        private UInt16 _BytesPerSample;
        private UInt32 _SamplesPerChannel;
        private String _ChannelName;
        private Single _VoltScale;
        private Single _Frequency;
        private Single _HorizontalBeamAngle;
        private Single _TiltAngle;
        private Single _BeamWidth;
        private Single _OffsetX;
        private Single _OffsetY;
        private Single _OffsetZ;
        private Single _OffsetYaw;
        private Single _OffsetPitch;
        private Single _OffsetRoll;
        private UInt16 _BeamsPerArray;
        private Single _Latency;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the kind of channel (Subbottom, Port, Starboard or Bathymetry).
        /// </summary>
        public ChannelTypes TypeOfChannel { get { return _TypeOfChannel; } }

        /// <summary>
        /// Gets the index for which this ChannelInfo is.
        /// </summary>
        public Byte SubChannelNumber { get { return _SubChannelNumber; } }

        /// <summary>
        /// Gets the way the data are stored.
        /// </summary>
        public CorrectionFlags CorrectionFlag { get { return _CorrectionFlag; } }

        /// <summary>
        /// Gets the polarity of the data.
        /// </summary>
        public DataPolarity Polarity { get { return _Polarity; } }

        /// <summary>
        /// Gets the quantity of bytes for each sample stored.
        /// </summary>
        public UInt16 BytesPerSample { get { return _BytesPerSample; } }

        /// <summary>
        /// Gets the number of samples per channel. This value will be overridden by the NumberSamples in PingChannelHeader subpacket.
        /// </summary>
        public UInt32 SamplesPerChannel { get { return _SamplesPerChannel; } }

        /// <summary>
        /// Gets the description of the channel.
        /// </summary>
        public String ChannelName { get { return _ChannelName; } }

        /// <summary>
        /// Gets how many volts are represented by a maximum sample value in the range -5.0 to +4.9998 volts.
        /// </summary>
        public Single VoltScale { get { return _VoltScale; } }

        /// <summary>
        /// Gets the center transmit frequency.
        /// </summary>
        public Single Frequency { get { return _Frequency; } }

        /// <summary>
        /// Gets the horizontal beam angle, tipically 1 degree.
        /// </summary>
        public Single HorizontalBeamAngle { get { return _HorizontalBeamAngle; } }

        /// <summary>
        /// Gets the tilt angle, tipically 30 degrees.
        /// </summary>
        public Single TiltAngle { get { return _TiltAngle; } }

        /// <summary>
        /// Gets the 3 dB beam width, tipically 50 degrees.
        /// </summary>
        public Single BeamWidth { get { return _BeamWidth; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive X is to starboard.
        /// </summary>
        public Single OffsetX { get { return _OffsetX; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive Y is forward.
        /// </summary>
        public Single OffsetY { get { return _OffsetY; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive Z is down, just like depth.
        /// </summary>
        public Single OffsetZ { get { return _OffsetZ; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive yaw is turn to right.
        /// If the multibeam sensor is reverse mounted (facing backwards), then OffsetYaw will be around 180 degrees.
        /// </summary>
        public Single OffsetYaw { get { return _OffsetYaw; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive pitch is nose up.
        /// </summary>
        public Single OffsetPitch { get { return _OffsetPitch; } }

        /// <summary>
        /// Gets the offset of MultiBeam system. Orientation of positive roll is lean to starboard.
        /// </summary>
        public Single OffsetRoll { get { return _OffsetRoll; } }

        /// <summary>
        /// Gets the beams for each array. Valid only for forward looking sonar systems.
        /// </summary>
        public UInt16 BeamsPerArray { get { return _BeamsPerArray; } }

        /// <summary>
        /// Gets the latency in milliseconds.
        /// </summary>
        public Single Latency { get { return _Latency; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the ChannelInfo class that has default zero values.
        /// </summary>
        public ChannelInfo()
        {
            _TypeOfChannel = ChannelTypes.Subbottom;
            _SubChannelNumber = 0;
            _CorrectionFlag = CorrectionFlags.Range;
            _Polarity = DataPolarity.Bipolar;
            _BytesPerSample = 0;
            _SamplesPerChannel = 0;
            _ChannelName = " ";
            _VoltScale = 0;
            _Frequency = 0;
            _HorizontalBeamAngle = 0;
            _TiltAngle = 0;
            _BeamWidth = 0;
            _OffsetX = 0;
            _OffsetY = 0;
            _OffsetZ = 0;
            _OffsetYaw = 0;
            _OffsetPitch = 0;
            _OffsetRoll = 0;
            _BeamsPerArray = 0;
            _Latency = 0;
        }

        /// <summary>
        /// Initializes a new instance of the ChannelInfo class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 128 bytes.</param>
        public ChannelInfo(Byte[] byteArray)
        {
            _TypeOfChannel = ChannelTypes.Subbottom;
            _SubChannelNumber = 0;
            _CorrectionFlag = CorrectionFlags.Range;
            _Polarity = DataPolarity.Bipolar;
            _BytesPerSample = 0;
            _SamplesPerChannel = 0;
            _ChannelName = " ";
            _VoltScale = 0;
            _Frequency = 0;
            _HorizontalBeamAngle = 0;
            _TiltAngle = 0;
            _BeamWidth = 0;
            _OffsetX = 0;
            _OffsetY = 0;
            _OffsetZ = 0;
            _OffsetYaw = 0;
            _OffsetPitch = 0;
            _OffsetRoll = 0;
            _BeamsPerArray = 0;
            _Latency = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 128)
                {
                    _TypeOfChannel = (ChannelTypes)dp.ReadByte(); // 0
                    _SubChannelNumber = dp.ReadByte(); // 1
                    _CorrectionFlag = (CorrectionFlags)dp.ReadUInt16(); // 2-3
                    _Polarity = (DataPolarity)dp.ReadUInt16(); // 4-5
                    _BytesPerSample = dp.ReadUInt16(); // 6-7
                    _SamplesPerChannel = dp.ReadUInt32(); // 8-9-10-11
                    _ChannelName = new String(dp.ReadChars(16)); // 12-13-14-15-16-17-18-19-20-21-22-23-24-25-26-27
                    _VoltScale = dp.ReadSingle(); // 28-29-30-31
                    _Frequency = dp.ReadSingle(); // 32-33-34-35
                    _HorizontalBeamAngle = dp.ReadSingle(); // 36-37-38-39
                    _TiltAngle = dp.ReadSingle(); // 40-41-42-43
                    _BeamWidth = dp.ReadSingle(); // 44-45-46-47
                    _OffsetX = dp.ReadSingle(); // 48-49-50-51
                    _OffsetY = dp.ReadSingle(); // 52-53-54-55
                    _OffsetZ = dp.ReadSingle(); // 56-57-58-59
                    _OffsetYaw = dp.ReadSingle(); // 60-61-62-63
                    _OffsetPitch = dp.ReadSingle(); // 64-65-66-67
                    _OffsetRoll = dp.ReadSingle(); // 68-69-70-71
                    _BeamsPerArray = dp.ReadUInt16(); // 72-73
                    _Latency = dp.ReadSingle(); // 74-75-76-77
                }
            }
        }

        #endregion constructors
    }
}