namespace SSIS_FRONT.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public long Date { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public int Balance { get; set; }
        public int UpdatedByEmpId { get; set; }
        public string RefCode { get; set; }

        // FKs
        public Product Product { get; set; }
        public Employee UpdatedByEmp { get; set; }
    }
}
