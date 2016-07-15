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
            return GetListOfFilesInDir(path + "\\config\\locales", ".yml", false);
        }

        public static List<string> GetModelFiles(string path)
        {
            return GetListOfFilesInDir(path + "\\app\\models", ".rb");
        }

        public static List<string> GetControllerFiles(string path)
        {
            return GetListOfFilesInDir(path + "\\app\\controllers", ".rb");
        }

        public static List<string> GetViewFiles(string path)
        {
            return GetListOfFilesInDir(path + "\\app\\views", ".rb");
        }

        public static List<string> GetLayoutFiles(string path)
        {
            return GetListOfFilesInDir(path + "\\app\\views\\layouts", ".rb");
        }

        public static List<string> GetFileNames(string path, Func<string, List<string>> method)
        {
            var r = method(path);
            return r.Select(Path.GetFileName).Where(filename => !string.IsNullOrWhiteSpace(filename)).ToList();
        }

        private static List<string> GetListOfFilesInDir(string path, string fileType = ".", bool recursive = true)
        {
            IEnumerable<string> content;
            if (recursive)
            {
                content = Directory.EnumerateFileSystemEntries(path);
            }
            else
            {
                content = Directory.EnumerateFiles(path);
            }
            return content.Where(file => file.Contains(fileType)).ToList();
        }

        public static FileStream GetLocaleFile(string localeFilePath)
        {
            return new FileStream(localeFilePath, FileMode.Open, FileAccess.Read);
        }
    }
}
