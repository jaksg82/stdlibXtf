using System;

namespace stdlibXtf.Common
{
    public class StatEntry
    {
        #region Private properties

        private byte _ID;
        private String _Name;
        private String _Description;
        private UInt16 _Count;
        
        #endregion

        #region Public properties

        public byte ID {
            get { return _ID; }
            set {
                PacketHeaderTypes hdrTypes = new PacketHeaderTypes();
                _ID = value;
                _Name = hdrTypes.GetName(_ID);
                _Description = hdrTypes.GetDescription(_ID);
            } }

        public String Name { get { return _Name; } }
        public String Description { get { return _Description; } }
        public UInt16 Count { get { return _Count; } }
        public IPacket FirstPacket { get; set; }
        public IPacket LastPacket { get; set; }

        #endregion

        #region Constructors

        public StatEntry()
        {
            ID = 255;
            _Count = 0;
        }

        public StatEntry(IPacket packet)
        {
            ID = packet.HeaderType;
            _Count = 1;
            FirstPacket = packet;
            LastPacket = packet;
        }

        #endregion

        #region Functions

        public bool Add(IPacket packet)
        {
            if (packet.HeaderType == ID)
            {
                _Count = (UInt16)(_Count + 1);
                LastPacket = packet;
                return true;
            }
            else { return false; }
        }

        #endregion

    }
}
