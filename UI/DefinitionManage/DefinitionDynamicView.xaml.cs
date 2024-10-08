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
using static MaterialDesignThemes.Wpf.Theme;

namespace UI.DefinitionManage
{
    /// <summary>
    /// DefinitionDynamicView.xaml
    /// </summary>
    public partial class DefinitionDynamicView : UserControl
    {
        public DefinitionDynamicView()
        {
            InitializeComponent();
            DataContext = new DefinitionDynamicViewModel();

            Make(0, 0);
            Make(1, 1);
            Make(2, 2);
            Make(1, 3);
            Make(0, 4);
        }

        private void Make(int row, int column)
        {
            var cell = new TextBlock
            {
                Text = "test",
                FontSize = 50,
                Background = Brushes.Aqua,
            };

            Grid.SetColumn(cell, column);
            Grid.SetRow(cell, row);
            myGrid.Children.Add(cell);
        }
    }
}
