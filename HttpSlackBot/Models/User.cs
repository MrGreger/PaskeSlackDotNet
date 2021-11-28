using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Models
{
    public class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("team_id")]
        public string TeamId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("profile")]
        public Profile Profile { get; set; }
        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }
        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }
        [JsonProperty("is_primary_owner")]
        public bool IsPrimaryOwner { get; set; }
    }
}
