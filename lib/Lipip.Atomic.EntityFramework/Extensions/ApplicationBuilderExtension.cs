using Lipip.Atomic.EntityFramework.Infrustructure.Exceptions;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lipip.Atomic.EntityFramework.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseAtomicExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
