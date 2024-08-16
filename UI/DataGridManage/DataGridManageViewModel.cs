using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace UI.DataGridManage
{
    /// <summary>
    /// manage datagrids
    /// </summary>
    internal partial class DataGridManageViewModel : ObservableObject
    {
        public ObservableCollection<DataGridModel> Collection1 { get; set; } = [];
        public ObservableCollection<DataGridModel> Collection2 { get; set; } = [];

        [ObservableProperty] private DataGridModel? _selected; //select row

        public DataGridManageViewModel()
        {
            Collection1.Add(new() { Name = "Name1", Data = 1 });
            Collection1.Add(new() { Name = "Name2", Data = 2 });
            Collection1.Add(new() { Name = "Name3", Data = 3 });

            Collection2.Add(new() { Name = "Name4", Data = 4 });
            Collection2.Add(new() { Name = "Name4", Data = 4 });
            Collection2.Add(new() { Name = "Name4", Data = 4 });

            timeTick();
        }

        /// <summary>
        /// change value selcted row
        /// </summary>
        private async void timeTick()
        {
            while (true)
            {
                await Task.Delay(1000);

                if (Selected == null)
                    continue;

                Selected.Data++;
                Selected.Name = "name" + new Random().Next(100);
            }
        }
    }
}
