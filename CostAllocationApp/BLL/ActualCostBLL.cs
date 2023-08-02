using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.BLL
{
    public class ActualCostBLL
    {
        ActualCostDAL actualCostDAL = null;
        EmployeeAssignmentDAL employeeAssignmentDAL = null;
        public ActualCostBLL()
        {
            actualCostDAL = new ActualCostDAL();
            employeeAssignmentDAL = new EmployeeAssignmentDAL();
        }
        public List<ActualCost> GetActualCostsByYear(int year)
        {
            return actualCostDAL.GetActualCostsByYear(year);
        }
        public bool CheckAssignmentId(int assignmentId, int year)
        {
            return actualCostDAL.CheckAssignmentId(assignmentId,year);
        }
        public int CreateActualCost(ActualCost actualCost)
        {
            return actualCostDAL.CreateActualCost(actualCost);
        }
        public int UpdateActualCost(int year, int assignmentId, string costColumnName, string pointColumnName, double costAmount, double pointAmount, string updatedBy, DateTime updatedDate)
        {
            return actualCostDAL.UpdateActualCost(year, assignmentId, costColumnName, pointColumnName, costAmount, pointAmount, updatedBy, updatedDate);
        }
        public bool CheckSukeyAssignmentId(int assignmentId, int year)
        {
            return actualCostDAL.CheckSukeyAssignmentId(assignmentId, year);
        }
        public int CreateSukey(Sukey sukey)
        {
            return actualCostDAL.CreateSukey(sukey);
        }
        public int UpdateSukey(Sukey sukey)
        {
            return actualCostDAL.UpdateSukey(sukey);
        }
        public List<SukeyDto> GetAllSukeyData(int year)
        {
            List<SukeyDto> sukeyListWithDepartment = new List<SukeyDto>();
            List<Sukey> sukeyList = new List<Sukey>();
            var sukeys =  actualCostDAL.GetAllSukeyData(year);
            var distinctDepartmentIds = new List<string>();
            foreach (var item in sukeys)
            {
                
                var employeeAssignment = employeeAssignmentDAL.GetDepartmentByAssignmentId(item.AssignmentId);
                if (!String.IsNullOrEmpty(employeeAssignment.DepartmentId))
                {
                    item.DepartmentId = employeeAssignment.DepartmentId;
                    item.DepartmentName = employeeAssignment.DepartmentName;
                    sukeyList.Add(item);
                    if (!distinctDepartmentIds.Contains(item.DepartmentId))
                    {
                        distinctDepartmentIds.Add(item.DepartmentId);
                    }
                }
            }

            foreach (var item in distinctDepartmentIds)
            {
                sukeyListWithDepartment.Add(new SukeyDto
                {
                    DepartmentId = item,
                    DepartmentName = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().DepartmentName,
                    OctCost = sukeyList.Where(s=>s.DepartmentId==item).Sum(s=>s.OctCost),
                    NovCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.NovCost),
                    DecCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.DecCost),
                    JanCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.JanCost),
                    FebCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.FebCost),
                    MarCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.MarCost),
                    AprCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.AprCost),
                    MayCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.MayCost),
                    JunCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.JunCost),
                    JulCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.JulCost),
                    AugCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.AugCost),
                    SepCost = sukeyList.Where(s => s.DepartmentId == item).Sum(s => s.SepCost),
                });
            }

            return sukeyListWithDepartment;
        }

        public List<Apportionment> GetAllApportionmentData(int year)
        {
            return actualCostDAL.GetAllApportionmentData(year);
        }
        public int CreateApportionment(Apportionment apportionment)
        {
            return actualCostDAL.CreateApportionment(apportionment);
        }
        public int UpdateApportionment(Apportionment apportionment)
        {
            return actualCostDAL.UpdateApportionment(apportionment);
        }
        public bool CheckApportionment(int departmentId, int year)
        {
            return actualCostDAL.CheckApportionment(departmentId, year);
        }
        public List<ActualCost> GetActualCostsByYear_AssignmentId(int year, int assignmentId)
        {
            return actualCostDAL.GetActualCostsByYear_AssignmentId(year, assignmentId);
        }
        public int GetLeatestForcastYear()
        {
            return actualCostDAL.GetLeatestForcastYear();
        }
        public int GetLeatestActualCostYear()
        {
            return actualCostDAL.GetLeatestActualCostYear();
        }
    }
}