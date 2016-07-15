using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using RailsLocalesAuxilium.Sources.ProjectData;

namespace RailsLocalesAuxilium.ProjectPages
{
    /// <summary>
    /// Interaction logic for ModelPage.xaml
    /// </summary>
    public partial class ModelPage : ProjectPage
    {
        public ModelPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public override void OnNavigatedTo()
        {
            UpdateModels();
            UpdateView();
        }

        private void UpdateView()
        {
            ModelsTree.Children.Clear();
            foreach (var model in Models)
            {
                var exp = new Expander()
                {
                    Name = model.Name + "_Expander",
                    Header = model.Name,
                    IsExpanded = true
                };
                var list = new StackPanel();
                var mts = model.SortedTranslations;
                var d=new Dictionary<string,List<Translation>>();
                foreach (var translation in mts)
                {
                    if (!d.ContainsKey(translation.Code))
                    {
                        d.Add(translation.Code,new List<Translation>());
                    }
                    d[translation.Code].Add(translation);
                }
                foreach (var t in mts)
                {
                    var ts = mts.Where(t2 => t2.Code == t.Code).ToArray();
                    var tUi = new TranslationUI(ts);
                    list.Children.Add(tUi);
                }
                exp.Content = list;
                ModelsTree.Children.Add(exp);
            }
        }
        
        private void UpdateModels()
        {
            var models = CurrentProject.AllModelsTranslations;
            Models.Clear();
            Models.AddRange(models.OrderBy(m => m.Name).ToList());
        }

        public List<Model> Models { get; } = new List<Model>();
    }
}
