using ChatAggregator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAggregator.Api.Repositories
{
    public interface IChatAggregatorRepository
    {
        List<ChatEvent> GetChatEventsBySessionId(Guid sessionId);
    }
}
