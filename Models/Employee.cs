using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNo { get; set; }
        public string DepartmentId { get; set; }
        public int ManagerId { get; set; }
        public long? DelegateFromDate { get; set; }
        public long? DelegateToDate { get; set; }
        public string Role { get; set; }

        // FKs
        public Department Department { get; set; }
        public Employee Manager { get; set; }
    }
}
