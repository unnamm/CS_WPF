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
        private string _filePath = string.Empty;

        public void Load()
        {
            var myType = this.GetType();

            _filePath = myType.Name + ".yaml";
            _filePath = Path.Combine("Yaml", _filePath); //Yaml\\class.yaml

            var target = new DeserializerBuilder().Build().Deserialize(File.ReadAllText(_filePath), myType);

            var properties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(target));
            }
        }

        public void Save()
        {
            var temp = new SerializerBuilder().Build().Serialize(this);
            File.WriteAllText(_filePath, temp);
        }

    }
}
