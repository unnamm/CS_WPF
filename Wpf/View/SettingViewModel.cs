using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    public class SettingViewModel : NavigationAware
    {
        readonly ILogger _logger;

        public SettingViewModel(ILogger<SettingViewModel> logger)
        {
            _logger = logger;
            logger.LogInformation("setting");
        }

        public override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            _logger.LogInformation("to SettingViewModel");
        }

        public override void OnNavigatedFrom()
        {
            base.OnNavigatedFrom();
            _logger.LogInformation("from SettingViewModel");
        }
    }
}
