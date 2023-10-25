using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Dtos;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace CostAllocationApp.Controllers.Api
{
    public class UtilitiesController : ApiController
    {
        DepartmentBLL departmentBLL = null;
        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        SalaryBLL salaryBLL = null;
        SectionBLL sectionBLL = null;
        CompanyBLL companyBLL = new CompanyBLL();
        InChargeBLL inChargeBLL = null;
        RoleBLL roleBLL = null;
        ExplanationsBLL explanationsBLL = null;
        ForecastBLL forecastBLL = null;
        EmployeeBLL employeeBLL = null;
        UserBLL userBLL = null;
        ActualCostBLL actualCostBLL = null;
        UserRoleBLL userRoleBLL = null;
        QaProportionBLL qaProportionBLL = null;
        TotalBLL totalBLL = null;
        CategoryBLL categoryBLL = null;
        SubCategoryBLL subCategoryBLL = null;
        DetailsItemBLL detailsItemBLL = null;

        public UtilitiesController()
        {
            departmentBLL = new DepartmentBLL();
            employeeAssignmentBLL = new EmployeeAssignmentBLL();
            salaryBLL = new SalaryBLL();
            sectionBLL = new SectionBLL();
            companyBLL = new CompanyBLL();
            inChargeBLL = new InChargeBLL();
            roleBLL = new RoleBLL();
            explanationsBLL = new ExplanationsBLL();
            forecastBLL = new ForecastBLL();
            employeeBLL = new EmployeeBLL();
            userBLL = new UserBLL();
            actualCostBLL = new ActualCostBLL();
            userRoleBLL = new UserRoleBLL();
            qaProportionBLL = new QaProportionBLL();
            totalBLL = new TotalBLL();
            categoryBLL = new CategoryBLL();
            subCategoryBLL = new SubCategoryBLL();
            detailsItemBLL = new DetailsItemBLL();
        }


        [Route("api/utilities/DepartmentsBySection/{id}")]
        [HttpGet]
        [ActionName("DepartmentsBySection")]
        public IHttpActionResult DepartmentsBySectionId(string id)
        {
            int tempValue = 0;
            if (int.TryParse(id, out tempValue))
            {
                if (tempValue > 0)
                {
                    List<Department> departments = departmentBLL.GetAllDepartmentsBySectionId(sectionId: tempValue);
                    return Ok(departments);
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Something Went Wrong!!!");
            }
        }

        [Route("api/utilities/AssignmentById/{id}")]
        [HttpGet]
        public IHttpActionResult AssignmentById(string id)
        {
            int tempValue = 0;
            if (int.TryParse(id, out tempValue))
            {
                if (tempValue > 0)
                {
                    EmployeeAssignmentViewModel employeeAssignmentViewModel = employeeAssignmentBLL.GetAssignmentById(assignmentId: tempValue);
                    return Ok(employeeAssignmentViewModel);
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Something Went Wrong!!!");
            }
        }

        [HttpGet]
        public IHttpActionResult SearchEmployee(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status)
        {
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();
            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }
            if (!string.IsNullOrEmpty(sectionId))
            {
                employeeAssignment.SectionId = Convert.ToInt32(sectionId);
            }
            else
            {
                employeeAssignment.SectionId = 0;
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                employeeAssignment.DepartmentId = Convert.ToInt32(departmentId);
            }
            else
            {
                employeeAssignment.DepartmentId = 0;
            }
            if (!string.IsNullOrEmpty(inchargeId))
            {
                employeeAssignment.InchargeId = Convert.ToInt32(inchargeId);
            }
            else
            {
                employeeAssignment.InchargeId = 0;
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                employeeAssignment.RoleId = Convert.ToInt32(roleId);
            }
            else
            {
                employeeAssignment.RoleId = 0;
            }
            //if (!string.IsNullOrEmpty(explanationId))
            //{
            //    employeeAssignment.ExplanationId = Convert.ToInt32(explanationId);
            //}
            //else
            //{
            //    employeeAssignment.ExplanationId = 0;
            //}

            employeeAssignment.ExplanationId = explanationId;
            if (!string.IsNullOrEmpty(companyId))
            {
                employeeAssignment.CompanyId = Convert.ToInt32(companyId);
            }
            else
            {
                employeeAssignment.CompanyId = 0;
            }

            if (!string.IsNullOrEmpty(status))
            {
                employeeAssignment.IsActive = status;
            }
            else
            {
                employeeAssignment.IsActive = "";
            }

            List<EmployeeAssignmentViewModel> employeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesBySearchFilter(employeeAssignment);

            return Ok(employeeAssignmentViewModels);

        }

        [HttpGet]
        public IHttpActionResult SearchForecastEmployee(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status, string year, string timeStampId)
        {
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();

            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }

            if (!string.IsNullOrEmpty(sectionId))
            {
                employeeAssignment.SectionId = sectionId;
            }
            else
            {
                employeeAssignment.SectionId = "";
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                employeeAssignment.DepartmentId = departmentId;
            }
            else
            {
                employeeAssignment.DepartmentId = "";
            }
            if (!string.IsNullOrEmpty(inchargeId))
            {
                employeeAssignment.InchargeId = inchargeId;
            }
            else
            {
                employeeAssignment.InchargeId = "";
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                employeeAssignment.RoleId = roleId;
            }
            else
            {
                employeeAssignment.RoleId = "";
            }

            employeeAssignment.ExplanationId = explanationId;
            if (!string.IsNullOrEmpty(companyId))
            {
                employeeAssignment.CompanyId = companyId;
            }
            else
            {
                employeeAssignment.CompanyId = "";
            }

            if (!string.IsNullOrEmpty(year))
            {
                employeeAssignment.Year = year;
            }
            else
            {
                employeeAssignment.Year = "";
            }

            if (!string.IsNullOrEmpty(status))
            {
                employeeAssignment.IsActive = status;
            }
            else
            {
                employeeAssignment.IsActive = "";
            }

            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);
            //List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);
            List<ForecastAssignmentViewModel> _forecsatEmployeeAssignmentViewModels = new List<ForecastAssignmentViewModel>();


            return Ok(forecsatEmployeeAssignmentViewModels);
        }


        [Route("api/utilities/GetAllAssignmentData")]
        [HttpGet]
        public IHttpActionResult GetAllAssignmentData(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status, string year, string timeStampId)
        {
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();

            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }

            if (!string.IsNullOrEmpty(sectionId))
            {
                employeeAssignment.SectionId = sectionId;
            }
            else
            {
                employeeAssignment.SectionId = "";
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                employeeAssignment.DepartmentId = departmentId;
            }
            else
            {
                employeeAssignment.DepartmentId = "";
            }
            if (!string.IsNullOrEmpty(inchargeId))
            {
                employeeAssignment.InchargeId = inchargeId;
            }
            else
            {
                employeeAssignment.InchargeId = "";
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                employeeAssignment.RoleId = roleId;
            }
            else
            {
                employeeAssignment.RoleId = "";
            }

            employeeAssignment.ExplanationId = explanationId;
            if (!string.IsNullOrEmpty(companyId))
            {
                employeeAssignment.CompanyId = companyId;
            }
            else
            {
                employeeAssignment.CompanyId = "";
            }

            if (!string.IsNullOrEmpty(year))
            {
                employeeAssignment.Year = year;
            }
            else
            {
                employeeAssignment.Year = "";
            }

            if (!string.IsNullOrEmpty(status))
            {
                employeeAssignment.IsActive = status;
            }
            else
            {
                employeeAssignment.IsActive = "";
            }

            //List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);
            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetAllAssignmentData(employeeAssignment);
            //List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);
            List<ForecastAssignmentViewModel> _forecsatEmployeeAssignmentViewModels = new List<ForecastAssignmentViewModel>();


            return Ok(forecsatEmployeeAssignmentViewModels);
        }

        [Route("api/utilities/GetAllBudgetData")]
        [HttpGet]
        public IHttpActionResult GetAllBudgetData(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status, string year, string timeStampId)
        {
            EmployeeBudgetAssignment employeeAssignment = new EmployeeBudgetAssignment();

            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }

            if (!string.IsNullOrEmpty(sectionId))
            {
                employeeAssignment.SectionId = sectionId;
            }
            else
            {
                employeeAssignment.SectionId = "";
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                employeeAssignment.DepartmentId = departmentId;
            }
            else
            {
                employeeAssignment.DepartmentId = "";
            }
            if (!string.IsNullOrEmpty(inchargeId))
            {
                employeeAssignment.InchargeId = inchargeId;
            }
            else
            {
                employeeAssignment.InchargeId = "";
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                employeeAssignment.RoleId = roleId;
            }
            else
            {
                employeeAssignment.RoleId = "";
            }

            employeeAssignment.ExplanationId = explanationId;
            if (!string.IsNullOrEmpty(companyId))
            {
                employeeAssignment.CompanyId = companyId;
            }
            else
            {
                employeeAssignment.CompanyId = "";
            }

            if (!string.IsNullOrEmpty(year))
            {
                var arrYearWithBudgetType = year.Split('_');
                employeeAssignment.Year = arrYearWithBudgetType[0];
                string budgetType = arrYearWithBudgetType[1];
                if (!string.IsNullOrEmpty(budgetType))
                {
                    if (Convert.ToInt32(budgetType) == 1)
                    {
                        employeeAssignment.FirstHalfBudget = true;
                        employeeAssignment.SecondHalfBudget = false;
                    }
                    else if (Convert.ToInt32(budgetType) == 2)
                    {
                        employeeAssignment.FirstHalfBudget = false;
                        employeeAssignment.SecondHalfBudget = true;
                    }
                }
            }
            else
            {
                employeeAssignment.Year = "";
                employeeAssignment.FirstHalfBudget = false;
                employeeAssignment.SecondHalfBudget = false;
            }

            if (!string.IsNullOrEmpty(status))
            {
                employeeAssignment.IsActive = status;
            }
            else
            {
                employeeAssignment.IsActive = "";
            }

            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetAllBudgetData(employeeAssignment);

            return Ok(forecsatEmployeeAssignmentViewModels);
        }

        [Route("api/utilities/CompareGrade/{unitPrice}")]
        [HttpGet]
        public IHttpActionResult CompareGrade(string unitPrice)
        {
            decimal tempVal = 0;
            if (decimal.TryParse(unitPrice, out tempVal))
            {
                if (tempVal > 0)
                {
                    Salary salary = salaryBLL.CompareSalary(tempVal);
                    if (salary != null)
                    {
                        return Ok(salary);
                    }
                    else
                    {
                        return BadRequest("Invalid Unit Price");
                    }
                }
                else
                {
                    return BadRequest("Invalid Unit Price");
                }
            }
            else
            {
                return BadRequest("Invalid Unit Price");
            }
        }

        [Route("api/utilities/EmployeeListByNameFilter/{employeeName}")]
        [HttpGet]
        public IHttpActionResult EmployeeListByNameFilter(string employeeName)
        {
            List<Employee> employees = employeeBLL.EmployeeListByNameFilter(employeeName);
            return Ok(employees);

            //if (!string.IsNullOrEmpty(employeeName))
            //{
            //    List<Employee> employees = employeeBLL.EmployeeListByNameFilter(employeeName);
            //    return Ok(employees);
            //}
            //else
            //{
            //    return BadRequest("Employee Name is empty");
            //}                       
        }


        [Route("api/utilities/GetEmployeesByName/{employeeName}")]
        [HttpGet]
        public IHttpActionResult GetEmployeesByName(string employeeName)
        {
            if (!String.IsNullOrEmpty(employeeName))
            {
                List<EmployeeAssignmentViewModel> employeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesByName(employeeName.Trim());

                if (employeeAssignmentViewModels.Count > 0)
                {
                    return Ok(employeeAssignmentViewModels);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IHttpActionResult SearchMultipleEmployee(EmployeeAssignmentDTO employeeAssignment)
        {
            List<EmployeeAssignmentViewModel> employeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesBySearchFilterForMultipleSearch(employeeAssignment);

            return Ok(employeeAssignmentViewModels);
        }

        [HttpGet]
        public IHttpActionResult SectionCount(string sectionIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(sectionIds))
            {
                int tempSectionId = 0;
                String[] _ids = sectionIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempSectionId);
                        if (tempSectionId > 0)
                        {
                            var section = sectionBLL.GetSectionBySectionId(tempSectionId);
                            if (section != null)
                            {
                                int result = sectionBLL.GetSectionCountWithEmployeeAsignment(tempSectionId);
                                //countMessage.Add(result + " rows counted for " + section.SectionName);
                                if (result > 1)
                                {
                                    ///countMessage.Add(result + " projects assigned for " + section.SectionName);
                                    countMessage.Add(result + " のプロジェクトが " + section.SectionName + " にアサインされています");

                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + section.SectionName);
                                    countMessage.Add(result + " のプロジェクトが " + section.SectionName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }
        [HttpGet]
        public IHttpActionResult CategoryCount(string categoryIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(categoryIds))
            {
                int tempCategoryId = 0;
                String[] _ids = categoryIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempCategoryId);
                        if (tempCategoryId > 0)
                        {
                            var section = categoryBLL.GetCategoryByCategoryId(tempCategoryId);
                            if (section != null)
                            {
                                int result = sectionBLL.GetSectionCountWithEmployeeAsignment(tempCategoryId);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + section.CategoryName);
                                    countMessage.Add(result + " のプロジェクトが " + section.CategoryName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + section.CategoryName);
                                    countMessage.Add(result + " のプロジェクトが " + section.CategoryName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }
        [HttpGet]
        public IHttpActionResult DepartmentCount(string departmentIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(departmentIds))
            {
                int tempDepartmentId = 0;
                String[] _ids = departmentIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempDepartmentId);
                        if (tempDepartmentId > 0)
                        {
                            var department = departmentBLL.GetDepartmentByDepartemntId(tempDepartmentId);
                            if (department != null)
                            {
                                int result = departmentBLL.GetDepartmentCountWithEmployeeAsignment(tempDepartmentId);
                                //countMessage.Add(result + " rows counted for " + department.DepartmentName);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + department.DepartmentName);
                                    countMessage.Add(result + " のプロジェクトが " + department.DepartmentName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + department.DepartmentName);
                                    countMessage.Add(result + " のプロジェクトが " + department.DepartmentName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [HttpGet]
        public IHttpActionResult CompanyCount(string companyIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(companyIds))
            {
                int tempCompanyId = 0;
                String[] _ids = companyIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempCompanyId);
                        if (tempCompanyId > 0)
                        {
                            var company = companyBLL.GetCompanyByCompanyId(tempCompanyId);
                            if (company != null)
                            {
                                int result = companyBLL.GetCompanyCountWithEmployeeAsignment(tempCompanyId);
                                //countMessage.Add(result + " rows counted for " + company.CompanyName);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + company.CompanyName);
                                    countMessage.Add(result + " のプロジェクトが " + company.CompanyName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + company.CompanyName);
                                    countMessage.Add(result + " のプロジェクトが " + company.CompanyName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [HttpGet]
        public IHttpActionResult InChargeCount(string inChargeIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(inChargeIds))
            {
                int tempInChargeId = 0;
                String[] _ids = inChargeIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempInChargeId);
                        if (tempInChargeId > 0)
                        {
                            var inCharge = inChargeBLL.GetInChargeByInChargeId(tempInChargeId);
                            if (inCharge != null)
                            {
                                int result = inChargeBLL.GetInChargeCountWithEmployeeAsignment(tempInChargeId);
                                //countMessage.Add(result + " rows counted for " + inCharge.InChargeName);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + inCharge.InChargeName);
                                    countMessage.Add(result + " のプロジェクトが " + inCharge.InChargeName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + inCharge.InChargeName);
                                    countMessage.Add(result + " のプロジェクトが " + inCharge.InChargeName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [HttpGet]
        public IHttpActionResult RoleCount(string roleIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(roleIds))
            {
                int tempRoleId = 0;
                String[] _ids = roleIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempRoleId);
                        if (tempRoleId > 0)
                        {
                            var role = roleBLL.GetRoleByRoleId(tempRoleId);
                            if (role != null)
                            {
                                int result = roleBLL.GetRoleCountWithEmployeeAsignment(tempRoleId);
                                //countMessage.Add(result + " rows counted for " + role.RoleName);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + role.RoleName);
                                    countMessage.Add(result + " のプロジェクトが " + role.RoleName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + role.RoleName);
                                    countMessage.Add(result + " のプロジェクトが " + role.RoleName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [HttpGet]
        public IHttpActionResult ExplanationCount(string explanationIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(explanationIds))
            {
                int tempExplanationId = 0;
                String[] _ids = explanationIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempExplanationId);
                        if (tempExplanationId > 0)
                        {
                            var explanation = explanationsBLL.GetExplanationByExplanationId(tempExplanationId);
                            if (explanation != null)
                            {
                                int result = explanationsBLL.GetExplanationCountWithEmployeeAsignment(tempExplanationId);
                                countMessage.Add(result + " rows counted for " + explanation.ExplanationName);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + explanation.ExplanationName);
                                    countMessage.Add(result + " のプロジェクトが " + explanation.ExplanationName + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + explanation.ExplanationName);
                                    countMessage.Add(result + " のプロジェクトが " + explanation.ExplanationName + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [HttpGet]
        public IHttpActionResult SalaryCount(string salaryIds)
        {
            List<string> countMessage = new List<string>();
            if (!String.IsNullOrEmpty(salaryIds))
            {
                int tempSalaryId = 0;
                String[] _ids = salaryIds.Split(',');

                if (_ids.Length > 0)
                {
                    foreach (string item in _ids)
                    {
                        Int32.TryParse(item, out tempSalaryId);
                        if (tempSalaryId > 0)
                        {
                            var salary = salaryBLL.GetSalaryBySalaryId(tempSalaryId);
                            if (salary != null)
                            {
                                int result = salaryBLL.GetSalaryCountWithEmployeeAsignment(tempSalaryId);
                                //countMessage.Add(result + " rows counted for " + salary.SalaryGrade);
                                if (result > 1)
                                {
                                    //countMessage.Add(result + " projects assigned for " + salary.SalaryGrade);
                                    countMessage.Add(result + " のプロジェクトが " + salary.SalaryGrade + " にアサインされています");
                                }
                                else
                                {
                                    //countMessage.Add(result + " project assigned for " + salary.SalaryGrade);
                                    countMessage.Add(result + " のプロジェクトが " + salary.SalaryGrade + " にアサインされています");
                                }
                            }
                        }
                    }
                    return Ok(countMessage);
                }
                else
                {
                    return BadRequest("Invalid Data");
                }


            }
            else
            {
                return BadRequest("Invalid Data");
            }

        }

        [Route("api/utilities/DeleteAssignments/")]
        [HttpGet]
        [ActionName("DeleteAssignments")]
        public IHttpActionResult DeleteAssignments()
        {
            string id = "0";
            int tempValue = 0;
            if (int.TryParse(id, out tempValue))
            {
                if (tempValue > 0)
                {
                    List<Department> departments = departmentBLL.GetAllDepartmentsBySectionId(sectionId: tempValue);
                    return Ok(departments);
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Something Went Wrong!!!");
            }


        }

        [HttpPost]
        [Route("api/utilities/CreateHistory/")]
        public IHttpActionResult CreateForecastHistory(ForecastHistoryDto forecastHistoryDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            List<Forecast> forecasts = new List<Forecast>();
            string message = "Something went wrong!!!";
            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                        employeeAssignment.Id = Convert.ToInt32(item.AssignmentId);
                        employeeAssignment.Remarks = item.Remarks;
                        employeeAssignment.UpdatedBy = session["userName"].ToString();
                        employeeAssignment.UpdatedDate = DateTime.Now;
                        employeeAssignment.EmployeeId = item.EmployeeId;
                        employeeAssignment.SectionId = item.SectionId;
                        employeeAssignment.DepartmentId = item.DepartmentId;
                        employeeAssignment.InchargeId = item.InchargeId;
                        employeeAssignment.RoleId = item.RoleId;
                        employeeAssignment.ExplanationId = item.ExplanationId;
                        employeeAssignment.CompanyId = item.CompanyId;
                        employeeAssignment.GradeId = item.GradeId;
                        employeeAssignment.UnitPrice = item.UnitPrice;
                        int updateResult = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 10, item.OctPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 11, item.NovPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 12, item.DecPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 1, item.JanPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 2, item.FebPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 3, item.MarPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 4, item.AprPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 5, item.MayPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 6, item.JunPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 7, item.JulPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 8, item.AugPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 9, item.SepPoint));


                    }

                    ForecastHisory forecastHisory = new ForecastHisory();
                    forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                    forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                    forecastHisory.Forecasts = forecasts;
                    forecastHisory.CreatedBy = session["userName"].ToString();
                    forecastHisory.CreatedDate = DateTime.Now;


                    var result = forecastBLL.CreateTimeStamp(forecastHisory);

                    if (result > 0)
                    {
                        message = "データが保存されました!";
                    }
                }
            }



            return Ok(message);
        }

        [HttpPost]
        [Route("api/utilities/UpdateForecastData/")]
        public IHttpActionResult UpdateForecastData(ForecastHistoryDto forecastHistoryDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            List<Forecast> forecastsPrevious = new List<Forecast>();
            List<AssignmentHistory> assignmentHistories = new List<AssignmentHistory>();
            List<AssignmentHistory> assignmentHistoriesOriginal = new List<AssignmentHistory>();

            List<EmployeeAssignment> employeeAssignmentsForTimeStamp = new List<EmployeeAssignment>();
            List<ForecastList> forecastsForTimeStamps = new List<ForecastList>();
            List<Forecast> forecasts = new List<Forecast>();

            string strUpdatedAssignmentIds = "";
            int selected_year = 0;

            string message = "Something went wrong!!!";
            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    ForecastHisory forecastHisory = new ForecastHisory();
                    forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                    forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                    forecastHisory.Forecasts = forecastsPrevious;
                    forecastHisory.CreatedBy = session["userName"].ToString();
                    forecastHisory.CreatedDate = DateTime.Now;

                    int yearlyDataTimeStampId = forecastBLL.CreateTimeStampsForYearlyEditData(forecastHisory);


                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                        if (string.IsNullOrEmpty(strUpdatedAssignmentIds))
                        {
                            strUpdatedAssignmentIds = item.AssignmentId;
                        }
                        else
                        {
                            strUpdatedAssignmentIds = strUpdatedAssignmentIds + "," + item.AssignmentId;
                        }
                        employeeAssignment.Id = Convert.ToInt32(item.AssignmentId);
                        employeeAssignment.Remarks = item.Remarks;
                        employeeAssignment.UpdatedBy = session["userName"].ToString();
                        employeeAssignment.UpdatedDate = DateTime.Now;
                        employeeAssignment.EmployeeId = item.EmployeeId;
                        employeeAssignment.SectionId = item.SectionId;
                        employeeAssignment.DepartmentId = item.DepartmentId;
                        employeeAssignment.InchargeId = item.InchargeId;
                        employeeAssignment.RoleId = item.RoleId;
                        employeeAssignment.ExplanationId = item.ExplanationId;
                        employeeAssignment.CompanyId = item.CompanyId;

                        employeeAssignment.Year = item.Year.ToString();
                        selected_year = item.Year;
                        employeeAssignment.EmployeeName = item.EmployeeName;
                        employeeAssignment.BCYR = item.BCYR;
                        employeeAssignment.BCYRCell = item.BCYRCell;

                        if (item.CompanyId != null)
                        {
                            if (item.CompanyId.Value != 3)
                            {
                                employeeAssignment.GradeId = null;
                            }
                            else
                            {
                                employeeAssignment.GradeId = item.GradeId;
                            }
                        }
                        else
                        {
                            employeeAssignment.GradeId = null;
                        }

                        employeeAssignment.UnitPrice = item.UnitPrice;

                        //previous data
                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(employeeAssignment.Id);

                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;


                        if (forecastHistoryDto.CellInfo.Count > 0)
                        {
                            foreach (var cellItem in forecastHistoryDto.CellInfo)
                            {
                                var itemData = cellItem.Split('_');
                                if (Convert.ToInt32(itemData[0]) == employeeAssignment.Id)
                                {
                                    int checkResults = employeeAssignmentBLL.CheckForOriginalAssignmentIsExists(employeeAssignment.Id);

                                    if (Convert.ToInt32(itemData[1]) == 2)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "2", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.Remarks, "Remarks");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.Remarks, "Remarks");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 3)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "3", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.SectionId, "SectionId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.SectionId, "SectionId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 4)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "4", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.DepartmentId, "DepartmentId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.DepartmentId, "DepartmentId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 5)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "5", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.InChargeId, "InChargeId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.InChargeId, "InChargeId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 6)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "6", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.RoleId, "RoleId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.RoleId, "RoleId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 7)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "7", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.ExplanationId, "ExplanationId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.ExplanationId, "ExplanationId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 8)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "8", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.CompanyId, "CompanyId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.CompanyId, "CompanyId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 9)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "9", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.GradeId, "GradeId");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.GradeId, "GradeId");
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 10)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "10", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            if (checkResults > 0)
                                            {
                                                //update original data
                                                int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory, _assignmentHistory.UnitPrice, "UnitPrice");
                                            }
                                            else
                                            {
                                                //insert original data
                                                int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory, _assignmentHistory.UnitPrice, "UnitPrice");
                                            }
                                        }
                                    }

                                }
                            }
                        }

                        //update assignment data
                        string updateBYCRCCell = item.BCYRCell;
                        if (!string.IsNullOrEmpty(updateBYCRCCell))
                        {
                            if (item.IsDeletePending)
                            {
                                employeeAssignment.IsDeletePending = false;
                            }
                        }
                        employeeAssignment.IsActiveAssignment = true;
                        int updateResult = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

                        int timestampResults = employeeAssignmentBLL.InsertEmployeeAssignmentsForTimeStamps(employeeAssignment, yearlyDataTimeStampId);
                        int latestAssignmentIdForTimeStamps = 0;
                        if (timestampResults > 0)
                        {
                            latestAssignmentIdForTimeStamps = employeeAssignmentBLL.GetAssignmentTimeStampsLastId();
                        }

                        //original forecasted data
                        forecastsPrevious.AddRange(forecastBLL.GetForecastsByAssignmentId(Convert.ToInt32(item.AssignmentId)));
                        assignmentHistories.Add(_assignmentHistory);

                        //update/insert the original forecasted data
                        if (forecastHistoryDto.CellInfo.Count > 0)
                        {
                            List<Forecast> _objPreviousForecastedData = forecastBLL.GetForecastsByAssignmentId(Convert.ToInt32(item.AssignmentId));

                            foreach (var cellItem in forecastHistoryDto.CellInfo)
                            {
                                var itemData = cellItem.Split('_');
                                if (Convert.ToInt32(itemData[0]) == employeeAssignment.Id)
                                {
                                    //check if forecast 同一データが登録済みです in original
                                    int forecastResulttOrg = employeeAssignmentBLL.CheckForOriginalForecastDataIsExists(Convert.ToInt32(item.AssignmentId));

                                    if (Convert.ToInt32(itemData[1]) == 11)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "11", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 10);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 10)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 10)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 12)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "12", _assignmentHistory.BCYRCell);

                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 11);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 11)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 11)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 13)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "13", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 12);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 12)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 12)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 14)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "14", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 1);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 1)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 1)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 15)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "15", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 2);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 2)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 2)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 16)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "16", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 3);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 3)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 3)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 17)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "17", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 4);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 4)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 4)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 18)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "18", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 5);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 5)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 5)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 19)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "19", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 6);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 6)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 6)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 20)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "20", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 7);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 7)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 7)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 21)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "21", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 8);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 8)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 8)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (Convert.ToInt32(itemData[1]) == 22)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "22", _assignmentHistory.BCYRCell);
                                        if (validForOriginalData)
                                        {
                                            int isMonthExists = employeeAssignmentBLL.CheckMonthIdExistsForOrgForecast(Convert.ToInt32(item.AssignmentId), 9);
                                            if (_objPreviousForecastedData.Count > 0)
                                            {
                                                if (isMonthExists > 0)
                                                {
                                                    //update org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 9)
                                                        {
                                                            forecastBLL.UpdateOriginalForecast(forecastItem);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //insert org
                                                    foreach (var forecastItem in _objPreviousForecastedData)
                                                    {
                                                        if (forecastItem.Month == 9)
                                                        {
                                                            forecastItem.CreatedBy = session["userName"].ToString();
                                                            forecastBLL.InsertOriginalForecast(forecastItem);
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 10, item.OctPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 11, item.NovPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 12, item.DecPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 1, item.JanPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 2, item.FebPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 3, item.MarPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 4, item.AprPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 5, item.MayPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 6, item.JunPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 7, item.JulPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 8, item.AugPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 9, item.SepPoint));

                        if (forecasts.Count > 0)
                        {
                            foreach (var forecast in forecasts)
                            {
                                forecastBLL.UpdateForecast(forecast);
                                forecast.EmployeeAssignmentId = latestAssignmentIdForTimeStamps;
                                forecastBLL.InsertForecastWithTimeStamp(forecast);
                            }
                            forecasts = new List<Forecast>();
                        }

                        //updated data
                        AssignmentHistory _assignmentHistoryOriginal = new AssignmentHistory();
                        _assignmentHistoryOriginal = forecastBLL.GetPreviousAssignmentDataById(employeeAssignment.Id);

                        _assignmentHistoryOriginal.CreatedBy = session["userName"].ToString();
                        _assignmentHistoryOriginal.CreatedDate = DateTime.Now;
                        assignmentHistoriesOriginal.Add(_assignmentHistoryOriginal);


                    }

                    foreach (var forecastPrevious in forecastsPrevious)
                    {
                        forecastPrevious.CreatedBy = session["userName"].ToString();
                        forecastPrevious.CreatedDate = DateTime.Now;
                    }

                    //ForecastHisory forecastHisory = new ForecastHisory();
                    //forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                    //forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                    //forecastHisory.Forecasts = forecastsPrevious;
                    //forecastHisory.CreatedBy = session["userName"].ToString();
                    //forecastHisory.CreatedDate = DateTime.Now;

                    //author: sudipto,31/5/23: history create
                    //var resultTimeStamp = forecastBLL.CreateTimeStamp(forecastHisory);
                    bool isUpdate = true;
                    bool isDeleted = false;

                    //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate, isDeleted);
                    var resultTimeStamp = forecastBLL.CreateAssignmentHistoryWithTimeStampId(assignmentHistories, isUpdate, isDeleted, yearlyDataTimeStampId);

                    //assignment aganist timestamps store all other data with year
                    int update_results = 0;
                    List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();
                    List<ForecastDto> forecastWithTimeStamps = new List<ForecastDto>();

                    employeeAssignments = employeeAssignmentBLL.GetEmployeesAssignmentsByYear(selected_year, strUpdatedAssignmentIds);
                    if (employeeAssignments.Count > 0)
                    {
                        foreach (var assignmentItem in employeeAssignments)
                        {
                            var forecastList = employeeAssignmentBLL.GetAssignmentForecastByYearAndAssignmentId(assignmentItem.Id, selected_year);
                            update_results = employeeAssignmentBLL.InsertEmployeeAssignmentsForTimeStamps(assignmentItem, yearlyDataTimeStampId);

                            int latestAssignmentId = 0;
                            if (update_results > 0)
                            {
                                latestAssignmentId = employeeAssignmentBLL.GetAssignmentTimeStampsLastId();
                            }

                            if (forecastList.Count > 0)
                            {
                                foreach (var forecast in forecastList)
                                {
                                    //forecastBLL.UpdateForecast(forecast);
                                    forecast.EmployeeAssignmentId = latestAssignmentId;
                                    forecast.CreatedBy = session["userName"].ToString();
                                    forecastBLL.InsertForecastWithTimeStamp(forecast);
                                }
                                forecasts = new List<Forecast>();
                            }
                        }
                    }

                    ////edited data history: start
                    //AssignmentHistory _originalAssignmentHistory = new AssignmentHistory();
                    //_originalAssignmentHistory = forecastBLL.GetPreviousAssignmentDataById(employeeAssignment.Id);

                    //_originalAssignmentHistory.CreatedBy = session["userName"].ToString();
                    //_originalAssignmentHistory.CreatedDate = DateTime.Now;
                    if (assignmentHistoriesOriginal.Count > 0)
                    {
                        foreach (var item in assignmentHistoriesOriginal)
                        {
                            forecastBLL.CreateAssignmenttHistory(item, yearlyDataTimeStampId, isUpdate, isDeleted, true);
                        }
                    }

                    ////edited data history: end


                    if (forecastHistoryDto.CellInfo.Count > 0)
                    {
                        foreach (var item in forecastHistoryDto.CellInfo)
                        {
                            var storePreviousCells = "";
                            var itemData = item.Split('_');
                            EmployeeAssignment _employeeAssignment = employeeAssignmentBLL.GetBCYRCellAndPendingCellsByAssignmentId(Convert.ToInt32(itemData[0]));
                            if (String.IsNullOrEmpty(_employeeAssignment.BCYRCell))
                            {
                                //result += itemData[1];
                                storePreviousCells += itemData[1];
                            }
                            else
                            {
                                var arrPreviousCells = _employeeAssignment.BCYRCell.Split(',');
                                foreach (var previousItem in arrPreviousCells)
                                {
                                    if (string.IsNullOrEmpty(storePreviousCells))
                                    {
                                        storePreviousCells = previousItem;
                                    }
                                    else
                                    {
                                        if (storePreviousCells.IndexOf(',') > 0)
                                        {
                                            var arrCheckForCellExits = storePreviousCells.Split(',');
                                            var isValidCellForUpdate = true;

                                            foreach (var checkExitsItem in arrCheckForCellExits)
                                            {
                                                if (checkExitsItem == previousItem)
                                                {
                                                    isValidCellForUpdate = false;
                                                }
                                            }
                                            if (isValidCellForUpdate)
                                            {
                                                storePreviousCells = storePreviousCells + "," + previousItem;
                                            }
                                        }
                                        else
                                        {
                                            if (storePreviousCells != previousItem)
                                            {
                                                storePreviousCells = storePreviousCells + "," + previousItem;
                                            }
                                        }
                                    }
                                }

                                var arrCheckForAlreadyExitsCell = storePreviousCells.Split(',');
                                var isValidForUpdateCell = true;

                                foreach (var cellItem in arrCheckForAlreadyExitsCell)
                                {
                                    if (cellItem == itemData[1])
                                    {
                                        isValidForUpdateCell = false;
                                    }
                                }
                                if (isValidForUpdateCell)
                                {
                                    //result += "," + itemData[1];
                                    storePreviousCells += "," + itemData[1];
                                }
                            }
                            string bCYRCellPending = _employeeAssignment.BCYRCellPending;
                            string storePendingCells = "";

                            var arrPendingCells = bCYRCellPending.Split(',');
                            foreach (var pendingItem in arrPendingCells)
                            {
                                bool isValidForPendingCellUpdate = true;
                                var arrPreviousCells = storePreviousCells.Split(','); ;
                                foreach (var previousCellItem in arrPreviousCells)
                                {
                                    if (pendingItem == previousCellItem)
                                    {
                                        isValidForPendingCellUpdate = false;
                                    }
                                }
                                if (isValidForPendingCellUpdate)
                                {
                                    if (string.IsNullOrEmpty(storePendingCells))
                                    {
                                        storePendingCells = pendingItem;
                                    }
                                    else
                                    {
                                        storePendingCells = storePendingCells + "," + pendingItem;
                                    }
                                }
                            }
                            //employeeAssignmentBLL.UpdateBCYRCellByAssignmentId(Convert.ToInt32(itemData[0]), storePreviousCells);
                            employeeAssignmentBLL.UpdateBCYRCellBCYRPendingCellByAssignmentId(Convert.ToInt32(itemData[0]), storePreviousCells, storePendingCells);
                        }
                    }


                    if (yearlyDataTimeStampId > 0)
                    {
                        message = yearlyDataTimeStampId.ToString();
                    }
                }
            }

            return Ok(message);
        }

        [HttpPost]
        [Route("api/utilities/UpdateBudgetData/")]
        public IHttpActionResult UpdateBudgetData(ForecastHistoryDto forecastHistoryDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            List<Forecast> forecasts = new List<Forecast>();
            string message = "Something went wrong!!!";

            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                        employeeAssignment.Id = Convert.ToInt32(item.AssignmentId);
                        employeeAssignment.Remarks = item.Remarks;
                        employeeAssignment.UpdatedBy = session["userName"].ToString();
                        employeeAssignment.UpdatedDate = DateTime.Now;
                        employeeAssignment.EmployeeId = item.EmployeeId;
                        employeeAssignment.SectionId = item.SectionId;
                        employeeAssignment.DepartmentId = item.DepartmentId;
                        employeeAssignment.InchargeId = item.InchargeId;
                        employeeAssignment.RoleId = item.RoleId;
                        employeeAssignment.ExplanationId = item.ExplanationId;
                        employeeAssignment.CompanyId = item.CompanyId;

                        if (item.CompanyId != null)
                        {
                            if (item.CompanyId.Value != 3)
                            {
                                employeeAssignment.GradeId = null;
                            }
                            else
                            {
                                employeeAssignment.GradeId = item.GradeId;
                            }
                        }
                        else
                        {
                            employeeAssignment.GradeId = null;
                        }

                        employeeAssignment.UnitPrice = item.UnitPrice;

                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;

                        employeeAssignment.IsActiveAssignment = true;
                        int updateResult = employeeAssignmentBLL.UpdateBudgetAssignment(employeeAssignment);


                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 10, item.OctPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 11, item.NovPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 12, item.DecPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 1, item.JanPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 2, item.FebPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 3, item.MarPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 4, item.AprPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 5, item.MayPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 6, item.JunPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 7, item.JulPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 8, item.AugPoint));
                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId), item.Year, 9, item.SepPoint));

                        if (forecasts.Count > 0)
                        {
                            foreach (var forecast in forecasts)
                            {
                                forecastBLL.UpdateBudgetForecast(forecast);
                            }
                            forecasts = new List<Forecast>();
                        }
                    }
                }
            }

            return Ok(message);
        }


        [HttpGet]
        public IHttpActionResult ApprovedForecastData(string assignementId, bool isDeletedRow)
        {
            int results;
            if (!isDeletedRow)
            {
                results = employeeAssignmentBLL.ApproveDeletedRow(assignementId);
            }
            else
            {
                results = employeeAssignmentBLL.ApproveAssignement(assignementId);
            }
            return Ok(results);
        }

        //un-approve add employee and delete employee
        [HttpGet]
        [Route("api/utilities/UnApprovedForecastData/")]
        public IHttpActionResult UnApprovedForecastData(string assignementId, bool isDeletedRow)
        {
            int results = 0;
            bool isValidForUnapprovedRow = employeeAssignmentBLL.CheckForUnApprovedRow(assignementId, isDeletedRow);
            if (isValidForUnapprovedRow)
            {
                if (!isDeletedRow)
                {
                    results = employeeAssignmentBLL.UnApproveDeletedRow(assignementId);
                }
                else
                {
                    results = employeeAssignmentBLL.UnApproveAssignement(assignementId);
                }
            }
            return Ok(results);
        }

        [HttpGet]
        public IHttpActionResult ApprovedCellData(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();
            bool isUpdateData = false;

            //bool isValidForApproved = employeeAssignmentBLL.CheckForApprovedCells(assignementId, selectedCells);
            int resultData = employeeAssignmentBLL.CheckForApprovedCells(assignementId, selectedCells);
            if (resultData > 0)
            {
                //string previousApprovedCells = employeeAssignmentBLL.GetPreviousApprovedCells(assignementId);
                _employeeAssignment = employeeAssignmentBLL.GetPreviousApprovedCells(assignementId);

                if (string.IsNullOrEmpty(_employeeAssignment.BCYRCellApproved))
                {
                    _employeeAssignment.BCYRCellApproved = "";
                    _employeeAssignment.BCYRCellApproved = selectedCells;
                    isUpdateData = true;
                }
                else
                {
                    bool isCellAlreadyHave = _employeeAssignment.BCYRCellApproved.Contains(selectedCells);
                    if (!isCellAlreadyHave)
                    {
                        isUpdateData = true;
                        _employeeAssignment.BCYRCellApproved = _employeeAssignment.BCYRCellApproved + "," + selectedCells;
                    }
                }

                if (isUpdateData)
                {
                    string storeBYCRCells = "";
                    //commented out because: cell will not update while approve but only when saved.
                    //if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCell))
                    //{
                    //    var arrBYCRCells = _employeeAssignment.BCYRCell.Split(',');
                    //    foreach (var bycrCell in arrBYCRCells)
                    //    {
                    //        if (bycrCell != selectedCells)
                    //        {
                    //            if (storeBYCRCells == "")
                    //            {
                    //                storeBYCRCells = bycrCell;
                    //            }
                    //            else
                    //            {
                    //                var arrCheckForCellValueIsExists = storeBYCRCells.Split(',');
                    //                bool isCellExists = false;
                    //                foreach (var cellIteam in arrCheckForCellValueIsExists)
                    //                {
                    //                    if (cellIteam == bycrCell)
                    //                    {
                    //                        isCellExists = true;
                    //                        break;
                    //                    }
                    //                }
                    //                if (!isCellExists)
                    //                {
                    //                    storeBYCRCells = storeBYCRCells + "," + bycrCell;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}

                    //updated change cells: because some cells value is duplicated.
                    if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCell))
                    {
                        var arrBYCRCells = _employeeAssignment.BCYRCell.Split(',');
                        foreach (var bycrCell in arrBYCRCells)
                        {
                            if (storeBYCRCells == "")
                            {
                                storeBYCRCells = bycrCell;
                            }
                            else
                            {
                                var arrCheckForCellValueIsExists = storeBYCRCells.Split(',');
                                bool isCellExists = false;
                                foreach (var cellIteam in arrCheckForCellValueIsExists)
                                {
                                    if (cellIteam == bycrCell)
                                    {
                                        isCellExists = true;
                                        break;
                                    }
                                }
                                if (!isCellExists)
                                {
                                    storeBYCRCells = storeBYCRCells + "," + bycrCell;
                                }
                            }
                        }
                    }

                    //int results = employeeAssignmentBLL.UpdateBYCRCells(assignementId, _employeeAssignment.BCYRCellApproved,storeBYCRCells);
                    int results = employeeAssignmentBLL.UpdateBYCRCells(assignementId, _employeeAssignment.BCYRCellApproved, storeBYCRCells);
                    return Ok(results);
                }
                else
                {
                    return Ok(0); ;
                }
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpGet]
        [Route("api/utilities/IsValidForApprovalCell/")]
        public IHttpActionResult IsValidForApprovalCell(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();

            //bool isValidForApproved = employeeAssignmentBLL.CheckForApprovedCells(assignementId, selectedCells);
            int resultData = employeeAssignmentBLL.CheckForApprovedCells(assignementId, selectedCells);
            return Ok(resultData);
        }

        [HttpGet]
        [Route("api/utilities/IsValidForApprovalRow/")]
        public IHttpActionResult IsValidForApprovalRow(string assignementId, bool isDeletedRow)
        {
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();
            bool isValidForApproved = false;
            _employeeAssignment = employeeAssignmentBLL.GetEmployeeAssignmentForCheckApproval(assignementId);

            if (!isDeletedRow)
            {
                //check for delete
                //if ((!Convert.ToBoolean(_employeeAssignment.IsActive) && !_employeeAssignment.IsDeleted) || _employeeAssignment.IsDeletePending)
                if ((!Convert.ToBoolean(_employeeAssignment.IsActive) && !_employeeAssignment.IsDeleted) && !_employeeAssignment.IsDeletePending)
                {
                    return Ok(3);
                }
                else if (_employeeAssignment.IsDeletePending)
                {
                    return Ok(4);
                }
                else
                {
                    return Ok(0);
                }
            }
            else
            {
                //check for add row data       
                //if (_employeeAssignment.BCYR || _employeeAssignment.IsRowPending)
                if (_employeeAssignment.BCYR)
                {
                    return Ok(1);
                }
                else if (_employeeAssignment.IsRowPending)
                {
                    return Ok(2);
                }
                else
                {
                    return Ok(0);
                }
            }
        }

        //un-approve cell wise data
        [HttpGet]
        [Route("api/utilities/UnApprovedCellData/")]
        public IHttpActionResult UnApprovedCellData(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();
            bool isUpdateData = false;

            //string previousApprovedCells = employeeAssignmentBLL.GetPreviousApprovedCells(assignementId);
            bool isValidForUnapproved = employeeAssignmentBLL.CheckForUnApprovedCells(assignementId, selectedCells);
            if (isValidForUnapproved)
            {

                _employeeAssignment = employeeAssignmentBLL.GetPreviousApprovedCells(assignementId);
                string approvedBCYRCellList = "";
                //unapproved: start
                if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCellApproved))
                {
                    var arrBCYRCellApproved = _employeeAssignment.BCYRCellApproved.Split(',');
                    foreach (var bycrCellApproved in arrBCYRCellApproved)
                    {
                        if (bycrCellApproved != selectedCells)
                        {
                            isUpdateData = true;

                            if (approvedBCYRCellList == "")
                            {
                                approvedBCYRCellList = bycrCellApproved;
                            }
                            else
                            {
                                approvedBCYRCellList = approvedBCYRCellList + "," + bycrCellApproved;
                            }
                        }
                    }
                }

                string bCYRCellList = "";
                //unapproved: start
                if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCell))
                {
                    isUpdateData = true;
                    bCYRCellList = selectedCells + "," + _employeeAssignment.BCYRCell;
                }
                else
                {
                    isUpdateData = true;
                    bCYRCellList = selectedCells;
                }
                //unapproved: end

                if (isUpdateData)
                {
                    int results = employeeAssignmentBLL.UpdateBYCRCells(assignementId, approvedBCYRCellList, bCYRCellList);
                    return Ok(results);
                }
                else
                {
                    return Ok(0);
                }
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpGet]
        public IHttpActionResult GetTimeStamps(int year)
        {
            List<ForecastHisory> forecastHisories = forecastBLL.GetTimeStamps_Year(year);

            if (forecastHisories.Count > 0)
            {
                return Ok(forecastHisories);
            }
            else
            {
                return Ok(0);
            }
        }

        [HttpGet]
        [Route("api/utilities/GetApprovalTimeStamps/")]
        public IHttpActionResult GetApprovalTimeStamps(int year)
        {
            List<ForecastHisory> forecastHisories = forecastBLL.GetApprovalTimeStamps(year);

            if (forecastHisories.Count > 0)
            {
                return Ok(forecastHisories);
            }
            else
            {
                return NotFound();
            }
        }

        public Forecast ExtraxctToForecast(int assignmentId, int year, int monthId, decimal point)
        {
            var session = System.Web.HttpContext.Current.Session;

            Forecast forecast = new Forecast();
            forecast.EmployeeAssignmentId = assignmentId;
            forecast.CreatedBy = session["userName"].ToString();
            forecast.CreatedDate = DateTime.Now;
            forecast.UpdatedBy = session["userName"].ToString();
            forecast.UpdatedDate = DateTime.Now;
            forecast.Year = year;
            forecast.Month = monthId;
            forecast.Points = point;

            return forecast;
        }


        [HttpPost]
        [Route("api/utilities/CreateEmployee/")]
        public IHttpActionResult CreateNewEmployee(Employee employee)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (!String.IsNullOrEmpty(employee.FullName))
            {
                if (employeeBLL.CheckEmployeeDuplication(employee.FullName))
                {
                    return BadRequest("要員は登録済みです!!!");
                }
                else
                {
                    int result = 0;
                    if (employee.IsUpdate)
                    {
                        employee.IsActive = true;
                        employee.UpdatedBy = session["userName"].ToString();
                        employee.UpdatedDate = DateTime.Now;
                        result = employeeBLL.UpdateEmployee(employee);
                    }
                    else
                    {
                        employee.IsActive = true;
                        employee.CreatedBy = session["userName"].ToString();
                        employee.CreatedDate = DateTime.Now;
                        result = employeeBLL.CreateEmployee(employee);
                    }
                    
                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong");
                    }
                }

            }
            else
            {
                return BadRequest("Invalid Employee Name");
            }
        }

        [HttpPost]
        [Route("api/utilities/CreateUserName/")]
        public IHttpActionResult CreateUserName(User user)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (!String.IsNullOrEmpty(user.UserName))
            {
                if (employeeBLL.CheckUserNameDuplication(user.UserName))
                {
                    return BadRequest("User Name Already Exists!!!");
                }
                else if (employeeBLL.CheckUserEmailDuplication(user.Email))
                {
                    return BadRequest("User Email Already Exists!!!");
                }
                else
                {
                    user.IsActive = true;
                    //user.CreatedBy = session["userName"].ToString();
                    user.CreatedBy = "";
                    user.CreatedDate = DateTime.Now;
                    int result = userBLL.CreateUserName(user);
                    if (result > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong");
                    }
                }

            }
            else
            {
                return BadRequest("Invalid User Name");
            }
        }

        [HttpPost]
        [Route("api/utilities/UpdateUserName/")]
        public IHttpActionResult UpdateUserName(User user)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (!String.IsNullOrEmpty(user.UserName))
            {
                user.UpdatedBy = session["userName"].ToString();
                user.UpdatedDate = DateTime.Now;
                int result = userBLL.UpdateUserName(user);
                if (result > 0)
                {
                    var removedFlag = userBLL.RemoveUserPermissions(user.Id);
                    if (user.UserRoleId == "1")
                    {
                        foreach (var item in UserLinks.adminLinks)
                        {
                            userBLL.CreateUserPermissions(item, user.Id);
                        }
                    }
                    if (user.UserRoleId == "2")
                    {
                        foreach (var item in UserLinks.editorLinks)
                        {
                            userBLL.CreateUserPermissions(item, user.Id);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }

            }
            else
            {
                return BadRequest("Invalid User Name");
            }
        }

        [HttpGet]
        [Route("api/utilities/EmployeeList/")]
        public IHttpActionResult GetEmployeeList()
        {
            List<Employee> employees = employeeBLL.GetAllEmployees();
            if (employees.Count > 0)
            {
                return Ok(employees);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/utilities/EmployeeListBudgetEditFiltered/{budget_year}")]
        public IHttpActionResult GetEmployeeListForBudgetEdit(string budget_year)
        {
            if (!string.IsNullOrEmpty(budget_year))
            {
                var arrBudgetYear = budget_year.Split('_');
                if (!string.IsNullOrEmpty(arrBudgetYear[0].ToString()))
                {
                    List<Employee> _objEmployees = employeeBLL.GetEmployeeListForBudgetEdit(Convert.ToInt32(arrBudgetYear[0]), Convert.ToInt32(arrBudgetYear[1]));

                    if (_objEmployees.Count > 0)
                    {
                        return Ok(_objEmployees);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/utilities/EmployeeListForEmployeeAssignment/{assignment_year}")]
        public IHttpActionResult GetEmployeeListEmployeeAssignments(string assignment_year)
        {
            if (!string.IsNullOrEmpty(assignment_year))
            {
                List<Employee> _objEmployees = employeeBLL.GetEmployeeListEmployeeAssignments(Convert.ToInt32(assignment_year));

                if (_objEmployees.Count > 0)
                {
                    return Ok(_objEmployees);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("api/utilities/GetOnlyAdmin/")]
        public IHttpActionResult GetOnlyAdmin()
        {
            User filteredUser = new User();
            var session = System.Web.HttpContext.Current.Session;
            var user = userBLL.GetUserByUserName(session["userName"].ToString());
            List<User> users = userBLL.GetAllUsers();
            if (users.Count > 0)
            {
                foreach (var item in users)
                {
                    if (item.Id == user.Id)
                    {
                        item.Password = "***";
                        filteredUser = item;
                    }
                }
            }

            return Ok(filteredUser);
        }
        [HttpGet]
        [Route("api/utilities/GetSingleUserInfo/")]
        public IHttpActionResult GetSingleUserInfo(string userName)
        {
            User filteredUser = new User();
            var user = userBLL.GetUserByUserName(userName);
            List<User> users = userBLL.GetAllUsers();
            if (users.Count > 0)
            {
                foreach (var item in users)
                {
                    if (item.Id == user.Id)
                    {
                        filteredUser = item;
                    }
                }
            }

            return Ok(filteredUser);
        }
        [HttpGet]
        [Route("api/utilities/UpdateUserStatus/")]
        public IHttpActionResult UpdateUserStatus(string changeUserName, string changeUserRole, string changeUserStatus)
        {
            string userRoleId = "";
            bool isActive = false;
            if (string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "1")
            {
                userRoleId = "3";
                isActive = true;
            }
            else if (string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "0")
            {
                userRoleId = "0";
                isActive = false;
            }
            else if (string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "3")
            {
                userRoleId = "0";
                isActive = true;
            }
            else if (!string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "3")
            {
                userRoleId = "0";
                isActive = true;
            }
            else if (!string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "1")
            {
                if (changeUserRole.ToLower() == "admin")
                {
                    userRoleId = "1";
                }
                else if (changeUserRole.ToLower() == "editor")
                {
                    userRoleId = "2";
                }
                else if (changeUserRole.ToLower() == "Visitor")
                {
                    userRoleId = "3";
                }
                else
                {
                    userRoleId = "0";
                }

                isActive = true;
            }
            else
            {
                if (changeUserRole.ToLower() == "admin")
                {
                    userRoleId = "1";
                }
                else if (changeUserRole.ToLower() == "editor")
                {
                    userRoleId = "2";
                }
                else if (changeUserRole.ToLower() == "visitor")
                {
                    userRoleId = "3";
                }
                else
                {
                    userRoleId = "0";
                }
                isActive = false;
            }
            var session = System.Web.HttpContext.Current.Session;
            string updatedBy = session["userName"].ToString();
            DateTime updatedDate = DateTime.Now;

            int results = userBLL.UpdateUserStatus(changeUserName, userRoleId, isActive, updatedBy, updatedDate);
            //List<User> users = userBLL.GetAllUsers();
            //if (users.Count > 0)
            //{
            //    foreach (var item in users)
            //    {
            //        if (item.Id == user.Id)
            //        {
            //            filteredUser = item;
            //        }
            //    }
            //}

            return Ok(results);
        }
        [HttpGet]
        [Route("api/utilities/GetUserList/")]
        public IHttpActionResult GetUserList()
        {
            var session = System.Web.HttpContext.Current.Session;
            var user = userBLL.GetUserByUserName(session["userName"].ToString());
            List<User> filteredUsers = new List<User>();
            List<User> users = userBLL.GetAllUsers();
            if (user.UserRoleId == "1")
            {
                if (users.Count > 0)
                {
                    foreach (var item in users)
                    {
                        if (item.Id == user.Id)
                        {
                            continue;
                        }
                        item.Password = "***";
                        filteredUsers.Add(item);
                    }
                    return Ok(filteredUsers);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                if (users.Count > 0)
                {
                    foreach (var item in users)
                    {
                        if (item.Id == user.Id)
                        {
                            item.Password = "***";
                            filteredUsers.Add(item);
                        }

                    }
                    return Ok(filteredUsers);
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPut]
        [Route("api/utilities/UpdateEmployee/")]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (!String.IsNullOrEmpty(employee.FullName))
            {
                employee.UpdatedBy = session["userName"].ToString();
                employee.UpdatedDate = DateTime.Now;
                int result = employeeBLL.UpdateEmployee(employee);
                if (result > 0)
                {
                    return Ok("Updated Successfully");
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }

            }
            else
            {
                return BadRequest("Something Went Wrong");
            }

        }

        [HttpDelete]
        [Route("api/utilities/InactiveEmployee/")]
        public IHttpActionResult InactiveEmployee(Employee employee)
        {
            var session = System.Web.HttpContext.Current.Session;
            employee.IsActive = false;
            employee.UpdatedBy = session["userName"].ToString();
            employee.UpdatedDate = DateTime.Now;
            int result = employeeBLL.InactiveEmployee(employee);
            if (result > 0)
            {
                return Ok("Deactivated Successfully");
            }
            else
            {
                return BadRequest("Something Went Wrong");
            }
        }

        [HttpPost]
        [Route("api/utilities/ExcelAssignment/")]
        //public IHttpActionResult CreateAssignment_Excel(List<ExcelAssignmentDto> excelAssignmentDtos)
        public IHttpActionResult CreateAssignment_Excel(ForecastHistoryDto forecastHistoryDto)
        {
            List<object> returnedIdList = new List<object>();
            List<AssignmentHistory> assignmentHistories = new List<AssignmentHistory>();

            string tempTimeStampId = "";
            var selected_year = 0;
            var strUpdatedAssignmentIds = "";
            string yearlyDataTimeStampId = "";

            var session = System.Web.HttpContext.Current.Session;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    ForecastHisory forecastHisory = new ForecastHisory();
                    forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                    forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                    //forecastHisory.Forecasts = forecastsPrevious;
                    forecastHisory.CreatedBy = session["userName"].ToString();
                    forecastHisory.CreatedDate = DateTime.Now;
                    yearlyDataTimeStampId = forecastHistoryDto.TimeStampId;
                    int timestampId_add_employee = 0;
                    bool isOnlyAdd = false;

                    if (string.IsNullOrEmpty(yearlyDataTimeStampId))
                    {
                        isOnlyAdd = true;
                        timestampId_add_employee = forecastBLL.CreateTimeStampsForYearlyEditData(forecastHisory);
                        yearlyDataTimeStampId = timestampId_add_employee.ToString();
                    }
                    //int yearlyDataTimeStampId = forecastBLL.CreateTimeStampsForYearlyEditData(forecastHisory);

                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();

                        //tempTimeStampId = forecastHistoryDto.TimeStampId;

                        if (item.EmployeeId == "" || item.EmployeeId == null)
                        {
                            continue;
                        }

                        employeeAssignment.EmployeeId = item.EmployeeId;
                        employeeAssignment.SectionId = item.SectionId;
                        employeeAssignment.DepartmentId = item.DepartmentId;
                        employeeAssignment.InchargeId = item.InchargeId;
                        employeeAssignment.RoleId = item.RoleId;
                        employeeAssignment.ExplanationId = item.ExplanationId == null ? null : item.ExplanationId.ToString();
                        employeeAssignment.CompanyId = item.CompanyId;
                        selected_year = item.Year;

                        if (item.CompanyId != null)
                        {
                            if (item.CompanyId.Value != 3)
                            {
                                employeeAssignment.GradeId = null;
                            }
                            else
                            {
                                employeeAssignment.GradeId = item.GradeId;
                            }
                        }
                        else
                        {
                            employeeAssignment.GradeId = null;
                        }
                        employeeAssignment.UnitPrice = item.UnitPrice;
                        employeeAssignment.Year = item.Year.ToString();
                        employeeAssignment.IsActive = true.ToString();
                        employeeAssignment.SubCode = 1;
                        employeeAssignment.CreatedBy = session["userName"].ToString();
                        employeeAssignment.CreatedDate = DateTime.Now;
                        employeeAssignment.Remarks = item.Remarks;

                        //check for bcyr value
                        employeeAssignment.BCYR = item.BCYR;
                        employeeAssignment.BCYRCell = "";
                        employeeAssignment.EmployeeName = item.EmployeeName;


                        employeeAssignment.EmployeeName = item.EmployeeName;
                        employeeAssignment.DuplicateFrom = item.DuplicateFrom;
                        employeeAssignment.DuplicateCount = item.DuplicateCount;
                        employeeAssignment.RoleChanged = item.RoleChanged;
                        employeeAssignment.UnitPriceChanged = item.UnitPriceChanged;

                        int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
                        int return_assignmentIdWithTimeStamp = employeeAssignmentBLL.InsertEmployeeAssignmentsForTimeStamps(employeeAssignment, Convert.ToInt32(yearlyDataTimeStampId));
                        int latestAssignmentIdForTimeStamps = 0;
                        if (return_assignmentIdWithTimeStamp > 0)
                        {
                            latestAssignmentIdForTimeStamps = employeeAssignmentBLL.GetAssignmentTimeStampsLastId();
                        }
                        if (result == 1)
                        {
                            int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                            if (string.IsNullOrEmpty(strUpdatedAssignmentIds))
                            {
                                strUpdatedAssignmentIds = employeeAssignmentLastId.ToString();
                            }
                            else
                            {
                                strUpdatedAssignmentIds = strUpdatedAssignmentIds + "," + employeeAssignmentLastId;
                            }
                            returnedIdList.Add(new
                            {
                                assignmentId = item.AssignmentId,
                                returnedId = employeeAssignmentLastId
                            });
                            List<Forecast> forecasts = new List<Forecast>();

                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });

                            foreach (var forecastItem in forecasts)
                            {
                                int resultSave = forecastBLL.CreateForecast(forecastItem);
                                forecastItem.EmployeeAssignmentId = latestAssignmentIdForTimeStamps;
                                forecastBLL.InsertForecastWithTimeStamp(forecastItem);
                            }
                        }
                        int lastAssignmentId = employeeAssignmentBLL.GetLastId();
                        if (!item.BCYR)
                        {
                            var getDataBySplit = item.BCYRCell.Split(',');

                            foreach (var splittedData in getDataBySplit)
                            {
                                var cells = employeeAssignmentBLL.GetBCYRCellByAssignmentId(lastAssignmentId);
                                var nestedSplittedData = splittedData.Split('_');
                                if (String.IsNullOrEmpty(cells))
                                {
                                    cells = nestedSplittedData[1];
                                }
                                else
                                {
                                    cells += "," + nestedSplittedData[1];
                                }
                                employeeAssignmentBLL.UpdateBCYRCellByAssignmentId(lastAssignmentId, cells);

                            }
                        }

                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(lastAssignmentId);

                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;
                        assignmentHistories.Add(_assignmentHistory);
                    }

                    bool isUpdate = false;
                    bool isDeleted = false;

                    if (!string.IsNullOrEmpty(yearlyDataTimeStampId))
                    {
                        foreach (var item in assignmentHistories)
                        {
                            forecastBLL.CreateAssignmenttHistory(item, Convert.ToInt32(yearlyDataTimeStampId), isUpdate, isDeleted, false);
                        }
                    }
                    else
                    {
                        //ForecastHisory forecastHisory = new ForecastHisory();
                        //forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                        //forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                        ////forecastHisory.Forecasts = forecastsPrevious;
                        //forecastHisory.CreatedBy = session["userName"].ToString();
                        //forecastHisory.CreatedDate = DateTime.Now;

                        //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate, isDeleted);
                        var resultTimeStamp = forecastBLL.CreateAssignmentHistoryWithTimeStampId(assignmentHistories, isUpdate, isDeleted, Convert.ToInt32(yearlyDataTimeStampId));
                    }

                    if (isOnlyAdd)
                    {
                        //assignment aganist timestamps store all other data with year
                        int update_results = 0;
                        List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();
                        List<ForecastDto> forecastWithTimeStamps = new List<ForecastDto>();

                        employeeAssignments = employeeAssignmentBLL.GetEmployeesAssignmentsByYear(selected_year, strUpdatedAssignmentIds);
                        if (employeeAssignments.Count > 0)
                        {
                            foreach (var assignmentItem in employeeAssignments)
                            {
                                var forecastList = employeeAssignmentBLL.GetAssignmentForecastByYearAndAssignmentId(assignmentItem.Id, selected_year);
                                update_results = employeeAssignmentBLL.InsertEmployeeAssignmentsForTimeStamps(assignmentItem, Convert.ToInt32(yearlyDataTimeStampId));

                                int latestAssignmentId = 0;
                                if (update_results > 0)
                                {
                                    latestAssignmentId = employeeAssignmentBLL.GetAssignmentTimeStampsLastId();
                                }

                                if (forecastList.Count > 0)
                                {
                                    foreach (var forecast in forecastList)
                                    {
                                        //forecastBLL.UpdateForecast(forecast);
                                        forecast.EmployeeAssignmentId = latestAssignmentId;
                                        forecast.CreatedBy = session["userName"].ToString();
                                        forecastBLL.InsertForecastWithTimeStamp(forecast);
                                    }
                                    //forecasts = new List<Forecast>();
                                }
                            }
                        }
                    }
                }
            }
            return Ok(returnedIdList);
        }

        [HttpPost]
        [Route("api/utilities/ExcelAssignmentBudget/")]
        //public IHttpActionResult CreateAssignment_Excel(List<ExcelAssignmentDto> excelAssignmentDtos)
        public IHttpActionResult CreateAssignment_Excel_Budget(EmployeeBudgetDto forecastHistoryDto)
        {
            List<object> returnedIdList = new List<object>();
            List<AssignmentHistory> assignmentHistories = new List<AssignmentHistory>();

            string tempTimeStampId = "";
            var session = System.Web.HttpContext.Current.Session;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeBudget employeeAssignment = new EmployeeBudget();

                        if (item.EmployeeId == "" || item.EmployeeId == null)
                        {
                            continue;
                        }

                        employeeAssignment.EmployeeId = item.EmployeeId;
                        employeeAssignment.SectionId = item.SectionId;
                        employeeAssignment.DepartmentId = item.DepartmentId;
                        employeeAssignment.InchargeId = item.InchargeId;
                        employeeAssignment.RoleId = item.RoleId;
                        employeeAssignment.ExplanationId = item.ExplanationId == null ? null : item.ExplanationId.ToString();
                        employeeAssignment.CompanyId = item.CompanyId;
                        if (item.CompanyId != null)
                        {
                            if (item.CompanyId.Value != 3)
                            {
                                employeeAssignment.GradeId = null;
                            }
                            else
                            {
                                employeeAssignment.GradeId = item.GradeId;
                            }
                        }
                        else
                        {
                            employeeAssignment.GradeId = null;
                        }

                        employeeAssignment.UnitPrice = item.UnitPrice;
                        employeeAssignment.Year = item.Year.ToString();
                        employeeAssignment.IsActive = true.ToString();
                        employeeAssignment.CreatedBy = session["userName"].ToString();
                        employeeAssignment.CreatedDate = DateTime.Now;
                        employeeAssignment.Remarks = item.Remarks;
                        employeeAssignment.BCYR = false;
                        employeeAssignment.BCYRCell = "";
                        employeeAssignment.EmployeeName = item.EmployeeName;

                        employeeAssignment.DuplicateFrom = item.DuplicateFrom;
                        employeeAssignment.DuplicateCount = item.DuplicateCount;
                        employeeAssignment.RoleChanged = item.RoleChanged;
                        employeeAssignment.UnitPriceChanged = item.UnitPriceChanged;

                        if (!string.IsNullOrEmpty(forecastHistoryDto.YearWithBudgetType))
                        {
                            var arrYearWithBudget = forecastHistoryDto.YearWithBudgetType.Split('_');
                            if (Convert.ToInt32(arrYearWithBudget[1]) == 1)
                            {
                                employeeAssignment.FirstHalfBudget = true;
                                employeeAssignment.SecondHalfBudget = false;
                            }
                            else if (Convert.ToInt32(arrYearWithBudget[1]) == 2)
                            {
                                employeeAssignment.FirstHalfBudget = false;
                                employeeAssignment.SecondHalfBudget = true;
                            }
                            else
                            {
                                employeeAssignment.FirstHalfBudget = false;
                                employeeAssignment.SecondHalfBudget = false;
                            }
                            employeeAssignment.FinalizedBudget = false;
                        }

                        int result = employeeAssignmentBLL.CreateBudgets(employeeAssignment);
                        //checked:done

                        EmployeeAssignmentDTO employeeAssignmentDTO = new EmployeeAssignmentDTO();
                        employeeAssignmentDTO = new EmployeeAssignmentDTO();


                        //checking:start
                        if (result == 1)
                        {
                            int employeeAssignmentLastId = employeeAssignmentBLL.GetBudgetLastId();
                            returnedIdList.Add(new
                            {
                                assignmentId = item.AssignmentId,
                                returnedId = employeeAssignmentLastId
                            });
                            List<Forecast> forecasts = new List<Forecast>();

                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                            foreach (var forecastItem in forecasts)
                            {
                                int resultSave = forecastBLL.CreateForecastBudget(forecastItem);
                            }
                        }
                    }
                }
            }
            return Ok(returnedIdList);
        }

        [HttpDelete]
        [Route("api/utilities/ExcelDeleteAssignment/")]
        //public IHttpActionResult DeleteAssignment_Excel(int[] ids,string tempTimeStampId,string historyName,int year)
        public IHttpActionResult DeleteAssignment_Excel(ForecastHistoryDto forecastHistoryDto)
        {
            string tempTimeStampId = forecastHistoryDto.TimeStampId;
            int[] ids = forecastHistoryDto.DeletedRowIds;
            string historyName = forecastHistoryDto.HistoryName;
            int year = forecastHistoryDto.Year;

            var session = System.Web.HttpContext.Current.Session;
            List<AssignmentHistory> assignmentHistories = new List<AssignmentHistory>();

            if (ids.Length > 0)
            {
                ForecastHisory forecastHisory = new ForecastHisory();
                forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                forecastHisory.Year = forecastHistoryDto.Year; //forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                //forecastHisory.Forecasts = forecastsPrevious;
                forecastHisory.CreatedBy = session["userName"].ToString();
                forecastHisory.CreatedDate = DateTime.Now;
                int yearlyDataTimeStampId = forecastBLL.CreateTimeStampsForYearlyEditData(forecastHisory);

                foreach (var item in ids)
                {
                    if (!string.IsNullOrEmpty(item.ToString()))
                    {
                        employeeAssignmentBLL.RemoveAssignment(item);

                        //store delete information as a delete history

                        //get deleted information 
                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(item);

                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;
                        assignmentHistories.Add(_assignmentHistory);
                    }
                }

                //store the information as history
                bool isUpdate = false;
                bool isDeleted = true;

                if (!string.IsNullOrEmpty(tempTimeStampId))
                {
                    foreach (var item in assignmentHistories)
                    {
                        forecastBLL.CreateAssignmenttHistory(item, Convert.ToInt32(tempTimeStampId), isUpdate, isDeleted, false);
                    }
                }
                else
                {
                    //ForecastHisory forecastHisory = new ForecastHisory();
                    //forecastHisory.TimeStamp = historyName;
                    //forecastHisory.Year = year;
                    //forecastHisory.CreatedBy = session["userName"].ToString();
                    //forecastHisory.CreatedDate = DateTime.Now;
                    //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate, isDeleted);
                    var resultTimeStamp = forecastBLL.CreateAssignmentHistoryWithTimeStampId(assignmentHistories, isUpdate, isDeleted, yearlyDataTimeStampId);
                }
            }

            return Ok("Operation Completed!");
        }
        [HttpGet]
        [Route("api/utilities/GetUserLogs/")]
        public IHttpActionResult GetUserLogs()
        {
            var result = userBLL.GetUserLogs();
            return Ok(result);
        }
        [HttpPost]
        [Route("api/utilities/GetMatchedRowNumber/")]
        public IHttpActionResult GetMatchedRowNumber(ForecastHistoryDto forecastHistoryDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            User user = userBLL.GetUserLog(session["userName"].ToString());

            int matchedCount = 0;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
            {
                foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                {
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(Convert.ToInt32(item.AssignmentId), user.LoginTime);
                    //var compareResult = TimeSpan.Compare(user.LoginTime.TimeOfDay,result.CreatedDate.TimeOfDay);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    //if (compareDate>=0)
                    //{
                    if (compareDate >= 0)
                    {
                        if (user.UserName == result.CreatedBy)
                        {
                            continue;
                        }
                        matchedCount++;
                    }
                    //}

                }

            }
            return Ok(matchedCount);
        }

        [HttpPost]
        [Route("api/utilities/GetMatchedUserNames/")]
        public IHttpActionResult GetMatchedUserNames(ForecastHistoryDto forecastHistoryDto)
        {
            List<string> userNameList = new List<string>();
            string userNames = "";
            var session = System.Web.HttpContext.Current.Session;
            User user = userBLL.GetUserLog(session["userName"].ToString());

            //int matchedCount = 0;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
            {
                foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                {
                    var result = forecastBLL.MatchForecastHistoryUsernamesByAssignmentId(Convert.ToInt32(item.AssignmentId), user.LoginTime);
                    //var compareResult = TimeSpan.Compare(user.LoginTime.TimeOfDay, result.CreatedDate.TimeOfDay);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    if (compareDate >= 0)
                    {
                        if (user.UserName == result.CreatedBy)
                        {
                            continue;
                        }
                        if (!userNameList.Contains(result.CreatedBy))
                        {
                            userNameList.Add(result.CreatedBy);
                        }
                    }


                }

            }
            if (userNameList.Count > 0)
            {
                foreach (var item in userNameList)
                {
                    userNames += item + ",";
                }
                userNames = userNames.TrimEnd(',');
            }

            return Ok(userNames);
        }

        [HttpPost]
        [Route("api/utilities/GetMatchedRows/")]
        public IHttpActionResult GetMatchedRows(ForecastHistoryDto forecastHistoryDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            User user = userBLL.GetUserLog(session["userName"].ToString());

            List<Object> matchedRows = new List<object>();
            int matchedCount = 0;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
            {
                foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                {
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(Convert.ToInt32(item.AssignmentId), user.LoginTime);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    if (compareDate >= 0)
                    {
                        if (user.UserName == result.CreatedBy)
                        {
                            continue;
                        }
                        // latest assignment history
                        var resultList = forecastBLL.GetMatchedForecastHistoryByAssignmentId(Convert.ToInt32(item.AssignmentId));

                        var singleForecastList = forecastBLL.GetForecastsByAssignmentId(Convert.ToInt32(item.AssignmentId));


                        string _octP = "";
                        if (resultList.Where(f => f.Month == 10).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 10).SingleOrDefault().Points)
                        {
                            _octP = "";
                        }
                        else
                        {
                            _octP = "(" + resultList.Where(f => f.Month == 10).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 10).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _novP = "";
                        if (resultList.Where(f => f.Month == 11).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 11).SingleOrDefault().Points)
                        {
                            _novP = "";
                        }
                        else
                        {
                            _novP = "(" + resultList.Where(f => f.Month == 11).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 11).SingleOrDefault().Points.ToString("0.0");
                        }

                        string _decP = "";
                        if (resultList.Where(f => f.Month == 12).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 12).SingleOrDefault().Points)
                        {
                            _decP = "";
                        }
                        else
                        {
                            _decP = "(" + resultList.Where(f => f.Month == 12).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 12).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _janP = "";
                        if (resultList.Where(f => f.Month == 1).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 1).SingleOrDefault().Points)
                        {
                            _janP = "";
                        }
                        else
                        {
                            _janP = "(" + resultList.Where(f => f.Month == 1).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 1).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _febP = "";
                        if (resultList.Where(f => f.Month == 2).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 2).SingleOrDefault().Points)
                        {
                            _febP = "";
                        }
                        else
                        {
                            _febP = "(" + resultList.Where(f => f.Month == 2).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 2).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _marP = "";
                        if (resultList.Where(f => f.Month == 3).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 3).SingleOrDefault().Points)
                        {
                            _marP = "";
                        }
                        else
                        {
                            _marP = "(" + resultList.Where(f => f.Month == 3).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 3).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _aprP = "";
                        if (resultList.Where(f => f.Month == 4).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 4).SingleOrDefault().Points)
                        {
                            _aprP = "";
                        }
                        else
                        {
                            _aprP = "(" + resultList.Where(f => f.Month == 4).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 4).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _mayP = "";
                        if (resultList.Where(f => f.Month == 5).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 5).SingleOrDefault().Points)
                        {
                            _mayP = "";
                        }
                        else
                        {
                            _mayP = "(" + resultList.Where(f => f.Month == 5).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 5).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _junP = "";
                        if (resultList.Where(f => f.Month == 6).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 6).SingleOrDefault().Points)
                        {
                            _junP = "";
                        }
                        else
                        {
                            _junP = "(" + resultList.Where(f => f.Month == 6).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 6).SingleOrDefault().Points.ToString("0.0");
                        }

                        string _julP = "";
                        if (resultList.Where(f => f.Month == 7).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 7).SingleOrDefault().Points)
                        {
                            _julP = "";
                        }
                        else
                        {
                            _julP = "(" + resultList.Where(f => f.Month == 7).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 7).SingleOrDefault().Points.ToString("0.0");
                        }
                        string _augP = "";
                        if (resultList.Where(f => f.Month == 8).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 8).SingleOrDefault().Points)
                        {
                            _augP = "";
                        }
                        else
                        {
                            _augP = "(" + resultList.Where(f => f.Month == 8).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 8).SingleOrDefault().Points.ToString("0.0");
                        }

                        string _sepP = "";
                        if (resultList.Where(f => f.Month == 9).SingleOrDefault().Points == singleForecastList.Where(sf => sf.Month == 9).SingleOrDefault().Points)
                        {
                            _sepP = "";
                        }
                        else
                        {
                            _sepP = "(" + resultList.Where(f => f.Month == 9).SingleOrDefault().Points.ToString("0.0") + ") " + singleForecastList.Where(sf => sf.Month == 9).SingleOrDefault().Points.ToString("0.0");
                        }

                        matchedRows.Add(new
                        {
                            EmployeeName = item.EmployeeName,
                            CreatedBy = resultList[0].CreatedBy,
                            OctPoints = _octP,
                            NovPoints = _novP,
                            DecPoints = _decP,
                            JanPoints = _janP,
                            FebPoints = _febP,
                            MarPoints = _marP,
                            AprPoints = _aprP,
                            MayPoints = _mayP,
                            JunPoints = _junP,
                            JulPoints = _julP,
                            AugPoints = _augP,
                            SepPoints = _sepP,
                        });


                    }
                }

            }
            return Ok(matchedRows);
        }

        [HttpGet]
        [Route("api/utilities/GetForecatYear/")]
        public IHttpActionResult GetForecatYear()
        {
            var result = forecastBLL.GetForecastYear();
            return Ok(result);
        }

        [HttpGet]
        [Route("api/utilities/GetBudgetYear/")]
        public IHttpActionResult GetBudgetYear()
        {
            var result = forecastBLL.GetBudgetYear();
            return Ok(result);
        }
        [HttpGet]
        [Route("api/utilities/GetBudgetFinalizeYear/")]
        public IHttpActionResult GetBudgetFinalizeYear()
        {
            var result = forecastBLL.GetBudgetFinalizeYear();
            return Ok(result);
        }
        [HttpGet]
        [Route("api/utilities/GetImportYearAndBudgetType/")]
        public IHttpActionResult GetImportYearAndBudgetType()
        {
            ForecastYear _forecastYear = new ForecastYear();

            int latestYear = forecastBLL.GetLatestBudgetYear();
            bool isInitialDataExists = false;
            bool isFirstHalfFinalize = false;

            bool isSecondHalfBudgetExists = false;
            bool isSecondtHalfFinalize = false;


            if (latestYear > 0)
            {
                isInitialDataExists = departmentBLL.CheckForBudgetInitialDataExists(latestYear);
                isSecondHalfBudgetExists = departmentBLL.CheckForBudgetSecondHalfDataExists(latestYear);

                if (isInitialDataExists && isSecondHalfBudgetExists)
                {
                    latestYear = latestYear + 1;
                    isInitialDataExists = false;
                    isSecondHalfBudgetExists = false;
                    isFirstHalfFinalize = false;
                    isSecondtHalfFinalize = false;
                }
                else
                {
                    if (isInitialDataExists)
                    {
                        isFirstHalfFinalize = departmentBLL.CheckForBudgetInitialDataFinalizeExists(latestYear);
                    }
                    if (isSecondHalfBudgetExists)
                    {
                        isSecondtHalfFinalize = departmentBLL.CheckForBudgetSecondHalfDataFinalizeExists(latestYear);
                    }
                }


                _forecastYear.Year = latestYear;
                _forecastYear.FirstHalfBudget = isInitialDataExists;
                _forecastYear.FirstHalfFinalize = isFirstHalfFinalize;
                _forecastYear.SecondHalfBudget = isSecondHalfBudgetExists;
                _forecastYear.SecondHalfFinalze = isSecondtHalfFinalize;
            }
            else
            {
                _forecastYear.Year = 2023;
                _forecastYear.FirstHalfBudget = false;
                _forecastYear.FirstHalfFinalize = false;
                _forecastYear.SecondHalfBudget = false;
                _forecastYear.SecondHalfFinalze = false;
            }


            return Ok(_forecastYear);
        }
        [HttpGet]
        [Route("api/utilities/DuplicateForecastYear/")]
        public IHttpActionResult DuplicateForecastYear(string copyYear, string insertYear, string budgetType)
        {
            var session = System.Web.HttpContext.Current.Session;
            int results = 0;
            int fromDate = 0;
            int toDate = 0;

            if (!string.IsNullOrEmpty(copyYear) && !string.IsNullOrEmpty(insertYear) && !string.IsNullOrEmpty(budgetType))
            {
                fromDate = Convert.ToInt32(copyYear);
                toDate = Convert.ToInt32(insertYear);
                results = forecastBLL.DuplicateBudget(fromDate, toDate, Convert.ToInt32(budgetType));

            }

            //2nd half budget: start
            if (!string.IsNullOrEmpty(budgetType))
            {
                if (Convert.ToInt32(budgetType) == 2)
                {
                    List<EmployeeBudget> _employeeBudgets = new List<EmployeeBudget>();
                    _employeeBudgets = employeeAssignmentBLL.GetSecondHlafBudgetData(Convert.ToInt32(insertYear), Convert.ToInt32(budgetType));

                    int returnAssingmentId = 0;
                    foreach (var budgetItem in _employeeBudgets)
                    {
                        returnAssingmentId = 0;
                        budgetItem.Year = copyYear;
                        returnAssingmentId = employeeAssignmentBLL.IsBudgetMatchWithAssignmentData(budgetItem);

                        List<ForecastDto> _forecastDto = new List<ForecastDto>();
                        if (returnAssingmentId > 0)
                        {
                            _forecastDto = employeeAssignmentBLL.GettForecastDataForSecondHalfBudgetByAssignmentId(returnAssingmentId, Convert.ToInt32(copyYear));
                        }
                        if (_forecastDto.Count > 0)
                        {
                            foreach (var forecastItem in _forecastDto)
                            {
                                Forecast _forecast = new Forecast();
                                _forecast.Points = forecastItem.Points;
                                _forecast.Total = Convert.ToDecimal(forecastItem.Total);
                                //_forecast.Year = forecastItem.Year;
                                _forecast.Year = Convert.ToInt32(insertYear);
                                _forecast.EmployeeAssignmentId = budgetItem.Id;
                                _forecast.Month = forecastItem.Month;
                                _forecast.UpdatedBy = session["userName"].ToString();
                                if (forecastItem.Month == 10)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 11);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 10, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                                if (forecastItem.Month == 11)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 12);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 11, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                                if (forecastItem.Month == 12)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 13);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 12, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                                if (forecastItem.Month == 1)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 14);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 1, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                                if (forecastItem.Month == 2)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 15);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 2, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                                if (forecastItem.Month == 3)
                                {
                                    bool isOriginal = employeeAssignmentBLL.IsOriginalForecastData(returnAssingmentId, Convert.ToInt32(copyYear), 16);
                                    if (isOriginal)
                                    {
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                    else
                                    {
                                        _forecast.Points = employeeAssignmentBLL.GetForecastOriginalPointsForBudget(returnAssingmentId, 3, Convert.ToInt32(copyYear));
                                        results = forecastBLL.UpdateBudgetForecast(_forecast);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Forecast _forecast = new Forecast();
                            _forecast.Points = 0;
                            _forecast.Total = 0;
                            _forecast.Year = Convert.ToInt32(insertYear);
                            _forecast.EmployeeAssignmentId = budgetItem.Id;
                            _forecast.UpdatedBy = session["userName"].ToString();

                            results = 0;

                            _forecast.Month = 10;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                            _forecast.Month = 11;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                            _forecast.Month = 12;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                            _forecast.Month = 1;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                            _forecast.Month = 2;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                            _forecast.Month = 3;
                            results = forecastBLL.UpdateBudgetForecast(_forecast);
                        }
                    }
                }
            }


            return Ok(results);
        }

        [HttpGet]
        [Route("api/utilities/GetHistoriesByTimeStampId/")]
        public IHttpActionResult GetHistoriesByTimeStampId(int timeStampId)
        {
            List<object> forecastHistoryList = new List<object>();
            List<Forecast> historyList = forecastBLL.GetHistoriesByTimeStampId(timeStampId);
            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            if (distinctAssignmentId.Count > 0)
            {
                foreach (var item in distinctAssignmentId)
                {
                    var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                    var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                    var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                    var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                    var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                    var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                    var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                    var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                    var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                    var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                    var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                    var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                    var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                    var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                    var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);

                    var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
                    var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
                    var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
                    var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
                    var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
                    var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
                    var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
                    var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
                    var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
                    var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
                    var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
                    var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

                    forecastHistoryList.Add(new
                    {
                        EmployeeName = employeeName,
                        CreatedBy = historyList[0].CreatedBy,
                        OctPoints = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0"),
                        NovPoints = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0"),
                        DecPoints = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0"),
                        JanPoints = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0"),
                        FebPoints = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0"),
                        MarPoints = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0"),
                        AprPoints = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0"),
                        MayPoints = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0"),
                        JunPoints = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0"),
                        JulPoints = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0"),
                        AugPoints = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0"),
                        SepPoints = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0"),
                    });
                }
            }

            return Ok(forecastHistoryList);
        }

        [HttpGet]
        [Route("api/utilities/GetAssignmentHistoriesByTimeStampId/")]
        public IHttpActionResult GetAssignmentHistoriesByTimeStampId(int timeStampId)
        {
            List<object> forecastHistoryList = new List<object>();
            List<Forecast> historyList = forecastBLL.GetAssignmentHistoriesByTimeStampId(timeStampId, false);

            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            if (distinctAssignmentId.Count > 0)
            {
                foreach (var item in distinctAssignmentId)
                {
                    //Get previous data for history
                    AssignmentHistoryViewModal _assignmentHistoryViewModal = new AssignmentHistoryViewModal();
                    _assignmentHistoryViewModal = forecastBLL.GetAssignmentNamesForHistory(item, timeStampId, false);
                    var employeeName = _assignmentHistoryViewModal.EmployeeName;
                    var rootEmployeeName = _assignmentHistoryViewModal.RootEmployeeName;
                    if (string.IsNullOrEmpty(employeeName))
                    {
                        employeeName = rootEmployeeName;
                    }
                    var sectionName = _assignmentHistoryViewModal.SectionName;
                    var departmentName = _assignmentHistoryViewModal.DepartmentName;
                    var inChargeName = _assignmentHistoryViewModal.InChargeName;
                    var roleName = _assignmentHistoryViewModal.RoleName;
                    var explanationName = _assignmentHistoryViewModal.ExplanationName;
                    var companyName = _assignmentHistoryViewModal.CompanyName;
                    var gradePoints = _assignmentHistoryViewModal.GradePoints;
                    var unitPrice = _assignmentHistoryViewModal.UnitPrice;
                    var remarks = _assignmentHistoryViewModal.Remarks;
                    var isUpdate = _assignmentHistoryViewModal.IsUpdate;
                    var isDeleted = _assignmentHistoryViewModal.IsDeleted;

                    var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                    var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                    var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                    var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                    var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                    var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                    var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                    var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                    var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                    var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                    var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                    var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                    var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                    //Get Edited data for history
                    AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                    _objOriginalForecastedData = forecastBLL.GetAssignmentNamesForHistory(item, timeStampId, true);
                    var employeeNameOrg = _objOriginalForecastedData.EmployeeName;
                    var rootEmployeeNameOrg = _objOriginalForecastedData.RootEmployeeName;
                    if (string.IsNullOrEmpty(employeeNameOrg))
                    {
                        employeeNameOrg = rootEmployeeNameOrg;
                    }
                    var sectionNameOrg = _objOriginalForecastedData.SectionName;
                    var departmentNameOrg = _objOriginalForecastedData.DepartmentName;
                    var inChargeNameOrg = _objOriginalForecastedData.InChargeName;
                    var roleNameOrg = _objOriginalForecastedData.RoleName;
                    var explanationNameOrg = _objOriginalForecastedData.ExplanationName;
                    var companyNameOrg = _objOriginalForecastedData.CompanyName;
                    var gradePointsOrg = _objOriginalForecastedData.GradePoints;
                    var unitPriceOrg = _objOriginalForecastedData.UnitPrice;
                    var remarksOrg = _objOriginalForecastedData.Remarks;
                    var isUpdateOrg = _objOriginalForecastedData.IsUpdate;
                    var isDeletedOrg = _objOriginalForecastedData.IsDeleted;

                    string operationType = "";
                    if (!isUpdate)
                    {
                        if (isDeleted)
                        {
                            operationType = "削除(Deleted)";
                        }
                        else
                        {
                            operationType = "追加 (Inserted)";
                        }
                    }

                    if (isUpdate)
                    {
                        List<Forecast> editedHistoryList = forecastBLL.GetAssignmentHistoriesByTimeStampId(timeStampId, true);
                        var tempListOrg = editedHistoryList.Where(h => h.EmployeeAssignmentId == item).ToList();

                        var octPOriginal = tempListOrg.Where(p => p.Month == 10).SingleOrDefault().Points;
                        var novPOriginal = tempListOrg.Where(p => p.Month == 11).SingleOrDefault().Points;
                        var decPOriginal = tempListOrg.Where(p => p.Month == 12).SingleOrDefault().Points;
                        var janPOriginal = tempListOrg.Where(p => p.Month == 1).SingleOrDefault().Points;
                        var febPOriginal = tempListOrg.Where(p => p.Month == 2).SingleOrDefault().Points;
                        var marPOriginal = tempListOrg.Where(p => p.Month == 3).SingleOrDefault().Points;
                        var aprPOriginal = tempListOrg.Where(p => p.Month == 4).SingleOrDefault().Points;
                        var mayPOriginal = tempListOrg.Where(p => p.Month == 5).SingleOrDefault().Points;
                        var junPOriginal = tempListOrg.Where(p => p.Month == 6).SingleOrDefault().Points;
                        var julPOriginal = tempListOrg.Where(p => p.Month == 7).SingleOrDefault().Points;
                        var augPOriginal = tempListOrg.Where(p => p.Month == 8).SingleOrDefault().Points;
                        var sepPOriginal = tempListOrg.Where(p => p.Month == 9).SingleOrDefault().Points;

                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,
                            IsUpdate = isUpdate,
                            SectionName = sectionName == sectionNameOrg ? "" : "(" + sectionName + ") " + sectionNameOrg,
                            DepartmentName = departmentName == departmentNameOrg ? "" : "(" + departmentName + ") " + departmentNameOrg,
                            InChargeName = inChargeName == inChargeNameOrg ? "" : "(" + inChargeName + ") " + inChargeNameOrg,
                            RoleName = roleName == roleNameOrg ? "" : "(" + roleName + ") " + roleNameOrg,
                            ExplanationName = explanationName == explanationNameOrg ? "" : "(" + explanationName + ") " + explanationNameOrg,
                            CompanyName = companyName == companyNameOrg ? "" : "(" + companyName + ") " + companyNameOrg,
                            GradePoints = gradePoints == gradePointsOrg ? "" : "(" + gradePoints + ") " + gradePointsOrg,
                            UnitPrice = unitPrice == unitPriceOrg ? "" : "(" + Convert.ToInt32(unitPrice).ToString("N0") + ") " + Convert.ToInt32(unitPriceOrg).ToString("N0"),
                            Remarks = remarks == remarksOrg ? "" : "(" + remarks + ") " + remarksOrg,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = "更新 (Updated)",
                            OctPoints = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0"),
                            NovPoints = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0"),
                            DecPoints = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0"),
                            JanPoints = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0"),
                            FebPoints = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0"),
                            MarPoints = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0"),
                            AprPoints = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0"),
                            MayPoints = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0"),
                            JunPoints = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0"),
                            JulPoints = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0"),
                            AugPoints = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0"),
                            SepPoints = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0"),
                        });
                    }
                    else
                    {
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,
                            IsUpdate = isUpdate,
                            SectionName = sectionName == "" ? "" : sectionName,
                            DepartmentName = departmentName == "" ? "" : departmentName,
                            InChargeName = inChargeName == "" ? "" : inChargeName,
                            RoleName = roleName == "" ? "" : roleName,
                            ExplanationName = explanationName == "" ? "" : explanationName,
                            CompanyName = companyName == "" ? "" : companyName,
                            GradePoints = gradePoints == "0" ? "" : gradePoints,
                            UnitPrice = unitPrice == "0" ? "" : Convert.ToInt32(unitPrice).ToString("N0"),
                            Remarks = remarks == "" ? "" : remarks,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = operationType,
                            OctPoints = octP == 0 ? "" : octP.ToString("0.0"),
                            NovPoints = novP == 0 ? "" : novP.ToString("0.0"),
                            DecPoints = decP == 0 ? "" : decP.ToString("0.0"),
                            JanPoints = janP == 0 ? "" : janP.ToString("0.0"),
                            FebPoints = febP == 0 ? "" : febP.ToString("0.0"),
                            MarPoints = marP == 0 ? "" : marP.ToString("0.0"),
                            AprPoints = aprP == 0 ? "" : aprP.ToString("0.0"),
                            MayPoints = mayP == 0 ? "" : mayP.ToString("0.0"),
                            JunPoints = junP == 0 ? "" : junP.ToString("0.0"),
                            JulPoints = julP == 0 ? "" : julP.ToString("0.0"),
                            AugPoints = augP == 0 ? "" : augP.ToString("0.0"),
                            SepPoints = sepP == 0 ? "" : sepP.ToString("0.0"),
                        });
                    }

                }
            }

            return Ok(forecastHistoryList);
        }

        [HttpGet]
        [Route("api/utilities/GetApprovalHistoriesByTimeStampId/")]
        public IHttpActionResult GetApprovalHistoriesByTimeStampId(int timeStampId)
        {
            List<object> forecastHistoryList = new List<object>();
            List<Forecast> historyList = forecastBLL.GetApprovalHistoriesByTimeStampId(timeStampId);

            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            if (distinctAssignmentId.Count > 0)
            {
                //<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td><lable>${element.ExplanationName}</label></td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`
                foreach (var item in distinctAssignmentId)
                {
                    ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();
                    AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                    _approvalHistoryViewModal = forecastBLL.GetApprovalNamesForHistory(item, timeStampId);

                    //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                    var employeeName = _approvalHistoryViewModal.EmployeeName;
                    var sectionName = _approvalHistoryViewModal.SectionName;
                    var departmentName = _approvalHistoryViewModal.DepartmentName;
                    var inChargeName = _approvalHistoryViewModal.InChargeName;
                    var roleName = _approvalHistoryViewModal.RoleName;
                    var explanationName = _approvalHistoryViewModal.ExplanationName;
                    var companyName = _approvalHistoryViewModal.CompanyName;
                    var gradePoints = _approvalHistoryViewModal.GradePoints;
                    var unitPrice = _approvalHistoryViewModal.UnitPrice;
                    var remarks = _approvalHistoryViewModal.Remarks;
                    var isUpdate = _approvalHistoryViewModal.IsUpdate;
                    var isDeleteRow = _approvalHistoryViewModal.IsDeleteEmployee;
                    var isAddRow = _approvalHistoryViewModal.IsAddEmployee;
                    var isUpdateCells = _approvalHistoryViewModal.IsCellWiseUpdate;
                    var approvedEmployeeAssignmentId = _approvalHistoryViewModal.Id;

                    var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

                    var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
                    var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
                    var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
                    var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
                    var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
                    var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
                    var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
                    var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
                    var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
                    var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
                    var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
                    var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

                    //var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);
                    var originalForecastData = forecastBLL.GetForecastHitostyForApproval(approvedEmployeeAssignmentId);

                    //_objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(approvedEmployeeAssignmentId);
                    _objOriginalForecastedData = forecastBLL.GetOriginalForecastedDataForApproval(item, timeStampId);

                    var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
                    var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
                    var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
                    var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
                    var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
                    var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
                    var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
                    var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
                    var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
                    var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
                    var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
                    var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

                    if (isAddRow)
                    {
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,
                            IsUpdate = isUpdate,
                            SectionName = sectionName == "" ? "" : sectionName,
                            DepartmentName = departmentName == "" ? "" : departmentName,
                            InChargeName = inChargeName == "" ? "" : inChargeName,
                            RoleName = roleName == "" ? "" : roleName,
                            ExplanationName = explanationName == "" ? "" : explanationName,
                            CompanyName = companyName == "" ? "" : companyName,
                            GradePoints = gradePoints == "0" ? "" : gradePoints,
                            UnitPrice = unitPrice == "0" ? "" : Convert.ToInt32(unitPrice).ToString("N0"),
                            Remarks = remarks == "" ? "" : remarks,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = "追加 (Add Employee)",
                            OctPoints = octP == 0 ? "" : octP.ToString("0.0"),
                            NovPoints = novP == 0 ? "" : novP.ToString("0.0"),
                            DecPoints = decP == 0 ? "" : decP.ToString("0.0"),
                            JanPoints = janP == 0 ? "" : janP.ToString("0.0"),
                            FebPoints = febP == 0 ? "" : febP.ToString("0.0"),
                            MarPoints = marP == 0 ? "" : marP.ToString("0.0"),
                            AprPoints = aprP == 0 ? "" : aprP.ToString("0.0"),
                            MayPoints = mayP == 0 ? "" : mayP.ToString("0.0"),
                            JunPoints = junP == 0 ? "" : junP.ToString("0.0"),
                            JulPoints = julP == 0 ? "" : julP.ToString("0.0"),
                            AugPoints = augP == 0 ? "" : augP.ToString("0.0"),
                            SepPoints = sepP == 0 ? "" : sepP.ToString("0.0"),
                        });
                    }
                    else if (isDeleteRow)
                    {
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,
                            IsUpdate = isUpdate,
                            SectionName = sectionName == "" ? "" : sectionName,
                            DepartmentName = departmentName == "" ? "" : departmentName,
                            InChargeName = inChargeName == "" ? "" : inChargeName,
                            RoleName = roleName == "" ? "" : roleName,
                            ExplanationName = explanationName == "" ? "" : explanationName,
                            CompanyName = companyName == "" ? "" : companyName,
                            GradePoints = gradePoints == "0" ? "" : gradePoints,
                            UnitPrice = unitPrice == "0" ? "" : Convert.ToInt32(unitPrice).ToString("N0"),
                            Remarks = remarks == "" ? "" : remarks,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = "削除 (Delete Employee)",
                            OctPoints = octP == 0 ? "" : octP.ToString("0.0"),
                            NovPoints = novP == 0 ? "" : novP.ToString("0.0"),
                            DecPoints = decP == 0 ? "" : decP.ToString("0.0"),
                            JanPoints = janP == 0 ? "" : janP.ToString("0.0"),
                            FebPoints = febP == 0 ? "" : febP.ToString("0.0"),
                            MarPoints = marP == 0 ? "" : marP.ToString("0.0"),
                            AprPoints = aprP == 0 ? "" : aprP.ToString("0.0"),
                            MayPoints = mayP == 0 ? "" : mayP.ToString("0.0"),
                            JunPoints = junP == 0 ? "" : junP.ToString("0.0"),
                            JulPoints = julP == 0 ? "" : julP.ToString("0.0"),
                            AugPoints = augP == 0 ? "" : augP.ToString("0.0"),
                            SepPoints = sepP == 0 ? "" : sepP.ToString("0.0"),
                        });
                    }
                    else if (isUpdateCells)
                    {
                        var cellWisePreviousData = forecastBLL.GetCellWiseUpdatePreviousData(item);
                        var cellWiseOriginalData = forecastBLL.GetCellWiseUpdateOriginalData(item, timeStampId);

                        //var employeeName_Cells = _approvalHistoryViewModal.EmployeeName;
                        var employeeName_Cells = cellWiseOriginalData.EmployeeName;

                        var approvedCells = cellWiseOriginalData.ApprovedCells;

                        var remarks_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            remarks_Cells = forecastBLL.GetApproveCellData(2, cellWisePreviousData.Remarks, cellWiseOriginalData.Remarks, approvedCells);
                        }
                        else
                        {
                            //remarks_Cells = "("+cellWisePreviousData.Remarks+")"+""+cellWiseOriginalData.Remarks;
                            remarks_Cells = cellWisePreviousData.Remarks == cellWiseOriginalData.Remarks ? "" : "(" + cellWisePreviousData.Remarks + ") " + cellWiseOriginalData.Remarks;
                        }

                        var sectionName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            sectionName_Cells = forecastBLL.GetApproveCellData(3, cellWisePreviousData.SectionName, cellWiseOriginalData.SectionName, approvedCells);
                        }
                        else
                        {
                            //sectionName_Cells = "(" + cellWisePreviousData.SectionName + ")" + "" + cellWiseOriginalData.SectionName;                            
                            sectionName_Cells = cellWisePreviousData.SectionName == cellWiseOriginalData.SectionName ? "" : "(" + cellWisePreviousData.SectionName + ") " + cellWiseOriginalData.SectionName;
                        }

                        var departmentName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            departmentName_Cells = forecastBLL.GetApproveCellData(4, cellWisePreviousData.DepartmentName, cellWiseOriginalData.DepartmentName, approvedCells);
                        }
                        else
                        {
                            //departmentName_Cells = "(" + cellWisePreviousData.DepartmentName + ")" + "" + cellWiseOriginalData.DepartmentName;
                            departmentName_Cells = cellWisePreviousData.DepartmentName == cellWiseOriginalData.DepartmentName ? "" : "(" + cellWisePreviousData.DepartmentName + ") " + cellWiseOriginalData.DepartmentName;
                        }

                        var inChargeName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            inChargeName_Cells = forecastBLL.GetApproveCellData(5, cellWisePreviousData.InChargeName, cellWiseOriginalData.InChargeName, approvedCells);
                        }
                        else
                        {
                            //inChargeName_Cells = "(" + cellWisePreviousData.InChargeName + ")" + "" + cellWiseOriginalData.InChargeName;
                            inChargeName_Cells = cellWisePreviousData.InChargeName == cellWiseOriginalData.InChargeName ? "" : "(" + cellWisePreviousData.InChargeName + ") " + cellWiseOriginalData.InChargeName;
                        }

                        var roleName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            roleName_Cells = forecastBLL.GetApproveCellData(6, cellWisePreviousData.RoleName, cellWiseOriginalData.RoleName, approvedCells);
                        }
                        else
                        {
                            //roleName_Cells = "(" + cellWisePreviousData.RoleName + ")" + "" + cellWiseOriginalData.RoleName;
                            roleName_Cells = cellWisePreviousData.RoleName == cellWiseOriginalData.RoleName ? "" : "(" + cellWisePreviousData.RoleName + ") " + cellWiseOriginalData.RoleName;
                        }

                        var explanationName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            explanationName_Cells = forecastBLL.GetApproveCellData(7, cellWisePreviousData.ExplanationName, cellWiseOriginalData.ExplanationName, approvedCells);
                        }
                        else
                        {
                            //explanationName_Cells = "(" + cellWisePreviousData.ExplanationName + ")" + "" + cellWiseOriginalData.ExplanationName;
                            explanationName_Cells = cellWisePreviousData.ExplanationName == cellWiseOriginalData.ExplanationName ? "" : "(" + cellWisePreviousData.ExplanationName + ") " + cellWiseOriginalData.ExplanationName;
                        }

                        var companyName_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            companyName_Cells = forecastBLL.GetApproveCellData(8, cellWisePreviousData.CompanyName, cellWiseOriginalData.CompanyName, approvedCells);
                        }
                        else
                        {
                            //companyName_Cells = "(" + cellWisePreviousData.CompanyName + ")" + "" + cellWiseOriginalData.CompanyName;
                            companyName_Cells = cellWisePreviousData.CompanyName == cellWiseOriginalData.CompanyName ? "" : "(" + cellWisePreviousData.CompanyName + ") " + cellWiseOriginalData.CompanyName;
                        }

                        var gradePoints_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            gradePoints_Cells = forecastBLL.GetApproveCellData(9, cellWisePreviousData.GradePoints, cellWiseOriginalData.GradePoints, approvedCells);
                        }
                        else
                        {
                            //gradePoints_Cells = "(" + cellWisePreviousData.GradePoints + ")" + "" + cellWiseOriginalData.GradePoints;
                            gradePoints_Cells = cellWisePreviousData.GradePoints == cellWiseOriginalData.GradePoints ? "" : "(" + cellWisePreviousData.GradePoints + ") " + cellWiseOriginalData.GradePoints;
                        }

                        var unitPrice_Cells = "";
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            unitPrice_Cells = forecastBLL.GetApproveCellData(10, cellWisePreviousData.UnitPrice, cellWiseOriginalData.UnitPrice, approvedCells);
                        }
                        else
                        {
                            //unitPrice_Cells = "(" + cellWisePreviousData.UnitPrice + ")" + "" + cellWiseOriginalData.UnitPrice;
                            unitPrice_Cells = cellWisePreviousData.UnitPrice == cellWiseOriginalData.UnitPrice ? "" : "(" + cellWisePreviousData.UnitPrice + ") " + cellWiseOriginalData.UnitPrice;
                        }

                        var _previousManMonthForecast = forecastBLL.GetPreviousManMonth(cellWisePreviousData.MonthId_Points);
                        var _originalManMonthForecast = forecastBLL.GetPreviousManMonth(cellWiseOriginalData.MonthId_Points);

                        var oct_Cell = "";
                        var octPrevious = _previousManMonthForecast.Where(p => p.Month == 10).SingleOrDefault().Points;
                        var octOrg = _originalManMonthForecast.Where(p => p.Month == 10).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            oct_Cell = forecastBLL.GetApproveForecastCellData(16, octPrevious, octOrg, approvedCells);
                        }
                        else
                        {
                            oct_Cell = octPrevious == octOrg ? "" : "(" + octPrevious.ToString("0.0") + ") " + octOrg.ToString("0.0");
                        }

                        var nov_Cell = "";
                        var novPrevious = _previousManMonthForecast.Where(p => p.Month == 11).SingleOrDefault().Points;
                        var novOrg = _originalManMonthForecast.Where(p => p.Month == 11).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            nov_Cell = forecastBLL.GetApproveForecastCellData(17, novPrevious, novOrg, approvedCells);
                        }
                        else
                        {
                            nov_Cell = novPrevious == novOrg ? "" : "(" + novPrevious.ToString("0.0") + ") " + novOrg.ToString("0.0");
                        }

                        var dec_Cell = "";
                        var decPrevious = _previousManMonthForecast.Where(p => p.Month == 12).SingleOrDefault().Points;
                        var decOrg = _originalManMonthForecast.Where(p => p.Month == 12).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            dec_Cell = forecastBLL.GetApproveForecastCellData(18, decPrevious, decOrg, approvedCells);
                        }
                        else
                        {
                            dec_Cell = decPrevious == decOrg ? "" : "(" + decPrevious.ToString("0.0") + ") " + decOrg.ToString("0.0");
                        }

                        var jan_Cell = "";
                        var janPrevious = _previousManMonthForecast.Where(p => p.Month == 1).SingleOrDefault().Points;
                        var janOrg = _originalManMonthForecast.Where(p => p.Month == 1).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            jan_Cell = forecastBLL.GetApproveForecastCellData(19, janPrevious, janOrg, approvedCells);
                        }
                        else
                        {
                            jan_Cell = janPrevious == janOrg ? "" : "(" + janPrevious.ToString("0.0") + ") " + janOrg.ToString("0.0");
                        }

                        var feb_Cell = "";
                        var febPrevious = _previousManMonthForecast.Where(p => p.Month == 2).SingleOrDefault().Points;
                        var febOrg = _originalManMonthForecast.Where(p => p.Month == 2).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            feb_Cell = forecastBLL.GetApproveForecastCellData(20, febPrevious, febOrg, approvedCells);
                        }
                        else
                        {
                            feb_Cell = febPrevious == febOrg ? "" : "(" + febPrevious.ToString("0.0") + ") " + febOrg.ToString("0.0");
                        }

                        var mar_Cell = "";
                        var marPrevious = _previousManMonthForecast.Where(p => p.Month == 3).SingleOrDefault().Points;
                        var marOrg = _originalManMonthForecast.Where(p => p.Month == 3).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            mar_Cell = forecastBLL.GetApproveForecastCellData(21, marPrevious, marOrg, approvedCells);
                        }
                        else
                        {
                            mar_Cell = marPrevious == marOrg ? "" : "(" + marPrevious.ToString("0.0") + ") " + marOrg.ToString("0.0");
                        }

                        var apr_Cell = "";
                        var aprPrevious = _previousManMonthForecast.Where(p => p.Month == 4).SingleOrDefault().Points;
                        var aprOrg = _originalManMonthForecast.Where(p => p.Month == 4).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            apr_Cell = forecastBLL.GetApproveForecastCellData(22, aprPrevious, aprOrg, approvedCells);
                        }
                        else
                        {
                            apr_Cell = aprPrevious == aprOrg ? "" : "(" + aprPrevious.ToString("0.0") + ") " + aprOrg.ToString("0.0");
                        }

                        var may_Cell = "";
                        var mayPrevious = _previousManMonthForecast.Where(p => p.Month == 5).SingleOrDefault().Points;
                        var mayOrg = _originalManMonthForecast.Where(p => p.Month == 5).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            may_Cell = forecastBLL.GetApproveForecastCellData(23, mayPrevious, mayOrg, approvedCells);
                        }
                        else
                        {
                            may_Cell = mayPrevious == mayOrg ? "" : "(" + mayPrevious.ToString("0.0") + ") " + mayOrg.ToString("0.0");
                        }

                        var jun_Cell = "";
                        var junPrevious = _previousManMonthForecast.Where(p => p.Month == 6).SingleOrDefault().Points;
                        var junOrg = _originalManMonthForecast.Where(p => p.Month == 6).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            jun_Cell = forecastBLL.GetApproveForecastCellData(24, junPrevious, junOrg, approvedCells);
                        }
                        else
                        {
                            jun_Cell = junPrevious == junOrg ? "" : "(" + junPrevious.ToString("0.0") + ") " + junOrg.ToString("0.0");
                        }

                        var jul_Cell = "";
                        var julPrevious = _previousManMonthForecast.Where(p => p.Month == 7).SingleOrDefault().Points;
                        var julOrg = _originalManMonthForecast.Where(p => p.Month == 7).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            jul_Cell = forecastBLL.GetApproveForecastCellData(25, julPrevious, julOrg, approvedCells);
                        }
                        else
                        {
                            jul_Cell = julPrevious == julOrg ? "" : "(" + julPrevious.ToString("0.0") + ") " + julOrg.ToString("0.0");
                        }

                        var aug_Cell = "";
                        var augPrevious = _previousManMonthForecast.Where(p => p.Month == 8).SingleOrDefault().Points;
                        var augOrg = _originalManMonthForecast.Where(p => p.Month == 8).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            aug_Cell = forecastBLL.GetApproveForecastCellData(26, augPrevious, augOrg, approvedCells);
                        }
                        else
                        {
                            aug_Cell = augPrevious == augOrg ? "" : "(" + augPrevious.ToString("0.0") + ") " + augOrg.ToString("0.0");
                        }

                        var sep_Cell = "";
                        var sepPrevious = _previousManMonthForecast.Where(p => p.Month == 9).SingleOrDefault().Points;
                        var sepOrg = _originalManMonthForecast.Where(p => p.Month == 9).SingleOrDefault().Points;
                        if (!string.IsNullOrEmpty(approvedCells))
                        {
                            sep_Cell = forecastBLL.GetApproveForecastCellData(27, sepPrevious, sepOrg, approvedCells);
                        }
                        else
                        {
                            sep_Cell = sepPrevious == sepOrg ? "" : "(" + sepPrevious.ToString("0.0") + ") " + sepOrg.ToString("0.0");
                        }
                        //employeeName_Cells,remarks_Cells,sectionName_Cells,departmentName_Cells,inChargeName_Cells,roleName_Cells,explanationName_Cells,companyName_Cells,gradePoints_Cells
                        //unitPrice_Cells
                        //oct_Cell,nov_Cell,dec_Cell,jan_Cell,feb_Cell,mar_Cell,apr_Cell,may_Cell,jun_Cell
                        //jul_Cell,aug_Cell,sep_Cell    
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName_Cells,
                            Remarks = remarks_Cells,
                            SectionName = sectionName_Cells,
                            DepartmentName = departmentName_Cells,
                            InChargeName = inChargeName_Cells,
                            RoleName = roleName_Cells,
                            ExplanationName = explanationName_Cells,
                            CompanyName = companyName_Cells,
                            GradePoints = gradePoints_Cells,
                            UnitPrice = unitPrice_Cells,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = "更新 (Cell Update)",
                            IsUpdate = isUpdate,
                            OctPoints = oct_Cell,
                            NovPoints = nov_Cell,
                            DecPoints = dec_Cell,
                            JanPoints = jan_Cell,
                            FebPoints = feb_Cell,
                            MarPoints = mar_Cell,
                            AprPoints = apr_Cell,
                            MayPoints = may_Cell,
                            JunPoints = jun_Cell,
                            JulPoints = jul_Cell,
                            AugPoints = aug_Cell,
                            SepPoints = sep_Cell,
                        });
                    }
                }
            }

            //string strHistoryDetailsWithHtmlBody = "";
            //foreach (var historyItem in forecastHistoryList)
            //{
            //    var isUpdate = historyItem.IsUpdate;
            //}
            //foreach (var forecastItem in forecastHistoryList)
            //{
            //    //if (forecastItem.)
            //    //{
            //    //    forecastType = "Updated";
            //    //}
            //    //else
            //    //{
            //    //    forecastItem = "Inserted";
            //    //}
            //    //if (string.IsNullOrEmpty(strHistoryDetailsWithHtmlBody))
            //    //{
            //    //    strHistoryDetailsWithHtmlBody = "<tr><td></td>>";
            //    //}
            //    //else
            //    //{
            //    //    strHistoryDetailsWithHtmlBody
            //    //}                
            //}
            return Ok(forecastHistoryList);
        }

        [HttpGet]
        [Route("api/utilities/GetTimeWiseChanges/")]
        public IHttpActionResult GetTimeWiseChanges()
        {
            var session = System.Web.HttpContext.Current.Session;
            User user = userBLL.GetUserLog(session["userName"].ToString());

            return Ok();
        }

        [HttpGet]
        [Route("api/utilities/GetYearFromHistory/")]
        public IHttpActionResult GetYearFromHistory()
        {
            var years = forecastBLL.GetYearFromHistory();
            return Ok(years);
        }
        [HttpGet]
        [Route("api/utilities/GetAssignmentYearList/")]
        public IHttpActionResult GetAssignmentYearList()
        {
            var years = forecastBLL.GetAssignmentYearList();
            return Ok(years);
        }
        [HttpGet]
        [Route("api/utilities/GetApprovalAssignmentYearList/")]
        public IHttpActionResult GetApprovalAssignmentYearList()
        {
            var years = forecastBLL.GetApprovalAssignmentYearList();
            return Ok(years);
        }
        [HttpGet]
        [Route("api/utilities/GetAssignmentsByYear/")]
        public IHttpActionResult GetAssignmentsByYear(int year)
        {
            List<ActualCostViewModel> actualCostViewModels = new List<ActualCostViewModel>();

            List<EmployeeAssignmentViewModel> employeeAssignments = employeeAssignmentBLL.GetAssignmentsByYear(year);

            List<ActualCost> actualCosts = actualCostBLL.GetActualCostsByYear(year);

            if (actualCosts.Count > 0)
            {
                foreach (var item in employeeAssignments)
                {
                    var actualCost = actualCosts.Where(ac => ac.AssignmentId == item.Id).SingleOrDefault();
                    if (actualCost == null)
                    {
                        actualCostViewModels.Add(MergeAssignmentWithActualCost(item, null));
                    }
                    else
                    {
                        actualCostViewModels.Add(MergeAssignmentWithActualCost(item, actualCost));
                    }
                }
            }
            else
            {
                foreach (var item in employeeAssignments)
                {
                    actualCostViewModels.Add(MergeAssignmentWithActualCost(item, null));

                }
            }

            var totalCount = actualCostViewModels.Count;

            actualCostViewModels.Add(new ActualCostViewModel
            {
                AssignmentId = 0,
                OctCost = $@"=SUM(J{1}:J{totalCount})",
                NovCost = $@"=SUM(K{1}:K{totalCount})",
                DecCost = $@"=SUM(L{1}:L{totalCount})",
                JanCost = $@"=SUM(M{1}:M{totalCount})",
                FebCost = $@"=SUM(N{1}:N{totalCount})",
                MarCost = $@"=SUM(O{1}:O{totalCount})",
                AprCost = $@"=SUM(P{1}:P{totalCount})",
                MayCost = $@"=SUM(Q{1}:Q{totalCount})",
                JunCost = $@"=SUM(R{1}:R{totalCount})",
                JulCost = $@"=SUM(S{1}:S{totalCount})",
                AugCost = $@"=SUM(T{1}:T{totalCount})",
                SepCost = $@"=SUM(U{1}:U{totalCount})",
            });
            return Ok(actualCostViewModels);
        }

        [HttpGet]
        [Route("api/utilities/GetActualCostConfirmData/")]
        public IHttpActionResult GetActualCostConfirmData(int year, int monthId)
        {
            List<ActualCostViewModel> actualCostViewModels = new List<ActualCostViewModel>();

            //List<EmployeeAssignmentViewModel> employeeAssignments = employeeAssignmentBLL.GetAssignmentsByYear(year);

            List<EmployeeAssignmentViewModel> employeeAssignments = employeeAssignmentBLL.GetSpecificAssignmentDataData(year, monthId);
            foreach (var item in employeeAssignments)
            {
                item.ForecastedTotal = item.ForecastedPoints * Convert.ToDecimal(item.UnitPrice);
                var actualCostList = actualCostBLL.GetActualCostsByYear_AssignmentId(year, item.Id);
                if (actualCostList.Count > 0)
                {
                    if (monthId == 10)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].OctPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].OctCost);
                    }
                    if (monthId == 11)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].NovPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].NovCost);
                    }
                    if (monthId == 12)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].DecPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].DecCost);
                    }
                    if (monthId == 1)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].JanPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].JanCost);
                    }
                    if (monthId == 2)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].FebPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].FebCost);
                    }
                    if (monthId == 3)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].MarPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].MarCost);
                    }
                    if (monthId == 4)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].AprPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].AprCost);
                    }
                    if (monthId == 5)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].MayPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].MayCost);
                    }
                    if (monthId == 6)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].JunPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].JunCost);
                    }
                    if (monthId == 7)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].JulPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].JulCost);
                    }
                    if (monthId == 8)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].AugPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].AugCost);
                    }
                    if (monthId == 9)
                    {
                        item.ManMonth = Convert.ToDecimal(actualCostList[0].SepPoint);
                        item.ActualCostAmount = Convert.ToDecimal(actualCostList[0].SepCost);
                    }

                }

            }
            return Ok(employeeAssignments);
        }

        [HttpPost]
        [Route("api/utilities/CreateActualCost/")]
        public IHttpActionResult CreateActualCost(ActualCostDto actualCostDto)
        {
            string costColumnName = "";
            string pointColumnName = "";
            var session = System.Web.HttpContext.Current.Session;
            if (actualCostDto.ActualCosts.Count > 0)
            {
                foreach (var item in actualCostDto.ActualCosts)
                {
                    if (item.AssignmentId == 0)
                    {
                        continue;
                    }

                    var flag = actualCostBLL.CheckAssignmentId(item.AssignmentId, actualCostDto.Year);
                    if (flag)
                    {
                        if (actualCostDto.Month == 10)
                        {
                            costColumnName = "OctCost";
                            pointColumnName = "OctPoint";
                        }
                        if (actualCostDto.Month == 11)
                        {
                            costColumnName = "NovCost";
                            pointColumnName = "NovPoint";
                        }
                        if (actualCostDto.Month == 12)
                        {
                            costColumnName = "DecCost";
                            pointColumnName = "DecPoint";
                        }
                        if (actualCostDto.Month == 1)
                        {
                            costColumnName = "JanCost";
                            pointColumnName = "JanPoint";
                        }
                        if (actualCostDto.Month == 2)
                        {
                            costColumnName = "FebCost";
                            pointColumnName = "FebPoint";
                        }
                        if (actualCostDto.Month == 3)
                        {
                            costColumnName = "MarCost";
                            pointColumnName = "MarPoint";
                        }
                        if (actualCostDto.Month == 4)
                        {
                            costColumnName = "AprCost";
                            pointColumnName = "AprPoint";
                        }
                        if (actualCostDto.Month == 5)
                        {
                            costColumnName = "MayCost";
                            pointColumnName = "MayPoint";
                        }
                        if (actualCostDto.Month == 6)
                        {
                            costColumnName = "JunCost";
                            pointColumnName = "JunPoint";
                        }
                        if (actualCostDto.Month == 7)
                        {
                            costColumnName = "JulCost";
                            pointColumnName = "JulPoint";
                        }
                        if (actualCostDto.Month == 8)
                        {
                            costColumnName = "AugCost";
                            pointColumnName = "AugPoint";
                        }
                        if (actualCostDto.Month == 9)
                        {
                            costColumnName = "SepCost";
                            pointColumnName = "SepPoint";
                        }
                        item.UpdatedBy = session["userName"].ToString();
                        item.UpdatedDate = DateTime.Now;
                        actualCostBLL.UpdateActualCost(actualCostDto.Year, item.AssignmentId, costColumnName, pointColumnName, item.ActualCostAmount, item.ManHour, item.UpdatedBy, item.UpdatedDate);
                    }
                    else
                    {
                        item.Year = actualCostDto.Year;
                        item.CreatedBy = session["userName"].ToString();
                        item.CreatedDate = DateTime.Now;
                        if (actualCostDto.Month == 10)
                        {
                            item.OctCost = item.ActualCostAmount;
                            item.OctPoint = item.ManHour;
                        }
                        if (actualCostDto.Month == 11)
                        {
                            item.NovCost = item.ActualCostAmount;
                            item.NovCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 12)
                        {
                            item.DecCost = item.ActualCostAmount;
                            item.DecCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 1)
                        {
                            item.JanCost = item.ActualCostAmount;
                            item.JanCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 2)
                        {
                            item.FebCost = item.ActualCostAmount;
                            item.FebCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 3)
                        {
                            item.MarCost = item.ActualCostAmount;
                            item.MarCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 4)
                        {
                            item.AprCost = item.ActualCostAmount;
                            item.AprCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 5)
                        {
                            item.MayCost = item.ActualCostAmount;
                            item.MayCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 6)
                        {
                            item.JunCost = item.ActualCostAmount;
                            item.JunCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 7)
                        {
                            item.JulCost = item.ActualCostAmount;
                            item.JulCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 8)
                        {
                            item.AugCost = item.ActualCostAmount;
                            item.AugCost = item.ManHour;
                        }
                        if (actualCostDto.Month == 9)
                        {
                            item.SepCost = item.ActualCostAmount;
                            item.SepCost = item.ManHour;
                        }
                        actualCostBLL.CreateActualCost(item);
                    }
                }

                //foreach (var item in actualCostDto.ActualCosts)
                //{
                //    if (item.AssignmentId == 0)
                //    {
                //        continue;
                //    }
                //    List<Forecast> forecasts =  forecastBLL.GetForecastDetails(item.AssignmentId,actualCostDto.Year);

                //    Sukey sukey = new Sukey();
                //    sukey.AssignmentId = item.AssignmentId;
                //    sukey.Year = item.Year;
                //    // assign data in sukey object.
                //    {


                //        if (actualCostDto.OctFlag)
                //        {
                //            sukey.OctCost = item.OctCost;
                //        }
                //        else
                //        {
                //            sukey.OctCost = Convert.ToDouble(forecasts.Where(f => f.Month == 10).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.NovFlag)
                //        {
                //            sukey.NovCost = item.NovCost;
                //        }
                //        else
                //        {
                //            sukey.NovCost = Convert.ToDouble(forecasts.Where(f => f.Month == 11).SingleOrDefault().Total);
                //        }

                //        if (actualCostDto.DecFlag)
                //        {
                //            sukey.DecCost = item.DecCost;
                //        }
                //        else
                //        {
                //            sukey.DecCost = Convert.ToDouble(forecasts.Where(f => f.Month == 12).SingleOrDefault().Total);
                //        }

                //        if (actualCostDto.JanFlag)
                //        {
                //            sukey.JanCost = item.JanCost;
                //        }
                //        else
                //        {
                //            sukey.JanCost = Convert.ToDouble(forecasts.Where(f => f.Month == 1).SingleOrDefault().Total);
                //        }

                //        if (actualCostDto.FebFlag)
                //        {
                //            sukey.FebCost = item.FebCost;
                //        }
                //        else
                //        {
                //            sukey.FebCost = Convert.ToDouble(forecasts.Where(f => f.Month == 2).SingleOrDefault().Total);
                //        }

                //        if (actualCostDto.MarFlag)
                //        {
                //            sukey.MarCost = item.MarCost;
                //        }
                //        else
                //        {
                //            sukey.MarCost = Convert.ToDouble(forecasts.Where(f => f.Month == 3).SingleOrDefault().Total);
                //        }

                //        if (actualCostDto.AprFlag)
                //        {
                //            sukey.AprCost = item.AprCost;
                //        }
                //        else
                //        {
                //            sukey.AprCost = Convert.ToDouble(forecasts.Where(f => f.Month == 4).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.MayFlag)
                //        {
                //            sukey.MayCost = item.MayCost;
                //        }
                //        else
                //        {
                //            sukey.MayCost = Convert.ToDouble(forecasts.Where(f => f.Month == 5).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.JunFlag)
                //        {
                //            sukey.JunCost = item.JunCost;
                //        }
                //        else
                //        {
                //            sukey.JunCost = Convert.ToDouble(forecasts.Where(f => f.Month == 6).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.JulFlag)
                //        {
                //            sukey.JulCost = item.JulCost;
                //        }
                //        else
                //        {
                //            sukey.JulCost = Convert.ToDouble(forecasts.Where(f => f.Month == 7).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.AugFlag)
                //        {
                //            sukey.AugCost = item.AugCost;
                //        }
                //        else
                //        {
                //            sukey.AugCost = Convert.ToDouble(forecasts.Where(f => f.Month == 8).SingleOrDefault().Total);
                //        }
                //        if (actualCostDto.SepFlag)
                //        {
                //            sukey.SepCost = item.SepCost;
                //        }
                //        else
                //        {
                //            sukey.SepCost = Convert.ToDouble(forecasts.Where(f => f.Month == 9).SingleOrDefault().Total);
                //        }
                //    }

                //    var sukeyFlag = actualCostBLL.CheckSukeyAssignmentId(sukey.AssignmentId,sukey.Year);
                //    if (sukeyFlag)
                //    {
                //        sukey.UpdatedBy = session["userName"].ToString();
                //        sukey.UpdatedDate = DateTime.Now;
                //        actualCostBLL.UpdateSukey(sukey);
                //    }
                //    else
                //    {
                //        sukey.CreatedBy = session["userName"].ToString();
                //        sukey.CreatedDate = DateTime.Now;
                //        actualCostBLL.CreateSukey(sukey);
                //    }
                //}
                return Ok("Operation Completed.");
            }
            else
            {
                return NotFound();
            }

        }


        public ActualCostViewModel MergeAssignmentWithActualCost(EmployeeAssignmentViewModel employeeAssignment, ActualCost actualCost)
        {
            ActualCostViewModel actualCostViewModel = new ActualCostViewModel();
            actualCostViewModel.EmployeeName = employeeAssignment.EmployeeName;
            actualCostViewModel.AssignmentId = employeeAssignment.Id;
            actualCostViewModel.SectionId = employeeAssignment.SectionId;
            actualCostViewModel.DepartmentId = employeeAssignment.DepartmentId;
            actualCostViewModel.InchargeId = employeeAssignment.InchargeId;
            actualCostViewModel.RoleId = employeeAssignment.RoleId;
            actualCostViewModel.ExplanationId = employeeAssignment.ExplanationId;
            actualCostViewModel.CompanyId = employeeAssignment.CompanyId;
            actualCostViewModel.Remarks = employeeAssignment.Remarks;

            if (actualCost != null)
            {
                actualCostViewModel.OctCost = actualCost.OctCost == 0 ? "" : actualCost.OctCost.ToString();
                actualCostViewModel.NovCost = actualCost.NovCost == 0 ? "" : actualCost.NovCost.ToString();
                actualCostViewModel.DecCost = actualCost.DecCost == 0 ? "" : actualCost.DecCost.ToString();
                actualCostViewModel.JanCost = actualCost.JanCost == 0 ? "" : actualCost.JanCost.ToString();
                actualCostViewModel.FebCost = actualCost.FebCost == 0 ? "" : actualCost.FebCost.ToString();
                actualCostViewModel.MarCost = actualCost.MarCost == 0 ? "" : actualCost.MarCost.ToString();
                actualCostViewModel.AprCost = actualCost.AprCost == 0 ? "" : actualCost.AprCost.ToString();
                actualCostViewModel.MayCost = actualCost.MayCost == 0 ? "" : actualCost.MayCost.ToString();
                actualCostViewModel.JunCost = actualCost.JunCost == 0 ? "" : actualCost.JunCost.ToString();
                actualCostViewModel.JulCost = actualCost.JulCost == 0 ? "" : actualCost.JulCost.ToString();
                actualCostViewModel.AugCost = actualCost.AugCost == 0 ? "" : actualCost.AugCost.ToString();
                actualCostViewModel.SepCost = actualCost.SepCost == 0 ? "" : actualCost.SepCost.ToString();
            }
            else
            {
                actualCostViewModel.OctCost = "";
                actualCostViewModel.NovCost = "";
                actualCostViewModel.DecCost = "";
                actualCostViewModel.JanCost = "";
                actualCostViewModel.FebCost = "";
                actualCostViewModel.MarCost = "";
                actualCostViewModel.AprCost = "";
                actualCostViewModel.MayCost = "";
                actualCostViewModel.JunCost = "";
                actualCostViewModel.JulCost = "";
                actualCostViewModel.AugCost = "";
                actualCostViewModel.SepCost = "";
            }

            return actualCostViewModel;


        }

        [HttpGet]
        [Route("api/utilities/GetAllUserRoles/")]
        public IHttpActionResult GetAllUserRoles()
        {
            var roles = userRoleBLL.GetAllUserRoles();
            return Ok(roles);
        }

        [HttpGet]
        [Route("api/utilities/GetSukeyData/")]
        public IHttpActionResult GetSukeyData(int year)
        {
            List<SukeyDto> sukeys = actualCostBLL.GetAllSukeyData(year);
            return Ok(sukeys);
        }

        [HttpGet]
        [Route("api/utilities/CreateApportionment/")]
        public IHttpActionResult CreateApportionment(int year)
        {
            // pull distinct department Ids...
            var distinctDepartmentIds = new List<string>();
            // pull All Sukeys Data....
            var sukeyList = actualCostBLL.GetAllSukeyData(year);

            foreach (var item in sukeyList)
            {
                if (!String.IsNullOrEmpty(item.DepartmentId))
                {
                    if (!distinctDepartmentIds.Contains(item.DepartmentId))
                    {
                        if (item.DepartmentId == "8")
                        {
                            continue;
                        }
                        distinctDepartmentIds.Add(item.DepartmentId);
                    }
                }
            }

            List<object> apportionmentList = new List<object>();

            var apportionments = actualCostBLL.GetAllApportionmentData(year);
            //var apportionmentsC = 0;
            if (apportionments.Count > 0)
            {
                var count = 1;
                foreach (var item in apportionments)
                {
                    var department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item.DepartmentId));
                    apportionmentList.Add(new
                    {
                        DepartmentId = department.Id,//a
                        DepartmentName = department.DepartmentName, //b

                        //OctActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().OctCost,//c
                        //OctPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().OctPercentage,//d
                        //OctResult = $@"=C{count}+((D{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().OctCost})",//e
                        OctPercentage = item.OctPercentage,
                        NovPercentage = item.NovPercentage,
                        DecPercentage = item.DecPercentage,
                        JanPercentage = item.JanPercentage,
                        FebPercentage = item.FebPercentage,
                        MarPercentage = item.MarPercentage,
                        AprPercentage = item.AprPercentage,
                        MayPercentage = item.MayPercentage,
                        JunPercentage = item.JunPercentage,
                        JulPercentage = item.JulPercentage,
                        AugPercentage = item.AugPercentage,
                        SepPercentage = item.SepPercentage,
                        Id = item.Id,

                        //NovActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().NovCost,//f
                        //NovPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().NovPercentage,//g
                        //NovResult = $@"=F{count}+(G{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().NovCost}",//h

                        //DecActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().DecCost,//i
                        //DecPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().DecPercentage,//j
                        // DecResult = $@"=I{count}+(J{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().DecCost}",//k

                        //JanActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JanCost,//l
                        //JanPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JanPercentage,//m
                        //JanResult = $@"=L{count}+(M{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JanCost}",//n

                        //FebActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().FebCost,//o
                        // FebPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().FebPercentage,//p
                        //FebResult = $@"=O{count}+(P{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().FebCost}",//q

                        //MarActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MarCost,//r
                        //MarPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().MarPercentage,//s
                        //MarResult = $@"=R{count}+(S{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MarCost}",//t

                        //AprActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AprCost,//u
                        //AprPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().AprPercentage,//v
                        //AprResult = $@"=U{count}+(V{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AprCost}",//w

                        //MayActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MayCost,//x
                        //MayPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().MayPercentage,//y
                        //MayResult = $@"=X{count}+(Y{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MayCost}",//z

                        //JunActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JunCost,//aa
                        //JunPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JunPercentage,//ab
                        //JunResult = $@"=AA{count}+(AB{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JunCost}",//ac

                        //JulActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JulCost,//ad
                        //JulPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JulPercentage,//ae
                        //JulResult = $@"=AD{count}+(AE{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JulCost}",//af

                        //AugActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AugCost,//ag
                        //AugPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().AugPercentage,//ah
                        // AugResult = $@"=AG{count}+(AH{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AugCost}",//ai

                        //SepActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().SepCost,//aj
                        //SepPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().SepPercentage,//ak
                        //SepResult = $@"=AJ{count}+(AK{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().SepCost}",//al
                    });
                    count++;
                }
            }
            //else
            //{
            //    var count = 1;
            //    foreach (var item in distinctDepartmentIds)
            //    {
            //        var department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item));
            //        apportionmentList.Add(new {
            //            DepartmentId= department.Id,//a
            //            DepartmentName = department.DepartmentName, //b

            //            //OctActualCost= sukeyList.Where(s=>s.DepartmentId== item).FirstOrDefault().OctCost,//c
            //            OctPercentage = 0,//d
            //            //OctResult=$@"=C{count}+((D{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().OctCost})",//e

            //            //NovActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().NovCost,//f
            //            NovPercentage = 0,//g
            //            //NovResult = $@"=F{count}+(G{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().NovCost}",//h

            //            //DecActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().DecCost,//i
            //            DecPercentage = 0,//j
            //            //DecResult = $@"=I{count}+(J{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().DecCost}",//k

            //            //JanActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JanCost,//l
            //            JanPercentage = 0,//m
            //            //JanResult = $@"=L{count}+(M{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JanCost}",//n

            //            //FebActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().FebCost,//o
            //            FebPercentage = 0,//p
            //            //FebResult = $@"=O{count}+(P{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().FebCost}",//q

            //            //MarActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MarCost,//r
            //            MarPercentage = 0,//s
            //            //MarResult = $@"=R{count}+(S{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MarCost}",//t

            //            //AprActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AprCost,//u
            //            AprPercentage = 0,//v
            //            //AprResult = $@"=U{count}+(V{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AprCost}",//w

            //            //MayActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MayCost,//x
            //            MayPercentage = 0,//y
            //            //MayResult = $@"=X{count}+(Y{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MayCost}",//z

            //            //JunActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JunCost,//aa
            //            JunPercentage = 0,//ab
            //            //JunResult = $@"=AA{count}+(AB{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JunCost}",//ac

            //            //JulActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JulCost,//ad
            //            JulPercentage = 0,//ae
            //            //JulResult = $@"=AD{count}+(AE{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JulCost}",//af

            //            //AugActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AugCost,//ag
            //            AugPercentage = 0,//ah
            //            //AugResult = $@"=AG{count}+(AH{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AugCost}",//ai

            //            //SepActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().SepCost,//aj
            //            SepPercentage = 0,//ak
            //            //SepResult = $@"=AJ{count}+(AK{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().SepCost}",//al
            //        });
            //        count++;
            //    }
            //}


            return Ok(apportionmentList);
        }


        [HttpPost]
        [Route("api/utilities/CreateApportionment/")]
        public IHttpActionResult CreateApportionment(ApportionmentDto apportionment)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (apportionment.Apportionments.Count > 0)
            {
                foreach (var item in apportionment.Apportionments)
                {
                    var flag = actualCostBLL.CheckApportionment(item.DepartmentId, apportionment.Year);
                    if (flag)
                    {
                        item.UpdatedBy = session["userName"].ToString();
                        item.UpdatedDate = DateTime.Now;
                        item.Year = apportionment.Year;
                        actualCostBLL.UpdateApportionment(item);
                    }
                    else
                    {
                        item.CreatedBy = session["userName"].ToString();
                        item.CreatedDate = DateTime.Now;
                        item.Year = apportionment.Year;
                        actualCostBLL.CreateApportionment(item);
                    }
                }
            }
            return Ok("Operation Completed!");
        }

        [HttpGet]
        [Route("api/utilities/GetSukeyWithQA/")]
        public IHttpActionResult GetSukeyWithQA(int year = 2023)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();

            List<SukeyDto> sukeys = actualCostBLL.GetAllSukeyData(year);

            //var sukeyList = actualCostBLL.GetAllSukeyData(year);

            var singleHinsho = sukeys.Where(s => s.DepartmentId == "8").FirstOrDefault();

            var apportionmentList = actualCostBLL.GetAllApportionmentData(year);
            if (apportionmentList.Count > 0)
            {
                foreach (var item in sukeys)
                {
                    if (item.DepartmentId == "8")
                    {
                        continue;
                    }
                    SukeyQADto sukeyQADto = new SukeyQADto();
                    sukeyQADto.DepartmentName = item.DepartmentName;
                    var _octP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().OctPercentage;
                    sukeyQADto.OctCost.Add(item.OctCost);
                    sukeyQADto.OctCost.Add((_octP / 100) * singleHinsho.OctCost);
                    sukeyQADto.OctCost.Add(item.OctCost + ((_octP / 100) * singleHinsho.OctCost));

                    var _novP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().NovPercentage;
                    sukeyQADto.NovCost.Add(item.NovCost);
                    sukeyQADto.NovCost.Add((_novP / 100) * singleHinsho.NovCost);
                    sukeyQADto.NovCost.Add(item.NovCost + ((_novP / 100) * singleHinsho.NovCost));

                    var _decP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().DecPercentage;
                    sukeyQADto.DecCost.Add(item.DecCost);
                    sukeyQADto.DecCost.Add((_decP / 100) * singleHinsho.DecCost);
                    sukeyQADto.DecCost.Add(item.NovCost + ((_decP / 100) * singleHinsho.DecCost));

                    var _janP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().DecPercentage;
                    sukeyQADto.JanCost.Add(item.JanCost);
                    sukeyQADto.JanCost.Add((_janP / 100) * singleHinsho.JanCost);
                    sukeyQADto.JanCost.Add(item.JanCost + ((_janP / 100) * singleHinsho.JanCost));

                    var _febP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().FebPercentage;
                    sukeyQADto.FebCost.Add(item.FebCost);
                    sukeyQADto.FebCost.Add((_febP / 100) * singleHinsho.FebCost);
                    sukeyQADto.FebCost.Add(item.FebCost + ((_febP / 100) * singleHinsho.FebCost));

                    var _marP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().MarPercentage;
                    sukeyQADto.MarCost.Add(item.MarCost);
                    sukeyQADto.MarCost.Add((_marP / 100) * singleHinsho.MarCost);
                    sukeyQADto.MarCost.Add(item.MarCost + ((_marP / 100) * singleHinsho.MarCost));

                    var _aprP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().AprPercentage;
                    sukeyQADto.AprCost.Add(item.AprCost);
                    sukeyQADto.AprCost.Add((_aprP / 100) * singleHinsho.AprCost);
                    sukeyQADto.AprCost.Add(item.AprCost + ((_aprP / 100) * singleHinsho.AprCost));

                    var _mayP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().AprPercentage;
                    sukeyQADto.MayCost.Add(item.MayCost);
                    sukeyQADto.MayCost.Add((_mayP / 100) * singleHinsho.MayCost);
                    sukeyQADto.MayCost.Add(item.MayCost + ((_mayP / 100) * singleHinsho.MayCost));

                    var _junP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().JunPercentage;
                    sukeyQADto.JunCost.Add(item.JunCost);
                    sukeyQADto.JunCost.Add((_junP / 100) * singleHinsho.JunCost);
                    sukeyQADto.JunCost.Add(item.JunCost + ((_junP / 100) * singleHinsho.JunCost));

                    var _julP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().JulPercentage;
                    sukeyQADto.JulCost.Add(item.JulCost);
                    sukeyQADto.JulCost.Add((_julP / 100) * singleHinsho.JulCost);
                    sukeyQADto.JulCost.Add(item.JulCost + ((_junP / 100) * singleHinsho.JulCost));

                    var _augP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().AugPercentage;
                    sukeyQADto.AugCost.Add(item.AugCost);
                    sukeyQADto.AugCost.Add((_augP / 100) * singleHinsho.AugCost);
                    sukeyQADto.AugCost.Add(item.AugCost + ((_augP / 100) * singleHinsho.AugCost));

                    var _sepP = apportionmentList.Where(ap => ap.DepartmentId.ToString() == item.DepartmentId).SingleOrDefault().SepPercentage;
                    sukeyQADto.SepCost.Add(item.SepCost);
                    sukeyQADto.SepCost.Add((_sepP / 100) * singleHinsho.SepCost);
                    sukeyQADto.SepCost.Add(item.SepCost + ((_sepP / 100) * singleHinsho.SepCost));

                    sukeyQADtos.Add(sukeyQADto);
                }
            }



            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/LatestFiscalYear/")]
        public IHttpActionResult LatestFiscalYear()
        {
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            return Ok(forecastLeatestYear);
        }

        [HttpGet]
        [Route("api/utilities/GetTotal/")]
        public IHttpActionResult GetTotal(string companiIds)
        {
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            List<Department> departments = departmentBLL.GetAllDepartments();
            Department qaDepartmentByName = departments.Where(d => d.DepartmentName == "品証").SingleOrDefault();
            if (qaDepartmentByName == null)
            {
                return NotFound();
            }
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                //if (department.Id==8)
                //{
                //    continue;
                //}
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    if (_octActualCostTotal > 0)
                    {
                        sukeyDto.OctCost.Add(_octActualCostTotal + _octCalculation);
                        rowTotal += _octActualCostTotal + _octCalculation;
                    }
                    else
                    {
                        sukeyDto.OctCost.Add(_octTotal + _octCalculation);
                        rowTotal += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    if (_novActualCostTotal > 0)
                    {
                        sukeyDto.NovCost.Add(_novActualCostTotal + _novCalculation);
                        rowTotal += _novActualCostTotal + _novCalculation;
                    }
                    else
                    {
                        sukeyDto.NovCost.Add(_novTotal + _novCalculation);
                        rowTotal += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    if (_decActualCostTotal > 0)
                    {
                        sukeyDto.DecCost.Add(_decActualCostTotal + _decCalculation);
                        rowTotal += _decActualCostTotal + _decCalculation;
                    }
                    else
                    {
                        sukeyDto.DecCost.Add(_decTotal + _decCalculation);
                        rowTotal += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    if (_janActualCostTotal > 0)
                    {
                        sukeyDto.JanCost.Add(_janActualCostTotal + _janCalculation);
                        rowTotal += _janActualCostTotal + _janCalculation;
                    }
                    else
                    {
                        sukeyDto.JanCost.Add(_janTotal + _janCalculation);
                        rowTotal += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    if (_febActualCostTotal > 0)
                    {
                        sukeyDto.FebCost.Add(_febActualCostTotal + _febCalculation);
                        rowTotal += _febActualCostTotal + _febCalculation;
                    }
                    else
                    {
                        sukeyDto.FebCost.Add(_febTotal + _febCalculation);
                        rowTotal += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    if (_marActualCostTotal > 0)
                    {
                        sukeyDto.MarCost.Add(_marActualCostTotal + _marCalculation);
                        rowTotal += _marActualCostTotal + _marCalculation;
                    }
                    else
                    {
                        sukeyDto.MarCost.Add(_marTotal + _marCalculation);
                        rowTotal += _marTotal + _marCalculation;
                    }
                    sukeyDto.FirstSlot.Add(rowTotal);
                    firstSlot = rowTotal;

                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    if (_aprActualCostTotal > 0)
                    {
                        sukeyDto.AprCost.Add(_aprActualCostTotal + _aprCalculation);
                        rowTotal += _aprActualCostTotal + _aprCalculation;
                    }
                    else
                    {
                        sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
                        rowTotal += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    if (_mayActualCostTotal > 0)
                    {
                        sukeyDto.MayCost.Add(_mayActualCostTotal + _mayCalculation);
                        rowTotal += _mayActualCostTotal + _mayCalculation;
                    }
                    else
                    {
                        sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
                        rowTotal += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    if (_junActualCostTotal > 0)
                    {
                        sukeyDto.JunCost.Add(_junActualCostTotal + _junCalculation);
                        rowTotal += _junActualCostTotal + _junCalculation;
                    }
                    else
                    {
                        sukeyDto.JunCost.Add(_junTotal + _junCalculation);
                        rowTotal += _junTotal + _junCalculation;
                    }
                    var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    if (_julActualCostTotal > 0)
                    {
                        sukeyDto.JulCost.Add(_julActualCostTotal + _julCalculation);
                        rowTotal += _julActualCostTotal + _julCalculation;
                    }
                    else
                    {
                        sukeyDto.JulCost.Add(_julTotal + _julCalculation);
                        rowTotal += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    if (_augActualCostTotal > 0)
                    {
                        sukeyDto.AugCost.Add(_augActualCostTotal + _augCalculation);
                        rowTotal += _augActualCostTotal + _augCalculation;
                    }
                    else
                    {
                        sukeyDto.AugCost.Add(_augTotal + _augCalculation);
                        rowTotal += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    if (_sepActualCostTotal > 0)
                    {
                        sukeyDto.SepCost.Add(_sepActualCostTotal + _sepCalculation);
                        rowTotal += _sepActualCostTotal + _sepCalculation;
                    }
                    else
                    {
                        sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
                        rowTotal += _sepTotal + _sepCalculation;
                    }
                    sukeyDto.RowTotal.Add(rowTotal);
                    sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

                }
                else
                {
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.RowTotal.Add(0);
                    sukeyDto.FirstSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                }



                sukeyQADtos.Add(sukeyDto);
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetTotalWithQA/")]
        public IHttpActionResult GetTotalWithQA(string companiIds, int departmentId)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            if (qaDepartmentByName == null)
            {
                return NotFound();
            }
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            List<Department> departments = departmentBLL.GetAllDepartments();
            foreach (var department in departments)
            {
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                if (department.Id == departmentId)
                {
                    var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == departmentId).SingleOrDefault();
                    if (apportionmentByDepartment == null)
                    {
                        apportionmentByDepartment = new Apportionment();
                    }

                    // update hinso variables by percentage.
                    {
                        _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                        _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                        _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                        _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                        _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                        _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                        _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                        _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                        _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                        _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                        _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                        _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    }


                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                    if (forecastAssignmentViewModels.Count > 0)
                    {
                        double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                        double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                        double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                        double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                        double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                        double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                        double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                        double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                        double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                        double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                        double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                        double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                        double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                        double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                        double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                        double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                        double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                        double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                        double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                        double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                        double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                        double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                        double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                        double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                        if (_octActualCostTotal > 0)
                        {
                            //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octActualCostTotal);
                            sukeyDto.OctCost.Add(Math.Round(_octHinsho, 2));
                            sukeyDto.OctCost.Add(_octActualCostTotal + _octHinsho);

                            rowTotalDept += _octActualCostTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octActualCostTotal + _octHinsho;
                        }
                        else
                        {
                            //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octTotal);
                            sukeyDto.OctCost.Add(Math.Round(_octHinsho, 2));
                            sukeyDto.OctCost.Add(_octTotal + _octHinsho);

                            rowTotalDept += _octTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octTotal + _octHinsho;
                        }
                        if (_novActualCostTotal > 0)
                        {
                            //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novActualCostTotal);
                            sukeyDto.NovCost.Add(Math.Round(_novHinsho, 2));
                            sukeyDto.NovCost.Add(_novActualCostTotal + _novHinsho);

                            rowTotalDept += _novActualCostTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novActualCostTotal + _novHinsho;
                        }
                        else
                        {
                            //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novTotal);
                            sukeyDto.NovCost.Add(Math.Round(_novHinsho, 2));
                            sukeyDto.NovCost.Add(_novTotal + _novHinsho);

                            rowTotalDept += _novTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novTotal + _novHinsho;
                        }
                        if (_decActualCostTotal > 0)
                        {
                            //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decActualCostTotal);
                            sukeyDto.DecCost.Add(Math.Round(_decHinsho, 2));
                            sukeyDto.DecCost.Add(_decActualCostTotal + _decHinsho);

                            rowTotalDept += _decActualCostTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decActualCostTotal + _decHinsho;
                        }
                        else
                        {
                            //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decTotal);
                            sukeyDto.DecCost.Add(Math.Round(_decHinsho, 2));
                            sukeyDto.DecCost.Add(_decTotal + _decHinsho);

                            rowTotalDept += _decTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decTotal + _decHinsho;
                        }
                        if (_janActualCostTotal > 0)
                        {
                            //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janActualCostTotal);
                            sukeyDto.JanCost.Add(Math.Round(_janHinsho, 2));
                            sukeyDto.JanCost.Add(_janActualCostTotal + _janHinsho);

                            rowTotalDept += _janActualCostTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janActualCostTotal + _janHinsho;
                        }
                        else
                        {
                            //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janTotal);
                            sukeyDto.JanCost.Add(Math.Round(_janHinsho, 2));
                            sukeyDto.JanCost.Add(_janTotal + _janHinsho);

                            rowTotalDept += _janTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janTotal + _janHinsho;
                        }
                        if (_febActualCostTotal > 0)
                        {
                            //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febActualCostTotal);
                            sukeyDto.FebCost.Add(Math.Round(_febHinsho, 2));
                            sukeyDto.FebCost.Add(_febActualCostTotal + _febHinsho);

                            rowTotalDept += _febActualCostTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febActualCostTotal + _febHinsho;
                        }
                        else
                        {
                            //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febTotal);
                            sukeyDto.FebCost.Add(Math.Round(_febHinsho, 2));
                            sukeyDto.FebCost.Add(_febTotal + _febHinsho);

                            rowTotalDept += _febTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febTotal + _febHinsho;
                        }
                        if (_marActualCostTotal > 0)
                        {
                            //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marActualCostTotal);
                            sukeyDto.MarCost.Add(Math.Round(_marHinsho, 2));
                            sukeyDto.MarCost.Add(_marActualCostTotal + _marHinsho);

                            rowTotalDept += _marActualCostTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marActualCostTotal + _marHinsho;
                        }
                        else
                        {
                            //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marTotal);
                            sukeyDto.MarCost.Add(Math.Round(_marHinsho, 2));
                            sukeyDto.MarCost.Add(_marTotal + _marHinsho);

                            rowTotalDept += _marTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marTotal + _marHinsho;
                        }

                        deptFirstSlot = rowTotalDept;
                        qaFirstSlot = rowTotalQa;
                        totalFirstSlot = rowTotal;


                        if (_aprActualCostTotal > 0)
                        {
                            //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprActualCostTotal);
                            sukeyDto.AprCost.Add(Math.Round(_aprHinsho, 2));
                            sukeyDto.AprCost.Add(_aprActualCostTotal + _aprHinsho);

                            rowTotalDept += _aprActualCostTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprActualCostTotal + _aprHinsho;
                        }
                        else
                        {
                            //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprTotal);
                            sukeyDto.AprCost.Add(Math.Round(_aprHinsho, 2));
                            sukeyDto.AprCost.Add(_aprTotal + _aprHinsho);


                            rowTotalDept += _aprTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprTotal + _aprHinsho;
                        }
                        if (_mayActualCostTotal > 0)
                        {
                            //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayActualCostTotal);
                            sukeyDto.MayCost.Add(Math.Round(_mayHinsho, 2));
                            sukeyDto.MayCost.Add(_mayActualCostTotal + _mayHinsho);

                            rowTotalDept += _mayActualCostTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayActualCostTotal + _mayHinsho;
                        }
                        else
                        {
                            //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayTotal);
                            sukeyDto.MayCost.Add(Math.Round(_mayHinsho, 2));
                            sukeyDto.MayCost.Add(_mayTotal + _mayHinsho);

                            rowTotalDept += _mayTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayTotal + _mayHinsho;
                        }
                        if (_junActualCostTotal > 0)
                        {
                            //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junActualCostTotal);
                            sukeyDto.JunCost.Add(Math.Round(_junHinsho, 2));
                            sukeyDto.JunCost.Add(_junActualCostTotal + _junHinsho);

                            rowTotalDept += _junActualCostTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junActualCostTotal + _junHinsho;
                        }
                        else
                        {
                            //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junTotal);
                            sukeyDto.JunCost.Add(Math.Round(_junHinsho, 2));
                            sukeyDto.JunCost.Add(_junTotal + _junHinsho);

                            rowTotalDept += _junTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junTotal + _junHinsho;
                        }
                        if (_julActualCostTotal > 0)
                        {
                            //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julActualCostTotal);
                            sukeyDto.JulCost.Add(Math.Round(_julHinsho, 2));
                            sukeyDto.JulCost.Add(_julActualCostTotal + _julHinsho);

                            rowTotalDept += _julActualCostTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julActualCostTotal + _julHinsho;
                        }
                        else
                        {
                            //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julTotal);
                            sukeyDto.JulCost.Add(Math.Round(_julHinsho, 2));
                            sukeyDto.JulCost.Add(_julTotal + _julHinsho);

                            rowTotalDept += _julTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julTotal + _julHinsho;
                        }
                        if (_augActualCostTotal > 0)
                        {
                            //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augActualCostTotal);
                            sukeyDto.AugCost.Add(Math.Round(_augHinsho, 2));
                            sukeyDto.AugCost.Add(_augActualCostTotal + _augHinsho);

                            rowTotalDept += _augActualCostTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augActualCostTotal + _augHinsho;
                        }
                        else
                        {
                            //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augTotal);
                            sukeyDto.AugCost.Add(Math.Round(_augHinsho, 2));
                            sukeyDto.AugCost.Add(_augTotal + _augHinsho);

                            rowTotalDept += _augTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augTotal + _augHinsho;
                        }
                        if (_sepActualCostTotal > 0)
                        {
                            //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepActualCostTotal);
                            sukeyDto.SepCost.Add(Math.Round(_sepHinsho, 2));
                            sukeyDto.SepCost.Add(_sepActualCostTotal + _sepHinsho);

                            rowTotalDept += _sepActualCostTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepActualCostTotal + _sepHinsho;
                        }
                        else
                        {
                            //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepTotal);
                            sukeyDto.SepCost.Add(Math.Round(_sepHinsho, 2));
                            sukeyDto.SepCost.Add(_sepTotal + _sepHinsho);

                            rowTotalDept += _sepTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepTotal + _sepHinsho;
                        }

                        sukeyDto.RowTotal.Add(rowTotalDept);
                        sukeyDto.RowTotal.Add(rowTotalQa);
                        sukeyDto.RowTotal.Add(rowTotal);

                        sukeyDto.FirstSlot.Add(deptFirstSlot);
                        sukeyDto.FirstSlot.Add(qaFirstSlot);
                        sukeyDto.FirstSlot.Add(totalFirstSlot);

                        sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotalQa - qaFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
                    }
                    else
                    {
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);

                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);

                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);

                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);

                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);

                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);

                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);

                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);

                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);

                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);

                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);

                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);

                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);

                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);

                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                    }



                    sukeyQADtos.Add(sukeyDto);
                }
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetTotalWithoutQA/")]
        public IHttpActionResult GetTotalWithoutQA(string companiIds, int departmentId)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            if (qaDepartmentByName == null)
            {
                return NotFound();
            }
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            List<Department> departments = departmentBLL.GetAllDepartments();
            foreach (var department in departments)
            {
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                if (department.Id == departmentId)
                {
                    //var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == departmentId).SingleOrDefault();
                    //if (apportionmentByDepartment == null)
                    //{
                    //    apportionmentByDepartment = new Apportionment();
                    //}

                    // update hinso variables by percentage.
                    //{
                    //    _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    //    _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    //    _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    //    _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    //    _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    //    _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    //    _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    //    _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    //    _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    //    _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    //    _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    //    _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    //}


                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                    if (forecastAssignmentViewModels.Count > 0)
                    {
                        double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                        double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                        double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                        double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                        double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                        double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                        double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                        double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                        double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                        double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                        double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                        double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                        double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                        double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                        double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                        double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                        double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                        double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                        double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                        double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                        double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                        double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                        double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                        double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                        if (_octActualCostTotal > 0)
                        {
                            //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octActualCostTotal);
                            sukeyDto.OctCost.Add(_octHinsho);
                            sukeyDto.OctCost.Add(_octActualCostTotal + _octHinsho);

                            rowTotalDept += _octActualCostTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octActualCostTotal + _octHinsho;
                        }
                        else
                        {
                            //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                            sukeyDto.OctCost.Add(_octTotal);
                            sukeyDto.OctCost.Add(_octHinsho);
                            sukeyDto.OctCost.Add(_octTotal + _octHinsho);

                            rowTotalDept += _octTotal;
                            rowTotalQa += _octHinsho;
                            rowTotal += _octTotal + _octHinsho;
                        }
                        if (_novActualCostTotal > 0)
                        {
                            //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novActualCostTotal);
                            sukeyDto.NovCost.Add(_novHinsho);
                            sukeyDto.NovCost.Add(_novActualCostTotal + _novHinsho);

                            rowTotalDept += _novActualCostTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novActualCostTotal + _novHinsho;
                        }
                        else
                        {
                            //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                            sukeyDto.NovCost.Add(_novTotal);
                            sukeyDto.NovCost.Add(_novHinsho);
                            sukeyDto.NovCost.Add(_novTotal + _novHinsho);

                            rowTotalDept += _novTotal;
                            rowTotalQa += _novHinsho;
                            rowTotal += _novTotal + _novHinsho;
                        }
                        if (_decActualCostTotal > 0)
                        {
                            //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decActualCostTotal);
                            sukeyDto.DecCost.Add(_decHinsho);
                            sukeyDto.DecCost.Add(_decActualCostTotal + _decHinsho);

                            rowTotalDept += _decActualCostTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decActualCostTotal + _decHinsho;
                        }
                        else
                        {
                            //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                            sukeyDto.DecCost.Add(_decTotal);
                            sukeyDto.DecCost.Add(_decHinsho);
                            sukeyDto.DecCost.Add(_decTotal + _decHinsho);

                            rowTotalDept += _decTotal;
                            rowTotalQa += _decHinsho;
                            rowTotal += _decTotal + _decHinsho;
                        }
                        if (_janActualCostTotal > 0)
                        {
                            //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janActualCostTotal);
                            sukeyDto.JanCost.Add(_janHinsho);
                            sukeyDto.JanCost.Add(_janActualCostTotal + _janHinsho);

                            rowTotalDept += _janActualCostTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janActualCostTotal + _janHinsho;
                        }
                        else
                        {
                            //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                            sukeyDto.JanCost.Add(_janTotal);
                            sukeyDto.JanCost.Add(_janHinsho);
                            sukeyDto.JanCost.Add(_janTotal + _janHinsho);

                            rowTotalDept += _janTotal;
                            rowTotalQa += _janHinsho;
                            rowTotal += _janTotal + _janHinsho;
                        }
                        if (_febActualCostTotal > 0)
                        {
                            //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febActualCostTotal);
                            sukeyDto.FebCost.Add(_febHinsho);
                            sukeyDto.FebCost.Add(_febActualCostTotal + _febHinsho);

                            rowTotalDept += _febActualCostTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febActualCostTotal + _febHinsho;
                        }
                        else
                        {
                            //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                            sukeyDto.FebCost.Add(_febTotal);
                            sukeyDto.FebCost.Add(_febHinsho);
                            sukeyDto.FebCost.Add(_febTotal + _febHinsho);

                            rowTotalDept += _febTotal;
                            rowTotalQa += _febHinsho;
                            rowTotal += _febTotal + _febHinsho;
                        }
                        if (_marActualCostTotal > 0)
                        {
                            //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marActualCostTotal);
                            sukeyDto.MarCost.Add(_marHinsho);
                            sukeyDto.MarCost.Add(_marActualCostTotal + _marHinsho);

                            rowTotalDept += _marActualCostTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marActualCostTotal + _marHinsho;
                        }
                        else
                        {
                            //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                            sukeyDto.MarCost.Add(_marTotal);
                            sukeyDto.MarCost.Add(_marHinsho);
                            sukeyDto.MarCost.Add(_marTotal + _marHinsho);

                            rowTotalDept += _marTotal;
                            rowTotalQa += _marHinsho;
                            rowTotal += _marTotal + _marHinsho;
                        }

                        deptFirstSlot = rowTotalDept;
                        qaFirstSlot = rowTotalQa;
                        totalFirstSlot = rowTotal;


                        if (_aprActualCostTotal > 0)
                        {
                            //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprActualCostTotal);
                            sukeyDto.AprCost.Add(_aprHinsho);
                            sukeyDto.AprCost.Add(_aprActualCostTotal + _aprHinsho);

                            rowTotalDept += _aprActualCostTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprActualCostTotal + _aprHinsho;
                        }
                        else
                        {
                            //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                            sukeyDto.AprCost.Add(_aprTotal);
                            sukeyDto.AprCost.Add(_aprHinsho);
                            sukeyDto.AprCost.Add(_aprTotal + _aprHinsho);


                            rowTotalDept += _aprTotal;
                            rowTotalQa += _aprHinsho;
                            rowTotal += _aprTotal + _aprHinsho;
                        }
                        if (_mayActualCostTotal > 0)
                        {
                            //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayActualCostTotal);
                            sukeyDto.MayCost.Add(_mayHinsho);
                            sukeyDto.MayCost.Add(_mayActualCostTotal + _mayHinsho);

                            rowTotalDept += _mayActualCostTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayActualCostTotal + _mayHinsho;
                        }
                        else
                        {
                            //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                            sukeyDto.MayCost.Add(_mayTotal);
                            sukeyDto.MayCost.Add(_mayHinsho);
                            sukeyDto.MayCost.Add(_mayTotal + _mayHinsho);

                            rowTotalDept += _mayTotal;
                            rowTotalQa += _mayHinsho;
                            rowTotal += _mayTotal + _mayHinsho;
                        }
                        if (_junActualCostTotal > 0)
                        {
                            //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junActualCostTotal);
                            sukeyDto.JunCost.Add(_junHinsho);
                            sukeyDto.JunCost.Add(_junActualCostTotal + _junHinsho);

                            rowTotalDept += _junActualCostTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junActualCostTotal + _junHinsho;
                        }
                        else
                        {
                            //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                            sukeyDto.JunCost.Add(_junTotal);
                            sukeyDto.JunCost.Add(_junHinsho);
                            sukeyDto.JunCost.Add(_junTotal + _junHinsho);

                            rowTotalDept += _junTotal;
                            rowTotalQa += _junHinsho;
                            rowTotal += _junTotal + _junHinsho;
                        }
                        if (_julActualCostTotal > 0)
                        {
                            //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julActualCostTotal);
                            sukeyDto.JulCost.Add(_julHinsho);
                            sukeyDto.JulCost.Add(_julActualCostTotal + _julHinsho);

                            rowTotalDept += _julActualCostTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julActualCostTotal + _julHinsho;
                        }
                        else
                        {
                            //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                            sukeyDto.JulCost.Add(_julTotal);
                            sukeyDto.JulCost.Add(_julHinsho);
                            sukeyDto.JulCost.Add(_julTotal + _julHinsho);

                            rowTotalDept += _julTotal;
                            rowTotalQa += _julHinsho;
                            rowTotal += _julTotal + _julHinsho;
                        }
                        if (_augActualCostTotal > 0)
                        {
                            //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augActualCostTotal);
                            sukeyDto.AugCost.Add(_augHinsho);
                            sukeyDto.AugCost.Add(_augActualCostTotal + _augHinsho);

                            rowTotalDept += _augActualCostTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augActualCostTotal + _augHinsho;
                        }
                        else
                        {
                            //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                            sukeyDto.AugCost.Add(_augTotal);
                            sukeyDto.AugCost.Add(_augHinsho);
                            sukeyDto.AugCost.Add(_augTotal + _augHinsho);

                            rowTotalDept += _augTotal;
                            rowTotalQa += _augHinsho;
                            rowTotal += _augTotal + _augHinsho;
                        }
                        if (_sepActualCostTotal > 0)
                        {
                            //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepActualCostTotal);
                            sukeyDto.SepCost.Add(_sepHinsho);
                            sukeyDto.SepCost.Add(_sepActualCostTotal + _sepHinsho);

                            rowTotalDept += _sepActualCostTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepActualCostTotal + _sepHinsho;
                        }
                        else
                        {
                            //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                            sukeyDto.SepCost.Add(_sepTotal);
                            sukeyDto.SepCost.Add(_sepHinsho);
                            sukeyDto.SepCost.Add(_sepTotal + _sepHinsho);

                            rowTotalDept += _sepTotal;
                            rowTotalQa += _sepHinsho;
                            rowTotal += _sepTotal + _sepHinsho;
                        }

                        sukeyDto.RowTotal.Add(rowTotalDept);
                        sukeyDto.RowTotal.Add(rowTotalQa);
                        sukeyDto.RowTotal.Add(rowTotal);

                        sukeyDto.FirstSlot.Add(deptFirstSlot);
                        sukeyDto.FirstSlot.Add(qaFirstSlot);
                        sukeyDto.FirstSlot.Add(totalFirstSlot);

                        sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotalQa - qaFirstSlot);
                        sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
                    }
                    else
                    {
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);
                        sukeyDto.OctCost.Add(0);

                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);
                        sukeyDto.NovCost.Add(0);

                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);
                        sukeyDto.DecCost.Add(0);

                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);
                        sukeyDto.JanCost.Add(0);

                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);
                        sukeyDto.FebCost.Add(0);

                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);
                        sukeyDto.MarCost.Add(0);

                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);
                        sukeyDto.AprCost.Add(0);

                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);
                        sukeyDto.MayCost.Add(0);

                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);
                        sukeyDto.JunCost.Add(0);

                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);
                        sukeyDto.JulCost.Add(0);

                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);
                        sukeyDto.AugCost.Add(0);

                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);
                        sukeyDto.SepCost.Add(0);

                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);
                        sukeyDto.RowTotal.Add(0);

                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);
                        sukeyDto.FirstSlot.Add(0);

                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                        sukeyDto.SecondSlot.Add(0);
                    }



                    sukeyQADtos.Add(sukeyDto);
                }
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetQaByDepartment/")]
        public IHttpActionResult GetQaByDepartment(string companiIds, string departmentIds)
        {
            List<string> departmentIdList = departmentIds.Split(',').ToList();
            if (departmentIdList.Count == 0)
            {
                return NotFound();
            }
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(8, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            //List<Department> departments = departmentBLL.GetAllDepartments();
            foreach (var departmentId in departmentIdList)
            {
                Department department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(departmentId));
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                //if (department.Id == departmentId)
                //{
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                //update hinso variables by percentage.
                {
                    _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                }


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                    if (_octActualCostTotal > 0)
                    {
                        //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                        //sukeyDto.OctCost.Add(_octActualCostTotal);
                        sukeyDto.OctCost.Add(_octHinsho);
                        //sukeyDto.OctCost.Add(_octActualCostTotal + _octHinsho);

                        //rowTotalDept += _octActualCostTotal;
                        rowTotalQa += _octHinsho;
                        //rowTotal += _octActualCostTotal + _octHinsho;
                    }
                    else
                    {
                        //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                        //sukeyDto.OctCost.Add(_octTotal);
                        sukeyDto.OctCost.Add(_octHinsho);
                        //sukeyDto.OctCost.Add(_octTotal + _octHinsho);

                        // rowTotalDept += _octTotal;
                        rowTotalQa += _octHinsho;
                        //rowTotal += _octTotal + _octHinsho;
                    }
                    if (_novActualCostTotal > 0)
                    {
                        //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                        //sukeyDto.NovCost.Add(_novActualCostTotal);
                        sukeyDto.NovCost.Add(_novHinsho);
                        //sukeyDto.NovCost.Add(_novActualCostTotal + _novHinsho);

                        //rowTotalDept += _novActualCostTotal;
                        rowTotalQa += _novHinsho;
                        //rowTotal += _novActualCostTotal + _novHinsho;
                    }
                    else
                    {
                        //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                        //sukeyDto.NovCost.Add(_novTotal);
                        sukeyDto.NovCost.Add(_novHinsho);
                        //sukeyDto.NovCost.Add(_novTotal + _novHinsho);

                        //rowTotalDept += _novTotal;
                        rowTotalQa += _novHinsho;
                        //rowTotal += _novTotal + _novHinsho;
                    }
                    if (_decActualCostTotal > 0)
                    {
                        //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                        //sukeyDto.DecCost.Add(_decActualCostTotal);
                        sukeyDto.DecCost.Add(_decHinsho);
                        //sukeyDto.DecCost.Add(_decActualCostTotal + _decHinsho);

                        //rowTotalDept += _decActualCostTotal;
                        rowTotalQa += _decHinsho;
                        //rowTotal += _decActualCostTotal + _decHinsho;
                    }
                    else
                    {
                        //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                        //sukeyDto.DecCost.Add(_decTotal);
                        sukeyDto.DecCost.Add(_decHinsho);
                        //sukeyDto.DecCost.Add(_decTotal + _decHinsho);

                        //rowTotalDept += _decTotal;
                        rowTotalQa += _decHinsho;
                        //rowTotal += _decTotal + _decHinsho;
                    }
                    if (_janActualCostTotal > 0)
                    {
                        //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                        //sukeyDto.JanCost.Add(_janActualCostTotal);
                        sukeyDto.JanCost.Add(_janHinsho);
                        //sukeyDto.JanCost.Add(_janActualCostTotal + _janHinsho);

                        //rowTotalDept += _janActualCostTotal;
                        rowTotalQa += _janHinsho;
                        //rowTotal += _janActualCostTotal + _janHinsho;
                    }
                    else
                    {
                        //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                        //sukeyDto.JanCost.Add(_janTotal);
                        sukeyDto.JanCost.Add(_janHinsho);
                        //sukeyDto.JanCost.Add(_janTotal + _janHinsho);

                        //rowTotalDept += _janTotal;
                        rowTotalQa += _janHinsho;
                        //rowTotal += _janTotal + _janHinsho;
                    }
                    if (_febActualCostTotal > 0)
                    {
                        //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                        //sukeyDto.FebCost.Add(_febActualCostTotal);
                        sukeyDto.FebCost.Add(_febHinsho);
                        //sukeyDto.FebCost.Add(_febActualCostTotal + _febHinsho);

                        //rowTotalDept += _febActualCostTotal;
                        rowTotalQa += _febHinsho;
                        //rowTotal += _febActualCostTotal + _febHinsho;
                    }
                    else
                    {
                        //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                        //sukeyDto.FebCost.Add(_febTotal);
                        sukeyDto.FebCost.Add(_febHinsho);
                        //sukeyDto.FebCost.Add(_febTotal + _febHinsho);

                        //rowTotalDept += _febTotal;
                        rowTotalQa += _febHinsho;
                        //rowTotal += _febTotal + _febHinsho;
                    }
                    if (_marActualCostTotal > 0)
                    {
                        //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                        // sukeyDto.MarCost.Add(_marActualCostTotal);
                        sukeyDto.MarCost.Add(_marHinsho);
                        //sukeyDto.MarCost.Add(_marActualCostTotal + _marHinsho);

                        //rowTotalDept += _marActualCostTotal;
                        rowTotalQa += _marHinsho;
                        //rowTotal += _marActualCostTotal + _marHinsho;
                    }
                    else
                    {
                        //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                        //sukeyDto.MarCost.Add(_marTotal);
                        sukeyDto.MarCost.Add(_marHinsho);
                        //sukeyDto.MarCost.Add(_marTotal + _marHinsho);

                        // rowTotalDept += _marTotal;
                        rowTotalQa += _marHinsho;
                        //rowTotal += _marTotal + _marHinsho;
                    }

                    //deptFirstSlot = rowTotalDept;
                    qaFirstSlot = rowTotalQa;
                    //totalFirstSlot = rowTotal;


                    if (_aprActualCostTotal > 0)
                    {
                        //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                        //sukeyDto.AprCost.Add(_aprActualCostTotal);
                        sukeyDto.AprCost.Add(_aprHinsho);
                        //sukeyDto.AprCost.Add(_aprActualCostTotal + _aprHinsho);

                        //rowTotalDept += _aprActualCostTotal;
                        rowTotalQa += _aprHinsho;
                        //rowTotal += _aprActualCostTotal + _aprHinsho;
                    }
                    else
                    {
                        //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                        //sukeyDto.AprCost.Add(_aprTotal);
                        sukeyDto.AprCost.Add(_aprHinsho);
                        //sukeyDto.AprCost.Add(_aprTotal + _aprHinsho);


                        //rowTotalDept += _aprTotal;
                        rowTotalQa += _aprHinsho;
                        //rowTotal += _aprTotal + _aprHinsho;
                    }
                    if (_mayActualCostTotal > 0)
                    {
                        //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                        //sukeyDto.MayCost.Add(_mayActualCostTotal);
                        sukeyDto.MayCost.Add(_mayHinsho);
                        //sukeyDto.MayCost.Add(_mayActualCostTotal + _mayHinsho);

                        //rowTotalDept += _mayActualCostTotal;
                        rowTotalQa += _mayHinsho;
                        //rowTotal += _mayActualCostTotal + _mayHinsho;
                    }
                    else
                    {
                        //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                        //sukeyDto.MayCost.Add(_mayTotal);
                        sukeyDto.MayCost.Add(_mayHinsho);
                        //sukeyDto.MayCost.Add(_mayTotal + _mayHinsho);

                        //rowTotalDept += _mayTotal;
                        rowTotalQa += _mayHinsho;
                        //rowTotal += _mayTotal + _mayHinsho;
                    }
                    if (_junActualCostTotal > 0)
                    {
                        //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                        //sukeyDto.JunCost.Add(_junActualCostTotal);
                        sukeyDto.JunCost.Add(_junHinsho);
                        //sukeyDto.JunCost.Add(_junActualCostTotal + _junHinsho);

                        //rowTotalDept += _junActualCostTotal;
                        rowTotalQa += _junHinsho;
                        //rowTotal += _junActualCostTotal + _junHinsho;
                    }
                    else
                    {
                        //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                        //sukeyDto.JunCost.Add(_junTotal);
                        sukeyDto.JunCost.Add(_junHinsho);
                        //sukeyDto.JunCost.Add(_junTotal + _junHinsho);

                        //rowTotalDept += _junTotal;
                        rowTotalQa += _junHinsho;
                        //rowTotal += _junTotal + _junHinsho;
                    }
                    if (_julActualCostTotal > 0)
                    {
                        //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                        //sukeyDto.JulCost.Add(_julActualCostTotal);
                        sukeyDto.JulCost.Add(_julHinsho);
                        //sukeyDto.JulCost.Add(_julActualCostTotal + _julHinsho);

                        //rowTotalDept += _julActualCostTotal;
                        rowTotalQa += _julHinsho;
                        //rowTotal += _julActualCostTotal + _julHinsho;
                    }
                    else
                    {
                        //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                        //sukeyDto.JulCost.Add(_julTotal);
                        sukeyDto.JulCost.Add(_julHinsho);
                        //sukeyDto.JulCost.Add(_julTotal + _julHinsho);

                        //rowTotalDept += _julTotal;
                        rowTotalQa += _julHinsho;
                        //rowTotal += _julTotal + _julHinsho;
                    }
                    if (_augActualCostTotal > 0)
                    {
                        //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                        //sukeyDto.AugCost.Add(_augActualCostTotal);
                        sukeyDto.AugCost.Add(_augHinsho);
                        //sukeyDto.AugCost.Add(_augActualCostTotal + _augHinsho);

                        //rowTotalDept += _augActualCostTotal;
                        rowTotalQa += _augHinsho;
                        // rowTotal += _augActualCostTotal + _augHinsho;
                    }
                    else
                    {
                        //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                        //sukeyDto.AugCost.Add(_augTotal);
                        sukeyDto.AugCost.Add(_augHinsho);
                        //sukeyDto.AugCost.Add(_augTotal + _augHinsho);

                        //rowTotalDept += _augTotal;
                        rowTotalQa += _augHinsho;
                        //rowTotal += _augTotal + _augHinsho;
                    }
                    if (_sepActualCostTotal > 0)
                    {
                        //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                        //sukeyDto.SepCost.Add(_sepActualCostTotal);
                        sukeyDto.SepCost.Add(_sepHinsho);
                        //sukeyDto.SepCost.Add(_sepActualCostTotal + _sepHinsho);

                        //rowTotalDept += _sepActualCostTotal;
                        rowTotalQa += _sepHinsho;
                        //rowTotal += _sepActualCostTotal + _sepHinsho;
                    }
                    else
                    {
                        //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                        //sukeyDto.SepCost.Add(_sepTotal);
                        sukeyDto.SepCost.Add(_sepHinsho);
                        //sukeyDto.SepCost.Add(_sepTotal + _sepHinsho);

                        //rowTotalDept += _sepTotal;
                        rowTotalQa += _sepHinsho;
                        //rowTotal += _sepTotal + _sepHinsho;
                    }

                    //sukeyDto.RowTotal.Add(rowTotalDept);
                    sukeyDto.RowTotal.Add(rowTotalQa);
                    //sukeyDto.RowTotal.Add(rowTotal);

                    //sukeyDto.FirstSlot.Add(deptFirstSlot);
                    sukeyDto.FirstSlot.Add(qaFirstSlot);
                    //sukeyDto.FirstSlot.Add(totalFirstSlot);

                    //sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                    sukeyDto.SecondSlot.Add(rowTotalQa - qaFirstSlot);
                    //sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
                }
                else
                {
                    //sukeyDto.OctCost.Add(0);
                    //sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);

                    //sukeyDto.NovCost.Add(0);
                    //sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);

                    //sukeyDto.DecCost.Add(0);
                    //sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);

                    //sukeyDto.JanCost.Add(0);
                    //sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);

                    //sukeyDto.FebCost.Add(0);
                    //sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);

                    //sukeyDto.MarCost.Add(0);
                    //sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);

                    //sukeyDto.AprCost.Add(0);
                    //sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);

                    //sukeyDto.MayCost.Add(0);
                    //sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);

                    //sukeyDto.JunCost.Add(0);
                    //sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);

                    //sukeyDto.JulCost.Add(0);
                    //sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);

                    //sukeyDto.AugCost.Add(0);
                    //sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);

                    //sukeyDto.SepCost.Add(0);
                    //sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);

                    //sukeyDto.RowTotal.Add(0);
                    //sukeyDto.RowTotal.Add(0);
                    sukeyDto.RowTotal.Add(0);

                    //sukeyDto.FirstSlot.Add(0);
                    //sukeyDto.FirstSlot.Add(0);
                    sukeyDto.FirstSlot.Add(0);

                    //sukeyDto.SecondSlot.Add(0);
                    //sukeyDto.SecondSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                }



                sukeyQADtos.Add(sukeyDto);
                //}
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetTotalByIncharge/")]
        public IHttpActionResult GetTotalByIncharge(string companiIds, string incgangeIds)
        {
            List<string> inchargeIdArray = incgangeIds.Split(',').ToList();
            if (inchargeIdArray.Count == 0)
            {
                return NotFound();
            }
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(8, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            //List<Department> departments = departmentBLL.GetAllDepartments();

            //List<InCharge> inCharges = inChargeBLL.GetAllInCharges();
            foreach (var inchargeId in inchargeIdArray)
            {
                InCharge inCharge = inChargeBLL.GetInChargeByInChargeId(Convert.ToInt32(inchargeId));
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.InchargeId = inCharge.Id.ToString();
                sukeyDto.InchargeName = inCharge.InChargeName;

                //var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == departmentId).SingleOrDefault();
                //if (apportionmentByDepartment == null)
                //{
                //    apportionmentByDepartment = new Apportionment();
                //}

                // update hinso variables by percentage.
                //{
                //    _octHinsho = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                //    _novHinsho = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                //    _decHinsho = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                //    _janHinsho = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                //    _febHinsho = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                //    _marHinsho = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                //    _aprHinsho = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                //    _mayHinsho = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                //    _junHinsho = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                //    _julHinsho = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                //    _augHinsho = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                //    _sepHinsho = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                //}


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByIncharge_Company(inCharge.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));


                    if (_octActualCostTotal > 0)
                    {
                        //var tempQa = _octActualCostTotal * (apportionmentByDepartment.OctPercentage / 100);
                        sukeyDto.OctCost.Add(_octActualCostTotal);
                        sukeyDto.OctCost.Add(_octHinsho);
                        sukeyDto.OctCost.Add(_octActualCostTotal + _octHinsho);

                        rowTotalDept += _octActualCostTotal;
                        rowTotalQa += _octHinsho;
                        rowTotal += _octActualCostTotal + _octHinsho;
                    }
                    else
                    {
                        //var tempQa = _octTotal * (apportionmentByDepartment.OctPercentage / 100);
                        sukeyDto.OctCost.Add(_octTotal);
                        sukeyDto.OctCost.Add(_octHinsho);
                        sukeyDto.OctCost.Add(_octTotal + _octHinsho);

                        rowTotalDept += _octTotal;
                        rowTotalQa += _octHinsho;
                        rowTotal += _octTotal + _octHinsho;
                    }
                    if (_novActualCostTotal > 0)
                    {
                        //var tempQa = _novActualCostTotal * (apportionmentByDepartment.NovPercentage / 100);
                        sukeyDto.NovCost.Add(_novActualCostTotal);
                        sukeyDto.NovCost.Add(_novHinsho);
                        sukeyDto.NovCost.Add(_novActualCostTotal + _novHinsho);

                        rowTotalDept += _novActualCostTotal;
                        rowTotalQa += _novHinsho;
                        rowTotal += _novActualCostTotal + _novHinsho;
                    }
                    else
                    {
                        //var tempQa = _novTotal * (apportionmentByDepartment.NovPercentage / 100);
                        sukeyDto.NovCost.Add(_novTotal);
                        sukeyDto.NovCost.Add(_novHinsho);
                        sukeyDto.NovCost.Add(_novTotal + _novHinsho);

                        rowTotalDept += _novTotal;
                        rowTotalQa += _novHinsho;
                        rowTotal += _novTotal + _novHinsho;
                    }
                    if (_decActualCostTotal > 0)
                    {
                        //var tempQa = _decActualCostTotal * (apportionmentByDepartment.DecPercentage / 100);
                        sukeyDto.DecCost.Add(_decActualCostTotal);
                        sukeyDto.DecCost.Add(_decHinsho);
                        sukeyDto.DecCost.Add(_decActualCostTotal + _decHinsho);

                        rowTotalDept += _decActualCostTotal;
                        rowTotalQa += _decHinsho;
                        rowTotal += _decActualCostTotal + _decHinsho;
                    }
                    else
                    {
                        //var tempQa = _decTotal * (apportionmentByDepartment.DecPercentage / 100);
                        sukeyDto.DecCost.Add(_decTotal);
                        sukeyDto.DecCost.Add(_decHinsho);
                        sukeyDto.DecCost.Add(_decTotal + _decHinsho);

                        rowTotalDept += _decTotal;
                        rowTotalQa += _decHinsho;
                        rowTotal += _decTotal + _decHinsho;
                    }
                    if (_janActualCostTotal > 0)
                    {
                        //var tempQa = _janActualCostTotal * (apportionmentByDepartment.JanPercentage / 100);
                        sukeyDto.JanCost.Add(_janActualCostTotal);
                        sukeyDto.JanCost.Add(_janHinsho);
                        sukeyDto.JanCost.Add(_janActualCostTotal + _janHinsho);

                        rowTotalDept += _janActualCostTotal;
                        rowTotalQa += _janHinsho;
                        rowTotal += _janActualCostTotal + _janHinsho;
                    }
                    else
                    {
                        //var tempQa = _janTotal * (apportionmentByDepartment.JanPercentage / 100);
                        sukeyDto.JanCost.Add(_janTotal);
                        sukeyDto.JanCost.Add(_janHinsho);
                        sukeyDto.JanCost.Add(_janTotal + _janHinsho);

                        rowTotalDept += _janTotal;
                        rowTotalQa += _janHinsho;
                        rowTotal += _janTotal + _janHinsho;
                    }
                    if (_febActualCostTotal > 0)
                    {
                        //var tempQa = _febActualCostTotal * (apportionmentByDepartment.FebPercentage / 100);
                        sukeyDto.FebCost.Add(_febActualCostTotal);
                        sukeyDto.FebCost.Add(_febHinsho);
                        sukeyDto.FebCost.Add(_febActualCostTotal + _febHinsho);

                        rowTotalDept += _febActualCostTotal;
                        rowTotalQa += _febHinsho;
                        rowTotal += _febActualCostTotal + _febHinsho;
                    }
                    else
                    {
                        //var tempQa = _febTotal * (apportionmentByDepartment.FebPercentage / 100);
                        sukeyDto.FebCost.Add(_febTotal);
                        sukeyDto.FebCost.Add(_febHinsho);
                        sukeyDto.FebCost.Add(_febTotal + _febHinsho);

                        rowTotalDept += _febTotal;
                        rowTotalQa += _febHinsho;
                        rowTotal += _febTotal + _febHinsho;
                    }
                    if (_marActualCostTotal > 0)
                    {
                        //var tempQa = _marActualCostTotal * (apportionmentByDepartment.MarPercentage / 100);
                        sukeyDto.MarCost.Add(_marActualCostTotal);
                        sukeyDto.MarCost.Add(_marHinsho);
                        sukeyDto.MarCost.Add(_marActualCostTotal + _marHinsho);

                        rowTotalDept += _marActualCostTotal;
                        rowTotalQa += _marHinsho;
                        rowTotal += _marActualCostTotal + _marHinsho;
                    }
                    else
                    {
                        //var tempQa = _marTotal * (apportionmentByDepartment.MarPercentage / 100);
                        sukeyDto.MarCost.Add(_marTotal);
                        sukeyDto.MarCost.Add(_marHinsho);
                        sukeyDto.MarCost.Add(_marTotal + _marHinsho);

                        rowTotalDept += _marTotal;
                        rowTotalQa += _marHinsho;
                        rowTotal += _marTotal + _marHinsho;
                    }

                    deptFirstSlot = rowTotalDept;
                    qaFirstSlot = rowTotalQa;
                    totalFirstSlot = rowTotal;


                    if (_aprActualCostTotal > 0)
                    {
                        //var tempQa = _aprActualCostTotal * (apportionmentByDepartment.AprPercentage / 100);
                        sukeyDto.AprCost.Add(_aprActualCostTotal);
                        sukeyDto.AprCost.Add(_aprHinsho);
                        sukeyDto.AprCost.Add(_aprActualCostTotal + _aprHinsho);

                        rowTotalDept += _aprActualCostTotal;
                        rowTotalQa += _aprHinsho;
                        rowTotal += _aprActualCostTotal + _aprHinsho;
                    }
                    else
                    {
                        //var tempQa = _aprTotal * (apportionmentByDepartment.AprPercentage / 100);
                        sukeyDto.AprCost.Add(_aprTotal);
                        sukeyDto.AprCost.Add(_aprHinsho);
                        sukeyDto.AprCost.Add(_aprTotal + _aprHinsho);


                        rowTotalDept += _aprTotal;
                        rowTotalQa += _aprHinsho;
                        rowTotal += _aprTotal + _aprHinsho;
                    }
                    if (_mayActualCostTotal > 0)
                    {
                        //var tempQa = _mayActualCostTotal * (apportionmentByDepartment.MayPercentage / 100);
                        sukeyDto.MayCost.Add(_mayActualCostTotal);
                        sukeyDto.MayCost.Add(_mayHinsho);
                        sukeyDto.MayCost.Add(_mayActualCostTotal + _mayHinsho);

                        rowTotalDept += _mayActualCostTotal;
                        rowTotalQa += _mayHinsho;
                        rowTotal += _mayActualCostTotal + _mayHinsho;
                    }
                    else
                    {
                        //var tempQa = _mayTotal * (apportionmentByDepartment.MayPercentage / 100);
                        sukeyDto.MayCost.Add(_mayTotal);
                        sukeyDto.MayCost.Add(_mayHinsho);
                        sukeyDto.MayCost.Add(_mayTotal + _mayHinsho);

                        rowTotalDept += _mayTotal;
                        rowTotalQa += _mayHinsho;
                        rowTotal += _mayTotal + _mayHinsho;
                    }
                    if (_junActualCostTotal > 0)
                    {
                        //var tempQa = _junActualCostTotal * (apportionmentByDepartment.JunPercentage / 100);
                        sukeyDto.JunCost.Add(_junActualCostTotal);
                        sukeyDto.JunCost.Add(_junHinsho);
                        sukeyDto.JunCost.Add(_junActualCostTotal + _junHinsho);

                        rowTotalDept += _junActualCostTotal;
                        rowTotalQa += _junHinsho;
                        rowTotal += _junActualCostTotal + _junHinsho;
                    }
                    else
                    {
                        //var tempQa = _junTotal * (apportionmentByDepartment.JunPercentage / 100);
                        sukeyDto.JunCost.Add(_junTotal);
                        sukeyDto.JunCost.Add(_junHinsho);
                        sukeyDto.JunCost.Add(_junTotal + _junHinsho);

                        rowTotalDept += _junTotal;
                        rowTotalQa += _junHinsho;
                        rowTotal += _junTotal + _junHinsho;
                    }
                    if (_julActualCostTotal > 0)
                    {
                        //var tempQa = _julActualCostTotal * (apportionmentByDepartment.JulPercentage / 100);
                        sukeyDto.JulCost.Add(_julActualCostTotal);
                        sukeyDto.JulCost.Add(_julHinsho);
                        sukeyDto.JulCost.Add(_julActualCostTotal + _julHinsho);

                        rowTotalDept += _julActualCostTotal;
                        rowTotalQa += _julHinsho;
                        rowTotal += _julActualCostTotal + _julHinsho;
                    }
                    else
                    {
                        //var tempQa = _julTotal * (apportionmentByDepartment.JulPercentage / 100);
                        sukeyDto.JulCost.Add(_julTotal);
                        sukeyDto.JulCost.Add(_julHinsho);
                        sukeyDto.JulCost.Add(_julTotal + _julHinsho);

                        rowTotalDept += _julTotal;
                        rowTotalQa += _julHinsho;
                        rowTotal += _julTotal + _julHinsho;
                    }
                    if (_augActualCostTotal > 0)
                    {
                        //var tempQa = _augActualCostTotal * (apportionmentByDepartment.AugPercentage / 100);
                        sukeyDto.AugCost.Add(_augActualCostTotal);
                        sukeyDto.AugCost.Add(_augHinsho);
                        sukeyDto.AugCost.Add(_augActualCostTotal + _augHinsho);

                        rowTotalDept += _augActualCostTotal;
                        rowTotalQa += _augHinsho;
                        rowTotal += _augActualCostTotal + _augHinsho;
                    }
                    else
                    {
                        //var tempQa = _augTotal * (apportionmentByDepartment.AugPercentage / 100);
                        sukeyDto.AugCost.Add(_augTotal);
                        sukeyDto.AugCost.Add(_augHinsho);
                        sukeyDto.AugCost.Add(_augTotal + _augHinsho);

                        rowTotalDept += _augTotal;
                        rowTotalQa += _augHinsho;
                        rowTotal += _augTotal + _augHinsho;
                    }
                    if (_sepActualCostTotal > 0)
                    {
                        //var tempQa = _sepActualCostTotal * (apportionmentByDepartment.SepPercentage / 100);
                        sukeyDto.SepCost.Add(_sepActualCostTotal);
                        sukeyDto.SepCost.Add(_sepHinsho);
                        sukeyDto.SepCost.Add(_sepActualCostTotal + _sepHinsho);

                        rowTotalDept += _sepActualCostTotal;
                        rowTotalQa += _sepHinsho;
                        rowTotal += _sepActualCostTotal + _sepHinsho;
                    }
                    else
                    {
                        //var tempQa = _sepTotal * (apportionmentByDepartment.SepPercentage / 100);
                        sukeyDto.SepCost.Add(_sepTotal);
                        sukeyDto.SepCost.Add(_sepHinsho);
                        sukeyDto.SepCost.Add(_sepTotal + _sepHinsho);

                        rowTotalDept += _sepTotal;
                        rowTotalQa += _sepHinsho;
                        rowTotal += _sepTotal + _sepHinsho;
                    }

                    sukeyDto.RowTotal.Add(rowTotalDept);
                    sukeyDto.RowTotal.Add(rowTotalQa);
                    sukeyDto.RowTotal.Add(rowTotal);

                    sukeyDto.FirstSlot.Add(deptFirstSlot);
                    sukeyDto.FirstSlot.Add(qaFirstSlot);
                    sukeyDto.FirstSlot.Add(totalFirstSlot);

                    sukeyDto.SecondSlot.Add(rowTotalDept - deptFirstSlot);
                    sukeyDto.SecondSlot.Add(rowTotalQa - qaFirstSlot);
                    sukeyDto.SecondSlot.Add(rowTotal - totalFirstSlot);
                }
                else
                {
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.OctCost.Add(0);

                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.NovCost.Add(0);

                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.DecCost.Add(0);

                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.JanCost.Add(0);

                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.FebCost.Add(0);

                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.MarCost.Add(0);

                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.AprCost.Add(0);

                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.MayCost.Add(0);

                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JunCost.Add(0);

                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.JulCost.Add(0);

                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.AugCost.Add(0);

                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.SepCost.Add(0);

                    sukeyDto.RowTotal.Add(0);
                    sukeyDto.RowTotal.Add(0);
                    sukeyDto.RowTotal.Add(0);

                    sukeyDto.FirstSlot.Add(0);
                    sukeyDto.FirstSlot.Add(0);
                    sukeyDto.FirstSlot.Add(0);

                    sukeyDto.SecondSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                }



                sukeyQADtos.Add(sukeyDto);
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetManmonthByDepartment/")]
        public IHttpActionResult GetManmonthByDepartment(string companiIds, string departmentIds)
        {
            List<string> deprmentdList = departmentIds.Split(',').ToList();
            if (deprmentdList.Count == 0)
            {
                return NotFound();
            }
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;

            foreach (var deprmentId in deprmentdList)
            {
                Department department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(deprmentId));
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    sukeyDto.OctPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.OctPoints));
                    sukeyDto.NovPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.NovPoints));
                    sukeyDto.DecPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.DecPoints));
                    sukeyDto.JanPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JanPoints));
                    sukeyDto.FebPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.FebPoints));
                    sukeyDto.MarPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.MarPoints));
                    sukeyDto.AprPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.AprPoints));
                    sukeyDto.MayPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.MayPoints));
                    sukeyDto.JunPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JunPoints));
                    sukeyDto.JulPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JulPoints));
                    sukeyDto.AugPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.AugPoints));
                    sukeyDto.SepPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.SepPoints));

                }



                sukeyQADtos.Add(sukeyDto);
                //}
            }
            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetManmonthByIncharges/")]
        public IHttpActionResult GetManmonthByIncharges(string companiIds, string inchargeIds)
        {
            List<string> inchargeIdList = inchargeIds.Split(',').ToList();
            if (inchargeIdList.Count == 0)
            {
                return NotFound();
            }
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int year = 0;

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;

            foreach (var inchargeId in inchargeIdList)
            {
                InCharge inCharge = inChargeBLL.GetInChargeByInChargeId(Convert.ToInt32(inchargeId));
                double rowTotal = 0;
                double rowTotalQa = 0;
                double rowTotalDept = 0;
                double deptFirstSlot = 0;
                double qaFirstSlot = 0;
                double totalFirstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.InchargeId = inCharge.Id.ToString();
                sukeyDto.InchargeName = inCharge.InChargeName;


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByIncharge_Company(inCharge.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    sukeyDto.OctPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.OctPoints));
                    sukeyDto.NovPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.NovPoints));
                    sukeyDto.DecPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.DecPoints));
                    sukeyDto.JanPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JanPoints));
                    sukeyDto.FebPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.FebPoints));
                    sukeyDto.MarPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.MarPoints));
                    sukeyDto.AprPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.AprPoints));
                    sukeyDto.MayPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.MayPoints));
                    sukeyDto.JunPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JunPoints));
                    sukeyDto.JulPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.JulPoints));
                    sukeyDto.AugPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.AugPoints));
                    sukeyDto.SepPoint = forecastAssignmentViewModels.Sum(f => Convert.ToDouble(f.SepPoints));

                }



                sukeyQADtos.Add(sukeyDto);
                //}
            }
            return Ok(sukeyQADtos);
        }


        [HttpGet]
        [Route("api/utilities/GetInitialBudgetForTotal/")]
        public IHttpActionResult GetInitialBudgetForTotal(string companiIds)
        {

            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;

            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            List<Department> departments = departmentBLL.GetAllDepartments();




            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(8, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }
            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                if (department.Id == qaDepartmentByName.Id)
                {
                    continue;
                }
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = totalBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    //double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    //double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    //double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    //double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    //double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    //double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    //double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    //double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    //double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    //double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    //double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    //double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    {
                        sukeyDto.OctCost.Add(_octTotal + _octCalculation);
                        rowTotal += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);

                    {
                        sukeyDto.NovCost.Add(_novTotal + _novCalculation);
                        rowTotal += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);

                    {
                        sukeyDto.DecCost.Add(_decTotal + _decCalculation);
                        rowTotal += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);

                    {
                        sukeyDto.JanCost.Add(_janTotal + _janCalculation);
                        rowTotal += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);

                    {
                        sukeyDto.FebCost.Add(_febTotal + _febCalculation);
                        rowTotal += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);

                    {
                        sukeyDto.MarCost.Add(_marTotal + _marCalculation);
                        rowTotal += _marTotal + _marCalculation;
                    }
                    sukeyDto.FirstSlot.Add(rowTotal);
                    firstSlot = rowTotal;

                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);

                    {
                        sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
                        rowTotal += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);

                    {
                        sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
                        rowTotal += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);

                    {
                        sukeyDto.JunCost.Add(_junTotal + _junCalculation);
                        rowTotal += _junTotal + _junCalculation;
                    }
                    var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);

                    {
                        sukeyDto.JulCost.Add(_julTotal + _julCalculation);
                        rowTotal += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);

                    {
                        sukeyDto.AugCost.Add(_augTotal + _augCalculation);
                        rowTotal += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);

                    {
                        sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
                        rowTotal += _sepTotal + _sepCalculation;
                    }
                    sukeyDto.RowTotal.Add(rowTotal);
                    sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

                }
                else
                {
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.RowTotal.Add(0);
                    sukeyDto.FirstSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                }



                sukeyQADtos.Add(sukeyDto);

            }

            return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetHeadCount/")]
        public IHttpActionResult GetHeadCount(string companiIds)
        {

            int year = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            List<Department> departments = departmentBLL.GetAllDepartments();
            List<SubCategory> subCategories = departmentBLL.GetAllSubCategories();
            List<HeadCountInner> _headCountList = new List<HeadCountInner>();
            List<ForecastAssignmentViewModel> _allforecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
            //Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            foreach (var department in departments)
            {
                //if (department.Id == qaDepartmentByName.Id)
                //{
                //    continue;
                //}

                var subCategory = subCategories.Where(sc => sc.Id == Convert.ToInt32(department.SubCategoryId)).SingleOrDefault();

                _headCountList.Add(new HeadCountInner
                {
                    DepartmentId = department.Id,
                    DepartmentName = department.DepartmentName,
                    CategoryName = subCategory.CategoryName,
                    SubCategoryName = subCategory.SubCategoryName,
                    OctCount = 0,
                    NovCount = 0,
                    DecCount = 0,
                    JanCount = 0,
                    FebCount = 0,
                    MarCount = 0,
                    AprCount = 0,
                    MayCount = 0,
                    JunCount = 0,
                    JulCount = 0,
                    AugCount = 0,
                    SepCount = 0,
                });

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    _allforecastAssignmentViewModels.AddRange(forecastAssignmentViewModels);
                }
            }

            if (_allforecastAssignmentViewModels.Count > 0)
            {
                var _uniqueItemList = _allforecastAssignmentViewModels.GroupBy(x => x.EmployeeId).Select(x => x.First()).ToList();
                var _uniqueEmployeeIdList = _uniqueItemList.Select(x => x.EmployeeId).ToList();
                foreach (var employeeId in _uniqueEmployeeIdList)
                {
                    var filteredByEmployeeId = _allforecastAssignmentViewModels.Where(x => x.EmployeeId == employeeId).ToList();
                    if (filteredByEmployeeId.Count == 1)
                    {
                        foreach (var item in filteredByEmployeeId)
                        {
                            var getSingleDeptHeadCount = _headCountList.Where(h => h.DepartmentId == Convert.ToInt32(item.DepartmentId)).SingleOrDefault();
                            if (Convert.ToDouble(item.OctPoints) > 0)
                            {
                                getSingleDeptHeadCount.OctCount++; ;
                            }
                            if (Convert.ToDouble(item.NovPoints) > 0)
                            {
                                getSingleDeptHeadCount.NovCount++;
                            }
                            if (Convert.ToDouble(item.DecPoints) > 0)
                            {
                                getSingleDeptHeadCount.DecCount++;
                            }
                            if (Convert.ToDouble(item.JanPoints) > 0)
                            {
                                getSingleDeptHeadCount.JanCount++;
                            }
                            if (Convert.ToDouble(item.FebPoints) > 0)
                            {
                                getSingleDeptHeadCount.FebCount++;
                            }
                            if (Convert.ToDouble(item.MarPoints) > 0)
                            {
                                getSingleDeptHeadCount.MarCount++;
                            }
                            if (Convert.ToDouble(item.AprPoints) > 0)
                            {
                                getSingleDeptHeadCount.AprCount++;
                            }
                            if (Convert.ToDouble(item.MayPoints) > 0)
                            {
                                getSingleDeptHeadCount.MayCount++;
                            }
                            if (Convert.ToDouble(item.JunPoints) > 0)
                            {
                                getSingleDeptHeadCount.JunCount++;
                            }
                            if (Convert.ToDouble(item.JulPoints) > 0)
                            {
                                getSingleDeptHeadCount.JulCount++;
                            }
                            if (Convert.ToDouble(item.AugPoints) > 0)
                            {
                                getSingleDeptHeadCount.AugCount++;
                            }
                            if (Convert.ToDouble(item.SepPoints) > 0)
                            {
                                getSingleDeptHeadCount.SepCount++;
                            }

                        }


                    }
                    else if (filteredByEmployeeId.Count > 1)
                    {
                        List<int> _octDeptId = new List<int>();
                        List<int> _novDeptId = new List<int>();
                        List<int> _decDeptId = new List<int>();
                        List<int> _janDeptId = new List<int>();
                        List<int> _febDeptId = new List<int>();
                        List<int> _marDeptId = new List<int>();
                        List<int> _aprDeptId = new List<int>();
                        List<int> _mayDeptId = new List<int>();
                        List<int> _junDeptId = new List<int>();
                        List<int> _julDeptId = new List<int>();
                        List<int> _augDeptId = new List<int>();
                        List<int> _sepDeptId = new List<int>();
                        bool octFlag = false, novFlag = false, decFlag = false, janFlag = false, febFlag = false, marFlag = false, aprFlag = false, mayFlag = false, junFlag = false, julFlag = false, augFlag = false, sepFlag = false;
                        List<ForecastAssignmentViewModel> _tempArray = new List<ForecastAssignmentViewModel>();
                        for (int i = 0; i < filteredByEmployeeId.Count; i++)
                        {

                            ForecastAssignmentViewModel _filterForOct, _filterForNov, _filterForDec, _filterForJan, _filterForFeb, _filterForMar, _filterForApr, _filterForMay, _filterForJun, _filterForJul, _filterForAug, _filterForSep;

                            var _octVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.OctPoints));
                            if (_octVal == 0)
                            {
                                _filterForOct = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForOct = filteredByEmployeeId.Where(a => Convert.ToDouble(a.OctPoints) == _octVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForOct.OctPoints) > 0)
                            {
                                _octDeptId.Add(Convert.ToInt32(_filterForOct.DepartmentId));
                            }



                            var _novVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.NovPoints));
                            if (_novVal == 0)
                            {
                                _filterForNov = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForNov = filteredByEmployeeId.Where(a => Convert.ToDouble(a.NovPoints) == _novVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForNov.NovPoints) > 0)
                            {
                                _novDeptId.Add(Convert.ToInt32(_filterForNov.DepartmentId));
                            }


                            var _decVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.DecPoints));
                            if (_decVal == 0)
                            {
                                _filterForDec = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForDec = filteredByEmployeeId.Where(a => Convert.ToDouble(a.DecPoints) == _decVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForDec.DecPoints) > 0)
                            {
                                _decDeptId.Add(Convert.ToInt32(_filterForDec.DepartmentId));
                            }



                            var _janVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JanPoints));
                            if (_janVal == 0)
                            {
                                _filterForJan = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJan = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JanPoints) == _janVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJan.JanPoints) > 0)
                            {
                                _janDeptId.Add(Convert.ToInt32(_filterForJan.DepartmentId));
                            }



                            var _febVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.FebPoints));
                            if (_febVal == 0)
                            {
                                _filterForFeb = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForFeb = filteredByEmployeeId.Where(a => Convert.ToDouble(a.FebPoints) == _febVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForFeb.FebPoints) > 0)
                            {
                                _febDeptId.Add(Convert.ToInt32(_filterForFeb.DepartmentId));
                            }



                            var _marVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MarPoints));
                            if (_marVal == 0)
                            {
                                _filterForMar = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMar = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MarPoints) == _marVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMar.MarPoints) > 0)
                            {
                                _marDeptId.Add(Convert.ToInt32(_filterForMar.DepartmentId));
                            }



                            var _aprVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AprPoints));
                            if (_aprVal == 0)
                            {
                                _filterForApr = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForApr = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AprPoints) == _aprVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForApr.AprPoints) > 0)
                            {
                                _aprDeptId.Add(Convert.ToInt32(_filterForApr.DepartmentId));
                            }



                            var _mayVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.MayPoints));
                            if (_mayVal == 0)
                            {
                                _filterForMay = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForMay = filteredByEmployeeId.Where(a => Convert.ToDouble(a.MayPoints) == _mayVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForMay.MayPoints) > 0)
                            {
                                _mayDeptId.Add(Convert.ToInt32(_filterForMay.DepartmentId));
                            }



                            var _junVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JunPoints));
                            if (_junVal == 0)
                            {
                                _filterForJun = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJun = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JunPoints) == _junVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJun.JunPoints) > 0)
                            {
                                _junDeptId.Add(Convert.ToInt32(_filterForJun.DepartmentId));
                            }



                            var _julVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.JulPoints));
                            if (_julVal == 0)
                            {
                                _filterForJul = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForJul = filteredByEmployeeId.Where(a => Convert.ToDouble(a.JulPoints) == _julVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForJul.JulPoints) > 0)
                            {
                                _julDeptId.Add(Convert.ToInt32(_filterForJul.DepartmentId));
                            }



                            var _augVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.AugPoints));
                            if (_augVal == 0)
                            {
                                _filterForAug = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForAug = filteredByEmployeeId.Where(a => Convert.ToDouble(a.AugPoints) == _augVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForAug.AugPoints) > 0)
                            {
                                _augDeptId.Add(Convert.ToInt32(_filterForAug.DepartmentId));
                            }



                            var _sepVal = filteredByEmployeeId.Max(a => Convert.ToDouble(a.SepPoints));
                            if (_sepVal == 0)
                            {
                                _filterForSep = filteredByEmployeeId[0];
                            }
                            else
                            {
                                _filterForSep = filteredByEmployeeId.Where(a => Convert.ToDouble(a.SepPoints) == _sepVal).FirstOrDefault();
                            }
                            if (Convert.ToDouble(_filterForSep.SepPoints) > 0)
                            {
                                _sepDeptId.Add(Convert.ToInt32(_filterForSep.DepartmentId));
                            }



                            //if (_tempArray.Count == 0)
                            //{
                            //    _tempArray.Add(filteredByEmployeeId[i]);
                            //}
                            //else
                            //{


                            //    foreach (var tempItem in _tempArray)
                            //    {


                            //        // for oct
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].OctPoints) > Convert.ToDouble(tempItem.OctPoints))
                            //            {
                            //                octFlag = false;
                            //                _octDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].OctPoints) < Convert.ToDouble(tempItem.OctPoints))
                            //            {
                            //                octFlag = false;
                            //                _octDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (octFlag == false)
                            //                {
                            //                    octFlag = true;
                            //                    _octDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for nov
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].NovPoints) > Convert.ToDouble(tempItem.NovPoints))
                            //            {
                            //                novFlag = false;
                            //                _novDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].NovPoints) < Convert.ToDouble(tempItem.NovPoints))
                            //            {
                            //                novFlag = false;
                            //                _novDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (novFlag == false)
                            //                {
                            //                    novFlag = true;
                            //                    _novDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for dec
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].DecPoints) > Convert.ToDouble(tempItem.DecPoints))
                            //            {
                            //                decFlag = false;
                            //                _decDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].DecPoints) < Convert.ToDouble(tempItem.DecPoints))
                            //            {
                            //                decFlag = false;
                            //                _decDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (decFlag == false)
                            //                {
                            //                    decFlag = true;
                            //                    _decDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for jan
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].JanPoints) > Convert.ToDouble(tempItem.JanPoints))
                            //            {
                            //                janFlag = false;
                            //                _janDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].JanPoints) < Convert.ToDouble(tempItem.JanPoints))
                            //            {
                            //                janFlag = false;
                            //                _janDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (janFlag == false)
                            //                {
                            //                    janFlag = true;
                            //                    _janDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for feb
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].FebPoints) > Convert.ToDouble(tempItem.FebPoints))
                            //            {
                            //                febFlag = false;
                            //                _febDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].FebPoints) < Convert.ToDouble(tempItem.FebPoints))
                            //            {
                            //                febFlag = false;
                            //                _febDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (febFlag == false)
                            //                {
                            //                    febFlag = true;
                            //                    _febDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }
                            //        // for mar
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].MarPoints) > Convert.ToDouble(tempItem.MarPoints))
                            //            {
                            //                marFlag = false;
                            //                _marDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].MarPoints) < Convert.ToDouble(tempItem.MarPoints))
                            //            {
                            //                marFlag = false;
                            //                _marDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (marFlag == false)
                            //                {
                            //                    marFlag = true;
                            //                    _marDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for apr
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].AprPoints) > Convert.ToDouble(tempItem.AprPoints))
                            //            {
                            //                aprFlag = false;
                            //                _aprDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].AprPoints) < Convert.ToDouble(tempItem.AprPoints))
                            //            {
                            //                aprFlag = false;
                            //                _aprDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (aprFlag == false)
                            //                {
                            //                    aprFlag = true;
                            //                    _aprDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for may
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].MayPoints) > Convert.ToDouble(tempItem.MayPoints))
                            //            {
                            //                mayFlag = false;
                            //                _mayDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].MayPoints) < Convert.ToDouble(tempItem.MayPoints))
                            //            {
                            //                mayFlag = false;
                            //                _mayDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (mayFlag == false)
                            //                {
                            //                    mayFlag = true;
                            //                    _mayDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for jun
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].JunPoints) > Convert.ToDouble(tempItem.JunPoints))
                            //            {
                            //                junFlag = false;
                            //                _junDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].JunPoints) < Convert.ToDouble(tempItem.JunPoints))
                            //            {
                            //                junFlag = false;
                            //                _junDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (junFlag == false)
                            //                {
                            //                    junFlag = true;
                            //                    _junDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }
                            //        // for jul
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].JulPoints) > Convert.ToDouble(tempItem.JulPoints))
                            //            {
                            //                julFlag = false;
                            //                _julDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].JulPoints) < Convert.ToDouble(tempItem.JulPoints))
                            //            {
                            //                julFlag = false;
                            //                _julDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (julFlag == false)
                            //                {
                            //                    julFlag = true;
                            //                    _julDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //        // for aug
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].AugPoints) > Convert.ToDouble(tempItem.AugPoints))
                            //            {
                            //                augFlag = false;
                            //                _augDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].AugPoints) < Convert.ToDouble(tempItem.AugPoints))
                            //            {
                            //                augFlag = false;
                            //                _augDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (augFlag == false)
                            //                {
                            //                    augFlag = true;
                            //                    _augDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }
                            //        // for sep
                            //        {
                            //            if (Convert.ToDouble(filteredByEmployeeId[i].SepPoints) > Convert.ToDouble(tempItem.SepPoints))
                            //            {
                            //                sepFlag = false;
                            //                _sepDeptId.Add(Convert.ToInt32(filteredByEmployeeId[i].DepartmentId));
                            //            }
                            //            else if (Convert.ToDouble(filteredByEmployeeId[i].SepPoints) < Convert.ToDouble(tempItem.SepPoints))
                            //            {
                            //                sepFlag = false;
                            //                _sepDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));
                            //            }
                            //            else
                            //            {
                            //                if (sepFlag == false)
                            //                {
                            //                    sepFlag = true;
                            //                    _sepDeptId.Add(Convert.ToInt32(tempItem.DepartmentId));

                            //                }

                            //            }
                            //        }

                            //    }
                            //}
                        }

                        if (_octDeptId.Count > 0)
                        {
                            var val = _octDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.OctCount++;
                        }
                        if (_novDeptId.Count > 0)
                        {
                            var val = _novDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.NovCount++;
                        }
                        if (_decDeptId.Count > 0)
                        {
                            var val = _decDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.DecCount++;
                        }
                        if (_janDeptId.Count > 0)
                        {
                            var val = _janDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JanCount++;
                        }
                        if (_febDeptId.Count > 0)
                        {
                            var val = _febDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.FebCount++;
                        }
                        if (_marDeptId.Count > 0)
                        {
                            var val = _marDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.MarCount++;
                        }
                        if (_aprDeptId.Count > 0)
                        {
                            var val = _aprDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.AprCount++;
                        }
                        if (_mayDeptId.Count > 0)
                        {
                            var val = _mayDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.MayCount++;
                        }
                        if (_junDeptId.Count > 0)
                        {
                            var val = _junDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JunCount++;
                        }
                        if (_julDeptId.Count > 0)
                        {
                            var val = _julDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.JulCount++;
                        }
                        if (_augDeptId.Count > 0)
                        {
                            var val = _augDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.AugCount++;
                        }
                        if (_sepDeptId.Count > 0)
                        {
                            var val = _sepDeptId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.DepartmentId == val).SingleOrDefault();
                            singleHeadCount.SepCount++;
                        }
                    }
                }

            }
            return Ok(_headCountList);
        }

        [HttpGet]
        [Route("api/utilities/GetHeadCountByIncharges/")]
        public IHttpActionResult GetHeadCountByIncharges(string companiIds)
        {

            int year = 0;
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            //List<Department> departments = departmentBLL.GetAllDepartments();
            List<InCharge> inCharges = inChargeBLL.GetAllInCharges();
            List<SubCategory> subCategories = departmentBLL.GetAllSubCategories();
            List<HeadCountInner> _headCountList = new List<HeadCountInner>();
            List<ForecastAssignmentViewModel> _allforecastAssignmentViewModels = new List<ForecastAssignmentViewModel>();
            foreach (var incharge in inCharges)
            {
                //if (department.Id == 8)
                //{
                //    continue;
                //}

                //var subCategory = subCategories.Where(sc => sc.Id == Convert.ToInt32(department.SubCategoryId)).SingleOrDefault();

                _headCountList.Add(new HeadCountInner
                {
                    InchargeId = incharge.Id,
                    DepartmentName = incharge.InChargeName,
                    //CategoryName = subCategory.CategoryName,
                    //SubCategoryName = subCategory.SubCategoryName,
                    OctCount = 0,
                    NovCount = 0,
                    DecCount = 0,
                    JanCount = 0,
                    FebCount = 0,
                    MarCount = 0,
                    AprCount = 0,
                    MayCount = 0,
                    JunCount = 0,
                    JulCount = 0,
                    AugCount = 0,
                    SepCount = 0,
                });

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByIncharge_Company(incharge.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    _allforecastAssignmentViewModels.AddRange(forecastAssignmentViewModels);
                }
            }

            if (_allforecastAssignmentViewModels.Count > 0)
            {
                var _uniqueItemList = _allforecastAssignmentViewModels.GroupBy(x => x.EmployeeId).Select(x => x.First()).ToList();
                var _uniqueEmployeeIdList = _uniqueItemList.Select(x => x.EmployeeId).ToList();
                foreach (var employeeId in _uniqueEmployeeIdList)
                {
                    var filteredByEmployeeId = _allforecastAssignmentViewModels.Where(x => x.EmployeeId == employeeId).ToList();
                    if (filteredByEmployeeId.Count == 1)
                    {
                        foreach (var item in filteredByEmployeeId)
                        {
                            var getSingleInchargetHeadCount = _headCountList.Where(h => h.InchargeId == Convert.ToInt32(item.InchargeId)).SingleOrDefault();
                            if (Convert.ToDouble(item.OctPoints) > 0)
                            {
                                getSingleInchargetHeadCount.OctCount += 1;
                            }
                            if (Convert.ToDouble(item.NovPoints) > 0)
                            {
                                getSingleInchargetHeadCount.NovCount += 1;
                            }
                            if (Convert.ToDouble(item.DecPoints) > 0)
                            {
                                getSingleInchargetHeadCount.DecCount += 1;
                            }
                            if (Convert.ToDouble(item.JanPoints) > 0)
                            {
                                getSingleInchargetHeadCount.JanCount += 1;
                            }
                            if (Convert.ToDouble(item.FebPoints) > 0)
                            {
                                getSingleInchargetHeadCount.FebCount += 1;
                            }
                            if (Convert.ToDouble(item.MarPoints) > 0)
                            {
                                getSingleInchargetHeadCount.MarCount += 1;
                            }
                            if (Convert.ToDouble(item.AprPoints) > 0)
                            {
                                getSingleInchargetHeadCount.AprCount += 1;
                            }
                            if (Convert.ToDouble(item.MayPoints) > 0)
                            {
                                getSingleInchargetHeadCount.MayCount += 1;
                            }
                            if (Convert.ToDouble(item.JunPoints) > 0)
                            {
                                getSingleInchargetHeadCount.JunCount += 1;
                            }
                            if (Convert.ToDouble(item.JulPoints) > 0)
                            {
                                getSingleInchargetHeadCount.JulCount += 1;
                            }
                            if (Convert.ToDouble(item.AugPoints) > 0)
                            {
                                getSingleInchargetHeadCount.AugCount += 1;
                            }
                            if (Convert.ToDouble(item.SepPoints) > 0)
                            {
                                getSingleInchargetHeadCount.SepCount += 1;
                            }

                        }


                    }
                    else if (filteredByEmployeeId.Count > 1)
                    {
                        List<int> _octInchargeId = new List<int>();
                        List<int> _novInchargeId = new List<int>();
                        List<int> _decInchargeId = new List<int>();
                        List<int> _janInchargeId = new List<int>();
                        List<int> _febInchargeId = new List<int>();
                        List<int> _marInchargeId = new List<int>();
                        List<int> _aprInchargeId = new List<int>();
                        List<int> _mayInchargeId = new List<int>();
                        List<int> _junInchargeId = new List<int>();
                        List<int> _julInchargeId = new List<int>();
                        List<int> _augInchargeId = new List<int>();
                        List<int> _sepInchargeId = new List<int>();
                        bool octFlag = false, novFlag = false, decFlag = false, janFlag = false, febFlag = false, marFlag = false, aprFlag = false, mayFlag = false, junFlag = false, julFlag = false, augFlag = false, sepFlag = false;
                        List<ForecastAssignmentViewModel> _tempArray = new List<ForecastAssignmentViewModel>();
                        for (int i = 0; i < filteredByEmployeeId.Count; i++)
                        {
                            if (_tempArray.Count == 0)
                            {
                                _tempArray.Add(filteredByEmployeeId[i]);
                            }
                            else
                            {


                                foreach (var tempItem in _tempArray)
                                {
                                    // for oct
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].OctPoints) > Convert.ToDouble(tempItem.OctPoints))
                                        {
                                            octFlag = false;
                                            _octInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].OctPoints) < Convert.ToDouble(tempItem.OctPoints))
                                        {
                                            octFlag = false;
                                            _octInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (octFlag == false)
                                            {
                                                octFlag = true;
                                                _octInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for nov
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].NovPoints) > Convert.ToDouble(tempItem.NovPoints))
                                        {
                                            novFlag = false;
                                            _novInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].NovPoints) < Convert.ToDouble(tempItem.NovPoints))
                                        {
                                            novFlag = false;
                                            _novInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (novFlag == false)
                                            {
                                                novFlag = true;
                                                _novInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for dec
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].DecPoints) > Convert.ToDouble(tempItem.DecPoints))
                                        {
                                            decFlag = false;
                                            _decInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].DecPoints) < Convert.ToDouble(tempItem.DecPoints))
                                        {
                                            decFlag = false;
                                            _decInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (decFlag == false)
                                            {
                                                decFlag = true;
                                                _decInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for jan
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].JanPoints) > Convert.ToDouble(tempItem.JanPoints))
                                        {
                                            janFlag = false;
                                            _janInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].JanPoints) < Convert.ToDouble(tempItem.JanPoints))
                                        {
                                            janFlag = false;
                                            _janInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (janFlag == false)
                                            {
                                                janFlag = true;
                                                _janInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for feb
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].FebPoints) > Convert.ToDouble(tempItem.FebPoints))
                                        {
                                            febFlag = false;
                                            _febInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].FebPoints) < Convert.ToDouble(tempItem.FebPoints))
                                        {
                                            febFlag = false;
                                            _febInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (febFlag == false)
                                            {
                                                febFlag = true;
                                                _febInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }
                                    // for mar
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].MarPoints) > Convert.ToDouble(tempItem.MarPoints))
                                        {
                                            marFlag = false;
                                            _marInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].MarPoints) < Convert.ToDouble(tempItem.MarPoints))
                                        {
                                            marFlag = false;
                                            _marInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (marFlag == false)
                                            {
                                                marFlag = true;
                                                _marInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for apr
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].AprPoints) > Convert.ToDouble(tempItem.AprPoints))
                                        {
                                            aprFlag = false;
                                            _aprInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].AprPoints) < Convert.ToDouble(tempItem.AprPoints))
                                        {
                                            aprFlag = false;
                                            _aprInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (aprFlag == false)
                                            {
                                                aprFlag = true;
                                                _aprInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for may
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].MayPoints) > Convert.ToDouble(tempItem.MayPoints))
                                        {
                                            mayFlag = false;
                                            _mayInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].MayPoints) < Convert.ToDouble(tempItem.MayPoints))
                                        {
                                            mayFlag = false;
                                            _mayInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (mayFlag == false)
                                            {
                                                mayFlag = true;
                                                _mayInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for jun
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].JunPoints) > Convert.ToDouble(tempItem.JunPoints))
                                        {
                                            junFlag = false;
                                            _junInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].JunPoints) < Convert.ToDouble(tempItem.JunPoints))
                                        {
                                            junFlag = false;
                                            _junInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (junFlag == false)
                                            {
                                                junFlag = true;
                                                _junInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }
                                    // for jul
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].JulPoints) > Convert.ToDouble(tempItem.JulPoints))
                                        {
                                            julFlag = false;
                                            _julInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].JulPoints) < Convert.ToDouble(tempItem.JulPoints))
                                        {
                                            julFlag = false;
                                            _julInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (julFlag == false)
                                            {
                                                julFlag = true;
                                                _julInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                    // for aug
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].AugPoints) > Convert.ToDouble(tempItem.AugPoints))
                                        {
                                            augFlag = false;
                                            _augInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].AugPoints) < Convert.ToDouble(tempItem.AugPoints))
                                        {
                                            augFlag = false;
                                            _augInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (augFlag == false)
                                            {
                                                augFlag = true;
                                                _augInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }
                                    // for sep
                                    {
                                        if (Convert.ToDouble(filteredByEmployeeId[i].SepPoints) > Convert.ToDouble(tempItem.SepPoints))
                                        {
                                            sepFlag = false;
                                            _sepInchargeId.Add(Convert.ToInt32(filteredByEmployeeId[i].InchargeId));
                                        }
                                        else if (Convert.ToDouble(filteredByEmployeeId[i].SepPoints) < Convert.ToDouble(tempItem.SepPoints))
                                        {
                                            sepFlag = false;
                                            _sepInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));
                                        }
                                        else
                                        {
                                            if (sepFlag == false)
                                            {
                                                sepFlag = true;
                                                _sepInchargeId.Add(Convert.ToInt32(tempItem.InchargeId));

                                            }

                                        }
                                    }

                                }
                            }
                        }

                        if (_octInchargeId.Count > 0)
                        {
                            var val = _octInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.OctCount++;
                        }
                        if (_novInchargeId.Count > 0)
                        {
                            var val = _novInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.NovCount++;
                        }
                        if (_decInchargeId.Count > 0)
                        {
                            var val = _decInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.DecCount++;
                        }
                        if (_janInchargeId.Count > 0)
                        {
                            var val = _janInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.JanCount++;
                        }
                        if (_febInchargeId.Count > 0)
                        {
                            var val = _febInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.FebCount++;
                        }
                        if (_marInchargeId.Count > 0)
                        {
                            var val = _marInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.MarCount++;
                        }
                        if (_aprInchargeId.Count > 0)
                        {
                            var val = _aprInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.AprCount++;
                        }
                        if (_mayInchargeId.Count > 0)
                        {
                            var val = _mayInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.MayCount++;
                        }
                        if (_junInchargeId.Count > 0)
                        {
                            var val = _junInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.JunCount++;
                        }
                        if (_julInchargeId.Count > 0)
                        {
                            var val = _julInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.JulCount++;
                        }
                        if (_augInchargeId.Count > 0)
                        {
                            var val = _augInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.AugCount++;
                        }
                        if (_sepInchargeId.Count > 0)
                        {
                            var val = _sepInchargeId.LastOrDefault();
                            var singleHeadCount = _headCountList.Where(d => d.InchargeId == val).SingleOrDefault();
                            singleHeadCount.SepCount++;
                        }
                    }
                }

            }
            return Ok(_headCountList);
        }

        class HeadCountInner
        {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }
            public int InchargeId { get; set; }
            public string InchagreName { get; set; }
            public string CategoryName { get; set; }
            public string SubCategoryName { get; set; }
            public double OctCount { get; set; }
            public double NovCount { get; set; }
            public double DecCount { get; set; }
            public double JanCount { get; set; }
            public double FebCount { get; set; }
            public double MarCount { get; set; }
            public double AprCount { get; set; }
            public double MayCount { get; set; }
            public double JunCount { get; set; }
            public double JulCount { get; set; }
            public double AugCount { get; set; }
            public double SepCount { get; set; }
        }

        [HttpGet]
        [Route("api/utilities/SearchForApprovalEmployee/")]
        public IHttpActionResult SearchForApprovalEmployee(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status, string year, string timeStampId)
        {
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();

            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }
            if (!string.IsNullOrEmpty(sectionId))
            {
                employeeAssignment.SectionId = sectionId;
            }
            else
            {
                employeeAssignment.SectionId = "";
            }
            if (!string.IsNullOrEmpty(departmentId))
            {
                employeeAssignment.DepartmentId = departmentId;
            }
            else
            {
                employeeAssignment.DepartmentId = "";
            }
            if (!string.IsNullOrEmpty(inchargeId))
            {
                employeeAssignment.InchargeId = inchargeId;
            }
            else
            {
                employeeAssignment.InchargeId = "";
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                employeeAssignment.RoleId = roleId;
            }
            else
            {
                employeeAssignment.RoleId = "";
            }
            employeeAssignment.ExplanationId = explanationId;
            if (!string.IsNullOrEmpty(companyId))
            {
                employeeAssignment.CompanyId = companyId;
            }
            else
            {
                employeeAssignment.CompanyId = "";
            }

            if (!string.IsNullOrEmpty(year))
            {
                employeeAssignment.Year = year;
            }
            else
            {
                employeeAssignment.Year = "";
            }

            if (!string.IsNullOrEmpty(status))
            {
                employeeAssignment.IsActive = status;
            }
            else
            {
                employeeAssignment.IsActive = "";
            }
            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetAllAssignmentData(employeeAssignment);
            

            return Ok(forecsatEmployeeAssignmentViewModels);
        }

        [HttpGet]
        [Route("api/utilities/UpdateApprovedData/")]
        public IHttpActionResult UpdateApprovedData(string assignmentYear, string historyName, string approvalCellsWithAssignmentId, string approvedRows)
        {
            int results = 0;
            int updateResults = 0;

            //approve history: start
            var session = System.Web.HttpContext.Current.Session;
            string createdBy = session["userName"].ToString();
            DateTime createdDate = DateTime.Now;
            int approveTimeStampId = 0;

            List<AssignmentHistory> _assignmentHistories_Add = new List<AssignmentHistory>();

            List<AssignmentHistory> _assignmentHistorys_Delete = new List<AssignmentHistory>();

            //Add Employee And Delete Employee Approve: Start   
            if (!string.IsNullOrEmpty(approvedRows))
            {
                var arrApprovalRowIds = approvedRows.Split(',');
                foreach (var approvedRowId in arrApprovalRowIds)
                {
                    if (!string.IsNullOrEmpty(approvedRowId))
                    {
                        EmployeeAssignment employeeAssignment = forecastBLL.GetAssignmentDetailsById(Convert.ToInt32(approvedRowId), Convert.ToInt32(assignmentYear));
                        AssignmentHistory assignmentHistory_add = new AssignmentHistory();
                        AssignmentHistory assignmentHistory_delete = new AssignmentHistory();

                        //new row approved and deleted row approved: start
                        if ((employeeAssignment.BCYR && Convert.ToBoolean(employeeAssignment.IsActive)) || employeeAssignment.IsRowPending)
                        {
                            updateResults = employeeAssignmentBLL.UpdateApprovedRowByAssignmentId(Convert.ToInt32(approvedRowId));
                            assignmentHistory_add = forecastBLL.GetAddEmployeeApprovedData(Convert.ToInt32(approvedRowId));
                            _assignmentHistories_Add.Add(assignmentHistory_add);

                        }
                        else if ((!Convert.ToBoolean(employeeAssignment.IsActive) && !employeeAssignment.IsDeleted) || employeeAssignment.IsDeletePending)
                        {
                            updateResults = employeeAssignmentBLL.UpdateDeletedRowByAssignmentId(Convert.ToInt32(approvedRowId));
                            assignmentHistory_delete = forecastBLL.GetDeleteEmployeeApprovedData(Convert.ToInt32(approvedRowId));
                            _assignmentHistorys_Delete.Add(assignmentHistory_delete);

                        }
                        //new row approved and deleted row approved: end   
                    }
                }
            }
            //Add Employee And Delete Employee Approve: End

            //Cell Wise Approve: Start
            string approvedCellAssignmentId = "";
            string approvedCellNo = "";
            List<AssignmentHistory> _assignmentHistorys_CellWise = new List<AssignmentHistory>();

            //update cells: start            
            if (!string.IsNullOrEmpty(approvalCellsWithAssignmentId))
            {
                var arrCellWithAssignmentids = approvalCellsWithAssignmentId.Split(',');
                foreach (var cellAndAssignmentIdItem in arrCellWithAssignmentids)
                {
                    var arrCellAndAssignmentId = cellAndAssignmentIdItem.Split('_');
                    if (!string.IsNullOrEmpty(arrCellAndAssignmentId[0].ToString()))
                    {
                        string updatedApprovedCells = "";
                        string updatePendingCells = "";
                        int removeResults = employeeAssignmentBLL.RemoveApprovedDataFromOriginalTable(Convert.ToInt32(arrCellAndAssignmentId[0]), Convert.ToInt32(arrCellAndAssignmentId[1]));

                        EmployeeAssignment employeeAssignment = forecastBLL.GetAssignmentDetailsById(Convert.ToInt32(arrCellAndAssignmentId[0]), Convert.ToInt32(assignmentYear));

                        //cell wise history
                        AssignmentHistory assignmentHistory_cell = forecastBLL.GetCellWiseEmployeeApprovedData(Convert.ToInt32(arrCellAndAssignmentId[0]), Convert.ToInt32(assignmentYear), Convert.ToInt32(arrCellAndAssignmentId[1]));

                        if (_assignmentHistorys_CellWise.Count > 0)
                        {
                            string tempCellNo = "";

                            foreach (var checkSameAssignmentId in _assignmentHistorys_CellWise)
                            {
                                if (checkSameAssignmentId.Id == assignmentHistory_cell.Id)
                                {
                                    if (!string.IsNullOrEmpty(checkSameAssignmentId.ApprovedCells))
                                    {
                                        tempCellNo = checkSameAssignmentId.ApprovedCells + "," + assignmentHistory_cell.ApprovedCells;
                                        checkSameAssignmentId.ApprovedCells = tempCellNo;
                                    }
                                }
                            }
                            if (string.IsNullOrEmpty(tempCellNo))
                            {
                                _assignmentHistorys_CellWise.Add(assignmentHistory_cell);
                            }
                        }
                        else
                        {
                            _assignmentHistorys_CellWise.Add(assignmentHistory_cell);
                        }

                        if (string.IsNullOrEmpty(approvedCellAssignmentId))
                        {
                            approvedCellAssignmentId = arrCellAndAssignmentId[0];
                        }
                        else
                        {

                        }
                        if (string.IsNullOrEmpty(approvedCellNo))
                        {

                        }

                        if (!string.IsNullOrEmpty((employeeAssignment.BCYRCell)))
                        {
                            var arrPendingCells = employeeAssignment.BCYRCell.Split(',');
                            foreach (var item in arrPendingCells)
                            {
                                if (item != arrCellAndAssignmentId[1])
                                {
                                    if (string.IsNullOrEmpty(updatedApprovedCells))
                                    {
                                        updatedApprovedCells = item;
                                    }
                                    else
                                    {
                                        updatedApprovedCells = updatedApprovedCells + "," + item;
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty((employeeAssignment.BCYRCellPending)))
                        {
                            var arrPendingCells = employeeAssignment.BCYRCellPending.Split(',');
                            foreach (var item in arrPendingCells)
                            {
                                if (item != arrCellAndAssignmentId[1])
                                {
                                    if (string.IsNullOrEmpty(updatePendingCells))
                                    {
                                        updatePendingCells = item;
                                    }
                                    else
                                    {
                                        updatePendingCells = updatePendingCells + "," + item;
                                    }
                                }
                            }
                            //int updateResults = employeeAssignmentBLL.UpdateCellsByAssignmentid(updatedApprovedCells, Convert.ToInt32(arrCellAndAssignmentId[0]));
                        }

                        updateResults = employeeAssignmentBLL.UpdateCellsByAssignmentid(updatedApprovedCells, updatePendingCells, Convert.ToInt32(arrCellAndAssignmentId[0]));
                        if (updateResults > 0)
                        {
                            results = 1;
                        }
                    }
                }
            }
            //update cells: end

            //cells and row/delete pending data update
            if (!string.IsNullOrEmpty(approvalCellsWithAssignmentId) || !string.IsNullOrEmpty(approvedRows))
            {
                //update all the un-approved row data.
                int unapprovedRowResults = employeeAssignmentBLL.UpdateUnapprovedPendingRows(Convert.ToInt32(assignmentYear));
                //update all the un-approved deleted data.
                int unapprovedDeleteResults = employeeAssignmentBLL.UpdateUnapprovedPendingDeleteRows(Convert.ToInt32(assignmentYear));
                //row wise update: end

                //pending cell update
                List<EmployeeAssignment> employeeAssignments = forecastBLL.GetAllUnapprovalDataForCells(Convert.ToInt32(assignmentYear));
                if (employeeAssignments.Count > 0)
                {
                    foreach (var updateItem in employeeAssignments)
                    {
                        string udpatePendingCellsAfterSave = "";
                        if (!string.IsNullOrEmpty(updateItem.BCYRCell))
                        {
                            if (!string.IsNullOrEmpty(updateItem.BCYRCellPending))
                            {
                                //bool isCellAlreadyExists = false;

                                //var arrBCYRCellPending = updateItem.BCYRCellPending.Split(',');
                                //foreach(var pendingItem in arrBCYRCellPending)
                                //{

                                //}

                                udpatePendingCellsAfterSave = updateItem.BCYRCellPending + "," + updateItem.BCYRCell;
                            }
                            else
                            {
                                udpatePendingCellsAfterSave = updateItem.BCYRCell;
                            }
                            updateResults = employeeAssignmentBLL.UpdateCellsByAssignmentid("", udpatePendingCellsAfterSave, updateItem.Id);
                        }
                    }
                }

            }

            if (_assignmentHistories_Add.Count > 0 || _assignmentHistorys_Delete.Count > 0 || _assignmentHistorys_CellWise.Count > 0)
            {
                approveTimeStampId = forecastBLL.CreateApproveTimeStamp(historyName, Convert.ToInt32(assignmentYear), createdBy, createdDate);
                if (approveTimeStampId > 0)
                {
                    int approveResults = forecastBLL.CreateApprovetHistory(approveTimeStampId, Convert.ToInt32(assignmentYear), createdBy, _assignmentHistories_Add, _assignmentHistorys_Delete, _assignmentHistorys_CellWise);
                }
                //store approved row for download excel
                if (_assignmentHistories_Add.Count > 0)
                {
                    foreach (var addRowItem in _assignmentHistories_Add)
                    {
                        int updateEmployeeAssignmentApprovedCellsResults = forecastBLL.UpdateEmployeeAssignmentApprovedRowByAssignmentId(addRowItem);
                    }
                }

                //store approved cell for download excel 
                if (_assignmentHistorys_CellWise.Count > 0)
                {
                    foreach (var cellWiseEmployeeItem in _assignmentHistorys_CellWise)
                    {
                        string tempApprovedCells = forecastBLL.GetApprovedCellsByAssignmentId(cellWiseEmployeeItem.EmployeeAssignmentId);
                        cellWiseEmployeeItem.ApprovedCells = cellWiseEmployeeItem.ApprovedCells + "," + tempApprovedCells;
                        var storeApprovedCells = "";
                        if (!string.IsNullOrEmpty(cellWiseEmployeeItem.ApprovedCells))
                        {
                            var arrApprovedCells = cellWiseEmployeeItem.ApprovedCells.Split(',');
                            foreach (var approvedCellItem in arrApprovedCells)
                            {
                                if (string.IsNullOrEmpty(storeApprovedCells))
                                {
                                    storeApprovedCells = approvedCellItem;
                                }
                                else
                                {
                                    var arrCheckIfTheCellsAlreadyExists = storeApprovedCells.Split(',');
                                    foreach (var indexItem in arrCheckIfTheCellsAlreadyExists)
                                    {
                                        if (indexItem != approvedCellItem)
                                        {
                                            storeApprovedCells = storeApprovedCells + "," + approvedCellItem;
                                        }
                                    }
                                }
                            }
                        }
                        cellWiseEmployeeItem.ApprovedCells = storeApprovedCells;
                        int updateEmployeeAssignmentApprovedCellsResults = forecastBLL.UpdateEmployeeAssignmentApprovedCellsByAssignmentId(cellWiseEmployeeItem);
                    }
                }
            }
            //approve history: end
            var insertResults = 0;
            var cleanResults = 0;
            if (approveTimeStampId > 0)
            {
                insertResults = forecastBLL.InsertApprovedForecastedDataByYear(approveTimeStampId, Convert.ToInt32(assignmentYear), session["userName"].ToString());
                if (_assignmentHistories_Add.Count > 0 || _assignmentHistorys_Delete.Count > 0 || _assignmentHistorys_CellWise.Count > 0)
                {
                    //approved row
                    if (_assignmentHistories_Add.Count > 0)
                    {
                        foreach (var addRowItem in _assignmentHistories_Add)
                        {
                            int updateEmployeeAssignmentApprovedCellsResults = forecastBLL.UpdateApprovedData_AddRow(addRowItem, approveTimeStampId, assignmentYear);
                        }
                    }

                    //approved delete
                    if (_assignmentHistorys_Delete.Count > 0)
                    {
                        foreach (var addRowItem in _assignmentHistorys_Delete)
                        {
                            int updateEmployeeAssignmentApprovedCellsResults = forecastBLL.UpdateApprovedData_DeleteRow(addRowItem, approveTimeStampId, assignmentYear);
                        }
                    }

                    //approved cells
                    if (!string.IsNullOrEmpty(approvalCellsWithAssignmentId))
                    {
                        var arrApprovalCellsWithAssignmentId = approvalCellsWithAssignmentId.Split(',');
                        foreach (var cellWithIdItem in arrApprovalCellsWithAssignmentId)
                        {
                            var arrCellWithId = cellWithIdItem.Split('_');
                            if (!string.IsNullOrEmpty(arrCellWithId[0].ToString()))
                            {
                                //get just updated cell if has
                                //update approved cell
                                var assignmentId = Convert.ToInt32(arrCellWithId[0]);
                                var approvedCells = Convert.ToInt32(arrCellWithId[1]);
                                var tempApprovedCells = approvedCells.ToString();

                                string justUpdatedApprovedCells = forecastBLL.GetApprovedCellsByTimestampId(assignmentId, approvedCells, approveTimeStampId, assignmentYear);
                                if (!string.IsNullOrEmpty(justUpdatedApprovedCells))
                                {
                                    tempApprovedCells = tempApprovedCells + "," + justUpdatedApprovedCells;
                                }

                                int tempResults = forecastBLL.UpdateApprovedCells(assignmentId, tempApprovedCells, approveTimeStampId, Convert.ToInt32(assignmentYear));
                            }
                        }
                    }

                    cleanResults = forecastBLL.CleanPreviousApprovedDeletedRows(Convert.ToInt32(assignmentYear), approveTimeStampId);
                }
            }

            if (results > 0 || updateResults > 0 || cleanResults > 0)
            {
                results = 1;
            }
            else
            {
                results = 0;
            }
            return Ok(results);
        }

        //[HttpGet]
        //[Route("api/utilities/DownloadHistoryData/")]
        //public HttpResponseMessage DownloadHistoryData(int timeStampId)
        ////public IHttpActionResult DownloadHistoryData(int timeStampId)
        //{
        //    List<object> forecastHistoryList = new List<object>();
        //    List<Forecast> historyList = forecastBLL.GetAssignmentHistoriesByTimeStampId(timeStampId);
        //    string timeStampName = forecastBLL.GetHistoryTimeStampName(timeStampId);

        //    List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
        //    if (distinctAssignmentId.Count > 0)
        //    {
        //        using (var package = new ExcelPackage())
        //        {

        //            var sheet = package.Workbook.Worksheets.Add("Sheet1");
        //            sheet.Cells["A1"].Value = "利用者";
        //            sheet.Cells["A1"].Style.Font.Bold = true;
        //            sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["B1"].Value = "従業員名(Emp)";
        //            sheet.Cells["B1"].Style.Font.Bold = true; ;
        //            sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["C1"].Value = "操作内容 (Type)";
        //            sheet.Cells["C1"].Style.Font.Bold = true;
        //            sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["D1"].Value = "Remaks";
        //            sheet.Cells["D1"].Style.Font.Bold = true;
        //            sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


        //            sheet.Cells["E1"].Value = "区分(Section)	";
        //            sheet.Cells["E1"].Style.Font.Bold = true;
        //            sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


        //            sheet.Cells["F1"].Value = "部署(Dept)";
        //            sheet.Cells["F1"].Style.Font.Bold = true;
        //            sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);


        //            sheet.Cells["G1"].Value = "担当作業(In chg)	";
        //            sheet.Cells["G1"].Style.Font.Bold = true;
        //            sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["H1"].Value = "役割(Role)";
        //            sheet.Cells["H1"].Style.Font.Bold = true;
        //            sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["I1"].Value = "説明(expl)";
        //            sheet.Cells["I1"].Style.Font.Bold = true;
        //            sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["J1"].Value = "会社(Com)	";
        //            sheet.Cells["J1"].Style.Font.Bold = true;
        //            sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["K1"].Value = "グレード(Grade)";
        //            sheet.Cells["K1"].Style.Font.Bold = true;
        //            sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["L1"].Value = "単価(Unit Price)	";
        //            sheet.Cells["L1"].Style.Font.Bold = true;
        //            sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["M1"].Value = "10";
        //            sheet.Cells["M1"].Style.Font.Bold = true;
        //            sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["N1"].Value = "11";
        //            sheet.Cells["N1"].Style.Font.Bold = true;
        //            sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["O1"].Value = "12";
        //            sheet.Cells["O1"].Style.Font.Bold = true;
        //            sheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["P1"].Value = "1";
        //            sheet.Cells["P1"].Style.Font.Bold = true;
        //            sheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["Q1"].Value = "2";
        //            sheet.Cells["Q1"].Style.Font.Bold = true;
        //            sheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["R1"].Value = "3";
        //            sheet.Cells["R1"].Style.Font.Bold = true;
        //            sheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["S1"].Value = "4";
        //            sheet.Cells["S1"].Style.Font.Bold = true;
        //            sheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["T1"].Value = "5";
        //            sheet.Cells["T1"].Style.Font.Bold = true;
        //            sheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["U1"].Value = "6";
        //            sheet.Cells["U1"].Style.Font.Bold = true;
        //            sheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["V1"].Value = "7";
        //            sheet.Cells["V1"].Style.Font.Bold = true;
        //            sheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["W1"].Value = "8";
        //            sheet.Cells["W1"].Style.Font.Bold = true;
        //            sheet.Cells["W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["W1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            sheet.Cells["X1"].Value = "9";
        //            sheet.Cells["X1"].Style.Font.Bold = true;
        //            sheet.Cells["X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //            sheet.Cells["X1"].Style.Fill.BackgroundColor.SetColor(Color.SkyBlue);

        //            int count = 2;
        //            foreach (var item in distinctAssignmentId)
        //            {
        //                AssignmentHistoryViewModal _assignmentHistoryViewModal = new AssignmentHistoryViewModal();
        //                AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
        //                _assignmentHistoryViewModal = forecastBLL.GetAssignmentNamesForHistory(item, timeStampId);

        //                var employeeName = _assignmentHistoryViewModal.EmployeeName;
        //                var sectionName = _assignmentHistoryViewModal.SectionName;
        //                var departmentName = _assignmentHistoryViewModal.DepartmentName;
        //                var inChargeName = _assignmentHistoryViewModal.InChargeName;
        //                var roleName = _assignmentHistoryViewModal.RoleName;
        //                var explanationName = _assignmentHistoryViewModal.ExplanationName;
        //                var companyName = _assignmentHistoryViewModal.CompanyName;
        //                var gradePoints = _assignmentHistoryViewModal.GradePoints;
        //                var unitPrice = _assignmentHistoryViewModal.UnitPrice;
        //                var remarks = _assignmentHistoryViewModal.Remarks;
        //                var isUpdate = _assignmentHistoryViewModal.IsUpdate;

        //                var tempList = historyList.Where(h => h.EmployeeAssignmentId == item).ToList();

        //                var octP = tempList.Where(p => p.Month == 10).SingleOrDefault().Points;
        //                var novP = tempList.Where(p => p.Month == 11).SingleOrDefault().Points;
        //                var decP = tempList.Where(p => p.Month == 12).SingleOrDefault().Points;
        //                var janP = tempList.Where(p => p.Month == 1).SingleOrDefault().Points;
        //                var febP = tempList.Where(p => p.Month == 2).SingleOrDefault().Points;
        //                var marP = tempList.Where(p => p.Month == 3).SingleOrDefault().Points;
        //                var aprP = tempList.Where(p => p.Month == 4).SingleOrDefault().Points;
        //                var mayP = tempList.Where(p => p.Month == 5).SingleOrDefault().Points;
        //                var junP = tempList.Where(p => p.Month == 6).SingleOrDefault().Points;
        //                var julP = tempList.Where(p => p.Month == 7).SingleOrDefault().Points;
        //                var augP = tempList.Where(p => p.Month == 8).SingleOrDefault().Points;
        //                var sepP = tempList.Where(p => p.Month == 9).SingleOrDefault().Points;

        //                var originalForecastData = forecastBLL.GetForecastsByAssignmentId(item);

        //                _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

        //                var octPOriginal = originalForecastData.Where(p => p.Month == 10).SingleOrDefault().Points;
        //                var novPOriginal = originalForecastData.Where(p => p.Month == 11).SingleOrDefault().Points;
        //                var decPOriginal = originalForecastData.Where(p => p.Month == 12).SingleOrDefault().Points;
        //                var janPOriginal = originalForecastData.Where(p => p.Month == 1).SingleOrDefault().Points;
        //                var febPOriginal = originalForecastData.Where(p => p.Month == 2).SingleOrDefault().Points;
        //                var marPOriginal = originalForecastData.Where(p => p.Month == 3).SingleOrDefault().Points;
        //                var aprPOriginal = originalForecastData.Where(p => p.Month == 4).SingleOrDefault().Points;
        //                var mayPOriginal = originalForecastData.Where(p => p.Month == 5).SingleOrDefault().Points;
        //                var junPOriginal = originalForecastData.Where(p => p.Month == 6).SingleOrDefault().Points;
        //                var julPOriginal = originalForecastData.Where(p => p.Month == 7).SingleOrDefault().Points;
        //                var augPOriginal = originalForecastData.Where(p => p.Month == 8).SingleOrDefault().Points;
        //                var sepPOriginal = originalForecastData.Where(p => p.Month == 9).SingleOrDefault().Points;

        //                if (isUpdate)
        //                {
        //                    sheet.Cells["A" + count].Value = historyList[0].CreatedBy;
        //                    sheet.Cells["B" + count].Value = employeeName;
        //                    sheet.Cells["C" + count].Value = "Updated";
        //                    sheet.Cells["D" + count].Value = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks;
        //                    sheet.Cells["E" + count].Value = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName;
        //                    sheet.Cells["F" + count].Value = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName;
        //                    sheet.Cells["G" + count].Value = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName;
        //                    sheet.Cells["H" + count].Value = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName;
        //                    sheet.Cells["I" + count].Value = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName;
        //                    sheet.Cells["J" + count].Value = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName;
        //                    sheet.Cells["K" + count].Value = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints;
        //                    sheet.Cells["L" + count].Value = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice;

        //                    sheet.Cells["M" + count].Value = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0");
        //                    sheet.Cells["N" + count].Value = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0");
        //                    sheet.Cells["O" + count].Value = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0");
        //                    sheet.Cells["P" + count].Value = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0");
        //                    sheet.Cells["Q" + count].Value = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0");
        //                    sheet.Cells["R" + count].Value = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0");
        //                    sheet.Cells["S" + count].Value = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0");
        //                    sheet.Cells["T" + count].Value = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0");
        //                    sheet.Cells["U" + count].Value = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0");
        //                    sheet.Cells["V" + count].Value = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0");
        //                    sheet.Cells["W" + count].Value = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0");
        //                    sheet.Cells["X" + count].Value = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0");


        //                    //forecastHistoryList.Add(new
        //                    //{
        //                    //    EmployeeName = employeeName,
        //                    //    IsUpdate = isUpdate,
        //                    //    //EmployeeName = employeeName == _objOriginalForecastedData.EmployeeName ? "" : "(" + employeeName + ") " + _objOriginalForecastedData.EmployeeName,
        //                    //    SectionName = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName,
        //                    //    DepartmentName = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName,
        //                    //    InChargeName = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName,
        //                    //    RoleName = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName,
        //                    //    ExplanationName = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName,
        //                    //    CompanyName = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName,
        //                    //    GradePoints = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints,
        //                    //    UnitPrice = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice,
        //                    //    Remarks = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks,
        //                    //    CreatedBy = historyList[0].CreatedBy,
        //                    //    OctPoints = octP == octPOriginal ? "" : "(" + octP.ToString("0.0") + ") " + octPOriginal.ToString("0.0"),
        //                    //    NovPoints = novP == novPOriginal ? "" : "(" + novP.ToString("0.0") + ") " + novPOriginal.ToString("0.0"),
        //                    //    DecPoints = decP == decPOriginal ? "" : "(" + decP.ToString("0.0") + ") " + decPOriginal.ToString("0.0"),
        //                    //    JanPoints = janP == janPOriginal ? "" : "(" + janP.ToString("0.0") + ") " + janPOriginal.ToString("0.0"),
        //                    //    FebPoints = febP == febPOriginal ? "" : "(" + febP.ToString("0.0") + ") " + febPOriginal.ToString("0.0"),
        //                    //    MarPoints = marP == marPOriginal ? "" : "(" + marP.ToString("0.0") + ") " + marPOriginal.ToString("0.0"),
        //                    //    AprPoints = aprP == aprPOriginal ? "" : "(" + aprP.ToString("0.0") + ") " + aprPOriginal.ToString("0.0"),
        //                    //    MayPoints = mayP == mayPOriginal ? "" : "(" + mayP.ToString("0.0") + ") " + mayPOriginal.ToString("0.0"),
        //                    //    JunPoints = junP == junPOriginal ? "" : "(" + junP.ToString("0.0") + ") " + junPOriginal.ToString("0.0"),
        //                    //    JulPoints = julP == julPOriginal ? "" : "(" + julP.ToString("0.0") + ") " + julPOriginal.ToString("0.0"),
        //                    //    AugPoints = augP == augPOriginal ? "" : "(" + augP.ToString("0.0") + ") " + augPOriginal.ToString("0.0"),
        //                    //    SepPoints = sepP == sepPOriginal ? "" : "(" + sepP.ToString("0.0") + ") " + sepPOriginal.ToString("0.0"),
        //                    //});
        //                }
        //                else
        //                {
        //                    sheet.Cells["A" + count].Value = historyList[0].CreatedBy;
        //                    sheet.Cells["B" + count].Value = employeeName;
        //                    sheet.Cells["C" + count].Value = "Inserted";
        //                    sheet.Cells["D" + count].Value = remarks == "" ? "" : remarks;
        //                    sheet.Cells["E" + count].Value = sectionName == "" ? "" : sectionName;
        //                    sheet.Cells["F" + count].Value = departmentName == "" ? "" : departmentName;
        //                    sheet.Cells["G" + count].Value = inChargeName == "" ? "" : inChargeName;
        //                    sheet.Cells["H" + count].Value = roleName == "" ? "" : roleName;
        //                    sheet.Cells["I" + count].Value = explanationName == "" ? "" : explanationName;
        //                    sheet.Cells["J" + count].Value = companyName == "" ? "" : companyName;
        //                    sheet.Cells["K" + count].Value = gradePoints == "0" ? "" : gradePoints;
        //                    sheet.Cells["L" + count].Value = unitPrice == "0" ? "" : unitPrice;

        //                    sheet.Cells["M" + count].Value = octP == 0 ? "" : octP.ToString("0.0");
        //                    sheet.Cells["N" + count].Value = novP == 0 ? "" : novP.ToString("0.0");
        //                    sheet.Cells["O" + count].Value = decP == 0 ? "" : decP.ToString("0.0");
        //                    sheet.Cells["P" + count].Value = janP == 0 ? "" : janP.ToString("0.0");
        //                    sheet.Cells["Q" + count].Value = febP == 0 ? "" : febP.ToString("0.0");
        //                    sheet.Cells["R" + count].Value = marP == 0 ? "" : marP.ToString("0.0");
        //                    sheet.Cells["S" + count].Value = aprP == 0 ? "" : aprP.ToString("0.0");
        //                    sheet.Cells["T" + count].Value = mayP == 0 ? "" : mayP.ToString("0.0");
        //                    sheet.Cells["U" + count].Value = junP == 0 ? "" : junP.ToString("0.0");
        //                    sheet.Cells["V" + count].Value = julP == 0 ? "" : julP.ToString("0.0");
        //                    sheet.Cells["W" + count].Value = augP == 0 ? "" : augP.ToString("0.0");
        //                    sheet.Cells["X" + count].Value = sepP == 0 ? "" : sepP.ToString("0.0");

        //                    //insert data udpate
        //                    //forecastHistoryList.Add(new
        //                    //{
        //                    //    EmployeeName = employeeName,
        //                    //    IsUpdate = isUpdate,
        //                    //    SectionName = sectionName == "" ? "" : sectionName,
        //                    //    DepartmentName = departmentName == "" ? "" : departmentName,
        //                    //    InChargeName = inChargeName == "" ? "" : inChargeName,
        //                    //    RoleName = roleName == "" ? "" : roleName,
        //                    //    ExplanationName = explanationName == "" ? "" : explanationName,
        //                    //    CompanyName = companyName == "" ? "" : companyName,
        //                    //    GradePoints = gradePoints == "0" ? "" : gradePoints,
        //                    //    UnitPrice = unitPrice == "0" ? "" : unitPrice,
        //                    //    Remarks = remarks == "" ? "" : remarks,
        //                    //    CreatedBy = historyList[0].CreatedBy,
        //                    //    OctPoints = octP == 0 ? "" : octP.ToString("0.0"),
        //                    //    NovPoints = novP == 0 ? "" : novP.ToString("0.0"),
        //                    //    DecPoints = decP == 0 ? "" : decP.ToString("0.0"),
        //                    //    JanPoints = janP == 0 ? "" : janP.ToString("0.0"),
        //                    //    FebPoints = febP == 0 ? "" : febP.ToString("0.0"),
        //                    //    MarPoints = marP == 0 ? "" : marP.ToString("0.0"),
        //                    //    AprPoints = aprP == 0 ? "" : aprP.ToString("0.0"),
        //                    //    MayPoints = mayP == 0 ? "" : mayP.ToString("0.0"),
        //                    //    JunPoints = junP == 0 ? "" : junP.ToString("0.0"),
        //                    //    JulPoints = julP == 0 ? "" : julP.ToString("0.0"),
        //                    //    AugPoints = augP == 0 ? "" : augP.ToString("0.0"),
        //                    //    SepPoints = sepP == 0 ? "" : sepP.ToString("0.0"),
        //                    //});
        //                }

        //                count++;
        //            }

        //            var excelData = package.GetAsByteArray();
        //            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            var fileName = timeStampName + ".xlsx";
        //            return File(excelData, contentType, fileName);
        //        }


        //    }

        //    return Ok(forecastHistoryList);
        //}

        [HttpGet]
        [Route("api/utilities/QaProportion/")]
        public IHttpActionResult QaProportion(int year)
        {
            var department = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            List<QaProportionViewModel> qaProportions = qaProportionBLL.SearchAssignmentByYear_Department(year, department.Id);
            return Ok(qaProportions);
        }
        [HttpGet]
        [Route("api/utilities/QaProportionDataByYear/")]
        public IHttpActionResult QaProportionDataByYear(int year)
        {
            var qaProportions = qaProportionBLL.GetQaProportionDataByYear(year);
            return Ok(qaProportions);
        }

        [HttpGet]
        [Route("api/utilities/QaAssignmentTotal/")]
        public IHttpActionResult QaAssignmentTotal(int year)
        {
            var department = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();
            List<object> qaAssignments = qaProportionBLL.SearchAssignmentByYear_Department_Data(year, department.Id);
            return Ok(qaAssignments);
        }

        [HttpPost]
        [Route("api/utilities/CreateQaProportion/")]
        public IHttpActionResult CreateQaProportion(QaProportionDto proportionDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (proportionDto.QaProportionViewModels.Count > 0)
            {
                foreach (var item in proportionDto.QaProportionViewModels)
                {

                    QaProportion qaProportion = new QaProportion();
                    qaProportion.Id = item.Id;
                    qaProportion.EmployeeId = item.EmployeeId;
                    qaProportion.DepartmentId = item.DepartmentId;
                    qaProportion.OctPercentage = item.OctPercentage;
                    qaProportion.NovPercentage = item.NovPercentage;
                    qaProportion.DecPercentage = item.DecPercentage;
                    qaProportion.JanPercentage = item.JanPercentage;
                    qaProportion.FebPercentage = item.FebPercentage;
                    qaProportion.MarPercentage = item.MarPercentage;
                    qaProportion.AprPercentage = item.AprPercentage;
                    qaProportion.MayPercentage = item.MayPercentage;
                    qaProportion.JunPercentage = item.JunPercentage;
                    qaProportion.JulPercentage = item.JulPercentage;
                    qaProportion.AugPercentage = item.AugPercentage;
                    qaProportion.SepPercentage = item.SepPercentage;
                    qaProportion.Year = proportionDto.Year;

                    if (qaProportion.Id > 0)
                    {
                        qaProportion.UpdatedBy = session["userName"].ToString();
                        qaProportion.UpdatedDate = DateTime.Now;
                        qaProportionBLL.UpdateQaProportion(qaProportion);
                    }
                    else
                    {
                        qaProportion.CreatedBy = session["userName"].ToString();
                        qaProportion.CreatedDate = DateTime.Now;
                        qaProportionBLL.CreateQaProportion(qaProportion);
                    }

                }
                return Ok("Operation Completed!");
            }
            else
            {
                return Ok("No data to update!");
            }

        }

        [HttpGet]
        [Route("api/utilities/GetFilteredDepartments/")]
        public IHttpActionResult GetFilteredDepartments()
        {
            List<Department> departments = new List<Department>();
            var deartmentList = departmentBLL.GetAllDepartments();

            foreach (var item in deartmentList)
            {
                if (item.DepartmentName == "品証")
                {
                    continue;
                }
                departments.Add(item);
            }

            return Ok(departments);
        }

        [HttpGet]
        [Route("api/utilities/CheckBudgetWithYear/")]
        public IHttpActionResult CheckBudgetWithYear(int BudgetYear)
        {
            BudgetImport _budgetAssignment = new BudgetImport();

            bool isInitialDataExists = departmentBLL.CheckForBudgetInitialDataExists(BudgetYear);
            _budgetAssignment.FirstHalfBudget = isInitialDataExists;
            bool isFirstHalfFinalize = false;
            if (isInitialDataExists)
            {
                isFirstHalfFinalize = departmentBLL.CheckForBudgetInitialDataFinalizeExists(BudgetYear);
            }
            _budgetAssignment.FirstHalfFinalize = isFirstHalfFinalize;

            bool isSecondHalfBudgetExists = departmentBLL.CheckForBudgetSecondHalfDataExists(BudgetYear);
            _budgetAssignment.SecondHalfBudget = isSecondHalfBudgetExists;
            bool isSecondtHalfFinalize = false;
            if (isSecondHalfBudgetExists)
            {
                isSecondtHalfFinalize = departmentBLL.CheckForBudgetSecondHalfDataFinalizeExists(BudgetYear);
            }
            _budgetAssignment.SecondHalfFinalize = isSecondtHalfFinalize;

            return Ok(_budgetAssignment);
        }

        [HttpGet]
        [Route("api/utilities/FinalizeBudgetAssignment/")]
        public IHttpActionResult FinalizeBudgetAssignment(string year)
        {
            var session = System.Web.HttpContext.Current.Session;
            bool isValidRequestForFinalize = false;
            int isFinalized = 0;
            int results = 0;
            if (!string.IsNullOrEmpty(year))
            {
                isValidRequestForFinalize = true;
            }
            if (isValidRequestForFinalize)
            {
                var arrYear = year.Split('_');
                isFinalized = employeeAssignmentBLL.FinalizeBudgetAssignment(Convert.ToInt32(arrYear[0]), Convert.ToInt32(arrYear[1]));
                if (isFinalized > 0)
                {
                    employeeAssignmentBLL.DeleteAssignment_PreviousFinalizeData(Convert.ToInt32(arrYear[0]));
                    employeeAssignmentBLL.DeletePreviousFinalBudgetData(Convert.ToInt32(arrYear[0]));

                    List<EmployeeBudget> _employeeAssignments = new List<EmployeeBudget>();
                    _employeeAssignments = employeeAssignmentBLL.GetFinalizedBudgetData(Convert.ToInt32(arrYear[0]), Convert.ToInt32(arrYear[1]));
                    

                    if (_employeeAssignments.Count > 0)
                    {

                        List<string> uniqueEmployeeNameList = new List<string>();

                        // get unique employee names
                        foreach (var assignmentItem in _employeeAssignments)
                        {
                            if (!uniqueEmployeeNameList.Contains(assignmentItem.EmployeeName))
                            {
                                uniqueEmployeeNameList.Add(assignmentItem.EmployeeName);
                            }
                        }
                        List<string> assignedEmployeeName = new List<string>();

                        foreach (string employeeName in uniqueEmployeeNameList)
                        {
                            if (!assignedEmployeeName.Contains(employeeName))
                            {
                                var _tempAssignmentItemList = _employeeAssignments.Where(emp=>emp.EmployeeName== employeeName).ToList();
                                List<string> _matchedItems = new List<string>();
                                foreach (var assignmentItem in _tempAssignmentItemList)
                                {
                                    EmployeeAssignment _assignmentData = new EmployeeAssignment();
                                    _assignmentData.Id = assignmentItem.Id;
                                    _assignmentData.EmployeeId = assignmentItem.EmployeeId;
                                    _assignmentData.SectionId = assignmentItem.SectionId;
                                    _assignmentData.SectionId = assignmentItem.SectionId;
                                    _assignmentData.DepartmentId = assignmentItem.DepartmentId;
                                    _assignmentData.DepartmentId = assignmentItem.DepartmentId;
                                    _assignmentData.InchargeId = assignmentItem.InchargeId;
                                    _assignmentData.RoleId = assignmentItem.RoleId;
                                    _assignmentData.ExplanationId = assignmentItem.ExplanationId;
                                    _assignmentData.CompanyId = assignmentItem.CompanyId;
                                    _assignmentData.GradeId = assignmentItem.GradeId;
                                    _assignmentData.GradeId = assignmentItem.GradeId;
                                    _assignmentData.UnitPrice = assignmentItem.UnitPrice;
                                    _assignmentData.CreatedBy = assignmentItem.CreatedBy;
                                    _assignmentData.CreatedDate = DateTime.Now;
                                    _assignmentData.Remarks = assignmentItem.Remarks;
                                    _assignmentData.SubCode = assignmentItem.SubCode;
                                    _assignmentData.Year = assignmentItem.Year;
                                    _assignmentData.BCYR = assignmentItem.BCYR;
                                    _assignmentData.BCYRCell = assignmentItem.BCYRCell;
                                    _assignmentData.EmployeeName = assignmentItem.EmployeeName;
                                    if (_matchedItems.Count > 0)
                                    {
                                        foreach (var item in _matchedItems)
                                        {
                                            var splittedString = item.Split('_');
                                            if (Convert.ToInt32(splittedString[0]) == Convert.ToInt32(assignmentItem.DuplicateFrom))
                                            {
                                                _assignmentData.DuplicateFrom = splittedString[1];
                                                break;
                                            }
                                        }
                                        
                                    }
                                    else
                                    {
                                        _assignmentData.DuplicateFrom = 0.ToString();
                                        
                                    }
                                    
                                    _assignmentData.DuplicateCount = assignmentItem.DuplicateCount;
                                    _assignmentData.RoleChanged = assignmentItem.RoleChanged;
                                    _assignmentData.UnitPriceChanged = assignmentItem.UnitPriceChanged;

                                    int finalBudgetAssignments = employeeAssignmentBLL.CreateFinalBudgetAssignment(_assignmentData);

                                    int assignmentCreateResults = employeeAssignmentBLL.CreateAssignment(_assignmentData);

                                    if (assignmentCreateResults == 1)
                                    {
                                        int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                                        int finalAssignmentId = employeeAssignmentBLL.GetFinalBudgetLastId();
                                        _matchedItems.Add(_assignmentData.Id.ToString()+"_"+ employeeAssignmentLastId);
                                        List<Forecast> forecasts = new List<Forecast>();
                                        forecasts = forecastBLL.GetBudgetForecastsByAssignmentId(_assignmentData.Id);
                                        foreach (var forecastItem in forecasts)
                                        {
                                            forecastItem.EmployeeAssignmentId = employeeAssignmentLastId;
                                            forecastItem.CreatedBy = session["userName"].ToString();
                                            results = forecastBLL.CreateForecast(forecastItem);

                                            forecastItem.EmployeeAssignmentId = finalAssignmentId;
                                            results = forecastBLL.CreateFinalBudgetForecast(forecastItem);
                                        }
                                    }
                                }
                            }

                            assignedEmployeeName.Add(employeeName);
                        }
                    }

                }
            }
            return Ok(results);
        }
        [HttpGet]
        [Route("api/utilities/CheckYearIfFinalize/")]
        public IHttpActionResult CheckYearIfFinalize(int select_year_type, int budgetReqType)
        {
            //check the year and budget type is finalize or not. if finalize then returns true.
            bool isFinalizeBudgetYear = false;
            isFinalizeBudgetYear = employeeAssignmentBLL.CheckYearIfFinalize(select_year_type, budgetReqType);

            return Ok(isFinalizeBudgetYear);
        }

        [HttpGet]
        [Route("api/utilities/CheckIsValidYearForImport/")]
        public IHttpActionResult CheckIsValidYearForImport(int select_year_type)
        {
            bool isValidYearForImport = false;
            if (select_year_type == 2023)
            {
                isValidYearForImport = true;
            }
            else
            {
                select_year_type = Convert.ToInt32(select_year_type) - 1;
                isValidYearForImport = employeeAssignmentBLL.CheckIsValidYearForImport(select_year_type);
            }

            return Ok(isValidYearForImport);
        }

        [HttpGet]
        [Route("api/utilities/GetTotalCalculationForManmonthAndCost/")]
        public IHttpActionResult GetTotalCalculationForManmonthAndCost(int year)
        {
            var result = employeeAssignmentBLL.GetTotalCalculationForManmonthAndCost(year);
            return Ok(result);
        }
        [HttpGet]
        [Route("api/utilities/GetTotalManMonthAndCostForBudgetEdit/")]
        public IHttpActionResult GetTotalManMonthAndCostForBudgetEdit(string yearWithType)
        {
            if (!string.IsNullOrEmpty(yearWithType))
            {
                var arrYearBudgetType = yearWithType.Split('_');
                if (!string.IsNullOrEmpty(arrYearBudgetType[0]))
                {
                    int year = Convert.ToInt32(arrYearBudgetType[0]);
                    int budgetType = Convert.ToInt32(arrYearBudgetType[1]);
                    var result = employeeAssignmentBLL.GetTotalManMonthAndCostForBudgetEdit(year, budgetType);
                    return Ok(result);
                }
                else
                {
                    return Ok("Budget Type is empty!");
                }
            }
            else
            {
                return Ok("Year is empty!");
            }

        }
        [HttpGet]
        [Route("api/utilities/GetEmployeeNameForMenuChange/")]
        public IHttpActionResult GetEmployeeNameForMenuChange(string employeeAssignmentId, int employeeId, string menuType, int year)
        {
            List<EmployeeAssignment> employeeAssignments = employeeAssignmentBLL.GetEmployeeNameForMenuChange(year, employeeId);
            List<EmployeeAssignment> _employeeAssignmentCount = employeeAssignmentBLL.GetDeletedEmployeeCount(year, employeeId);
            EmployeeAssignment singleEmployeeAssignment = new EmployeeAssignment();

            int employeeCount = 0;
            employeeCount = employeeAssignments.Count;

            string employeeName = "";
            foreach (var assignmentItem in employeeAssignments)
            {
                employeeName = assignmentItem.EmployeeRootName;
            }
            singleEmployeeAssignment.EmployeeName = employeeName;
            singleEmployeeAssignment.EmployeeCount = _employeeAssignmentCount.Count;

            if (!string.IsNullOrEmpty(menuType))
            {
                if (menuType.ToLower() == "unit")
                {
                    employeeName = employeeName + " (" + (employeeCount + 1) + ")";
                }
            }

            var results = singleEmployeeAssignment;
            //return Ok(employeeName);
            return Ok(results);
        }

        [Route("api/utilities/GetSubCategoryByCategoryId/{id}")]

        // have to check.......
        [HttpGet]
        [ActionName("GetSubCategoryByCategoryId")]
        public IHttpActionResult GetSubCategoryByCategoryId(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                DepartmentWithSubCategoryBLL departmentWithSubCategoryBLL = new DepartmentWithSubCategoryBLL();

                int tempValue = 0;
                if (int.TryParse(id, out tempValue))
                {
                    if (tempValue > 0)
                    {
                        List<SubCategory> subCategories = departmentWithSubCategoryBLL.GetSubCategoryByCategoryId(Convert.ToInt32(id));
                        return Ok(subCategories);
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Category Id is empty!!!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetAllUnAssignedDepartments/")]
        public IHttpActionResult GetAllUnAssignedDepartments(string sub_categoryId)
        {
            if (!string.IsNullOrEmpty(sub_categoryId))
            {
                DepartmentWithSubCategoryBLL departmentWithSubCategoryBLL = new DepartmentWithSubCategoryBLL();

                int tempValue = 0;
                if (int.TryParse(sub_categoryId, out tempValue))
                {
                    if (tempValue > 0)
                    {
                        List<Department> departments = departmentWithSubCategoryBLL.GetAllUnassignedDepartments();
                        return Ok(departments);
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("sub-category Id is empty!!!");
            }
        }

        [HttpGet]
        [Route("api/utilities/DeleteBudgetAssignment/")]
        public IHttpActionResult DeleteBudgetAssignment(string assignementId)
        {
            if (!string.IsNullOrEmpty(assignementId))
            {
                employeeAssignmentBLL.RemoveBudgetAssignment(assignementId);
                return Ok("1");
            }
            else
            {
                return Ok("0");
            }
        }

        [HttpPost]
        [Route("api/utilities/CreateDynamicTable/")]
        public IHttpActionResult CreateDynamicTable(DynamicTable dynamicTable)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(dynamicTable.TableName))
            {
                return BadRequest("Table name empty!!!");
            }
            if (String.IsNullOrEmpty(dynamicTable.TableTitle))
            {
                return BadRequest("Table title empty!!!");
            }
            else
            {
                dynamicTable.CreatedBy = session["userName"].ToString();
                dynamicTable.CreatedDate = DateTime.Now;
                dynamicTable.IsActive = true;


                bool isExists = totalBLL.IsNameAndPositionExists(dynamicTable.TableName, dynamicTable.TablePosition, dynamicTable.Id, "add");
                if (isExists)
                {
                    return Ok(1);
                }
                else
                {
                    var result = totalBLL.CreateDynamicTable(dynamicTable);
                    if (result > 0)
                    {
                        return Ok("保存されました.");
                    }
                    else
                    {
                        return BadRequest("Something went wrong!!!");
                    }
                }
            }
        }

        [HttpGet]
        [Route("api/utilities/GetDynamicTables/")]
        public IHttpActionResult GetDynamicTables()
        {
            var result = totalBLL.GetAllDynamicTables();
            return Ok(result);

        }

        [HttpPost]
        [Route("api/utilities/UpdateDynamicTable/")]
        public IHttpActionResult UpdateDynamicTable(DynamicTable dynamicTable)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (String.IsNullOrEmpty(dynamicTable.TableName))
            {
                return BadRequest("Table name empty!!!");
            }
            if (String.IsNullOrEmpty(dynamicTable.TableTitle))
            {
                return BadRequest("Table title empty!!!");
            }
            else
            {
                bool isExists = totalBLL.IsNameAndPositionExists(dynamicTable.TableName, dynamicTable.TablePosition, dynamicTable.Id, "edit");
                if (isExists)
                {
                    return BadRequest("Data already exists!");
                }
                else
                {
                    dynamicTable.UpdatedBy = session["userName"].ToString();
                    dynamicTable.UpdatedDate = DateTime.Now;
                    var result = totalBLL.UpdateDynamicTable(dynamicTable);
                    if (result > 0)
                    {
                        return Ok("データが保存されました.");
                    }
                    else
                    {
                        return BadRequest("Something went wrong!!!");
                    }
                }

            }
            //bool isExists = totalBLL.IsNameAndPositionExists(dynamicTable.TableName,dynamicTable.TablePosition,dynamicTable.Id,"edit");
            //if (isExists) {
            //    return Ok(1);
            //}
            //else
            //{
            //    var result = totalBLL.UpdateDynamicTable(dynamicTable);
            //    if (result > 0)
            //    {
            //        return Ok("データが保存されました.");
            //    }
            //    else
            //    {
            //        return BadRequest("Something went wrong!!!");
            //    }
            //}                
        }


        [HttpPost]
        [Route("api/utilities/RemoveDynamicTable/")]
        public IHttpActionResult RemoveDynamicTable(string tableId)
        {
            var session = System.Web.HttpContext.Current.Session;

            var singleDynamicTable = totalBLL.GetAllDynamicTables().Where(dt => dt.Id == Convert.ToInt32(tableId)).FirstOrDefault();

            if (singleDynamicTable != null)
            {

                var removedResult = totalBLL.RemoveDynamicTable(singleDynamicTable);
                if (removedResult > 0)
                {
                    return Ok("保存されました!");
                }
                else
                {
                    return BadRequest("Something went wrong.");
                }
            }
            else
            {
                return Ok("Data not found!");
            }
        }

        [HttpPost]
        [Route("api/utilities/DeleteDynamicTableSettings/")]
        public IHttpActionResult DeleteDynamicTableSettings(string tableId, string settingIds)
        {
            if (!string.IsNullOrEmpty(tableId))
            {
                if (!string.IsNullOrEmpty(settingIds))
                {
                    int resultsDelete = totalBLL.DeleteDynamicTableSettings(tableId, settingIds);
                    if (resultsDelete > 0)
                    {
                        return Ok("設定が削除されました.");
                    }
                    else
                    {
                        return BadRequest("Something went wrong.");
                    }
                }
                else
                {
                    return Ok("Data not found!");
                }
            }
            else
            {
                return Ok("Data not found!");
            }
        }
        [HttpPost]
        [Route("api/utilities/InsertUpdateDynamicSettings/")]
        public IHttpActionResult InsertUpdateDynamicSettings(string tableSettingsParameters, string tableId)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (!string.IsNullOrEmpty(tableSettingsParameters))
            {
                int finalResults = 0;

                var arrTableSettingParams = tableSettingsParameters.Split('-');
                foreach (var settingItem in arrTableSettingParams)
                {
                    if (!string.IsNullOrEmpty(settingItem))
                    {
                        string insertType = "";
                        DynamicSetting dynamicSetting = new DynamicSetting();
                        var arrSettingItem = settingItem.Split('_');
                        dynamicSetting.Id = Convert.ToInt32(arrSettingItem[0]);
                        dynamicSetting.CategoryId = arrSettingItem[1].ToString();
                        dynamicSetting.SubCategoryId = arrSettingItem[2].ToString();
                        dynamicSetting.DetailsId = arrSettingItem[3].ToString();
                        dynamicSetting.MethodId = arrSettingItem[4].ToString();
                        dynamicSetting.ParameterId = arrSettingItem[5].ToString();
                        dynamicSetting.DynamicTableId = tableId;
                        dynamicSetting.IsActive = true;

                        insertType = arrSettingItem[6].ToString();
                        if (insertType == "insert")
                        {
                            dynamicSetting.CreatedBy = session["userName"].ToString();
                            dynamicSetting.CreatedDate = DateTime.Now;
                            finalResults = totalBLL.CreateDynamicSetting(dynamicSetting);
                        }
                        else if (insertType == "update")
                        {
                            dynamicSetting.UpdatedBy = session["userName"].ToString();
                            dynamicSetting.UpdatedDate = DateTime.Now;
                            finalResults = totalBLL.UpdateDynamicTableSettings(dynamicSetting);
                        }
                    }
                }

                if (finalResults > 0)
                {
                    return Ok("設定が削除されました.");
                }
                else
                {
                    return BadRequest("Something went wrong.");
                }
            }
            else
            {
                return Ok("Data not found!");
            }
        }
        [Route("api/utilities/GetDynamicTableById/{table_id}")]
        [HttpGet]
        [ActionName("GetDynamicTableById")]
        public IHttpActionResult GetDynamicTableById(string table_id)
        {
            var result = totalBLL.GetDynamicTableById(Convert.ToInt32(table_id));
            return Ok(result);
        }

        [HttpGet]
        [Route("api/utilities/GetMethodList/")]
        public IHttpActionResult GetMethodList()
        {
            return Ok(DynamicMethodDefinition.GetMethods());
        }

        [HttpPost]
        [Route("api/utilities/CreateSubCategory/")]
        public IHttpActionResult CreateSubCategory(SubCategory subCategory)
        {
            if (String.IsNullOrEmpty(subCategory.SubCategoryName))
            {
                return BadRequest("Sub-Category name required!");
            }
            
            subCategory.CreatedBy = "";
            subCategory.CreatedDate = DateTime.Now;
            subCategory.IsActive = true;

            var result = subCategoryBLL.CreateSubCategory(subCategory);

            if (result > 0)
            {
                return Ok("Data Saved Successfully!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [Route("api/utilities/CreateCategory/")]
        public IHttpActionResult CreateCategory(Category category)
        {
            if (String.IsNullOrEmpty(category.CategoryName))
            {
                return BadRequest("Category name required!");
            }

            category.CreatedBy = "";
            category.CreatedDate = DateTime.Now;
            category.IsActive = true;

            var result = categoryBLL.CreateCategory(category);

            if (result > 0)
            {
                return Ok("Data Saved Successfully!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetCategoryById/")]
        public IHttpActionResult GetCategoryById(string categoryId)
        {
            if (String.IsNullOrEmpty(categoryId))
            {
                return BadRequest("Category required!");
            }

            Category category = categoryBLL.GetCategoryByCategoryId(Convert.ToInt32(categoryId));
            if (category == null)
            {
                return BadRequest("Something went wrong!");
            }
            return Ok(category);
        }

        [HttpPut]
        [Route("api/utilities/UpdateCategory/")]
        public IHttpActionResult UpdateCategory(Category category)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (category == null)
            {
                return BadRequest("Something went wrong!");
            }

            category.UpdatedBy = session["userName"].ToString();
            category.UpdatedDate = DateTime.Now;

            var result = categoryBLL.UpdateCategory(category);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete]
        [Route("api/utilities/RemoveCategory/")]
        public IHttpActionResult RemoveCategory(string categoryId)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (String.IsNullOrEmpty(categoryId))
            {
                return BadRequest("Category required!");
            }

            Category category = categoryBLL.GetCategoryByCategoryId(Convert.ToInt32(categoryId));
            if (category == null)
            {
                return BadRequest("Something went wrong!");
            }

            var result = categoryBLL.RemoveCategory(category);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete]
        [Route("api/utilities/RemoveSubCategory/")]
        public IHttpActionResult RemoveSubCategory(string subCategoryId)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (String.IsNullOrEmpty(subCategoryId))
            {
                return BadRequest("Category required!");
            }

            SubCategory subCategory = subCategoryBLL.GetSubCategoryById(Convert.ToInt32(subCategoryId));
            if (subCategory == null)
            {
                return BadRequest("Something went wrong!");
            }

            var result = subCategoryBLL.RemoveSubCategory(subCategory);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpPost]
        [Route("api/utilities/CreateDetailsItem/")]
        public IHttpActionResult CreateDetailsItem(DeatailsItem deatailsItem)
        {
            if (String.IsNullOrEmpty(deatailsItem.DetailsItemName))
            {
                return BadRequest("Details Item Name required!");
            }
            if (String.IsNullOrEmpty(deatailsItem.SubCategoryId))
            {
                return BadRequest("Sub Item required!");
            }

            deatailsItem.CreatedBy = "";
            deatailsItem.CreatedDate = DateTime.Now;
            deatailsItem.IsActive = true;

            var result = detailsItemBLL.CreateDetailsItem(deatailsItem);

            if (result > 0)
            {
                return Ok("Data Saved Successfully!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetDetailsItemBySubItemsId/")]
        public IHttpActionResult GetDetailsItemBySubItemsId(int subItemId)
        {
            List<DeatailsItem> deatailsItems = detailsItemBLL.GetDetailsItemBySubItemsId(subItemId);

            return Ok(deatailsItems);
        }

        [HttpGet]
        [Route("api/utilities/GetDetailsItemById/")]
        public IHttpActionResult GetDetailsItemById(int detailsId)
        {
            DeatailsItem deatailsItem = detailsItemBLL.GetDetailsItemById(detailsId);

            return Ok(deatailsItem);
        }

        [HttpPut]
        [Route("api/utilities/UpdateDetailItem/")]
        public IHttpActionResult UpdateDetailItem(DeatailsItem deatailsItem)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (deatailsItem == null)
            {
                return BadRequest("Something went wrong!");
            }

            deatailsItem.UpdatedBy = session["userName"].ToString();
            deatailsItem.UpdatedDate = DateTime.Now;

            var result = detailsItemBLL.UpdateDetailsItem(deatailsItem);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpDelete]
        [Route("api/utilities/RemoveDetailItem/")]
        public IHttpActionResult RemoveDetailItem(string detailId)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (String.IsNullOrEmpty(detailId))
            {
                return BadRequest("Detail item required!");
            }

            DeatailsItem deatailsItem = detailsItemBLL.GetDetailsItemById(Convert.ToInt32(detailId));
            if (deatailsItem == null)
            {
                return BadRequest("Something went wrong!");
            }

            var result = detailsItemBLL.RemoveDetailsItem(deatailsItem);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetSubCategorieById/")]
        public IHttpActionResult GetSubCategorieById(int subCategoryId)
        {
            SubCategory subCategory = subCategoryBLL.GetSubCategoryById(subCategoryId);
            return Ok(subCategory);
        }

        [HttpPut]
        [Route("api/utilities/UpdateSubCategory/")]
        public IHttpActionResult UpdateSubCategory(SubCategory subCategory)
        {
            var session = System.Web.HttpContext.Current.Session;

            if (subCategory == null)
            {
                return BadRequest("Something went wrong!");
            }

            subCategory.UpdatedBy = session["userName"].ToString();
            subCategory.UpdatedDate = DateTime.Now;

            var result = subCategoryBLL.UpdateSubCategory(subCategory);

            if (result > 0)
            {
                return Ok("Operation Completed!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetCategories/")]
        public IHttpActionResult GetCategories(string dynamicTableId)
        {
            CategoryBLL categoryBLL = new CategoryBLL();
            return Ok(categoryBLL.GetAllCategoriesByDynamicTableId(Convert.ToInt32(dynamicTableId)));
        }

        //[HttpGet]
        //[Route("api/utilities/GetCategories/")]
        //public IHttpActionResult GetCategories()
        //{
        //    CategoryBLL categoryBLL = new CategoryBLL();
        //    return Ok(categoryBLL.GetAllCategoriesByDynamicTableId(Convert.ToInt32(dynamicTableId)));
        //}

        [HttpGet]
        [Route("api/utilities/GetSubCategoriesByCategory/")]
        public IHttpActionResult GetSubCategoriesByCategory(int categoryId)
        {
            List<SubCategory> subCategories = subCategoryBLL.GetSubCategoryByCategoryId(categoryId);

            return Ok(subCategories);
        }

        [HttpPost]
        [Route("api/utilities/CreateDynamicSetting/")]
        public IHttpActionResult CreateDynamicSetting(DynamicSetting dynamicSettingData)
        {
            foreach (var dynamicSetting in dynamicSettingData.DynamicSettings)
            {
                if (String.IsNullOrEmpty(dynamicSetting.DynamicTableId))
                {
                    return BadRequest("TableId required!");
                }
                if (String.IsNullOrEmpty(dynamicSetting.CategoryId))
                {
                    return BadRequest("Input Category!");
                }
                if (String.IsNullOrEmpty(dynamicSetting.SubCategoryId))
                {
                    return BadRequest("Input Sub-Category!");
                }

                dynamicSetting.CreatedBy = "";
                dynamicSetting.CreatedDate = DateTime.Now;
                dynamicSetting.IsActive = true;

                var result = totalBLL.CreateDynamicSetting(dynamicSetting);
            }


            return Ok("Operation Completed!");

        }

        [HttpGet]
        [Route("api/utilities/GetDynamicSettingsByDynamicTableId/")]
        public IHttpActionResult GetDynamicSettingsByDynamicTableId(int dynamicTableId)
        {
            List<DynamicSetting> dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(dynamicTableId);

            return Ok(dynamicSettings);
        }

        [HttpPost]
        [Route("api/utilities/UpdateDynamicTablesTitle/")]
        public IHttpActionResult UpdateDynamicTablesTitle(int dynamicTableId, string categoryTitle, string subCategoryTitle, string detailsTitle)
        {
            DynamicTable dynamicTable = totalBLL.GetDynamicTableById(dynamicTableId);
            dynamicTable.CategoryTitle = categoryTitle;
            dynamicTable.SubCategoryTitle = subCategoryTitle;
            dynamicTable.DetailsTitle = detailsTitle;
            dynamicTable.UpdatedBy = "";
            dynamicTable.UpdatedDate = DateTime.Now;

            var result = totalBLL.UpdateDynamicTablesTitle(dynamicTable);
            if (result > 0)
            {
                return Ok("Data updated successfully!");
            }
            else
            {
                return BadRequest("Something went wrong!");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetDynamicCostTalbes/")]
        public IHttpActionResult GetDynamicCostTalbes(string companiIds, string year, string strTableType,string timestampsId)
        {
            List<DynamicTable> dynamicTables = new List<DynamicTable>();


            dynamicTables = totalBLL.GetAllDynamicTables();
            if (dynamicTables.Count > 0)
            {
                string strTotalTalbe = "";
                foreach (var tableItem in dynamicTables)
                {
                    if (!string.IsNullOrEmpty(tableItem.TablePosition.ToString()))
                    {
                        //total table
                        if (!string.IsNullOrEmpty(strTableType))
                        {
                            if (strTableType == "total")
                            {
                                //total table 
                                if (Convert.ToInt32(tableItem.TablePosition) == 1)
                                {
                                    List<DynamicSetting> dynamicSettings = new List<DynamicSetting>();
                                    dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(tableItem.Id);
                                    if (dynamicSettings.Count > 0)
                                    {
                                        string strTotalTableHeader = "";
                                        string strTotalTableBody = "";
                                        string tableTitle = "";
                                        string strTotalTableBodyStart = "<tbody>";
                                        string strTotalTableBodyEnd = "</tbody>";

                                        tableTitle = tableItem.TableTitle;

                                        strTotalTableHeader = totalBLL.GetCostTableHeaderPart(tableItem.CategoryTitle, tableItem.SubCategoryTitle, tableItem.DetailsTitle, tableTitle, year);
                                        
                                        int totalTableIndexCount = 0;
                                        string multiTotalBody = "";
                                        DynamicTableViewModal _totalCost = new DynamicTableViewModal();
                                        double octTotalSum = 0, novTotalSum = 0, decTotalSum = 0, janTotalSum = 0, febTotalSum = 0, marTotalSum = 0, aprTotalSum = 0, mayTotalSum = 0, junTotalSum = 0, julTotalSum = 0, augTotalSum = 0, sepTotalSum = 0, firstHalfTotalSum = 0, secondHalfTotalSum = 0, yearCostTotalSum = 0;                                       

                                        foreach (var settingItem in dynamicSettings)
                                        {                                                                           
                                            string singleTotalBody = "";
                                            singleTotalBody = totalBLL.GetTotalCostTableBody(settingItem, totalTableIndexCount, year, timestampsId, companiIds);
                                            
                                            //total row calculation
                                            { 
                                                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                                                dynamicTableViewModal = totalBLL.GetTotalCostWithoutQA(settingItem, companiIds, Convert.ToInt32(year), timestampsId);
                                                if (octTotalSum > 0)
                                                {
                                                    octTotalSum = octTotalSum + dynamicTableViewModal.OctTotalCost;
                                                }
                                                else
                                                {
                                                    octTotalSum = dynamicTableViewModal.OctTotalCost;
                                                }
                                                if (novTotalSum > 0)
                                                {
                                                    novTotalSum = novTotalSum + dynamicTableViewModal.NovTotalCost;
                                                }
                                                else
                                                {
                                                    novTotalSum = dynamicTableViewModal.NovTotalCost;
                                                }
                                                if (decTotalSum > 0)
                                                {
                                                    decTotalSum = decTotalSum + dynamicTableViewModal.DecTotalCost;
                                                }
                                                else
                                                {
                                                    decTotalSum = dynamicTableViewModal.DecTotalCost;
                                                }
                                                if (janTotalSum > 0)
                                                {
                                                    janTotalSum = janTotalSum + dynamicTableViewModal.JanTotalCost;
                                                }
                                                else
                                                {
                                                    janTotalSum = dynamicTableViewModal.JanTotalCost;
                                                }
                                                if (febTotalSum > 0)
                                                {
                                                    febTotalSum = febTotalSum + dynamicTableViewModal.FebTotalCost;
                                                }
                                                else
                                                {
                                                    febTotalSum = dynamicTableViewModal.FebTotalCost;
                                                }
                                                if (marTotalSum > 0)
                                                {
                                                    marTotalSum = marTotalSum + dynamicTableViewModal.MarTotalCost;
                                                }
                                                else
                                                {
                                                    marTotalSum = dynamicTableViewModal.MarTotalCost;
                                                }
                                                if (aprTotalSum > 0)
                                                {
                                                    aprTotalSum = aprTotalSum + dynamicTableViewModal.AprTotalCost;
                                                }
                                                else
                                                {
                                                    aprTotalSum = dynamicTableViewModal.AprTotalCost;
                                                }
                                                if (mayTotalSum > 0)
                                                {
                                                    mayTotalSum = mayTotalSum + dynamicTableViewModal.MayTotalCost;
                                                }
                                                else
                                                {
                                                    mayTotalSum = dynamicTableViewModal.MayTotalCost;
                                                }
                                                if (junTotalSum > 0)
                                                {
                                                    junTotalSum = junTotalSum + dynamicTableViewModal.JunTotalCost;
                                                }
                                                else
                                                {
                                                    junTotalSum = dynamicTableViewModal.JunTotalCost;
                                                }
                                                if (julTotalSum > 0)
                                                {
                                                    julTotalSum = julTotalSum + dynamicTableViewModal.JulTotalCost;
                                                }
                                                else
                                                {
                                                    julTotalSum = dynamicTableViewModal.JulTotalCost;
                                                }
                                                if (augTotalSum > 0)
                                                {
                                                    augTotalSum = augTotalSum + dynamicTableViewModal.AugTotalCost;
                                                }
                                                else
                                                {
                                                    augTotalSum = dynamicTableViewModal.AugTotalCost;
                                                }
                                                if (sepTotalSum > 0)
                                                {
                                                    sepTotalSum = sepTotalSum + dynamicTableViewModal.SepTotalCost;
                                                }
                                                else
                                                {
                                                    sepTotalSum = dynamicTableViewModal.SepTotalCost;
                                                }
                                                if (firstHalfTotalSum > 0)
                                                {
                                                    firstHalfTotalSum = firstHalfTotalSum + dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                else
                                                {
                                                    firstHalfTotalSum = dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                if (secondHalfTotalSum > 0)
                                                {
                                                    secondHalfTotalSum = secondHalfTotalSum + dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                else
                                                {
                                                    secondHalfTotalSum = dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                if (yearCostTotalSum > 0)
                                                {
                                                    yearCostTotalSum = yearCostTotalSum + dynamicTableViewModal.YearTotalCost;
                                                }
                                                else
                                                {
                                                    yearCostTotalSum = dynamicTableViewModal.YearTotalCost;
                                                }
                                            }

                                            if (string.IsNullOrEmpty(multiTotalBody))
                                            {
                                                multiTotalBody = singleTotalBody;
                                            }
                                            else
                                            {
                                                multiTotalBody = multiTotalBody + "" + singleTotalBody;
                                            }
                                        }

                                        string totalRowTd = "";
                                        totalRowTd = totalRowTd +"<tr data-indentity='" + totalTableIndexCount + "'>";
                                        totalRowTd = totalRowTd + "<td>Total</td>";
                                        if (!string.IsNullOrEmpty(tableItem.SubCategoryTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        if (!string.IsNullOrEmpty(tableItem.DetailsTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }    
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(octTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(novTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(decTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(janTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(febTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(marTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(aprTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(mayTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(junTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(julTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(augTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(sepTotalSum).ToString("N0") + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(yearCostTotalSum).ToString("N0") + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(firstHalfTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(secondHalfTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "</tr>";
                                    
                                        if (string.IsNullOrEmpty(multiTotalBody))
                                        {
                                            multiTotalBody = totalRowTd;
                                        }
                                        else
                                        {
                                            multiTotalBody = multiTotalBody + "" + totalRowTd;
                                        }

                                        strTotalTableBody = strTotalTableBodyStart + "" + multiTotalBody + "" + strTotalTableBodyEnd;

                                        if (string.IsNullOrEmpty(strTotalTalbe))
                                        {
                                            strTotalTalbe = strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                        else
                                        {
                                            strTotalTalbe = strTotalTalbe + "" + strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                    }
                                }
                            }
                            else if (strTableType == "initial")
                            {
                                //initial budget table
                                if (Convert.ToInt32(tableItem.TablePosition) == 2)
                                {
                                    List<DynamicSetting> dynamicSettings = new List<DynamicSetting>();
                                    dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(tableItem.Id);
                                    if (dynamicSettings.Count > 0)
                                    {
                                        string strTotalTableHeader = "";
                                        string strTotalTableBody = "";
                                        string tableTitle = "";
                                        string strTotalTableBodyStart = "<tbody>";
                                        string strTotalTableBodyEnd = "</tbody>";

                                        tableTitle = tableItem.TableTitle;

                                        strTotalTableHeader = totalBLL.GetCostTableHeaderPart(tableItem.CategoryTitle, tableItem.SubCategoryTitle, tableItem.DetailsTitle, tableTitle, year);

                                        double octTotalSum = 0, novTotalSum = 0, decTotalSum = 0, janTotalSum = 0, febTotalSum = 0, marTotalSum = 0, aprTotalSum = 0, mayTotalSum = 0, junTotalSum = 0, julTotalSum = 0, augTotalSum = 0, sepTotalSum = 0, firstHalfTotalSum = 0, secondHalfTotalSum = 0, yearCostTotalSum = 0;
                                        int totalTableIndexCount = 0;
                                        string multiTotalBody = "";

                                        foreach (var settingItem in dynamicSettings)
                                        {
                                            string singleTotalBody = "";

                                            singleTotalBody = totalBLL.GetBudgetTableBodyPart(settingItem, totalTableIndexCount, companiIds, Convert.ToInt32(year), timestampsId);

                                            //total row calculation
                                            {
                                                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                                                dynamicTableViewModal = totalBLL.GetBudgetCostWithoutQA(settingItem, companiIds, Convert.ToInt32(year), timestampsId);
                                                if (octTotalSum > 0)
                                                {
                                                    octTotalSum = octTotalSum + dynamicTableViewModal.OctTotalCost;
                                                }
                                                else
                                                {
                                                    octTotalSum = dynamicTableViewModal.OctTotalCost;
                                                }
                                                if (novTotalSum > 0)
                                                {
                                                    novTotalSum = novTotalSum + dynamicTableViewModal.NovTotalCost;
                                                }
                                                else
                                                {
                                                    novTotalSum = dynamicTableViewModal.NovTotalCost;
                                                }
                                                if (decTotalSum > 0)
                                                {
                                                    decTotalSum = decTotalSum + dynamicTableViewModal.DecTotalCost;
                                                }
                                                else
                                                {
                                                    decTotalSum = dynamicTableViewModal.DecTotalCost;
                                                }
                                                if (janTotalSum > 0)
                                                {
                                                    janTotalSum = janTotalSum + dynamicTableViewModal.JanTotalCost;
                                                }
                                                else
                                                {
                                                    janTotalSum = dynamicTableViewModal.JanTotalCost;
                                                }
                                                if (febTotalSum > 0)
                                                {
                                                    febTotalSum = febTotalSum + dynamicTableViewModal.FebTotalCost;
                                                }
                                                else
                                                {
                                                    febTotalSum = dynamicTableViewModal.FebTotalCost;
                                                }
                                                if (marTotalSum > 0)
                                                {
                                                    marTotalSum = marTotalSum + dynamicTableViewModal.MarTotalCost;
                                                }
                                                else
                                                {
                                                    marTotalSum = dynamicTableViewModal.MarTotalCost;
                                                }
                                                if (aprTotalSum > 0)
                                                {
                                                    aprTotalSum = aprTotalSum + dynamicTableViewModal.AprTotalCost;
                                                }
                                                else
                                                {
                                                    aprTotalSum = dynamicTableViewModal.AprTotalCost;
                                                }
                                                if (mayTotalSum > 0)
                                                {
                                                    mayTotalSum = mayTotalSum + dynamicTableViewModal.MayTotalCost;
                                                }
                                                else
                                                {
                                                    mayTotalSum = dynamicTableViewModal.MayTotalCost;
                                                }
                                                if (junTotalSum > 0)
                                                {
                                                    junTotalSum = junTotalSum + dynamicTableViewModal.JunTotalCost;
                                                }
                                                else
                                                {
                                                    junTotalSum = dynamicTableViewModal.JunTotalCost;
                                                }
                                                if (julTotalSum > 0)
                                                {
                                                    julTotalSum = julTotalSum + dynamicTableViewModal.JulTotalCost;
                                                }
                                                else
                                                {
                                                    julTotalSum = dynamicTableViewModal.JulTotalCost;
                                                }
                                                if (augTotalSum > 0)
                                                {
                                                    augTotalSum = augTotalSum + dynamicTableViewModal.AugTotalCost;
                                                }
                                                else
                                                {
                                                    augTotalSum = dynamicTableViewModal.AugTotalCost;
                                                }
                                                if (sepTotalSum > 0)
                                                {
                                                    sepTotalSum = sepTotalSum + dynamicTableViewModal.SepTotalCost;
                                                }
                                                else
                                                {
                                                    sepTotalSum = dynamicTableViewModal.SepTotalCost;
                                                }
                                                if (firstHalfTotalSum > 0)
                                                {
                                                    firstHalfTotalSum = firstHalfTotalSum + dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                else
                                                {
                                                    firstHalfTotalSum = dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                if (secondHalfTotalSum > 0)
                                                {
                                                    secondHalfTotalSum = secondHalfTotalSum + dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                else
                                                {
                                                    secondHalfTotalSum = dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                if (yearCostTotalSum > 0)
                                                {
                                                    yearCostTotalSum = yearCostTotalSum + dynamicTableViewModal.YearTotalCost;
                                                }
                                                else
                                                {
                                                    yearCostTotalSum = dynamicTableViewModal.YearTotalCost;
                                                }
                                            }



                                            if (string.IsNullOrEmpty(multiTotalBody))
                                            {
                                                multiTotalBody = singleTotalBody;
                                            }
                                            else
                                            {
                                                multiTotalBody = multiTotalBody + "" + singleTotalBody;
                                            }
                                        }
                                        string totalRowTd = "";
                                        totalRowTd = totalRowTd + "<tr data-indentity='" + totalTableIndexCount + "'>";
                                        totalRowTd = totalRowTd + "<td>Total</td>";
                                        if (!string.IsNullOrEmpty(tableItem.SubCategoryTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        if (!string.IsNullOrEmpty(tableItem.DetailsTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(octTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(novTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(decTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(janTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(febTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(marTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(aprTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(mayTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(junTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(julTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(augTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(sepTotalSum).ToString("N0") + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(yearCostTotalSum).ToString("N0") + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(firstHalfTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(secondHalfTotalSum).ToString("N0") + "</td>";
                                        totalRowTd = totalRowTd + "</tr>";

                                        if (string.IsNullOrEmpty(multiTotalBody))
                                        {
                                            multiTotalBody = totalRowTd;
                                        }
                                        else
                                        {
                                            multiTotalBody = multiTotalBody + "" + totalRowTd;
                                        }

                                        strTotalTableBody = strTotalTableBodyStart + "" + multiTotalBody + "" + strTotalTableBodyEnd;


                                        if (string.IsNullOrEmpty(strTotalTalbe))
                                        {
                                            strTotalTalbe = strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                        else
                                        {
                                            strTotalTalbe = strTotalTalbe + "" + strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                    }
                                }
                            }

                            else if (strTableType == "difference")
                            {
                                //difference table
                                if (Convert.ToInt32(tableItem.TablePosition) == 3)
                                {
                                    List<DynamicSetting> dynamicSettings = new List<DynamicSetting>();
                                    dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(tableItem.Id);
                                    if (dynamicSettings.Count > 0)
                                    {
                                        string strTotalTableHeader = "";
                                        string strTotalTableBody = "";
                                        string tableTitle = "";
                                        string strTotalTableBodyStart = "<tbody>";
                                        string strTotalTableBodyEnd = "</tbody>";

                                        tableTitle = tableItem.TableTitle;

                                        strTotalTableHeader = totalBLL.GetCostTableHeaderPart(tableItem.CategoryTitle, tableItem.SubCategoryTitle, tableItem.DetailsTitle, tableTitle, year);

                                        double octTotalSum = 0, novTotalSum = 0, decTotalSum = 0, janTotalSum = 0, febTotalSum = 0, marTotalSum = 0, aprTotalSum = 0, mayTotalSum = 0, junTotalSum = 0, julTotalSum = 0, augTotalSum = 0, sepTotalSum = 0, firstHalfTotalSum = 0, secondHalfTotalSum = 0, yearCostTotalSum = 0;
                                        int totalTableIndexCount = 0;
                                        string multiTotalBody = "";

                                        foreach (var settingItem in dynamicSettings)
                                        {
                                            string singleTotalBody = "";

                                            singleTotalBody = totalBLL.GeDifferenceTableBodyPart(settingItem, totalTableIndexCount,companiIds,Convert.ToInt32(year),timestampsId);

                                            //total row calculation
                                            {
                                                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                                                DynamicTableViewModal costWithQAProportion = new DynamicTableViewModal();
                                                DynamicTableViewModal budgetWithQAProportion = new DynamicTableViewModal();

                                                costWithQAProportion = totalBLL.GetTotalCostWithQA(settingItem, companiIds, Convert.ToInt32(year), timestampsId);
                                                budgetWithQAProportion = totalBLL.GetBudgetCostWithoutQA(settingItem, companiIds, Convert.ToInt32(year), timestampsId);

                                                dynamicTableViewModal.OctTotalCost = costWithQAProportion.OctTotalCost - budgetWithQAProportion.OctTotalCost;
                                                dynamicTableViewModal.NovTotalCost = costWithQAProportion.NovTotalCost - budgetWithQAProportion.NovTotalCost;
                                                dynamicTableViewModal.DecTotalCost = costWithQAProportion.DecTotalCost - budgetWithQAProportion.DecTotalCost;
                                                dynamicTableViewModal.JanTotalCost = costWithQAProportion.JanTotalCost - budgetWithQAProportion.JanTotalCost;
                                                dynamicTableViewModal.FebTotalCost = costWithQAProportion.FebTotalCost - budgetWithQAProportion.FebTotalCost;
                                                dynamicTableViewModal.MarTotalCost = costWithQAProportion.MarTotalCost - budgetWithQAProportion.MarTotalCost;
                                                dynamicTableViewModal.AprTotalCost = costWithQAProportion.AprTotalCost - budgetWithQAProportion.AprTotalCost;
                                                dynamicTableViewModal.MayTotalCost = costWithQAProportion.MayTotalCost - budgetWithQAProportion.MayTotalCost;
                                                dynamicTableViewModal.JunTotalCost = costWithQAProportion.JunTotalCost - budgetWithQAProportion.JunTotalCost;
                                                dynamicTableViewModal.JulTotalCost = costWithQAProportion.JulTotalCost - budgetWithQAProportion.JulTotalCost;
                                                dynamicTableViewModal.AugTotalCost = costWithQAProportion.AugTotalCost - budgetWithQAProportion.AugTotalCost;
                                                dynamicTableViewModal.SepTotalCost = costWithQAProportion.SepTotalCost - budgetWithQAProportion.SepTotalCost;

                                                if (octTotalSum > 0)
                                                {
                                                    octTotalSum = octTotalSum + dynamicTableViewModal.OctTotalCost;
                                                }
                                                else
                                                {
                                                    octTotalSum = dynamicTableViewModal.OctTotalCost;
                                                }
                                                if (novTotalSum > 0)
                                                {
                                                    novTotalSum = novTotalSum + dynamicTableViewModal.NovTotalCost;
                                                }
                                                else
                                                {
                                                    novTotalSum = dynamicTableViewModal.NovTotalCost;
                                                }
                                                if (decTotalSum > 0)
                                                {
                                                    decTotalSum = decTotalSum + dynamicTableViewModal.DecTotalCost;
                                                }
                                                else
                                                {
                                                    decTotalSum = dynamicTableViewModal.DecTotalCost;
                                                }
                                                if (janTotalSum > 0)
                                                {
                                                    janTotalSum = janTotalSum + dynamicTableViewModal.JanTotalCost;
                                                }
                                                else
                                                {
                                                    janTotalSum = dynamicTableViewModal.JanTotalCost;
                                                }
                                                if (febTotalSum > 0)
                                                {
                                                    febTotalSum = febTotalSum + dynamicTableViewModal.FebTotalCost;
                                                }
                                                else
                                                {
                                                    febTotalSum = dynamicTableViewModal.FebTotalCost;
                                                }
                                                if (marTotalSum > 0)
                                                {
                                                    marTotalSum = marTotalSum + dynamicTableViewModal.MarTotalCost;
                                                }
                                                else
                                                {
                                                    marTotalSum = dynamicTableViewModal.MarTotalCost;
                                                }
                                                if (aprTotalSum > 0)
                                                {
                                                    aprTotalSum = aprTotalSum + dynamicTableViewModal.AprTotalCost;
                                                }
                                                else
                                                {
                                                    aprTotalSum = dynamicTableViewModal.AprTotalCost;
                                                }
                                                if (mayTotalSum > 0)
                                                {
                                                    mayTotalSum = mayTotalSum + dynamicTableViewModal.MayTotalCost;
                                                }
                                                else
                                                {
                                                    mayTotalSum = dynamicTableViewModal.MayTotalCost;
                                                }
                                                if (junTotalSum > 0)
                                                {
                                                    junTotalSum = junTotalSum + dynamicTableViewModal.JunTotalCost;
                                                }
                                                else
                                                {
                                                    junTotalSum = dynamicTableViewModal.JunTotalCost;
                                                }
                                                if (julTotalSum > 0)
                                                {
                                                    julTotalSum = julTotalSum + dynamicTableViewModal.JulTotalCost;
                                                }
                                                else
                                                {
                                                    julTotalSum = dynamicTableViewModal.JulTotalCost;
                                                }
                                                if (augTotalSum > 0)
                                                {
                                                    augTotalSum = augTotalSum + dynamicTableViewModal.AugTotalCost;
                                                }
                                                else
                                                {
                                                    augTotalSum = dynamicTableViewModal.AugTotalCost;
                                                }
                                                if (sepTotalSum > 0)
                                                {
                                                    sepTotalSum = sepTotalSum + dynamicTableViewModal.SepTotalCost;
                                                }
                                                else
                                                {
                                                    sepTotalSum = dynamicTableViewModal.SepTotalCost;
                                                }
                                                if (firstHalfTotalSum > 0)
                                                {
                                                    firstHalfTotalSum = firstHalfTotalSum + dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                else
                                                {
                                                    firstHalfTotalSum = dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                if (secondHalfTotalSum > 0)
                                                {
                                                    secondHalfTotalSum = secondHalfTotalSum + dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                else
                                                {
                                                    secondHalfTotalSum = dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                if (yearCostTotalSum > 0)
                                                {
                                                    yearCostTotalSum = yearCostTotalSum + dynamicTableViewModal.YearTotalCost;
                                                }
                                                else
                                                {
                                                    yearCostTotalSum = dynamicTableViewModal.YearTotalCost;
                                                }
                                            }


                                            if (string.IsNullOrEmpty(multiTotalBody))
                                            {
                                                multiTotalBody = singleTotalBody;
                                            }
                                            else
                                            {
                                                multiTotalBody = multiTotalBody + "" + singleTotalBody;
                                            }
                                        }
                                        string totalRowTd = "";
                                        totalRowTd = totalRowTd + "<tr data-indentity='" + totalTableIndexCount + "'>";
                                        totalRowTd = totalRowTd + "<td>Total</td>";
                                        if (!string.IsNullOrEmpty(tableItem.SubCategoryTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        if (!string.IsNullOrEmpty(tableItem.DetailsTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        if (Convert.ToInt32(octTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(octTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(octTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(novTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(novTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(novTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(decTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(decTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(decTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(janTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(janTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(janTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(febTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(febTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(febTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(marTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(marTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(marTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(aprTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(aprTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(aprTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(mayTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(mayTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(mayTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(junTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(junTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(junTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(julTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(julTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(julTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(augTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(augTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(augTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(sepTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(sepTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(sepTotalSum).ToString("N0") + "</td>";
                                        }
                                        if (Convert.ToInt32(yearCostTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(yearCostTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(yearCostTotalSum).ToString("N0") + "</td>";
                                        }

                                        if (Convert.ToInt32(firstHalfTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(firstHalfTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(firstHalfTotalSum).ToString("N0") + "</td>";
                                        }

                                        if (Convert.ToInt32(secondHalfTotalSum) < 0)
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right' style='color:red;'>" + Convert.ToInt32(secondHalfTotalSum).ToString("N0") + "</td>";
                                        }
                                        else
                                        {
                                            totalRowTd = totalRowTd + "<td class='text-right'>" + Convert.ToInt32(secondHalfTotalSum).ToString("N0") + "</td>";
                                        }                                        
                                        totalRowTd = totalRowTd + "</tr>";

                                        if (string.IsNullOrEmpty(multiTotalBody))
                                        {
                                            multiTotalBody = totalRowTd;
                                        }
                                        else
                                        {
                                            multiTotalBody = multiTotalBody + "" + totalRowTd;
                                        }
                                        strTotalTableBody = strTotalTableBodyStart + "" + multiTotalBody + "" + strTotalTableBodyEnd;


                                        if (string.IsNullOrEmpty(strTotalTalbe))
                                        {
                                            strTotalTalbe = strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                        else
                                        {
                                            strTotalTalbe = strTotalTalbe + "" + strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                    }
                                }
                            }
                            else if (strTableType == "headcount")
                            {
                                //headcount table
                                if (Convert.ToInt32(tableItem.TablePosition) == 4)
                                {
                                    List<DynamicSetting> dynamicSettings = new List<DynamicSetting>();
                                    dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(tableItem.Id);
                                    if (dynamicSettings.Count > 0)
                                    {
                                        string strTotalTableHeader = "";
                                        string strTotalTableBody = "";
                                        string tableTitle = "";
                                        string strTotalTableBodyStart = "<tbody>";
                                        string strTotalTableBodyEnd = "</tbody>";

                                        tableTitle = tableItem.TableTitle;

                                        strTotalTableHeader = totalBLL.GetCostTableHeaderPart(tableItem.CategoryTitle, tableItem.SubCategoryTitle, tableItem.DetailsTitle, tableTitle, year);

                                        double octTotalSum = 0, novTotalSum = 0, decTotalSum = 0, janTotalSum = 0, febTotalSum = 0, marTotalSum = 0, aprTotalSum = 0, mayTotalSum = 0, junTotalSum = 0, julTotalSum = 0, augTotalSum = 0, sepTotalSum = 0, firstHalfTotalSum = 0, secondHalfTotalSum = 0, yearCostTotalSum = 0;
                                        int totalTableIndexCount = 0;
                                        string multiTotalBody = "";

                                        foreach (var settingItem in dynamicSettings)
                                        {
                                            string singleTotalBody = "";

                                            singleTotalBody = totalBLL.GetTotalTableBodyPart(settingItem, totalTableIndexCount);

                                            //total row calculation
                                            {
                                                DynamicTableViewModal dynamicTableViewModal = new DynamicTableViewModal();
                                                dynamicTableViewModal = totalBLL.GetTotalCostWithoutQA(settingItem, companiIds, Convert.ToInt32(year), timestampsId);
                                                if (octTotalSum > 0)
                                                {
                                                    octTotalSum = octTotalSum + dynamicTableViewModal.OctTotalCost;
                                                }
                                                else
                                                {
                                                    octTotalSum = dynamicTableViewModal.OctTotalCost;
                                                }
                                                if (novTotalSum > 0)
                                                {
                                                    novTotalSum = novTotalSum + dynamicTableViewModal.NovTotalCost;
                                                }
                                                else
                                                {
                                                    novTotalSum = dynamicTableViewModal.NovTotalCost;
                                                }
                                                if (decTotalSum > 0)
                                                {
                                                    decTotalSum = decTotalSum + dynamicTableViewModal.DecTotalCost;
                                                }
                                                else
                                                {
                                                    decTotalSum = dynamicTableViewModal.DecTotalCost;
                                                }
                                                if (janTotalSum > 0)
                                                {
                                                    janTotalSum = janTotalSum + dynamicTableViewModal.JanTotalCost;
                                                }
                                                else
                                                {
                                                    janTotalSum = dynamicTableViewModal.JanTotalCost;
                                                }
                                                if (febTotalSum > 0)
                                                {
                                                    febTotalSum = febTotalSum + dynamicTableViewModal.FebTotalCost;
                                                }
                                                else
                                                {
                                                    febTotalSum = dynamicTableViewModal.FebTotalCost;
                                                }
                                                if (marTotalSum > 0)
                                                {
                                                    marTotalSum = marTotalSum + dynamicTableViewModal.MarTotalCost;
                                                }
                                                else
                                                {
                                                    marTotalSum = dynamicTableViewModal.MarTotalCost;
                                                }
                                                if (aprTotalSum > 0)
                                                {
                                                    aprTotalSum = aprTotalSum + dynamicTableViewModal.AprTotalCost;
                                                }
                                                else
                                                {
                                                    aprTotalSum = dynamicTableViewModal.AprTotalCost;
                                                }
                                                if (mayTotalSum > 0)
                                                {
                                                    mayTotalSum = mayTotalSum + dynamicTableViewModal.MayTotalCost;
                                                }
                                                else
                                                {
                                                    mayTotalSum = dynamicTableViewModal.MayTotalCost;
                                                }
                                                if (junTotalSum > 0)
                                                {
                                                    junTotalSum = junTotalSum + dynamicTableViewModal.JunTotalCost;
                                                }
                                                else
                                                {
                                                    junTotalSum = dynamicTableViewModal.JunTotalCost;
                                                }
                                                if (julTotalSum > 0)
                                                {
                                                    julTotalSum = julTotalSum + dynamicTableViewModal.JulTotalCost;
                                                }
                                                else
                                                {
                                                    julTotalSum = dynamicTableViewModal.JulTotalCost;
                                                }
                                                if (augTotalSum > 0)
                                                {
                                                    augTotalSum = augTotalSum + dynamicTableViewModal.AugTotalCost;
                                                }
                                                else
                                                {
                                                    augTotalSum = dynamicTableViewModal.AugTotalCost;
                                                }
                                                if (sepTotalSum > 0)
                                                {
                                                    sepTotalSum = sepTotalSum + dynamicTableViewModal.SepTotalCost;
                                                }
                                                else
                                                {
                                                    sepTotalSum = dynamicTableViewModal.SepTotalCost;
                                                }
                                                if (firstHalfTotalSum > 0)
                                                {
                                                    firstHalfTotalSum = firstHalfTotalSum + dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                else
                                                {
                                                    firstHalfTotalSum = dynamicTableViewModal.FirstHalfTotalCost;
                                                }
                                                if (secondHalfTotalSum > 0)
                                                {
                                                    secondHalfTotalSum = secondHalfTotalSum + dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                else
                                                {
                                                    secondHalfTotalSum = dynamicTableViewModal.SecondHalfTotalCost;
                                                }
                                                if (yearCostTotalSum > 0)
                                                {
                                                    yearCostTotalSum = yearCostTotalSum + dynamicTableViewModal.YearTotalCost;
                                                }
                                                else
                                                {
                                                    yearCostTotalSum = dynamicTableViewModal.YearTotalCost;
                                                }
                                            }


                                            if (string.IsNullOrEmpty(multiTotalBody))
                                            {
                                                multiTotalBody = singleTotalBody;
                                            }
                                            else
                                            {
                                                multiTotalBody = multiTotalBody + "" + singleTotalBody;
                                            }
                                        }
                                        string totalRowTd = "";
                                        totalRowTd = totalRowTd + "<tr data-indentity='" + totalTableIndexCount + "'>";
                                        totalRowTd = totalRowTd + "<td>Total</td>";
                                        if (!string.IsNullOrEmpty(tableItem.SubCategoryTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        if (!string.IsNullOrEmpty(tableItem.DetailsTitle))
                                        {
                                            totalRowTd = totalRowTd + "<td></td>";
                                        }
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + octTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + novTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + decTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + janTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + febTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + marTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + aprTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + mayTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + junTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + julTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + augTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + sepTotalSum + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + yearCostTotalSum + "</td>";

                                        totalRowTd = totalRowTd + "<td class='text-right'>" + firstHalfTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "<td class='text-right'>" + secondHalfTotalSum + "</td>";
                                        totalRowTd = totalRowTd + "</tr>";

                                        if (string.IsNullOrEmpty(multiTotalBody))
                                        {
                                            multiTotalBody = totalRowTd;
                                        }
                                        else
                                        {
                                            multiTotalBody = multiTotalBody + "" + totalRowTd;
                                        }

                                        strTotalTableBody = strTotalTableBodyStart + "" + multiTotalBody + "" + strTotalTableBodyEnd;


                                        if (string.IsNullOrEmpty(strTotalTalbe))
                                        {
                                            strTotalTalbe = strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                        else
                                        {
                                            strTotalTalbe = strTotalTalbe + "" + strTotalTableHeader + "" + strTotalTableBody;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                return Ok(strTotalTalbe);
            }
            else
            {
                return Ok("Table not created!");
            }

            //int year = 0;
            //double _octHinsho = 0;
            //double _novHinsho = 0;
            //double _decHinsho = 0;
            //double _janHinsho = 0;
            //double _febHinsho = 0;
            //double _marHinsho = 0;
            //double _aprHinsho = 0;
            //double _mayHinsho = 0;
            //double _junHinsho = 0;
            //double _julHinsho = 0;
            //double _augHinsho = 0;
            //double _sepHinsho = 0;
            //List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            //int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            ////int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            //year = forecastLeatestYear;
            //List<Department> departments = departmentBLL.GetAllDepartments();
            //Department qaDepartmentByName = departments.Where(d => d.DepartmentName == "品証").SingleOrDefault();
            //if (qaDepartmentByName == null)
            //{
            //    return NotFound();
            //}
            //var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            //if (hinsoData.Count > 0)
            //{
            //    _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
            //    _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
            //    _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
            //    _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
            //    _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
            //    _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
            //    _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
            //    _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
            //    _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
            //    _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
            //    _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
            //    _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            //}

            //foreach (var department in departments)
            //{
            //    double rowTotal = 0;
            //    double firstSlot = 0;
            //    SukeyQADto sukeyDto = new SukeyQADto();
            //    sukeyDto.DepartmentId = department.Id.ToString();
            //    sukeyDto.DepartmentName = department.DepartmentName;
            //    //if (department.Id==8)
            //    //{
            //    //    continue;
            //    //}
            //    var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
            //    if (apportionmentByDepartment == null)
            //    {
            //        apportionmentByDepartment = new Apportionment();
            //    }


            //    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
            //    if (forecastAssignmentViewModels.Count > 0)
            //    {
            //        double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
            //        double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
            //        double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
            //        double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
            //        double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
            //        double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
            //        double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
            //        double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
            //        double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
            //        double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
            //        double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
            //        double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

            //        double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
            //        double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
            //        double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
            //        double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
            //        double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
            //        double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
            //        double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
            //        double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
            //        double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
            //        double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
            //        double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
            //        double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

            //        var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
            //        if (_octActualCostTotal > 0)
            //        {
            //            sukeyDto.OctCost.Add(_octActualCostTotal + _octCalculation);
            //            rowTotal += _octActualCostTotal + _octCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.OctCost.Add(_octTotal + _octCalculation);
            //            rowTotal += _octTotal + _octCalculation;
            //        }
            //        var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
            //        if (_novActualCostTotal > 0)
            //        {
            //            sukeyDto.NovCost.Add(_novActualCostTotal + _novCalculation);
            //            rowTotal += _novActualCostTotal + _novCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.NovCost.Add(_novTotal + _novCalculation);
            //            rowTotal += _novTotal + _novCalculation;
            //        }
            //        var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
            //        if (_decActualCostTotal > 0)
            //        {
            //            sukeyDto.DecCost.Add(_decActualCostTotal + _decCalculation);
            //            rowTotal += _decActualCostTotal + _decCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.DecCost.Add(_decTotal + _decCalculation);
            //            rowTotal += _decTotal + _decCalculation;
            //        }
            //        var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
            //        if (_janActualCostTotal > 0)
            //        {
            //            sukeyDto.JanCost.Add(_janActualCostTotal + _janCalculation);
            //            rowTotal += _janActualCostTotal + _janCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JanCost.Add(_janTotal + _janCalculation);
            //            rowTotal += _janTotal + _janCalculation;
            //        }
            //        var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
            //        if (_febActualCostTotal > 0)
            //        {
            //            sukeyDto.FebCost.Add(_febActualCostTotal + _febCalculation);
            //            rowTotal += _febActualCostTotal + _febCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.FebCost.Add(_febTotal + _febCalculation);
            //            rowTotal += _febTotal + _febCalculation;
            //        }
            //        var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
            //        if (_marActualCostTotal > 0)
            //        {
            //            sukeyDto.MarCost.Add(_marActualCostTotal + _marCalculation);
            //            rowTotal += _marActualCostTotal + _marCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.MarCost.Add(_marTotal + _marCalculation);
            //            rowTotal += _marTotal + _marCalculation;
            //        }
            //        sukeyDto.FirstSlot.Add(rowTotal);
            //        firstSlot = rowTotal;

            //        var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
            //        if (_aprActualCostTotal > 0)
            //        {
            //            sukeyDto.AprCost.Add(_aprActualCostTotal + _aprCalculation);
            //            rowTotal += _aprActualCostTotal + _aprCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
            //            rowTotal += _aprTotal + _aprCalculation;
            //        }
            //        var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
            //        if (_mayActualCostTotal > 0)
            //        {
            //            sukeyDto.MayCost.Add(_mayActualCostTotal + _mayCalculation);
            //            rowTotal += _mayActualCostTotal + _mayCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
            //            rowTotal += _mayTotal + _mayCalculation;
            //        }
            //        var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
            //        if (_junActualCostTotal > 0)
            //        {
            //            sukeyDto.JunCost.Add(_junActualCostTotal + _junCalculation);
            //            rowTotal += _junActualCostTotal + _junCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JunCost.Add(_junTotal + _junCalculation);
            //            rowTotal += _junTotal + _junCalculation;
            //        }
            //        var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
            //        if (_julActualCostTotal > 0)
            //        {
            //            sukeyDto.JulCost.Add(_julActualCostTotal + _julCalculation);
            //            rowTotal += _julActualCostTotal + _julCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JulCost.Add(_julTotal + _julCalculation);
            //            rowTotal += _julTotal + _julCalculation;
            //        }
            //        var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
            //        if (_augActualCostTotal > 0)
            //        {
            //            sukeyDto.AugCost.Add(_augActualCostTotal + _augCalculation);
            //            rowTotal += _augActualCostTotal + _augCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.AugCost.Add(_augTotal + _augCalculation);
            //            rowTotal += _augTotal + _augCalculation;
            //        }
            //        var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
            //        if (_sepActualCostTotal > 0)
            //        {
            //            sukeyDto.SepCost.Add(_sepActualCostTotal + _sepCalculation);
            //            rowTotal += _sepActualCostTotal + _sepCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
            //            rowTotal += _sepTotal + _sepCalculation;
            //        }
            //        sukeyDto.RowTotal.Add(rowTotal);
            //        sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

            //    }
            //    else
            //    {
            //        sukeyDto.OctCost.Add(0);
            //        sukeyDto.NovCost.Add(0);
            //        sukeyDto.DecCost.Add(0);
            //        sukeyDto.JanCost.Add(0);
            //        sukeyDto.FebCost.Add(0);
            //        sukeyDto.MarCost.Add(0);
            //        sukeyDto.AprCost.Add(0);
            //        sukeyDto.MayCost.Add(0);
            //        sukeyDto.JunCost.Add(0);
            //        sukeyDto.JulCost.Add(0);
            //        sukeyDto.AugCost.Add(0);
            //        sukeyDto.SepCost.Add(0);
            //        sukeyDto.RowTotal.Add(0);
            //        sukeyDto.FirstSlot.Add(0);
            //        sukeyDto.SecondSlot.Add(0);
            //    }



            //    sukeyQADtos.Add(sukeyDto);
            //}
            //return Ok(sukeyQADtos);
        }

        [HttpGet]
        [Route("api/utilities/GetDynamicTableTitleByPosition/")]
        public IHttpActionResult GetDynamicTableTitleByPosition(string tablePosition)
        {
            string retunTitle = "";
            if (!string.IsNullOrEmpty(tablePosition))
            {
                retunTitle = totalBLL.GetDynamicTableTitleByPosition(tablePosition);
                return Ok(retunTitle);
            }
            else
            {
                return Ok("");
            }
        }

        [HttpGet]
        [Route("api/utilities/GetHeadCount_Headerpart/")]
        public IHttpActionResult GetHeadCount_Headerpart(string companiIds, string year, string strTableType,string timestampsId)
        {
            List<DynamicTable> dynamicTables = new List<DynamicTable>();


            dynamicTables = totalBLL.GetAllDynamicTables();
            if (dynamicTables.Count > 0)
            {
                string strTotalTalbe = "";
                foreach (var tableItem in dynamicTables)
                {
                    if (!string.IsNullOrEmpty(tableItem.TablePosition.ToString()))
                    {
                        //total table
                        if (!string.IsNullOrEmpty(strTableType))
                        {
                            if (strTableType == "headcount")
                            {
                                //headcount table
                                if (Convert.ToInt32(tableItem.TablePosition) == 4)
                                {
                                    List<DynamicSetting> dynamicSettings = new List<DynamicSetting>();
                                    dynamicSettings = totalBLL.GetDynamicSettingsByDynamicTableId(tableItem.Id);
                                    if (dynamicSettings.Count > 0)
                                    {
                                        string strTotalTableHeader = "";
                                        string tableTitle = "";

                                        tableTitle = tableItem.TableTitle;

                                        strTotalTableHeader = totalBLL.GetCostTableHeaderPart(tableItem.CategoryTitle, tableItem.SubCategoryTitle, tableItem.DetailsTitle, tableTitle, year);


                                        strTotalTalbe = strTotalTalbe + strTotalTableHeader;

                                    }
                                }
                            }
                        }
                    }

                }
                return Ok(strTotalTalbe);
            }
            else
            {
                return Ok("Table not created!");
            }

            //int year = 0;
            //double _octHinsho = 0;
            //double _novHinsho = 0;
            //double _decHinsho = 0;
            //double _janHinsho = 0;
            //double _febHinsho = 0;
            //double _marHinsho = 0;
            //double _aprHinsho = 0;
            //double _mayHinsho = 0;
            //double _junHinsho = 0;
            //double _julHinsho = 0;
            //double _augHinsho = 0;
            //double _sepHinsho = 0;
            //List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            //int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            ////int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            //year = forecastLeatestYear;
            //List<Department> departments = departmentBLL.GetAllDepartments();
            //Department qaDepartmentByName = departments.Where(d => d.DepartmentName == "品証").SingleOrDefault();
            //if (qaDepartmentByName == null)
            //{
            //    return NotFound();
            //}
            //var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            //if (hinsoData.Count > 0)
            //{
            //    _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
            //    _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
            //    _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
            //    _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
            //    _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
            //    _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
            //    _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
            //    _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
            //    _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
            //    _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
            //    _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
            //    _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            //}

            //foreach (var department in departments)
            //{
            //    double rowTotal = 0;
            //    double firstSlot = 0;
            //    SukeyQADto sukeyDto = new SukeyQADto();
            //    sukeyDto.DepartmentId = department.Id.ToString();
            //    sukeyDto.DepartmentName = department.DepartmentName;
            //    //if (department.Id==8)
            //    //{
            //    //    continue;
            //    //}
            //    var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
            //    if (apportionmentByDepartment == null)
            //    {
            //        apportionmentByDepartment = new Apportionment();
            //    }


            //    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
            //    if (forecastAssignmentViewModels.Count > 0)
            //    {
            //        double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
            //        double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
            //        double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
            //        double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
            //        double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
            //        double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
            //        double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
            //        double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
            //        double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
            //        double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
            //        double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
            //        double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

            //        double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
            //        double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
            //        double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
            //        double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
            //        double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
            //        double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
            //        double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
            //        double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
            //        double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
            //        double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
            //        double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
            //        double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

            //        var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
            //        if (_octActualCostTotal > 0)
            //        {
            //            sukeyDto.OctCost.Add(_octActualCostTotal + _octCalculation);
            //            rowTotal += _octActualCostTotal + _octCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.OctCost.Add(_octTotal + _octCalculation);
            //            rowTotal += _octTotal + _octCalculation;
            //        }
            //        var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
            //        if (_novActualCostTotal > 0)
            //        {
            //            sukeyDto.NovCost.Add(_novActualCostTotal + _novCalculation);
            //            rowTotal += _novActualCostTotal + _novCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.NovCost.Add(_novTotal + _novCalculation);
            //            rowTotal += _novTotal + _novCalculation;
            //        }
            //        var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
            //        if (_decActualCostTotal > 0)
            //        {
            //            sukeyDto.DecCost.Add(_decActualCostTotal + _decCalculation);
            //            rowTotal += _decActualCostTotal + _decCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.DecCost.Add(_decTotal + _decCalculation);
            //            rowTotal += _decTotal + _decCalculation;
            //        }
            //        var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
            //        if (_janActualCostTotal > 0)
            //        {
            //            sukeyDto.JanCost.Add(_janActualCostTotal + _janCalculation);
            //            rowTotal += _janActualCostTotal + _janCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JanCost.Add(_janTotal + _janCalculation);
            //            rowTotal += _janTotal + _janCalculation;
            //        }
            //        var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
            //        if (_febActualCostTotal > 0)
            //        {
            //            sukeyDto.FebCost.Add(_febActualCostTotal + _febCalculation);
            //            rowTotal += _febActualCostTotal + _febCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.FebCost.Add(_febTotal + _febCalculation);
            //            rowTotal += _febTotal + _febCalculation;
            //        }
            //        var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
            //        if (_marActualCostTotal > 0)
            //        {
            //            sukeyDto.MarCost.Add(_marActualCostTotal + _marCalculation);
            //            rowTotal += _marActualCostTotal + _marCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.MarCost.Add(_marTotal + _marCalculation);
            //            rowTotal += _marTotal + _marCalculation;
            //        }
            //        sukeyDto.FirstSlot.Add(rowTotal);
            //        firstSlot = rowTotal;

            //        var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
            //        if (_aprActualCostTotal > 0)
            //        {
            //            sukeyDto.AprCost.Add(_aprActualCostTotal + _aprCalculation);
            //            rowTotal += _aprActualCostTotal + _aprCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
            //            rowTotal += _aprTotal + _aprCalculation;
            //        }
            //        var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
            //        if (_mayActualCostTotal > 0)
            //        {
            //            sukeyDto.MayCost.Add(_mayActualCostTotal + _mayCalculation);
            //            rowTotal += _mayActualCostTotal + _mayCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
            //            rowTotal += _mayTotal + _mayCalculation;
            //        }
            //        var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
            //        if (_junActualCostTotal > 0)
            //        {
            //            sukeyDto.JunCost.Add(_junActualCostTotal + _junCalculation);
            //            rowTotal += _junActualCostTotal + _junCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JunCost.Add(_junTotal + _junCalculation);
            //            rowTotal += _junTotal + _junCalculation;
            //        }
            //        var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
            //        if (_julActualCostTotal > 0)
            //        {
            //            sukeyDto.JulCost.Add(_julActualCostTotal + _julCalculation);
            //            rowTotal += _julActualCostTotal + _julCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.JulCost.Add(_julTotal + _julCalculation);
            //            rowTotal += _julTotal + _julCalculation;
            //        }
            //        var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
            //        if (_augActualCostTotal > 0)
            //        {
            //            sukeyDto.AugCost.Add(_augActualCostTotal + _augCalculation);
            //            rowTotal += _augActualCostTotal + _augCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.AugCost.Add(_augTotal + _augCalculation);
            //            rowTotal += _augTotal + _augCalculation;
            //        }
            //        var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
            //        if (_sepActualCostTotal > 0)
            //        {
            //            sukeyDto.SepCost.Add(_sepActualCostTotal + _sepCalculation);
            //            rowTotal += _sepActualCostTotal + _sepCalculation;
            //        }
            //        else
            //        {
            //            sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
            //            rowTotal += _sepTotal + _sepCalculation;
            //        }
            //        sukeyDto.RowTotal.Add(rowTotal);
            //        sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

            //    }
            //    else
            //    {
            //        sukeyDto.OctCost.Add(0);
            //        sukeyDto.NovCost.Add(0);
            //        sukeyDto.DecCost.Add(0);
            //        sukeyDto.JanCost.Add(0);
            //        sukeyDto.FebCost.Add(0);
            //        sukeyDto.MarCost.Add(0);
            //        sukeyDto.AprCost.Add(0);
            //        sukeyDto.MayCost.Add(0);
            //        sukeyDto.JunCost.Add(0);
            //        sukeyDto.JulCost.Add(0);
            //        sukeyDto.AugCost.Add(0);
            //        sukeyDto.SepCost.Add(0);
            //        sukeyDto.RowTotal.Add(0);
            //        sukeyDto.FirstSlot.Add(0);
            //        sukeyDto.SecondSlot.Add(0);
            //    }



            //    sukeyQADtos.Add(sukeyDto);
            //}
            //return Ok(sukeyQADtos);
        }
        
        [HttpGet]
        [Route("api/utilities/IsGradeExists/")]
        public IHttpActionResult IsGradeExists(string gradePoint)
        {
            int returnGradeId = 0;
            if (!string.IsNullOrEmpty(gradePoint))
            {
                returnGradeId = salaryBLL.GetGradeIdByGradePoint(gradePoint);
                return Ok(returnGradeId);
            }
            else
            {
                return Ok(returnGradeId);
            }
        }

        [HttpGet]
        [Route("api/utilities/GetSectionNameBySectionId/")]
        public IHttpActionResult GetSectionNameBySectionId(string sectionIds)
        {
            Section section = new Section();
            if (!string.IsNullOrEmpty(sectionIds))
            {
                var arrSectionId = sectionIds.Split(',');
                section = sectionBLL.GetSectionBySectionId(Convert.ToInt32(arrSectionId[0]));
            }
            
            return Ok(section);                            
        }
        [HttpGet]
        [Route("api/utilities/GetDepartmentByDepartemntId/")]
        public IHttpActionResult GetDepartmentByDepartemntId(string departmentId)
        {
            Department department = new Department();
            if (!string.IsNullOrEmpty(departmentId))
            {
                var arrDeptId = departmentId.Split(',');
                department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(arrDeptId[0]));
            }

            return Ok(department);
        }
        [HttpGet]
        [Route("api/utilities/GetInchargeNameByInchargeId/")]
        public IHttpActionResult GetInchargeNameByInchargeId(string inchargeId)
        {
            InCharge inCharge = new InCharge();
            if (!string.IsNullOrEmpty(inchargeId))
            {
                var arrInchargeId = inchargeId.Split(',');
                inCharge = inChargeBLL.GetInChargeByInChargeId(Convert.ToInt32(arrInchargeId[0]));
            }

            return Ok(inCharge);
        }
        [HttpGet]
        [Route("api/utilities/GetRoleNameByRoleId/")]
        public IHttpActionResult GetRoleNameByRoleId(string roleId)
        {
            Role role = new Role();
            if (!string.IsNullOrEmpty(roleId))
            {
                var arrRoleId = roleId.Split(',');
                role = roleBLL.GetRoleByRoleId(Convert.ToInt32(arrRoleId[0]));
            }

            return Ok(role);
        }
        [HttpGet]
        [Route("api/utilities/GetExplanationNameByExplanationId/")]
        public IHttpActionResult GetExplanationNameByExplanationId(string explanationId)
        {
            Explanation explanation = new Explanation();
            if (!string.IsNullOrEmpty(explanationId))
            {
                var arrExplanationId = explanationId.Split(',');
                explanation = explanationsBLL.GetExplanationByExplanationId(Convert.ToInt32(arrExplanationId[0]));
            }

            return Ok(explanation);
        }
        [HttpGet]
        [Route("api/utilities/GetCompanyByCompanyId/")]
        public IHttpActionResult GetCompanyByCompanyId(string companyId)
        {
            Company company = new Company();
            if (!string.IsNullOrEmpty(companyId))
            {
                var arrCompanyIds = companyId.Split(',');
                company = companyBLL.GetCompanyByCompanyId(Convert.ToInt32(arrCompanyIds[0]));
            }

            return Ok(company);
        }
        [HttpGet]
        [Route("api/utilities/GetSalaryBySalaryId/")]
        public IHttpActionResult GetSalaryBySalaryId(string salaryId)
        {
            Salary salary = new Salary();
            if (!string.IsNullOrEmpty(salaryId))
            {
                var arrSalaryIds = salaryId.Split(',');
                salary = salaryBLL.GetSalaryBySalaryId(Convert.ToInt32(arrSalaryIds[0]));
            }

            return Ok(salary);
        }

        [HttpGet]
        [Route("api/utilities/GetEmployeeById/")]
        public IHttpActionResult GetEmployeeById(string employeeId)
        {
            Employee employee = new Employee();

            if (!string.IsNullOrEmpty(employeeId))
            {
                var arrEmployeeId = employeeId.Split(',');

                employee = employeeBLL.GetEmployeeById(Convert.ToInt32(arrEmployeeId[0]));
            }

            return Ok(employee);
        }
    }
}
