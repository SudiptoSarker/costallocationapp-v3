﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class AssignmentHistoryViewModal
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string RootEmployeeName { get; set; }
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
        public bool IsDeleted { get; set; }
        public string MonthId_Points { get; set; }
        public string ApprovedCells { get; set; }
    }
}