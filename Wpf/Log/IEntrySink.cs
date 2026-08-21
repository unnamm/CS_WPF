using System;
using System.Collections.Generic;
using System.Text;

namespace Log
{
    internal interface IEntrySink
    {
        void Add(LogEntry log);
    }
}
