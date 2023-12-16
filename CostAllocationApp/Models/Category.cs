using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class Category:Common
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public string DynamicTableId { get; set; }
        public string SubTitleName { get; set; }
        public bool IsSubTitle { get; set; }
    }
}