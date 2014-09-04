using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Logging
{
    public interface ILogger
    {
        void Log(string message, MessageType messageType = MessageType.Info);
    }
}
