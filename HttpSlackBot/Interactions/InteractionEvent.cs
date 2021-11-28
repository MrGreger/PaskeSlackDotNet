using HttpSlackBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpSlackBot.Interactions
{
    public class InteractionEvent
    {
        [JsonProperty("channel")]
        public SlackChannel Channel { get; set; }
        [JsonProperty("user")]
        public User User { get; set; }
        [JsonProperty("state")]
        public JObject SerializedState { get; set; }
        [JsonProperty("message")]
        public Message Message { get; set; }
        [JsonProperty("actions")]
        public InteractionAction[] InteractionActions { get; set; }
        public InteractionAction Action => InteractionActions?.First();

    }

    public class InteractionEvent<TState> : InteractionEvent
    {
        public static InteractionEvent<TState> FromBaseEvent(InteractionEvent baseEvent)
        {
            var serialized = JsonConvert.SerializeObject(baseEvent);
            return JsonConvert.DeserializeObject<InteractionEvent<TState>>(serialized);
        }

        private InteractionState<TState> _state;
        public InteractionState<TState> State
        {
            get
            {
                if(SerializedState == null)
                {
                    return null;
                }

                if (_state == null)
                {
                    _state = SerializedState.ToObject<InteractionState<TState>>();
                }

                return _state;
            }
        }
    }
}
