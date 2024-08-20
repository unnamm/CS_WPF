using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace UI.CollectionManage
{
    internal partial class CollectViewModel
    {
        public ICollectionView Sources1 { get; set; } //show filter
        public ICollectionView Sources2 { get; set; } //devide to filter
        public ICollectionView Sources3 { get; set; } //devide to filter

        private int _index = 0;
        private ObservableCollection<string> _list = []; //binding need ObservableCollection

        public CollectViewModel()
        {
            foreach (var _ in Enumerable.Range(0, 30))
            {
                _list.Add("data" + _index);
                _index++;
            }

            Sources1 = CollectionViewSource.GetDefaultView(_list); //same IEnumerable is same collectionview, make new object

            Sources2 = CollectionViewSource.GetDefaultView(_list.ToArray());
            Sources2.Filter = x => ((string)x).Contains('1') == true;
            Sources3 = CollectionViewSource.GetDefaultView(_list.ToArray());
            Sources3.Filter = x => ((string)x).Contains('2') == true;

            startTick();
        }

        private async void startTick()
        {
            while (true)
            {
                await Task.Delay(1000);
                _list.Add("data" + _index);
                _index++;
            }
        }

        [RelayCommand]
        public void Filter1()
        {
            Sources1.Filter = x => ((string)x).Contains('1') == true;
        }

        [RelayCommand]
        public void Filter2()
        {
            Sources1.Filter = x => ((string)x).Contains('2') == true;
        }

        [RelayCommand]
        public void Filter3() //no filter
        {
            Sources1.Filter = null;
        }
    }
}
