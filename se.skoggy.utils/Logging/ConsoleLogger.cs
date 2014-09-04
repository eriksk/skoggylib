using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace se.skoggy.utils.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message, MessageType messageType = MessageType.Info)
        {
            Console.WriteLine(string.Format("{0}: '{1}'", messageType.ToString(), message));
        }
    }
}
