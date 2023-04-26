using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class ActualCostBLL
    {
        ActualCostDAL actualCostDAL = null;
        public ActualCostBLL()
        {
            actualCostDAL = new ActualCostDAL();
        }
        public List<ActualCost> GetActualCostsByYear(int year)
        {
            return actualCostDAL.GetActualCostsByYear(year);
        }
        public bool CheckAssignmentId(int assignmentId, int year)
        {
            return actualCostDAL.CheckAssignmentId(assignmentId,year);
        }
    }
}