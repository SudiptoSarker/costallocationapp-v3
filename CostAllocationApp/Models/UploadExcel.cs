using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class UploadExcel
    {
        public int EmployeeId { get; set; }
        public int? SectionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? InchargeId { get; set; }
        public int? RoleId { get; set; }
        public int? ExplanationId { get; set; }
        public int? CompanyId { get; set; }
        public int? GradeId { get; set; }
        public double UnitPrice { get; set; }
        public string Remarks { get; set; }
        public string EmployeeName { get; set; }

        public string DuplicateFrom { get; set; }
        public string DuplicateCount { get; set; }
        public string RoleChanged { get; set; }
        public string UnitPriceChanged { get; set; }
    }
}