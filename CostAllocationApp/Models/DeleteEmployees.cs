using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.Models
{
    public class DeleteEmployees
    {
        public string BudgetYear { get; set; }
        public string FinalBudgetYear { get; set; }
        public string AssignmentYear { get; set; }
        public string ActualCostYear { get; set; }
        public string QAProportionYear { get; set; }
    }
}