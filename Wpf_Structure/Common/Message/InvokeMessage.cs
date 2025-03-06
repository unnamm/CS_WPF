using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// Application.Current.Dispatcher.Invoke
    /// </summary>
    /// <param name="Action"></param>
    /// <param name="Message"></param>
    public record InvokeMessage(Action<string> @Action, string Message);
}
