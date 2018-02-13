using System;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define the container for the statistics of a specific packet.
    /// </summary>
    public class StatEntry
    {
        #region Private properties

        private byte _ID;
        private String _Name;
        private String _Description;
        private UInt16 _Count;
        private IPacket _FirstPacket;
        private IPacket _LastPacket;

        #endregion Private properties

        #region Public properties

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

        /// <summary>
        /// Gets the number of the packets added to this instance of StatEntry.
        /// </summary>
        public UInt16 Count { get { return _Count; } }

        /// <summary>
        /// Gets the packet used to inizialise this instance of StatEntry.
        /// </summary>
        public IPacket FirstPacket { get { return _FirstPacket; } }

        /// <summary>
        /// Gets the last packet added to this instance of StatEntry.
        /// </summary>
        public IPacket LastPacket { get { return _LastPacket; } }

        #endregion Public properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StatEntry class that has default zero values.
        /// </summary>
        public StatEntry()
        {
            _ID = 255;
            _Count = 0;
            UpdateID(ID);
        }

        /// <summary>
        /// Initializes a new instance of the StatEntry class with the values from the given packet.
        /// </summary>
        /// <param name="packet">The packet to be added.</param>
        public StatEntry(IPacket packet)
        {
            _ID = packet.HeaderType;
            UpdateID(ID);
            _Count = 1;
            _FirstPacket = packet;
            _LastPacket = packet;
        }

        #endregion Constructors

        #region methods

        /// <summary>
        /// The given packet will be added to this instance of the StatEntry class only if is of the same kind of the initialization one.
        /// </summary>
        /// <param name="packet">The packet to be added.</param>
        /// <returns>True if the packet is added correctly.</returns>
        public bool Add(IPacket packet)
        {
            if (ID == 255) // If ID == 255 then initialize with the added packet
            {
                _ID = packet.HeaderType;
                UpdateID(ID);
                _Count = 1;
                _FirstPacket = packet;
                _LastPacket = packet;
                return true;
            }

            if (packet.HeaderType == ID)
            {
                _Count = (UInt16)(_Count + 1);
                _LastPacket = packet;
                return true;
            }
            else { return false; }
        }

        private void UpdateID(Byte id)
        {
            PacketHeaderTypes hdrTypes = new PacketHeaderTypes();
            _ID = id;
            _Name = hdrTypes.GetName(_ID);
            _Description = hdrTypes.GetDescription(_ID);
        }

        #endregion methods
    }
}