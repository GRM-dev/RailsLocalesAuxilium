using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    public class Model
    {
        public string Name { get; set; }
        public List<Translation> Translations { get; } = new List<Translation>();
        public ObservableCollection<Translation> TranslationCollection => new ObservableCollection<Translation>(Translations);

        public Model(string name)
        {
            Name = name;
        }

        public Model(string name, Dictionary<string, string> translations) : this(name)
        {
            foreach (var tPair in translations)
            {
                Translations.Add(new Translation(tPair.Key, tPair.Value));
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
    }
}