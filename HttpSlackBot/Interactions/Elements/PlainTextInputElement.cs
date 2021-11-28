using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Interactions.Elements
{
    public class PlainTextInputElement
    {
        public class PlainTextInputAction
        {
            [JsonProperty("value")]
            public string Value { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        [JsonProperty("plain_text_input-action")]
        public PlainTextInputAction InputAction { get; set; }

        public string Value => InputAction.Value;
        public string Type => InputAction.Type;
    }
}
