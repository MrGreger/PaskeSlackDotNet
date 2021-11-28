using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Models
{
    public class SlackChannel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("is_im")]
        public bool IsIM { get; set; }
        [JsonProperty("user")]
        public string UserId { get; set; }
    }
}
