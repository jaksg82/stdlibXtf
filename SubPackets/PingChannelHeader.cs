using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Represent the channel informations that can be unique from ping to ping.
    /// </summary>
    public class PingChannelHeader
    {
        #region private properties

        private UInt16 _ChannelNumber;
        private UInt16 _DownSampleMethod;
        private Single _SlantRange;
        private Single _GroundRange;
        private Single _TimeDelay;
        private Single _TimeDuration;
        private Single _SecondsPerPing;
        private UInt16 _ProcessingFlags;
        private UInt16 _Frequency;
        private UInt16 _InitialGainCode;
        private UInt16 _GainCode;
        private UInt16 _Bandwidth;
        private UInt32 _ContactNumber;
        private UInt16 _ContactClassification;
        private Byte _ContactSubNumber;
        private Byte _ContactType;
        private UInt32 _NumberSamples;
        private UInt16 _MillivoltScale;
        private Single _ContactTimeOffTrack;
        private Byte _ContactCloseNumber;
        private Single _FixedVSOP;
        private Int16 _Weight;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the number of the channel.
        /// Typically: 0=port low frequency, 1=starboard low frequency, 2=port high frequency and 3=starboard high frequency.
        /// </summary>
        public UInt16 ChannelNumber { get { return _ChannelNumber; } }

        /// <summary>
        /// Gets the method used for the downsample. 2=MAX, 4=RMS.
        /// </summary>
        public UInt16 DownSampleMethod { get { return _DownSampleMethod; } }

        /// <summary>
        /// Gets the slant range of the data in meters.
        /// </summary>
        public Single SlantRange { get { return _SlantRange; } }

        /// <summary>
        /// Gets the ground range of the data in meters (SlantRange^2 - Altitude^2).
        /// </summary>
        public Single GroundRange { get { return _GroundRange; } }

        /// <summary>
        /// Gets the amount of time, in seconds, to the start of recorded data.
        /// </summary>
        public Single TimeDelay { get { return _TimeDelay; } }

        /// <summary>
        /// Gets the amount of time, in seconds, recorded. Typically SlantRange/750.
        /// </summary>
        public Single TimeDuration { get { return _TimeDuration; } }

        /// <summary>
        /// Gets the amount of time, in seconds, from ping to ping (SlantRange/750).
        /// </summary>
        public Single SecondsPerPing { get { return _SecondsPerPing; } }

        /// <summary>
        /// Gets the kind of processing used.
        /// </summary>
        public UInt16 ProcessingFlags { get { return _ProcessingFlags; } }

        /// <summary>
        /// Gets the center transmit frequency for this channel.
        /// </summary>
        public UInt16 Frequency { get { return _Frequency; } }

        /// <summary>
        /// Gets the setting as transmitted by sonar.
        /// </summary>
        public UInt16 InitialGainCode { get { return _InitialGainCode; } }

        /// <summary>
        /// Gets the setting as transmitted by sonar.
        /// </summary>
        public UInt16 GainCode { get { return _GainCode; } }

        /// <summary>
        /// Gets the setting as transmitted by sonar.
        /// </summary>
        public UInt16 Bandwidth { get { return _Bandwidth; } }

        /// <summary>
        /// Gets the contact number. Updated when contacts are saved in Target utility.
        /// </summary>
        public UInt32 ContactNumber { get { return _ContactNumber; } }

        /// <summary>
        /// Gets the contact classification. Updated when contacts are saved in Target utility.
        /// </summary>
        public UInt16 ContactClassification { get { return _ContactClassification; } }

        /// <summary>
        /// Gets the contact sub number. Updated when contacts are saved in Target utility.
        /// </summary>
        public Byte ContactSubNumber { get { return _ContactSubNumber; } }

        /// <summary>
        /// Gets the contact type. Updated when contacts are saved in Target utility.
        /// </summary>
        public Byte ContactType { get { return _ContactType; } }

        /// <summary>
        /// Gets the number of samples that will follow this packet.
        /// The number of bytes will be this value multiplied by the number of bytes per sample (ChannelInfo.BytesPerSample).
        /// </summary>
        public UInt32 NumberSamples { get { return _NumberSamples; } }

        /// <summary>
        /// Gets the maximum voltage, in milliVolts, represented by a full-scale value in the data.
        /// If zero, then the value stored in ChannelInfo.VoltScale should be used instead.
        /// </summary>
        public UInt16 MillivoltScale { get { return _MillivoltScale; } }

        /// <summary>
        /// Gets the time off track to this contact, stored in milliseconds.
        /// </summary>
        public Single ContactTimeOffTrack { get { return _ContactTimeOffTrack; } }

        /// <summary>
        /// Gets the number.
        /// </summary>
        public Byte ContactCloseNumber { get { return _ContactCloseNumber; } }

        /// <summary>
        /// Gets the fixed along-track size of each ping, in centimeters.
        /// On multibeam systems this value need to be filled in to prevent Isis from calculating along-track ground coverage based on beam spread and spedd over ground.
        /// </summary>
        public Single FixedVSOP { get { return _FixedVSOP; } }

        /// <summary>
        /// Gets the weighting factor passed by some sonars.
        /// This value is mandatory for Edgetech digital sonars types 24, 35, 38 and Kongsberg SA type 48.
        /// </summary>
        public Int16 Weight { get { return _Weight; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the PingChannelHeader class that has default zero values.
        /// </summary>
        public PingChannelHeader()
        {
            _ChannelNumber = 0;
            _DownSampleMethod = 0;
            _SlantRange = 0;
            _GroundRange = 0;
            _TimeDelay = 0;
            _TimeDuration = 0;
            _SecondsPerPing = 0;
            _ProcessingFlags = 0;
            _Frequency = 0;
            _InitialGainCode = 0;
            _GainCode = 0;
            _Bandwidth = 0;
            _ContactNumber = 0;
            _ContactClassification = 0;
            _ContactSubNumber = 0;
            _ContactType = 0;
            _NumberSamples = 0;
            _MillivoltScale = 0;
            _ContactTimeOffTrack = 0;
            _ContactCloseNumber = 0;
            _FixedVSOP = 0;
            _Weight = 0;
        }

        /// <summary>
        /// Initializes a new instance of the PingChannelHeader class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 64 bytes.</param>
        public PingChannelHeader(Byte[] byteArray)
        {
            _ChannelNumber = 0;
            _DownSampleMethod = 0;
            _SlantRange = 0;
            _GroundRange = 0;
            _TimeDelay = 0;
            _TimeDuration = 0;
            _SecondsPerPing = 0;
            _ProcessingFlags = 0;
            _Frequency = 0;
            _InitialGainCode = 0;
            _GainCode = 0;
            _Bandwidth = 0;
            _ContactNumber = 0;
            _ContactClassification = 0;
            _ContactSubNumber = 0;
            _ContactType = 0;
            _NumberSamples = 0;
            _MillivoltScale = 0;
            _ContactTimeOffTrack = 0;
            _ContactCloseNumber = 0;
            _FixedVSOP = 0;
            _Weight = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 60)
                {
                    _ChannelNumber = dp.ReadUInt16(); // 0-1
                    _DownSampleMethod = dp.ReadUInt16(); // 2-3
                    _SlantRange = dp.ReadSingle(); // 4-5-6-7
                    _GroundRange = dp.ReadSingle(); // 8-9-10-11
                    _TimeDelay = dp.ReadSingle(); // 12-13-14-15
                    _TimeDuration = dp.ReadSingle(); // 16-17-18-19
                    _SecondsPerPing = dp.ReadSingle(); // 20-21-22-23
                    _ProcessingFlags = dp.ReadUInt16(); // 24-25
                    _Frequency = dp.ReadUInt16(); // 26-27
                    _InitialGainCode = dp.ReadUInt16(); // 28-29
                    _GainCode = dp.ReadUInt16(); // 30-31
                    _Bandwidth = dp.ReadUInt16(); // 32-33
                    _ContactNumber = dp.ReadUInt32(); // 34-35-36-37
                    _ContactClassification = dp.ReadUInt16(); // 38-39
                    _ContactSubNumber = dp.ReadByte(); // 40
                    _ContactType = dp.ReadByte(); // 41
                    _NumberSamples = dp.ReadUInt32(); // 42-43-44-45
                    _MillivoltScale = dp.ReadUInt16(); // 46-47
                    _ContactTimeOffTrack = dp.ReadSingle(); // 48-49-50-51
                    _ContactCloseNumber = dp.ReadByte(); // 52
                    dp.ReadByte(); // 53 Unused
                    _FixedVSOP = dp.ReadSingle(); // 54-55-56-57
                    _Weight = dp.ReadInt16(); // 58-59
                }
            }
        }

        #endregion constructors
    }
}