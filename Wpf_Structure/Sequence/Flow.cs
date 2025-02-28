using Common.Message;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow : IRecipient<MainWindowRenderedMessage>
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
    }
}
