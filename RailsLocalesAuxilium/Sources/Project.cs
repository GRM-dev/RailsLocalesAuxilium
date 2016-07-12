using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources
{
    public class Project
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> Files { get; set; }

        public Project()
        {
        }

        private Project(string name, string path, List<string> files) : this()
        {
            Name = name;
            Path = path;
            Files = files;
        }

        public static Project CreateProject(string name, string path)
        {
            var files = FileHelper.GetLocalesFiles(path);
            var p = new Project(name, path, files);
            Config.Instance.AddProject(p);
            return p;
        }
    }
}
