using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// show busyMessage at window
    /// </summary>
    /// <param name="IsShow"></param>
    /// <param name="Text"></param>
    public record BusyMessage(bool IsShow, string Text = "");
}
