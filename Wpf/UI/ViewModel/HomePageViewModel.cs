using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace UI.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        [ObservableProperty] public partial BitmapImage? Image { get; set; }

        [RelayCommand]
        void SelectImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() != true)
                return;

            Image = new BitmapImage(new Uri(dialog.FileName));
        }
    }
}
