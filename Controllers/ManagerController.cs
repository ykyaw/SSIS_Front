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
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url,new List<Product>(), Request, Response);
            ViewData["Items"] = result.data;
            return View();
        }

        [HttpGet]
        [Route("TenderQuotations/{id}")]
        public List<TenderQuotation> TenderQuotations(string id)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storemgmt/retrievesuppliers/"+id;
            Result<List<TenderQuotation>> result = HttpUtils.Get(url, new List<TenderQuotation>(), Request, Response);
            return result.data;
        }



        [HttpPut]
        [Route("TenderQuotations/{id}")]
        public bool UpdateTop3Supplier([FromBody] List<TenderQuotation> quotations)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storemgmt/updatesupplier/";
            Result<Object> result = HttpUtils.Put(url, quotations, Request, Response);
            return (bool)result.data;
        }

    }
}
