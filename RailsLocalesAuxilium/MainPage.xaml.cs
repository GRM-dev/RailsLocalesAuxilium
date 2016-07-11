using System;
using System.Collections.Generic;
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

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Instance = this;
        }
        
        public void OnNavigatedTo()
        {
            
        }

        public void OpenNewProject(string path)
        {
             Project = ProjectHandler.CreateProject(path);
        }

        public ProjectHandler Project { get; set; }

        public static MainPage Instance { get; private set; }
    }
}
