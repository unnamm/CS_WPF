using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI
{
    internal class App : Application
    {
        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow().Show(); //async show
            //new MainWindow().ShowDialog(); //sync show
        }
    }
}
