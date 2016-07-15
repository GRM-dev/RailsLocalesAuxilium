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
            ProjectPage.NavigateToProjectPage(typeof(ModelPage));
        }, CanExecute, MainPage.Instance, typeof(ModelPage));

        public static ICommand AttributesCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Attribute Page");
            ProjectPage.NavigateToProjectPage(typeof(AttributePage));
        }, CanExecute, MainPage.Instance, typeof(AttributePage));

        public static ICommand ControllerCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Controller Page");
            ProjectPage.NavigateToProjectPage(typeof(ControllerPage));
        }, CanExecute, MainPage.Instance, typeof(ControllerPage));

        public static ICommand ViewCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateToProjectPage(typeof(ViewPage));
        }, CanExecute, MainPage.Instance, typeof(ViewPage));

        public static ICommand MailerCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateToProjectPage(typeof(MailerPage));
        }, CanExecute, MainPage.Instance, typeof(MailerPage));

        public static ICommand LayoutCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening View Page");
            ProjectPage.NavigateToProjectPage(typeof(LayoutPage));
        }, CanExecute, MainPage.Instance, typeof(LayoutPage));

        public static ICommand LanguageCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Language Page");
            ProjectPage.NavigateToProjectPage(typeof(LanguagePage));
        }, CanExecute, MainPage.Instance, typeof(LanguagePage));

        public static ICommand OtherCommand = CommandHandler.CreateHandlerWithBinding(() =>
        {
            Debug.WriteLine("Opening Other Page");
            ProjectPage.NavigateToProjectPage(typeof(OtherPage));
        }, CanExecute, MainPage.Instance, typeof(OtherPage));

        private static bool CanExecute(object o, CanExecuteRoutedEventArgs e)
        {
            var target = e.Source as Button;
            if ((target == null || o == null) || MainPage.Instance == null) { return false; }
            ProjectPage pp;
            if ((pp = MainPage.Instance.CurrentPage) == null) { return true; }
            if (e.Command.GetType() != typeof(RoutedCommand)) { return true; }
            var c = (RoutedCommand)e.Command;
            if (string.IsNullOrWhiteSpace(c.Name)) { return true; }
            return pp.GetType().Name != c.Name;
        }
    }
}
