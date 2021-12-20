using ChatAggregator.Api.DBContext;
using ChatAggregator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatAggregator.Api.Repositories
{
    public class ChatAggregatorRepository : IChatAggregatorRepository
    {
        private readonly CAContext _context;

        public ChatAggregatorRepository(CAContext context) 
        {
            _context = context;
        }

        public List<ChatEvent> GetChatEventsBySessionId(Guid sessionId)
        {
            var result = _context.ChatEvents.Where(c => c.SessionId.Equals(sessionId));

            return result.ToList();
        }
    }
}
