namespace stdlibXtf.Enums
{
    /// <summary>
    /// Specify the units of the coordinates stored in the XTF file.
    /// Stored as 16 bit Unsigned Integer
    /// </summary>
    public enum CoordinateUnits
    {
        /// <summary>
        /// The coordinates are metric (i.e. UTM).
        /// </summary>
        Meters = 0,

        /// <summary>
        /// The coordinates are geographic (i.e. GPS).
        /// </summary>
        LatLon = 3
    }
}