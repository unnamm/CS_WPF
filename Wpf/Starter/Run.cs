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
        readonly LoadingWindow _loading;
        readonly SQLite _db;

        public Run(MainWindow main, LoadingWindow loading, ILogger<Run> logger, ViewLoggerProvider viewLog, SQLite db)
        {
            _logger = logger;
            _main = main;
            _loading = loading;
            _db = db;

            viewLog.SetInvoker(Application.Current.Dispatcher.Invoke);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                _loading.Show();
                _loading.SetStatus("connecting database...");
                await _db.ConnectAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("StartAsync() {ex.Message}", ex.Message);
            }

            _loading.Close();
            _main.Show();

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
