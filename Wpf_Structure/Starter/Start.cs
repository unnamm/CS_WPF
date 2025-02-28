using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter
{
    /// <summary>
    /// main start
    /// </summary>
    internal class Start
    {
        [STAThread]
        private static void Main()
        {
            new App().Run(); //App.Startup()
        }
    }
}
