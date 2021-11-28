using EventsBot.Helpers;
using EventsBot.Settings;
using HttpSlackBot.Blocks;
using HttpSlackBot.Messaging;
using HttpSlackBot.Models;
using MassTransit;
using PaskeApp.Events.ProfileEvents;
using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsBot.EventConsumers.Account
{
    public class AppLoginEventConsumer : IConsumer<LoggedInEvent>
    {
        private EventsBotMessageSender _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public AppLoginEventConsumer(EventsBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
        {
            _messageSender = messageSender;
            _appSettingsStorage = appSettingsStorage;
        }

        public async Task Consume(ConsumeContext<LoggedInEvent> context)
        {
            var usersToNotify = await _appSettingsStorage.GetUsersToNotify(_messageSender);

            var blocks = new BlocksContainer();

            blocks.Add(new Header($"[Authorization] Пользователь авторизовался в приложении ({context.Message.Date})"));
            blocks.Add(new PlainText($"Email: {context.Message.Email}"));
            blocks.Add(new PlainText($"Имя: {context.Message.Firstname}"));
            blocks.Add(new PlainText($"Фамилия: {context.Message.Lastname}"));
            blocks.Add(new PlainText($"Платформа: {context.Message.AuthPlatform.ToString()}"));

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
