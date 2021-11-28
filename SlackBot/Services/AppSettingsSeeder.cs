using SlackBot.Common;
using SlackBot.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SlackBot.Services
{
    public static class AppSettingsSeeder
    {
        public static void SeedSettings(this SlackContext context, params Type[] settingsFileTypes)
        {
            foreach (var settingsFileType in settingsFileTypes)
            {
                var settings = settingsFileType.GetProperties();

                var initialSettings = new List<AppSetting>();

                foreach (var setting in settings)
                {
                    var settingNameAttr = setting.GetCustomAttribute<AppSettingNameAttribute>();

                    var settingName = settingNameAttr?.Name ?? setting.Name;

                    var settingValueAttr = setting.GetCustomAttribute<AppSettingDefaultValueAttribute>();

                    var settingValue = settingValueAttr?.Value;

                    initialSettings.Add(new AppSetting
                    {
                        Id = (string)setting.GetValue(setting),
                        Name = settingName
                    });
                }

                var createdIds = initialSettings.Select(x => x.Id.ToUpper());

                var settingsInDb = context.AppSettings.Where(x => createdIds.Contains(x.Id.ToUpper())).ToList();

                var settingsInDbId = settingsInDb.Select(x => x.Id.ToUpper());

                initialSettings = initialSettings.Where(x => !settingsInDbId.Contains(x.Id.ToUpper())).ToList();

                context.AppSettings.AddRange(initialSettings);
                context.SaveChanges(); 
            }
        }
    }
}
