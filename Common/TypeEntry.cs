using System;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define the entry for the collections of known sonar and packets.
    /// </summary>
    public class TypeEntry
    {
        #region private properties

        private Byte _ID;
        private String _Name;
        private String _Description;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the number that identify the kind of packet header.
        /// </summary>
        public byte ID { get { return _ID; } }

        /// <summary>
        /// Gets the known name of the packet.
        /// </summary>
        public String Name { get { return _Name; } }

        /// <summary>
        /// Gets the known description of the packet.
        /// </summary>
        public String Description { get { return _Description; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the TypeEntry class that has default zero values.
        /// </summary>
        public TypeEntry()
        {
            _ID = 255;
            _Name = "None";
            _Description = "Default";
        }

        /// <summary>
        /// Initializes a new instance of the TypeEntry class with the given values.
        /// </summary>
        /// <param name="id">Byte value of the packet type.</param>
        /// <param name="name">Name string.</param>
        /// <param name="description">Description string.</param>
        public TypeEntry(Byte id, String name, String description)
        {
            _ID = id;
            _Name = name;
            _Description = description;
        }

        #endregion constructors
    }
}