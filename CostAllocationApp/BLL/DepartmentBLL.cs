using System;
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
        public int UpdateDepartment(Department department)
        {
            return departmentDAL.UpdateDepartment(department);
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

        public List<Category> GetAllCategories()
        {
            return departmentDAL.GetAllCategories();
        }
        public List<SubCategory> GetAllSubCategories()
        {
            return departmentDAL.GetAllSubCategories();
        }

        public int RetrieveDepartmentIdByDepartmentName(string departmentName,int sectionId,string userName)
        {
            Department department = new Department();

            int depatmentId = 0;

            if (!string.IsNullOrEmpty(departmentName))
            {
                depatmentId = departmentDAL.GetDepartmentIdByDepartmentName(departmentName);

                if (depatmentId > 0)
                {
                    return depatmentId;
                }
                else
                {
                    department.CreatedBy = userName;
                    department.CreatedDate = DateTime.Now;
                    department.IsActive = true;
                    department.DepartmentName = departmentName;
                    department.SectionId = sectionId;

                    int result = departmentDAL.CreateDepartment(department);
                    depatmentId = departmentDAL.GetDepartmentIdByName(departmentName);
                    return depatmentId;
                }
            }
            else
            {
                return depatmentId;
            }  
        }
        public List<Department> GetDepartmentsById(string departmentIds)
        {
            return departmentDAL.GetDepartmentsById(departmentIds);
        }
    }
}