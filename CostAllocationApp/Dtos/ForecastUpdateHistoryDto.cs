using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Dtos
{
    public class ForecastUpdateHistoryDto
    {
        public int AssignmentId { get; set; }
        public decimal UnitPrice { get; set; }
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
        public int Year { get; set; }

    }
}