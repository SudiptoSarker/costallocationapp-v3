using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;


namespace CostAllocationApp.Controllers.Api
{
    public class SalariesController :  ApiController
    {
        SalaryBLL salaryBLL = null;
        public SalariesController()
        {
            salaryBLL = new SalaryBLL();
        }
        // create grade
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
                salary.CreatedBy = session["userName"].ToString();
                salary.CreatedDate = DateTime.Now;
                salary.IsActive = true;

                // checking existing salary
                if (salaryBLL.CheckGrade(salary))
                {
                    return BadRequest("同一データが登録済みです!!!");
                }
                else
                {
                    // create salary
                    int result = salaryBLL.CreateSalary(salary);
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
        // get all the salaries
        [HttpGet]
        public IHttpActionResult Salaries()
        {
            List<Salary> salaries = salaryBLL.GetAllSalaryPoints();
            return Ok(salaries);
        }

        // remove salaries
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