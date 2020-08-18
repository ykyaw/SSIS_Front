using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class DepartmentController : Controller
    {
        protected HttpClient httpClient;
        protected IConfiguration cfg;
        public DepartmentController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult reqStationery()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);

            ViewData["products"] = result.data;


            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/createRF";
            Result<Requisition> result2 = HttpUtils.Post(url2, new Requisition(), new Requisition(), Request, Response);

            ViewData["requisitions"] = result2.data;
            return View();
        }

        [HttpPut]
        public bool RequisitionDetail([FromBody] List<RequisitionDetail> requisitionDetail)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/updateRF"; //return null for now from backend function 
            Result<Object> result = HttpUtils.Post(url, requisitionDetail, Request, Response);
            return (bool)result.data;
        }


        [HttpPost]
        public bool RequisitionDetailSubmit([FromBody] List<RequisitionDetail> requisitionDetail)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/submitrf"; //return null for now from backend function 
            Result<Object> result = HttpUtils.Post(url, requisitionDetail, Request, Response);
            return (bool)result.data;
        }

        public IActionResult viewRequisitionDeptHead()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfl";
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;

            return View();
        }

        [Route("Department/viewRequisitionDeptHead/{RequisitionId}")]
        public IActionResult viewRequisitionDetailDeptHead(int RequisitionId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfld/" + RequisitionId;
            Result<Requisition> result = HttpUtils.Get(url, new Requisition(), Request, Response);
            ViewData["requisition"] = result.data;
            return View();
        }



        public IActionResult viewRequisitionEmp()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfl";
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;


            return View();
        }

        [Route("Department/viewRequisitionEmp/{RequisitionId}")]
        public IActionResult viewRequisitionDetailEmp(int RequisitionId)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfld/" + RequisitionId;
            Result<Requisition> result1 = HttpUtils.Get(url1, new Requisition(), Request, Response);
            ViewData["requisition"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/catalogue";
            Result<List<Product>> result2 = HttpUtils.Get(url2, new List<Product>(), Request, Response);
            ViewData["products"] = result2.data;

            List<RequisitionDetail> requisitionDetails = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetails;

            RequisitionDetail rd1 = new RequisitionDetail();
            //rd1.Product.Description = "Clips";
            rd1.QtyNeeded = 10;
            rd1.QtyDisbursed = 8;
            rd1.DisburseRemark = "Insufficient stock";
            rd1.QtyReceived = 8;
            rd1.RepRemark = "Receieved in good order";
            requisitionDetails.Add(rd1);


            return View();
        }



        public IActionResult updateCollectionPoint()
        {

            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");


            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dept";
            Result<Department> result1 = HttpUtils.Get(url1, new Department(), Request, Response);
            ViewData["departments"] = result1.data;


            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/clist";
            Result<List<CollectionPoint>> result2 = HttpUtils.Get(url2, new List<CollectionPoint>(), Request, Response);
            ViewData["collectionpoints"] = result2.data;

            return View();
        }


        [HttpPut]
        public bool Savecp([FromBody] CollectionPoint collectionPoint)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/ucp";
            Result<Object> result = HttpUtils.Put(url, collectionPoint, Request, Response);
            return (bool)result.data;
        }



        public IActionResult viewDisbBefAck()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            List<RequisitionDetail> requisitionDetails = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetails;

            RequisitionDetail rd1 = new RequisitionDetail();
            //rd1.Product.Description = "Clips";
            rd1.QtyNeeded = 10;
            rd1.QtyDisbursed = 8;
            rd1.DisburseRemark = "Insufficient stock";
            requisitionDetails.Add(rd1);

            return View();
        }

        public IActionResult viewDisbAftAck()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            List<RequisitionDetail> requisitionDetails = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetails;

            RequisitionDetail rd1 = new RequisitionDetail();
            //rd1.Product.Description = "Clips";
            rd1.QtyNeeded = 10;
            rd1.QtyDisbursed = 8;
            rd1.DisburseRemark = "Insufficient stock";
            rd1.QtyReceived = 8;
            rd1.RepRemark = "Receieved in good order";
            requisitionDetails.Add(rd1);

            List<Requisition> requisitions = new List<Requisition>();
            ViewData["requisition"] = requisitions;

            Requisition rq = new Requisition();
            //rd1.Product.Description = "Clips";
            rq.ReceivedByRepId = 1;
            rq.ReceivedDate = 07082020;

            requisitions.Add(rq);



            return View();
        }

        public IActionResult delegateAuthority()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/gae";
            Result<List<Employee>> result1 = HttpUtils.Get(url1, new List<Employee>(), Request, Response);
            ViewData["employees"] = result1.data;



            return View();
        }

        [HttpPut]
        public bool assignDelegate([FromBody] Employee employee)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/del";
            Result<Object> result = HttpUtils.Put(url,employee, Request, Response);
            return (bool)result.data;
        }

        public IActionResult viewDisbursementDeptRep(string errMsg = "")
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");
            ViewData["errMsg"] = errMsg;

            //string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dis/" + collectiondate;
            //Result<List<RequisitionDetail>> result1 = HttpUtils.Get(url1, new List<RequisitionDetail>(), Request, Response);
            //ViewData["departments"] = result1.data;
            return View();
        }

        public IActionResult viewDisbursementDetailDeptRep([FromBody] long collectiondate)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dis/" + collectiondate;
            Result<List<RequisitionDetail>> result1 = HttpUtils.Get(url1, new List<RequisitionDetail>(),Request, Response);
            ViewData["requisition"] = result1.data;
            return View();
        }

        public bool AckDisbursement([FromBody] List<RequisitionDetail> requisitionDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/ack"; 
            Result<Object> result = HttpUtils.Put(url, requisitionDetails, Request, Response);
            return (bool)result.data;
        }

        public IActionResult assignDeptRep()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/gae";
            Result<List<Employee>> result2 = HttpUtils.Get(url2, new List<Employee>(), Request, Response);
            ViewData["employees"] = result2.data;

            return View();
        }

        [HttpPut]
        public bool assignDeptRep([FromBody] int employeeID)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/adr/" + employeeID;
            Result<Object> result = HttpUtils.Put(url, employeeID, Request, Response);
            return (bool)result.data;
        }

    }
}
