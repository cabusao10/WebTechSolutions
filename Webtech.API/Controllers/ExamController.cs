using MediatR;
using Microsoft.AspNetCore.Mvc;
using Webtech.Features.ExcelFileManagement.Services.Commands;
using Webtech.Shared.DTOs;

namespace Webtech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    ///  The controller for exam
    /// </summary>
    public class ExamController : ControllerBase
    {
        /// <summary>
        /// Mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes the exam controller
        /// </summary>
        /// <param name="mediator"></param>
        public ExamController(IMediator mediator)
        {
            this._mediator = mediator;
        }
        /// <summary>
        /// Uploads an excel file and return the data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] IFormFile file)
        {
            var result = await this._mediator.Send( new UploadExcelFileCommand(new UploadFileRequest() { File = file }));
            return this.Ok(result);
        }
    }
}
