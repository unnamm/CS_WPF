using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace UI.CollectionManage
{
    internal partial class CollectViewModel
    {
        public ICollectionView Sources1 { get; } //show filter
        public ICollectionView Sources2 { get; } //devide to filter
        public ICollectionView Sources3 { get; } //devide to filter

        private readonly ObservableCollection<string> _list = [];

        public CollectViewModel()
        {
            foreach (var i in Enumerable.Range(0, 10))
            {
                _list.Add("data" + i);
            }

            //one collection, each ICollectionView has different filter
            Sources1 = new CollectionViewSource { Source = _list }.View;
            Sources2 = new CollectionViewSource { Source = _list }.View;
            Sources2.Filter = x => ((string)x).Contains('1') == true;
            Sources3 = new CollectionViewSource { Source = _list }.View;
            Sources3.Filter = x => ((string)x).Contains('2') == true;

            StartTick();
        }

        private async void StartTick()
        {
            while (true)
            {
                await Task.Delay(1000);

                Application.Current.Dispatcher.Invoke(() =>
                {
                    _list.Add("data" + _list.Count);
                });
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
