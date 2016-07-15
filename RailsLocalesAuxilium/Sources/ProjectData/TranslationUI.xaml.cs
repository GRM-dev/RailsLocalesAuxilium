using System;
using System.Collections.Generic;
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

namespace RailsLocalesAuxilium.Sources.ProjectData
{
    /// <summary>
    /// Interaction logic for TranslationUI.xaml
    /// </summary>
    public partial class TranslationUI : UserControl
    {
        private readonly Translation[] _translations;

        public TranslationUI(Translation[] translations)
        {
            _translations = translations;
            InitializeComponent();
            if (_translations == null || _translations.Length == 0)
            {
                Tcode.Visibility = Visibility.Hidden;
            }
            else
            {
                ParseTranslations();
            }
        }

        private void ParseTranslations()
        {
            Tcode.Text = _translations[0].Code;
            for (var i = 0; i < _translations.Length; i++)
            {
                var t = _translations[i];
                MGrid.ColumnDefinitions.Add(new ColumnDefinition());
                var tb = new TextBox { Text = t.TranslatedString };
                Grid.SetColumn(tb, i + 1);
                Grid.SetRow(tb, 1);
                MGrid.Children.Add(tb);

                var tbl = new TextBlock() { Text = t.Lang };
                Grid.SetColumn(tbl, i + 1);
                Grid.SetRow(tbl, 0);
                MGrid.Children.Add(tbl);
            }
        }
    }
}
