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
        private readonly List<Type> _menuViews = [];
        private readonly Dictionary<Type, Type> _viewPairs = [];

        public App()
        {
            var builder = Host.CreateApplicationBuilder();
            _servicesCollection = builder.Services;

            #region add
            _servicesCollection.AddSingleton<Log>();
            _servicesCollection.AddSingleton<Flow>();
            _servicesCollection.AddSingleton<DataYaml>();
            _servicesCollection.AddSingleton<DialogView>();

            AddViewAndViewModel<ContentView, ContentViewModel>(true);
            AddViewAndViewModel<SettingView, SettingViewModel>(true);
            AddViewAndViewModel<MainWindowView, MainWindowViewModel>();
            #endregion

            _serviceProvider = builder.Build().Services;
            Ioc.Default.ConfigureServices(_serviceProvider);

            _mainView = _serviceProvider.GetService<MainWindowView>()!;

            AutoConnectViewAndViewModel();
            _serviceProvider.GetService<Flow>(); //make instance, receive message
            _serviceProvider.GetService<DialogView>(); //make instance, receive message

            Startup += (x, y) => _mainView.Show(); //mainwindow show
        }

        /// <summary>
        /// auto connect view and viewmodel
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void AutoConnectViewAndViewModel()
        {
            foreach (var pair in _viewPairs)
            {
                var cc = (ContentControl)_serviceProvider.GetService(pair.Key)!;
                cc.DataContext = _serviceProvider.GetService(pair.Value) ?? throw new Exception("viewmodel null");
            }

            var mainVM = _serviceProvider.GetService<MainWindowViewModel>()!;
            foreach (var view in _menuViews)
            {
                var cc = (ContentControl)_serviceProvider.GetService(view)!;
                mainVM.MenuItems.Add(new UI.Model.ItemMenu(view.Name.Replace("View", ""), cc));
            }
            mainVM.SelectedItem = mainVM.MenuItems.First(); //set default content
        }

        /// <summary>
        /// add view and viewmodel
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <typeparam name="ViewModel"></typeparam>
        /// <param name="isShowMenuList">show menu list</param>
        private void AddViewAndViewModel<View, ViewModel>(bool isShowMenuList = false) where View : ContentControl where ViewModel : class
        {
            _viewPairs.Add(typeof(View), typeof(ViewModel));

            _servicesCollection.AddSingleton<View>();
            _servicesCollection.AddSingleton<ViewModel>();

            if (isShowMenuList)
            {
                _menuViews.Add(typeof(View));
            }
        }

    }
}
