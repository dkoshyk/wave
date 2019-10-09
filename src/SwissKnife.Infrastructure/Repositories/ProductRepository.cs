using System.Collections.Generic;
using System.Linq;
using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public Product Add(Product product)
        {
            return _context.Products.Add(product).Entity;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IQueryable<Product> GetQuery()
        {
            return _context.Products;
        }
    }
}
