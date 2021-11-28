using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class SlackResponseBase
    {
        [JsonProperty("ok")]
        public bool Success { get; set; }
    }

    public abstract class SlackResponseBase<T> : SlackResponseBase
    {
        public abstract T Content { get; set; }
    }
}
