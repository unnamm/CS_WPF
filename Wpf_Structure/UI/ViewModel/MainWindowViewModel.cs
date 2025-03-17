using Common.Message;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UI.Model;
using UI.View;

namespace UI.ViewModel
{
    public partial class MainWindowViewModel : ObservableRecipient,
        IRecipient<BusyMessage>, IRecipient<InvokeMessage>
    {
        [ObservableProperty] private bool _isBusy;
        [ObservableProperty] private string? _busyText;
        [ObservableProperty] private string _dialogIdentifier;
        [ObservableProperty] private ItemMenu? _selectedItem;

        public ObservableCollection<ItemMenu> MenuItems { get; } = [];

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
