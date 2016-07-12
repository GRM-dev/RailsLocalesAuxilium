using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources
{
    public static class FileHelper
    {
        public static List<string> GetLocalesFiles(string path)
        {
            return Directory.EnumerateFiles(path + "\\config\\locales").Where(file => file.Contains(".yml")).ToList();
        }

        public static List<string> GetModelFiles(string path)
        {
            return null;
        }
    }
}
