﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SSIS_FRONT.Utils
{

        /**
         * this method transit the http request to according api
         */
        public class HttpUtils
        {
            public static Result Post<T>(string url,T value, HttpRequest request,HttpResponse httpResponse)
            {
                Result result = null;
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
                        HttpContent data = new StringContent(
                            System.Text.Json.JsonSerializer.Serialize(value),
                            Encoding.UTF8,
                            "application/json"
                        );
                        HttpResponseMessage response = await httpClient.PostAsync(
                            url, data
                        );
                        if (response.StatusCode.GetHashCode() == 200)
                        {

                            IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri(url)).Cast<Cookie>();
                            foreach(Cookie cookie in responseCookies)
                            {
                                Debug.WriteLine("cookie name: {0}, cookie value: {1}", cookie.Name, cookie.Value);
                                if (cookie.Name == "token")
                                {
                                    httpResponse.Cookies.Append(cookie.Name, cookie.Value);
                                }
                            }
                            string content = await response.Content.ReadAsStringAsync();
                            result  = System.Text.Json.JsonSerializer.Deserialize<Result>(content);
                        }else if (response.StatusCode.GetHashCode() == CommonConstant.ErrorCode.INVALID_TOKEN)
                        {
                            result=new Result()
                            {
                                code = CommonConstant.ErrorCode.INVALID_TOKEN,
                                sub_msg = "invalid token",
                                msg = ""
                            };
                            
                        }
                    }).Wait();

                return result;
            }

            public static Result Put<T>(string url, T value, HttpRequest request, HttpResponse httpResponse)
            {
                Result result = null;
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
                    HttpContent data = new StringContent(
                        System.Text.Json.JsonSerializer.Serialize(value),
                        Encoding.UTF8,
                        "application/json"
                    );
                    HttpResponseMessage response = await httpClient.PutAsync(
                        url, data
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
                        result = new Result()
                        {
                            code = CommonConstant.ErrorCode.INVALID_TOKEN,
                            sub_msg = "invalid token",
                            msg = ""
                        };

                    }
                }).Wait();

                return result;
            }

            public static Result Delete(string url, HttpRequest request, HttpResponse httpResponse)
            {
                Result result = null;
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
                        result = new Result()
                        {
                            code = CommonConstant.ErrorCode.INVALID_TOKEN,
                            sub_msg = "invalid token",
                            msg = ""
                        };

                    }
                }).Wait();

                return result;
            }

            public static Result Get(string url, HttpRequest request, HttpResponse httpResponse)
            {
                Result result = null;
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
                    HttpResponseMessage response = await httpClient.GetAsync(
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
                        result = new Result()
                        {
                            code = CommonConstant.ErrorCode.INVALID_TOKEN,
                            sub_msg = "invalid token",
                            msg = ""
                        };

                    }
                }).Wait();

                return result;
            }
        }
    

}
