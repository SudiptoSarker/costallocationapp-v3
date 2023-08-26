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
    public class SubCategoryController : ApiController
    {
        SubCategoryBLL subCategoryBLL = null;
        public SubCategoryController()
        {
            subCategoryBLL = new SubCategoryBLL();
        }

        /***************************\                           
            SubCategory Master Api: SubCategory created through this api.                               
        \***************************/
        [HttpPost]
        public IHttpActionResult CreateSubCategory(SubCategory subCategory)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(subCategory.SubCategoryName))
            {
                return BadRequest("SubCategory Name Required");
            }
            else if (String.IsNullOrEmpty(subCategory.CategoryId.ToString()))
            {
                return BadRequest("Please select Category");
            }
            else
            {
                subCategory.CreatedBy = session["userName"].ToString();
                subCategory.CreatedDate = DateTime.Now;
                subCategory.IsActive = true;

                if (subCategoryBLL.CheckSubCategory(subCategory.SubCategoryName))
                {
                    return BadRequest("SubCategory Already Exists!!!");
                }
                else
                {
                    int result = subCategoryBLL.CreateSubCategory(subCategory);
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
        //    SubCategory Master Api: All the SubCategory are fetched using this api.                            
        //\***************************/
        [HttpGet]
        public IHttpActionResult SubCategory()
        {
            List<SubCategory> categories = subCategoryBLL.GetAllSubCategories();
            return Ok(categories);
        }

        ///***************************\                           
        //    Category Master Api: Category are removed using this api.                          
        //\***************************/
        [HttpDelete]
        public IHttpActionResult RemoveSubCategory([FromUri] string subCategoryId)
        {
            int result = 0;


            if (!String.IsNullOrEmpty(subCategoryId))
            {
                string[] ids = subCategoryId.Split(',');

                foreach (var item in ids)
                {
                    result += subCategoryBLL.RemoveSubCategory(Convert.ToInt32(item));
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
                return BadRequest("Select SubCategory Id!");
            }

        }
    }
}
