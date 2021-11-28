using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot
{
    public class HandlersRegistry<THandler>
    {
        private Dictionary<string, Type> _handlers = new Dictionary<string, Type>();

        public void AddEventHandler(string eventName, Type handlerType)
        {
            if(!typeof(THandler).IsAssignableFrom(handlerType))
            {
                throw new Exception($"Handler must be subtype of {nameof(THandler)}");
            }

            if(!_handlers.TryAdd(eventName, handlerType))
            {
                throw new Exception($"Can not add handler for {eventName}");
            }
        }

        public Type GetHandlerForEvent(string eventName)
        {
            if(_handlers.TryGetValue(eventName, out var type))
            {
                return type;
            }

            return null;
        }
    }
}
