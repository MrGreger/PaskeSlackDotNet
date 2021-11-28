using EventsBot.Settings;
using HttpSlackBot.Messaging;
using HttpSlackBot.Models;
using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsBot.Helpers
{
    public static class SlackHelpers
    {
        public async static Task<IEnumerable<User>> GetUsersToNotify(this IAppSettingsStorage appSettingsStorage, SlackMessageSender<EventsBotOptions> messageSender)
        {
            var emailsToNotify = await appSettingsStorage.GetAppSettingCollectionByIdAsync(EventsBotSettings.EventRecieverEmails, ";", new StringParser());

            return await messageSender.GetUserProfiles(emailsToNotify);
        }
    }
}
