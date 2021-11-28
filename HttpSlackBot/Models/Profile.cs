using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Models
{
    public class Profile
    {
        [JsonProperty("real_name")]
        public string RealName { get; set; }
    }
}
