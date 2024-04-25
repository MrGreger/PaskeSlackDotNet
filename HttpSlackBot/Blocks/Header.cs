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
            HeaderText = new TextAttribute(TextTypes.Plain,withEmoji);
            HeaderText.Value = text;
        }

        public override string Type => BlockTypes.Header;
        [JsonProperty("text")]
        public TextAttribute HeaderText { get; }
    }
}
