using Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.ObjectModel;
using View.Settings;
using Wpf.Ui.Controls;

namespace View
{
    public class MainWindowViewModel
    {
        public ObservableCollection<object> MenuItems { get; set; } = [];
        public ObservableCollection<object> FooterMenuItems { get; set; } = [];

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger)
        {
            using (logger.BeginScope("print test log"))
            {
                foreach (var level in Enum.GetValues<LogLevel>())
                {
                    logger.Log(level, "log level: {level}", level);
                }
            }

            MenuItems.Add(new NavigationViewItem
            {
                Content = "Dashboard",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home16 },
                TargetPageType = typeof(Dashboard)
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
