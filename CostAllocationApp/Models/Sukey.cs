using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class Sukey:Common
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
    }
}