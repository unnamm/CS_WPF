using Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    public class SettingData2 : YamlBase
    {
        [SettingMember("item1", ConvertType.Text, "text sample")]
        public string? Data1 { get; set; }

        [SettingMember("item2", ConvertType.Combo, ["select1", "select2"])]
        public string? Mode { get; set; }

        public string? Third { get; set; }
    }
}
