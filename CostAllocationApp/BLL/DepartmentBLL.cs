﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class DepartmentBLL
    {
        DepartmentDAL departmentDAL = null;
        public DepartmentBLL()
        {
            departmentDAL = new DepartmentDAL();
        }
        public int CreateDepartment(Department department)
        {
            return departmentDAL.CreateDepartment(department);
        }
        public List<Department> GetAllDepartments()
        {
            return departmentDAL.GetAllDepartments();
        }
        public bool CheckForBudgetInitialDataExists(int budgetYear)
        {
            return departmentDAL.CheckForBudgetInitialDataExists(budgetYear);
        }
        public bool CheckForBudgetSecondHalfDataExists(int budgetYear)
        {
            return departmentDAL.CheckForBudgetSecondHalfDataExists(budgetYear);
        }
        public bool CheckForBudgetInitialDataFinalizeExists(int budgetYear)
        {
            return departmentDAL.CheckForBudgetInitialDataFinalizeExists(budgetYear);
        }
        public bool CheckForBudgetSecondHalfDataFinalizeExists(int budgetYear)
        {
            return departmentDAL.CheckForBudgetSecondHalfDataFinalizeExists(budgetYear);
        }
        public int RemoveDepartment(int departmentIds)
        {
            return departmentDAL.RemoveDepartment(departmentIds);
        }
        public List<Department> GetAllDepartmentsBySectionId(int sectionId)
        {
            return departmentDAL.GetAllDepartmentsBySectionId(sectionId);
        }
        public bool CheckDepartment(Department department)
        {
            return departmentDAL.CheckDepartment(department);
        }
        public int GetDepartmentCountWithEmployeeAsignment(int departmentId)
        {
            return departmentDAL.GetDepartmentCountWithEmployeeAsignment(departmentId);
        }
        public Department GetDepartmentByDepartemntId(int departmentId)
        {
            return departmentDAL.GetDepartmentByDepartemntId(departmentId);
        }
    }
}