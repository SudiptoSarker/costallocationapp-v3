using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class ActualCost : Common
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public int Year { get; set; }
        public double ActualCostAmount { get; set; }
        public double ManHour { get; set; }

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

        public double OctPoint { get; set; }
        public double NovPoint { get; set; }
        public double DecPoint { get; set; }
        public double JanPoint { get; set; }
        public double FebPoint { get; set; }
        public double MarPoint { get; set; }
        public double AprPoint { get; set; }
        public double MayPoint { get; set; }
        public double JunPoint { get; set; }
        public double JulPoint { get; set; }
        public double AugPoint { get; set; }
        public double SepPoint { get; set; }
    }
}