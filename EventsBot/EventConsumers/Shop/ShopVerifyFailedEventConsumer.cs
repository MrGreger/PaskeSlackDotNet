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
    public class ShopVerifyFailedEventConsumer : IConsumer<ShopVerifyFailedEvent>
    {
        private SlackMessageSender<EventsBotOptions> _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public ShopVerifyFailedEventConsumer(EventsBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
        {
            _messageSender = messageSender;
            _appSettingsStorage = appSettingsStorage;
        }

        public async Task Consume(ConsumeContext<ShopVerifyFailedEvent> context)
        {
            var usersToNotify = await _appSettingsStorage.GetUsersToNotify(_messageSender);

            var blocks = new BlocksContainer();

            blocks.Add(new Header($"[Verification] Верификация не удалась ({context.Message.Date})"));
            blocks.Add(new PlainText($"Email: {context.Message.Email}"));
            blocks.Add(new Header($"Магазин:"));
            blocks.Add(new PlainText($"Название: {context.Message.ShopName}"));
            blocks.Add(new PlainText($"Адрес: {context.Message.ShopAddress}"));

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
