using SwissKnifeDotNetCore.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SwissKnifeDotNetCore.Queries
{
    public interface IQueriesService
    {
        Task<IList<Product>> GetAll();
    }
}
