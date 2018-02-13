namespace stdlibXtf.Enums
{
    /// <summary>
    /// Specify the type of the information stored in a channel.
    /// Stored as Byte
    /// </summary>
    public enum ChannelTypes
    {
        /// <summary>
        /// The data stored in this channel represent a subbottom sensor.
        /// </summary>
        Subbottom = 0,

        /// <summary>
        /// The data stored in this channel represent the port sensor of a side scan sonar.
        /// </summary>
        Port = 1,

        /// <summary>
        /// The data stored in this channel represent the starboard sensor of a side scan sonar.
        /// </summary>
        Starboard = 2,

        /// <summary>
        /// The data stored in this channel represent a multibeam echosounder sensor.
        /// </summary>
        Bathymetry = 3
    }
}