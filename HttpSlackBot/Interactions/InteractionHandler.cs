using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HttpSlackBot.Interactions
{
    public abstract class InteractionHandler
    {
        public abstract Task HandleInteraction(InteractionEvent payload);
    }

    public abstract class InteractionHandler<T> : InteractionHandler
    {
        public override Task HandleInteraction(InteractionEvent interaction)
        {
            var castedInteraction = InteractionEvent<T>.FromBaseEvent(interaction);
            return OnEvent(castedInteraction);
        }

        protected abstract Task OnEvent(InteractionEvent<T> payload);
    }
}
