using System;

namespace stdlibXtf.Common
{
    public class TypeEntry
    {
        // TODO: make read-only
        public byte ID { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public TypeEntry()
        {
            ID = 0;
            Name = "None";
            Description = "Default";
        }

        public TypeEntry(byte id, String name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
        }

    }
}
