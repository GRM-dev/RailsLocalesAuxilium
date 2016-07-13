using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    public class Locale
    {
        public List<Model> Models { get; set; } 
        public List<Attributes> Attributes{ get; set; } 
        public List<Layout> Layouts{ get; set; } 
    }
}
