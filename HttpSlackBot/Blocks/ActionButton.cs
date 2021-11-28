using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class ActionButton : BlockBase
    {
        public ActionButton(string text, string actionId, string value = null, bool allowEmoji = false)
        {
            Content = new TextAttribute("plain_text", allowEmoji);
            Content.Value = text;
            ActionId = actionId;
            Value = value;
        }

        public override string Type => "button";
        [JsonProperty("text")]
        public TextAttribute Content { get; } = new TextAttribute("plain_text",false);
        [JsonProperty("action_id")]
        public string ActionId { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
