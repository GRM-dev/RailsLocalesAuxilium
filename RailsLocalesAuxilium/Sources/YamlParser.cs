using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RailsLocalesAuxilium.Sources.ProjectData;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RailsLocalesAuxilium.Sources
{
    public class YamlParser
    {
        private static YamlParser _instance;

        private YamlParser()
        {

        }

        public List<Model> ReadBaseLocaleFile(string localeFilePath)
        {
            var des = new Deserializer(namingConvention: new PascalCaseNamingConvention());
            var sr = FileHelper.GetLocaleFile(localeFilePath);
            var l = Path.GetFileNameWithoutExtension(localeFilePath);
            try
            {
                var r =
                    des.Deserialize<Dictionary<string, Dictionary<string, dynamic>>>(new StreamReader(sr, Encoding.UTF8))[l];
                var list = new List<Model>();
                var models = (Dictionary<object, object>)r["activerecord"]["models"];
                foreach (var m in models)
                {
                    var values = ((Dictionary<object, object>)m.Value).ToDictionary(p => (string)p.Key, p => (string)p.Value);
                    var model = new Model((string)m.Key, values, l);
                    list.Add(model);
                }
                return list;
            }
            catch (KeyNotFoundException)
            {
                MessageBox.Show($"Could not load locale file: {l} from {localeFilePath} cause inner key do not fit file name", "YAML file load error");
            }
            return null;
        }

        public static YamlParser Instance => _instance ?? (_instance = new YamlParser());
    }
}
