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
            ProjectPage.NavigateTo(typeof(ModelPage));
        }, CanExecute, MainPage.Instance, typeof(ModelPage));

        public static ICommand AttributesCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Attribute Page");
            ProjectPage.NavigateTo(typeof(AttributePage));
        }, CanExecute, MainPage.Instance, typeof(AttributePage));

        public static ICommand ControllerCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Controller Page");
            ProjectPage.NavigateTo(typeof(ControllerPage));
        }, CanExecute, MainPage.Instance, typeof(ControllerPage));

        public static ICommand ViewCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateTo(typeof(ViewPage));
        }, CanExecute, MainPage.Instance, typeof(ViewPage));

        public static ICommand LayoutCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateTo(typeof(LayoutPage));
        }, CanExecute, MainPage.Instance, typeof(LayoutPage));

        public static ICommand LanguageCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Language Page");
            ProjectPage.NavigateTo(typeof(LanguagePage));
        }, CanExecute, MainPage.Instance, typeof(LanguagePage));

        public static ICommand OtherCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Other Page");
            ProjectPage.NavigateTo(typeof(OtherPage));
        }, CanExecute, MainPage.Instance, typeof(OtherPage));

        private static bool CanExecute(object o, CanExecuteRoutedEventArgs e)
        {
            var target = e.Source as Button;
            var r1 = target != null && o != null;
            var r2 = MainPage.Instance != null;
            if (!r1 || !r2) { return false; }
            ProjectPage pp;
            if ((pp = MainPage.Instance.CurrentPage) == null) { return true; }
            if (e.Command.GetType() != typeof(RoutedCommand)) { return true; }
            var c = (RoutedCommand)e.Command;
            if (string.IsNullOrWhiteSpace(c.Name)) { return true; }
            return pp.GetType().Name != c.Name;
        }
    }
}
