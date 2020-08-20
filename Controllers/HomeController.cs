using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SSIS_FRONT.Common;
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
        public string GetRole()
        {
            int id = (int)HttpContext.Session.GetInt32("Id");
            string role = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            if (role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_EMPLOYEE])
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/drep";
                Result<Employee> result = HttpUtils.Get(url, new Employee(), Request, Response);
                if (result.data.Id == id)
                {
                    return CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_REPRESENTATIVE];
                }
            }
            return role;
        }
        public string GetName()
        {
            return HttpContext.Session.GetString("Name");
        }
        public IActionResult Index(bool isLogin = true, bool isLogout = false)
        {
            if (isLogout)
            {
                ViewData["errmsg"] = "";
            }
            else if (!isLogin)
            {
                ViewData["errmsg"] = "username or password incorrect";
            }

            return View();
        }

        public IActionResult Welcome()
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public Result<Employee> WithoutToken(Employee emp)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Test";
            Employee user = new Employee() { Email = "123@tt.com", Password = "123" };
            Result<Employee> result = HttpUtils.Post(url, user, new Employee(), Request, Response);
            return result;
        }

        public Result<Object> WithToken()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/Login/Index";
            Employee user = new Employee() { Email = "123@tt.com", Password = "123" };
            Result<Object> result = HttpUtils.Post(url, user, Request, Response);
            return result;
        }
    }
}
