using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Model> ReadBaseLocaleFile(string path, string defName)
        {
            var des = new Deserializer(namingConvention: new PascalCaseNamingConvention());
            var sr = FileHelper.GetLocaleFile(path, defName);
            var r = des.Deserialize<Dictionary<string, Dictionary<string, dynamic>>>(new StreamReader(sr, Encoding.UTF8))[defName];
            var list = new List<Model>();
            var models = (Dictionary<object, object>)r["activerecord"]["models"];
            foreach (var m in models)
            {
                var values = ((Dictionary<object,object>)m.Value).ToDictionary(p => (string) p.Key, p => (string) p.Value);
                var model=new Model((string)m.Key, values);
                list.Add(model);
            }
            return list;
        }

        public static YamlParser Instance => _instance ?? (_instance = new YamlParser());
    }
}
