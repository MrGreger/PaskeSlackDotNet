using MassTransit;
using PaskeApp.Events.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.Common.EventSending
{
    public class RabbitMqEventsSender : IEventsSender
    {
        private static Dictionary<Type, string> _queueNameCache = new Dictionary<Type, string>();

        private readonly IBus _messageBus;

        public RabbitMqEventsSender(IBus bus)
        {
            _messageBus = bus;
        }

        public async Task SendEvent<T>(T @event)
        {
            var queueName = GetQueueNameFromEvent(@event);

            var endpontUri = new Uri($"queue:{queueName}");
            var endpint = await _messageBus.GetSendEndpoint(endpontUri);

            await endpint.Send(@event);
        }

        private string GetQueueNameFromEvent<T>(T @event)
        {
            if(_queueNameCache.TryGetValue(@event.GetType(), out var result))
            {
                return result;
            }

            var queueNameAttribute = @event.GetType().GetCustomAttribute<ForQueueAttribure>();

            if(queueNameAttribute == null)
            {
                throw new Exception($"Event must have ForQueueAttribure: {@event.GetType().FullName}");
            }

            return queueNameAttribute.QueueName;
        }
    }
}
