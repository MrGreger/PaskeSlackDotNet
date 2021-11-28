using HttpSlackBot.Messaging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SalaryBot
{
    public class SalaryBotMessageSender : SlackMessageSender<SalaryBotOptions>
    {
        public SalaryBotMessageSender(IOptions<SalaryBotOptions> botOptions, IHttpClientFactory httpClient) : base(botOptions, httpClient)
        {
        }
    }
}
