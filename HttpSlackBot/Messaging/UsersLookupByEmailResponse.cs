using HttpSlackBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Messaging
{
    public class UsersLookupByEmailResponse : SlackResponseBase<User>
    {
        [JsonProperty("user")]
        public override User Content { get; set; }
    }
}
