using CodeWalker.GameFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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

        public ContentPropItem(string aName, YdrFile aYdrFile)
        {
            Name = aName;
            YdrFile = aYdrFile;

            var codeWalkerDir = AppDomain.CurrentDomain.BaseDirectory;
            var thumbnailDir = Path.Combine(codeWalkerDir, "thumbnails");

            if (!Directory.Exists(thumbnailDir))
                Directory.CreateDirectory(thumbnailDir);

            ThumbnailPath = Path.Combine(thumbnailDir, Name + ".jpg");

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
            return File.Exists(ThumbnailPath);
        }
    }
}
