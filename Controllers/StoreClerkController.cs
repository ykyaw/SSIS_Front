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
            //string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rf";
            //Result<List<Requisition>>  result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            //ViewData["requisitions"] = result.data;

            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rf";
            Result<List<Requisition>> result1 = HttpUtils.Get(url1, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/retrievealldept";
            Result<List<Department>> result2 = HttpUtils.Get(url2, new List<Department>(), Request, Response);
            ViewData["departments"] = result2.data;

            return View();
        }
        [Route("StoreClerk/Requisition/{RequisitionId}")]
        public IActionResult RequisitionDetail(int RequisitionId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rfld2/" + RequisitionId;
            Result<Requisition> result = HttpUtils.Get(url, new Requisition(), Request, Response);
            ViewData["requisition"] = result.data;

            return View();
        }
        public bool UpdateRequisition([FromBody] Requisition requisition)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/rfld";
            Result<Object> result = HttpUtils.Put(url, requisition, Request, Response);
            return (bool)result.data;
        }
        public IActionResult Disbursement(string errMsg = "")
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/retrievealldept";
            Result<List<Department>> result1 = HttpUtils.Get(url1, new List<Department>(), Request, Response);
            ViewData["departments"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/alldis";
            Result<List<Requisition>> result2 = HttpUtils.Get(url2, new List<Requisition>(), Request, Response);
            ViewData["disbursements"] = result2.data;

            ViewData["errMsg"] = errMsg;

            return View();
        }
        public IActionResult DisbursementDetail(long date, string deptid)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            Requisition requisition = new Requisition();
            requisition.CollectionDate = date;
            requisition.DepartmentId = deptid;

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/disbursement";
            Result<List<RequisitionDetail>> result = HttpUtils.Post(url, requisition, new List<RequisitionDetail>(), Request, Response);
            if (result.code == 200)
            {
                ViewData["disbursementList"] = result.data;
                return View();
            }
            else
            {
                return RedirectToAction("Disbursement", "StoreClerk", new { errMsg = result.msg });
            }
        }
        public bool AckDisbursement([FromBody] List<RequisitionDetail> requisitionDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/ackreq";
            Result<Object> result = HttpUtils.Put(url, requisitionDetails, Request, Response);
            return (bool)result.data;
        }
        public IActionResult Catalogue()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/glt";
            Result<List<Transaction>> result = HttpUtils.Get(url, new List<Transaction>(), Request, Response);
            ViewData["products"] = result.data;

            List<string> categories = result.data.Select(x => x.Product.Category.Name).Distinct().ToList();
            ViewData["categories"] = categories;

            return View();
        }
        [Route("StoreClerk/StockCard/{ProductId}")]
        public IActionResult StockCard(string ProductId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/sc/" + ProductId;
            Result<List<Transaction>> result1 = HttpUtils.Get(url1, new List<Transaction>(), Request, Response);
            ViewData["transactions"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/supplier/" + ProductId;
            Result<List<TenderQuotation>> result2 = HttpUtils.Get(url2, new List<TenderQuotation>(), Request, Response);
            ViewData["tenderquotations"] = result2.data;

            return View();
        }
        public bool UpdateStockCard([FromBody] Transaction transaction)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/updatesc";
            Result<Object> result = HttpUtils.Post(url, transaction, Request, Response);
            return (bool)result.data;
        }
        public IActionResult GeneratePurchaseRequest()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["products"] = result.data;

            return View();
        }
        [HttpPost]
        public List<PurchaseRequestDetail> CreatePurchaseRequest([FromBody] List<string> productIds)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/createpr";
            Result<List<PurchaseRequestDetail>> result = HttpUtils.Post(url, productIds, new List<PurchaseRequestDetail>(), Request, Response);
            return result.data;
        }
        public IActionResult PurchaseRequest()
        {
            //string role = HttpContext.Session.GetString("Role");
            //Result<List<PurchaseRequestDetail>> result = null;
            //if (role == CommonConstant.ROLE.STORE_CLERK)
            //{
            //    string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pr";
            //    result = HttpUtils.Get(url, new List<PurchaseRequestDetail>(), Request, Response);
            //}
            //else
            //{
            //    string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/pr";
            //    result = HttpUtils.Get(url, new List<PurchaseRequestDetail>(), Request, Response);
            //}

            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

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
        [HttpPost]
        public bool GenerateQuote([FromBody] List<PurchaseRequestDetail> details)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/generatequote";
            Result<Object> result = HttpUtils.Post(url, details, Request, Response);
            return (bool)result.data;
        }
        [HttpPut]
        [Route("StoreClerk/PurchaseRequest")]
        public bool UpdatePurchaseRequestDetail([FromBody] List<PurchaseRequestDetail> details)
        {
            //string role = HttpContext.Session.GetString("Role");
            //string url = "";
            //if (role == CommonConstant.ROLE.STORE_CLERK)
            //{

            //}
            //else
            //{
            //    url = cfg.GetValue<string>("Hosts:Boot") + "﻿/storesup/updatepr";
            //} 
            //Result<Object> result = HttpUtils.Put(url,details, Request, Response);
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/updatepr";
            Result<Object> result = HttpUtils.Put(url, details, Request, Response);
            return (bool)result.data;
        }
        [Route("StoreClerk/PurchaseRequest/{PurchaseRequestId}")]
        public IActionResult PurchaseRequestDetail(long PurchaseRequestId)
        {
            //string role= CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            //string name= (string)HttpContext.Session.GetString("Name");
            //string url1 = "";
            //if(HttpContext.Session.GetString("Role") == CommonConstant.ROLE.STORE_CLERK)
            //{
            //    url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/prdetails/" + PurchaseRequestId;
            //}
            //else
            //{
            //    url1 = cfg.GetValue<string>("Hosts:Boot") + "/storesup/prdetails/" + PurchaseRequestId;
            //}

            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/prdetails/" + PurchaseRequestId;
            Result<List<PurchaseRequestDetail>> result1 = HttpUtils.Get(url1, new List<PurchaseRequestDetail>(), Request, Response);
            Dictionary<PurchaseRequestDetail, List<TenderQuotation>> TQbyPRD = new Dictionary<PurchaseRequestDetail, List<TenderQuotation>>();
            ViewData["Detail"] = result1.data;

            foreach (PurchaseRequestDetail detail in result1.data)
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/supplier/" + detail.ProductId;
                Result<List<TenderQuotation>> result = HttpUtils.Get(url, new List<TenderQuotation>(), Request, Response);
                TQbyPRD.Add(detail, result.data);
            }
            ViewData["TQbyPRD"] = TQbyPRD;

            return View();
        }
        public IActionResult PurchaseOrder()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["purchaseOrders"] = result.data;

            return View();
        }
        [Route("StoreClerk/PurchaseOrder/{PurchaseOrderId}")]
        public IActionResult PurchaseOrderDetail(int PurchaseOrderId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pod/" + PurchaseOrderId;
            Result<List<PurchaseOrderDetail>> result = HttpUtils.Get(url, new List<PurchaseOrderDetail>(), Request, Response);
            ViewData["purchaseOrderDetails"] = result.data;

            return View();
        }
        public IActionResult DeliveryOrder()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/po";
            Result<List<PurchaseOrder>> result = HttpUtils.Get(url, new List<PurchaseOrder>(), Request, Response);
            ViewData["deliveryOrders"] = result.data;

            return View();
        }
        [Route("StoreClerk/DeliveryOrder/{DeliveryOrderId}")]
        public IActionResult DeliveryOrderDetail(int DeliveryOrderId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/pod/" + DeliveryOrderId;
            Result<List<PurchaseOrderDetail>> result = HttpUtils.Get(url, new List<PurchaseOrderDetail>(), Request, Response);
            ViewData["deliveryOrderDetails"] = result.data;

            return View();
        }
        public bool UpdateDelivery([FromBody] List<PurchaseOrderDetail> purchaseOrderDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/updatepod";
            Result<Object> result = HttpUtils.Put(url, purchaseOrderDetails, Request, Response);
            return (bool)result.data;
        }

        /**
         * @author WUYUDING
         */
        public IActionResult GenerateRetrievalForm(string errMsg = "")
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/allrf";
            Result<List<Retrieval>> result = HttpUtils.Get(url, new List<Retrieval>(), Request, Response);
            ViewData["retrievals"] = result.data;

            ViewData["errMsg"] = errMsg;
            return View();
        }

        public IActionResult RetrievalForm(long date)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/ret/" + date;
            Result<Retrieval> result = HttpUtils.Get(url, new Retrieval(), Request, Response);
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
                return RedirectToAction("GenerateRetrievalForm", "StoreClerk", new { errMsg = result.msg });
            }
        }

        public IActionResult RetrievalDetail(int id)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/retid/" + id;
            Result<Retrieval> result = HttpUtils.Get(url, new Retrieval(), Request, Response);
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
                return View("RetrievalForm");
            }
            else
            {
                return RedirectToAction("GenerateRetrievalForm", "StoreClerk", new { errMsg = result.msg });
            }
        }

        [HttpPut]
        public bool Retrieval([FromBody] Retrieval retrieval)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/ret";
            Result<Object> result = HttpUtils.Put(url, retrieval, Request, Response);
            if (result.code != 200)
            {
                throw new Exception(result.msg);
            }
            return (bool)result.data;
        }

        /**
         * @author XJ
         */
        public IActionResult AdjustmentVoucher()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");
            ViewData["ClerkId"] = (int)HttpContext.Session.GetInt32("Id");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/adv";
            Result<List<AdjustmentVoucher>> result = HttpUtils.Get(url, new List<AdjustmentVoucher>(), Request, Response);
            ViewData["adjustmentVoucher"] = result.data;

            return View();
        }

        [Route("StoreClerk/AdjustmentVoucher/{advId}")]
        public IActionResult AdjustmentVoucherDetail(string advId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");
            ViewData["ClerkId"] = (int)HttpContext.Session.GetInt32("Id");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/findAdjustmentVoucher/" + advId;
            Result<AdjustmentVoucher> result1 = HttpUtils.Get(url1, new AdjustmentVoucher(), Request, Response);
            ViewData["adjustmentVoucher"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result2 = HttpUtils.Get(url2, new List<Product>(), Request, Response);
            ViewData["products"] = result2.data;

            return View();
        }

        public IActionResult GenerateAdjustmentVoucher()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/createav";
            Result<AdjustmentVoucher> result1 = HttpUtils.Get(url1, new AdjustmentVoucher(), Request, Response);
            ViewData["NewAV"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result2 = HttpUtils.Get(url2, new List<Product>(), Request, Response);
            ViewData["products"] = result2.data;

            return View();
        }

        [Route("/StoreClerk/GetProductPrice/{ProductId}")]
        public double GetProductPrice(string ProductId)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/FirstTenderbyProdutId/" + ProductId;
            Result<TenderQuotation> result = HttpUtils.Get(url, new TenderQuotation(), Request, Response);
            double tenderprice = (double)result.data.Unitprice;
            return tenderprice;
        }

        public bool SaveVoucher([FromBody] List<AdjustmentVoucherDetail> voucherDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/UpdateAdjustmentDetails/";
            Result<Object> result = HttpUtils.Put(url, voucherDetails, Request, Response);
            return (bool)result.data;
        }

        public bool SubmitVoucher([FromBody] List<AdjustmentVoucherDetail> voucherDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/SubmitAdjustmentDetails/";
            Result<Object> result = HttpUtils.Put(url, voucherDetails, Request, Response);
            return (bool)result.data;
        }
    }
}
