using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace UI.Container
{
    public class DeviceTracker
    {
        public ObservableCollection<Model.DeviceStateItem> DeviceStates { get; set; } = [];

        readonly ILogger _logger;

        public DeviceTracker(Database.SQLite db, ILogger<DeviceTracker> logger)
        {
            _logger = logger;

            DeviceStates.Add(new(db));
            _ = Update(1000);
        }

        async Task Update(int milliseconds)
        {
            while (true)
            {
                try
                {
                    await Task.Delay(milliseconds);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        foreach (var item in DeviceStates)
                        {
                            item.Update();
                        }
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError("{ex.Message}", ex.Message);
                }
            }
        }
    }
}
