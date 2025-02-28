using Common.Message;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.View;

namespace UI.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<BusyMessage>
    {
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string _busyText = string.Empty;

        public MainWindowViewModel()
        {
            IsActive = true;
            //F();
        }

        private async void F()
        {
            await Task.Delay(1500);
            _ = Application.Current.Dispatcher.Invoke(async () =>
            {
                var view = new DialogView();
                await DialogHost.Show(view, "RootDialog");
            });
        }

        public void Receive(BusyMessage message)
        {
            IsBusy = message.IsShow;
            BusyText = message.Text;
        }
    }
}
