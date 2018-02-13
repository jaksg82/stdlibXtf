using System;
using System.Collections.Generic;
using System.Text;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define a index/value pair that is used to store and retrieve a packet location inside the XTF file.
    /// </summary>
    public class IndexEntry
    {
        #region private properties

        private Byte _PacketType;
        private UInt32 _Index;

        #endregion private properties

        #region public properties

        /// <summary>
        /// Gets the number that identify the kind of packet header.
        /// </summary>
        public byte PacketType { get { return _PacketType; } }

        /// <summary>
        /// Gets the position where start the packet inside the XTF file.
        /// </summary>
        public UInt32 Index { get { return _Index; } }

        #endregion public properties

        #region constructors

        /// <summary>
        /// Initializes a new instance of the IndexEntry class that has default zero values.
        /// </summary>
        public IndexEntry()
        {
            _PacketType = 255;
            _Index = 0;
        }

        /// <summary>
        /// Initializes a new instance of the IndexEntry class with the given values.
        /// </summary>
        /// <param name="ID">Byte value that identify the type of package.</param>
        /// <param name="index">Byte number where the packet start.</param>
        public IndexEntry(byte ID, UInt32 index)
        {
            _PacketType = ID;
            _Index = index;
        }

        #endregion constructors
    }
}