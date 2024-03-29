﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Dtos
{
    public class ExcelAssignmentDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public int? SectionId { get; set; }
        public int? DepartmentId { get; set; }
        public int? InchargeId { get; set; }
        public int? RoleId { get; set; }
        public int? ExplanationId { get; set; }
        public int? CompanyId { get; set; }
        public decimal UnitPrice { get; set; }
        public int? GradeId { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        //public int SubCode { get; set; }
        public string Year { get; set; }
        public bool BCYR { get; set; }
        public string BCYRCell { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeRootName { get; set; }
        public string EmployeeModifiedName { get; set; }
        public bool IsDeleted { get; set; }
        public string BCYRCellPending { get; set; }
        public bool IsRowPending { get; set; }

        public decimal OctPoint { get; set; }
        public decimal NovPoint { get; set; }
        public decimal DecPoint { get; set; }
        public decimal JanPoint { get; set; }
        public decimal FebPoint { get; set; }
        public decimal MarPoint { get; set; }
        public decimal AprPoint { get; set; }
        public decimal MayPoint { get; set; }
        public decimal JunPoint { get; set; }
        public decimal JulPoint { get; set; }
        public decimal AugPoint { get; set; }
        public decimal SepPoint { get; set; }


        public string DuplicateFrom { get; set; }
        public string DuplicateCount { get; set; }
        public string RoleChanged { get; set; }
        public string UnitPriceChanged { get; set; }
    }
}