using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.ViewModels
{
    public class ForecastAssignmentViewModel1
    {
        // for assignment
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string SectionId { get; set; }
        public string DepartmentId { get; set; }
        public string InchargeId { get; set; }
        public string RoleId { get; set; }
        public string ExplanationId { get; set; }
        public string CompanyId { get; set; }
        public string GradeId { get; set; }
        public string UnitPrice { get; set; }
        


        // points
        public decimal OctPoints { get; set; }
        public decimal NovPoints { get; set; }
        public decimal DecPoints { get; set; }
        public decimal JanPoints { get; set; }
        public decimal FebPoints { get; set; }
        public decimal MarPoints { get; set; }
        public decimal AprPoints { get; set; }
        public decimal MayPoints { get; set; }
        public decimal JunPoints { get; set; }
        public decimal JulPoints { get; set; }
        public decimal AugPoints { get; set; }
        public decimal SepPoints { get; set; }

        //total
        public string OctTotal { get; set; }
        public string NovTotal { get; set; }
        public string DecTotal { get; set; }
        public string JanTotal { get; set; }
        public string FebTotal { get; set; }
        public string MarTotal { get; set; }
        public string AprTotal { get; set; }
        public string MayTotal { get; set; }
        public string JunTotal { get; set; }
        public string JulTotal { get; set; }
        public string AugTotal { get; set; }
        public string SepTotal { get; set; }
    }
}