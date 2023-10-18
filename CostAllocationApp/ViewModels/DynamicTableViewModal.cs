using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.ViewModels
{
    public class DynamicTableViewModal
    {
        public double OctTotalCost { get; set; }
        public double NovTotalCost { get; set; }
        public double DecTotalCost { get; set; }
        public double JanTotalCost { get; set; }
        public double FebTotalCost { get; set; }
        public double MarTotalCost { get; set; }
        public double AprTotalCost { get; set; }
        public double MayTotalCost { get; set; }
        public double JunTotalCost { get; set; }
        public double JulTotalCost { get; set; }
        public double AugTotalCost { get; set; }
        public double SepTotalCost { get; set; }

        public double FirstHalfTotalCost { get; set; }
        public double SecondHalfTotalCost { get; set; }
        public double YearTotalCost { get; set; }
    }
}