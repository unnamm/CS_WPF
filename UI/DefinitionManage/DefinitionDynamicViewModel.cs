using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.DefinitionManage
{
    internal partial class DefinitionDynamicViewModel : ObservableObject
    {
        [ObservableProperty] int _rowNum;
        [ObservableProperty] int _columnNum;
        [ObservableProperty] string _rowStar = string.Empty;
        [ObservableProperty] string _columnStar = string.Empty;

        public DefinitionDynamicViewModel()
        {
        }
    }
}
