using ChatAggregator.Api.Enums;
using ChatAggregator.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ChatAggregator.Api.DBContext
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CAContext(
                serviceProvider.GetRequiredService<DbContextOptions<CAContext>>()))
            {
                var sessionId1 = Guid.Parse("55a292f4-0257-402c-89d7-a8a1bf8096f6");
                var sessionId2 = Guid.Parse("9ab13fed-9c6c-4ff7-9cb2-1196cf377e05");

                var time = DateTime.Parse("2021-05-26 17:00:37");
                var time2 = DateTime.Parse("2021-06-26 17:00:37");

                if (context.SessionInfo.Any() && context.ChatEvents.Any())
                {
                    return;   // Data was already seeded
                }
                #region DataMock
                context.SessionInfo.AddRange(
                    new SessionInfo() 
                    { 
                        Id = sessionId1,
                        StartTime = time,
                        EndTime = time.AddMinutes(22)
                    },
                    new SessionInfo()
                    {
                        Id = sessionId2,
                        StartTime = time2,
                        EndTime = time2.AddHours(2).AddMinutes(26)
                    }
                );
                #region Session1
                context.ChatEvents.AddRange(
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
                    },
                #endregion
                #region Session2
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
                    });
                #endregion
                #endregion
                context.SaveChanges();
            }
        }

    }
}
