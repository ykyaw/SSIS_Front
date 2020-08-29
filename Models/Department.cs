using System;
using System.Collections.Generic;
namespace SSIS_FRONT.Models
{
    public class Department
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int PhoneNo { get; set; }
        public int? FaxNo { get; set; }
        public int RepId { get; set; }
        public int HeadId { get; set; }
        public int CollectionPointId { get; set; }

        // FKs
        public Employee Rep { get; set; }
        public Employee Head { get; set; }
        public CollectionPoint CollectionPoint { get; set; }
    }
}
