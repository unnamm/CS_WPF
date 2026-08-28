using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using View.Model;

namespace View.Container
{
    public class DeviceTracker
    {
        public ObservableCollection<DeviceStateItem> DeviceStates { get; set; } = [];

        public DeviceTracker(Database.SQLite db)
        {
            DeviceStates.Add(new(db));
        }

        public void Update()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var item in DeviceStates)
                {
                    item.Update();
                }
            });
        }
    }
}
