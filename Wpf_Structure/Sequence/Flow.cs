using Common.Message;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    public class Flow : IRecipient<MainViewInitMessage>
    {
        public void Receive(MainViewInitMessage message)
        {
            //init all function
        }
    }
}
