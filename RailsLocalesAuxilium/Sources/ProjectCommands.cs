using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using RailsLocalesAuxilium.ProjectPages;

namespace RailsLocalesAuxilium.Sources
{
    public static class ProjectCommands
    {
        public static ICommand ModelCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Model Page");
            ProjectPage.NavigateTo(typeof (ModelPage));
        }, CanExecute, MainPage.Instance);

        private static bool CanExecute(object o, CanExecuteRoutedEventArgs e)
        {
            var target = e.Source as Button;
            return target != null && o != null && MainPage.Instance != null;
        }
    }
}
