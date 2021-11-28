using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackBot.Common
{
    public interface IAppSettingsStorage
    {
        Task<AppSetting> GetAppSettingByIdAsync(string appsettingId);
        Task<T> GetAppSettingByIdAsync<T>(string appsettingId, IAppSettingValueParser<T> valueParser);
        Task<ICollection<T>> GetAppSettingCollectionByIdAsync<T>(string appsettingId, string appSettingSeparator, IAppSettingValueParser<T> valueParser);

        Task UpdateAppsetting(string appsettingId, string value);
    }
}
