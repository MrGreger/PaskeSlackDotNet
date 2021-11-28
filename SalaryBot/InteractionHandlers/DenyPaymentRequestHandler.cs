using HttpSlackBot.Blocks;
using HttpSlackBot.Interactions;
using HttpSlackBot.Interactions.Elements;
using HttpSlackBot.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryBot.InteractionHandlers
{
    public class DenyPaymentRequestEvent
    {
        public PlainTextInputElement PaymentRequestComment { get; set; }
    }

    [InteractionHandler("deny-payment-request")]
    public class DenyPaymentRequestHandler : InteractionHandler<DenyPaymentRequestEvent>
    {
        private SalaryBotMessageSender _messageSender;

        public DenyPaymentRequestHandler(SalaryBotMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        protected override async Task OnEvent(InteractionEvent<DenyPaymentRequestEvent> payload)
        {
            if (string.IsNullOrWhiteSpace(payload.State.Values.PaymentRequestComment.Value))
            {
                var replies = await _messageSender.GetReplies(payload.Channel.Id, payload.Message.Ts);

                var slackMessage = new EphemeralMessage
                {
                    Attachments = new object[0],
                    Blocks = null,
                    Channel = payload.Channel.Id,
                    User = payload.User.Id,
                    Text = "Напишите почему отклонили запрос",
                    ThreadTs = payload.Message.ThreadTs,
                    Ts = null
                };

                await _messageSender.SendEphemeralMessage(slackMessage);
                return;
            }

            var denyInitiator = await _messageSender.GetUser(payload.User.Id);

            var blocks = new BlocksContainer();

            blocks.Add(new Header($"{denyInitiator.Profile.RealName} отклонил запрос на оплату:"));
            blocks.Add(new PlainText(payload.State.Values.PaymentRequestComment.Value));

            var requestModel = JsonConvert.DeserializeObject<PaymentRequestInfo>(payload.Action.Value);

            var channelId = (await _messageSender.OpenConversation(userIds: requestModel.UserId)).Content.Id;

            var message = new SlackMessage
            {
                Blocks = JsonConvert.SerializeObject(blocks.Serialize()),
                Text = "Запрос на оплату отклонен",
                Channel = channelId,
                ThreadTs = requestModel.RequestFormTs
            };

            await _messageSender.SendSlackMessage(message);

            var resultMessage = new EphemeralMessage
            {
                Text = $"Ответ отправлен",
                Attachments = new object[0],
                Blocks = null,
                Channel = payload.Channel.Id,
                User = payload.User.Id,
                ThreadTs = payload.Message.ThreadTs
            };

            await _messageSender.SendEphemeralMessage(resultMessage);
        }
    }
}
