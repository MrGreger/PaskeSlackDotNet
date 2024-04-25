using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class MarkdownTextSection : BlockBase
    {
        public MarkdownTextSection(bool withEmoji)
        {
            Text = new TextAttribute(TextTypes.Markdown, withEmoji);
        }

        public override string Type => BlockTypes.Section;
        [JsonProperty("text")]
        public TextAttribute Text { get; }
    }
}
