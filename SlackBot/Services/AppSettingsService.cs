using Microsoft.EntityFrameworkCore;
using SlackBot.Common;
using SlackBot.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlackBot.Services
{
    public class AppSettingsService : IAppSettingsStorage
    {
        private SlackContext _context;

        public AppSettingsService(SlackContext context)
        {
            _context = context;
        }

        public async Task<AppSetting> GetAppSettingByIdAsync(string appsettingId)
        {
            return await _context.AppSettings.FirstOrDefaultAsync(x => EF.Functions.ILike(x.Id, $"{appsettingId}"));
        }

        public async Task<T> GetAppSettingByIdAsync<T>(string appsettingId, IAppSettingValueParser<T> valueParser)
        {
            var setting = await _context.AppSettings.FirstAsync(x => EF.Functions.ILike(x.Id, $"{appsettingId}"));

            return valueParser.Parse(setting.Value);
        }

        public async Task<ICollection<T>> GetAppSettingCollectionByIdAsync<T>(string appsettingId,
                                                                              string appSettingSeparator, IAppSettingValueParser<T> valueParser)
        {
            var setting = await _context.AppSettings.FirstAsync(x => EF.Functions.ILike(x.Id, $"{appsettingId}"));

            return valueParser.ParseCollection(setting.Value, appSettingSeparator);
        }

        public async Task UpdateAppsetting(string appsettingId, string value)
        {
            var setting = await _context.AppSettings.FirstAsync(x => EF.Functions.ILike(x.Id, $"{appsettingId}"));
            setting.Value = value;
            _context.AppSettings.Update(setting);
            await _context.SaveChangesAsync();
        }
    }
}
