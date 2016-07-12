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

        public static ICommand AttributesCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Attribute Page");
            ProjectPage.NavigateTo(typeof(AttributePage));
        }, CanExecute, MainPage.Instance);

        public static ICommand ControllerCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Controller Page");
            ProjectPage.NavigateTo(typeof(ControllerPage));
        }, CanExecute, MainPage.Instance);

        public static ICommand ViewCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateTo(typeof(ViewPage));
        }, CanExecute, MainPage.Instance);

        public static ICommand LanguageCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Language Page");
            ProjectPage.NavigateTo(typeof(LanguagePage));
        }, CanExecute, MainPage.Instance);

        public static ICommand OtherCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Other Page");
            ProjectPage.NavigateTo(typeof(OtherPage));
        }, CanExecute, MainPage.Instance);

        private static bool CanExecute(object o, CanExecuteRoutedEventArgs e)
        {
            var target = e.Source as Button;
            return target != null && o != null && MainPage.Instance != null;
        }
    }
}
