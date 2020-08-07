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
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected HttpClient httpClient;
        protected IConfiguration cfg;

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration cfg)
        {
            _logger = logger;
            this.httpClient = httpClient;
            this.cfg = cfg;
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

        public Result WithoutToken()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Test";
            User user = new User() { email = "123@tt.com", password = "123" };
            Result result = HttpUtils.Post(url, user, Request,Response);
            return result;
        }

        public Result WithToken()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Index";
            User user = new User() { email = "123@tt.com", password = "123" };
            Result result = HttpUtils.Post(url, user, Request,Response);
            return result;
        }
    }
}
