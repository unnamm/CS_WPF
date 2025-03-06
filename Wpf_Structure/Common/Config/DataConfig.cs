using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Config
{
    public class DataConfig : ConfigBase
    {
        public int LogMaxLine;
        public string LogFolderName = string.Empty;

        public DataConfig()
        {
            base.Get(ref LogMaxLine, "Log");
            base.Get(ref LogFolderName, "Log");
        }
    }
}
