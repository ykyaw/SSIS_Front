namespace SSIS_FRONT.Models
{
    public class PurchaseRequestDetail
    {
        public int Id { get; set; }
        public long PurchaseRequestId { get; set; }
        public int CreatedByClerkId { get; set; }
        public string ProductId { get; set; }
        public string SupplierId { get; set; }
        public int CurrentStock { get; set; }
        public int ReorderQty { get; set; }
        public string? VenderQuote { get; set; } //Can't submit if null
        public double TotalPrice { get; set; }
        public long? SubmitDate { get; set; }
        public long? ApprovedDate { get; set; }
        public int? ApprovedBySupId { get; set; }
        public string Status { get; set; }
        public string? Remarks { get; set; }

        // FKs
        public Product Product { get; set; }
        public Employee CreatedByClerk { get; set; }
        public Supplier Supplier { get; set; }
        public Employee ApprovedBySup { get; set; }
    }
}
