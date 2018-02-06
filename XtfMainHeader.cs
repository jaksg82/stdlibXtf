using System;
using stdlibXtf.Enums;
using System.Collections.Generic;
using stdlibXtf.SubPackets;
using System.IO;

namespace stdlibXtf
{
    public class XtfMainHeader
    {
        #region private properties

        private String _RecordingProgramName;
        private String _RecordingProgramVersion;
        private String _SonarName;
        private String _NoteString;
        private String _ThisFileName;
        //private List<ChannelInfo> _Channels;

        #endregion

        #region public properties

        /// <summary>
        /// This is the number that identify the start of each packet header.
        /// </summary>
        // static public UInt16 MagicNumber { get { return 64206; } }

        /// <summary>
        /// Xtf file format version.
        /// This value should be 123 as per Triton documentation.
        /// </summary>
        public Byte FileFormat { get; set; }

        /// <summary>
        /// Xtf system type.
        /// This value should be 1 as per Triton documentation.
        /// </summary>
        public Byte SystemType { get; set; }

        /// <summary>
        /// Name of the program used to create this xtf file.
        /// The maximum size of the name is of 8 characters. Longer strings will be shortened.
        /// </summary>
        public String RecordingProgramName
        {
            get { return _RecordingProgramName; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _RecordingProgramName = " ".PadRight(8); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 8) { _RecordingProgramName = tmpVal.Substring(0, 8); }
                    else { _RecordingProgramName = tmpVal.PadRight(8); }
                }
            }
        }

        /// <summary>
        /// Version of the program used to create this xtf file.
        /// It's best to use 223, for compability reasons, or 341 for newer format improvments.
        /// The maximum size is of 8 characters. Longer strings will be shortened.
        /// </summary>
        public String RecordingProgramVersion
        {
            get { return _RecordingProgramVersion; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _RecordingProgramVersion = " ".PadRight(8); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 8) { _RecordingProgramVersion = tmpVal.Substring(0, 8); }
                    else { _RecordingProgramVersion = tmpVal.PadRight(8); }
                }
            }
        }

        /// <summary>
        /// Name of server used to access sonar.
        /// The maximum size is of 16 characters. Longer strings will be shortened.
        /// </summary>
        public String SonarName
        {
            get { return _SonarName; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _SonarName = " ".PadRight(16); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 16) { _SonarName = tmpVal.Substring(0, 16); }
                    else { _SonarName = tmpVal.PadRight(16); }
                }
            }
        }

        /// <summary>
        /// ID of the sonar type used to acquire the data.
        /// </summary>
        public UInt16 SonarType { get; set; }

        /// <summary>
        /// Notes.
        /// The maximum size is of 64 characters. Longer strings will be shortened.
        /// </summary>
        public String NoteString
        {
            get { return _NoteString; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _NoteString = " ".PadRight(64); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 64) { _NoteString = tmpVal.Substring(0, 64); }
                    else { _NoteString = tmpVal.PadRight(64); }
                }
            }
        }

        /// <summary>
        /// Name of this file.
        /// The maximum size is of 64 characters. Longer strings will be shortened.
        /// </summary>
        public String ThisFileName
        {
            get { return _ThisFileName; }
            set
            {
                if (String.IsNullOrEmpty(value)) { _ThisFileName = " ".PadRight(64); }
                else
                {
                    String tmpVal = value.Replace((char)0, ' ');
                    if (tmpVal.Length > 64) { _ThisFileName = tmpVal.Substring(0, 64); }
                    else { _ThisFileName = tmpVal.PadRight(64); }
                }
            }
        }

        /// <summary>
        /// Coordinate units.
        /// 0 for meters or 3 for Lat/Lon.
        /// </summary>
        public CoordinateUnits NavigationCoordinateUnits { get; set; }

        public UInt16 NumberOfSonarChannels { get; set; }
        public UInt16 NumberOfBathymetryChannels { get; set; }
        public Byte NumberOfSnippetChannels { get; set; }
        public Byte NumberOfForwardLookArrays { get; set; }
        public UInt16 NumberOfEchoStrengthChannels { get; set; }
        public Byte NumberOfInterferometryChannels { get; set; }

        /// <summary>
        /// Height of reference point above water line (m)
        /// </summary>
        public Single ReferencePointHeight { get; set; }

        /// <summary>
        /// Latency of nav system in milliceconds.
        /// Usually GPS latency.
        /// </summary>
        public Int32 NavigationLatency { get; set; }

        /// <summary>
        /// Orientation of positive Y is forward.
        /// </summary>
        public Single NavigationOffsetY { get; set; }

        /// <summary>
        /// Orientation of positive X is to starboard.
        /// </summary>
        public Single NavigationOffsetX { get; set; }

        /// <summary>
        /// Orientation of positive Z is down.
        /// </summary>
        public Single NavigationOffsetZ { get; set; }

        /// <summary>
        /// Orientation of positive Yaw is turn to right.
        /// </summary>
        public Single NavigationOffsetYaw { get; set; }

        /// <summary>
        /// Orientation of positive Y is forward.
        /// </summary>
        public Single MRUOffsetY { get; set; }

        /// <summary>
        /// Orientation of positive X is to starboard.
        /// </summary>
        public Single MRUOffsetX { get; set; }

        /// <summary>
        /// Orientation of positive Z is down.
        /// </summary>
        public Single MRUOffsetZ { get; set; }

        /// <summary>
        /// Orientation of positive Yaw is turn to right.
        /// </summary>
        public Single MRUOffsetYaw { get; set; }

        /// <summary>
        /// Orientation of positive Pitch is nose up.
        /// </summary>
        public Single MRUOffsetPitch { get; set; }

        /// <summary>
        /// Orientation of positive Roll is lean to starboard.
        /// </summary>
        public Single MRUOffsetRoll { get; set; }

        ///// <summary>
        ///// Data for each channel.
        ///// All sidescan channels will always precede the bathymetry channels.
        ///// </summary>
        //public List<ChannelInfo> Channels { get { return _Channels; } }

        #endregion

        #region contructors

        /// <summary>
        /// Create an empty xtf file header.
        /// </summary>
        public XtfMainHeader()
        {
            FileFormat = 123;
            SystemType = 1;
            RecordingProgramName = "nd";
            RecordingProgramVersion = "223";
            SonarName = "nd";
            SonarType = 0;
            NoteString = "nd";
            ThisFileName = "nd";
            NavigationCoordinateUnits = 0;
            NumberOfSonarChannels = 0;
            NumberOfBathymetryChannels = 0;
            NumberOfSnippetChannels = 0;
            NumberOfForwardLookArrays = 0;
            NumberOfEchoStrengthChannels = 0;
            NumberOfInterferometryChannels = 0;
            ReferencePointHeight = 0;
            NavigationLatency = 0;
            NavigationOffsetY = 0;
            NavigationOffsetX = 0;
            NavigationOffsetZ = 0;
            NavigationOffsetYaw = 0;
            MRUOffsetY = 0;
            MRUOffsetX = 0;
            MRUOffsetZ = 0;
            MRUOffsetYaw = 0;
            MRUOffsetPitch = 0;
            MRUOffsetRoll = 0;
            //_Channels = new List<ChannelInfo> { new ChannelInfo() };

        }

        /// <summary>
        /// Create the xtf header from the given bytes.
        /// 256 byte that contain the main header of the xtf file.
        /// </summary>
        public XtfMainHeader(Byte[] byteArray)
        {
            FileFormat = 123;
            SystemType = 1;
            RecordingProgramName = "nd";
            RecordingProgramVersion = "223";
            SonarName = "nd";
            SonarType = 0;
            NoteString = "nd";
            ThisFileName = "nd";
            NavigationCoordinateUnits = 0;
            NumberOfSonarChannels = 0;
            NumberOfBathymetryChannels = 0;
            NumberOfSnippetChannels = 0;
            NumberOfForwardLookArrays = 0;
            NumberOfEchoStrengthChannels = 0;
            NumberOfInterferometryChannels = 0;
            ReferencePointHeight = 0;
            NavigationLatency = 0;
            NavigationOffsetY = 0;
            NavigationOffsetX = 0;
            NavigationOffsetZ = 0;
            NavigationOffsetYaw = 0;
            MRUOffsetY = 0;
            MRUOffsetX = 0;
            MRUOffsetZ = 0;
            MRUOffsetYaw = 0;
            MRUOffsetPitch = 0;
            MRUOffsetRoll = 0;
            //_Channels = new List<ChannelInfo> { new ChannelInfo() };

            if (byteArray.Length >= 256)
            {
                using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
                {
                    FileFormat = dp.ReadByte(); // 0
                    SystemType = dp.ReadByte(); // 1
                    RecordingProgramName = new String(dp.ReadChars(8)); // 2-3-4-5-6-7-8-9
                    RecordingProgramVersion = new String(dp.ReadChars(8)); // 10-11-12-13-14-15-16-17
                    SonarName = new String(dp.ReadChars(16)); // 18-19-20-21-22-23-24-25-26-27-28-29-30-31-32-33
                    SonarType = dp.ReadUInt16(); // 34-35
                    NoteString = new String(dp.ReadChars(64)); // 36 -> 99
                    ThisFileName = new String(dp.ReadChars(64)); // 100 -> 163
                    NavigationCoordinateUnits = (CoordinateUnits)dp.ReadUInt16(); // 164-165
                    NumberOfSonarChannels = dp.ReadUInt16(); // 166-167
                    NumberOfBathymetryChannels = dp.ReadUInt16(); // 168-169
                    NumberOfSnippetChannels = dp.ReadByte(); // 170
                    NumberOfForwardLookArrays = dp.ReadByte(); // 171
                    NumberOfEchoStrengthChannels = dp.ReadUInt16(); // 172-173
                    NumberOfInterferometryChannels = dp.ReadByte(); // 174
                    dp.ReadByte(); // 175 Reserved
                    dp.ReadUInt16(); // 176-177 Reserved
                    ReferencePointHeight = dp.ReadSingle(); // 178-179-180-181
                    dp.ReadBytes(12); // 182 -> 193 ProjectionType Not currently used
                    dp.ReadBytes(10); // 194 -> 203 SpheroidType Not currently used
                    NavigationLatency = dp.ReadInt32(); // 204-205-206-207
                    dp.ReadSingle(); // 208-209-210-211 OriginY Not currently used
                    dp.ReadSingle(); // 212-213-214-215 OriginX Not currently used
                    NavigationOffsetY = dp.ReadSingle(); // 216-217-218-219
                    NavigationOffsetX = dp.ReadSingle(); // 220-221-222-223
                    NavigationOffsetZ = dp.ReadSingle(); // 224-225-226-227
                    NavigationOffsetYaw = dp.ReadSingle(); // 228-229-230-231
                    MRUOffsetY = dp.ReadSingle(); // 232-233-234-235
                    MRUOffsetX = dp.ReadSingle(); // 236-237-238-239
                    MRUOffsetZ = dp.ReadSingle(); // 240-241-242-243
                    MRUOffsetYaw = dp.ReadSingle(); // 244-245-246-247
                    MRUOffsetPitch = dp.ReadSingle(); // 248-249-250-251
                    MRUOffsetRoll = dp.ReadSingle(); // 252-253-254-255

                    //_Channels.Clear();
                    //int chnum = NumberOfSonarChannels + NumberOfBathymetryChannels - 1;

                    //if (chnum <= 6)
                    //{
                    //    for (int i = 0; i <= chnum; i++)
                    //    {
                    //        _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                    //    }
                    //}
                    //else
                    //{
                    //    if (byteArray.Length >= 2048)
                    //    {
                    //        for (int i = 0; i <= chnum; i++)
                    //        {
                    //            _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        for (int i = 0; i <= 6; i++)
                    //        {
                    //            _Channels.Add(new ChannelInfo(dp.ReadBytes(128)));
                    //        }
                    //    }
                    //}
                }
            }

            #endregion

        }
    }
}
