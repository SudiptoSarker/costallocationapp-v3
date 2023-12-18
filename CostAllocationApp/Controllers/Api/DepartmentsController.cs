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
    public class DepartmentsController : ApiController
    {
        DepartmentBLL departmentBLL = null;
        public DepartmentsController()
        {
            departmentBLL = new DepartmentBLL();
        }

        /*
        Get Department List
        Request: Get
        Response: List of Departments
        */
        [HttpGet]
        public IHttpActionResult Departments()
        {
            // Get Department List
            List<Department> departments = departmentBLL.GetAllDepartments();
            return Ok(departments);
        }

        /*
        Create/Update Departments
        Request: Post
        Response: string
        */
        [HttpPost]
        public IHttpActionResult CreateDepartment(Department department)
        {
            var session = System.Web.HttpContext.Current.Session;

            // Department Name Validation: The Department must have a value
            if (String.IsNullOrEmpty(department.DepartmentName))
            {
                return BadRequest("部署名は必須です");
            }
            else if (String.IsNullOrEmpty(department.SectionId.ToString()))
            {
                return BadRequest("セクションを選択してください");
            }
            else
            {
                // Department Name must be unique
                if (departmentBLL.CheckDepartment(department))
                {
                    return BadRequest("部門は登録済みです!!!");
                }
                else {
                    int result = 0;
                    if (department.IsUpdate)
                    {
                        department.UpdatedBy = session["userName"].ToString();
                        department.UpdatedDate = DateTime.Now;
                        department.IsActive = true;

                        // Update Department
                        result = departmentBLL.UpdateDepartment(department);
                    }
                    else
                    {
                        department.CreatedBy = session["userName"].ToString();
                        department.CreatedDate = DateTime.Now;
                        department.IsActive = true;

                        // Create New Department
                        result = departmentBLL.CreateDepartment(department);
                    }

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
        Delete Departments
        Request: HttpDelete
        Response: string
        */
        [HttpDelete]
        public IHttpActionResult RemoveDepartment([FromUri] string departmentIds)
        {
            int result = 0;

            // Department Validation: The Department must have a value
            if (!String.IsNullOrEmpty(departmentIds))
            {
                string[] ids = departmentIds.Split(',');

                // Department Deletion 
                foreach (var item in ids)
                {
                    result += departmentBLL.RemoveDepartment(Convert.ToInt32(item));
                }

                if (result == ids.Length)
                {
                    return Ok("正常に削除がされました!");
                }
                else
                {
                    return BadRequest("何か問題が発生しました");
                }
            }
            else
            {
                return BadRequest("部署を選択 Id!");
            }

        }

    }
}
