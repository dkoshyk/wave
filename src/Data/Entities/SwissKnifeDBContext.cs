using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public partial class SwissKnifeDBContext : DbContext
    {
        public virtual DbSet<PollOptions> PollOptions { get; set; }
        public virtual DbSet<Polls> Polls { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=KOSH-HP;Database=SwissKnifeDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PollOptions>(entity =>
            {
                entity.HasIndex(e => e.PollId);

                entity.HasOne(d => d.Poll)
                    .WithMany(p => p.PollOptions)
                    .HasForeignKey(d => d.PollId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
