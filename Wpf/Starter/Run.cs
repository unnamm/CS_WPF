using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using View;

namespace Starter
{
    internal class Run : BackgroundService
    {
        readonly MainWindow _main;

        public Run(ViewContainer vc, IServiceProvider sp, MainWindow main)
        {
            _main = main;
            vc.ConnectContext(sp);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Application.Current.Dispatcher.Invoke(_main.Show);

            return Task.CompletedTask;
        }
    }
}
