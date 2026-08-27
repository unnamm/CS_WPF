using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Log;
using System.Collections.ObjectModel;
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
        public UserSession Session { get; }

        public MainWindowViewModel(ViewLoggerProvider viewLog, UserSession session, AppNavigator navigator, MenuContainer menuContainer)
        {
            Logs = viewLog.Logs;
            Session = session;
            _navigator = navigator;
            Menu = menuContainer;

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
            _navigator.Navigate(typeof(Login));
        }
    }
}
