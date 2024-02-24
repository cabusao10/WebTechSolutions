using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Domain.Entities;
using Webtech.Infrastructure.Context;
using Webtech.Infrastructure.Repository;

namespace Webtech.Infrastructure.UnitOfWork
{
    public class ExcelUnitOfWork : IExcelUnitOfWork
    {
        private readonly IWebtechContext dbContext;
        private IApplicationRepository<FileEntity> _excelRepostory;
        public ExcelUnitOfWork(IWebtechContext webtechContext,
            IApplicationRepository<FileEntity> fileEntity)
        {
            this.dbContext = webtechContext;
            this._excelRepostory = fileEntity;
        }

        public IApplicationRepository<FileEntity> FilesRepository { get { return _excelRepostory; } }


        /// <inheritdoc/>
        public async Task<int> Save()
        {
            return await this.dbContext.SaveChanges();
        }
    }
}
