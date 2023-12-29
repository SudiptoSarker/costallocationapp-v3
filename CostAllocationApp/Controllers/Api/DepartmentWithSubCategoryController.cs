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

        /*
        Description: create department with sub-category.
        Type: POST
        */
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
                        return Ok("データが保存されました!");
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
            }
        }

        /*
        Description: get all  department with sub-categories.
        Type: GET
        */
        [HttpGet]
        public IHttpActionResult DepartmentWithSubCategory()
        {
            List<DepartmentWithSubCategory> departmentWithSubCategories = departmentWithSubCategoryBLL.GetDepartmentCategoryAndSubCategory();
            return Ok(departmentWithSubCategories);
        }

    }
}
