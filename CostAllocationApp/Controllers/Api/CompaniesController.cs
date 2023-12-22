using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers.Api
{
    public class CompaniesController :  ApiController
    {
        CompanyBLL companyBLL = null;
        public CompaniesController()
        {
            companyBLL = new CompanyBLL();
        }

        /*
         Description: create company.
         Type: POST
        */
        [HttpPost]
        public IHttpActionResult CreateCompany(Company company)
        {
            var session = System.Web.HttpContext.Current.Session;
            // checking null or empty
            if (String.IsNullOrEmpty(company.CompanyName))
            {
                return BadRequest("Company Name Required");
            }
            else
            {
                // checking existing company
                if (companyBLL.CheckComany(company.CompanyName))
                {
                    return BadRequest("既に登録済みです");
                }
                else
                {
                    int result = 0;
                    if (company.IsUpdate)
                    {
                        company.UpdatedBy = session["userName"].ToString();
                        company.UpdatedDate = DateTime.Now;
                        company.IsActive = true;
                        result = companyBLL.UpdateCompany(company);
                    }
                    else
                    {
                        company.CreatedBy = session["userName"].ToString();
                        company.CreatedDate = DateTime.Now;
                        company.IsActive = true;
                        result = companyBLL.CreateCompany(company);
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
         Description: get companies.
         Type: GET
        */

        [HttpGet]
        public IHttpActionResult Companies()
        {
            List<Company> companies = companyBLL.GetAllCompanies();
            return Ok(companies);
        }

        /*
         Description: remove companies.
         Type: DELETE
        */
        [HttpDelete]
        public IHttpActionResult RemoveCompanies([FromUri] string companyIds)
        {
            int result = 0;

            // check company ID null or empty
            if (!String.IsNullOrEmpty(companyIds))
            {
                string[] ids = companyIds.Split(',');

                foreach (var item in ids)
                {
                    // remove companies
                    result += companyBLL.RemoveCompanies(Convert.ToInt32(item));
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
                return BadRequest("Select InCharge Id!");
            }

        }
    }
}