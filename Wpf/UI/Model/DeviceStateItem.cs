using CommunityToolkit.Mvvm.ComponentModel;
using Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Model
{
    public partial class DeviceStateItem : ObservableObject
    {
        [ObservableProperty] public partial string? Name { get; set; }
        public bool IsConnected => _device.IsConnected;

        readonly IDevice _device;

        public DeviceStateItem(IDevice device)
        {
            Name = device.GetType().Name;
            _device = device;
            _device.ConnectionChanged += () => this.OnPropertyChanged(nameof(IsConnected));
        }
    }
}
