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

        public TempCollection Temp { get; } = new();

        public ObservableCollection<string> StringCollection { get; } = []; //need public, need get property

        internal BindingViewModel()
        {
            TimeText = DateTime.Now;

            startTick();

            Temp.Col = new ObservableCollection<string>(["a", "b"]); //only use in Constructor
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
                //Temp.Col = new ObservableCollection<string>(["a", "b"]); //imposible
                Temp.Col.Add(TimeText.ToString());
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
        /// auto button enable
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [RelayCommand]
        public static async Task ButtonClickParam(string param)
        {
            await Task.Delay(1000);
            MessageBox.Show(param, "click");
        }
    }
}
