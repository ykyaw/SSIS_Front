namespace SSIS_FRONT.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string EmployeeRole { get; set; }
        public string UrlPath { get; set; } // access allowed URLs
    }
}
