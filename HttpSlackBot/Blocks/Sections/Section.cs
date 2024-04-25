using System;
using System.Linq;
using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;

namespace HttpSlackBot.Blocks
{
    public class SectionConstraints
    {
        private static string[] _availableElements = new[]
        {
            ElementTypes.Button,
            ElementTypes.CheckBoxes,
        };

        public bool CanAdd(BlockBase block)
        {
            return _availableElements.Contains(block.Type, StringComparer.InvariantCulture);
        }
    }

    public class Section : BlockBase
    {
        public override string Type => BlockTypes.Section;

        [JsonProperty("text")]
        public TextAttribute Text { get; set; }
        [JsonProperty("fields")]
        public TextAttribute[] Fields { get; set; }
        [JsonProperty("accessory")]
        public BlockBase Acessory { get; set; }
    }
}