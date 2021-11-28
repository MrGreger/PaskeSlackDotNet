using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpSlackBot.Interactions.Elements
{
    public class UsersMultiSelect
    {
        public class UsersMultiSelectAction
        {
            [JsonProperty("selected_users")]
            public string[] SelectedUsers { get; set; }
            [JsonProperty("type")]
            public string Type { get; set; }
        }

        [JsonProperty("multi_users_select-action")]
        public UsersMultiSelectAction InputAction { get; set; }

        public string[] SelectedUsers => InputAction.SelectedUsers;
        public string Type => InputAction.Type;
    }
}
