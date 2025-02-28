using Common.Message;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow
    {
        public Flow()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public async void Init()
        {
            await Task.Delay(1000); //init time
            WeakReferenceMessenger.Default.Send(new BusyMessage(false));
        }
    }
}
