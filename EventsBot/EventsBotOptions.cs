using HttpSlackBot.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsBot
{
    public class EventsBotOptions : IBotOptions
    {
        public string Token { get; set; }
    }
}
