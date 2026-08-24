using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    public class DashboardViewModel : NavigationAware
    {
        public ObservableCollection<Log.LogEntry> LogList { get; set; }

        readonly ILogger _logger;

        public DashboardViewModel(Log.ViewLoggerProvider logProvider, ILogger<DashboardViewModel> logger)
        {
            LogList = logProvider.Logs;
            _logger = logger;
            logger.LogInformation("instance DashboardViewModel");
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            _logger.LogInformation("to DashboardViewModel");
        }

        public override void OnNavigatedFrom()
        {
            base.OnNavigatedFrom();
            _logger.LogInformation("from DashboardViewModel");
        }

        public override async Task OnNavigatedToAsync()
        {
            await base.OnNavigatedToAsync();
            await Task.Delay(1000);
            _logger.LogInformation("to delay DashboardViewModel");
        }

        public override async Task OnNavigatedFromAsync()
        {
            await base.OnNavigatedFromAsync();
            await Task.Delay(1000);
            _logger.LogInformation("from delay DashboardViewModel");
        }
    }
}
