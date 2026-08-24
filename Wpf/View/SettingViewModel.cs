using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace View
{
    public class SettingViewModel
    {
        public SettingViewModel(ILogger<SettingViewModel> logger)
        {
            logger.LogInformation("setting");
        }
    }
}
