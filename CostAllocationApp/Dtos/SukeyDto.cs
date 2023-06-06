using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Dtos
{
    public class SukeyDto
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
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
    }
}