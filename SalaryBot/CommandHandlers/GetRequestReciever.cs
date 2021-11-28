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
    [CommandHandler("/get-payment-request-reciever")]
    public class GetRequestReciever: ICommandHandler
    {
        private SalaryBotMessageSender _messageSender;
        private IAppSettingsStorage _appSettingsStorage;

        public GetRequestReciever(SalaryBotMessageSender messageSender, IAppSettingsStorage appSettingsStorage)
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

            var recieverEmail = await _appSettingsStorage.GetAppSettingByIdAsync(SalaryBotAppSettingIds.RequestRecieverEmail, new StringParser());

            var message = new EphemeralMessage
            {
                Text = $"Current request reciever: {recieverEmail}",
                Channel = payload.ChannelId, User = payload.UserId

            };

            var response = await _messageSender.SendEphemeralMessage(message);
        }
    }
}
