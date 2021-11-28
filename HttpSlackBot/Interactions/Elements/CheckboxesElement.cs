using HttpSlackBot.Blocks;
using HttpSlackBot.Blocks.Checkbox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpSlackBot.Interactions.Elements
{
    public class CheckboxesElement : IOptionsContainer
    {
        public class CheckboxesElementAction
        {
            [JsonProperty("selected_options")]
            public CheckboxOption[] Options { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class CheckboxOption
        {
            private string _serializedValue;

            [JsonProperty("value")]
            public string SerializedValue
            {
                get { return _serializedValue; }
                set
                {
                    _serializedValue = value;
                    Option = JsonConvert.DeserializeObject<CheckboxValue>(_serializedValue);
                }
            }

            public CheckboxValue Option { get; private set; }
        }

        [JsonProperty("checkboxes-action")]
        public CheckboxesElementAction InputAction { get; set; }

        public IEnumerable<CheckboxOption> SelectedOptions => InputAction.Options;
        public string Type => InputAction.Type;

        public T GetValue<T>(string valueName)  
        {
            return (T)SelectedOptions.FirstOrDefault(x => x.Option.OptionName.Equals(valueName, StringComparison.Ordinal)).Option.Value;
        }

        public IEnumerable<T> GetValues<T>(string valueName)
        {
            return SelectedOptions.Where(x => x.Option.OptionName.Equals(valueName, StringComparison.OrdinalIgnoreCase)).Select(x => (T)x.Option.Value);
        }

        public bool HasValue(string valueName, bool caseSensetive = true)
        {
            return SelectedOptions.Any(x => x.Option.OptionName.Equals(valueName, caseSensetive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase));
        }
    }
}
