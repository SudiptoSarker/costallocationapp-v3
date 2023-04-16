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
        public List<Employee> GetAllEmployees()
        {
            return employeeDAL.GetAllEmployees();
        }
        //public int RemoveEmployee(int employeeIds)
        //{
        //    return employeeDAL.RemoveEmployee(employeeIds);
        //}

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
        public List<Employee> EmployeeListByNameFilter(string employeeName)
        {
            return employeeDAL.EmployeeListByNameFilter(employeeName);
        }
        public string GetEmployeeNameByAssignmentId(int assignmentId)
        {
            return employeeDAL.GetEmployeeNameByAssignmentId(assignmentId);
        }
    }
}