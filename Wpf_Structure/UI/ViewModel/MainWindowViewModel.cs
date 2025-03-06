using Common;
using Common.Message;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<BusyMessage>
    {
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string _busyText = string.Empty;

        public MainWindowViewModel()
        {
            Receive(new BusyMessage(true, "loading..."));
            IsActive = true;
        }

        public void Receive(BusyMessage message)
        {
            IsBusy = message.IsShow;
            BusyText = message.Text;
        }
    }
}
