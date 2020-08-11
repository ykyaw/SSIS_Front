using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSIS_FRONT.Models;

namespace SSIS_FRONT.Controllers
{
    public class StoreClerkController : Controller
    {
        protected HttpClient httpClient;
        protected IConfiguration cfg;
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Catalogue()
        {
            List<Product> products = new List<Product>;
            ViewData["products"] = products;
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
