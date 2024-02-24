using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webtech.Infrastructure.Repository
{
    public interface IApplicationRepository<T>
         where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(T[] entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteRangeAsync(T[] entities);

        Task UpdateRangeAsync(IQueryable<T> entities);

        Task<int> SaveChangesAsync();
    }
}
