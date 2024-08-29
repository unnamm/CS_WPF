using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace UI.ComboboxManage
{
    internal partial class ComboboxViewModel : ObservableObject
    {
        public ObservableCollection<string> Items { get; } = [];

        [ObservableProperty] string _selectedItem1 = string.Empty;
        [ObservableProperty] string _selectedValue1 = string.Empty;

        public ComboboxViewModel()
        {
            Items.Add("data1");
            Items.Add("data2");
            Items.Add("data3");
            Items.Add("data4");
            Items.Add("data5");
        }

        [RelayCommand]
        public void Change(string data)
        {
            //select combobox
        }
    }
}
