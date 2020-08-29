using System.Collections.Generic;

namespace SSIS_FRONT.Models
{
    public class Requisition
    {
        public int Id { get; set; }
        public string DepartmentId { get; set; }
        public int ReqByEmpId { get; set; }
        public long? SubmittedDate { get; set; }
        public int? ApprovedById { get; set; }
        public long? ApprovalDate { get; set; }
        public string Remarks { get; set; }
        public int? ProcessedByClerkId { get; set; }
        public long CreatedDate { get; set; }
        public string Status { get; set; }
        public int? CollectionPointId { get; set; }
        public long? CollectionDate { get; set; }
        public int? ReceivedByRepId { get; set; }
        public long? ReceivedDate { get; set; }
        public int? AckByClerkId { get; set; }
        public long? AckDate { get; set; }

        // FKs
        public Department Department { get; set; }
        public Employee ReqByEmp { get; set; }
        public Employee ApprovedBy { get; set; }
        public Employee ProcessedByClerk { get; set; }
        public Employee ReceivedByRep { get; set; }
        public Employee AckByClerk { get; set; }
        public CollectionPoint CollectionPoint { get; set; }
        public List<RequisitionDetail> RequisitionDetails { get; set; }
    }
}
