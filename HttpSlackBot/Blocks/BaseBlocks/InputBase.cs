using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks.BaseBlocks
{
    public abstract class InputBase : BlockBase
    {
        protected InputBase(string blockId = null) : base(blockId)
        {
        }

        public override string Type => "input";
        [JsonProperty("label")]
        public virtual TextAttribute Label { get; } = new TextAttribute("plain_text", true);
        [JsonProperty("element")]
        public abstract BlockBase Element { get; }
    }
}
