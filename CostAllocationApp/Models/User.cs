using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class User:Common
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserTitle { get; set; }
        public string DepartmentId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
        public DateTime LoginTime { get; set; }

    }
}