using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DeatailsItem : Common
    {
        public int Id { get; set; }
        public string DetailsItemName { get; set; }
        public string SubCategoryId { get; set; }
        public bool IsActive { get; set; }

    }
}