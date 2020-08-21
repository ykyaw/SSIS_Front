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
    public class SupervisorController : Controller
    {

        protected HttpClient httpClient;
        protected IConfiguration cfg;
        public SupervisorController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Route("Store/Vouchers")]
        public IActionResult Vouchers()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/allvoucher";
            Result<List<AdjustmentVoucher>> result = HttpUtils.Get(url, new List<AdjustmentVoucher>(), Request, Response);
            ViewData["Vouchers"] = result.data;
            return View();
        }

        [Route("Voucher/{Id}")]
        public IActionResult VoucherDetail(string Id)
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/voucher/"+Id;
            Result<AdjustmentVoucher> result = HttpUtils.Get(url, new AdjustmentVoucher(), Request, Response);
            Employee employee = new Employee()
            {
                Name = HttpContext.Session.GetString("Name"),
                Role = HttpContext.Session.GetString("Role")
            };
            if (result.data.AdjustmentVoucherDetails == null)
            {
                result.data.AdjustmentVoucherDetails = new List<AdjustmentVoucherDetail>();
            }
            ViewData["Employee"] = employee;
            ViewData["Voucher"] = result.data;
            return View();
        }

        [HttpPut]
        [Route("Store/Voucher/{id}")]
        public bool UpdateVoucherStatuc([FromBody]AdjustmentVoucher voucher)
        {
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/voucher/" + voucher.Id;
            Result<Object> result = HttpUtils.Put(url, voucher, Request, Response);//TODO need the exception message more detail
            return (bool)result.data;
        }

        public IActionResult BarChart()
        {
            return View();
        }

        public IActionResult Forecast()
        {
            ViewData["Role"] = CommonConstant.ROLE_NAME[(string)HttpContext.Session.GetString("Role")];
            ViewData["Name"] = (string)HttpContext.Session.GetString("Name");

            string url = cfg.GetValue<string>("Hosts:Boot") + "/storeclerk/catalogue";
            Result<List<Product>> result = HttpUtils.Get(url, new List<Product>(), Request, Response);
            ViewData["Items"] = result.data;
            return View();
        }

        [HttpPut]
        [Route("Store/PurchaseRequestDetail")]
        public bool UpdatePurchaseRequestDetail([FromBody]List<PurchaseRequestDetail> details)
        {
            
                    /* THIS DOES NOT WORK */
            //string url = cfg.GetValue<string>("Hosts:Boot") + "﻿/storesup/updatepr";
            //Result<Object> result = HttpUtils.Put(url, details, Request, Response);

            
                    /*BUT THIS ONE WORK. WHY?!*/
            string url = cfg.GetValue<string>("Hosts:Boot") + "/storesup/updatepr";
            Result<Object> result = HttpUtils.Put(url, details, Request, Response);

            return (bool)result.data;
        }

    }
}
