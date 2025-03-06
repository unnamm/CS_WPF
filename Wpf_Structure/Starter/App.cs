using Common;
using Common.Config;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sequence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UI.View;
using UI.ViewModel;

namespace Starter
{
    internal class App : Application
    {
        private readonly MainWindowView _mainView;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _servicesCollection;
        private readonly Dictionary<Type, Type?> _viewPair = [];

        public App()
        {
            var builder = Host.CreateApplicationBuilder();
            _servicesCollection = builder.Services;

            #region add
            _servicesCollection.AddSingleton<Log>();
            _servicesCollection.AddSingleton<Flow>();
            _servicesCollection.AddSingleton<DataConfig>();
            _servicesCollection.AddSingleton<DialogView>();

            AddViewAndViewModel<ContentView, ContentViewModel>();
            AddViewAndViewModel<MainWindowView, MainWindowViewModel>();
            #endregion

            _serviceProvider = builder.Build().Services;
            Ioc.Default.ConfigureServices(_serviceProvider);

            _mainView = _serviceProvider.GetService<MainWindowView>()!;

            AutoConnectViewAndViewModel();
            _serviceProvider.GetService<Flow>(); //instance
            _serviceProvider.GetService<DialogView>(); //instance

            Startup += (x, y) => _mainView.Show(); //mainwindow show
        }

        /// <summary>
        /// auto connect view and viewmodel
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void AutoConnectViewAndViewModel()
        {
            foreach (var pair in _viewPair)
            {
                var uc = (ContentControl)_serviceProvider.GetService(pair.Key)!;
                uc.DataContext = _serviceProvider.GetService(pair.Value!) ?? throw new Exception("viewmodel null");
            }
        }

        /// <summary>
        /// add view and viewmodel
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <typeparam name="ViewModel"></typeparam>
        private void AddViewAndViewModel<View, ViewModel>() where View : ContentControl where ViewModel : class
        {
            _viewPair.Add(typeof(View), typeof(ViewModel));

            _servicesCollection.AddSingleton<View>();
            _servicesCollection.AddSingleton<ViewModel>();
        }

    }
}
