using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class ContentViewModel
    {
        public Log LogInstance { get; }

        public ContentViewModel(Log log)
        {
            LogInstance = log;
        }
    }
}
