using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.Model
{
    public class SettingItem
    {
        public string DisplayName { get; }
        public FrameworkElement Control { get; }

        public SettingItem(string displayName, FrameworkElement control)
        {
            DisplayName = displayName;
            Control = control;
        }
    }
}
