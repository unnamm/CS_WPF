using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace View
{
    public class DashboardViewModel
    {
        public ObservableCollection<Log.LogEntry> LogList { get; set; }

        public DashboardViewModel(Log.ViewLoggerProvider logProvider, ILogger<DashboardViewModel> logger)
        {
            LogList = logProvider.Logs;

            logger.LogInformation("DashboardViewModel");
        }
    }
}
