using Common.Message;
using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow : IRecipient<MainWindowRenderedMessage>, IRecipient<MainViewCloseMessage>
    {
        public Flow()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public async void Receive(MainWindowRenderedMessage message)
        {
            await Task.Delay(500); //init time
            WeakReferenceMessenger.Default.Send(new BusyMessage(false));
        }

        public async void Receive(MainViewCloseMessage message)
        {
            WeakReferenceMessenger.Default.Send(new BusyMessage(true, "exit..."));
            await Task.Delay(500); //dispose time
            Environment.Exit(0);
        }
    }
}
