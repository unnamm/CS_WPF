using System.Collections.ObjectModel;

namespace UI.Container
{
    public class DeviceTracker
    {
        public ObservableCollection<Model.DeviceStateItem> DeviceStates { get; set; } = [];

        public DeviceTracker(Database.SQLite db)
        {
            DeviceStates.Add(new(db));
        }
    }
}
