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
        public int? RetrievalId { get; set; }

        // FKs
        public Requisition Requisition { get; set; }
        public Product Product { get; set; }
    }
}