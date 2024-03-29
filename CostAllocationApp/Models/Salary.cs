﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class Salary : Common
    {
        public int Id { get; set; }
        public string SalaryGrade { get; set; }
        public decimal SalaryLowPoint { get; set; }
        public decimal SalaryHighPoint { get; set; }
        public bool IsActive { get; set; }
        public string SalaryLowPointWithComma { get; set; }
        public string SalaryHighPointWithComma { get; set; }
        public bool IsUpdate { get; set; }
    }
}