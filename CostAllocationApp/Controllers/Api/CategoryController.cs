using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;
namespace CostAllocationApp.Controllers.Api
{
    public class CategoryController : ApiController
    {
        CategoryBLL categoryBLL = null;
        public CategoryController()
        {
            categoryBLL = new CategoryBLL();
        }

        /***************************\                           
            Category Master Api: Category created through this api.                               
        \***************************/
        [HttpPost]
        public IHttpActionResult CreateCategory(Category category)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Category Name Required");
            }
            else
            {
                category.CreatedBy = session["userName"].ToString();
                category.CreatedDate = DateTime.Now;
                category.IsActive = true;

                if (categoryBLL.CheckCategory(category.CategoryName))
                {
                    return BadRequest("Category Already Exists!!!");
                }
                else
                {
                    int result = categoryBLL.CreateCategory(category);
                    if (result > 0)
                    {
                        return Ok("データが保存されました!");
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
            }            
        }

        ///***************************\                           
        //    Category Master Api: All the Category are fetched using this api.                            
        //\***************************/
        [HttpGet]
        public IHttpActionResult Category()
        {
            List<Category> categories = categoryBLL.GetAllCategories();
            return Ok(categories);
        }

        ///***************************\                           
        //    Category Master Api: Category are removed using this api.                          
        //\***************************/
        [HttpDelete]
        public IHttpActionResult RemoveCategory([FromUri] string CategoryId)
        {
            int result = 0;
            var session = System.Web.HttpContext.Current.Session;

            if (!String.IsNullOrEmpty(CategoryId))
            {
                string[] ids = CategoryId.Split(',');

                foreach (var item in ids)
                {
                    Category category = categoryBLL.GetAllCategories().Where(c => c.Id == Convert.ToInt32(item)).SingleOrDefault();
                    category.UpdatedBy = session["userName"].ToString();
                    category.UpdatedDate = DateTime.Now;
                    result += categoryBLL.RemoveCategory(category);
                }

                if (result == ids.Length)
                {
                    return Ok("正常に削除がされました!");
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Select Category Id!");
            }

        }
    }
}
