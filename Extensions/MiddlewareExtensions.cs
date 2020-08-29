using Microsoft.AspNetCore.Builder;
using SSIS_FRONT.Middlewares;

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
