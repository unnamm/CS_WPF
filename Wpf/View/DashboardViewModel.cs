using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    public class DashboardViewModel : INavigationAware
    {
        public ObservableCollection<Log.LogEntry> LogList { get; set; }

        readonly ILogger _logger;

        public DashboardViewModel(Log.ViewLoggerProvider logProvider, ILogger<DashboardViewModel> logger)
        {
            LogList = logProvider.Logs;
            _logger = logger;
            logger.LogInformation("DashboardViewModel");
        }

        public Task OnNavigatedToAsync()
        {
            _logger.LogInformation("open DashboardViewModel");
            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync()
        {
            _logger.LogInformation("close DashboardViewModel");
            return Task.CompletedTask;
        }
    }
}
