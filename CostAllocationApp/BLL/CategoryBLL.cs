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
        public CategoryBLL()
        {
            categoryDAL = new CategoryDAL();
        }

        public int CreateCategory(Category category)
        {
            return categoryDAL.CreateCategory(category);
        }

        public List<Category> GetAllCategories()
        {
            return categoryDAL.GetAllCategories();
        }
        public int RemoveCategory(Category category)
        {
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
    }
}