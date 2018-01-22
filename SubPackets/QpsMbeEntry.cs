using System;
using System.Globalization;
using System.IO;

namespace stdlibXtf.SubPackets
{
    public class QpsMbeEntry
    {
        #region private properties

        #endregion

        #region public properties

        public Int32 Id { get; set; }
        public Double Intensity { get; set; }
        public Int32 Quality { get; set; }
        public Double TwoWayTravelTime { get; set; }
        public Double DeltaTime { get; set; }
        public Double BeamAngle { get; set; }
        public Double TiltAngle { get; set; }

        #endregion

        #region constructors

        public QpsMbeEntry()
        {
            Id = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;
            DeltaTime = 0;
            BeamAngle = 0;
            TiltAngle = 0;

        }

        public QpsMbeEntry(Byte[] byteArray)
        {
            Id = 0;
            Intensity = 0;
            Quality = 0;
            TwoWayTravelTime = 0;
            DeltaTime = 0;
            BeamAngle = 0;
            TiltAngle = 0;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 48)
                {
                    Id = dp.ReadInt32(); // 0-1-2-3
                    Intensity = dp.ReadDouble(); // 4-5-6-7-8-9-10-11
                    Quality = dp.ReadInt32(); // 12-13-14-15
                    TwoWayTravelTime = dp.ReadDouble(); // 16-17-18-19-20-21-22-23
                    DeltaTime = dp.ReadDouble(); // 24-25-26-27-28-29-30-31
                    BeamAngle = dp.ReadDouble(); // 32-33-34-35-36-37-38-39
                    TiltAngle = dp.ReadDouble(); // 40-41-42-43-44-45-46-47

                }
            }
        }

        #endregion

    }
}
