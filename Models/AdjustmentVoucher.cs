using System.Collections.Generic;

namespace SSIS_FRONT.Models
{
    public class AdjustmentVoucher
    {
        public string Id { get; set; }
        public int InitiatedClerkId { get; set; }
        public long InitiatedDate { get; set; }
        public int? ApprovedSupId { get; set; }
        public long? ApprovedSupDate { get; set; }
        public int? ApprovedMgrId { get; set; }
        public long? ApprovedMgrDate { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }

        // FKs
        public List<AdjustmentVoucherDetail> AdjustmentVoucherDetails { set; get; }
        public Employee InitiatedClerk { get; set; }
        public Employee ApprovedSup { get; set; }
        public Employee ApprovedMgr { get; set; }
    }
}
