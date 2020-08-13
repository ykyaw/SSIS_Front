using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class PurchaseOrderDetail
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int PurchaseRequestDetailId { get; set; }
        public string ProductId { get; set; }
        public int QtyPurchased { get; set; }
        public int? QtyReceived { get; set; }
        public double TotalPrice { get; set; }
        public int? SupplierDeliveryNo { get; set; }
        public string Remark { get; set; }

        // FKs
        public PurchaseOrder PurchaseOrder { get; set; }
        public PurchaseRequestDetail PurchaseRequestDetail { get; set; }
        public Product Product { get; set; }
    }
}
