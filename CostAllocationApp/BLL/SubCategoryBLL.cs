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
        public int RemoveSubCategory(int subCategoryId)
        {
            return subCategoryDAL.RemoveSubCategory(subCategoryId);
        }

        public bool CheckSubCategory(string subCategoryName)
        {
            return subCategoryDAL.CheckSubCategory(subCategoryName);
        }
        public Category GetCategoryByCategoryId(int categoryId)
        {
            return subCategoryDAL.GetCategoryByCategoryId(categoryId);
        }
    }
}