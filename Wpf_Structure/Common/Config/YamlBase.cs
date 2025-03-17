using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Common.Config
{
    public abstract class YamlBase
    {
        private string _filePath = string.Empty;

        public async Task LoadAsync()
        {
            var myType = this.GetType();
            var folderName = myType.Namespace!.Split('.').Last();

            _filePath = myType.Name + ".yaml";
            _filePath = Path.Combine(folderName, _filePath);

            var readText = await File.ReadAllTextAsync(_filePath);

            var data = new DeserializerBuilder().Build().Deserialize(readText, myType);

            var properties = GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(data));
            }
        }

        public Task SaveAsync()
        {
            var temp = new SerializerBuilder().Build().Serialize(this);
            return File.WriteAllTextAsync(_filePath, temp);
        }

    }
}
