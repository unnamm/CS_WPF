using Common;
using Common.Message;
using Common.Config;
using CommunityToolkit.Mvvm.Messaging;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow : IRecipient<MainWindowRenderedMessage>, IRecipient<MainViewCloseMessage>
    {
        private readonly Log _log;
        private readonly DataYaml _yamlData;

        public Flow(Log log, DataYaml dataYaml)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
            _log = log;
            _yamlData = dataYaml;
        }

        public async void Receive(MainWindowRenderedMessage message)
        {
            try
            {
                //do init
                await _yamlData.LoadAsync();

                //SampleTest();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("init error", ex.Message));
                _log.Write(ex.Message);
            }
            finally
            {
                WeakReferenceMessenger.Default.Send(new BusyMessage(false)); //close wait
            }
        }

        public async void Receive(MainViewCloseMessage message)
        {
            WeakReferenceMessenger.Default.Send(new BusyMessage(true, "exit..."));
            try
            {
                //do dispose

                await Task.Delay(500); //dispose time

                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("dispose error", ex.Message));
                _log.Write(ex.Message);
            }
        }

        private async void SampleTest()
        {
            WeakReferenceMessenger.Default.Send(new DialogMessage("title", "content")); //popup sample test

            int i = 0;
            while (true)
            {
                _log.Write("test" + i++);
                await Task.Delay(1000);
            }
        }

    }
}
