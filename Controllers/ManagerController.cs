using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class ManagerController : Controller
    {
        protected HttpClient httpClient;
        protected IConfiguration cfg;

        public ManagerController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Store/Supplier")]
        public IActionResult Supplier()
        {
            return View();
        }

    }
}
