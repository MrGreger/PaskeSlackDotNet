using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SlackBot.Helpers
{
    public class SlackSerialization
    {
        public static NullValueHandling NullValueHandling => NullValueHandling.Ignore;
        public static JsonSerializerSettings SerializerSettings { get; } = new JsonSerializerSettings { NullValueHandling = NullValueHandling };
    }
}
