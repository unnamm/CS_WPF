using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.MessageManage
{
    internal class OtherThread
    {
        public OtherThread()
        {
            Run();
            //_ = Run2();

            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    WeakReferenceMessenger.Default.Send(new SendMessage("Task.Run"));
                }
            });
        }

        private static async void Run()
        {
            await Task.Delay(500);

            while (true)
            {
                await Task.Delay(1000);
                WeakReferenceMessenger.Default.Send(new SendMessage("Run"));
            }
        }

        private static async Task Run2()
        {
            await Task.Delay(500);

            while (true)
            {
                await Task.Delay(1000);
                WeakReferenceMessenger.Default.Send(new SendMessage("Run2"));
            }
        }
    }
}
