using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

        public IActionResult Verify(Employee user)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Verify";
            Result<Dictionary<string, Object>> result = HttpUtils.Post(url, user, new Dictionary<string, object>(), Request, Response);
            if (result.code != 200)
            {
                return RedirectToAction("Index", "Home", new { isLogin = false });
            }
            string token = result.data["token"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home", new { isLogin = false });
            }
        }

        public IActionResult Logout()
        {
            if (Request.Cookies["token"] != null)
            {
                Response.Cookies.Append("token", "");
            }
            return RedirectToAction("Index", "Home", new { isLogin = false, isLogout = true });
        }
    }
}