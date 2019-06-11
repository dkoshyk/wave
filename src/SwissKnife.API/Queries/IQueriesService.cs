using System.Collections.Generic;
using System.Threading.Tasks;
using SwissKnife.API.Data.Entities;

namespace SwissKnife.API.Queries
{
    public interface IQueriesService
    {
        Task<IList<Product>> GetAll();
    }
}