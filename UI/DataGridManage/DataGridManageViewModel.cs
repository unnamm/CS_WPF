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

        public List<DataGridModel> Collection2 { get; set; } = []; //add, remove error, hope no use

        public ObservableCollection<string> ComboData { get; set; } = []; //rows combobox data

        [ObservableProperty] private DataGridModel? _selected; //select row

        public DataGridManageViewModel()
        {
            Collection1.Add(new() { Name = "Name1", Data = 1, Check = true });
            Collection1.Add(new() { Name = "Name2", Data = 2 });
            Collection1.Add(new() { Name = "Name3", Data = 3 });

            Collection2.Add(new() { Name = "Name4", Data = 4 });
            Collection2.Add(new() { Name = "Name4", Data = 4 });
            Collection2.Add(new() { Name = "Name4", Data = 4 });

            ComboData.Add("combo1");
            ComboData.Add("combo2");
            ComboData.Add("combo3");

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

                //Collection1.Add(new() { Name = "Name3", Data = 3 }); //good
                //Collection2.Add(new() {Name = "test" }); //error
            }
        }
    }
}
