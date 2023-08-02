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

        //public List<double> OctRowTotal { get; set; } = new List<double>();
        //public List<double> NovRowTotal { get; set; } = new List<double>();
        //public List<double> DecRowTotal { get; set; } = new List<double>();
        //public List<double> JanRowTotal { get; set; } = new List<double>();
        //public List<double> FebRowTotal { get; set; } = new List<double>();
        //public List<double> MarRowTotal { get; set; } = new List<double>();
        //public List<double> AprRowTotal { get; set; } = new List<double>();
        //public List<double> MayRowTotal { get; set; } = new List<double>();
        //public List<double> JunRowTotal { get; set; } = new List<double>();
        //public List<double> JulRowTotal { get; set; } = new List<double>();
        //public List<double> AugRowTotal { get; set; } = new List<double>();
        //public List<double> SepRowTotal { get; set; } = new List<double>();
        public List<double> RowTotal { get; set; } = new List<double>();
    }
}