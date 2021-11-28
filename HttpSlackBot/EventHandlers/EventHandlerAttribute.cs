using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.EventHandlers
{
    public class EventHandlerAttribute : Attribute
    {
        public string EventName { get; }

        public EventHandlerAttribute(string eventName)
        {
            EventName = eventName;
        }
    }
}
