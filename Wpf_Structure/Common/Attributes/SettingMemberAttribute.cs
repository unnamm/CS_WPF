using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SettingMemberAttribute : Attribute
    {
        public string Name { get; }

        public ConvertType Converter { get; }

        public string[] Metadata { get; }

        public SettingMemberAttribute(string name, ConvertType converter, params string[] metadata)
        {
            Name = name;
            Converter = converter;
            Metadata = metadata;
        }
    }
}
