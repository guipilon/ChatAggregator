using ChatAggregator.Api.Models;
using System;

namespace ChatAggregator.Api.Applications
{
    public interface IChatAggregatorApplication
    {
        public AggregatedChat GetAggregatedChats(Guid sessionId, int granularity);
    }
}
