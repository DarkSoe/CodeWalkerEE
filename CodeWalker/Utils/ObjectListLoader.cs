using System;
using System.Collections.Generic;
using System.IO;

namespace CodeWalker.Utils
{
    public static class ObjectListLoader
    {
        public static List<string> Load(string baseDirectory = null)
        {
            var dir = baseDirectory ?? AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(dir, "ObjectList.ini");

            if (!File.Exists(path))
                return new List<string>();

            var list = new List<string>();
            foreach (var line in File.ReadAllLines(path))
            {
                var name = line.Trim();
                if (string.IsNullOrEmpty(name) || name.StartsWith(";") || name.StartsWith("#"))
                    continue;

                var commaIndex = name.IndexOf(',');
                if (commaIndex >= 0)
                    name = name.Substring(0, commaIndex).Trim();

                if (!string.IsNullOrEmpty(name))
                    list.Add(name);
            }

            return list;
        }
    }
}
