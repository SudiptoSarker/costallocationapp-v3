using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CostAllocationApp.BLL
{
    public class CompanyBLL
    {
        CompanyDAL companyDAL = null;
        public CompanyBLL()
        {
            companyDAL = new CompanyDAL();
        }
        public int CreateCompany(Company company)
        {
            return companyDAL.CreateCompany(company);
        }

        public int UpdateCompany(Company company)
        {
            return companyDAL.UpdateCompany(company);
        }

        public List<Company> GetAllCompanies()
        {
            return companyDAL.GetAllCompanies();
        }

        public int RemoveCompanies(int companyIds)
        {
            return companyDAL.RemoveCompanies(companyIds);
        }

        public bool CheckCompany(string companyName)
        {
            return companyDAL.CheckCompany(companyName);
        }

        public int GetCompanyCountWithEmployeeAsignment(int companyId)
        {
            return companyDAL.GetCompanyCountWithEmployeeAsignment(companyId);
        }

        public Company GetCompanyByCompanyId(int companyId)
        {
            return companyDAL.GetCompanyByCompanyId(companyId);
        }

        public int RetrieveCompanyIdByCompanyName(string companyName, string userName)
        {
            Company company = new Company();
            int companyId = 0;

            if (!string.IsNullOrEmpty(companyName))
            {
                companyId = companyDAL.GetCompanyIdByName(companyName);

                if (companyId > 0)
                {
                    return companyId;
                }
                else
                {
                    company.CreatedBy = userName;
                    company.CreatedDate = DateTime.Now;
                    company.IsActive = true;
                    company.CompanyName = companyName;

                    int result = companyDAL.CreateCompany(company);
                    companyId = companyDAL.GetCompanyIdByName(companyName);
                    return companyId;
                }
            }
            else
            {
                return companyId;
            }

        }
    }
}