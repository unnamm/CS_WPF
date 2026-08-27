using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using View.Rapper;
using Wpf.Ui.Controls;

namespace View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel vm, IServiceProvider serviceProvider, AppNavigator navigator)
        {
            InitializeComponent();
            DataContext = vm;
            RootNavigation.SetServiceProvider(serviceProvider); //Navigation use serviceprovider
            navigator.SetNavigationControl(RootNavigation); // set navigation
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var item = RootNavigation.MenuItems[0] as NavigationViewItem ?? throw new NullReferenceException();
            RootNavigation.Navigate(item.TargetPageType!);
        }
    }
}
