using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace UI.BindingManage
{
    /// <summary>
    /// need partial
    /// need ObservableObject
    /// </summary>
    internal partial class BindingViewModel : ObservableObject
    {
        [ObservableProperty] DateTime _timeText; //TimeText binding view
        [ObservableProperty] double _value;

        public ObservableCollection<string> StringCollection { get; } = []; //need public, need get property

        internal BindingViewModel()
        {
            TimeText = DateTime.Now;

            startTick();
        }

        /// <summary>
        /// set member var
        /// </summary>
        private async void startTick()
        {
            while (true)
            {
                await Task.Delay(1000);
                TimeText = DateTime.Now;
                Value = new Random().NextDouble();
                StringCollection.Add(TimeText.ToString("yyyy-MM-dd HH:mm:ss.f"));
            }
        }

        /// <summary>
        /// binding button command
        /// need public
        /// </summary>
        [RelayCommand]
        public static void ButtonClick()
        {
            MessageBox.Show("", "click");
        }

        [RelayCommand]
        public static void ButtonClickParam(string param)
        {
            MessageBox.Show(param, "click");
        }
    }
}
