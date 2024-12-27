using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ItemsControlManage
{
    internal class ItemsControlViewModel
    {
        public ObservableCollection<string> Datas { get; } = [];

        public ItemsControlViewModel()
        {
            for (int i = 0; i < 50; i++)
            {
                Datas.Add(i.ToString());
            }
        }
    }
}
