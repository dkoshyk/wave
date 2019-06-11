using SwissKnife.Domain.AggregatesModel.ProductAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwissKnife.API.Application.Queries
{
    public interface IQueriesService
    {
        Task<IList<Product>> GetAll();
    }
}