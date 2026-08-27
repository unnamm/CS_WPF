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

        public ObservableCollection<object> MenuItems { get; } = [];
        public ObservableCollection<object> FooterMenuItems { get; } = [];
        public ObservableCollection<LogEntry> Logs { get; } = [];
        public UserSession Session { get; }

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session, AppNavigator navigator)
        {
            Logs = viewLog.Logs;
            Session = session;
            _navigator = navigator;

            AddMenu<Dashboard>(SymbolRegular.Key16);
            AddMenu<HomePage>(SymbolRegular.Home16);

            foreach (var configType in ConfigSectionRegistry.All)
            {
                FooterMenuItems.Add(new NavigationViewItem
                {
                    Content = configType.Name,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(SettingSectionPage<>).MakeGenericType(configType)
                });
            }
        }

        void AddMenu<T>(SymbolRegular icon) where T : System.Windows.Controls.Page
        {
            MenuItems.Add(new NavigationViewItem
            {
                Content = nameof(T),
                Icon = new SymbolIcon { Symbol = icon },
                TargetPageType = typeof(T)
            });
        }

        [RelayCommand]
        void Logout()
        {
            Session.CurrentUserId = null;
            _navigator.Navigate(typeof(Dashboard));
        }
    }
}
