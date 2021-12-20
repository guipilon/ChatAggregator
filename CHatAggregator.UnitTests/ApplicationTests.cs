using ChatAggregator.Api.Applications;
using ChatAggregator.Api.Enums;
using ChatAggregator.Api.Models;
using ChatAggregator.Api.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace CHatAggregator.UnitTests
{
    [TestClass]
    public class ApplicationTests
    {
        private IChatAggregatorApplication _chatAggregatorApplication;
        private readonly Mock<IChatAggregatorRepository> _chatAggregatorRepositoryMock;

        public ApplicationTests()
        {
            _chatAggregatorRepositoryMock = new Mock<IChatAggregatorRepository>();
        }

        [DataTestMethod]
        [DataRow("55a292f4-0257-402c-89d7-a8a1bf8096f6", 1, 5)]
        [DataRow("55a292f4-0257-402c-89d7-a8a1bf8096f6", 5, 3)]
        [DataRow("9ab13fed-9c6c-4ff7-9cb2-1196cf377e05", 15, 3)]
        [DataRow("9ab13fed-9c6c-4ff7-9cb2-1196cf377e05", 60, 2)]
        public void GetAggregatedChats_Success(string id, int aggregator, int expectedNumberOfAggregations)
        {
            var sessionId1 = Guid.Parse("55a292f4-0257-402c-89d7-a8a1bf8096f6");
            var sessionId2 = Guid.Parse("9ab13fed-9c6c-4ff7-9cb2-1196cf377e05");

            var time = DateTime.Parse("2021-05-26 17:00:37");
            var time2 = DateTime.Parse("2021-06-26 17:00:37");

            var mockedData1 = new List<ChatEvent>() {
                    new ChatEvent()
                    {
                        CreateTime = time,
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId1,
                        Event = EventType.ENTER_THE_ROOM,
                        Transcript = "Bob enters the room"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(5),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId1,
                        Event = EventType.ENTER_THE_ROOM,
                        Transcript = "Kate enters the room"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(15),
                        Id = Guid.NewGuid(),
                        Sender = Sender.USER,
                        SessionId = sessionId1,
                        Event = EventType.COMMENT,
                        Transcript = $"Bob comments: \"Hey, Kate - high five ? \""
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(17),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId1,
                        Event = EventType.HIGH_FIVE_ANOTHER_USER,
                        Transcript = "Kate high-fives Bob"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(18),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId1,
                        Event = EventType.LEAVE_THE_ROOM,
                        Transcript = "Bob leaves"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(20),
                        Id = Guid.NewGuid(),
                        Sender = Sender.USER,
                        SessionId = sessionId1,
                        Event = EventType.COMMENT,
                        Transcript = "Kate comments: \"Oh, typical\""
                    },
                    new ChatEvent()
                    {
                        CreateTime = time.AddMinutes(21),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId1,
                        Event = EventType.LEAVE_THE_ROOM,
                        Transcript = "Kate leaves"
                    }};
            var mockedData2 = new List<ChatEvent>() {
                    new ChatEvent()
                    {
                        CreateTime = time2,
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId2,
                        Event = EventType.ENTER_THE_ROOM,
                        Transcript = "Carlos enters the room"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddMinutes(5),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId2,
                        Event = EventType.ENTER_THE_ROOM,
                        Transcript = "Denis enters the room"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddMinutes(15),
                        Id = Guid.NewGuid(),
                        Sender = Sender.USER,
                        SessionId = sessionId2,
                        Event = EventType.COMMENT,
                        Transcript = $"Carlos comments: \"Hey, Denis - high five ? \""
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddHours(2),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId2,
                        Event = EventType.HIGH_FIVE_ANOTHER_USER,
                        Transcript = "Denis high-fives Carlos"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddHours(2).AddMinutes(2),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId2,
                        Event = EventType.LEAVE_THE_ROOM,
                        Transcript = "Carlos leaves"
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddHours(2).AddMinutes(20),
                        Id = Guid.NewGuid(),
                        Sender = Sender.USER,
                        SessionId = sessionId2,
                        Event = EventType.COMMENT,
                        Transcript = "Denis comments: \"Oh, typical\""
                    },
                    new ChatEvent()
                    {
                        CreateTime = time2.AddHours(2).AddMinutes(26),
                        Id = Guid.NewGuid(),
                        Sender = Sender.SYSTEM,
                        SessionId = sessionId2,
                        Event = EventType.LEAVE_THE_ROOM,
                        Transcript = "Denis leaves"
                    }};

            _chatAggregatorRepositoryMock.Setup(x => x.GetChatEventsBySessionId(sessionId1)).Returns(mockedData1);
            _chatAggregatorRepositoryMock.Setup(x => x.GetChatEventsBySessionId(sessionId2)).Returns(mockedData2);

            _chatAggregatorApplication = new ChatAggregatorApplication(_chatAggregatorRepositoryMock.Object);
            var result = _chatAggregatorApplication.GetAggregatedChats(Guid.Parse(id), aggregator);

            Assert.AreEqual(expectedNumberOfAggregations, result.AggregatedChatEvents.Count);
        }
    }
}
