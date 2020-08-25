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
        public string GetRole()
        {
            string Role = HttpContext.Session.GetString("Role");
            if (Role == CommonConstant.ROLE.DEPARTMENT_HEAD)
            {
                string url1 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/cdel";
                Result<Employee> result1 = HttpUtils.Get(url1, new Employee(), Request, Response);
                if (result1.data != null && result1.data.Id == HttpContext.Session.GetInt32("Id"))
                {
                    return CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_DELEGATE];
                }
            }
            else if (Role == CommonConstant.ROLE.DEPARTMENT_EMPLOYEE)
            {
                string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/drep";
                Result<Employee> result = HttpUtils.Get(url, new Employee(), Request, Response);
                if (result.data.Id == HttpContext.Session.GetInt32("Id"))
                {
                    return CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_REPRESENTATIVE];
                }
            }
            return CommonConstant.ROLE_NAME[Role];
        }
        public string GetName()
        {
            return HttpContext.Session.GetString("Name");
        }
        public IActionResult RequestStationery()
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/catalogue";
            Result<List<Product>> result1 = HttpUtils.Get(url1, new List<Product>(), Request, Response);
            ViewData["products"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/createRF";
            Result<Requisition> result2 = HttpUtils.Post(url2, new Requisition(), new Requisition(), Request, Response);
            ViewData["requisitions"] = result2.data;

            return View();
        }
        public bool SaveRequest([FromBody] List<RequisitionDetail> requisitionDetail)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/updateRF";
            Result<Object> result = HttpUtils.Post(url, requisitionDetail, Request, Response);
            return (bool)result.data;
        }
        public bool SubmitRequest([FromBody] List<RequisitionDetail> requisitionDetail)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/submitrf";
            Result<Object> result = HttpUtils.Post(url, requisitionDetail, Request, Response);
            return (bool)result.data;
        }
        public IActionResult Requisition()
        {
            string role = GetRole();
            ViewData["Role"] = role;
            ViewData["Name"] = GetName();

            string url;
            if (role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_HEAD] || role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_DELEGATE])
            {
                url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfl";
            }
            else
            {
                url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfl";
            }
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["requisitions"] = result.data;

            return View();
        }
        [Route("Department/Requisition/{Id}")]
        public IActionResult RequisitionDetail(int Id)
        {
            string role = GetRole();
            ViewData["Role"] = role;
            ViewData["Name"] = GetName();

            string url1;
            if (role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_HEAD] || role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_DELEGATE])
            {
                url1 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/rfld/" + Id;
            }
            else
            {
                url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/rfld/" + Id;
            }
            Result<Requisition> result1 = HttpUtils.Get(url1, new Requisition(), Request, Response);
            ViewData["requisition"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/catalogue";
            Result<List<Product>> result2 = HttpUtils.Get(url2, new List<Product>(), Request, Response);
            ViewData["products"] = result2.data;

            ViewData["Id"] = HttpContext.Session.GetInt32("Id");

            return View();
        }
        public bool UpdateRequisition([FromBody] Requisition requisition)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/arr";
            Result<Object> result = HttpUtils.Put(url, requisition, Request, Response);
            return (bool)result.data;
        }

        public Requisition RepeatRequisition([FromBody] List<RequisitionDetail> requisitionDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/hrf";
            Result<Requisition> result = HttpUtils.Post(url, requisitionDetails, new Requisition(), Request, Response);
            return result.data;
        }

        public IActionResult Disbursement()
        {
            string role = GetRole();
            ViewData["Role"] = role;
            ViewData["Name"] = GetName();

            string url;
            if (role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_HEAD] || role == CommonConstant.ROLE_NAME[CommonConstant.ROLE.DEPARTMENT_DELEGATE])
            {
                url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/dis";
            }
            else
            {
                url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dis";
            }
            Result<List<Requisition>> result = HttpUtils.Get(url, new List<Requisition>(), Request, Response);
            ViewData["disbursements"] = result.data;

            return View();
        }
        public IActionResult DisbursementForm(long date)
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();

            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dis/" + date;
            Result<List<RequisitionDetail>> result = HttpUtils.Get(url, new List<RequisitionDetail>(), Request, Response);
            ViewData["disbursement"] = result.data;

            return View();
        }
        public bool AckDisbursement([FromBody] List<RequisitionDetail> requisitionDetails)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/ack";
            Result<Object> result = HttpUtils.Put(url, requisitionDetails, Request, Response);
            return (bool)result.data;
        }
        public IActionResult CollectionPoint()
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/dept";
            Result<Department> result1 = HttpUtils.Get(url1, new Department(), Request, Response);
            ViewData["departments"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/clist";
            Result<List<CollectionPoint>> result2 = HttpUtils.Get(url2, new List<CollectionPoint>(), Request, Response);
            ViewData["collectionPoints"] = result2.data;

            return View();
        }
        public bool SetCollectionPoint([FromBody] CollectionPoint collectionPoint)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/ucp";
            Result<Object> result = HttpUtils.Put(url, collectionPoint, Request, Response);
            return (bool)result.data;
        }
        public IActionResult Delegate()
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();

            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/gae";
            Result<List<Employee>> result = HttpUtils.Get(url, new List<Employee>(), Request, Response);
            ViewData["employees"] = result.data;

            DateTime dateTime = DateTime.UtcNow.Date;
            DateTimeOffset dt = new DateTimeOffset(dateTime, TimeSpan.Zero).ToUniversalTime();
            long date = dt.ToUnixTimeMilliseconds();
            List<Employee> delegates = result.data.OrderBy(x => x.DelegateFromDate).ToList();
            delegates.RemoveAll(x => x.DelegateToDate == null);
            delegates.RemoveAll(x => x.DelegateToDate < date);

            ViewData["delegates"] = delegates;

            return View();
        }
        public bool AssignDelegate([FromBody] Employee employee)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/del";
            Result<Object> result = HttpUtils.Put(url, employee, Request, Response);
            return (bool)result.data;
        }
        public IActionResult DeptRep()
        {
            ViewData["Role"] = GetRole();
            ViewData["Name"] = GetName();

            string url1 = cfg.GetValue<string>("Hosts:Boot") + "/depthead/gae";
            Result<List<Employee>> result1 = HttpUtils.Get(url1, new List<Employee>(), Request, Response);
            ViewData["employees"] = result1.data;

            string url2 = cfg.GetValue<string>("Hosts:Boot") + "/deptemp/drep";
            Result<Employee> result2 = HttpUtils.Get(url2, new Employee(), Request, Response);
            ViewData["deptrep"] = result2.data;

            return View();
        }

        public bool AssignDeptRep([FromBody] int Id)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/depthead/adr/" + Id;
            Result<Object> result = HttpUtils.Put(url, Id, Request, Response);
            return (bool)result.data;
        }
    }
}
