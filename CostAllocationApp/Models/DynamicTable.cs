using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DynamicTable : Common
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string TableTitle { get; set; }
        public int TablePosition { get; set; }
        public bool IsActive { get; set; }

    }
}