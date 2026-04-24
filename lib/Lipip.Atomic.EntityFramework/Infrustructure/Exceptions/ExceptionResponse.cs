using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Infrustructure.Exceptions
{
    public class ExceptionResponse
    {
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
