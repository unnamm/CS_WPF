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

            foreach (var level in Enum.GetValues<LogLevel>())
            {
                _logger.Log(level, "log level={level}", level);
            }
        }
    }
}
