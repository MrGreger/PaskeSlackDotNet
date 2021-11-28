using HttpSlackBot.CommandHandlers;
using HttpSlackBot.Commands;
using HttpSlackBot.Messaging;
using HttpSlackBot.Templates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryBot.CommandHandlers
{
    [CommandHandler("/request-payment")]
    public class RequestPaymentCommand : ICommandHandler
    {
        private SalaryBotMessageSender _messageSender;
        private ITemplatesProvider _templateProvider;

        public RequestPaymentCommand(SalaryBotMessageSender messageSender, ITemplatesProvider templateProvider)
        {
            _messageSender = messageSender;
            _templateProvider = templateProvider;
        }

        public async Task HandleCommand(SlackCommand payload)
        {
            var messageTemplate = await _templateProvider.GetMessageTemplate("SalaryRequestForm");

            var message = new SlackMessage
            {
                Blocks = messageTemplate.Template,
                Text = "Please, fill the form",
                Channel = payload.ChannelId
            };

            var response = await _messageSender.SendSlackMessage(message);
        }
    }
}
