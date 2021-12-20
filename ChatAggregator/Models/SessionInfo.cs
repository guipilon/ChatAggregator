using System;

namespace ChatAggregator.Api.Models
{
    public class SessionInfo
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
