using Configuration;
using Configuration.Config;
using Log;
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

                builder.Configuration
                    .AddJsonFile(Appsettings.FilePath, false, true)
                    .AddJsonFile(DbSettings.FilePath, false, true)
                    ;
                builder.Logging
                    .AddFilter<FileLoggerProvider>("", LogLevel.Warning)
                    .AddFilter<ViewLoggerProvider>("", LogLevel.Information)
                    ;
                builder.Services
                    .AddHostedService<Run>()
                    .Configure<Appsettings>(builder.Configuration.GetSection(Appsettings.SectionName))
                    .Configure<DbSettings>(builder.Configuration.GetSection(DbSettings.SectionName))
                    .AddSingleton<ViewLoggerProvider>()
                    .AddSingleton<FileLoggerProvider>()
                    .AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<ViewLoggerProvider>())
                    .AddSingleton<ILoggerProvider>(sp => sp.GetRequiredService<FileLoggerProvider>())
                    .AddSingleton<MainWindow>().AddSingleton<MainWindowViewModel>()
                    .AddSingleton<Dashboard>().AddSingleton<DashboardViewModel>()
                    .AddSingleton<Database.SQLite>()
                    ;

                foreach (var configType in ConfigSectionRegistry.All)
                {
                    builder.Services.AddSingleton(typeof(View.Settings.SettingSectionPage<>).MakeGenericType(configType));
                }
                _host = builder.Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "build error");
                this.Shutdown();
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
                this.Shutdown();
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
