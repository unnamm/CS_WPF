using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace UI.DataGridManage
{
    /// <summary>
    /// DataGridView.xaml
    /// reuse view
    /// </summary>
    public partial class DataGridView : UserControl
    {
        //add new view property
        public static DependencyProperty ItemsSourceMyProperty =
            DependencyProperty.Register("ItemsSourceMy", typeof(IEnumerable), typeof(DataGridView));
        public IEnumerable ItemsSourceMy
        {
            get => (IEnumerable)GetValue(ItemsSourceMyProperty);
            set => SetValue(ItemsSourceMyProperty, value);
        }

        public DataGridView()
        {
            InitializeComponent();
        }
    }
}
