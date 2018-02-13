namespace stdlibXtf.Enums
{
    /// <summary>
    /// Specify the subject of the annotation.
    /// Stored as Byte in the xtf file
    /// </summary>
    public enum NoteSubChannels
    {
        /// <summary>
        /// Annotation from Param window.
        /// </summary>
        Generic = 0,

        /// <summary>
        /// Annotation relative to the vessel name.
        /// </summary>
        VesselName = 1,

        /// <summary>
        /// Annotation relative to the survey area.
        /// </summary>
        SurveyArea = 2,

        /// <summary>
        /// Annotation relative to the operator name.
        /// </summary>
        OperatorName = 3
    }
}