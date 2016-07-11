using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StartPage _startPage;
        private readonly MainPage _mainPage;

        public MainWindow()
        {
            InitializeComponent();
             _startPage = new StartPage();
             _mainPage = new MainPage();
            Instance = this;
            NavigateTo(typeof(StartPage));
        }

        public static void NavigateTo(Type pageType)
        {
            if (pageType == null || !pageType.IsClass || pageType.IsAbstract || !pageType.IsSubclassOf(typeof(Page)))
            {
                return;
            }
            Instance.Dispatcher.BeginInvoke((Action) (() =>
            {
                if (pageType == Instance._startPage.GetType())
                {
                    Instance.MainFrame.Navigate(Instance._startPage);
                    Instance._startPage.OnNavigatedTo();
                }
                else if (pageType == Instance._mainPage.GetType())
                {
                    Instance.MainFrame.Navigate(Instance._mainPage);
                    Instance._mainPage.OnNavigatedTo();
                }
            }));
        }

        public static MainWindow Instance { get; private set; }
    }
}
