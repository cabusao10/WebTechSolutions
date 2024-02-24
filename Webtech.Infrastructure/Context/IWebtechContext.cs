using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Domain.Entities;

namespace Webtech.Infrastructure.Context
{
    public interface IWebtechContext
    {
        DbSet<FileEntity> Files { get; set; }
        Task<int> SaveChanges();

        DbSet<TEntity> Set<TEntity>()
         where TEntity : class;
    }
}
