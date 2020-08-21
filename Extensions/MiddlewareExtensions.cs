using Microsoft.AspNetCore.Builder;
using SSIS_FRONT.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddlewareExtensions(this IApplicationBuilder app)
        {
            app.UseMiddleware<TokenMiddleware>();
            app.UseMiddleware<ExceptionMiddleWare>();
            return app;
        }
    }
}
