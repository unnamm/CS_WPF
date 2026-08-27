using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Configuration;
using Log;
using System.Collections.ObjectModel;
using View.Container;
using View.Model;
using View.Rapper;
using View.Settings;
using Wpf.Ui.Controls;

namespace View
{
    public partial class MainWindowViewModel : ObservableObject
    {
        readonly AppNavigator _navigator;
        public MenuContainer Menu { get; }

        public ObservableCollection<NavigationViewItem> FooterMenuItems { get; } = [];
        public ObservableCollection<LogEntry> Logs { get; } = [];
        public UserSession Session { get; }

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session, AppNavigator navigator, MenuContainer menuContainer)
        {
            Logs = viewLog.Logs;
            Session = session;
            _navigator = navigator;
            Menu = menuContainer;

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
                if (e.PropertyName == nameof(Session.CurrentUserId))
                    Menu.UpdateMenuState(!string.IsNullOrEmpty(Session.CurrentUserId));
            };
        }

        [RelayCommand]
        void Logout()
        {
            Session.CurrentUserId = null;
            _navigator.Navigate(typeof(Login));
        }
    }
}
