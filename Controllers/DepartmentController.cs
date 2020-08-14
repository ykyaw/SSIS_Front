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
            List<Requisition> requisitions = new List<Requisition>();
            ViewData["requisitions"] = requisitions;
            List<Product> products = new List<Product>();

            Product p1 = new Product();
            p1.Description = "pen";
            products.Add(p1);

            Product p2 = new Product();
            p2.Description = "pencil";
            products.Add(p2);

            Product p3 = new Product();
            p3.Description = "eraser";
            products.Add(p3);

            Product p4 = new Product();
            p4.Description = "ruler";
            products.Add(p4);

            Product p5 = new Product();
            p5.Description = "fountain";
            products.Add(p5);

            ViewData["products"] = products;
            return View();
        }

        public IActionResult viewRequisitionDeptHead()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfl";
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;

            return View();            
        }

        [Route("Department/viewRequisitionDeptHead/{RequisitionId}")]
        public IActionResult viewRequisitionDetailDeptHead(int RequisitionId)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfld/" + RequisitionId;
            Result<Requisition> result = HttpUtils.Get(url, new Requisition(), Request, Response);
            ViewData["requisition"] = result.data;
            return View();
        }

        

        public IActionResult viewRequisitionEmp()
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfl";
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;


            return View();
        }

        public IActionResult viewRfApprovedDetailDeptRep()
        {
            List<RequisitionDetail> requisitionDetail = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetail;
            return View();
        }

        public IActionResult viewRfCompletedDetailDeptRep()
        {
            List<RequisitionDetail> requisitionDetail = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetail;
            return View();
        }

        public IActionResult viewRfConfirmedDetailDeptRep()
        {
            List<RequisitionDetail> requisitionDetail = new List<RequisitionDetail>();
            ViewData["requisitionDetail"] = requisitionDetail;
            return View();
        }

        public IActionResult updateCollectionPoint()
        {

            //string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfl";
            //Result<List<CollectionPoint>> result = HttpUtils.Get(url, new List<CollectionPoint>(), Request, Response);
            //ViewData["requisitions"] = result.data;
            //return View();

            List<CollectionPoint> collectionPoints = new List<CollectionPoint>();
            ViewData["collectionPoint"] = collectionPoints;

            CollectionPoint c1 = new CollectionPoint();
            c1.Id = 1;
            c1.Location = "Management School";
            c1.CollectionTime = "11:00am";
            collectionPoints.Add(c1);

            CollectionPoint c2 = new CollectionPoint();
            c2.Id = 1;
            c2.Location = "Science School";
            c2.CollectionTime = "10:00am";
            collectionPoints.Add(c2);

            CollectionPoint c3 = new CollectionPoint();
            c3.Id = 1;
            c3.Location = "History School";
            c3.CollectionTime = "9:00am";
            collectionPoints.Add(c3);

            return View();
        }
        

        public IActionResult viewDisbBefAck()
        {
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
            List<Employee> employees = new List<Employee>();
            Employee e1 = new Employee();
            e1.Name = "Jame";
            employees.Add(e1);

            Employee e2 = new Employee();
            e2.Name = "Sam";
            employees.Add(e2);

            Employee e3 = new Employee();
            e3.Name = "Mary";
            employees.Add(e3);

            Employee e4 = new Employee();
            e4.Name = "Tom";
            employees.Add(e4);

            Employee e5 = new Employee();
            e5.Name = "Jerry";
            employees.Add(e5);

            ViewData["employees"] = employees;
            return View();
        }

        public IActionResult viewDisbursementDeptRep()
        {

            return View();
        }

        public IActionResult assignDeptRep()
        {
            
            List<Employee> employees = new List<Employee>();

            Employee e1 = new Employee();
            e1.Name = "Jame";
            employees.Add(e1);

            Employee e2 = new Employee();
            e2.Name = "Sam";
            employees.Add(e2);

            Employee e3 = new Employee();
            e3.Name = "Mary";
            employees.Add(e3);

            Employee e4 = new Employee();
            e4.Name = "Tom";
            employees.Add(e4);

            Employee e5 = new Employee();
            e5.Name = "Jerry";
            employees.Add(e5);

            ViewData["employees"] = employees;
            return View();
        }
    }
}