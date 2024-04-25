using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class TextInput : BlockBase
    {
        public TextInput(bool multiline = false)
        {
            Multiline = multiline;
        }

        public override string Type => "plain_text_input";
        [JsonProperty("multiline")]
        public bool Multiline { get; set; }
        [JsonProperty("action_id")]
        public string ActionId { get; set; } = "plain_text_input-action";
    }
}
