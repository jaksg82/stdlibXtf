using System.Collections.Generic;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define a collection of the known sonar models.
    /// </summary>
    public class SonarModelTypes
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
        /// Initializes a new instance of the SonarModelTypes class that has all the values.
        /// </summary>
        public SonarModelTypes()
        {
            _KnownTypes = new List<TypeEntry>
            {
                new TypeEntry(0, "None", "Default"),
                new TypeEntry(1, "Jamstec", "Jamstec chirp 2-channel subbottom"),
                new TypeEntry(2, "AnalogC31", "PC31 8-channel"),
                new TypeEntry(3, "Sis1000", "Chirp SIS-1000 sonar"),
                new TypeEntry(4, "Analog32Chan", "Spectrum with 32-channel DSPlink card"),
                new TypeEntry(5, "Klein2000", "Klein system 2000 with digital interface"),
                new TypeEntry(6, "Rws", "Standard PC31 analog with special nav code"),
                new TypeEntry(7, "Df1000", "EG&G DF1000 digital interface"),
                new TypeEntry(8, "Seabat", "Reson SEABAT 900x analog/serial"),
                new TypeEntry(9, "Klein595", "4-chan Klein 595, same as ANALOG_C31"),
                new TypeEntry(10, "Egg260", "2-channel EGG260, same as ANALOG_C31"),
                new TypeEntry(11, "SonatechDds", "Sonatech Diver Detection System on Spectrum DSP32C"),
                new TypeEntry(12, "Echoscan", "Odom EchoScanII multibeam (with simultaneous analog sidescan)"),
                new TypeEntry(13, "Elac", "Elac multibeam system"),
                new TypeEntry(14, "Klein5000", "Klein system 5000 with digital interface"),
                new TypeEntry(15, "Reson8101", "Reson Seabat 8101"),
                new TypeEntry(16, "Imagenex858", "Imagenex model 858"),
                new TypeEntry(17, "UsnSilos", "USN SILOS with 3-channel analog"),
                new TypeEntry(18, "SonatechShr", "Sonatech Super-high res sidescan sonar"),
                new TypeEntry(19, "DelphAu32", "Delph AU32 Analog input (2 channel)"),
                new TypeEntry(20, "GenericMemory", "Generic sonar using the memory-mapped file interface"),
                new TypeEntry(21, "SimradSm2000", "Simrad SM2000 Multibeam Echo Sounder"),
                new TypeEntry(22, "Audio", "Standard multimedia audio"),
                new TypeEntry(23, "EdgetechAci", "Edgetech (EG&G) ACI card for 260 sonar through PC31 card"),
                new TypeEntry(24, "EdgetechBlackBox", "Edgetech Black Box"),
                new TypeEntry(25, "FugroDeepTow", "Fugro deeptow"),
                new TypeEntry(26, "EdgetechCC", "C&C Edgetech Chirp conversion program"),
                new TypeEntry(27, "DtiSas", "DTI SAS Synthetic Aperture processor (memmap file)"),
                new TypeEntry(28, "OsirisSss", "Fugro Osiris AUV Sidescan data"),
                new TypeEntry(29, "OsirisMbes", "Fugro Osiris AUV Multibeam data"),
                new TypeEntry(30, "GeoacousticsSls", "Geoacoustics SLS"),
                new TypeEntry(31, "SimradEm2000", "Simrad EM2000/EM3000"),
                new TypeEntry(32, "Klein3000", "Klein system 3000"),
                new TypeEntry(33, "ShrSss", "SHRSSS Chirp system"),
                new TypeEntry(34, "BenthosC3D", "Benthos C3D SARA/CAATI"),
                new TypeEntry(35, "EdgetechMpx", "Edgetech MP-X"),
                new TypeEntry(36, "Cmax", "CMAX"),
                new TypeEntry(37, "BenthosSis1624", "Benthos sis1624"),
                new TypeEntry(38, "Edgetech4200", "Edgetech 4200"),
                new TypeEntry(39, "BenthosSis1500", "Benthos SIS1500"),
                new TypeEntry(40, "BenthosSis1502", "Benthos SIS1502"),
                new TypeEntry(41, "BenthosSis3000", "Benthos SIS3000"),
                new TypeEntry(42, "BenthosSis7000", "Benthos SIS7000"),
                new TypeEntry(43, "Df1000Dcu", "DF1000 DCU"),
                new TypeEntry(44, "NoneSideScan", "NONE_SIDESCAN"),
                new TypeEntry(45, "NoneMultiBeam", "NONE_MULTIBEAM"),
                new TypeEntry(46, "Reson7125", "Reson 7125"),
                new TypeEntry(47, "Coda", "CODA Echoscope"),
                new TypeEntry(48, "KongsbergSas", "Kongsberg SAS"),
                new TypeEntry(49, "Qinsy", "QINSy"),
                new TypeEntry(50, "GeoacousticsDsss", "GeoAcoustics DSSS"),
                new TypeEntry(51, "CmaxUsb", "CMAX_USB"),
                new TypeEntry(52, "SwathPlusBathy", "SwathPlus Bathy"),
                new TypeEntry(53, "R2SonicQinsy", "R2Sonic QINSy"),
                new TypeEntry(54, "SwathPlusBathyConverted", "Converted SwathPlus Bathy"),
                new TypeEntry(55, "R2SonicTriton", "R2Sonic Triton"),
                new TypeEntry(56, "Edgetech4600", "Edgetech 4600"),
                new TypeEntry(57, "Klein3500", "Klein 3500"),
                new TypeEntry(58, "Klein5900", "Klein 5900")
            };
        }

        #endregion constructor

        #region methods

        /// <summary>
        /// Gets the name associated to the given ID.
        /// </summary>
        /// <param name="id">Byte value of the sonar type.</param>
        /// <returns>A string with the name of the sonar, if available.</returns>
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
        /// <param name="id">Byte value of the sonar type.</param>
        /// <returns>A string with the description of the sonar, if available.</returns>
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