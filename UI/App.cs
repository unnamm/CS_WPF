using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Controls;
using UI.BindingManage;
using UI.CollectionManage;
using UI.ComboboxManage;
using UI.ConvertManage;
using UI.DataGridManage;
using UI.DefinitionManage;
using UI.DesignManage;
using UI.ItemsControlManage;
using UI.LatexManage;
using UI.MessageManage;
using UI.StyleManage;

namespace UI
{
    internal class App : Application
    {
        private readonly MainWindow _mainView;
        private readonly List<Type> _viewList = [];
        private readonly IServiceCollection _services;
        private readonly Dictionary<Type, Type> _viewPair = [];

        public App()
        {
            var builder = Host.CreateApplicationBuilder();
            _services = builder.Services;

            #region host build
            AddViewAndViewModel<MainWindow, MainWindowViewModel>(); //main

            //add tab view and viewmodel
            AddViewAndViewModel<BindingView, BindingViewModel>();
            AddViewAndViewModel<CollectView, CollectViewModel>();
            AddViewAndViewModel<ComboboxView, ComboboxViewModel>();
            AddViewAndViewModel<ConvertView, ConvertViewModel>();
            AddViewAndViewModel<DataGridManageView, DataGridManageViewModel>();
            AddViewAndViewModel<DefinitionDynamicView, DefinitionDynamicViewModel>();
            AddViewAndViewModel<ItemsControlView, ItemsControlViewModel>();
            AddViewAndViewModel<LatexView, LatexViewModel>();
            AddViewAndViewModel<MessageView, MessageViewModel>();

            //add tab view
            AddView<DesignView>();
            AddView<StyleView>();
            #endregion

            var host = builder.Build();
            Ioc.Default.ConfigureServices(host.Services); //setting default
            _mainView = Ioc.Default.GetService<MainWindow>()!; //set mainview

            SettingView();

            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _mainView.Show(); //async show
            //_mainView.ShowDialog(); //sync show
        }

        /// <summary>
        /// datacontext of view connect viewmodel
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void SettingView()
        {
            var vm = Ioc.Default.GetService<MainWindowViewModel>()!;

            foreach (var pair in _viewPair)
            {
                var uc = (ContentControl)Ioc.Default.GetService(pair.Key)!;
                if (uc.DataContext != null)
                {
                    throw new Exception($"{uc} is already allocated DataContext");
                }
                uc.DataContext = Ioc.Default.GetService(pair.Value) ?? throw new Exception("viewmodel null");

                if (uc != _mainView) //add tabs usercontrol
                {
                    vm.TabItems.Add(new TabItem { Content = uc, Header = uc.GetType().Name.Replace("View", "") });
                }
            }

            foreach (var view in _viewList)
            {
                var uc = (ContentControl)Ioc.Default.GetService(view)!;
                vm.TabItems.Add(new TabItem { Content = uc, Header = uc.GetType().Name });
            }
        }

        /// <summary>
        /// add view and viewmodel connected
        /// </summary>
        /// <typeparam name="View"></typeparam>
        /// <typeparam name="ViewModel"></typeparam>
        private void AddViewAndViewModel<View, ViewModel>() where View : ContentControl where ViewModel : class
        {
            _services.AddSingleton<View>();
            _services.AddSingleton<ViewModel>();

            _viewPair.Add(typeof(View), typeof(ViewModel));
        }

        /// <summary>
        /// add only view
        /// </summary>
        /// <typeparam name="View"></typeparam>
        private void AddView<View>() where View : ContentControl
        {
            _services.AddSingleton<View>();
            _viewList.Add(typeof(View));
        }
    }
}
