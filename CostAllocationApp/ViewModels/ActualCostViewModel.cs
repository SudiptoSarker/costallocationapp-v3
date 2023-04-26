using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class ActualCostViewModel
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int Year { get; set; }

        public double OctCost { get; set; }
        public double NovCost { get; set; }
        public double DecCost { get; set; }
        public double JanCost { get; set; }
        public double FebCost { get; set; }
        public double MarCost { get; set; }
        public double AprCost { get; set; }
        public double MayCost { get; set; }
        public double JunCost { get; set; }
        public double JulCost { get; set; }
        public double AugCost { get; set; }
        public double SepCost { get; set; }


        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string SectionId { get; set; }
        public string DepartmentId { get; set; }
        public string InchargeId { get; set; }
        public string RoleId { get; set; }
        public string ExplanationId { get; set; }
        public string CompanyId { get; set; }
        //public decimal UnitPrice { get; set; }
        //public int? GradeId { get; set; }
        //public string IsActive { get; set; }
        public string Remarks { get; set; }
        public string SubCode { get; set; }
    }
}