using Microsoft.AspNetCore.Http;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components.JWT.Interfaces;
using SSIS_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


/**
 * check user token which is store in request header
 * @author WUYUDING
 */
namespace SSIS_FRONT.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;

        public TokenMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext context, IAuthService authService)
        {
            string controller = (string)context.Request.RouteValues["controller"];
            string action = (string)context.Request.RouteValues["action"];
            //get sessionId from cookie

            if ((controller != "Login" || action != "Verify") 
                && (controller != "Home" || action != "Error")
                &&(controller!="Home" ||action!="Index"))
            {
                string token = context.Request.Cookies["token"];
                if (token != null||token!="")
                {
                    if (!authService.IsTokenValid(token))
                    {
                        context.Response.StatusCode = CommonConstant.ErrorCode.INVALID_TOKEN;
                        return;
                    }
                    else
                    {
                        //decrypt token
                        List<Claim> claims = authService.GetTokenClaims(token).ToList();
                        Employee user = new Employee();
                        user.Name = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value;
                        user.Email = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Email)).Value;
                        user.Role = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Role)).Value;
                        try
                        {
                            context.Session.SetString("Name", user.Name);
                            context.Session.SetString("Role", user.Role);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message);
                        }
                        if (user == null)
                        {
                            context.Response.StatusCode = CommonConstant.ErrorCode.INVALID_TOKEN;
                            return;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = CommonConstant.ErrorCode.INVALID_TOKEN;
                    return;
                }
            }

            await next(context);
        }
    }
}
