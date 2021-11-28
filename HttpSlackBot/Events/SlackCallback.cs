using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Events
{
    public class SlackCallback
    {
        public string Type { get; set; }
        public string Challenge { get; set; }
        public SlackBaseEvent Event => EventObject.ToObject<SlackBaseEvent>();
        [JsonProperty("event")]
        public JObject EventObject { get; set; }
    }
}
