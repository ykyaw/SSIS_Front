using System;
using System.Diagnostics;
using System.Net.Http;
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
            string Role = HttpContext.Session.GetString("Role");
            if (Role == CommonConstant.ROLE.DEPARTMENT_HEAD)
            {
                string url1 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/cdel";
                Result<Employee> result1 = HttpUtils.Get(url1, new Employee(), Request, Response);
                if (result1.data != null && result1.data.Id == HttpContext.Session.GetInt32("Id"))
                {
                    return CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_DELEGATE];
                }
            }
            else if (Role == CommonConstant.ROLE.DEPARTMENT_EMPLOYEE)
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/drep";
                Result<Employee> result = HttpUtils.Get(url, new Employee(), Request, Response);
                if (result.data.Id == HttpContext.Session.GetInt32("Id"))
                {
                    return CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_REPRESENTATIVE];
                }
            }
            return CommonConstant.ROLE_NAME[Role];
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

        public IActionResult NoPermission()
        {
            return View();
        }
    }
}
