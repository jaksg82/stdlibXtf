using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    class BathySnippet0
    {
        #region private properties

        private UInt32 MagicId = 0x534E5030;

        #endregion

        #region public properties

        public UInt16 HeaderSize { get; set; }
        public UInt16 DataSize { get; set; }
        public UInt32 PingNumber { get; set; }
        public UInt32 Seconds { get; set; }
        public UInt32 Milliseconds { get; set; }
        public UInt16 Latency { get; set; }
        public UInt16 SonarId1 { get; set; }
        public UInt16 SonarId2 { get; set; }
        public UInt16 SonarModel { get; set; }
        public UInt16 Frequency { get; set; }
        public UInt16 SoundSpeed { get; set; }
        public UInt16 SampleRate { get; set; }
        public UInt16 PingRate { get; set; }
        public UInt16 Range { get; set; }
        public UInt16 Power { get; set; }
        public UInt16 Gain { get; set; }
        public UInt16 PulseWidth { get; set; }
        public UInt16 Spread { get; set; }
        public UInt16 Absorb { get; set; }
        public UInt16 ProjectorType { get; set; }
        public UInt16 ProjectorWidth { get; set; }
        public UInt16 SpacingNumerator { get; set; }
        public UInt16 SpacingDenominator { get; set; }
        public Int16 ProjectorAngle { get; set; }
        public UInt16 MinRange { get; set; }
        public UInt16 MaxRange { get; set; }
        public UInt16 MinDepth { get; set; }
        public UInt16 MaxDepth { get; set; }
        public UInt16 Filters { get; set; }
        public UInt16 Flags { get; set; }
        public Int16 HeadTemp { get; set; }
        public UInt16 BeamCount { get; set; }

        #endregion

        #region constructors

        public BathySnippet0()
        {
            HeaderSize = 0;
            DataSize = 0;
            PingNumber = 0;
            Seconds = 0;
            Milliseconds = 0;
            Latency = 0;
            SonarId1 = 0;
            SonarId2 = 0;
            SonarModel = 0;
            Frequency = 0;
            SoundSpeed = 0;
            SampleRate = 0;
            PingRate = 0;
            Range = 0;
            Power = 0;
            Gain = 0;
            PulseWidth = 0;
            Spread = 0;
            Absorb = 0;
            ProjectorType = 0;
            ProjectorWidth = 0;
            SpacingNumerator = 0;
            SpacingDenominator = 0;
            ProjectorAngle = 0;
            MinRange = 0;
            MaxRange = 0;
            MinDepth = 0;
            MaxDepth = 0;
            Filters = 0;
            Flags = 0;
            HeadTemp = 0;
            BeamCount = 0;

        }

        public BathySnippet0(Byte[] byteArray)
        {
            HeaderSize = 0;
            DataSize = 0;
            PingNumber = 0;
            Seconds = 0;
            Milliseconds = 0;
            Latency = 0;
            SonarId1 = 0;
            SonarId2 = 0;
            SonarModel = 0;
            Frequency = 0;
            SoundSpeed = 0;
            SampleRate = 0;
            PingRate = 0;
            Range = 0;
            Power = 0;
            Gain = 0;
            PulseWidth = 0;
            Spread = 0;
            Absorb = 0;
            ProjectorType = 0;
            ProjectorWidth = 0;
            SpacingNumerator = 0;
            SpacingDenominator = 0;
            ProjectorAngle = 0;
            MinRange = 0;
            MaxRange = 0;
            MinDepth = 0;
            MaxDepth = 0;
            Filters = 0;
            Flags = 0;
            HeadTemp = 0;
            BeamCount = 0;

            UInt32 chkNumber;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 74)
                {
                    chkNumber = dp.ReadUInt32(); // 0-1-2-3
                    if (chkNumber == MagicId)
                    {
                        HeaderSize = dp.ReadUInt16(); // 4-5
                        DataSize = dp.ReadUInt16(); // 6-7
                        PingNumber = dp.ReadUInt32(); // 8-9-10-11
                        Seconds = dp.ReadUInt32(); // 12-13-14-15
                        Milliseconds = dp.ReadUInt32(); // 16-17-18-19
                        Latency = dp.ReadUInt16(); // 20-21
                        SonarId1 = dp.ReadUInt16(); // 22-23
                        SonarId2 = dp.ReadUInt16(); // 24-25
                        SonarModel = dp.ReadUInt16(); // 26-27
                        Frequency = dp.ReadUInt16(); // 28-29
                        SoundSpeed = dp.ReadUInt16(); // 30-31
                        SampleRate = dp.ReadUInt16(); // 32-33
                        PingRate = dp.ReadUInt16(); // 34-35
                        Range = dp.ReadUInt16(); // 36-37
                        Power = dp.ReadUInt16(); // 38-39
                        Gain = dp.ReadUInt16(); // 40-41
                        PulseWidth = dp.ReadUInt16(); // 42-43
                        Spread = dp.ReadUInt16(); // 44-45
                        Absorb = dp.ReadUInt16(); // 46-47
                        ProjectorType = dp.ReadUInt16(); // 48-49
                        ProjectorWidth = dp.ReadUInt16(); // 50-51
                        SpacingNumerator = dp.ReadUInt16(); // 52-53
                        SpacingDenominator = dp.ReadUInt16(); // 54-55
                        ProjectorAngle = dp.ReadInt16(); // 56-57
                        MinRange = dp.ReadUInt16(); // 58-59
                        MaxRange = dp.ReadUInt16(); // 60-61
                        MinDepth = dp.ReadUInt16(); // 62-63
                        MaxDepth = dp.ReadUInt16(); // 64-65
                        Filters = dp.ReadUInt16(); // 66-67
                        Flags = dp.ReadUInt16(); // 68-69
                        HeadTemp = dp.ReadInt16(); // 70-71
                        BeamCount = dp.ReadUInt16(); // 72-73

                    }
                }
            }
        }

        #endregion

    }
}
