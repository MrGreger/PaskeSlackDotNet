using System;

namespace SlackBot.Common
{
    public class AppSetting 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public bool TryGetIntValue(out int result)
        {
            return int.TryParse(Value, out result);
        }

        public bool TryGetDecimalValue(out decimal result)
        {
            return decimal.TryParse(Value, out result);
        }

        public bool TryGetFloatValue(out float result)
        {
            return float.TryParse(Value, out result);
        }

        public bool TryGetBoolValue(out bool result)
        {
            return bool.TryParse(Value, out result);
        }
    }

    public class AppSettingNameAttribute : Attribute
    {
        public string Name { get; }

        public AppSettingNameAttribute(string name)
        {
            Name = name;
        }
    }

    public class AppSettingDefaultValueAttribute : Attribute
    {
        public string Value { get; }

        public AppSettingDefaultValueAttribute(object value)
        {
            Value = value.ToString();
        }
    }
}
