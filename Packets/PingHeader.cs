using System;
using System.Globalization;
using System.IO;
using stdlibXtf.Common;

namespace stdlibXtf.Packets
{
    /// <summary>
    /// Define a sonar ping header packet.
    /// </summary>
    public class PingHeader : IPacket
    {
        #region IPacket implementation

        private Byte _HeaderType;
        private ushort _MagicNumber;
        private byte _SubChannelNumber;
        private ushort _NumberChannelsToFollow;
        private uint _NumberBytesThisRecord;
        private DateTime _PacketTime;

        /// <summary>
        /// Gets the type of the packet header.
        /// </summary>
        public Byte HeaderType { get { return _HeaderType; } }

        /// <summary>
        /// Gets the number that identify the correct start of the packet.
        /// </summary>
        public ushort MagicNumber { get { return _MagicNumber; } }

        /// <summary>
        /// Gets the index number of which channels this packet are referred.
        /// </summary>
        public byte SubChannelNumber { get { return _SubChannelNumber; } }

        /// <summary>
        /// Gets the number of channels that follow this packet.
        /// </summary>
        public ushort NumberChannelsToFollow { get { return _NumberChannelsToFollow; } }

        /// <summary>
        /// Total byte count for this packet, including the header and the data if available.
        /// </summary>
        public uint NumberBytesThisRecord { get { return _NumberBytesThisRecord; } }

        /// <summary>
        /// Gets the packet recording time.
        /// </summary>
        public DateTime PacketTime { get { return _PacketTime; } }

        #endregion IPacket implementation

        #region private properties

        private UInt32 _EventNumber;
        private UInt32 _PingNumber;
        private Single _SoundVelocity;
        private Single _OceanTide;
        private Single _ConductivityFrequency;
        private Single _TemperatureFrequency;
        private Single _PressureFrequency;
        private Single _PressureTemp;
        private Single _Conductivity;
        private Single _WaterTemperature;
        private Single _Pressure;
        private Single _ComputedSoundVelocity;
        private Single _MagX;
        private Single _MagY;
        private Single _MagZ;
        private Single _AuxValue1;
        private Single _AuxValue2;
        private Single _AuxValue3;
        private Single _AuxValue4;
        private Single _AuxValue5;
        private Single _AuxValue6;
        private Single _SpeedLog;
        private Single _Turbidity;
        private Single _ShipSpeed;
        private Single _ShipGyro;
        private Double _ShipCoordinateY;
        private Double _ShipCoordinateX;
        private UInt16 _ShipAltitude;
        private UInt16 _ShipDepth;
        private DateTime _FixTime;
        private Single _SensorSpeed;
        private Single _KP;
        private Double _SensorCoordinateY;
        private Double _SensorCoordinateX;
        private UInt16 _SonarStatus;
        private UInt16 _RangeToFish;
        private UInt16 _BearingToFish;
        private UInt16 _CableOut;
        private Single _Layback;
        private Single _CableTension;
        private Single _SensorDepth;
        private Single _SensorPrimaryAltitude;
        private Single _SensorAuxAltitude;
        private Single _SensorPitch;
        private Single _SensorRoll;
        private Single _SensorHeading;
        private Single _Heave;
        private Single _Yaw;
        private UInt32 _AttitudeTimeTag;
        private Single _DistanceOffTrack;
        private UInt32 _NavigationFixMilliseconds;
        private DateTime _ComputerClockTime;
        private Int16 _FishPositionDeltaX;
        private Int16 _FishPositionDeltaY;
        private Byte _FishPositionErrorCode;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the last logged event number.
        /// </summary>
        public UInt32 EventNumber { get { return _EventNumber; } }

        /// <summary>
        /// Counts consecutively (usually from 0) and increments for each update.
        /// Isis Note: The counters are different between sonar and bathymetry updates.
        /// </summary>
        public UInt32 PingNumber { get { return _PingNumber; } }

        /// <summary>
        /// Get the speed of sound value in m/s, Isis uses 750 (one way), some XTF files use 1500.
        /// </summary>
        public Single SoundVelocity { get { return _SoundVelocity; } }

        /// <summary>
        /// Gets the altitude above Geoide (from RTK), if present, or the ocean tide in meters.
        /// </summary>
        public Single OceanTide { get { return _OceanTide; } }

        /// <summary>
        /// Gets the conductivity frequency in Hz.
        /// </summary>
        public Single ConductivityFrequency { get { return _ConductivityFrequency; } }

        /// <summary>
        /// Gets the temperature frequency in Hz.
        /// </summary>
        public Single TemperatureFrequency { get { return _TemperatureFrequency; } }

        /// <summary>
        /// Gets the pressure frequency in Hz.
        /// </summary>
        public Single PressureFrequency { get { return _PressureFrequency; } }

        /// <summary>
        /// Gets the pressure temperature in degrees Celsius.
        /// </summary>
        public Single PressureTemp { get { return _PressureTemp; } }

        /// <summary>
        /// Gets the conductivity in Siemens/m.
        /// </summary>
        public Single Conductivity { get { return _Conductivity; } }

        /// <summary>
        /// Gets the water temperature in degrees Celsius.
        /// </summary>
        public Single WaterTemperature { get { return _WaterTemperature; } }

        /// <summary>
        /// Gets the water pressure in psia.
        /// </summary>
        public Single Pressure { get { return _Pressure; } }

        /// <summary>
        /// Gets the speed of sound value in m/s computed from Conductivity, WaterTemperature,
        /// and Pressure using the Chen Millero formula (1977), formula (JASA, 62, 1129-1135).
        /// </summary>
        public Single ComputedSoundVelocity { get { return _ComputedSoundVelocity; } }

        /// <summary>
        /// Gets the X-axis magnetometer data in mgauss.
        /// </summary>
        public Single MagX { get { return _MagX; } }

        /// <summary>
        /// Gets the Y-axis magnetometer data in mgauss.
        /// </summary>
        public Single MagY { get { return _MagY; } }

        /// <summary>
        /// Gets the Z-axis magnetometer data in mgauss.
        /// </summary>
        public Single MagZ { get { return _MagZ; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue1 { get { return _AuxValue1; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue2 { get { return _AuxValue2; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue3 { get { return _AuxValue3; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue4 { get { return _AuxValue4; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue5 { get { return _AuxValue5; } }

        /// <summary>
        /// Gets the auxiliary values. This can be used to store and display any value at the user's discretion.
        /// </summary>
        public Single AuxValue6 { get { return _AuxValue6; } }

        /// <summary>
        /// Gets the speed log sensor value on towfish in knots.
        /// </summary>
        public Single SpeedLog { get { return _SpeedLog; } }

        /// <summary>
        /// Gets the turbidity sensor value. 0 to +5 volts, multiplied by 10000.
        /// </summary>
        public Single Turbidity { get { return _Turbidity; } }

        /// <summary>
        /// Gets the ship speed in knots.
        /// </summary>
        public Single ShipSpeed { get { return _ShipSpeed; } }

        /// <summary>
        /// Gets the ship gyro in degrees.
        /// </summary>
        public Single ShipGyro { get { return _ShipGyro; } }

        /// <summary>
        /// Gets the ship latitude (in degrees) or northing (in meters).
        /// </summary>
        public Double ShipCoordinateY { get { return _ShipCoordinateY; } }

        /// <summary>
        /// Gets the ship longitude (in degrees) or easting (in meters).
        /// </summary>
        public Double ShipCoordinateX { get { return _ShipCoordinateX; } }

        /// <summary>
        /// Gets the ship altitude in decimeters.
        /// </summary>
        public UInt16 ShipAltitude { get { return _ShipAltitude; } }

        /// <summary>
        /// Gets the ship depth in decimeters.
        /// </summary>
        public UInt16 ShipDepth { get { return _ShipDepth; } }

        /// <summary>
        /// Gets the time of most recent nav update.
        /// </summary>
        public DateTime FixTime { get { return _FixTime; } }

        /// <summary>
        /// Gets the speed of towfish in knots.
        /// </summary>
        public Single SensorSpeed { get { return _SensorSpeed; } }

        /// <summary>
        /// Gets the Kilometers Pipe.
        /// </summary>
        public Single KP { get { return _KP; } }

        /// <summary>
        /// Gets the sensor latitude (in degrees) or northing (in meters).
        /// </summary>
        public Double SensorCoordinateY { get { return _SensorCoordinateY; } }

        /// <summary>
        /// Gets the sensor longitude (in degrees) or easting (in meters).
        /// </summary>
        public Double SensorCoordinateX { get { return _SensorCoordinateX; } }

        /// <summary>
        /// Gets the system status value, sonar dependant.
        /// </summary>
        public UInt16 SonarStatus { get { return _SonarStatus; } }

        /// <summary>
        /// Gets the slant range to towfish from ship in decimeters.
        /// </summary>
        public UInt16 RangeToFish { get { return _RangeToFish; } }

        /// <summary>
        /// Gets the bearing to towfish from ship, stored in degrees multiplied by 100.
        /// </summary>
        public UInt16 BearingToFish { get { return _BearingToFish; } }

        /// <summary>
        /// Gets the amount of cable payed out in meters.
        /// </summary>
        public UInt16 CableOut { get { return _CableOut; } }

        /// <summary>
        /// Gets the distance over ground from ship to fish.
        /// </summary>
        public Single Layback { get { return _Layback; } }

        /// <summary>
        /// Gets the cable tension from serial port.
        /// </summary>
        public Single CableTension { get { return _CableTension; } }

        /// <summary>
        /// Gets the distance (m) from sea surface to sensor. The deeper the sensor goes, the bigger (positive) this value becomes.
        /// </summary>
        public Single SensorDepth { get { return _SensorDepth; } }

        /// <summary>
        /// Gets the distance from towfish to the sea floor.
        /// </summary>
        public Single SensorPrimaryAltitude { get { return _SensorPrimaryAltitude; } }

        /// <summary>
        /// Gets the auxiliary altitude.
        /// </summary>
        public Single SensorAuxAltitude { get { return _SensorAuxAltitude; } }

        /// <summary>
        /// Gets the pitch in degrees (positive=nose up).
        /// </summary>
        public Single SensorPitch { get { return _SensorPitch; } }

        /// <summary>
        /// Gets the roll in degrees (positive=roll to starboard).
        /// </summary>
        public Single SensorRoll { get { return _SensorRoll; } }

        /// <summary>
        /// Gets the heading in degrees.
        /// </summary>
        public Single SensorHeading { get { return _SensorHeading; } }

        /// <summary>
        /// Gets the sensors heave at start of ping. Positive value means sensor moved up.
        /// </summary>
        public Single Heave { get { return _Heave; } }

        /// <summary>
        /// Gets the sensor yaw. Positive means turn to right.
        /// </summary>
        public Single Yaw { get { return _Yaw; } }

        /// <summary>
        /// Gets the time tag in milliseconds. Used to coordinate with millisecond time value in Attitude packets.
        /// Mandatory when logging XTFATTITUDE packets.
        /// </summary>
        public UInt32 AttitudeTimeTag { get { return _AttitudeTimeTag; } }

        /// <summary>
        /// Gets the distance off track.
        /// </summary>
        public Single DistanceOffTrack { get { return _DistanceOffTrack; } }

        /// <summary>
        /// Gets the millisecond clock value when nav received.
        /// </summary>
        public UInt32 NavigationFixMilliseconds { get { return _NavigationFixMilliseconds; } }

        /// <summary>
        /// Gets the computer clock time when this ping was received. May be different from ping time at start of this record
        /// if the sonar timestamped the data and the two systems aren't synched. This time should be ignored in most cases.
        /// </summary>
        public DateTime ComputerClockTime { get { return _ComputerClockTime; } }

        /// <summary>
        /// Gets the additional Tow Cable and Fish information from Trackpoint.
        /// Stored as meters multiplied by 3.0, supporting +/- 10000.0m (usually from trackpoint).
        /// </summary>
        public Int16 FishPositionDeltaX { get { return _FishPositionDeltaX; } }

        /// <summary>
        /// Gets the additional Tow Cable and Fish information from Trackpoint.
        /// Stored as meters multiplied by 3.0, supporting +/- 10000.0m (usually from trackpoint).
        /// </summary>
        public Int16 FishPositionDeltaY { get { return _FishPositionDeltaY; } }

        /// <summary>
        /// Gets the error code for FishPosition delta x,y. Typically reported by Trackpoint.
        /// </summary>
        public Byte FishPositionErrorCode { get { return _FishPositionErrorCode; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the PingHeader class that has default zero values.
        /// </summary>
        public PingHeader()
        {
            _HeaderType = 0;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _EventNumber = 0;
            _PingNumber = 0;
            _SoundVelocity = 0;
            _OceanTide = 0;
            _ConductivityFrequency = 0;
            _TemperatureFrequency = 0;
            _PressureFrequency = 0;
            _PressureTemp = 0;
            _Conductivity = 0;
            _WaterTemperature = 0;
            _Pressure = 0;
            _ComputedSoundVelocity = 0;
            _MagX = 0;
            _MagY = 0;
            _MagZ = 0;
            _AuxValue1 = 0;
            _AuxValue2 = 0;
            _AuxValue3 = 0;
            _AuxValue4 = 0;
            _AuxValue5 = 0;
            _AuxValue6 = 0;
            _SpeedLog = 0;
            _Turbidity = 0;
            _ShipSpeed = 0;
            _ShipGyro = 0;
            _ShipCoordinateY = 0.0;
            _ShipCoordinateX = 0.0;
            _ShipAltitude = 0;
            _ShipDepth = 0;
            _FixTime = DateTime.MinValue;
            _SensorSpeed = 0;
            _KP = 0;
            _SensorCoordinateY = 0.0;
            _SensorCoordinateX = 0.0;
            _SonarStatus = 0;
            _RangeToFish = 0;
            _BearingToFish = 0;
            _CableOut = 0;
            _Layback = 0;
            _CableTension = 0;
            _SensorDepth = 0;
            _SensorPrimaryAltitude = 0;
            _SensorAuxAltitude = 0;
            _SensorPitch = 0;
            _SensorRoll = 0;
            _SensorHeading = 0;
            _Heave = 0;
            _Yaw = 0;
            _AttitudeTimeTag = 0;
            _DistanceOffTrack = 0;
            _NavigationFixMilliseconds = 0;
            _ComputerClockTime = DateTime.MinValue;
            _FishPositionDeltaX = 0;
            _FishPositionDeltaY = 0;
            _FishPositionErrorCode = 0;
        }

        /// <summary>
        /// Initializes a new instance of the PingHeader class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 256 bytes.</param>
        public PingHeader(Byte[] byteArray)
        {
            _HeaderType = 0;
            _MagicNumber = 0;
            _SubChannelNumber = 0;
            _NumberChannelsToFollow = 0;
            _NumberBytesThisRecord = 256;
            _PacketTime = DateTime.MinValue;

            _EventNumber = 0;
            _PingNumber = 0;
            _SoundVelocity = 0;
            _OceanTide = 0;
            _ConductivityFrequency = 0;
            _TemperatureFrequency = 0;
            _PressureFrequency = 0;
            _PressureTemp = 0;
            _Conductivity = 0;
            _WaterTemperature = 0;
            _Pressure = 0;
            _ComputedSoundVelocity = 0;
            _MagX = 0;
            _MagY = 0;
            _MagZ = 0;
            _AuxValue1 = 0;
            _AuxValue2 = 0;
            _AuxValue3 = 0;
            _AuxValue4 = 0;
            _AuxValue5 = 0;
            _AuxValue6 = 0;
            _SpeedLog = 0;
            _Turbidity = 0;
            _ShipSpeed = 0;
            _ShipGyro = 0;
            _ShipCoordinateY = 0.0;
            _ShipCoordinateX = 0.0;
            _ShipAltitude = 0;
            _ShipDepth = 0;
            _FixTime = DateTime.MinValue;
            _SensorSpeed = 0;
            _KP = 0;
            _SensorCoordinateY = 0.0;
            _SensorCoordinateX = 0.0;
            _SonarStatus = 0;
            _RangeToFish = 0;
            _BearingToFish = 0;
            _CableOut = 0;
            _Layback = 0;
            _CableTension = 0;
            _SensorDepth = 0;
            _SensorPrimaryAltitude = 0;
            _SensorAuxAltitude = 0;
            _SensorPitch = 0;
            _SensorRoll = 0;
            _SensorHeading = 0;
            _Heave = 0;
            _Yaw = 0;
            _AttitudeTimeTag = 0;
            _DistanceOffTrack = 0;
            _NavigationFixMilliseconds = 0;
            _ComputerClockTime = DateTime.MinValue;
            _FishPositionDeltaX = 0;
            _FishPositionDeltaY = 0;
            _FishPositionErrorCode = 0;

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
                    if (chkNumber == XtfDocument.MagicNumber)
                    {
                        _HeaderType = dp.ReadByte(); // 2
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

                        _EventNumber = dp.ReadUInt32(); // 24-25-26-27
                        _PingNumber = dp.ReadUInt32(); // 28-29-30-31
                        _SoundVelocity = dp.ReadSingle(); // 32-33-34-35
                        _OceanTide = dp.ReadSingle(); // 36-37-38-39
                        dp.ReadUInt32(); //Unused 40-41-42-43
                        _ConductivityFrequency = dp.ReadSingle(); // 44-45-46-47
                        _TemperatureFrequency = dp.ReadSingle(); // 48-49-50-51
                        _PressureFrequency = dp.ReadSingle(); // 52-53-54-55
                        _PressureTemp = dp.ReadSingle(); // 56-57-58-59
                        _Conductivity = dp.ReadSingle(); // 60-61-62-63
                        _WaterTemperature = dp.ReadSingle(); // 64-65-66-67
                        _Pressure = dp.ReadSingle(); // 68-69-70-71
                        _ComputedSoundVelocity = dp.ReadSingle(); // 72-73-74-75
                        _MagX = dp.ReadSingle(); // 76-77-78-79
                        _MagY = dp.ReadSingle(); // 80-81-82-83
                        _MagZ = dp.ReadSingle(); // 84-85-86-87
                        _AuxValue1 = dp.ReadSingle(); // 88-89-90-91
                        _AuxValue2 = dp.ReadSingle(); // 92-93-94-95
                        _AuxValue3 = dp.ReadSingle(); // 96-97-98-99
                        _AuxValue4 = dp.ReadSingle(); // 100-101-102-103
                        _AuxValue5 = dp.ReadSingle(); // 104-105-106-107
                        _AuxValue6 = dp.ReadSingle(); // 108-109-110-111
                        _SpeedLog = dp.ReadSingle(); // 112-113-114-115
                        _Turbidity = dp.ReadSingle(); // 116-117-118-119
                        _ShipSpeed = dp.ReadSingle(); // 120-121-122-123
                        _ShipGyro = dp.ReadSingle(); // 124-125-126-127
                        _ShipCoordinateY = dp.ReadDouble(); // 128-129-130-131-132-133-134-135
                        _ShipCoordinateX = dp.ReadDouble(); // 136-137-138-139-140-141-142-143
                        _ShipAltitude = dp.ReadUInt16(); // 144-145
                        _ShipDepth = dp.ReadUInt16(); // 146-147
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
                        { _FixTime = outTime; }
                        else
                        { _FixTime = DateTime.MinValue; }

                        _SensorSpeed = dp.ReadSingle(); // 152-153-154-155
                        _KP = dp.ReadSingle(); // 156-157-158-159
                        _SensorCoordinateY = dp.ReadDouble(); // 160-161-162-163-164-165-166-167
                        _SensorCoordinateX = dp.ReadDouble(); // 168-169-170-171-172-173-174-175
                        _SonarStatus = dp.ReadUInt16(); // 176-177
                        _RangeToFish = dp.ReadUInt16(); // 178-179
                        _BearingToFish = dp.ReadUInt16(); // 180-181
                        _CableOut = dp.ReadUInt16(); // 182-183
                        _Layback = dp.ReadSingle(); // 184-185-186-187
                        _CableTension = dp.ReadSingle(); // 188-189-190-191
                        _SensorDepth = dp.ReadSingle(); // 192-193-194-195
                        _SensorPrimaryAltitude = dp.ReadSingle(); // 196-197-198-199
                        _SensorAuxAltitude = dp.ReadSingle(); // 200-201-202-203
                        _SensorPitch = dp.ReadSingle(); // 204-205-206-207
                        _SensorRoll = dp.ReadSingle(); // 208-209-210-211
                        _SensorHeading = dp.ReadSingle(); // 212-213-214-215
                        _Heave = dp.ReadSingle(); // 216-217-218-219
                        _Yaw = dp.ReadSingle(); // 220-221-222-223
                        _AttitudeTimeTag = dp.ReadUInt32(); // 224-225-226-227
                        _DistanceOffTrack = dp.ReadSingle(); // 228-229-230-231
                        _NavigationFixMilliseconds = dp.ReadUInt32(); // 232-233-234-235
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
                        { _ComputerClockTime = outTime; }
                        else
                        { _ComputerClockTime = DateTime.MinValue; }

                        _FishPositionDeltaX = dp.ReadInt16(); // 240-241
                        _FishPositionDeltaY = dp.ReadInt16(); // 242-243
                        _FishPositionErrorCode = dp.ReadByte(); // 244
                    }
                }
            }
        }

        #endregion constructors
    }
}