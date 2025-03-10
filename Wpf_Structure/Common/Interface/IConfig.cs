using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interface
{
    /// <summary>
    /// setting local data
    /// </summary>
    interface IConfig
    {
        /// <summary>
        /// load all member from local file
        /// </summary>
        void Load();

        /// <summary>
        /// save all member to local file
        /// </summary>
        void Save();
    }
}
