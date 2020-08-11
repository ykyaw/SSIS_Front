﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BinNo { get; set; } // Same value as Id?

        public Category() { }
        public Category(string Name, string BinNo)
        {
            this.Name = Name;
            this.BinNo = BinNo;
        }
        public virtual List<Product> Products { get; set; }
    }
}
