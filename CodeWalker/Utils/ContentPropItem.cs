using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;

namespace CodeWalker.Utils
{
    public class ContentPropItem
    {
        private static readonly string[] _extensions = { ".ydr", ".ydd", ".ytf", ".ymap" };

        public string Name { get; set; }

        public YdrFile YdrFile { get; set; }

        public string FilePath { get; set; }

        public string ThumbnailPath { get; set; }

        public Archetype Archetype { get; set; }

        public bool IsFavorite { get; set; }

        public ContentPropItem(string aName, YdrFile aYdrFile = null)
        {
            Name = aName;
            YdrFile = aYdrFile;
            ThumbnailPath = ContentThumbnailCache.GetThumbnailPath(GetCleanName());
        }

        public string GetCleanName()
        {
            if (string.IsNullOrEmpty(Name)) return Name;
            foreach (var ext in _extensions)
            {
                if (Name.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                    return Name.Substring(0, Name.Length - ext.Length);
            }
            return Name;
        }

        public bool HasThumbnail()
        {
            return ContentThumbnailCache.Exists(GetCleanName());
        }

        public string GetFlagsText()
        {
            if (Archetype?.BaseArchetypeDef == null)
                return "—";

            uint flags = Archetype.BaseArchetypeDef.flags;
            var parts = new List<string>();

            if ((flags & 0x20000000) != 0) parts.Add("Dynamic");
            if ((flags & 0x00040000) != 0) parts.Add("Glass");
            if ((flags & 0x00008000) != 0) parts.Add("Ladder");
            if ((flags & 0x04000000) != 0) parts.Add("Door");
            if ((flags & 0x00000010) != 0) parts.Add("Tree");
            if ((flags & 0x00000002) != 0) parts.Add("MLO");

            return parts.Count > 0 ? string.Join(", ", parts) : "—";
        }
    }
}
