using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    public class PingHeader : IPacket 
    {
        #region private properties

        private Byte _HeaderType = 0; // XTF_HEADER_SONAR
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;

        #endregion

        #region public properties

        // IPacket implementation
        public Byte HeaderType { get { return _HeaderType; } }
        public ushort MagicNumber { get { return _MagicNumber; } }
        public byte SubChannelNumber { get { return _SubChannelNumber; } }
        public ushort NumberChannelsToFollow { get { return _NumberChannelsToFollow; } }
        public uint NumberBytesThisRecord { get { return _NumberBytesThisRecord; } }
        public DateTime PacketTime { get { return _PacketTime; } }

        // Other properties
        public UInt32 EventNumber { get; set; }
        public UInt32 PingNumber { get; set; }
        public Single SoundVelocity { get; set; }
        public Single OceanTide { get; set; }
        public Single ConductivityFrequency { get; set; }
        public Single TemperatureFrequency { get; set; }
        public Single PressureFrequency { get; set; }
        public Single PressureTemp { get; set; }
        public Single Conductivity { get; set; }
        public Single WaterTemperature { get; set; }
        public Single Pressure { get; set; }
        public Single ComputedSoundVelocity { get; set; }
        public Single MagX { get; set; }
        public Single MagY { get; set; }
        public Single MagZ { get; set; }
        public Single AuxValue1 { get; set; }
        public Single AuxValue2 { get; set; }
        public Single AuxValue3 { get; set; }
        public Single AuxValue4 { get; set; }
        public Single AuxValue5 { get; set; }
        public Single AuxValue6 { get; set; }
        public Single SpeedLog { get; set; }
        public Single Turbidity { get; set; }
        public Single ShipSpeed { get; set; }
        public Single ShipGyro { get; set; }
        public Double ShipCoordinateY { get; set; }
        public Double ShipCoordinateX { get; set; }
        public UInt16 ShipAltitude { get; set; }
        public UInt16 ShipDepth { get; set; }
        public DateTime FixTime { get; set; }
        public Single SensorSpeed { get; set; }
        public Single KP { get; set; }
        public Double SensorCoordinateY { get; set; }
        public Double SensorCoordinateX { get; set; }
        public UInt16 SonarStatus { get; set; }
        public UInt16 RangeToFish { get; set; }
        public UInt16 BearingToFish { get; set; }
        public UInt16 CableOut { get; set; }
        public Single Layback { get; set; }
        public Single CableTension { get; set; }
        public Single SensorDepth { get; set; }
        public Single SensorPrimaryAltitude { get; set; }
        public Single SensorAuxAltitude { get; set; }
        public Single SensorPitch { get; set; }
        public Single SensorRoll { get; set; }
        public Single SensorHeading { get; set; }
        public Single Heave { get; set; }
        public Single Yaw { get; set; }
        public UInt32 AttitudeTimeTag { get; set; }
        public Single DistanceOffTrack { get; set; }
        public UInt32 NavigationFixMilliseconds { get; set; }
        public DateTime ComputerClockTime { get; set; }
        public Int16 FishPositionDeltaX { get; set; }
        public Int16 FishPositionDeltaY { get; set; }
        public Byte FishPositionErrorCode { get; set; }

        #endregion

        #region constructors

        public PingHeader()
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            EventNumber = 0;
            PingNumber = 0;
            SoundVelocity = 0;
            OceanTide = 0;
            ConductivityFrequency = 0;
            TemperatureFrequency = 0;
            PressureFrequency = 0;
            PressureTemp = 0;
            Conductivity = 0;
            WaterTemperature = 0;
            Pressure = 0;
            ComputedSoundVelocity = 0;
            MagX = 0;
            MagY = 0;
            MagZ = 0;
            AuxValue1 = 0;
            AuxValue2 = 0;
            AuxValue3 = 0;
            AuxValue4 = 0;
            AuxValue5 = 0;
            AuxValue6 = 0;
            SpeedLog = 0;
            Turbidity = 0;
            ShipSpeed = 0;
            ShipGyro = 0;
            ShipCoordinateY = 0.0;
            ShipCoordinateX = 0.0;
            ShipAltitude = 0;
            ShipDepth = 0;
            FixTime = DateTime.MinValue;
            SensorSpeed = 0;
            KP = 0;
            SensorCoordinateY = 0.0;
            SensorCoordinateX = 0.0;
            SonarStatus = 0;
            RangeToFish = 0;
            BearingToFish = 0;
            CableOut = 0;
            Layback = 0;
            CableTension = 0;
            SensorDepth = 0;
            SensorPrimaryAltitude = 0;
            SensorAuxAltitude = 0;
            SensorPitch = 0;
            SensorRoll = 0;
            SensorHeading = 0;
            Heave = 0;
            Yaw = 0;
            AttitudeTimeTag = 0;
            DistanceOffTrack = 0;
            NavigationFixMilliseconds = 0;
            ComputerClockTime = DateTime.MinValue;
            FishPositionDeltaX = 0;
            FishPositionDeltaY = 0;
            FishPositionErrorCode = 0;

        }

        public PingHeader(Byte[] byteArray)
        {
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            EventNumber = 0;
            PingNumber = 0;
            SoundVelocity = 0;
            OceanTide = 0;
            ConductivityFrequency = 0;
            TemperatureFrequency = 0;
            PressureFrequency = 0;
            PressureTemp = 0;
            Conductivity = 0;
            WaterTemperature = 0;
            Pressure = 0;
            ComputedSoundVelocity = 0;
            MagX = 0;
            MagY = 0;
            MagZ = 0;
            AuxValue1 = 0;
            AuxValue2 = 0;
            AuxValue3 = 0;
            AuxValue4 = 0;
            AuxValue5 = 0;
            AuxValue6 = 0;
            SpeedLog = 0;
            Turbidity = 0;
            ShipSpeed = 0;
            ShipGyro = 0;
            ShipCoordinateY = 0.0;
            ShipCoordinateX = 0.0;
            ShipAltitude = 0;
            ShipDepth = 0;
            FixTime = DateTime.MinValue;
            SensorSpeed = 0;
            KP = 0;
            SensorCoordinateY = 0.0;
            SensorCoordinateX = 0.0;
            SonarStatus = 0;
            RangeToFish = 0;
            BearingToFish = 0;
            CableOut = 0;
            Layback = 0;
            CableTension = 0;
            SensorDepth = 0;
            SensorPrimaryAltitude = 0;
            SensorAuxAltitude = 0;
            SensorPitch = 0;
            SensorRoll = 0;
            SensorHeading = 0;
            Heave = 0;
            Yaw = 0;
            AttitudeTimeTag = 0;
            DistanceOffTrack = 0;
            NavigationFixMilliseconds = 0;
            ComputerClockTime = DateTime.MinValue;
            FishPositionDeltaX = 0;
            FishPositionDeltaY = 0;
            FishPositionErrorCode = 0;


            UInt16 Year;
            UInt16 HSecons;
            UInt16 chkNumber;
            Byte Month;
            Byte Day;
            Byte Hour;
            Byte Minutes;
            Byte Seconds;
            String TimeString;
            DateTime outTime;

            using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
            {
                if (byteArray.Length >= 245)
                {
                    chkNumber = dp.ReadUInt16(); // 0-1
                    if (chkNumber == XtfMainHeader.MagicNumber)
                    {
                        dp.ReadByte(); //HeaderType 2
                        _SubChannelNumber = dp.ReadByte(); // 3
                        _NumberChannelsToFollow = dp.ReadUInt16(); // 4-5
                        dp.ReadUInt16(); //Unused 6-7
                        dp.ReadUInt16(); //Unused 8-9
                        _NumberBytesThisRecord = dp.ReadUInt32(); // 10-11-12-13
                        //Read the ping time values
                        Year = dp.ReadUInt16(); // 14-15
                        Month = dp.ReadByte(); // 16
                        Day = dp.ReadByte(); // 17
                        Hour = dp.ReadByte(); // 18
                        Minutes = dp.ReadByte(); // 19
                        Seconds = dp.ReadByte(); // 20
                        HSecons = dp.ReadByte(); // 21
                        dp.ReadUInt16(); //JulianDay 22-23
                        //Compose the ping time value
                        TimeString = Year.ToString("0000", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Month.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Day.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + HSecons.ToString("00", CultureInfo.InvariantCulture);
                        if (DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { _PacketTime = outTime; }
                        else
                        { _PacketTime = DateTime.MinValue; }

                        EventNumber = dp.ReadUInt32(); // 24-25-26-27
                        PingNumber = dp.ReadUInt32(); // 28-29-30-31
                        SoundVelocity = dp.ReadSingle(); // 32-33-34-35
                        OceanTide = dp.ReadSingle(); // 36-37-38-39
                        dp.ReadUInt32(); //Unused 40-41-42-43
                        ConductivityFrequency = dp.ReadSingle(); // 44-45-46-47
                        TemperatureFrequency = dp.ReadSingle(); // 48-49-50-51
                        PressureFrequency = dp.ReadSingle(); // 52-53-54-55
                        PressureTemp = dp.ReadSingle(); // 56-57-58-59
                        Conductivity = dp.ReadSingle(); // 60-61-62-63
                        WaterTemperature = dp.ReadSingle(); // 64-65-66-67
                        Pressure = dp.ReadSingle(); // 68-69-70-71
                        ComputedSoundVelocity = dp.ReadSingle(); // 72-73-74-75
                        MagX = dp.ReadSingle(); // 76-77-78-79
                        MagY = dp.ReadSingle(); // 80-81-82-83
                        MagZ = dp.ReadSingle(); // 84-85-86-87
                        AuxValue1 = dp.ReadSingle(); // 88-89-90-91
                        AuxValue2 = dp.ReadSingle(); // 92-93-94-95
                        AuxValue3 = dp.ReadSingle(); // 96-97-98-99
                        AuxValue4 = dp.ReadSingle(); // 100-101-102-103
                        AuxValue5 = dp.ReadSingle(); // 104-105-106-107
                        AuxValue6 = dp.ReadSingle(); // 108-109-110-111
                        SpeedLog = dp.ReadSingle(); // 112-113-114-115
                        Turbidity = dp.ReadSingle(); // 116-117-118-119
                        ShipSpeed = dp.ReadSingle(); // 120-121-122-123
                        ShipGyro = dp.ReadSingle(); // 124-125-126-127
                        ShipCoordinateY = dp.ReadDouble(); // 128-129-130-131-132-133-134-135
                        ShipCoordinateX = dp.ReadDouble(); // 136-137-138-139-140-141-142-143
                        ShipAltitude = dp.ReadUInt16(); // 144-145
                        ShipDepth = dp.ReadUInt16(); // 146-147
                        //Read the ping time values
                        Hour = dp.ReadByte(); // 148
                        Minutes = dp.ReadByte(); // 149
                        Seconds = dp.ReadByte(); // 150
                        HSecons = dp.ReadByte(); // 151
                        //Compose the ping time value
                        TimeString = _PacketTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + HSecons.ToString("00", CultureInfo.InvariantCulture);
                        if (!DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { FixTime = outTime; }
                        else
                        { FixTime = DateTime.MinValue; }

                        SensorSpeed = dp.ReadSingle(); // 152-153-154-155
                        KP = dp.ReadSingle(); // 156-157-158-159
                        SensorCoordinateY = dp.ReadDouble(); // 160-161-162-163-164-165-166-167
                        SensorCoordinateX = dp.ReadDouble(); // 168-169-170-171-172-173-174-175
                        SonarStatus = dp.ReadUInt16(); // 176-177
                        RangeToFish = dp.ReadUInt16(); // 178-179
                        BearingToFish = dp.ReadUInt16(); // 180-181
                        CableOut = dp.ReadUInt16(); // 182-183
                        Layback = dp.ReadSingle(); // 184-185-186-187
                        CableTension = dp.ReadSingle(); // 188-189-190-191
                        SensorDepth = dp.ReadSingle(); // 192-193-194-195
                        SensorPrimaryAltitude = dp.ReadSingle(); // 196-197-198-199
                        SensorAuxAltitude = dp.ReadSingle(); // 200-201-202-203
                        SensorPitch = dp.ReadSingle(); // 204-205-206-207
                        SensorRoll = dp.ReadSingle(); // 208-209-210-211
                        SensorHeading = dp.ReadSingle(); // 212-213-214-215
                        Heave = dp.ReadSingle(); // 216-217-218-219
                        Yaw = dp.ReadSingle(); // 220-221-222-223
                        AttitudeTimeTag = dp.ReadUInt32(); // 224-225-226-227
                        DistanceOffTrack = dp.ReadSingle(); // 228-229-230-231
                        NavigationFixMilliseconds = dp.ReadUInt32(); // 232-233-234-235
                        //Read the computer time values
                        Hour = dp.ReadByte(); // 236
                        Minutes = dp.ReadByte(); // 237
                        Seconds = dp.ReadByte(); // 238
                        HSecons = dp.ReadByte(); // 239
                        //Compose the conputer time value
                        TimeString = _PacketTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Hour.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Minutes.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + Seconds.ToString("00", CultureInfo.InvariantCulture);
                        TimeString = TimeString + "-" + HSecons.ToString("00", CultureInfo.InvariantCulture);
                        if (!DateTime.TryParseExact(TimeString, "yyyy-MM-dd-HH-mm-ss-ff", CultureInfo.InvariantCulture, DateTimeStyles.None, out outTime))
                        { ComputerClockTime = outTime; }
                        else
                        { ComputerClockTime = DateTime.MinValue; }

                        FishPositionDeltaX = dp.ReadInt16(); // 240-241
                        FishPositionDeltaY = dp.ReadInt16(); // 242-243
                        FishPositionErrorCode = dp.ReadByte(); // 244

                    }
                }
            }
        }

        #endregion

    }
}
