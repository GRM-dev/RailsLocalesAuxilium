using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    public class Model
    {
        public string Name { get; }
        public List<Translation> Translations { get; } = new List<Translation>();
        public List<Translation> SortedTranslations => Translations.OrderBy(t => t.Code).ToList();

        public Model(string name)
        {
            Name = name;
        }

        public Model(string name, Dictionary<string, string> translations, string lang) : this(name)
        {
            foreach (var tPair in translations)
            {
                Translations.Add(new Translation(tPair.Key, tPair.Value, lang));
            }
        }

        public override bool Equals(object obj)
        {
            var m = (Model)obj;
            return m?.Name != null && Equals(m);
        }

        public bool Equals(Model other)
        {
            return string.Equals(Name, other.Name);
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }

        public static List<Model> GetModels(string path, string localePath)
        {
            var models = new List<Model>();
            var models1 = FileHelper.GetFileNames(path, FileHelper.GetModelFiles);
            foreach (var model in models1)
            {
                var name = model.Substring(0, model.LastIndexOf(".", StringComparison.Ordinal));
                models.Add(new Model(name));
            }
            var models2 = YamlParser.Instance.ReadBaseLocaleFile(localePath);
            if (models2 != null)
            {
                foreach (var model in models2)
                {
                    if (!models.Contains(model))
                    {
                        models.Add(new Model(model.Name));
                    }
                    models[models.IndexOf(model)].Translations.AddRange(model.Translations);
                }
            }
            return models;
        }


    }
}