using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class ConversationsRepliesResponse : SlackResponseBase<IEnumerable<Message>>
    {
        [JsonProperty("messages")]
        public override IEnumerable<Message> Content { get; set; }
    }
}
