using System.Collections.Generic;

namespace SSIS_FRONT.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BinNo { get; set; }

        public Category() { }
        public Category(string Name, string BinNo)
        {
            this.Name = Name;
            this.BinNo = BinNo;
        }
        public virtual List<Product> Products { get; set; }
    }
}
