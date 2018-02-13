using System.Collections.Generic;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Define the container for the statistics of the document.
    /// </summary>
    public class StatCollection
    {
        #region Private properties

        private List<StatEntry> _Groups;

        #endregion Private properties

        #region Public properties

        /// <summary>
        /// Gets the array of StatEntry objects that represent the added packets.
        /// </summary>
        public List<StatEntry> Groups { get { return _Groups; } }

        #endregion Public properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StatCollection class.
        /// </summary>
        public StatCollection()
        {
            _Groups = new List<StatEntry>();
        }

        #endregion Constructors

        #region methods

        /// <summary>
        /// Add the given packet to the statistic collection.
        /// </summary>
        /// <param name="packet">The packet to add.</param>
        /// <returns>True if successful added.</returns>
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

        #endregion methods
    }
}