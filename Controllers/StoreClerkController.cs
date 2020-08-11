using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    }
}
