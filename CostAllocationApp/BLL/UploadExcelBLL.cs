﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Dtos;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;

namespace CostAllocationApp.BLL
{
    public class UploadExcelBLL
    {
        SalaryDAL salaryDAL = null;
        SalaryBLL salaryBLL = null;
        public UploadExcelBLL()
        {
            salaryDAL = new SalaryDAL();
            salaryBLL = new SalaryBLL();
        }
        UploadExcelDAL _uploadExcelDAL = new UploadExcelDAL();
        public int? GetSectionIdByName(string sectionName)
        {
            return _uploadExcelDAL.GetSectionIdByName(sectionName);
        }
        public int? GetDepartmentIdByName(string departmentName)
        {
            return _uploadExcelDAL.GetDepartmentIdByName(departmentName);
        }
        public int? GetInchargeIdByName(string inchargeName)
        {
            return _uploadExcelDAL.GetInchargeIdByName(inchargeName);
        }
        public int? GetRoleIdByName(string roleName)
        {
            return _uploadExcelDAL.GetRoleIdByName(roleName);
        }
        public int? GetExplanationIdByName(string explanationName)
        {
            return _uploadExcelDAL.GetExplanationIdByName(explanationName);
        }
        public int? GetCompanyIdByName(string companyName)
        {
            return _uploadExcelDAL.GetCompanyIdByName(companyName);
        }
        public int? GetGradeIdByGradeName(string gradePoints)
        {
            return _uploadExcelDAL.GetGradeIdByGradeName(gradePoints);
        } 
        public double GetUnitPriceByGradeName(string gradePoints)
        {
            return _uploadExcelDAL.GetUnitPriceByGradeName(gradePoints);
        } 
        
        public int GetGradeIdByUnitPrice(string unitPrice)
        {
            Salary salary = null;
            decimal tempVal = 0;
            if (decimal.TryParse(unitPrice, out tempVal))
            {
                if (tempVal > 0)
                {
                    salary = salaryBLL.CompareSalary(tempVal);                    
                }
            }
            if(salary == null)
            {
                return 0;
            }
            else
            {
                return salary.Id;
            }            
        }

    }
}