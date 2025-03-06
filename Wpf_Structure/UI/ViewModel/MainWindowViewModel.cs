using Common.Message;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.View;

namespace UI.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient,
        IRecipient<BusyMessage>, IRecipient<InvokeMessage>
    {
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string _busyText = string.Empty;
        [ObservableProperty] private string _dialogIdentifier;

        public MainWindowViewModel()
        {
            Receive(new BusyMessage(true, "loading..."));
            IsActive = true;

            DialogIdentifier = DialogView.DialogIdentifire;
        }

        public void Receive(BusyMessage message)
        {
            IsBusy = message.IsShow;
            BusyText = message.Text;
        }

        public void Receive(InvokeMessage message)
        {
            Application.Current.Dispatcher.Invoke(() => message.Action(message.Message));
        }
    }
}
