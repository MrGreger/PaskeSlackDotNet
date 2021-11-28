using HttpSlackBot.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpSlackBot.EventHandlers
{

    public abstract class SlackEventHandler
    {
        public abstract Task HandleEvent(SlackCallback payload);
    }

    public abstract class SlackEventHandler<T> : SlackEventHandler where T : SlackBaseEvent
    {
        public override Task HandleEvent(SlackCallback payload)
        {
            return OnEvent(payload.EventObject.ToObject<T>());
        }

        protected abstract Task OnEvent(T payload);
    }
}
