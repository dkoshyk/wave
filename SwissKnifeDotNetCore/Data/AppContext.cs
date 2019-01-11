using Microsoft.EntityFrameworkCore;
using SwissKnifeDotNetCore.Data.Entities;

namespace SwissKnifeDotNetCore.Data
{
    /// <summary>
    /// Application Context
    /// </summary>
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options): base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }  
        public DbSet<PollOption> PollOptions { get; set; }
    }
}