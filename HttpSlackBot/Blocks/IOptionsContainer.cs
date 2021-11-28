using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Blocks
{
    public interface IOptionsContainer
    {
        bool HasValue(string valueName, bool caseSensetive = true);
        T GetValue<T>(string valueName);
        IEnumerable<T> GetValues<T>(string valueName);
    }
}
