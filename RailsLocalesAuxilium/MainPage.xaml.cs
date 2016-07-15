using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RailsLocalesAuxilium.ProjectPages;
using RailsLocalesAuxilium.Sources;

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {

        public MainPage()
        {
            Instance = this;
            InitializeComponent();
        }

        public void OnNavigatedTo()
        {
            if (Project == null)
            {
                return;
            }
            Project.Load();
        }
        
        public void OpenProject(Project project = null)
        {
            if (project != null)
            {
                Debug.WriteLine("Opening project: " + project.Path);
                Project = project;
            }
            if (Project != null)
            {
                MainWindow.NavigateTo(typeof(MainPage));
            }
        }

        public void NavigateToProjectPage(ProjectPage page)
        {
            if (page == null)
            {
                return;
            }
            ProjectFrame.Navigate(page);
            CurrentPage = page;
            page.OnNavigatedTo();
        }

        public Project Project { get; private set; }
        public static MainPage Instance { get; private set; }
        public ProjectPage CurrentPage { get; private set; }
    }
}
