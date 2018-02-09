using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Enums;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Channel information structure (contained in the file header).
    /// One-time information describing each channel.
    /// This is data pertaining to each channel that will NOT change during the course of a run.
    /// 64 bytes long.
    /// </summary>
    public class ChannelInfo
    {
        #region private properties

        private String _ChannelName;
        //private Byte _SubChannelNumber;

        #endregion

        #region public properties

        public ChannelTypes TypeOfChannel { get; set; }
        public Byte SubChannelNumber { get; set; }
        public CorrectionFlags CorrectionFlag { get; set; }
        public DataPolarity Polarity { get; set; }
        public UInt16 BytesPerSample { get; set; }
        public UInt32 SamplesPerChannel { get; set; }

        public String ChannelName
        {
            get { return _ChannelName; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _ChannelName = " ".PadRight(16); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 16) { _ChannelName = tmpVal.Substring(0, 16); }
                    else { _ChannelName = tmpVal.PadRight(16); }
                }
            }
        }

        public Single VoltScale { get; set; } // How many volts is represented by max sample value. Typically 5.0.
        public Single Frequency { get; set; } // Center transmit frequency
        public Single HorizontalBeamAngle { get; set; } // Typically 1 degree or so
        public Single TiltAngle { get; set; } // Typically 30 degrees
        public Single BeamWidth { get; set; } // 3dB beam width, Typically 50 degrees

        // Orientation of these offsets:
        // Positive Y is forward
        // Positive X is to starboard
        // Positive Z is down. Just like depth.
        // Positive roll is lean to starboard
        // Positive pitch is nose up
        // Positive yaw is turn to right

        public Single OffsetX { get; set; } // These offsets are entered in the Multibeam setup dialog box.
        public Single OffsetY { get; set; }
        public Single OffsetZ { get; set; }
        public Single OffsetYaw { get; set; } // If the multibeam sensor is reverse mounted (facing backwards), then OffsetYaw will be around 180 degrees.
        public Single OffsetPitch { get; set; }
        public Single OffsetRoll { get; set; }

        public UInt16 BeamsPerArray;

        public Single Latency; // valid up to X34 version

        #endregion

        #region constructors

        public ChannelInfo()
        {
            TypeOfChannel = ChannelTypes.Subbottom;
            SubChannelNumber = 0;
            CorrectionFlag = CorrectionFlags.Range;
            Polarity = DataPolarity.Bipolar;
            BytesPerSample = 0;
            SamplesPerChannel = 0;
            ChannelName = " ";
            VoltScale = 0;
            Frequency = 0;
            HorizontalBeamAngle = 0;
            TiltAngle = 0;
            BeamWidth = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetZ = 0;
            OffsetYaw = 0;
            OffsetPitch = 0;
            OffsetRoll = 0;
            BeamsPerArray = 0;
            Latency = 0;

        }

        public ChannelInfo(Byte[] byteArray)
        {
            TypeOfChannel = ChannelTypes.Subbottom;
            SubChannelNumber = 0;
            CorrectionFlag = CorrectionFlags.Range;
            Polarity = DataPolarity.Bipolar;
            BytesPerSample = 0;
            SamplesPerChannel = 0;
            ChannelName = " ";
            VoltScale = 0;
            Frequency = 0;
            HorizontalBeamAngle = 0;
            TiltAngle = 0;
            BeamWidth = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetZ = 0;
            OffsetYaw = 0;
            OffsetPitch = 0;
            OffsetRoll = 0;
            BeamsPerArray = 0;
            Latency = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 128)
                {
                    TypeOfChannel = (ChannelTypes)dp.ReadByte(); // 0
                    SubChannelNumber = dp.ReadByte(); // 1
                    CorrectionFlag = (CorrectionFlags)dp.ReadUInt16(); // 2-3
                    Polarity = (DataPolarity)dp.ReadUInt16(); // 4-5
                    BytesPerSample = dp.ReadUInt16(); // 6-7
                    SamplesPerChannel = dp.ReadUInt32(); // 8-9-10-11
                    ChannelName = new String(dp.ReadChars(16)); // 12-13-14-15-16-17-18-19-20-21-22-23-24-25-26-27
                    VoltScale = dp.ReadSingle(); // 28-29-30-31
                    Frequency = dp.ReadSingle(); // 32-33-34-35
                    HorizontalBeamAngle = dp.ReadSingle(); // 36-37-38-39
                    TiltAngle = dp.ReadSingle(); // 40-41-42-43
                    BeamWidth = dp.ReadSingle(); // 44-45-46-47
                    OffsetX = dp.ReadSingle(); // 48-49-50-51
                    OffsetY = dp.ReadSingle(); // 52-53-54-55
                    OffsetZ = dp.ReadSingle(); // 56-57-58-59
                    OffsetYaw = dp.ReadSingle(); // 60-61-62-63
                    OffsetPitch = dp.ReadSingle(); // 64-65-66-67
                    OffsetRoll = dp.ReadSingle(); // 68-69-70-71
                    BeamsPerArray = dp.ReadUInt16(); // 72-73
                    Latency = dp.ReadSingle(); // 74-75-76-77

                }
            }
        }

        #endregion

    }
}
