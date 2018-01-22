using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    public class BathySnippet1
    {
        #region private properties

        private UInt32 MagicId = 0x534E5031;

        #endregion

        #region public properties

        public UInt16 HeaderSize { get; set; }
        public UInt16 DataSize { get; set; }
        public UInt32 PingNumber { get; set; }
        public UInt16 Beam { get; set; }
        public UInt16 SnipSamples { get; set; }
        public UInt16 GainStart { get; set; }
        public UInt16 GainEnd { get; set; }
        public UInt16 FragOffset { get; set; }
        public UInt16 FragSamples { get; set; }

        #endregion

        #region constructors

        public BathySnippet1()
        {
            HeaderSize = 0;
            DataSize = 0;
            PingNumber = 0;
            Beam = 0;
            SnipSamples = 0;
            GainStart = 0;
            GainEnd = 0;
            FragOffset = 0;
            FragSamples = 0;

        }

        public BathySnippet1(Byte[] byteArray)
        {
            HeaderSize = 0;
            DataSize = 0;
            PingNumber = 0;
            Beam = 0;
            SnipSamples = 0;
            GainStart = 0;
            GainEnd = 0;
            FragOffset = 0;
            FragSamples = 0;

            UInt32 chkNumber;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 24)
                {
                    chkNumber = dp.ReadUInt32(); // 0-1-2-3
                    if (chkNumber == MagicId)
                    {
                        HeaderSize = dp.ReadUInt16(); // 4-5
                        DataSize = dp.ReadUInt16(); // 6-7
                        PingNumber = dp.ReadUInt32(); // 8-9-10-11
                        Beam = dp.ReadUInt16(); // 12-13
                        SnipSamples = dp.ReadUInt16(); // 14-15
                        GainStart = dp.ReadUInt16(); // 16-17
                        GainEnd = dp.ReadUInt16(); // 18-19
                        FragOffset = dp.ReadUInt16(); // 20-21
                        FragSamples = dp.ReadUInt16(); // 22-23
                    }
                }
            }
        }

        #endregion

    }
}
