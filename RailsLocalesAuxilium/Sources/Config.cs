using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace RailsLocalesAuxilium.Sources
{
    public class Config
    {
        private const string filename = "config.xml";
        private static Config _instance;
        private static XmlSerializer _ser;
        private static object _serLock = new object();

        private Config()
        {
            Projects = new List<Project>();
        }

        private static void Read()
        {
            if (File.Exists(filename))
            {
                lock (_serLock)
                {
                    using (var s = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        _instance = (Config)_ser.Deserialize(s);
                    }
                }
            }
        }

        private static void Write()
        {
            lock (_serLock)
            {
                if (_instance == null)
                {
                    _instance = new Config();
                }
                using (var s = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    _ser.Serialize(s, _instance);
                }
            }
        }

        public void AddProject(Project project)
        {
            if (Exists(project))
            {
                MessageBox.Show("Project already exists so opening it ...", "Opening project", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                Projects.Add(project);
                Write();
            }
        }

        public bool Exists(Project project)
        {
            return Projects.Contains(project) || Projects.Any(p => p.Path == project.Path);
        }

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    _ser = new XmlSerializer(typeof(Config));
                    Read();
                    Write();
                }
                return _instance;
            }
        }

        public List<Project> Projects { get; set; }
    }
}
