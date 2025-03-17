using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Common.Config
{
    public class DataYaml : YamlBase
    {
        [YamlMember(Alias = "logMaxLine")]
        public int LogMaxLine { get; set; }

        [YamlMember(Alias = "logFolderName")]
        public string LogFolderName { get; set; } = string.Empty;
    }
}
