using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Commands
{
    public class SlackCommand
    {
        public string Command { get; set; }
        public string Text { get; set; }
        public string ResponseUrl { get; set; }
        public string TriggerId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string ChannelId { get; set; }
    }
}
