using System;
using System.Collections.Generic;
using System.Text;

namespace Device
{
    public interface IDevice
    {
        public bool IsConnected { get; }
    }
}
