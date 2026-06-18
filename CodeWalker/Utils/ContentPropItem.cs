using CodeWalker.GameFiles;
using System;

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
    }
}
