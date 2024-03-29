﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class Department : Common
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public int SectionId { get; set; }
        public bool IsActive { get; set; }
        public string SectionName { get; set; }
        public string SubCategoryId { get; set; }
        public bool IsUpdate { get; set; }
    }
}