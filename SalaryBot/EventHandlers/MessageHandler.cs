using HttpSlackBot.EventHandlers;
using HttpSlackBot.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SalaryBot.EventHandlers.MessageHandler;

namespace SalaryBot.EventHandlers
{
    [EventHandler("message")]
    public class MessageHandler : SlackEventHandler<MessageEvent>
    {
        public class MessageEvent : SlackBaseEvent
        {
            public string Channel { get; set; }
            public string User { get; set; }
            public string Text { get; set; }
            public string Ts { get; set; }
        }
        protected override Task OnEvent(MessageEvent payload)
        {
            throw new NotImplementedException();
        }

    }
}
