using HttpSlackBot.Messaging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace EventsBot
{
    public class EventsBotMessageSender : SlackMessageSender<EventsBotOptions>
    {
        public EventsBotMessageSender(IOptions<EventsBotOptions> botOptions, IHttpClientFactory httpClient) : base(botOptions, httpClient)
        {
        }
    }
}
