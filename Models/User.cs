﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSIS_FRONT.Models
{
    public class User
    {
        public int id { set; get; }
        public string email { get; set; }

        public string username { get; set; }

        public string password { get; set; }
    }
}
