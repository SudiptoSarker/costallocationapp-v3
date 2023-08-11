using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Dtos;
using CostAllocationApp.Models;

namespace CostAllocationApp.ViewModels
{
    public class ForecastAssignmentViewModel
    {
        // for assignment
        public int SerialNumber { get; set; }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string InchargeId { get; set; }
        public string InchargeName { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string ExplanationId { get; set; }
        public string ExplanationName { get; set; }
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string UnitPrice { get; set; }
        public string GradeId { get; set; }
        public string GradePoint { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        public int SubCode { get; set; }
        public bool MarkedAsRed { get; set; }
        public string EmployeeNameWithCodeRemarks { get; set; }
        public bool BCYR { get; set; }
        public string BCYRCell { get; set; }
        public bool BCYRApproved { get; set; }
        public string BCYRCellApproved { get; set; }
        public bool IsApproved { get; set; }
        public string BCYRCellPending { get; set; }
        public bool IsRowPending { get; set; }
        public bool IsDeletePending { get; set; }
        public int Year { get; set; }
        public int EmployeeAssignmentIdOrg { get; set; }

        public bool IsAddEmployee { get; set; }
        public bool IsDeleteEmployee { get; set; }
        public bool IsCellWiseUpdate { get; set; }
        public string ApprovedCells { get; set; }
        public string RootEmployeeName { get; set; }
        // for forecast
        public List<ForecastDto> forecasts { get; set; }
        public List<ActualCost> ActualCosts { get; set; }

        // points
        public string OctPoints { get; set; }
        public string NovPoints { get; set; }
        public string DecPoints { get; set; }
        public string JanPoints { get; set; }
        public string FebPoints { get; set; }
        public string MarPoints { get; set; }
        public string AprPoints { get; set; }
        public string MayPoints { get; set; }
        public string JunPoints { get; set; }
        public string JulPoints { get; set; }
        public string AugPoints { get; set; }
        public string SepPoints { get; set; }

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

        public string TotalManMonth { get; set; }
        //public string OctTotalMM { get; set; }
        //public string NovTotalMM { get; set; }
        //public string DecTotalMM { get; set; }
        //public string JanTotalMM { get; set; }
        //public string FebTotalMM { get; set; }
        //public string MarTotalMM { get; set; }
        //public string AprTotalMM { get; set; }
        //public string MayTotalMM { get; set; }
        //public string JunTotalMM { get; set; }
        //public string JulTotalMM { get; set; }
        //public string AugTotalMM { get; set; }
        //public string SepTotalMM { get; set; }
    }
}