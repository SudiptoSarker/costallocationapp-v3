using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class QaProportionViewModel
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentId { get; set; }

        public double OctPercentage { get; set; }
        public double NovPercentage { get; set; }
        public double DecPercentage { get; set; }
        public double JanPercentage { get; set; }
        public double FebPercentage { get; set; }
        public double MarPercentage { get; set; }
        public double AprPercentage { get; set; }
        public double MayPercentage { get; set; }
        public double JunPercentage { get; set; }
        public double JulPercentage { get; set; }
        public double AugPercentage { get; set; }
        public double SepPercentage { get; set; }
    }
}