using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class ApprovalHistoryViewModal
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public string SectionName { get; set; }
        public string DepartmentName { get; set; }
        public string InChargeName { get; set; }
        public string RoleName { get; set; }
        public string ExplanationName { get; set; }
        public string CompanyName { get; set; }
        public string GradePoints { get; set; }
        public string UnitPrice { get; set; }
        public string Remarks { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsAddEmployee { get; set; }
        public bool IsDeleteEmployee { get; set; }
        public bool IsCellWiseUpdate { get; set; }

    }
}