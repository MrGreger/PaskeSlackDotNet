using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class TextAttribute
    {
        public TextAttribute(string type, bool emoji)
        {
            Type = type;
            Emoji = emoji;
        }

        [JsonProperty("type")]
        public string Type { get; }
        [JsonProperty("emoji")]
        public bool Emoji { get; set; }
        [JsonProperty("text")]
        public string Value { get; set; }
    }
}
