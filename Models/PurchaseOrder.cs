﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string SupplierId { get; set; }
        public double TotalPrice { get; set; }
        public int OrderedByClerkId { get; set; }
        public long? OrderedDate { get; set; }
        public long SupplyByDate { get; set; }
        public int? ApprovedBySupId { get; set; }
        public string Status { get; set; }
        //public int? ReceivedByClerkId { get; set; }
        public int? CollectionPointId { get; set; }

        // FKs
        public Supplier Supplier { get; set; }
        public Employee OrderedByClerk { get; set; }
        public Employee ApprovedBySup { get; set; }
        //public Employee ReceivedByClerk { get; set; }
        public CollectionPoint CollectionPoint { get; set; }
    }
}
