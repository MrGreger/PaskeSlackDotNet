using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Interactions
{
    public class InteractionAction
    {
        [JsonProperty("action_id")]
        public string ActionId { get; set; }
        [JsonProperty("block_id")]
        public string BlockId { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
