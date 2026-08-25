using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
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

            FooterMenuItems.Add(new NavigationViewItem
            {
                Content = "Setting",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Options16 },
                TargetPageType = typeof(Setting)
            });
        }
    }
}
