using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI.Model
{
    public partial class ItemMenu : ObservableObject
    {
        [ObservableProperty] private bool _isEnabled; //list select enable
        [ObservableProperty] private string _name; //list name
        [ObservableProperty] private ContentControl _context; //view

        public ItemMenu(string name, ContentControl page, bool enabled = true)
        {
            Name = name;
            Context = page;
            IsEnabled = enabled;
        }
    }
}
