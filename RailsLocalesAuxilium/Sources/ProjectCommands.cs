using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace RailsLocalesAuxilium.Sources
{
    public static class ProjectCommands
    {
        public static ICommand ModelCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Model Page");
        }, (o, e) =>
        {
            var target = e.Source as Button;
            return target != null && o != null;
        }, MainPage.Instance);



    }
}
