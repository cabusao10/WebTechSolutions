using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Domain.Entities;
using Webtech.Infrastructure.Repository;

namespace Webtech.Infrastructure.UnitOfWork
{
    public interface IExcelUnitOfWork
    {
        IApplicationRepository<FileEntity> FilesRepository { get; }

        /// <summary>
        /// Saves changes.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<int> Save();
    }
}
