using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils.Http;

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
            result = post.GetData(url, result, Request,Response);
            if (result.Value!=null&&!string.IsNullOrEmpty(result.Value.ToString()))
            {
                string token = result.Value.ToString();
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Index","Home",new { isLogin=false});
            }
            
        }
    }
}