using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class ConversationsOpenResponse : SlackResponseBase<SlackChannel>
    {
        [JsonProperty("channel")]
        public override SlackChannel Content { get; set; }
    }
}
