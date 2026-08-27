using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace View.Container
{
    public class MenuContainer
    {
        public ObservableCollection<NavigationViewItem> MenuItems { get; } = [];
        readonly List<NavigationViewItem> _menus = [];
        NavigationViewItem? _login;

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
            _login!.IsEnabled = !isLogined;
            foreach (var item in _menus)
            {
                item.IsEnabled = isLogined;
            }
        }
    }
}
