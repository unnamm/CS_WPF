using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UI.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        [ObservableProperty] public partial BitmapSource? Image { get; set; }

        readonly ILogger _logger;
        readonly Vision.Ocr _ocr = new(Vision.Ocr.KoKR);

        public HomePageViewModel(ILogger<HomePageViewModel> logger)
        {
            _logger = logger;
        }

        [RelayCommand]
        async Task SelectImage()
        {
            using var a = _logger.BeginScope("SelectImage");
            try
            {
                var dialog = new OpenFileDialog
                {
                    Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
                };

                if (dialog.ShowDialog() != true)
                    return;

                Image = LoadWithExifRotation(dialog.FileName);

                var imageData = File.ReadAllBytes(dialog.FileName);
                var stopwatch = Stopwatch.StartNew();
                var result = await _ocr.GetOcrAsync(imageData);
                stopwatch.Stop();
                _logger.LogInformation("\n tasktime: {time}\n{lines}", stopwatch, result.Text);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        static BitmapSource LoadWithExifRotation(string path)
        {
            using var stream = File.OpenRead(path);
            var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            var frame = decoder.Frames[0];

            var angle = 0;
            if (frame.Metadata is BitmapMetadata metadata && metadata.ContainsQuery("System.Photo.Orientation"))
            {
                var orientation = (ushort)(metadata.GetQuery("System.Photo.Orientation"));
                angle = orientation switch
                {
                    6 => 90,
                    3 => 180,
                    8 => 270,
                    _ => 0
                };
            }

            BitmapSource bitmap = angle == 0 ? frame : new TransformedBitmap(frame, new RotateTransform(angle));
            bitmap.Freeze();
            return bitmap;
        }
    }
}
