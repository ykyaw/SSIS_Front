using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SSIS_FRONT.Common;
using System;
using System.Threading.Tasks;

/**
 * if actions throw exception, handle that exception, encapsulate the error msg into global result
 * @author WUYUDING
 */
namespace SSIS_FRONT.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex);
            }
            finally
            {
                await WriteExceptionAsync(context, null);
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception != null)
            {
                var response = context.Response;
                string message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
                if (message == CommonConstant.ErrorCode.NO_PERMISSION_MSG)
                {
                    response.Redirect("/Home/NoPermission");
                }
                response.ContentType = "application/json";
                string tt = JsonConvert.SerializeObject(new { code = 400, msg = message });
                await response.WriteAsync(JsonConvert.SerializeObject(new { code = 400, msg = message })).ConfigureAwait(false);
            }
            else
            {
                var code = context.Response.StatusCode;
                switch (code)
                {
                    case 200:
                        return;
                    case 204:
                        return;
                    case 510:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { code = code, msg = "invalid token, please login again" })).ConfigureAwait(false);
                        break;
                    default:
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { code = code, msg = "unknow error" })).ConfigureAwait(false);
                        break;
                }
            }
        }
    }
}
