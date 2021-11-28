using HttpSlackBot.Interactions;
using PaskeApp.Events.ShopEvents;
using SlackBot.Common.EventSending;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsBot.InteractionHandlers
{
    public class DeclineShopVerificationEvent { }

    [InteractionHandler("decline-verification-request")]
    public class DeclineShopVerificationHandler : InteractionHandler<ShopVerifyDeclinedEvent>
    {
        private IEventsSender _eventsSender;

        public DeclineShopVerificationHandler(IEventsSender eventsSender)
        {
            _eventsSender = eventsSender;
        }

        protected override async Task OnEvent(InteractionEvent<ShopVerifyDeclinedEvent> payload)
        {
            var requestId = payload.Action.Value;

            await _eventsSender.SendEvent(new ShopVerifyDeclinedEvent { Date = DateTime.UtcNow, RequestId = requestId });
        }
    }
}
