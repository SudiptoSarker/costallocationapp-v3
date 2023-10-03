using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class SubCategoryBLL
    {
        SubCategoryDAL subCategoryDAL = null;
        public SubCategoryBLL()
        {
            subCategoryDAL = new SubCategoryDAL();
        }

        public int CreateSubCategory(SubCategory category)
        {
            return subCategoryDAL.CreateSubCategory(category);
        }

        public List<SubCategory> GetAllSubCategories()
        {
            return subCategoryDAL.GetAllSubCategories();
        }
        public int RemoveSubCategory(SubCategory subCategory)
        {
            return subCategoryDAL.RemoveSubCategory(subCategory);
        }
        public SubCategory GetSubCategoryById(int subCategoryId)
        {
            return subCategoryDAL.GetSubCategoryById(subCategoryId);
        }

        public bool CheckSubCategory(string subCategoryName)
        {
            return subCategoryDAL.CheckSubCategory(subCategoryName);
        }
        public List<SubCategory> GetSubCategoryByCategoryId(int categoryId)
        {
            return subCategoryDAL.GetSubCategoryByCategoryId(categoryId);
        }
    }
}