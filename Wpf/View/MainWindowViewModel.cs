using Configuration;
using Log;
using System.Collections.ObjectModel;
using View.Model;
using View.Settings;
using Wpf.Ui.Controls;

namespace View
{
    public class MainWindowViewModel
    {
        public ObservableCollection<object> MenuItems { get; } = [];
        public ObservableCollection<object> FooterMenuItems { get; } = [];
        public ObservableCollection<LogEntry> Logs { get; } = [];
        public UserSession Session { get; }

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session)
        {
            Logs = viewLog.Logs;
            Session = session;

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
    }
}
