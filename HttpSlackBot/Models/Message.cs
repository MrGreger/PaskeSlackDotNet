using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Models
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("blocks")]
        public JObject[] Blocks { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("ts")]
        public string Ts { get; set; }
        [JsonProperty("thread_ts")]
        public string ThreadTs { get; set; }
    }
}
