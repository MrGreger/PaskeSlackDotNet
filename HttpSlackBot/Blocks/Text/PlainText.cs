using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class PlainText : BlockBase
    {
        public PlainText(bool withEmoji)
        {
            Text = new TextAttribute("plain_text", withEmoji);
        }

        public PlainText(string text, bool withEmoji = false) : this(withEmoji)
        {
            Text.Value = text;
        }

        public override string Type => "section";
        [JsonProperty("text")]
        public TextAttribute Text { get; set; }
    }
}
