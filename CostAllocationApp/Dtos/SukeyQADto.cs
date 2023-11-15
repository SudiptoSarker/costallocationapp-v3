using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Dtos
{
    public class SukeyQADto
    {
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string InchargeId { get; set; }
        public string InchargeName { get; set; }
        public string DependencyName { get; set; }

        public List<double> OctCost { get; set; } = new List<double>();
        public List<double> NovCost { get; set; } = new List<double>();
        public List<double> DecCost { get; set; } = new List<double>();
        public List<double> JanCost { get; set; } = new List<double>();
        public List<double> FebCost { get; set; } = new List<double>();
        public List<double> MarCost { get; set; } = new List<double>();
        public List<double> AprCost { get; set; } = new List<double>();
        public List<double> MayCost { get; set; } = new List<double>();
        public List<double> JunCost { get; set; } = new List<double>();
        public List<double> JulCost { get; set; } = new List<double>();
        public List<double> AugCost { get; set; } = new List<double>();
        public List<double> SepCost { get; set; } = new List<double>();

        public List<double> RowTotal { get; set; } = new List<double>();
        public List<double> FirstSlot { get; set; } = new List<double>();
        public List<double> SecondSlot { get; set; } = new List<double>();

        //public double OctPoint { get; set; }
        //public double NovPoint { get; set; }
        //public double DecPoint { get; set; }
        //public double JanPoint { get; set; }
        //public double FebPoint { get; set; }
        //public double MarPoint { get; set; }
        //public double AprPoint { get; set; }
        //public double MayPoint { get; set; }
        //public double JunPoint { get; set; }
        //public double JulPoint { get; set; }
        //public double AugPoint { get; set; }
        //public double SepPoint { get; set; }

    }
}