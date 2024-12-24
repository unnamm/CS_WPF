using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MessageManage
{
    internal partial class MessageViewModel : ObservableObject, IRecipient<SendMessage>
    {
        [ObservableProperty] private string _data;

        public MessageViewModel()
        {
            Data = "abcd";

            WeakReferenceMessenger.Default.Register(this);

            var v = new OtherThread();
        }

        public void Receive(SendMessage message)
        {
            Data = message.Message;
        }
    }
}
