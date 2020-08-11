using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class RequisitionDetail
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public string ProductId { get; set; }
        public int QtyNeeded { get; set; }
        public int QtyDisbursed { get; set; }
        public int QtyReceived { get; set; }
        public string DisburseRemark { get; set; }
        public string RepRemark { get; set; }
        public string ClerkRemark { get; set; }

        // FKs
        public Requisition Requisition { get; set; }
        public Product Product { get; set; }
    }
}