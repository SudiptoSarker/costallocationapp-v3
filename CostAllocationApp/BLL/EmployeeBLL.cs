using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class EmployeeBLL
    {
        EmployeeDAL employeeDAL = null;
        public EmployeeBLL()
        {
            employeeDAL = new EmployeeDAL();
        }
        public int CreateEmployee(Employee employee)
        {
            return employeeDAL.CreateEmployee(employee);
        }
        public int CheckForEmployeeName(string employeeName)
        {
            return employeeDAL.CheckForEmployeeName(employeeName);
        }
        public List<Employee> GetAllEmployees(string orderby = "")
        {
            return employeeDAL.GetAllEmployees(orderby);
        }
        public List<Employee> GetEmployeeListForBudgetEdit(int year,int budgetType)
        {
            return employeeDAL.GetEmployeeListForBudgetEdit(year, budgetType);
        }
        public List<Employee> GetEmployeeListEmployeeAssignments(int assignmentYear)
        {
            return employeeDAL.GetEmployeeListEmployeeAssignments(assignmentYear);
        }

        public int UpdateEmployee(Employee employee)
        {
            return employeeDAL.UpdateEmployee(employee);
        }
        public int InactiveEmployee(Employee employee)
        {
            return employeeDAL.InactiveEmployee(employee);
        }
        public bool CheckEmployeeDuplication(string employeeName)
        {
            return employeeDAL.CheckEmployeeDuplication(employeeName);
        }
        public bool CheckUserNameDuplication(string userName)
        {
            return employeeDAL.CheckUserNameDuplication(userName);
        }
        public bool CheckUserEmailDuplication(string userEmail)
        {
            return employeeDAL.CheckUserEmailDuplication(userEmail);
        }
        public List<Employee> EmployeeListByNameFilter(string employeeName)
        {
            return employeeDAL.EmployeeListByNameFilter(employeeName);
        }
        public string GetEmployeeNameByAssignmentId(int assignmentId)
        {
            return employeeDAL.GetEmployeeNameByAssignmentId(assignmentId);
        }
        public string  GetFullNameFromCSV(string csvCellValue)
        {
            string employeeName = "";

            if (!string.IsNullOrEmpty(csvCellValue))
            {
                var arrEmployeeeName = csvCellValue.Split('.');
                if (!string.IsNullOrEmpty(arrEmployeeeName[0]))
                {
                    employeeName = arrEmployeeeName[0].ToString();
                }
            }            
            return employeeName;
        }
        public int RemoveEmployees(int employeeId)
        {
            //int budgetLatestYear = 0;
            //int finalBudgetLatestYear = 0;
            //int yearlyDataLatestYear = 0;
            //int actualCostLatestYear = 0;
            //int qaProrationLatestYear = 0;

            //DeleteEmployees deleteEmployees = GetMaxYears();
            //string assignmentMaxYear = deleteEmployees.AssignmentYear;
            //if (!string.IsNullOrEmpty(assignmentMaxYear))
            //{
            //    bool isBudgetEmployeeDelete = false;
            //    bool isFinalBudgetEmployeeDelete = false;
            //    bool isActualCostEmployeeDelete = false;
            //    bool isQAProrationEmployeeDelete = false;

            //    if (!string.IsNullOrEmpty(deleteEmployees.BudgetYear))
            //    {
            //        if(deleteEmployees.AssignmentYear == deleteEmployees.BudgetYear)
            //        {
            //            isBudgetEmployeeDelete = true;
            //        }                   
            //    }
            //    if (!string.IsNullOrEmpty(deleteEmployees.FinalBudgetYear))
            //    {
                    
            //        if (deleteEmployees.AssignmentYear == deleteEmployees.FinalBudgetYear)
            //        {
            //            isFinalBudgetEmployeeDelete = true;
            //        }                    
            //    }
            //    if (!string.IsNullOrEmpty(deleteEmployees.ActualCostYear))
            //    {
            //        if (deleteEmployees.AssignmentYear == deleteEmployees.ActualCostYear)
            //        {
            //            isActualCostEmployeeDelete = true;
            //        }                    
            //    }
            //    if (!string.IsNullOrEmpty(deleteEmployees.QAProportionYear))
            //    {
            //        if (deleteEmployees.AssignmentYear == deleteEmployees.QAProportionYear)
            //        {
            //            isQAProrationEmployeeDelete = true;
            //        }
            //    }

            //    //delete employee from budget 
            //    if (isBudgetEmployeeDelete)
            //    {
            //        List<EmployeeBudget> employeeBudgets = new List<EmployeeBudget>();
            //        employeeBudgets = employeeDAL.GetBudgetIdsByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        int budgetDelete = employeeDAL.RemoveBudgetByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        if (employeeBudgets.Count > 0 && budgetDelete>0)
            //        {
            //            foreach(var budgetItem in employeeBudgets)
            //            {
            //                int budgetCostDelete = employeeDAL.RemoveBudgetCostsByBudgetId(budgetItem.Id);
            //            }
            //        }
            //    }

            //    //delete employee from final budget 
            //    if (isFinalBudgetEmployeeDelete)
            //    {
            //        List<EmployeeBudget> employeeBudgets = new List<EmployeeBudget>();
            //        employeeBudgets = employeeDAL.GetFinalBudgetIdsByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        int finalBudgetDelete = employeeDAL.RemoveFinalBudgetByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        if (employeeBudgets.Count > 0 && finalBudgetDelete > 0)
            //        {
            //            foreach (var finalBudgetItem in employeeBudgets)
            //            {
            //                int finalBudgetCostDelete = employeeDAL.RemoveFinalBudgetCostsByBudgetId(finalBudgetItem.Id);
            //            }
            //        }
            //    }

            //    //delete employee from assignment                                 
            //    if (Convert.ToInt32(assignmentMaxYear) >0)
            //    {
            //        List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();
            //        employeeAssignments = employeeDAL.GetEmployeeAssignmentIdsByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        int assignmentDelete = employeeDAL.RemoveEmployeeAssignmentsByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //        if (employeeAssignments.Count > 0 && assignmentDelete > 0)
            //        {
            //            foreach (var assignmentItem in employeeAssignments)
            //            {
            //                int assignmentCostDelete = employeeDAL.RemoveEmployeeAssignmentsCostsByAssignmentId(assignmentItem.Id);
            //                //delete employee from actual cost 
            //                if (isActualCostEmployeeDelete)
            //                {
            //                    int actualCostDelete = employeeDAL.RemoveActualCostByYearAndAssingmentId(assignmentMaxYear,assignmentItem.Id);
            //                }
            //            }
            //        }
            //    }
            //    //delete employee from proration
            //    if (isQAProrationEmployeeDelete)
            //    {
            //        int prorationDelete = employeeDAL.RemoveQAProrationByYearAndEmployeeId(assignmentMaxYear, employeeId);
            //    }
            //}
            return employeeDAL.RemoveEmployees(employeeId);
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return employeeDAL.GetEmployeeById(employeeId);
        }
    }
}