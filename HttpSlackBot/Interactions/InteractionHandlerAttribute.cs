using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Interactions
{
    public class InteractionHandlerAttribute : Attribute
    {
        public string InteractionActionName { get; }

        public InteractionHandlerAttribute(string eventName)
        {
            InteractionActionName = eventName;
        }
    }
}
