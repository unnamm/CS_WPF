using Wpf.Ui.Controls;

namespace View.Rapper
{
    public class AppNavigator
    {
        NavigationView? _navigationView;

        public void SetNavigationControl(NavigationView navigationView) => _navigationView = navigationView;
        public void Navigate(Type pageType) => _navigationView!.Navigate(pageType);
    }
}
