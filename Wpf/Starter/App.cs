using Configuration.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using View;
using Wpf.Ui.Markup;

namespace Starter
{
    /// <summary>
    /// set app build
    /// </summary>
    internal class App : Application
    {
        readonly IHost? _host;

        public App()
        {
            try
            {
                Resources.MergedDictionaries.Add(new ThemesDictionary());
                Resources.MergedDictionaries.Add(new ControlsDictionary());

                var builder = Host.CreateApplicationBuilder();

                var viewContainer = new ViewContainer(builder.Services);
                viewContainer.AddViewViewModel<MainWindow, MainWindowViewModel>();

                builder.Configuration
                    .AddJsonFile("Config/appsettings.json", false, true)
                    ;
                builder.Logging
                    .AddFilter<Log.FileLoggerProvider>("", LogLevel.Warning)
                    .AddFilter<Log.ViewLoggerProvider>("", LogLevel.Information)
                    ;
                builder.Services
                    .AddHostedService<Run>()
                    .AddSingleton(viewContainer)
                    .AddSingleton<Log.ViewLoggerProvider>()
                    .AddSingleton<Log.FileLoggerProvider>()
                    .AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<Log.ViewLoggerProvider>())
                    .AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<Log.FileLoggerProvider>())
                    .Configure<Appsettings>(builder.Configuration.GetSection("Appsettings"))
                    ;

                _host = builder.Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "build error");
                Shutdown();
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (_host == null)
                return;

            try
            {
                await _host.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "host error");
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }

            base.OnExit(e);
        }
    }
}
