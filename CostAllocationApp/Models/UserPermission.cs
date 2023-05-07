using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class UserPermission
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public int UserId { get; set; }

    }
}