using ChatAggregator.Api.Enums;
using System;

namespace ChatAggregator.Api.Models
{
    public class ChatEvent
    {
        public Guid SessionId { get; set; }
        public Guid Id { get; set; }
        public EventType Event { get; set; }
        public Sender Sender { get; set; }
        public string Transcript { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
