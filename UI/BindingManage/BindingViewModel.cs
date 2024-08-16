using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.BindingManage
{
    internal partial class BindingViewModel : ObservableObject
    {
        [ObservableProperty] DateTime _timeText;

        internal BindingViewModel()
        {
            TimeText = DateTime.Now;

            startTick();
        }

        private async void startTick()
        {
            while (true)
            {
                await Task.Delay(500);
                TimeText = DateTime.Now;
            }
        }
    }
}
