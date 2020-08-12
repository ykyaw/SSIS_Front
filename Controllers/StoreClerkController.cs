﻿using System;
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
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["products"] = result.data;
            return View();
        }
        [Route("StoreClerk/StockCard/{productId}")]
        public IActionResult StockCard(string productId)
        {
            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/sc/" + productId;
            Result<List<Transaction>> result1 = HttpUtils.Get(url1, new List<Transaction>(), Request, Response);
            ViewData["transactions"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/supplier/" + productId;
            Result<List<TenderQuotation>> result2 = HttpUtils.Get(url2, new List<TenderQuotation>(), Request, Response);
            ViewData["tenderquotations"] = result2.data;
            return View();
        }
        public IActionResult GeneratePurchaseRequest()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["products"] = result.data;
            return View();
        }

        public IActionResult DeliveryOrder()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po﻿";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["purchaseOrders"] = result.data;
            return View();
        }
        public List<PurchaseOrder> DeliveryOrderList()
        {
            return null;
        }
        public List<RequisitionDetail> Disbursement()
        {
            return null;
        }
        public IActionResult PurchaseOrder()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po﻿";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["purchaseOrders"] = result.data;
            return View();
        }
        public List<PurchaseOrder> PurchaseOrderList()
        {
            return null;
        }
        public IActionResult PurchaseRequest()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pr";
            Result<List<PurchaseRequestDetail>> result = HttpUtils.Get(url, new List<PurchaseRequestDetail>(), Request, Response);
            Dictionary<long, List<PurchaseRequestDetail>> prDetailsByPr = new Dictionary<long, List<PurchaseRequestDetail>>();
            foreach (PurchaseRequestDetail detail in result.data)
            {
                if (prDetailsByPr.ContainsKey(detail.PurchaseRequestId))
                {
                    prDetailsByPr[detail.PurchaseRequestId].Add(detail);
                }
                else
                {
                    prDetailsByPr.Add(detail.PurchaseRequestId, new List<PurchaseRequestDetail>());
                    prDetailsByPr[detail.PurchaseRequestId].Add(detail);
                }
            }
            ViewData["prDetailsByPr"] = prDetailsByPr;
            return View();
        }
        public List<PurchaseRequestDetail> PurchaseRequestList()
        {
            return null;
        }
        public IActionResult Requisition()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rf";
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;
            return View();
        }

        public IActionResult GenerateRetrieveForm(string errMsg = "")
        {
            ViewData["errMsg"] = errMsg;
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
            if (result.code == 200)
            {
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
            else
            {
                return RedirectToAction("GenerateRetrieveForm", "StoreClerk", new { errMsg = result.msg });
            }
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
