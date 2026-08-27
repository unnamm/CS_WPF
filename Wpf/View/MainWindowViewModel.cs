using Configuration;
using Log;
using Microsoft.Extensions.Logging;
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

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger, ViewLoggerProvider viewLog, UserSession session)
        {
            Logs = viewLog.Logs;
            Session = session;

            using (logger.BeginScope("print test log"))
            {
                foreach (var level in Enum.GetValues<LogLevel>())
                {
                    logger.Log(level, "log level: {level}", level);
                }
            }

            MenuItems.Add(new NavigationViewItem
            {
                Content = nameof(Dashboard),
                Icon = new SymbolIcon { Symbol = SymbolRegular.Key16 },
                TargetPageType = typeof(Dashboard)
            });
            MenuItems.Add(new NavigationViewItem
            {
                Content = nameof(HomePage),
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home16 },
                TargetPageType = typeof(HomePage)
            });

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
    }
}
