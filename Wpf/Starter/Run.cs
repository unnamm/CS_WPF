using Database;
using Log;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using View;

namespace Starter
{
    /// <summary>
    /// host service
    /// </summary>
    internal class Run : BackgroundService
    {
        readonly ILogger _logger;
        readonly MainWindow _main;
        readonly SQLite _db;

        public Run(MainWindow main, ILogger<Run> logger, ViewLoggerProvider viewLog, SQLite db)
        {
            _logger = logger;
            _main = main;
            _db = db;

            viewLog.SetInvoker(Application.Current.Dispatcher.Invoke);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _main.Show();
            await _db.ConnectAsync(cancellationToken);

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1000, stoppingToken);
            _logger.LogInformation("execute");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
