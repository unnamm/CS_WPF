using Configuration.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Wpf.Ui.Abstractions.Controls;

namespace View
{
    public class SettingViewModel : NavigationAware
    {
        readonly ILogger _logger;
        readonly IOptionsMonitor<Appsettings> _appconfig;

        public SettingViewModel(ILogger<SettingViewModel> logger, IOptionsMonitor<Appsettings> config)
        {
            _logger = logger;
            logger.LogInformation("instance setting");

            _appconfig = config;
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

            _appconfig.Save(); //auto save
        }
    }
}
