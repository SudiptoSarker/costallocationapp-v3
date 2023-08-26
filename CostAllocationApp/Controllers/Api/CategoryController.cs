﻿using System;
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
                        return Ok("Data Saved Successfully!");
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


            if (!String.IsNullOrEmpty(CategoryId))
            {
                string[] ids = CategoryId.Split(',');

                foreach (var item in ids)
                {
                    result += categoryBLL.RemoveCategory(Convert.ToInt32(item));
                }

                if (result == ids.Length)
                {
                    return Ok("Data Removed Successfully!");
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