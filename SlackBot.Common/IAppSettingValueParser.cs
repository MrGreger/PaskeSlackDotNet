using System.Collections.Generic;

namespace SlackBot.Common
{
    public interface IAppSettingValueParser<T>
    {
        T Parse(string value);
        ICollection<T> ParseCollection(string value, string separator);
    }
}
