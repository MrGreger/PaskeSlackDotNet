using Microsoft.EntityFrameworkCore;
using SlackBot.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.Database
{
    public class SlackContext : DbContext
    {
        public SlackContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SlackMessageTemplate> Templates { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }
    }
}
