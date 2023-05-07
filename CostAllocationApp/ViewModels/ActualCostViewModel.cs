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

        public string OctCost { get; set; }
        public string NovCost { get; set; }
        public string DecCost { get; set; }
        public string JanCost { get; set; }
        public string FebCost { get; set; }
        public string MarCost { get; set; }
        public string AprCost { get; set; }
        public string MayCost { get; set; }
        public string JunCost { get; set; }
        public string JulCost { get; set; }
        public string AugCost { get; set; }
        public string SepCost { get; set; }


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