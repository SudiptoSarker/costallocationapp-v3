using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Models;

namespace CostAllocationApp.Dtos
{
    public class ActualCostDto
    {
        public List<ActualCost> ActualCosts { get; set; }
        public int Year { get; set; }
    }
}