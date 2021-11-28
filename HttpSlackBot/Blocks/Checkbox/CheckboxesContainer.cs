using HttpSlackBot.Blocks.BaseBlocks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpSlackBot.Blocks.Checkbox
{
    public class CheckboxesContainer : BlockBase, IOptionsContainer
    {
        public override string Type => "checkboxes";
        [JsonProperty("options")]
        public IList<InputCheckboxOption> Options { get; set; }

        public T GetValue<T>(string valueName)
        {
            return (T)Options.FirstOrDefault(x => x.Option.OptionName.Equals(valueName, StringComparison.Ordinal)).Option.Value;
        }

        public IEnumerable<T> GetValues<T>(string valueName)
        {
            return Options.Where(x => x.Option.OptionName.Equals(valueName, StringComparison.OrdinalIgnoreCase)).Select(x => (T)x.Option.Value);
        }

        public bool HasValue(string valueName, bool caseSensetive = true)
        {
            return Options.Any(x => x.Option.OptionName.Equals(valueName, caseSensetive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
        }
    }
}
