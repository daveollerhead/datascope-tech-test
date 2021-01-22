using System.Collections.Generic;
using System.Threading.Tasks;
using DatascopeTest.Models;

namespace DatascopeTest.Data.Repositories
{
    public interface IGamesRepository : IGenericRepository<Game>
    {
        Task<IEnumerable<Game>> GetPaged(int page, int pageSize);

    }
}