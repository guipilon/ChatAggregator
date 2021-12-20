using System;
using System.Collections.Generic;

namespace ChatAggregator.Api.Models
{
    public class AggregatedChat
    {
        public Guid SessionId { get; set; }
        public List<AggregatedChatEvent> AggregatedChatEvents { get; set; } = new();
    }
}
