using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Common.Yaml
{
    public abstract class YamlBase
    {
        /// <summary>
        /// init yaml class member
        /// </summary>
        /// <param name="path"></param>
        public void InitMember(string path = "")
        {
            var myType = this.GetType();

            if (path == string.Empty)
            {
                path = myType.Name + ".yaml";
                path = Path.Combine("Yaml", path); //Yaml\\class.yaml
            }

            var target = new DeserializerBuilder().Build().Deserialize(File.ReadAllText(path), myType);

            var properties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(target));
            }
        }

    }
}
