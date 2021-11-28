using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.CommandHandlers
{
    public class CommandHandlerAttribute : Attribute
    {
        public string CommandName { get; }

        public CommandHandlerAttribute(string eventName)
        {
            CommandName = eventName;
        }
    }
}
