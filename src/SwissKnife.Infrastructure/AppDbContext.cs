using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SwissKnife.API.Data.Entities;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Infrastructure
{
    /// <summary>
    ///     Application Context
    /// </summary>
    public class AppDbContext : DbContext, IUnitOfWork
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

        private readonly IMediator _mediator;

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

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}