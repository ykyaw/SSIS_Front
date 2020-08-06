using Microsoft.AspNetCore.Http;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

/**
 * @author WUYUDING
 */
namespace SSIS_FRONT.Utils.Http
{
    public class Delete
    {
        public Result GetData(string url, Result result, HttpRequest request, HttpResponse httpResponse)
        {

            var cookies = new CookieContainer();
            var handler = new HttpClientHandler() { UseCookies = true, CookieContainer = cookies };
            var httpClient = new HttpClient(handler);
            string token = request.Cookies["token"];
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + request.Cookies["token"]);
            }
            Task.Run(async () =>
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(
                    url
                );
                if (response.StatusCode.GetHashCode() == 200)
                {

                    IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri(url)).Cast<Cookie>();
                    foreach (Cookie cookie in responseCookies)
                    {
                        Debug.WriteLine("cookie name: {0}, cookie value: {1}", cookie.Name, cookie.Value);
                        if (cookie.Name == "token")
                        {
                            httpResponse.Cookies.Append(cookie.Name, cookie.Value);
                        }
                    }
                    string content = await response.Content.ReadAsStringAsync();
                    result = System.Text.Json.JsonSerializer.Deserialize<Result>(content);
                }
                else if (response.StatusCode.GetHashCode() == CommonConstant.ErrorCode.INVALID_TOKEN)
                {
                    result.Value = "Invalid token. Please login again.";
                }
            }).Wait();

            return result;
        }
    }
}
