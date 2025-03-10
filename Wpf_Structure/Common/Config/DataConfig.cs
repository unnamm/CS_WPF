using Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    public class DataConfig : ConfigBase, IConfig
    {
        public int LogMaxLine;
        public string LogFolderName = string.Empty;

        public DataConfig()
        {
            Load();
        }

        public void Load()
        {
            base.Get(ref LogMaxLine, "Log");
            base.Get(ref LogFolderName, "Log");
        }

        public void Save()
        {
            base.Set(LogMaxLine, "Log");
            base.Set(LogFolderName, "Log");
        }
    }
}
