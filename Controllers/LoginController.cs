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
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class LoginController : Controller
    {

        protected HttpClient httpClient;
        protected IConfiguration cfg;

        public LoginController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Verify(User user)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Verify";
            Result result = HttpUtils.Post(url, user,Request,Response);
            if (result.code != 200)
            {
                return RedirectToAction("Index", "Home", new { isLogin = false });
            }
            string token = result.data.ToString();
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Privacy", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { isLogin = false });
            }
        }
    }
}