using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Domain.Entities;

namespace Webtech.Infrastructure.Context
{
    public class WebtechContext : DbContext, IWebtechContext
    {
        public WebtechContext(DbContextOptions<WebtechContext> options) :base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }
        public DbSet<FileEntity> Files { get; set; }

        public async Task<int> SaveChanges()
        {
            return await this.SaveChangesAsync();
        }
    }
}
