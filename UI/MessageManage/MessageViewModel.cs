using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.MessageManage
{
    internal partial class MessageViewModel : ObservableObject, IRecipient<SendMessage>
    {
        [ObservableProperty] private string _data;
        public ObservableCollection<string> TestCol { get; } = [];

        public MessageViewModel()
        {
            Data = "abcd";

            WeakReferenceMessenger.Default.Register(this);

            _ = new OtherThread();
        }

        public void Receive(SendMessage message)
        {
            Data = message.Message;
            Application.Current.Dispatcher.Invoke(() =>
            {
                TestCol.Add(message.Message);
            });
        }
    }
}
