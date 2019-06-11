using Microsoft.EntityFrameworkCore;
using SwissKnife.API.Data.Entities;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;

namespace SwissKnife.Infrastructure
{
    /// <summary>
    ///     Application Context
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<PollOption> PollOptions { get; set; }
        public DbSet<Product> Products { get; set; }

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

            modelBuilder.Entity<Product>(entity =>
                entity.ToTable("Products")
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}