using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Model
{
    public class SettingTab
    {
        public string Header { get; }
        public ObservableCollection<SettingItem> Settings { get; } = [];

        public SettingTab(string header)
        {
            Header = header;
        }
    }
}
