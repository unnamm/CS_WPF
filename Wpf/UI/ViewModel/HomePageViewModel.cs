using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace UI.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        [ObservableProperty] public partial BitmapImage? Image { get; set; }

        readonly ILogger _logger;
        readonly Vision.Ocr _ocr = new(Vision.Ocr.KoKR);

        public HomePageViewModel(ILogger<HomePageViewModel> logger)
        {
            _logger = logger;
        }

        [RelayCommand]
        async Task SelectImage()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (dialog.ShowDialog() != true)
                return;

            Image = new BitmapImage(new Uri(dialog.FileName));

            var imageData = File.ReadAllBytes(dialog.FileName);
            var stopwatch = Stopwatch.StartNew();
            var result = await _ocr.GetOcrAsync(imageData);
            _logger.LogInformation("\n line1: {line1}\n line2: {line2} \n tasktime: {time}", result.Lines[0].Text, result.Lines[1].Text, stopwatch);
        }
    }
}
