using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks.Checkbox
{
    public struct CheckboxValue : IEquatable<CheckboxValue>
    {
        public string OptionName { get; set; }
        public object Value { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CheckboxValue value &&
                   OptionName == value.OptionName &&
                   EqualityComparer<object>.Default.Equals(Value, value.Value);
        }

        public bool Equals(CheckboxValue other)
        {
            return OptionName == other.OptionName &&
                   EqualityComparer<object>.Default.Equals(Value, other.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(OptionName, Value);
        }

        public T GetValue<T>()
        {
            return (T)Value;
        }
    }
}
