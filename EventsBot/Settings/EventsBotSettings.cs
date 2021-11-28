using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsBot.Settings
{
    public class EventsBotSettings
    {
        [AppSettingName("Events reciever emails (email;email;...)")]
        public static string EventRecieverEmails { get; } = "0a8ffb15-4c6f-4697-ab8b-9c74b217a88a";
    }
}
