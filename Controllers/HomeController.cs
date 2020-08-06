using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils.Http;

namespace SSIS_FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected HttpClient httpClient;
        protected IConfiguration cfg;
        protected Post post;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration cfg, Post post)
        {
            _logger = logger;
            this.httpClient = httpClient;
            this.cfg = cfg;
            this.post = post;
        }

        public IActionResult Index(bool isLogin=true)
        {
            if (!isLogin)
            {
                ViewData["errmsg"] = "username or password incorrect";
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string WithoutToken()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Index";
            Result result = new Result();
            result = post.GetData(url, result, Request,Response);
            return result.Value.ToString();
        }

        public string WithToken()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Index";
            Result result = new Result();
            result = post.GetData(url, result, Request,Response);
            return result.Value.ToString();
        }
    }
}
