using System;
using System.IO;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Represent the header part of the bathy data attached to a PingHeader packet.
    /// </summary>
    public class BathySnippet0
    {
        #region private properties

        private UInt32 MagicId = 0x534E5030;
        private UInt16 _HeaderSize;
        private UInt16 _DataSize;
        private UInt32 _PingNumber;
        private UInt32 _Seconds;
        private UInt32 _Milliseconds;
        private UInt16 _Latency;
        private UInt16 _SonarId1;
        private UInt16 _SonarId2;
        private UInt16 _SonarModel;
        private UInt16 _Frequency;
        private UInt16 _SoundSpeed;
        private UInt16 _SampleRate;
        private UInt16 _PingRate;
        private UInt16 _Range;
        private UInt16 _Power;
        private UInt16 _Gain;
        private UInt16 _PulseWidth;
        private UInt16 _Spread;
        private UInt16 _Absorb;
        private UInt16 _ProjectorType;
        private UInt16 _ProjectorWidth;
        private UInt16 _SpacingNumerator;
        private UInt16 _SpacingDenominator;
        private Int16 _ProjectorAngle;
        private UInt16 _MinRange;
        private UInt16 _MaxRange;
        private UInt16 _MinDepth;
        private UInt16 _MaxDepth;
        private UInt16 _Filters;
        private UInt16 _Flags;
        private Int16 _HeadTemp;
        private UInt16 _BeamCount;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Identifier code. SNP0= 0x534E5030
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
        /// Gets the time, in seconds, since 00:00:00 of 1st January 1970.
        /// </summary>
        public UInt32 Seconds { get { return _Seconds; } }

        /// <summary>
        /// Gets the milliseconds part of the time.
        /// </summary>
        public UInt32 Milliseconds { get { return _Milliseconds; } }

        /// <summary>
        /// Gets the time from ping to output in milliseconds.
        /// </summary>
        public UInt16 Latency { get { return _Latency; } }

        /// <summary>
        /// Gets the least significant four bytes of ethernet address.
        /// </summary>
        public UInt16 SonarId1 { get { return _SonarId1; } }

        /// <summary>
        /// Gets the least significant four bytes of ethernet address.
        /// </summary>
        public UInt16 SonarId2 { get { return _SonarId2; } }

        /// <summary>
        /// Gets the coded model number of sonar.
        /// </summary>
        public UInt16 SonarModel { get { return _SonarModel; } }

        /// <summary>
        /// Gets the sonar frequency in kHz.
        /// </summary>
        public UInt16 Frequency { get { return _Frequency; } }

        /// <summary>
        /// Gets the programmed sound velocity in m/s
        /// </summary>
        public UInt16 SoundSpeed { get { return _SoundSpeed; } }

        /// <summary>
        /// Gets the samples per seconds.
        /// </summary>
        public UInt16 SampleRate { get { return _SampleRate; } }

        /// <summary>
        /// Gets the pings per second, 0.001 Hz steps.
        /// </summary>
        public UInt16 PingRate { get { return _PingRate; } }

        /// <summary>
        /// Gets the range setting in meters.
        /// </summary>
        public UInt16 Range { get { return _Range; } }

        /// <summary>
        /// Gets the power setting.
        /// </summary>
        public UInt16 Power { get { return _Power; } }

        /// <summary>
        /// Gets the gain settings (b15 = auto, b14 = TVG, b6..0 = gain).
        /// </summary>
        public UInt16 Gain { get { return _Gain; } }

        /// <summary>
        /// Gets the transmit pulse width in microseconds.
        /// </summary>
        public UInt16 PulseWidth { get { return _PulseWidth; } }

        /// <summary>
        /// Gets the TVG spreading, n*log(R), 0.25dB steps.
        /// </summary>
        public UInt16 Spread { get { return _Spread; } }

        /// <summary>
        /// Gets the TVG absorption, dB/km, 1dB steps.
        /// </summary>
        public UInt16 Absorb { get { return _Absorb; } }

        /// <summary>
        /// Gets the type of projector (b7 = steering, b4..0 = projector type).
        /// </summary>
        public UInt16 ProjectorType { get { return _ProjectorType; } }

        /// <summary>
        /// Gets the transmit beam width along track, 0.1 degrees steps.
        /// </summary>
        public UInt16 ProjectorWidth { get { return _ProjectorWidth; } }

        /// <summary>
        /// Gets the receiver beam spacing, numerator part in degrees.
        /// </summary>
        public UInt16 SpacingNumerator { get { return _SpacingNumerator; } }

        /// <summary>
        /// Gets the receiver beam spacing, denominator part in degrees.
        /// </summary>
        public UInt16 SpacingDenominator { get { return _SpacingDenominator; } }

        /// <summary>
        /// Gets the projector steering, degrees * Pkt_Steer_Res.
        /// </summary>
        public Int16 ProjectorAngle { get { return _ProjectorAngle; } }

        /// <summary>
        /// Gets the range filter settings.
        /// </summary>
        public UInt16 MinRange { get { return _MinRange; } }

        /// <summary>
        /// Gets the range filter settings.
        /// </summary>
        public UInt16 MaxRange { get { return _MaxRange; } }

        /// <summary>
        /// Gets the depth filter settings.
        /// </summary>
        public UInt16 MinDepth { get { return _MinDepth; } }

        /// <summary>
        /// Gets the depth filter settings.
        /// </summary>
        public UInt16 MaxDepth { get { return _MaxDepth; } }

        /// <summary>
        /// Gets the enabled filters (b1=depth, b0=range).
        /// </summary>
        public UInt16 Filters { get { return _Filters; } }

        /// <summary>
        /// Gets the settings flags (b0..11=spare, b12..14=snip mode, b15=roll stab, b0=roll stab enabled).
        /// </summary>
        public UInt16 Flags { get { return _Flags; } }

        /// <summary>
        /// Gets the head temperature, 0.1 celsius steps.
        /// </summary>
        public Int16 HeadTemp { get { return _HeadTemp; } }

        /// <summary>
        /// Gets the number of beams.
        /// </summary>
        public UInt16 BeamCount { get { return _BeamCount; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the BathySnippet0 class that has default zero values.
        /// </summary>
        public BathySnippet0()
        {
            _HeaderSize = 0;
            _DataSize = 0;
            _PingNumber = 0;
            _Seconds = 0;
            _Milliseconds = 0;
            _Latency = 0;
            _SonarId1 = 0;
            _SonarId2 = 0;
            _SonarModel = 0;
            _Frequency = 0;
            _SoundSpeed = 0;
            _SampleRate = 0;
            _PingRate = 0;
            _Range = 0;
            _Power = 0;
            _Gain = 0;
            _PulseWidth = 0;
            _Spread = 0;
            _Absorb = 0;
            _ProjectorType = 0;
            _ProjectorWidth = 0;
            _SpacingNumerator = 0;
            _SpacingDenominator = 0;
            _ProjectorAngle = 0;
            _MinRange = 0;
            _MaxRange = 0;
            _MinDepth = 0;
            _MaxDepth = 0;
            _Filters = 0;
            _Flags = 0;
            _HeadTemp = 0;
            _BeamCount = 0;
        }

        /// <summary>
        /// Initializes a new instance of the BathySnippet0 class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 74 bytes.</param>
        public BathySnippet0(Byte[] byteArray)
        {
            _HeaderSize = 0;
            _DataSize = 0;
            _PingNumber = 0;
            _Seconds = 0;
            _Milliseconds = 0;
            _Latency = 0;
            _SonarId1 = 0;
            _SonarId2 = 0;
            _SonarModel = 0;
            _Frequency = 0;
            _SoundSpeed = 0;
            _SampleRate = 0;
            _PingRate = 0;
            _Range = 0;
            _Power = 0;
            _Gain = 0;
            _PulseWidth = 0;
            _Spread = 0;
            _Absorb = 0;
            _ProjectorType = 0;
            _ProjectorWidth = 0;
            _SpacingNumerator = 0;
            _SpacingDenominator = 0;
            _ProjectorAngle = 0;
            _MinRange = 0;
            _MaxRange = 0;
            _MinDepth = 0;
            _MaxDepth = 0;
            _Filters = 0;
            _Flags = 0;
            _HeadTemp = 0;
            _BeamCount = 0;

            UInt32 chkNumber;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 74)
                {
                    chkNumber = dp.ReadUInt32(); // 0-1-2-3
                    if (chkNumber == MagicId)
                    {
                        _HeaderSize = dp.ReadUInt16(); // 4-5
                        _DataSize = dp.ReadUInt16(); // 6-7
                        _PingNumber = dp.ReadUInt32(); // 8-9-10-11
                        _Seconds = dp.ReadUInt32(); // 12-13-14-15
                        _Milliseconds = dp.ReadUInt32(); // 16-17-18-19
                        _Latency = dp.ReadUInt16(); // 20-21
                        _SonarId1 = dp.ReadUInt16(); // 22-23
                        _SonarId2 = dp.ReadUInt16(); // 24-25
                        _SonarModel = dp.ReadUInt16(); // 26-27
                        _Frequency = dp.ReadUInt16(); // 28-29
                        _SoundSpeed = dp.ReadUInt16(); // 30-31
                        _SampleRate = dp.ReadUInt16(); // 32-33
                        _PingRate = dp.ReadUInt16(); // 34-35
                        _Range = dp.ReadUInt16(); // 36-37
                        _Power = dp.ReadUInt16(); // 38-39
                        _Gain = dp.ReadUInt16(); // 40-41
                        _PulseWidth = dp.ReadUInt16(); // 42-43
                        _Spread = dp.ReadUInt16(); // 44-45
                        _Absorb = dp.ReadUInt16(); // 46-47
                        _ProjectorType = dp.ReadUInt16(); // 48-49
                        _ProjectorWidth = dp.ReadUInt16(); // 50-51
                        _SpacingNumerator = dp.ReadUInt16(); // 52-53
                        _SpacingDenominator = dp.ReadUInt16(); // 54-55
                        _ProjectorAngle = dp.ReadInt16(); // 56-57
                        _MinRange = dp.ReadUInt16(); // 58-59
                        _MaxRange = dp.ReadUInt16(); // 60-61
                        _MinDepth = dp.ReadUInt16(); // 62-63
                        _MaxDepth = dp.ReadUInt16(); // 64-65
                        _Filters = dp.ReadUInt16(); // 66-67
                        _Flags = dp.ReadUInt16(); // 68-69
                        _HeadTemp = dp.ReadInt16(); // 70-71
                        _BeamCount = dp.ReadUInt16(); // 72-73
                    }
                }
            }
        }

        #endregion constructors
    }
}