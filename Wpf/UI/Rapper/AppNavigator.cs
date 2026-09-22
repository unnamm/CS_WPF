using Wpf.Ui.Controls;

namespace UI.Rapper
{
    public class AppNavigator
    {
        NavigationView? _navigationView;

        public void SetNavigationControl(NavigationView navigationView) => _navigationView = navigationView;
        public void Navigate<T>() where T : System.Windows.Controls.Page
        {
            _navigationView!.Navigate(typeof(T));
        }
    }
}
