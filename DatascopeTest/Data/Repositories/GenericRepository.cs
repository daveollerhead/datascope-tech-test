using System.Threading.Tasks;
using DatascopeTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DatascopeTest.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        protected readonly AppDbContext Context;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
        }

        public async Task<T> Get(int id)
        {
            return await Context.Set<T>().FindAsync(id);
        }

        public async Task<int> Count()
        {
            return await Context.Set<T>().CountAsync();
        }

        public void Add(T entity)
        {
            Context.Add(entity);
        }

        public void Remove(T entity)
        {
            Context.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}