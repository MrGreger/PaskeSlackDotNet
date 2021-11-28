using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class ChatPostMessageResponse : SlackResponseBase<Message>
    {
        [JsonProperty("message")]
        public override Message Content { get; set; }
    }
}
