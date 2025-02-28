using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// program start main
    /// </summary>
    internal class Starter
    {
        [STAThread]
        private static void Main()
        {
            new App().Run(); //run Application.Startup()
        }
    }
}
