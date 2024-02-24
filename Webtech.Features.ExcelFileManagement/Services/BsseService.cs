using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webtech.Infrastructure.UnitOfWork;

namespace Webtech.Features.ExcelFileManagement.Services
{
    public class BaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="userUnitOfWork">IUserUnit of work parameter..</param>
        public BaseService(IExcelUnitOfWork userUnitOfWork, IMapper mapper)
        {
            this.ExcelUnitOfWork = userUnitOfWork;
            this.Mapper = mapper;
        }


        /// <summary>
        /// Gets or sets the SQLUnitOfWork.
        /// </summary>
        protected internal IExcelUnitOfWork ExcelUnitOfWork { get; set; }

        /// <summary>
        /// Gets or sets the Mapper.
        /// </summary>
        protected internal IMapper Mapper { get; set; }

    
    }
}
