using System.Threading.Tasks;
using DatascopeTest.Models;

namespace DatascopeTest.Data.Repositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T> Get(int id);
        void Add(T entity);
        void Remove(T entity);
        Task SaveChanges();
        Task<int> Count();
    }
}