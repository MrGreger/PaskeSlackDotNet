using HttpSlackBot.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryBot
{
    public class SalaryBotOptions : IBotOptions
    {
        public string Token { get; set; }
    }
}
