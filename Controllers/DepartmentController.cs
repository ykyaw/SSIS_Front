using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SSIS_FRONT.Models;

namespace SSIS_FRONT.Controllers
{
    public class DepartmentController : Controller
    {
        protected HttpClient httpClient;
        protected IConfiguration cfg;
        public DepartmentController(HttpClient httpClient, IConfiguration cfg)
        {
            this.httpClient = httpClient;
            this.cfg = cfg;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult reqStationery()
        {
            List<Requisition> requisitions = new List<Requisition>();
            ViewData["requisitions"] = requisitions;
            List<Product> products = new List<Product>();

            Product p1 = new Product();
            p1.Description = "pen";
            products.Add(p1);

            Product p2 = new Product();
            p2.Description = "pencil";
            products.Add(p2);

            Product p3 = new Product();
            p3.Description = "eraser";
            products.Add(p3);

            Product p4 = new Product();
            p4.Description = "ruler";
            products.Add(p4);

            Product p5 = new Product();
            p5.Description = "fountain";
            products.Add(p5);

            ViewData["products"] = products;
            return View();
        }

        public IActionResult viewRequisition()
        {
            return View();
        }

        public IActionResult assignDeptRep()
        {
            
            List<Employee> employees = new List<Employee>();

            Employee e1 = new Employee();
            e1.Name = "Jame";
            employees.Add(e1);

            Employee e2 = new Employee();
            e2.Name = "Sam";
            employees.Add(e2);

            Employee e3 = new Employee();
            e3.Name = "Mary";
            employees.Add(e3);

            Employee e4 = new Employee();
            e4.Name = "Tom";
            employees.Add(e4);

            Employee e5 = new Employee();
            e5.Name = "Jerry";
            employees.Add(e5);

            ViewData["employees"] = employees;
            return View();
        }
    }
}