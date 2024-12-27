using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace UI.BindingManage
{
    internal partial class BindingViewModel : ObservableObject
    {
        //need partial, Inherit ObservableObject
        [ObservableProperty] double _value;
        [ObservableProperty] DateTime _timeText;

        public ObservableCollection<string> StringCol { get; } = []; //need public, get property

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

                //ObservableCollection.add need dispatch
                Application.Current.Dispatcher.Invoke(() =>
                {
                    StringCol.Add(TimeText.ToString("yyyy-MM-dd HH:mm:ss.f"));
                });
            }
        }

        /// <summary>
        /// binding button command
        /// </summary>
        [RelayCommand]
        public static void ButtonClick()
        {
            MessageBox.Show("", "click");
        }

        /// <summary>
        /// async button command, auto enable control
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
