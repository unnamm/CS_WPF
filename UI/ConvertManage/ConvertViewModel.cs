using CommunityToolkit.Mvvm.ComponentModel;

namespace UI.ConvertManage
{
    internal partial class ConvertViewModel : ObservableObject
    {
        [ObservableProperty] private bool _value = false;

        public ConvertViewModel()
        {
            startTick();
        }

        private async void startTick()
        {
            while(true)
            {
                await Task.Delay(1000);
                Value = !Value;
            }
        }
    }
}
