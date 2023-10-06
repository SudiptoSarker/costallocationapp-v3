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
        DetailsItemBLL detailsItemBLL = null;
        public SubCategoryBLL()
        {
            subCategoryDAL = new SubCategoryDAL();
            detailsItemBLL = new DetailsItemBLL();
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
            List<DeatailsItem> detailsItems = detailsItemBLL.GetDetailsItemBySubItemsId(subCategory.Id);
            if (detailsItems.Count > 0)
            {
                foreach (var detailsItem in detailsItems)
                {
                    detailsItemBLL.RemoveDetailsItem(detailsItem);
                }

            }
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
        public int UpdateSubCategory(SubCategory subCategory)
        {
            return subCategoryDAL.UpdateSubCategory(subCategory);
        }
    }
}