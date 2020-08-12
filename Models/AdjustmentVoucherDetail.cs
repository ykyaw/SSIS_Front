using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class AdjustmentVoucherDetail
    {
        public int Id { get; set; }
        public string AdjustmentVoucherId { get; set; }
        public string ProductId { get; set; }
        public int QtyAdjusted { get; set; }
        public double TotalPrice { get; set; }

        // FKs
        public AdjustmentVoucher AdjustmentVoucher { get; set; }
        public Product Product { get; set; }
    }
}
