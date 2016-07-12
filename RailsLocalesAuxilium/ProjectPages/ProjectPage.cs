using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace RailsLocalesAuxilium.ProjectPages
{
    /// <summary>
    /// Interaction logic for ProjectPage.xaml
    /// </summary>
    public abstract class ProjectPage : Page
    {
        private static Dictionary<Type, ProjectPage> _projectPages;

        public static void NavigateTo(Type type)
        {
            if (type == null || !type.IsSubclassOf(typeof(ProjectPage)) || MainPage.Instance == null)
            {
                return;
            }
            if (ProjectPages.ContainsKey(type))
            {
                MainPage.Instance.NavigateToPage(ProjectPages[type]);
            }
        }

        private static Dictionary<Type, ProjectPage> ProjectPages
        {
            get
            {
                if (_projectPages == null)
                {
                    _projectPages = new Dictionary<Type, ProjectPage>();
                    var types = typeof(ProjectPage).Module.Assembly.GetTypes();
                    foreach (var type in types.Where(type => type.IsSubclassOf(typeof(ProjectPage))))
                    {
                        _projectPages.Add(type, (ProjectPage)Activator.CreateInstance(type));
                    }
                }
                return _projectPages;
            }
        }
    }
}
