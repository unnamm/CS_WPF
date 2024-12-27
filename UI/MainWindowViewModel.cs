using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI
{
    class MainWindowViewModel : ObservableObject
    {
        public ObservableCollection<TabItem> TabItems { get; } = [];
    }
}
