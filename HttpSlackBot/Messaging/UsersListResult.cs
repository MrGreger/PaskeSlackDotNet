using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class UsersListResult : SlackResponseBase<IEnumerable<User>>
    {
        [JsonProperty("members")]
        public override IEnumerable<User> Content { get; set; }
    }
}
