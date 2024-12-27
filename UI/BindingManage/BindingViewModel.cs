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

        public BindingViewModel()
        {
            TimeText = DateTime.Now;

            StartTick();
        }

        /// <summary>
        /// set member var
        /// </summary>
        private async void StartTick()
        {
            while (true)
            {
                await Task.Delay(1000);
                TimeText = DateTime.Now;
                Value = new Random().NextDouble();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    StringCollection.Add(TimeText.ToString("yyyy-MM-dd HH:mm:ss.f"));
                });
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

        /// <summary>
        /// async button command, auto enable button control
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public static async Task ButtonClickDelay()
        {
            await Task.Delay(500);
            MessageBox.Show("delay 500ms", "click");
        }

        /// <summary>
        /// param button command
        /// </summary>
        /// <param name="param"></param>
        [RelayCommand]
        public static void ButtonClickParam(string param)
        {
            MessageBox.Show(param, "click");
        }
    }
}
