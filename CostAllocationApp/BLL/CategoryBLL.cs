using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;


namespace CostAllocationApp.BLL
{
    public class CategoryBLL
    {
        CategoryDAL categoryDAL = null;
        SubCategoryBLL subCategoryBLL = null;
        DetailsItemBLL detailsItemBLL = null;
        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();
            subCategoryBLL = new SubCategoryBLL();
            detailsItemBLL = new DetailsItemBLL();
        }

        public int CreateCategory(Category category)
        {
            return categoryDAL.CreateCategory(category);
        }

        public List<Category> GetAllCategoriesByDynamicTableId(int dynamicTableId)
        {
            List<Category> categories = new List<Category>();
            categories = categoryDAL.GetAllCategoriesByDynamicTableId(dynamicTableId);
            return categories;
        }
        public int RemoveCategory(Category category)
        {

            List<SubCategory> subCategories = subCategoryBLL.GetSubCategoryByCategoryId(category.Id);
            List<DeatailsItem> detailsItems = new List<DeatailsItem>();

            if (subCategories.Count > 0)
            {
                foreach (var subCategory in subCategories)
                {
                    var deatailsItemsList = detailsItemBLL.GetDetailsItemBySubItemsId(subCategory.Id);
                    detailsItems.AddRange(deatailsItemsList);
                }

                foreach (var subCategory in subCategories)
                {
                    subCategoryBLL.RemoveSubCategory(subCategory);
                }
            }

            if (detailsItems.Count > 0)
            {
                foreach (var detailsItem in detailsItems)
                {
                    detailsItemBLL.RemoveDetailsItem(detailsItem);
                }

            }

            return categoryDAL.RemoveCategory(category);
        }

        public bool CheckCategory(string categoryName)
        {
            return categoryDAL.CheckCategory(categoryName);
        }
        public Category GetCategoryByCategoryId(int categoryId)
        {
            return categoryDAL.GetCategoryByCategoryId(categoryId);
        }
        public int UpdateCategory(Category category)
        {
            return categoryDAL.UpdateCategory(category);
        }
    }
}