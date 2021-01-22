using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatascopeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DatascopeTest.Data.Repositories
{
    public class GamesRepository : GenericRepository<Game>, IGamesRepository
    {
        public GamesRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Game>> GetPaged(int page, int pageSize)
        {
            return await Context.Games
                .OrderByDescending(x => x.CreatedAt)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}