using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class EmployeeBudget : Common
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public int? SectionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? InchargeId { get; set; }
        public int? RoleId { get; set; }
        public string ExplanationId { get; set; }
        public int? CompanyId { get; set; }
        public decimal UnitPrice { get; set; }
        public int? GradeId { get; set; }
        public string IsActive { get; set; }
        public string Remarks { get; set; }
        public int SubCode { get; set; }
        public string Year { get; set; }
        public bool BCYR { get; set; }
        public string BCYRCell { get; set; }
        public bool BCYRApproved { get; set; }
        public string BCYRCellApproved { get; set; }
        public bool IsDeleted { get; set; }
        public string BCYRCellPending { get; set; }
        public bool IsRowPending { get; set; }
        public bool IsDeletePending { get; set; }
        public bool IsActiveAssignment { get; set; }
        public bool FirstHalfBudget { get; set; }
        public bool SecondHalfBudget { get; set; }
        public bool FinalizedBudget { get; set; }

        public string EmployeeRootName { get; set; }
        public string EmployeeModifiedName { get; set; }

        public string DuplicateFrom { get; set; }
        public string DuplicateCount { get; set; }
        public string RoleChanged { get; set; }
        public string UnitPriceChanged { get; set; }
    }
}