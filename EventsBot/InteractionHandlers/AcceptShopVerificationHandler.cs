using HttpSlackBot.Interactions;
using PaskeApp.Events.ShopEvents;
using SlackBot.Common.EventSending;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsBot.InteractionHandlers
{
    public class AcceptShopVerification { }

    [InteractionHandler("accept-verification-request")]
    public class AcceptShopVerificationHandler : InteractionHandler<AcceptShopVerification>
    {
        private IEventsSender _eventsSender;

        public AcceptShopVerificationHandler(IEventsSender eventsSender)
        {
            _eventsSender = eventsSender;
        }

        protected override async Task OnEvent(InteractionEvent<AcceptShopVerification> payload)
        {
            var requestId = payload.Action.Value;

            await _eventsSender.SendEvent(new ShopVerifiedByAdminEvent { Date = DateTime.UtcNow, RequestId = requestId });
        }
    }
}
