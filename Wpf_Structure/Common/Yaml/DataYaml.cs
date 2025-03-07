using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Common.Yaml
{
    public class DataYaml : YamlBase
    {
        [YamlMember(Alias = "string")]
        public string Data1 { get; set; } = string.Empty;

        [YamlMember(Alias = "int")]
        public int Data2 { get; set; }

        [YamlMember(Alias = "double")]
        public double Data3 { get; set; }

        [YamlMember(Alias = "array")]
        public object[] Data4 { get; set; } = [];

        [YamlMember(Alias = "json style array")]
        public object[] Data5 { get; set; } = [];
    }
}
