using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    /// <summary>
    /// Tab name, if empty is class name
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class SettingMapperAttribute : Attribute
    {
        public string Name { get; }

        public SettingMapperAttribute(string name)
        {
            Name = name;
        }
    }
}
