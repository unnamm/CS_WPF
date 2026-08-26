using System;
using System.Threading;
using System.Windows;

namespace Starter
{
    /// <summary>
    /// program entry point
    /// </summary>
    internal class Start
    {
        const string MutexName = "Wpf.Starter.Start";

        [STAThread]
        static void Main()
        {
            using var mutex = new Mutex(true, MutexName, out var createdNew);
            if (!createdNew)
            {
                MessageBox.Show("already running", "Warrning");
                return;
            }
            new App().Run();
        }
    }
}
