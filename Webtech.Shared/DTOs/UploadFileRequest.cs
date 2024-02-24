using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webtech.Shared.DTOs
{
    public class UploadFileRequest
    {
        public IFormFile File { get; set; }
    }
}
