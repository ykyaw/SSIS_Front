using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSIS_FRONT.Common;
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
            List<Product> products = new List<Product>();
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

        public IActionResult GenerateRetrieveForm()
        {
            return View();
        }

        public IActionResult RetrieveForm(string date)
        {
            //TODO call server api to generate this date retrieval form
            List<RequisitionDetail> requisitionDetails = new List<RequisitionDetail>();
            RequisitionDetail rd1 = new RequisitionDetail()
            {
                Product = new Product()
                {
                    Id = "1",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "pencils",
                        BinNo = "A7"
                    }
                },
                Requisition = new Requisition()
                {
                    Id = 1,
                    Department = new Department()
                    {
                        Id = "1",
                        Name = "English"
                    }
                },
                QtyNeeded = 5,
            };
            RequisitionDetail rd2 = new RequisitionDetail()
            {
                Product = new Product()
                {
                    Id = "1",
                    Category = new Category()
                    {
                        Id = 1,
                        Name = "pencils",
                        BinNo = "A7"
                    }
                },
                Requisition = new Requisition()
                {
                    Id = 2,
                    Department = new Department()
                    {
                        Id = "2",
                        Name = "Math"
                    }
                },
                QtyNeeded = 3,
            };
            requisitionDetails.Add(rd1);
            requisitionDetails.Add(rd2);
            Retrieval retrieval = new Retrieval()
            {
                Id = 128939232,
                Status = CommonConstant.RetrievalFormStatus.CREATING,
                Clerk = new Employee()
                {
                    Name = "Esther",
                    Id = 1
                },
                RequisitionDetails = requisitionDetails
            };
            ViewData["Retrieval"] = retrieval;
            return View();
        }
    }
}
