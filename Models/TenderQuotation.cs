using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class TenderQuotation
    {
        public int Id { get; set; }
        public string SupplierId { get; set; }
        public int Year { get; set; }
        public string ProductId { get; set; }
        public double Unitprice { get; set; }
        public string Uom { get; set; }
        public int? Rank { get ; set; }

        // FKs
        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
    }
}
