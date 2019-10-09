using System.Collections.Generic;
using System.Linq;
using SwissKnife.Domain.SeedWork;

namespace SwissKnife.Domain.AggregatesModel.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Product Add(Product product);
        IEnumerable<Product> GetAll();
        IQueryable<Product> GetQuery();
    }
}
