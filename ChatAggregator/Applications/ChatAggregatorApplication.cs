using ChatAggregator.Api.Models;
using ChatAggregator.Api.Repositories;
using System;
using System.Linq;

namespace ChatAggregator.Api.Applications
{
    public class ChatAggregatorApplication : IChatAggregatorApplication
    {
        private readonly IChatAggregatorRepository _chatAggregatorRepository;

        public ChatAggregatorApplication(IChatAggregatorRepository chatAggregatorRepository) 
        {
            _chatAggregatorRepository = chatAggregatorRepository;
        }

        public AggregatedChat GetAggregatedChats(Guid sessionId, int granularity)
        {
            //Will assume granularity is always in minutes

            var chatEvents = _chatAggregatorRepository.GetChatEventsBySessionId(sessionId);

            if(!chatEvents.Any())
            {
                //TODO: Add middleware to handle thrown exception
            }

            var result = new AggregatedChat();

            result.SessionId = sessionId;

            var timeToStartAggregation = chatEvents.First().CreateTime;
            var timeToEndAggregation = timeToStartAggregation.AddMinutes(granularity);
            AggregatedChatEvent aggChatEvent = new();
            aggChatEvent.Timestamp = timeToStartAggregation;

            chatEvents.ForEach(item => 
            {
                if (item.CreateTime <= timeToEndAggregation)
                {
                    aggChatEvent.Events.Add(item.Transcript);
                }
                else
                {
                    result.AggregatedChatEvents.Add(aggChatEvent);
                    timeToStartAggregation = item.CreateTime;
                    timeToEndAggregation = timeToStartAggregation.AddMinutes(granularity);
                    aggChatEvent = new();
                    aggChatEvent.Timestamp = timeToStartAggregation;
                    aggChatEvent.Events.Add(item.Transcript);
                }
            });

            if(aggChatEvent.Events.Any())
            {
                result.AggregatedChatEvents.Add(aggChatEvent);
            }

            return result;
        }
    }
}
