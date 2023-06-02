using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class AssignmentHistory:Common
    {
        public int Id { get; set; }
        public string TimeStampId { get; set; }
        public string Year { get; set; }
        public string EmployeeId { get; set; }
        public string SectionId { get; set; }
        public string DepartmentId { get; set; }
        public string InChargeId { get; set; }
        public string RoleId { get; set; }
        public string ExplanationId { get; set; }
        public string CompanyId { get; set; }
        public string UnitPrice { get; set; }
        public string GradeId { get; set; }
        public string EmployeeAssignmentId { get; set; }
        public string MonthId_Points { get; set; }

        //public List<Forecast> Forecasts { get; set; }
    }
}