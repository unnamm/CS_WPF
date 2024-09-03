using CommunityToolkit.Mvvm.ComponentModel;

namespace UI.ConvertManage
{
    internal partial class ConvertViewModel : ObservableObject
    {
        [ObservableProperty] private bool _isValue = false;
        [ObservableProperty] private int _value = 0;
        [ObservableProperty] private string _str = string.Empty;


        public ConvertViewModel()
        {
            startTick();
        }

        private async void startTick()
        {
            while (true)
            {
                await Task.Delay(1000);
                IsValue = !IsValue;
                Value++;
                Str = Str == "m/s" ? "kg" : "m/s";
            }
        }
    }
}
