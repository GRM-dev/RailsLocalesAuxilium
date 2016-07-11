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
using RailsLocalesAuxilium.Sources;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private bool _canExecute;
        private ICommand _openNewProjectCommand;
        private List<string> _dirRequirements;

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
            while (true)
            {
                Debug.WriteLine("Open New Project");
                var dialog = new System.Windows.Forms.FolderBrowserDialog
                {
                    ShowNewFolderButton = false,
                    Description = "Select Rails Project Folder"
                };
                var result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    var path = dialog.SelectedPath;
                    Uri uri = null;
                    if (string.IsNullOrEmpty(path) || !Uri.TryCreate(path, UriKind.Absolute, out uri))
                    {
                        if (ShowDirDialogAgain()) { continue; }
                    }
                    else
                    {
                        var reqs = new bool[DirRequirements.Count];
                        var i = 0;
                        var goodDir = false;
                        foreach (var entry in Directory.EnumerateFileSystemEntries(path))
                        {
                            if (DirRequirements.Contains(entry.Substring(entry.LastIndexOf("\\", StringComparison.Ordinal)+1)))
                            {
                                reqs[i++] = true;
                            }
                            if (i == reqs.Length)
                            {
                                goodDir = true;
                                break;
                            }
                        }
                        if (!goodDir)
                        {
                            if (ShowDirDialogAgain()) { continue; }
                        }
                        Debug.WriteLine("Appropriate dir selected");
                        if (!Uri.TryCreate(path=(path + "\\config\\locales"), UriKind.RelativeOrAbsolute, out uri))
                        {
                            if (ShowDirDialogAgain())
                            {
                                continue;
                            }
                        }
                        else
                        {
                            Task.Run(() =>
                            {
                                MainWindow.NavigateTo(typeof(MainPage));
                                MainPage.Instance.OpenNewProject(path);
                            });
                        }
                    }
                }
                break;
            }
        }

        private static bool ShowDirDialogAgain()
        {
            var mbResult = MessageBox.Show("Not Ruby on Rails Directory selected or it doesn't contains 'config\\locales' directory. Wanna try again?",
                "Wrong Directory", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            if (mbResult == MessageBoxResult.Yes)
            {
                return true;
            }
            return false;
        }

        public ICommand OpenNewProjectCommand => _openNewProjectCommand ?? (_openNewProjectCommand = new CommandHandler(OpenNewProject, _canExecute));

        public List<string> DirRequirements => _dirRequirements ?? (_dirRequirements = new List<string>()
        {
            "Gemfile",
            "config",
            "app"
        });
    }
}
