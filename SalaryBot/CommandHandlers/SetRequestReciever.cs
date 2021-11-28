using HttpSlackBot.CommandHandlers;
using HttpSlackBot.Commands;
using HttpSlackBot.Messaging;
using ShopList.Application.AppSettings;
using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryBot.CommandHandlers
{
    [CommandHandler("/set-payment-request-reciever")]
    public class SetRequestReciever : ICommandHandler
    {
        private SalaryBotMessageSender _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public SetRequestReciever(SalaryBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
        {
            _messageSender = messageSender;
            _appSettingsStorage = appSettingsStorage;
        }

        public async Task HandleCommand(SlackCommand payload)
        {
            var user = await _messageSender.GetUser(payload.UserId);

            if (!user.IsPrimaryOwner)
            {
                var errorMessage = new EphemeralMessage
                {
                    Text = "You must be primary owner to do this",
                    Channel = payload.ChannelId,
                    User = payload.UserId
                };

                var errorMessageSendResult = await _messageSender.SendEphemeralMessage(errorMessage);

                return;
            }

            await _appSettingsStorage.UpdateAppsetting(SalaryBotAppSettingIds.RequestRecieverEmail, payload.Text);

            var message = new EphemeralMessage
            {
                Text = $"New request reciever: {payload.Text}",
                Channel = payload.ChannelId,
                User = payload.UserId
            };

            var response = await _messageSender.SendEphemeralMessage(message);
        }
    }
}
