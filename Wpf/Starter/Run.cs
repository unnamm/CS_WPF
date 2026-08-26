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
        readonly MainWindow _main;
        readonly ILogger _logger;

        public Run(MainWindow main, ILogger<Run> logger, ViewLoggerProvider viewLog)
        {
            _main = main;
            _logger = logger;

            viewLog.SetInvoker(Application.Current.Dispatcher.Invoke);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _main.Show();

            return base.StartAsync(cancellationToken);
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
