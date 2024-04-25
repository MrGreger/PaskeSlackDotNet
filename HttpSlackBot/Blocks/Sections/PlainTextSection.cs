using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class PlainTextSection : Section
    {
        public PlainTextSection(bool withEmoji)
        {
            Text = new TextAttribute(TextTypes.Plain, withEmoji);
        }

        public PlainTextSection(string text, bool withEmoji = false) : this(withEmoji)
        {
            Text.Value = text;
        }
    }
}
