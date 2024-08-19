using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.DataGridManage
{
    /// <summary>
    /// datagrid data
    /// binding need ObservableObject and ObservableProperty
    /// </summary>
    internal partial class DataGridModel : ObservableObject
    {
        [ObservableProperty] private string _name = string.Empty;
        [ObservableProperty] private int _data;
        [ObservableProperty] private bool _check;
        [ObservableProperty] private string _comboSelect = string.Empty;
    }
}
