namespace SSIS_FRONT.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int ReorderLvl { get; set; }
        public int ReorderQty { get; set; }
        public string Uom { get; set; }
        public virtual Category Category { get; set; }
        public Product() { }
        public Product(string Id, string Description, int CategoryId)
        {
            this.Id = Id;
            this.Description = Description;
            this.CategoryId = CategoryId;
        }
    }
}
