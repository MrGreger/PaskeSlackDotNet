using HttpSlackBot.Blocks;
using HttpSlackBot.Interactions;
using HttpSlackBot.Interactions.Elements;
using HttpSlackBot.Messaging;
using Newtonsoft.Json;
using SlackBot.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryBot.InteractionHandlers
{
    public class AcceptPaymentRequestEvent
    {
        public PlainTextInputElement PaymentRequestComment { get; set; }
    }


    [InteractionHandler("accept-payment-request")]
    public class AcceptPaymentRequestHandler : InteractionHandler<AcceptPaymentRequestEvent>
    {
        private SalaryBotMessageSender _messageSender;

        public AcceptPaymentRequestHandler(SalaryBotMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        protected override async Task OnEvent(InteractionEvent<AcceptPaymentRequestEvent> payload)
        {
            var acceptInitiator = await _messageSender.GetUser(payload.User.Id);

            var blocks = new BlocksContainer();

            var comment = payload.State.Values.PaymentRequestComment.Value;

            blocks.Add(new Header($"{acceptInitiator.Profile.RealName} принял заявку:"))
                  .Add(new Header($"Спасибо. Если возникнут вопросы - мы свяжемся для уточнения информации."));

            if (!string.IsNullOrWhiteSpace(comment))
            {
                blocks.Add(new Header($"Комментарий:"))
                      .Add(new PlainText(comment));
            }

            var requestModel = JsonConvert.DeserializeObject<PaymentRequestInfo>(payload.Action.Value);

            var channelId = (await _messageSender.OpenConversation(userIds: requestModel.UserId)).Content.Id;

            var message = new SlackMessage
            {
                Blocks = blocks.Serialize(),
                Text = "Спасибо. Если возникнут вопросы - мы свяжемся для уточнения информации",
                Channel = channelId,
                ThreadTs = requestModel.RequestFormTs
            };

            await _messageSender.SendSlackMessage(message);

            var response = await _messageSender.FastEphemeralMessage($"Ответ отправлен", payload.Channel.Id, payload.User.Id, payload.Message.ThreadTs);
        }
    }
}
