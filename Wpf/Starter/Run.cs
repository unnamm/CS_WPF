using Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using View;
using View.Container;
using Wpf.Ui.Controls;

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
        readonly DeviceTracker _deviceTracker;

        public Run(MainWindow main, ILogger<Run> logger, Log.ViewLoggerProvider viewLog, SQLite db, MenuContainer mc, DeviceTracker dt)
        {
            _logger = logger;
            _main = main;
            _db = db;
            _deviceTracker = dt;

            viewLog.SetInvoker(Application.Current.Dispatcher.Invoke);

            mc.AddMenu<Login>(SymbolRegular.Key16, true);
            mc.AddMenu<HomePage>(SymbolRegular.Home16);
            mc.UpdateMenuState(false);
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var loadingWindow = new LoadingWindow();
            try
            {
                loadingWindow.Show();
                loadingWindow.SetStatus("connecting database...");
                await _db.ConnectAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("StartAsync() {ex.Message}", ex.Message);
            }

            loadingWindow.Close();
            _main.Closing += OnMainClosing;
            _main.Show();

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                try
                {
                    await Task.Delay(1000, stoppingToken);
                    _deviceTracker.Update();
                }
                catch (TaskCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError("ExecuteAsync() error: {m}", ex.Message);
                    await Task.Delay(5000, stoppingToken);
                }
            }
        }

        async void OnMainClosing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_main.Visibility != Visibility.Visible)
            {
                return;
            }

            try
            {
                e.Cancel = true;
                _main.Hide();
                var loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                loadingWindow.SetStatus("disposing database...");
                _db.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError("StopAsync() error: {m}", ex.Message);
            }

            Application.Current.Shutdown();
        }
    }
}
