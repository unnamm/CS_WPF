using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    public class SettingViewModel : INavigationAware
    {
        readonly ILogger _logger;

        public SettingViewModel(ILogger<SettingViewModel> logger)
        {
            _logger = logger;
            logger.LogInformation("setting");
        }

        public Task OnNavigatedFromAsync()
        {
            _logger.LogInformation("open settingView");
            return Task.CompletedTask;
        }

        public Task OnNavigatedToAsync()
        {
            _logger.LogInformation("close settingView");
            return Task.CompletedTask;
        }
    }
}
