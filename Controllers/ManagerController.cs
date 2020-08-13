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
            List<TenderQuotation> quotations = new List<TenderQuotation>();
            TenderQuotation t1 = new TenderQuotation()
            {
                Id = 1,
                Product = new Product()
                {
                    Id = id,
                    Description = "this is description"
                },
                Supplier = new Supplier()
                {
                    Id = "1",
                    Name = "ALPHA"
                },
                Rank = 1
            };
            TenderQuotation t2 = new TenderQuotation()
            {
                Id = 2,
                Product = new Product()
                {
                    Id = id,
                    Description = "this is description"
                },
                Supplier = new Supplier()
                {
                    Id = "2",
                    Name = "BANES"
                },
                Rank = 2
            };
            TenderQuotation t3 = new TenderQuotation()
            {
                Id = 3,
                Product = new Product()
                {
                    Id = id,
                    Description = "this is description"
                },
                Supplier = new Supplier()
                {
                    Id = "3",
                    Name = "CHARLIE"
                },
                Rank = 3
            };
            TenderQuotation t4 = new TenderQuotation()
            {
                Id = 4,
                Product = new Product()
                {
                    Id = id,
                    Description = "this is description"
                },
                Supplier = new Supplier()
                {
                    Id = "4",
                    Name = "TAN"
                },
                Rank = null
            };
            quotations.Add(t1);
            quotations.Add(t2);
            quotations.Add(t3);
            quotations.Add(t4);
            return quotations;
        }



        [HttpPut]
        [Route("TenderQuotations/{id}")]
        public bool UpdateTop3Supplier([FromBody] List<TenderQuotation> quotations)
        {
            return true;
        }

    }
}
