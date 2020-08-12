using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class StoreClerkController : Controller
    {
        protected HttpClient httpClient;
        protected IConfiguration cfg;
        public StoreClerkController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Catalogue()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/getcatalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["products"] = result.data;
            return View();
        }

        public List<PurchaseOrderDetail> DeliveryOrder()
        {
            return null;
        }
        public List<PurchaseOrder> DeliveryOrderList()
        {
            return null;
        }
        public List<RequisitionDetail> Disbursement()
        {
            return null;
        }
        public List<PurchaseOrderDetail> PurchaseOrder()
        {
            return null;
        }
        public List<PurchaseOrder> PurchaseOrderList()
        {
            return null;
        }
        public List<PurchaseRequestDetail> PurchaseRequest()
        {
            return null;
        }
        public List<PurchaseRequestDetail> PurchaseRequestList()
        {
            return null;
        }
        public List<Requisition> Requisition()
        {
            return null;
        }
        public List<Transaction> StockCard(string ProductId)
        {
            return null;
        }

        public IActionResult GenerateRetrieveForm()
        {
            return View();
        }

        public IActionResult RetrieveForm(long date)
        {
            //List<RequisitionDetail> requisitionDetails = new List<RequisitionDetail>();
            //RequisitionDetail rd1 = new RequisitionDetail()
            //{
            //    Id = 1,
            //    Product = new Product()
            //    {
            //        Id = "1",
            //        Category = new Category()
            //        {
            //            Id = 1,
            //            Name = "pencils",
            //            BinNo = "A7"
            //        }
            //    },
            //    Requisition = new Requisition()
            //    {
            //        Id = 1,
            //        Department = new Department()
            //        {
            //            Id = "1",
            //            Name = "English"
            //        }
            //    },
            //    QtyNeeded = 5,
            //};
            //RequisitionDetail rd2 = new RequisitionDetail()
            //{
            //    Id = 2,
            //    Product = new Product()
            //    {
            //        Id = "1",
            //        Category = new Category()
            //        {
            //            Id = 1,
            //            Name = "pencils",
            //            BinNo = "A7"
            //        }
            //    },
            //    Requisition = new Requisition()
            //    {
            //        Id = 2,
            //        Department = new Department()
            //        {
            //            Id = "2",
            //            Name = "Math"
            //        }
            //    },
            //    QtyNeeded = 3,
            //};
            //RequisitionDetail rd3 = new RequisitionDetail()
            //{
            //    Id = 3,
            //    Product = new Product()
            //    {
            //        Id = "2",
            //        Category = new Category()
            //        {
            //            Id = 1,
            //            Name = "pen",
            //            BinNo = "B9"
            //        }
            //    },
            //    Requisition = new Requisition()
            //    {
            //        Id = 3,
            //        Department = new Department()
            //        {
            //            Id = "2",
            //            Name = "Math"
            //        }
            //    },
            //    QtyNeeded = 10,
            //};
            //RequisitionDetail rd4 = new RequisitionDetail()
            //{
            //    Id = 4,
            //    Product = new Product()
            //    {
            //        Id = "2",
            //        Category = new Category()
            //        {
            //            Id = 1,
            //            Name = "pen",
            //            BinNo = "B9"
            //        }
            //    },
            //    Requisition = new Requisition()
            //    {
            //        Id = 4,
            //        Department = new Department()
            //        {
            //            Id = "1",
            //            Name = "English"
            //        }
            //    },
            //    QtyNeeded = 20,
            //};
            //requisitionDetails.Add(rd1);
            //requisitionDetails.Add(rd2);
            //requisitionDetails.Add(rd3);
            //requisitionDetails.Add(rd4);
            //Retrieval retrieval = new Retrieval()
            //{
            //    Id = 128939232,
            //    Status = CommonConstant.RetrievalFormStatus.CREATING,
            //    Clerk = new Employee()
            //    {
            //        Name = "Esther",
            //        Id = 1
            //    },
            //    RequisitionDetails = requisitionDetails,
            //    DisbursedDate = date
            //};
            //string json = JsonConvert.SerializeObject(retrieval);
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/ret";
            Result<Retrieval> result = HttpUtils.Post(url, date, new Retrieval(), Request, Response);
            Dictionary<string, List<RequisitionDetail>> categoryByProduct = new Dictionary<string, List<RequisitionDetail>>();
            foreach (RequisitionDetail detail in result.data.RequisitionDetails)
            {
                if (categoryByProduct.ContainsKey(detail.Product.Id))
                {
                    categoryByProduct[detail.Product.Id].Add(detail);
                }
                else
                {
                    categoryByProduct.Add(detail.Product.Id, new List<RequisitionDetail>());
                    categoryByProduct[detail.Product.Id].Add(detail);
                }
            }
            ViewData["categoryByProduct"] = categoryByProduct;
            ViewData["Retrieval"] = result.data;
            return View();
        }

        [HttpPut]
        public bool Retrieval([FromBody] Retrieval retrieval)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/ret";
            Result<Object> result = HttpUtils.Put(url, retrieval, Request, Response);
            return (bool)result.data;
        }
    }
}
