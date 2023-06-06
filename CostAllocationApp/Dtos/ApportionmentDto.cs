using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Models;

namespace CostAllocationApp.Dtos
{
    public class ApportionmentDto
    {
        public List<Apportionment> Apportionments { get; set; }
        public int Year { get; set; }
    }
}