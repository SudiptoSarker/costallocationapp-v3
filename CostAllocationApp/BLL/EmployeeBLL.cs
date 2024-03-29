﻿using System;
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
            return employeeDAL.RemoveEmployees(employeeId);
        }
        public Employee GetEmployeeById(int employeeId)
        {
            return employeeDAL.GetEmployeeById(employeeId);
        }
    }
}