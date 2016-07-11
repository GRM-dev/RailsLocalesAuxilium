using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using RailsLocalesAuxilium.Annotations;
using RailsLocalesAuxilium.Sources;

namespace RailsLocalesAuxilium
{
    /// <summary>
    /// Interaction logic for ExistingProjectButton.xaml
    /// </summary>
    public partial class ExistingProjectButton : UserControl, INotifyPropertyChanged
    {
        private readonly Uri _projectUri;
        private string _buttonText = "Project";
        private ICommand _openProjectCommand;
        private bool _canExecute;

        public ExistingProjectButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ExistingProjectButton(string projectName, Uri projectUri) : this()
        {
            _projectUri = projectUri;
            ButtonText = projectName;
            _canExecute = true;
        }


        public ICommand OpenProjectCommand => _openProjectCommand??(_openProjectCommand=new CommandHandler(OpenProject, _canExecute));

        private void OpenProject()
        {
            Debug.WriteLine("Opening project: "+_projectUri?.AbsolutePath);
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (value != null)
                {
                    _buttonText = value;
                }
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
