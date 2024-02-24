using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Webtech.Infrastructure.Context;

namespace Webtech.Infrastructure.Repository
{
    public class ApplicationRepository<T> : IApplicationRepository<T>
        where T : class
    {
        private readonly IWebtechContext dbContext;

        public ApplicationRepository(IWebtechContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> Entities => this.dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await this.dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task AddRangeAsync(T[] entities)
        {
            this.dbContext.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            this.dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteRangeAsync(T[] entities)
        {
            this.dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await this.dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this.dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await this.dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            this.dbContext.Set<T>().UpdateRange(entity);
            return Task.CompletedTask;
        }

        public Task UpdateRangeAsync(IQueryable<T> entities)
        {
            this.dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChanges();
        }
    }
}
