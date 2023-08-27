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
    public class DepartmentWithSubCategoryController : ApiController
    {
        DepartmentWithSubCategoryBLL departmentWithSubCategoryBLL = null;
        public DepartmentWithSubCategoryController()
        {
            departmentWithSubCategoryBLL = new DepartmentWithSubCategoryBLL();
        }

        /***************************\                           
            DepartmentWithSubCategory Master Api: DepartmentWithSubCategory created through this api.                               
        \***************************/
        [HttpPost]
        public IHttpActionResult CreateDepartmentWithSubCategory(DepartmentWithSubCategory departmentWithSubCategory)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(departmentWithSubCategory.SubCategoryId))
            {
                return BadRequest("SubCategory Id Required");
            }
            else if (String.IsNullOrEmpty(departmentWithSubCategory.DepartmentId.ToString()))
            {
                return BadRequest("Department Id Required");
            }
            else
            {
                departmentWithSubCategory.UpdatedBy = session["userName"].ToString();
                departmentWithSubCategory.UpdatedDate = DateTime.Now;
                departmentWithSubCategory.IsActive = true;

                if (departmentWithSubCategoryBLL.CheckSubCategoryIsAssignedToDepartment(departmentWithSubCategory.DepartmentId))
                {
                    return BadRequest("Department Already Assigned to a Category!!!");
                }
                else
                {
                    int result = departmentWithSubCategoryBLL.CreateDepartmentWithSubCategory(departmentWithSubCategory);
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
        public IHttpActionResult DepartmentWithSubCategory()
        {
            List<DepartmentWithSubCategory> departmentWithSubCategories = departmentWithSubCategoryBLL.GetDepartmentCategoryAndSubCategory();
            return Ok(departmentWithSubCategories);
        }

        ///***************************\                           
        //    Category Master Api: Category are removed using this api.                          
        //\***************************/
        //[HttpDelete]
        //public IHttpActionResult RemoveSubCategory([FromUri] string subCategoryId)
        //{
        //    int result = 0;


        //    if (!String.IsNullOrEmpty(subCategoryId))
        //    {
        //        string[] ids = subCategoryId.Split(',');

        //        foreach (var item in ids)
        //        {
        //            result += subCategoryBLL.RemoveSubCategory(Convert.ToInt32(item));
        //        }

        //        if (result == ids.Length)
        //        {
        //            return Ok("Data Removed Successfully!");
        //        }
        //        else
        //        {
        //            return BadRequest("Something Went Wrong!!!");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Select SubCategory Id!");
        //    }

        //}
    }
}
