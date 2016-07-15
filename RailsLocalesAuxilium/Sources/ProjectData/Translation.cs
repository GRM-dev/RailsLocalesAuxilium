using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    public class Translation
    {
        public string Code { get; }
        public string Lang { get; }
        public string TranslatedString { get; set; }

        public Translation() { }

        public Translation(string code, string translatedString, string lang)
        {
            Code = code;
            TranslatedString = translatedString;
            Lang = lang;
        }

        public Translation Clone()
        {
            return new Translation(Code, TranslatedString, Lang);
        }

        public override bool Equals(object obj)
        {
            var t = (Translation)obj;
            if (t?.Code == null)
            {
                return false;
            }
            return Code == t.Code && Lang == t.Lang;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Code?.GetHashCode() ?? 0) * 397) ^ (Lang?.GetHashCode() ?? 0);
            }
        }

        public override string ToString()
        {
            return $"Code: {Code}, Lang: {Lang}, Translation: {TranslatedString}";
        }
    }
}
