using EventsBot.Helpers;
using HttpSlackBot.Blocks;
using HttpSlackBot.Messaging;
using MassTransit;
using PaskeApp.Events.ShopEvents;
using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsBot.EventConsumers.Shop
{
    public class ShopVerifyRequestedEventConsumer : IConsumer<ShopVerifyRequestedEvent>
    {
        private SlackMessageSender<EventsBotOptions> _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public ShopVerifyRequestedEventConsumer(EventsBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
        {
            _messageSender = messageSender;
            _appSettingsStorage = appSettingsStorage;
        }

        public async Task Consume(ConsumeContext<ShopVerifyRequestedEvent> context)
        {
            var blocks = new BlocksContainer();

            blocks.Add(new Header($"[Verification] Пользователь подал заявку на верификацию ({context.Message.Date})"));
            blocks.Add(new PlainText($"Email: {context.Message.Email}"));
            blocks.Add(new PlainText($"Url: {context.Message.Url}"));
            blocks.Add(new Header($"Магазин:"));
            blocks.Add(new PlainText($"Название: {context.Message.ShopName}"));
            blocks.Add(new PlainText($"Адрес: {context.Message.ShopAddress}"));

            var buttonsSection = new ActionsSection();
            buttonsSection.Elements.Add(new ActionButton("Принять", "accept-verification-request", value: context.Message.RequestId));
            buttonsSection.Elements.Add(new ActionButton("Отклонить", "decline-verification-request", value: context.Message.RequestId));

            blocks.Add(buttonsSection);

            var usersToNotify = await _appSettingsStorage.GetUsersToNotify(_messageSender);

            foreach (var user in usersToNotify)
            {
                await _messageSender.SendSlackMessage(new SlackMessage
                {
                    Blocks = blocks.Serialize(),
                    Channel = user.Id
                });
            }
        }
    }
}
