using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    public class PingChannelHeader
    {
        #region private properties

        #endregion

        #region public properties

        public UInt16 ChannelNumber { get; set; }
        public UInt16 DownSampleMethod { get; set; }
        public Single SlantRange { get; set; }
        public Single GroundRange { get; set; }
        public Single TimeDelay { get; set; }
        public Single TimeDuration { get; set; }
        public Single SecondsPerPing { get; set; }
        public UInt16 ProcessingFlags { get; set; }
        public UInt16 Frequency { get; set; }
        public UInt16 InitialGainCode { get; set; }
        public UInt16 GainCode { get; set; }
        public UInt16 Bandwidth { get; set; }
        public UInt32 ContactNumber { get; set; }
        public UInt16 ContactClassification { get; set; }
        public Byte ContactSubNumber { get; set; }
        public Byte ContactType { get; set; }
        public UInt32 NumberSamples { get; set; }
        public UInt16 MillivoltScale { get; set; }
        public Single ContactTimeOffTrack { get; set; }
        public Byte ContactCloseNumber { get; set; }
        public Single FixedVSOP { get; set; }
        public Int16 Weight { get; set; }

        #endregion

        #region constructors

        public PingChannelHeader()
        {
            ChannelNumber = 0;
            DownSampleMethod = 0;
            SlantRange = 0;
            GroundRange = 0;
            TimeDelay = 0;
            TimeDuration = 0;
            SecondsPerPing = 0;
            ProcessingFlags = 0;
            Frequency = 0;
            InitialGainCode = 0;
            GainCode = 0;
            Bandwidth = 0;
            ContactNumber = 0;
            ContactClassification = 0;
            ContactSubNumber = 0;
            ContactType = 0;
            NumberSamples = 0;
            MillivoltScale = 0;
            ContactTimeOffTrack = 0;
            ContactCloseNumber = 0;
            FixedVSOP = 0;
            Weight = 0;

        }

        public PingChannelHeader(Byte[] byteArray)
        {
            ChannelNumber = 0;
            DownSampleMethod = 0;
            SlantRange = 0;
            GroundRange = 0;
            TimeDelay = 0;
            TimeDuration = 0;
            SecondsPerPing = 0;
            ProcessingFlags = 0;
            Frequency = 0;
            InitialGainCode = 0;
            GainCode = 0;
            Bandwidth = 0;
            ContactNumber = 0;
            ContactClassification = 0;
            ContactSubNumber = 0;
            ContactType = 0;
            NumberSamples = 0;
            MillivoltScale = 0;
            ContactTimeOffTrack = 0;
            ContactCloseNumber = 0;
            FixedVSOP = 0;
            Weight = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 60)
                {
                    ChannelNumber = dp.ReadUInt16(); // 0-1
                    DownSampleMethod = dp.ReadUInt16(); // 2-3
                    SlantRange = dp.ReadSingle(); // 4-5-6-7
                    GroundRange = dp.ReadSingle(); // 8-9-10-11
                    TimeDelay = dp.ReadSingle(); // 12-13-14-15
                    TimeDuration = dp.ReadSingle(); // 16-17-18-19
                    SecondsPerPing = dp.ReadSingle(); // 20-21-22-23
                    ProcessingFlags = dp.ReadUInt16(); // 24-25
                    Frequency = dp.ReadUInt16(); // 26-27
                    InitialGainCode = dp.ReadUInt16(); // 28-29
                    GainCode = dp.ReadUInt16(); // 30-31
                    Bandwidth = dp.ReadUInt16(); // 32-33
                    ContactNumber = dp.ReadUInt32(); // 34-35-36-37
                    ContactClassification = dp.ReadUInt16(); // 38-39
                    ContactSubNumber = dp.ReadByte(); // 40
                    ContactType = dp.ReadByte(); // 41
                    NumberSamples = dp.ReadUInt32(); // 42-43-44-45
                    MillivoltScale = dp.ReadUInt16(); // 46-47
                    ContactTimeOffTrack = dp.ReadSingle(); // 48-49-50-51
                    ContactCloseNumber = dp.ReadByte(); // 52
                    dp.ReadByte(); // 53 Unused
                    FixedVSOP = dp.ReadSingle(); // 54-55-56-57
                    Weight = dp.ReadInt16(); // 58-59

                }
            }
        }

        #endregion

    }
}
