using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.Database
{
    public class SlackMessageTemplate
    {
        public string Id { get; set; }
        public string TemplateName { get; set; }
        public string Template { get; set; }
    }
}
