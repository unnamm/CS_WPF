using CommunityToolkit.Mvvm.ComponentModel;
using Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace View.Model
{
    public partial class DeviceStateItem : ObservableObject
    {
        [ObservableProperty] public partial bool IsConnected { get; set; }
        [ObservableProperty] public partial string? Name { get; set; }

        readonly IDevice _device;

        public DeviceStateItem(IDevice device)
        {
            _device = device;
            Name = device.GetType().Name;
        }

        public void Update() => IsConnected = _device.IsConnected;
    }
}
