using Microsoft.AspNetCore.Http;
using SSIS_FRONT.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SSIS_FRONT.Utils.Http
{

    namespace APIGateway.Services
    {
        /**
         * this method transit the http request to according api
         */
        public class Post
        {
            public Result GetData(string url, Result result, HttpRequest request)
            {
                var handler = new HttpClientHandler() { UseCookies = true };
                var httpClient = new HttpClient(handler);// { BaseAddress = baseAddress };
                                                         //httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:57.0) Gecko/20100101 Firefox/57.0");
                                                         //httpClient.DefaultRequestHeaders.Add("Connection", "Keep-Alive");
                                                         //httpClient.DefaultRequestHeaders.Add("Keep-Alive", "timeout=600");
                //httpClient.DefaultRequestHeaders.Add("Cookie", "token=" + request.Cookies["token"]);
                //string token = request.Cookies["token"];
                //User user = JsonConvert.DeserializeObject<User>(RSA.RSADecrypt(token).ToString());
                //operand.User = user;
                Task.Run(async () =>
                {
                    HttpContent data = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(result),
                        Encoding.UTF8,
                        "application/json"
                    );

                    HttpResponseMessage response = await httpClient.PostAsync(
                        url, data
                    );
                    if (response.StatusCode.GetHashCode() == 200)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        result = System.Text.Json.JsonSerializer.Deserialize<Result>(content);
                    }
                }).Wait();

                return result;
            }
        }
    }

}
