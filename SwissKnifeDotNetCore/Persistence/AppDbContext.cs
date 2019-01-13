using Microsoft.EntityFrameworkCore;
using SwissKnifeDotNetCore.Data.Entities;

namespace SwissKnifeDotNetCore.Persistence
{
    /// <summary>
    /// Application Context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            optionsBuilder.UseSqlServer(@"Server=KOSH-HP\\SQLEXPRESS;Database=SwissKnifeDB;Integrated Security=True;");
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>(entity =>
                entity.ToTable("Polls")
                );

            modelBuilder.Entity<PollOption>(entity =>
                entity.HasOne(x => x.Poll)
                .WithMany(x => x.PollOptions)
                .HasForeignKey(x => x.PollId)
                .OnDelete(DeleteBehavior.Restrict)
                );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }




    }
}