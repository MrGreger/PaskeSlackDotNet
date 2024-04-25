using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class Button : BlockBase
    {
        public class Styles
        {
            public const string Primary = "primary";
            public const string Danger = "danger";
        }

        public override string Type => ElementTypes.Button;
        [JsonProperty("text")]
        public PlainText Content { get; set; }
        [JsonProperty("action_id")]
        public string ActionId { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
        [JsonProperty("style")]
        public string Style { get; set; }
        [JsonProperty("accessibility_label")]
        public string AccessibilityLabel { get; set; }
    }
}
