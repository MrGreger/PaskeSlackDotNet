using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public class BlocksContainer
    {
        public IReadOnlyCollection<BlockBase> Blocks => _blocks.AsReadOnly();

        private List<BlockBase> _blocks;

        public BlocksContainer()
        {
            _blocks = new List<BlockBase>();
        }

        public BlocksContainer Add(BlockBase block)
        {
            _blocks.Add(block);
            return this;
        }

        public BlocksContainer AddRange(IEnumerable<BlockBase> blocks)
        {
            _blocks.AddRange(blocks);
            return this;
        }

        public BlocksContainer Remove(BlockBase block)
        {
            _blocks.Remove(block);
            return this;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(_blocks, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
