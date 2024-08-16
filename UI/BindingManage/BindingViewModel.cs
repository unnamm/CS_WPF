using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            }
        }

        /// <summary>
        /// binding command
        /// </summary>
        [RelayCommand]
        public void ButtonClick()
        {
            MessageBox.Show("", "click");
        }

        [RelayCommand]
        public void ButtonClickParam(string param)
        {
            MessageBox.Show(param, "click");
        }
    }
}
