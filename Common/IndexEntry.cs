using System;
using System.Collections.Generic;
using System.Text;

namespace stdlibXtf.Common
{
    public class IndexEntry
    {
        // TODO: make read-only
        public byte PacketType { get; set; }
        public UInt32 Index { get; set; }


        public IndexEntry()
        {
            PacketType = 255;
            Index = 0;
        }

        public IndexEntry(byte ID, UInt32 index)
        {
            PacketType = ID;
            Index = index;
        }


    }
}
