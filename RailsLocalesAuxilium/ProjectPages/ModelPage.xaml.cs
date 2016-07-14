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
            //ModelsTree.Template = GetTemplate();
            ModelsTree.ItemsSource = Models;
        }

        private HierarchicalDataTemplate GetTemplate()
        {
            //create the data template
            HierarchicalDataTemplate dataTemplate = new HierarchicalDataTemplate();

            //create stack pane;
            FrameworkElementFactory grid = new FrameworkElementFactory(typeof(Grid));
            grid.Name = "parentStackpanel";
            grid.SetValue(Grid.WidthProperty, Convert.ToDouble(100));
            grid.SetValue(Grid.HeightProperty, Convert.ToDouble(24));
            grid.SetValue(Grid.MarginProperty, new Thickness(2));
            grid.SetValue(Grid.BackgroundProperty, new SolidColorBrush(Colors.LightSkyBlue));

            // Create Image
            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            image.SetValue(Image.MarginProperty, new Thickness(2));
            image.SetValue(Image.WidthProperty, Convert.ToDouble(32));
            image.SetValue(Image.HeightProperty, Convert.ToDouble(24));
            image.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            image.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            image.SetBinding(Image.SourceProperty, new Binding()
            { Path = new PropertyPath("ImageUrl") });

            grid.AppendChild(image);

            // create text
            FrameworkElementFactory label = new FrameworkElementFactory(typeof(TextBlock));
            label.SetBinding(TextBlock.TextProperty,
                new Binding() { Path = new PropertyPath("Name") });
            label.SetValue(TextBlock.MarginProperty, new Thickness(2));
            label.SetValue(TextBlock.FontWeightProperty, FontWeights.Bold);
            label.SetValue(TextBlock.ToolTipProperty, new Binding());

            grid.AppendChild(label);

            dataTemplate.ItemsSource = new Binding("Countries");

            //set the visual tree of the data template
            dataTemplate.VisualTree = grid;

            return dataTemplate;
        }

        private void UpdateModels()
        {
            var models1 = FileHelper.GetFileNames(CurrentProject.Path, FileHelper.GetModelFiles);
            Models.Clear();
            foreach (var model in models1)
            {
                var name = model.Substring(0, model.LastIndexOf(".", StringComparison.Ordinal));
                Models.Add(new Model(name));
            }
            var models2 = YamlParser.Instance.ReadBaseLocaleFile(CurrentProject.Path, "en");
            foreach (var model in models2)
            {
                if (!Models.Contains(model))
                {
                    Models.Add(new Model(model.Name));
                }
                Models[Models.IndexOf(model)].Translations.AddRange(model.Translations);
            }
        }

        public ObservableCollection<Model> Models { get; } = new ObservableCollection<Model>();
    }
}
