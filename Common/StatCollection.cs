using System;
using System.Collections.Generic;
using System.Text;

namespace stdlibXtf.Common
{
    public class StatCollection
    {
        #region Private properties

        private List<StatEntry> _Groups;

        #endregion

        #region Public properties

        public List<StatEntry> Groups { get { return _Groups; } }

        #endregion

        #region Constructors

        public StatCollection()
        {
            _Groups = new List<StatEntry>();
        }

        #endregion

        #region Functions

        public bool AddPacket(IPacket packet)
        {
            bool groupFound = false;
            if (_Groups.Count > 0)
            {
                for (int i = 0; i < _Groups.Count; i++)
                {
                    if (_Groups[i].ID == packet.HeaderType)
                    {
                        _Groups[i].Add(packet);
                        groupFound = true;
                    }
                }
                if (!groupFound)
                {
                    _Groups.Add(new StatEntry(packet));
                    return true;
                }
                else { return false; }
            }
            else
            {
                _Groups.Add(new StatEntry(packet));
                return true;
            }
        }

        #endregion

    }
}
