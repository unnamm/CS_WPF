using Common.Message;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UI.View;

namespace Starter
{
    internal class App : Application
    {
        private readonly MainWindowView _mainView;
        private readonly IServiceProvider _serviceProvider;
        private readonly IServiceCollection _servicesCollection;
        private readonly Dictionary<Type, Type> _viewPair = []; //view, viewmodel pair

        public App()
        {
            var builder = Host.CreateApplicationBuilder();
            _servicesCollection = builder.Services;

            #region add
            _servicesCollection.AddSingleton<MainWindowView>();
            #endregion

            _serviceProvider = builder.Build().Services;
            Ioc.Default.ConfigureServices(_serviceProvider);
            _mainView = _serviceProvider.GetService<MainWindowView>()!;

            AutoConnectViewAndViewModel();

            Startup += (x, y) => _mainView.Show(); //mainwindow show

            InitAsync();
        }

        /// <summary>
        /// run after appear mainwindow
        /// </summary>
        private async void InitAsync()
        {
            await WaitShowWindow();
            WeakReferenceMessenger.Default.Send(new MainViewInitMessage());
        }

        /// <summary>
        /// wait appear mainwindow
        /// </summary>
        /// <returns></returns>
        private async Task WaitShowWindow()
        {
            bool active = false;
            while (true)
            {
                await Task.Delay(1);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    active = _mainView.IsActive;
                });

                if (active == true)
                    break;
            }
        }

        /// <summary>
        /// auto connect view and viewmodel
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void AutoConnectViewAndViewModel()
        {
            foreach (var pair in _viewPair)
            {
                var uc = (ContentControl)Ioc.Default.GetService(pair.Key)!;
                if (uc.DataContext != null)
                {
                    throw new Exception($"{uc} is already allocated DataContext");
                }
                uc.DataContext = Ioc.Default.GetService(pair.Value) ?? throw new Exception("viewmodel null");
            }
        }

        /// <summary>
        /// add view and viewmodel
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <typeparam name="ViewModel"></typeparam>
        private void AddViewAndViewModel<View, ViewModel>() where View : ContentControl where ViewModel : class
        {
            _servicesCollection.AddSingleton<View>();
            _servicesCollection.AddSingleton<ViewModel>();

            _viewPair.Add(typeof(View), typeof(ViewModel));
        }

    }
}
