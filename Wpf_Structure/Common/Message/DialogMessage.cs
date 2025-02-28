using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Message
{
    /// <summary>
    /// show alert popup window
    /// </summary>
    /// <param name="Title"></param>
    /// <param name="Content"></param>
    public record DialogMessage(string Title, string Content);
}
