using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class ForecastDistributdViewModal
    {
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

        public bool IsAddEmployee { get; set; }
        public bool IsDeleteEmployee { get; set; }
        public bool IsCellWiseUpdate { get; set; }
        public string ApprovedCells { get; set; }
        public string RootEmployeeName { get; set; }
        // for forecast

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

        // percent
        public decimal OctPercentage { get; set; }
        public decimal NovPercentage { get; set; }
        public decimal DecPercentage { get; set; }
        public decimal JanPercentage { get; set; }
        public decimal FebPercentage { get; set; }
        public decimal MarPercentage { get; set; }
        public decimal AprPercentage { get; set; }
        public decimal Maypercentage { get; set; }
        public decimal JunPercentage { get; set; }
        public decimal JulPercentage { get; set; }
        public decimal AugPercentage { get; set; }
        public decimal SepPercentage { get; set; }

    }
}