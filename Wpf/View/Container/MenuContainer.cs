using Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using View.Settings;
using Wpf.Ui.Controls;

namespace View.Container
{
    public class MenuContainer
    {
        public ObservableCollection<NavigationViewItem> MenuItems { get; } = [];
        public ObservableCollection<NavigationViewItem> FooterMenuItems { get; } = [];
        readonly List<NavigationViewItem> _menus = [];
        NavigationViewItem? _login;

        public MenuContainer()
        {
            foreach (var configType in ConfigSectionRegistry.All)
            {
                FooterMenuItems.Add(new NavigationViewItem
                {
                    Content = configType.Name,
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(SettingSectionPage<>).MakeGenericType(configType)
                });
            }
        }

        public void AddMenu<T>(SymbolRegular icon, bool isLoginMenu = false) where T : Page
        {
            var item = new NavigationViewItem
            {
                Content = typeof(T).Name,
                Icon = new SymbolIcon { Symbol = icon },
                TargetPageType = typeof(T)
            };

            if (isLoginMenu)
                _login = item;
            else
                _menus.Add(item);

            MenuItems.Add(item);
        }

        public void UpdateMenuState(bool isLogined)
        {
            SetState(_login!, !isLogined);
            foreach (var item in _menus)
            {
                SetState(item, isLogined);
            }
        }

        static void SetState(NavigationViewItem item, bool enabled)
        {
            const double ActiveOpacity = 1;
            const double DisabledOpacity = 0.4;

            item.IsEnabled = enabled;
            item.Opacity = enabled ? ActiveOpacity : DisabledOpacity;
        }
    }
}
