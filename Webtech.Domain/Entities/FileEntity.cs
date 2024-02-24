using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webtech.Domain.Entities
{
    public class FileEntity : BaseEntity
    {
        public string Filename { get; set; }
        public byte[] Content { get; set; }
        public int Size { get; set; }
        
    }
}
