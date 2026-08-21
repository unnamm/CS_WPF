using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace View
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Log.LogEntry> LogList { get; set; }

        readonly ILogger<MainWindowViewModel> _logger;

        public MainWindowViewModel(ILogger<MainWindowViewModel> logger, Log.ViewLoggerProvider logProvider)
        {
            LogList = logProvider.Logs;
            _logger = logger;

            _logger.LogInformation("info");
            _logger.LogWarning("warning");
            _logger.LogError("error");
            _logger.LogCritical("critical");
        }
    }
}
