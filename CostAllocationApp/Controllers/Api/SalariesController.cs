using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;
using System.Linq;

namespace CostAllocationApp.Controllers.Api
{
    public class SalariesController :  ApiController
    {
        SalaryBLL salaryBLL = null;
        public SalariesController()
        {
            salaryBLL = new SalaryBLL();
        }
        /*
         Description: create salary.
         Type: POST
        */
        [HttpPost]
        public IHttpActionResult CreateSalary(Salary salary)
        {
            var session = System.Web.HttpContext.Current.Session;

            // checking salary null or empty
            if (String.IsNullOrEmpty(salary.SalaryGrade))
            {
                return BadRequest("Grade Required");
            }
            else
            {
                int result = 0;

                if (salary.IsUpdate)
                {
                    salary.UpdatedBy = session["userName"].ToString();
                    salary.UpdatedDate = DateTime.Now;

                    result = salaryBLL.UpdateSalary(salary);
                }
                else
                {
                    if (salaryBLL.CheckGrade(salary))
                    {
                        return BadRequest("同一データが登録済みです!!!");
                    }
                    else
                    {
                        salary.CreatedBy = session["userName"].ToString();
                        salary.CreatedDate = DateTime.Now;
                        salary.IsActive = true;

                        result = salaryBLL.CreateSalary(salary);
                    }                    
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
        /*
         Description: get all salaries.
         Type: GET
        */
        [HttpGet]
        public IHttpActionResult Salaries()
        {
            List<Salary> salaries = salaryBLL.GetAllSalaryPoints();
            return Ok(salaries);
        }

        /*
        Description: get single salary.
        Type: GET
       */
        [HttpGet]
        public IHttpActionResult Salary(int salaryGradeId)
        {
            Salary salary = null;
            List<Salary> salaries = salaryBLL.GetAllSalaryPoints();
            if (salaries.Count > 0)
            {
                salary = salaries.Where(s => s.Id == salaryGradeId).SingleOrDefault();
                if (salary==null)
                {
                    salary = new Salary();
                }
            }
            return Ok(salary);
        }

        /*
        Description: remove salaries.
        Type: DELETE
       */
        [HttpDelete]
        public IHttpActionResult RemoveSalary([FromUri] string salaryIds)
        {
            int result = 0;

            // checking salaries ID null or empty
            if (!String.IsNullOrEmpty(salaryIds))
            {
                string[] ids = salaryIds.Split(',');

                foreach (var item in ids)
                {
                    // remove salary
                    result += salaryBLL.RemoveSalary(Convert.ToInt32(item));
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
                return BadRequest("Select Grade Points!");
            }

        }


    }
}