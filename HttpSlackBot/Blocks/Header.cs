using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class Header : BlockBase
    {
        public Header(string text, bool withEmoji = false)
        {
            HeaderText = new TextAttribute("plain_text",withEmoji);
            HeaderText.Value = text;
        }

        public override string Type => "header";
        [JsonProperty("text")]
        public TextAttribute HeaderText { get; }
    }
}
