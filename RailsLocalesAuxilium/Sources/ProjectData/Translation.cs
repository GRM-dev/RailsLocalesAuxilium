using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    public class Translation
    {
        public string Code { get; set; }
        public string TranslatedString { get; set; }

        public Translation() { }

        public Translation(string code, string translatedString)
        {
            Code = code;
            TranslatedString = translatedString;
        }

        public Translation Clone()
        {
            return new Translation(Code, TranslatedString);
        }
    }
}
