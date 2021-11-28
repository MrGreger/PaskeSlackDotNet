using System;
using System.Collections.Generic;

namespace SlackBot.Common
{
    public class IntParser : IAppSettingValueParser<int>
    {
        private readonly int? _defaultValue;

        public IntParser(int? defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        public int Parse(string value)
        {
            var parseResult = int.TryParse(value, out var result);

            if (!parseResult)
            {
                return _defaultValue ?? throw new Exception("Can not parse setting.");
            }

            return result;
        }

        public ICollection<int> ParseCollection(string value, string separator)
        {
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<int>();

            foreach (var val in values)
            {
                result.Add(Parse(val));
            }

            return result;
        }
    }

    public class FloatParser : IAppSettingValueParser<float>
    {
        private readonly float? _defaultValue;

        public FloatParser(float? defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        public float Parse(string value)
        {
            value = value.Replace('.', ',');

            var parseResult = float.TryParse(value, out var result);

            if (!parseResult)
            {
                return _defaultValue ?? throw new Exception("Can not parse setting.");
            }

            return result;
        }

        public ICollection<float> ParseCollection(string value, string separator)
        {
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<float>();

            foreach (var val in values)
            {
                result.Add(Parse(val));
            }

            return result;
        }
    }

    public class DecimalParser : IAppSettingValueParser<decimal>
    {
        private readonly decimal? _defaultValue;

        public DecimalParser(decimal? defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        public decimal Parse(string value)
        {
            var parseResult = decimal.TryParse(value, out var result);

            if (!parseResult)
            {
                return _defaultValue ?? throw new Exception("Can not parse setting.");
            }

            return result;
        }

        public ICollection<decimal> ParseCollection(string value, string separator)
        {
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<decimal>();

            foreach (var val in values)
            {
                result.Add(Parse(val));
            }

            return result;
        }
    }

    public class BoolParser : IAppSettingValueParser<bool>
    {
        private readonly bool? _defaultValue;

        public BoolParser(bool? defaultValue = null)
        {
            _defaultValue = defaultValue;
        }

        public bool Parse(string value)
        {
            var parseResult = bool.TryParse(value, out var result);

            if (!parseResult)
            {
                return _defaultValue ?? throw new Exception("Can not parse setting.");
            }

            return result;
        }

        public ICollection<bool> ParseCollection(string value, string separator)
        {
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<bool>();

            foreach (var val in values)
            {
                result.Add(Parse(val));
            }

            return result;
        }
    }

    public class StringParser : IAppSettingValueParser<string>
    {
        public string Parse(string value)
        {
            return value;
        }

        public ICollection<string> ParseCollection(string value, string separator)
        {
            var values = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            var result = new List<string>();

            foreach (var val in values)
            {
                result.Add(Parse(val));
            }

            return result;
        }
    }
}
