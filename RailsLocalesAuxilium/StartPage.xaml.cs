using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private bool _canExecute;
        private ICommand _openNewProjectCommand;

        public StartPage()
        {
            InitializeComponent();
            DataContext = this;
            _canExecute = true;
        }

        public void OnNavigatedTo()
        {
            ProjectsPanel.Children.Add(new ExistingProjectButton("Project 1", null));
        }

        private void OpenNewProject()
        {
            Debug.WriteLine("Open New Project");
        }

        public ICommand OpenNewProjectCommand => _openNewProjectCommand ?? (_openNewProjectCommand = new CommandHandler(OpenNewProject, _canExecute));
    }
}
