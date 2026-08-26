using Log;
using Microsoft.Extensions.DependencyInjection;
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
        readonly IServiceProvider _service;

        public Run(MainWindow main, ILogger<Run> logger, ViewLoggerProvider viewLog, IServiceProvider service)
        {
            _logger = logger;
            _service = service;

            viewLog.SetInvoker(Application.Current.Dispatcher.Invoke);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _service.GetRequiredService<MainWindow>().Show();
            var db = _service.GetRequiredService<Database.SQLite>();
            await db.ConnectAsync(cancellationToken);

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
