using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils.Http.APIGateway.Services;

namespace SSIS_FRONT.Controllers
{
    public class LoginController : Controller
    {

        protected HttpClient httpClient;
        protected IConfiguration cfg;
        protected Post post;

        public LoginController(HttpClient httpClient, IConfiguration cfg,Post post)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
            this.post = post;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verify(User user)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Verify";
            Result result = new Result();
            result.Value = user;
            result = post.GetData(url, result, Request);
            bool isLogin = Convert.ToBoolean(result.Value.ToString());
            if (isLogin)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                return RedirectToAction("Index","Home",new { isLogin=isLogin});
            }
            
        }
    }
}