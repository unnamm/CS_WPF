using Common;
using Common.Message;
using Common.Yaml;
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

                await Task.Run(() => _yamlData.InitMember());
                LogSampleTest();

                WeakReferenceMessenger.Default.Send(new DialogMessage("title", "content")); //popup sample test

                WeakReferenceMessenger.Default.Send(new BusyMessage(false)); //close wait
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("init error", ex.Message));
                _log.Write(ex.Message);
            }
        }

        public async void Receive(MainViewCloseMessage message)
        {
            WeakReferenceMessenger.Default.Send(new BusyMessage(true, "exit..."));
            try
            {
                //do dispose

                await Task.Delay(500); //dispose time

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("dispose error", ex.Message));
                _log.Write(ex.Message);
            }
        }

        private async void LogSampleTest()
        {
            int i = 0;
            while (true)
            {
                await Task.Delay(1000);
                _log.Write("test" + i++);
            }
        }

    }
}
