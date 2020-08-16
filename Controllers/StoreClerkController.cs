using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /**
         * @author Thatoe
         */
        public IActionResult Requisition()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rf";
            Result<List<Requisition>>  result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;
            return View();
        }
        [Route("StoreClerk/Requisition/{RequisitionId}")]
        public IActionResult RequisitionDetail(int RequisitionId)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rfld/" + RequisitionId;
            Result<Requisition> result = HttpUtils.Get(url, new Requisition(), Request, Response);
            ViewData["requisition"] = result.data;
            return View();
        }
        public List<RequisitionDetail> Disbursement()
        {
            return null;
        }
        public IActionResult Catalogue()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["products"] = result.data;
            return View();
        }
        [Route("StoreClerk/StockCard/{ProductId}")]
        public IActionResult StockCard(string ProductId)
        {
            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/sc/" + ProductId;
            Result<List<Transaction>> result1 = HttpUtils.Get(url1, new List<Transaction>(), Request, Response);
            ViewData["transactions"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/supplier/" + ProductId;
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
        public IActionResult PurchaseRequest()
        {
            string role = HttpContext.Session.GetString("Role");
            Result<List<PurchaseRequestDetail>> result = null;
            if (role == CommonConstant.ROLE.STORE_CLERK)
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pr";
                result = HttpUtils.Get(url, new List<PurchaseRequestDetail>(), Request, Response);
            }
            else
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/pr";
                result = HttpUtils.Get(url, new List<PurchaseRequestDetail>(), Request, Response);
            }
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

        [HttpPut]
        [Route("Store/PurchaseRequestDetail")]
        public bool UpdatePurchaseRequestDetail([FromBody] List<PurchaseRequestDetail> details)
        {
            string role = HttpContext.Session.GetString("Role");
            string url = "";
            if (role == CommonConstant.ROLE.STORE_CLERK)
            {

            }
            else
            {
                url = cfg.GetValue<string>("Hosts:Boot") + "﻿/storesup/updatepr";
            } 
            Result<Object> result = HttpUtils.Put(url,details, Request, Response);
            return (Boolean)result.data;
        }

        [Route("StoreClerk/PurchaseRequest/{PurchaseRequestId}")]
        public IActionResult PurchaseRequestDetail(long PurchaseRequestId)
        {
            string role= CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            string name= (string)HttpContext.Session.GetString("Name");
            string url1 = "";
            if(HttpContext.Session.GetString("Role") == CommonConstant.ROLE.STORE_CLERK)
            {
                url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/prdetails/" + PurchaseRequestId;
            }
            else
            {
                url1 = cfg.GetValue<string>("Hosts:Boot") + "/storesup/prdetails/" + PurchaseRequestId;
            }
            Result<List<PurchaseRequestDetail>> result1 = HttpUtils.Get(url1, new List<PurchaseRequestDetail>(), Request, Response);



            Dictionary<PurchaseRequestDetail, List<TenderQuotation>> TQbyPRD = new Dictionary<PurchaseRequestDetail, List<TenderQuotation>>();
            foreach (PurchaseRequestDetail detail in result1.data)
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/supplier/" + detail.ProductId;
                Result<List<TenderQuotation>> result = HttpUtils.Get(url, new List<TenderQuotation>(), Request, Response);
                TQbyPRD.Add(detail, result.data);
            }
            ViewData["TQbyPRD"] = TQbyPRD;
            ViewData["Role"] = role;
            ViewData["Name"] = name;
            ViewData["Detail"] = result1.data;

            return View();
        }
        public IActionResult PurchaseOrder()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["purchaseOrders"] = result.data;
            return View();
        }
        [Route("StoreClerk/PurchaseOrder/{PurchaseOrderId}")]
        public IActionResult PurchaseOrderDetail(int PurchaseOrderId)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pod/" + PurchaseOrderId;
            Result<List<PurchaseOrderDetail>> result = HttpUtils.Get(url, new List<PurchaseOrderDetail>(), Request, Response);
            ViewData["purchaseOrderDetails"] = result.data;
            return View();
        }
        public IActionResult DeliveryOrder()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["deliveryOrders"] = result.data;
            return View();
        }
        [Route("StoreClerk/DeliveryOrder/{DeliveryOrderId}")]
        public IActionResult DeliveryOrderDetail(int DeliveryOrderId)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pod/" + DeliveryOrderId;
            Result<List<PurchaseOrderDetail>> result = HttpUtils.Get(url, new List<PurchaseOrderDetail>(), Request, Response);
            ViewData["deliveryOrderDetails"] = result.data;
            return View();
        }

        /**
         * @author WUYUDING
         */
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

        /**
         * @author XJ
         */
        public IActionResult AdjustmentVoucher()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/adv";
            Result<List<AdjustmentVoucher>> result = HttpUtils.Get(url, new List<AdjustmentVoucher>(), Request, Response);
            ViewData["adjustmentVoucherListToHTML"] = result.data;
            return View();

        }
    }
}
