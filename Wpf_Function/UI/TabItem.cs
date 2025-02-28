using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// main window tab item
    /// </summary>
    class TabItem
    {
        /// <summary>
        /// view
        /// </summary>
        public ContentControl? Content { get; set; }

        /// <summary>
        /// tab header
        /// </summary>
        public string? Header { get; set; }
    }
}
