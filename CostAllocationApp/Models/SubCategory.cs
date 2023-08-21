using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class SubCategory:Common
    {
        public int Id { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryId { get; set; }
        // for display category name
        public string CategoryName { get; set; }
    }
}