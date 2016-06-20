using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BroChatCatch.WebSocketProcessor
{
    public class BroChatMessage
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "statistic")]
        public BCStatistic Statistic { get; set; }

        [JsonProperty(PropertyName = "message")]
        public BCMessage Message { get; set; }
    }

    public class BCStatistic
    {
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }

        [JsonProperty(PropertyName = "statistic")]
        public string Statistic { get; set; }
    }

    public class BCMessage
    {
        [JsonProperty(PropertyName = "service")]
        public string Service { get; set; }

        [JsonProperty(PropertyName = "nick")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
