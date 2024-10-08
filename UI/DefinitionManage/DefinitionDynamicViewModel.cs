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
            SetCellNum(3, 7);
        }

        private void SetCellNum(int row, int column)
        {
            RowNum = row;
            ColumnNum = column;

            //"0,1,...,row"
            //default is "auto"
            //all definition "*"
            RowStar = string.Join(',', Enumerable.Range(0, row));
            ColumnStar = string.Join(',', Enumerable.Range(0, column));
        }
    }
}
