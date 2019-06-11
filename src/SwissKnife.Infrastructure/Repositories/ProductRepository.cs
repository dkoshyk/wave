using SwissKnife.Domain.AggregatesModel.ProductAggregate;

namespace SwissKnife.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
    }
}
