using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources
{
    public class ProjectHandler
    {
        private string _path;
        private readonly List<string> _files;

        private ProjectHandler(string path, List<string> files)
        {
            _path = path;
            _files = files;
        }

        public static ProjectHandler CreateProject(string path)
        {
            var files = Directory.EnumerateFiles(path).Where(file => file.Contains(".yml")).ToList();
            return new ProjectHandler(path, files);
        }


    }
}
