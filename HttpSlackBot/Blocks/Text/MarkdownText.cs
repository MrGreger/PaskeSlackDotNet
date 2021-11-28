using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class MarkdownText : BlockBase
    {
        public MarkdownText(bool withEmoji)
        {
            Text = new TextAttribute("mrkdwn", withEmoji);
        }

        public override string Type => "section";
        [JsonProperty("text")]
        public TextAttribute Text { get; }
    }
}
