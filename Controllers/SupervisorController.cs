using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSIS_FRONT.Common;
using SSIS_FRONT.Components;
using SSIS_FRONT.Models;
using SSIS_FRONT.Utils;

namespace SSIS_FRONT.Controllers
{
    public class SupervisorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [Route("Store/Vouchers")]
        public IActionResult Vouchers()
        {
            //string url = cfg.GetValue<string>("Hosts:Boot") + "";
            //Result < List <AdjustmentVoucher >> result = HttpUtils.Get(url, new List<AdjustmentVoucher>(), Request, Response);
            List<AdjustmentVoucher> vouchers = new List<AdjustmentVoucher>();
            AdjustmentVoucher a1 = new AdjustmentVoucher()
            {
                Id = "99",
                InitiatedDate = 1597223258858,
                Status = CommonConstant.AdjVoucherStatus.PENDING_APPROVE
            };
            AdjustmentVoucher a2 = new AdjustmentVoucher()
            {
                Id = "98",
                InitiatedDate = 1597223258858,
                Status = CommonConstant.AdjVoucherStatus.APPROVED
            };
            AdjustmentVoucher a3 = new AdjustmentVoucher()
            {
                Id = "97",
                InitiatedDate = 1597223258858,
                Status = CommonConstant.AdjVoucherStatus.REJECTED
            };
            vouchers.Add(a1);
            vouchers.Add(a2);
            vouchers.Add(a3);
            ViewData["Vouchers"] = vouchers;
            return View();
        }

        [Route("Voucher/{Id}")]
        public IActionResult VoucherDetail(string Id)
        {
            List<AdjustmentVoucherDetail> details = new List<AdjustmentVoucherDetail>();
            AdjustmentVoucherDetail d1 = new AdjustmentVoucherDetail()
            {
                Id = 1,
                Product = new Product()
                {
                    Id = "E005",
                    Description = "Eraser",
                },
                QtyAdjusted = 3,
                Reason = "Free Gift in Offer Pack",
                TotalPrice = 6,
            };

            AdjustmentVoucherDetail d2 = new AdjustmentVoucherDetail()
            {
                Id = 2,
                Product = new Product()
                {
                    Id = "H014",
                    Description = "Highlighter Yellow",
                },
                QtyAdjusted = -10,
                Reason = "Dry Ink",
                TotalPrice = 300,
            };
            details.Add(d1);
            details.Add(d2);
            AdjustmentVoucher a1 = new AdjustmentVoucher()
            {
                Id = "99",
                InitiatedDate = 1597223258858,
                Status = CommonConstant.AdjVoucherStatus.PENDING_APPROVE,
                details = details
            };
            Employee e1 = new Employee()
            {
                Id = 2,
                Name = "Peter",
                Role = CommonConstant.ROLE.STORE_SUPERVISOR
            };
            Employee e2 = new Employee()
            {
                Id = 3,
                Name = "James",
                Role = CommonConstant.ROLE.STORE_MANAGER
            };
            ViewData["Employee"] = e1;
            ViewData["Voucher"] = a1;
            return View();
        }

        [HttpPut]
        [Route("Store/Voucher/{id}")]
        public bool UpdateVoucherStatuc([FromBody]AdjustmentVoucher voucher)
        {
            //TODO update voucher
            return true;
        }

        
    }
}
