using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.DTOs
{
    public class SlackCommandDTO
    {
        [FromForm(Name = "command")]
        public string Command { get; set; }
        [FromForm(Name = "text")]
        public string Text { get; set; }
        [FromForm(Name = "response_url")]
        public string ResponseUrl { get; set; }
        [FromForm(Name = "trigger_id")]
        public string TriggerId { get; set; }
        [FromForm(Name = "user_id")]
        public string UserId { get; set; }
        [FromForm(Name = "user_name")]
        public string UserName { get; set; }
        [FromForm(Name = "channel_id")]
        public string ChannelId { get; set; }
    }
}
