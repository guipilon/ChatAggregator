using ChatAggregator.Api.Applications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ChatAggregator.Controllers
{
    [ApiController]
    [Route("chat-aggregator")]
    public class ChatAggregatorController : ControllerBase
    {

        private readonly ILogger<ChatAggregatorController> _logger;

        private readonly IChatAggregatorApplication _chatAggregatorApplication;

        public ChatAggregatorController(ILogger<ChatAggregatorController> logger, IChatAggregatorApplication chatAggregatorApplication)
        {
            _logger = logger;
            _chatAggregatorApplication = chatAggregatorApplication;
        }

        [HttpGet]
        [Route("/{sessionId}/{granularity}")]
        public IActionResult GetAggregatedChats(Guid sessionId, int granularity)
        {
            var result = _chatAggregatorApplication.GetAggregatedChats(sessionId, granularity);
            return Ok(result.AggregatedChatEvents);
        }
    }
}
