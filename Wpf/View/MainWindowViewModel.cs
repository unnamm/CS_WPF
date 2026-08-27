using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Configuration;
using Log;
using System.Collections.ObjectModel;
using View.Model;
using View.Rapper;
using View.Settings;
using Wpf.Ui.Controls;

namespace View
{
    public partial class MainWindowViewModel : ObservableObject
    {
        readonly AppNavigator _navigator;
        readonly NavigationViewItem _dashboardMenuItem;
        readonly NavigationViewItem _homeMenuItem;

        public ObservableCollection<object> MenuItems { get; } = [];
        public ObservableCollection<object> FooterMenuItems { get; } = [];
        public ObservableCollection<LogEntry> Logs { get; } = [];
        public UserSession Session { get; }

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session, AppNavigator navigator)
        {
            Logs = viewLog.Logs;
            Session = session;
            _navigator = navigator;

            _dashboardMenuItem = AddMenu<Dashboard>(SymbolRegular.Key16);
            _homeMenuItem = AddMenu<HomePage>(SymbolRegular.Home16);

            foreach (var configType in ConfigSectionRegistry.All)
            {
                FooterMenuItems.Add(new NavigationViewItem
                {
                    Content = configType.Name,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(SettingSectionPage<>).MakeGenericType(configType)
                });
            }

            Session.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Session.CurrentUserId)) UpdateMenuState();
            };
            UpdateMenuState();
        }

        NavigationViewItem AddMenu<T>(SymbolRegular icon) where T : System.Windows.Controls.Page
        {
            var item = new NavigationViewItem
            {
                Content = typeof(T).Name,
                Icon = new SymbolIcon { Symbol = icon },
                TargetPageType = typeof(T)
            };
            MenuItems.Add(item);
            return item;
        }

        void UpdateMenuState()
        {
            var loggedIn = !string.IsNullOrEmpty(Session.CurrentUserId);
            _dashboardMenuItem.IsEnabled = !loggedIn;
            _homeMenuItem.IsEnabled = loggedIn;
        }

        [RelayCommand]
        void Logout()
        {
            Session.CurrentUserId = null;
            _navigator.Navigate(typeof(Dashboard));
        }
    }
}
