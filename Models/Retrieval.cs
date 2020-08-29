using System.Collections.Generic;

namespace SSIS_FRONT.Models
{
    public class Retrieval
    {
        public int Id { get; set; }
        public int ClerkId { get; set; }
        public long? DisbursedDate { get; set; }
        public long? RetrievedDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public bool NeedAdjustment { get; set; }

        // FKs
        public List<RequisitionDetail> RequisitionDetails { get; set; }
        public Employee Clerk { get; set; }
    }
}
