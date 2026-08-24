using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            DataContext = vm;
            RootNavigation.SetServiceProvider(serviceProvider);
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var item = (NavigationViewItem)RootNavigation.MenuItems[0]!;
            RootNavigation.Navigate(item.TargetPageType!);
        }
    }
}
