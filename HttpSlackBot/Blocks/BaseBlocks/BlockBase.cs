using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks.BaseBlocks
{
    public abstract class BlockBase
    {
        [JsonProperty("type")]
        public abstract string Type { get; }
        [JsonProperty("block_id")]
        public string BlockId { get; set; }

        protected BlockBase(string blockId = null)
        {
            BlockId = blockId;
        }
    }
}
