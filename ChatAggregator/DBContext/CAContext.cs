using ChatAggregator.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAggregator.Api.DBContext
{
    public class CAContext : DbContext
    {
        public CAContext(DbContextOptions<CAContext> options)
        : base(options) { }

        public DbSet<SessionInfo> SessionInfo { get; set; }
        public DbSet<ChatEvent> ChatEvents { get; set; }
    }
}
