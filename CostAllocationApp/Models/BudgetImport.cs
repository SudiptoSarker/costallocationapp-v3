using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class BudgetImport
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool FirstHalfBudget { get; set; }
        public bool SecondHalfBudget { get; set; }
        public bool FinalizedBudget { get; set; }
        public bool FirstHalfFinalize { get; set; }
        public bool SecondHalfFinalize { get; set; }
    }
}