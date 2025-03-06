using Common;
using Common.Message;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow : IRecipient<MainWindowRenderedMessage>, IRecipient<MainViewCloseMessage>
    {
        private readonly Log _log;

        public Flow(Log log)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
            _log = log;
        }

        public async void Receive(MainWindowRenderedMessage message)
        {
            try
            {
                await Task.Delay(500); //init time
                WeakReferenceMessenger.Default.Send(new BusyMessage(false));

                AutoLog();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("init error", ex.Message));
                _log.Write(ex.Message);
            }
        }

        private async void AutoLog()
        {
            while (true)
            {
                await Task.Delay(1000);
                _log.Write("test");
            }
        }

        public async void Receive(MainViewCloseMessage message)
        {
            WeakReferenceMessenger.Default.Send(new BusyMessage(true, "exit..."));
            try
            {
                await Task.Delay(500); //dispose time
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("dispose error", ex.Message));
                _log.Write(ex.Message);
            }
        }

    }
}
