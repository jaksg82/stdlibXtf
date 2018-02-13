using System.Collections.Generic;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define a collection of the known packet header types.
    /// </summary>
    public class PacketHeaderTypes
    {
        #region private properties

        private List<TypeEntry> _KnownTypes;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the array of TypeEntry objects that represent all the known packets.
        /// </summary>
        public List<TypeEntry> KnownTypes { get { return _KnownTypes; } }

        #endregion public properties

        #region constructor

        /// <summary>
        /// Initializes a new instance of the PacketHeaderTypes class that has all the values.
        /// </summary>
        public PacketHeaderTypes()
        {
            _KnownTypes = new List<TypeEntry>
            {
                new TypeEntry(0, "XtfHeaderSonar", "Sidescan And subbottom"),
                new TypeEntry(1, "XtfHeaderNotes", "Notes - text annotation"),
                new TypeEntry(2, "XtfHeaderBathy", "Bathymetry (Seabat, Odom)"),
                new TypeEntry(3, "XtfHeaderAttitude", "TSS Or MRU attitude (pitch, roll, heave, yaw)"),
                new TypeEntry(4, "XtfHeaderForward", "Forward-look sonar (polar display)"),
                new TypeEntry(5, "XtfHeaderElac", "Elac multibeam"),
                new TypeEntry(6, "XtfHeaderRawSerial", "Raw data from serial port"),
                new TypeEntry(7, "XtfHeaderEmbeddedHead", "Embedded header Structure"),
                new TypeEntry(8, "XtfHeaderHiddenSonar", "Hidden (non-displayable) ping"),
                new TypeEntry(9, "XtfHeaderSeaviewProcessedBathy", "Bathymetry (angles) for Seaview"),
                new TypeEntry(10, "XtfHeaderSeaviewDepths", "Bathymetry from Seaview data (depths)"),
                new TypeEntry(11, "XtfHeaderRsvdHighSpeedSensor", "Used by Klein. 0=roll, 1=yaw"),
                new TypeEntry(12, "XtfHeaderEchoStrength", "Elac EchoStrength (10 values)"),
                new TypeEntry(13, "XtfHeaderGeorec", "Used to store mosaic parameters"),
                new TypeEntry(14, "XtfHeaderKleinRawBathy", "Bathymetry data from the Klein 5000"),
                new TypeEntry(15, "XtfHeaderHighSpeedSensor2", "High speed sensor from Klein 5000"),
                new TypeEntry(16, "XtfHeaderElacXse", "Elac dual-head"),
                new TypeEntry(17, "XtfHeaderBathyXYZA", "Processed bathymetry data"),
                new TypeEntry(18, "XtfHeaderK5000BathyIQ", "Raw IQ data from Klein 5000 server"),
                new TypeEntry(19, "XtfHeaderBathySnippet", "Bathymetry snippet data"),
                new TypeEntry(20, "XtfHeaderGps", "GPS Position"),
                new TypeEntry(21, "XtfHeaderStat", "GPS statistics"),
                new TypeEntry(22, "XtfHeaderSingleBeam", "Bathymetry data from singlebeam echosounder"),
                new TypeEntry(23, "XtfHeaderGyro", "Heading/Speed Sensor"),
                new TypeEntry(24, "XtfHeaderTrackPoint", ""),
                new TypeEntry(25, "XtfHeaderMultiBeam", ""),
                new TypeEntry(26, "XtfHeaderQpsSingleBeam", ""),
                new TypeEntry(27, "XtfHeaderQpsMultiTx", ""),
                new TypeEntry(28, "XtfHeaderQpsMultiBeam", ""),
                new TypeEntry(42, "XtfHeaderNavigation", "Source time-stamped navigation data"),
                new TypeEntry(50, "XtfHeaderTime", ""),
                new TypeEntry(60, "XtfHeaderBenthosCaatiSara", "Custom Benthos data."),
                new TypeEntry(61, "XtfHeader7125", "7125 Bathy Data"),
                new TypeEntry(62, "XtfHeader7125Snippet", "7125 Bathy Data Snippets"),
                new TypeEntry(65, "XtfHeaderQinsyR2SonicBathy", "QINSy R2Sonic bathymetry data"),
                new TypeEntry(66, "XtfHeaderQinsyR2SonicFts", "QINSy R2Sonics Foot Print Time Series (snippets)"),
                new TypeEntry(68, "XtfHeaderR2SonicBathy", "Triton R2Sonic bathymetry data"),
                new TypeEntry(69, "XtfHeaderR2SonicFts", "Triton R2Sonic Footprint Time Series"),
                new TypeEntry(70, "XtfHeaderCodaEchoscopeData", "Custom CODA Echoscope Data"),
                new TypeEntry(71, "XtfHeaderCodaEchoscopeConfig", "Custom CODA Echoscope Data"),
                new TypeEntry(72, "XtfHeaderCodaEchoscopeImage", "Custom CODA Echoscope Data"),
                new TypeEntry(73, "XtfHeaderEdgetech4600", ""),
                new TypeEntry(78, "XtfHeaderReson7018WaterColumn", ""),
                new TypeEntry(79, "XtfHeaderR2SonicWaterColumn", ""),
                new TypeEntry(84, "XtfHeaderSourceTimeGyro", "Source time-stamped gyro data"),
                new TypeEntry(100, "XtfHeaderPosition", "Raw position packet - Reserved for use by Reson, Inc. RESON ONLY."),
                new TypeEntry(102, "XtfHeaderBathyProcessed", ""),
                new TypeEntry(103, "XtfHeaderAttitudeProcessed", ""),
                new TypeEntry(104, "XtfHeaderSingleBeamProcessed", ""),
                new TypeEntry(105, "XtfHeaderAuxProcessed", "Aux Channel + AuxAltitude + Magnetometer."),
                new TypeEntry(106, "XtfHeaderKlein3000DataPage", ""),
                new TypeEntry(107, "XtfHeaderPositionRawNavigation", ""),
                new TypeEntry(108, "XtfHeaderKleinV4DataPage", ""),
                new TypeEntry(199, "XtfHeaderCustom", "Custom Vendor data"),
                new TypeEntry(200, "XtfHeaderUserDefined", "This packet type is reserved for specific applications.")
            };
        }

        #endregion constructor

        #region methods

        /// <summary>
        /// Gets the name associated to the given ID.
        /// </summary>
        /// <param name="id">Byte value of the packet type.</param>
        /// <returns>A string with the name of the packet, if available.</returns>
        public string GetName(byte id)
        {
            string foundname = null;
            foreach (var hdr in KnownTypes)
            {
                if (hdr.ID == id)
                    foundname = hdr.Name;
            }

            return string.IsNullOrWhiteSpace(foundname) ? "Unknown" : foundname;
        }

        /// <summary>
        /// Gets the description associated to the given ID.
        /// </summary>
        /// <param name="id">Byte value of the packet type.</param>
        /// <returns>A string with the description of the packet, if available.</returns>
        public string GetDescription(byte id)
        {
            string foundname = null;
            foreach (var hdr in KnownTypes)
            {
                if (hdr.ID == id)
                    foundname = hdr.Description;
            }

            return string.IsNullOrWhiteSpace(foundname) ? "Unknown" : foundname;
        }

        #endregion methods
    }
}