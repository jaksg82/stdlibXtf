using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    public class BeamXYZA
    {
        #region private properties

        #endregion

        #region public properties

        public Double PositionOffsetX { get; set; }
        public Double PositionOffsetY { get; set; }
        public Single Depth { get; set; }
        public Double Time { get; set; }
        public Int16 Amplitude { get; set; }
        public Byte Quality { get; set; }

        #endregion

        #region constructors

        public BeamXYZA()
        {
            PositionOffsetX = 0;
            PositionOffsetY = 0;
            Depth = 0;
            Time = 0;
            Amplitude = 0;
            Quality = 0;

        }

        public BeamXYZA(Byte[] byteArray)
        {
            PositionOffsetX = 0;
            PositionOffsetY = 0;
            Depth = 0;
            Time = 0;
            Amplitude = 0;
            Quality = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 31)
                {
                    PositionOffsetY = dp.ReadDouble(); // 0-1-2-3-4-5-6-7
                    PositionOffsetX = dp.ReadDouble(); // 8-9-10-11-12-13-14-15
                    Depth = dp.ReadSingle(); // 16-17-18-19
                    Time = dp.ReadDouble(); // 20-21-22-23-24-25-26-27
                    Amplitude = dp.ReadInt16(); // 28-29
                    Quality = dp.ReadByte(); // 30

                }
            }
        }

        #endregion

    }
}
