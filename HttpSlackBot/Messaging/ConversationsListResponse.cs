using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class ConversationsListResponse : SlackResponseBase<IEnumerable<SlackChannel>>
    {
        [JsonProperty("channels")]
        public override IEnumerable<SlackChannel> Content { get; set; }
    }
}
