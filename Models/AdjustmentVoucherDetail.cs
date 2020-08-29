namespace SSIS_FRONT.Models
{
    public class AdjustmentVoucherDetail
    {
        public int Id { get; set; }
        public string AdjustmentVoucherId { get; set; }
        public string ProductId { get; set; }
        public int QtyAdjusted { get; set; }
        public double TotalPrice { get; set; }
        public double Unitprice { get; set; }
        public string Reason { get; set; }

        // FKs
        public AdjustmentVoucher AdjustmentVoucher { get; set; }
        public Product Product { get; set; }
    }
}
