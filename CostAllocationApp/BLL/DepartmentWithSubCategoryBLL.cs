using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class DepartmentWithSubCategoryBLL
    {
        DepartmentWithSubCategoryDAL departmentWithSubCategoryDAL = null;
        public DepartmentWithSubCategoryBLL()
        {
            departmentWithSubCategoryDAL = new DepartmentWithSubCategoryDAL();
        }

        public int CreateDepartmentWithSubCategory(DepartmentWithSubCategory departmentWithSubCategory)
        {
            return departmentWithSubCategoryDAL.CreateDepartmentWithSubCategory(departmentWithSubCategory);
        }

        public bool CheckSubCategoryIsAssignedToDepartment(string departmentId)
        {
            return departmentWithSubCategoryDAL.CheckSubCategoryIsAssignedToDepartment(departmentId);
        }

        public List<SubCategory> GetSubCategoryByCategoryId(int categoryId)
        {
            return departmentWithSubCategoryDAL.GetSubCategoryByCategoryId(categoryId);
        }
        public List<Department> GetAllUnassignedDepartments()
        {
            return departmentWithSubCategoryDAL.GetAllUnassignedDepartments();
        }
        public List<DepartmentWithSubCategory> GetDepartmentCategoryAndSubCategory()
        {
            return departmentWithSubCategoryDAL.GetDepartmentCategoryAndSubCategory();
        }
    }
}