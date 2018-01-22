using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    public class QpsMultiTxEntry
    {
        #region private properties

        #endregion

        #region public properties

        public Int32 Id { get; set; }
        public Single Intensity { get; set; }
        public Int32 Quality { get; set; }
        public Single TwoWayTravelTime { get; set; }
        public Single DeltaTime { get; set; }
        public Single OffsetX { get; set; }
        public Single OffsetY { get; set; }
        public Single OffsetZ { get; set; }

        #endregion

        #region constructors

        public QpsMultiTxEntry()
        {
            Id = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;
            DeltaTime = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetZ = 0;

        }

        public QpsMultiTxEntry(Byte[] byteArray)
        {
            Id = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;
            DeltaTime = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetZ = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 32)
                {
                    Id = dp.ReadInt32(); // 0-1-2-3
                    Intensity = dp.ReadSingle(); // 4-5-6-7
                    Quality = dp.ReadInt32(); // 8-9-10-11
                    TwoWayTravelTime = dp.ReadSingle(); // 12-13-14-15
                    DeltaTime = dp.ReadSingle(); // 16-17-18-19
                    OffsetX = dp.ReadSingle(); // 20-21-22-23
                    OffsetY = dp.ReadSingle(); // 24-25-26-27
                    OffsetZ = dp.ReadSingle(); // 28-29-30-31

                }
            }
        }

        #endregion

    }
}
