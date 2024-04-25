using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class ActionsSection : BlockBase
    {
        public override string Type => BlockTypes.Actions;
        [JsonProperty("elements")]
        public IList<BlockBase> Elements { get; set; }

        public ActionsSection()
        {
            Elements = new List<BlockBase>();
        }
    }
}
