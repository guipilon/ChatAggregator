using System;
using System.Collections.Generic;

namespace ChatAggregator.Api.Models
{
    public class AggregatedChatEvent
    {
        public DateTime Timestamp { get; set; }

        public List<string> Events { get; set; } = new();
    }
}
