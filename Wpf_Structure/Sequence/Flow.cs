using Common.Message;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    public class Flow : IRecipient<MainViewInitMessage>
    {
        public Flow()
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
        }

        public void Receive(MainViewInitMessage message)
        {
            //init all function
        }
    }
}
