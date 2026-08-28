using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Log;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using View.Container;
using View.Model;
using View.Rapper;

namespace View
{
    public partial class MainWindowViewModel : ObservableObject
    {
        readonly AppNavigator _navigator;
        public ObservableCollection<LogEntry> Logs { get; } = [];
        public MenuContainer Menu { get; }
        public DeviceTracker Devices { get; }
        public UserSession Session { get; }
        public string BuildDate { get; } = "App " + File.GetLastWriteTime(Assembly.GetEntryAssembly()!.Location).ToString("yyyy.MM.dd.HHmm");

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session, AppNavigator navigator, MenuContainer menuContainer, DeviceTracker deviceTracker)
        {
            Logs = viewLog.Logs;
            Session = session;
            _navigator = navigator;
            Menu = menuContainer;
            Devices = deviceTracker;

            Session.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Session.CurrentUserId))
                    Menu.UpdateMenuState(!string.IsNullOrEmpty(Session.CurrentUserId));
            };
        }

        [RelayCommand]
        void Logout()
        {
            Session.CurrentUserId = null;
            _navigator.Navigate<Login>();
        }
    }
}
