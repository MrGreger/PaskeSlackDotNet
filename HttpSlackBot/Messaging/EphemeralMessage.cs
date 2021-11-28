using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class EphemeralMessage : SlackMessage
    {
        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }
        [JsonProperty("user")]
        public string User { get; set; }
    }
}
