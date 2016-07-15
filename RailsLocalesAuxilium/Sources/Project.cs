using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using RailsLocalesAuxilium.Sources.ProjectData;

namespace RailsLocalesAuxilium.Sources
{
    public class Project
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<string> LocaleFiles { get; set; }
        [XmlIgnore]
        public List<Locale> ActiveLocales { get; private set; }
        [XmlIgnore]
        public List<Model> AllModelsTranslations
        {
            get
            {
                var models = new List<Model>();
                foreach (var locale in ActiveLocales)
                {
                    foreach (var model in locale.Models)
                    {
                        if (!models.Contains(model))
                        {
                            models.Add(model);
                        }
                        else
                        {
                            var m = models[models.IndexOf(model)];
                            foreach (var t in model.Translations)
                            {
                                if (m.Translations.All(translation => translation.Code != t.Code))
                                {
                                    m.Translations.Add(t);
                                }
                            }
                        }
                    }
                }
                return models;
            }
        }

        public Project()
        {
        }

        private Project(string name, string path, List<string> localeFiles) : this()
        {
            Name = name;
            Path = path;
            LocaleFiles = localeFiles;
        }

        public static Project CreateProject(string name, string path)
        {
            var files = FileHelper.GetLocalesFiles(path);
            var p = new Project(name, path, files);
            Config.Instance.AddProject(p);
            return p;
        }

        public void Load(bool reload = false)
        {
            if (ActiveLocales != null && !reload)
            {
                return;
            }
            var f2 = FileHelper.GetLocalesFiles(Path);
            if (LocaleFiles == null)
            {
                LocaleFiles = f2;
            }
            else
            {
                foreach (var f in f2.Where(f => !LocaleFiles.Contains(f)))
                {
                    LocaleFiles.Add(f);
                }
            }
            ActiveLocales = new List<Locale>();
            if (LocaleFiles != null && LocaleFiles.Count > 0)
            {
                foreach (var l in LocaleFiles)
                {
                    var locale = new Locale() { Models = Model.GetModels(Path, l) };

                    ActiveLocales.Add(locale);
                }
            }
        }
    }
}
