namespace SSIS_FRONT.Models
{
    public class Supplier
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ContactPersonName { get; set; }
        public int PhoneNo { get; set; }
        public int? FaxNo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string GstRegNo { get; set; }
    }
}
