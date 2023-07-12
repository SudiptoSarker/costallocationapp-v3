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
        public IHttpActionResult SearchForecastEmployee(string employeeName, string sectionId, string departmentId, string inchargeId, string roleId, string explanationId, string companyId, string status, string year,string timeStampId)        
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
                                    countMessage.Add(result + " projects assigned for " + section.SectionName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + section.SectionName);
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
                                    countMessage.Add(result + " projects assigned for " + department.DepartmentName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + department.DepartmentName);
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
                                    countMessage.Add(result + " projects assigned for " + company.CompanyName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + company.CompanyName);
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
                                    countMessage.Add(result + " projects assigned for " + inCharge.InChargeName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + inCharge.InChargeName);
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
                                    countMessage.Add(result + " projects assigned for " + role.RoleName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + role.RoleName);

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
                                    countMessage.Add(result + " projects assigned for " + explanation.ExplanationName);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + explanation.ExplanationName);
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
                                    countMessage.Add(result + " projects assigned for " + salary.SalaryGrade);
                                }
                                else
                                {
                                    countMessage.Add(result + " project assigned for " + salary.SalaryGrade);
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
                        int updateResult =  employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

                        forecasts.Add(ExtraxctToForecast(Convert.ToInt32(item.AssignmentId),item.Year,10,item.OctPoint));
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
                        message = "Data Saved Successfully!!!";
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
            List<Forecast> forecasts = new List<Forecast>();
            List<Forecast> forecastsPrevious = new List<Forecast>();
            List<AssignmentHistory> assignmentHistories = new List<AssignmentHistory>();
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

                        //AssignmentHistory
                        //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory);
                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(employeeAssignment.Id);
                        
                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;

                        //check if assignment exists in original
                        //int  checkResults = employeeAssignmentBLL.CheckForOriginalAssignmentIsExists(employeeAssignment.Id);

                        //if (checkResults > 0) {
                        //    //update original data
                        //    int updateOriginalAssignmentDataResults = employeeAssignmentBLL.UpdateOriginalAssignment(_assignmentHistory);
                        //}
                        //else
                        //{
                        //    //insert original data
                        //    int intsertOriginalData = employeeAssignmentBLL.InsertOriginalAssignment(_assignmentHistory);
                        //}

                        if (forecastHistoryDto.CellInfo.Count > 0)
                        {
                            foreach (var cellItem in forecastHistoryDto.CellInfo)
                            {                                
                                var itemData = cellItem.Split('_');
                                if(Convert.ToInt32(itemData[0]) == employeeAssignment.Id)
                                {
                                    int checkResults = employeeAssignmentBLL.CheckForOriginalAssignmentIsExists(employeeAssignment.Id);

                                    if (Convert.ToInt32(itemData[1]) == 2)
                                    {
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending, "2",_assignmentHistory.BCYRCell);
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
                        int updateResult = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

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
                                    //check if forecast data already exists in original
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
                                        bool validForOriginalData = employeeAssignmentBLL.CheckForValidOriginalData(_assignmentHistory.BCYRCellPending,"12", _assignmentHistory.BCYRCell);

                                        if (validForOriginalData) { 
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

                        if (forecasts.Count>0)
                        {
                            foreach (var forecast in forecasts)
                            {
                                forecastBLL.UpdateForecast(forecast);
                            }
                            forecasts = new List<Forecast>();
                        }
                    }

                    foreach (var forecastPrevious in forecastsPrevious)
                    {
                        forecastPrevious.CreatedBy = session["userName"].ToString();
                        forecastPrevious.CreatedDate = DateTime.Now;
                    }
                    
                    ForecastHisory forecastHisory = new ForecastHisory();
                    forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                    forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                    forecastHisory.Forecasts = forecastsPrevious;
                    forecastHisory.CreatedBy = session["userName"].ToString();
                    forecastHisory.CreatedDate = DateTime.Now;

                    //author: sudipto,31/5/23: history create
                    //var resultTimeStamp = forecastBLL.CreateTimeStamp(forecastHisory);
                    bool isUpdate = true;
                    var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate);

                    if (forecastHistoryDto.CellInfo.Count > 0)
                    {
                        foreach (var item in forecastHistoryDto.CellInfo)
                        {
                            var storePreviousCells = "";
                            var itemData = item.Split('_');
                            string result = employeeAssignmentBLL.GetBCYRCellByAssignmentId(Convert.ToInt32(itemData[0]));
                            if (String.IsNullOrEmpty(result))
                            {
                                //result += itemData[1];
                                storePreviousCells += itemData[1];
                            }
                            else
                            {
                                var arrPreviousCells = result.Split(',');
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

                                            foreach(var checkExitsItem in arrCheckForCellExits)
                                            {
                                                if(checkExitsItem == previousItem)
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
                                            if(storePreviousCells != previousItem) {
                                                storePreviousCells = storePreviousCells + "," + previousItem;
                                            }
                                        }
                                    }
                                }

                                var arrCheckForAlreadyExitsCell = storePreviousCells.Split(',');
                                var isValidForUpdateCell = true;

                                foreach(var cellItem in arrCheckForAlreadyExitsCell)
                                {                                   
                                    if(cellItem == itemData[1])
                                    {
                                        isValidForUpdateCell = false;
                                    }
                                }
                                if (isValidForUpdateCell) { 
                                    //result += "," + itemData[1];
                                    storePreviousCells += "," + itemData[1];
                                }
                            }
                            employeeAssignmentBLL.UpdateBCYRCellByAssignmentId(Convert.ToInt32(itemData[0]), storePreviousCells);
                        }
                    }
                    

                    if (resultTimeStamp > 0)
                    {
                        //message = "Data Saved Successfully!!!";
                        message = resultTimeStamp.ToString();
                    }
                }
            }

            return Ok(message);
        }

        [HttpGet]
        public IHttpActionResult ApprovedForecastData(string assignementId,bool isDeletedRow)
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
            bool isValidForUnapprovedRow = employeeAssignmentBLL.CheckForUnApprovedRow(assignementId,isDeletedRow);
            if (isValidForUnapprovedRow) { 
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
            if (resultData>0) { 
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

                if (isUpdateData) { 
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
            bool isValidForUnapproved = employeeAssignmentBLL.CheckForUnApprovedCells(assignementId,selectedCells);
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
                return NotFound();
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

        public Forecast ExtraxctToForecast(int assignmentId,int year, int monthId,decimal point)
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
                    return BadRequest("Employee Already Exists!!!");
                }
                else
                {
                    employee.IsActive = true;
                    employee.CreatedBy = session["userName"].ToString();
                    employee.CreatedDate = DateTime.Now;
                    int result = employeeBLL.CreateEmployee(employee);
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
                    if (user.UserRoleId=="1")
                    {
                        foreach (var item in UserLinks.adminLinks)
                        {
                            userBLL.CreateUserPermissions(item,user.Id);
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
            if (employees.Count>0)
            {
                return Ok(employees);
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
            if (users.Count>0)
            {
                foreach (var item in users)
                {
                    if (item.Id==user.Id)
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
        public IHttpActionResult UpdateUserStatus(string changeUserName,string changeUserRole, string changeUserStatus)
        {
            string userRoleId = "";
            bool isActive = false;
            if(string.IsNullOrEmpty(changeUserRole) && changeUserStatus == "1")
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
                }else if(changeUserRole.ToLower() == "editor")
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
            if (user.UserRoleId=="1")
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
                        if (item.Id==user.Id)
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
            var session = System.Web.HttpContext.Current.Session;
            if (forecastHistoryDto.ForecastUpdateHistoryDtos != null)
            {
                if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count > 0)
                {
                    foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();

                        tempTimeStampId = forecastHistoryDto.TimeStampId;

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
                        employeeAssignment.SubCode = 1;
                        employeeAssignment.CreatedBy = session["userName"].ToString();
                        employeeAssignment.CreatedDate = DateTime.Now;
                        employeeAssignment.Remarks = item.Remarks;
                        
                        //check for bcyr value
                        employeeAssignment.BCYR = item.BCYR;
                        employeeAssignment.BCYRCell = "";

                        employeeAssignment.EmployeeName = item.EmployeeName;
                        int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);

                        if (result == 1)
                        {
                            int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                            returnedIdList.Add(new {
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

                        //AssignmentHistory
                        //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory);
                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(lastAssignmentId);
                        
                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;
                        assignmentHistories.Add(_assignmentHistory);
                    }

                    bool isUpdate = false;
                    if (!string.IsNullOrEmpty(tempTimeStampId))
                    {
                        foreach (var item in assignmentHistories)
                        {
                            forecastBLL.CreateAssignmenttHistory(item, Convert.ToInt32(tempTimeStampId), isUpdate);
                        }
                    }
                    else
                    {
                        ForecastHisory forecastHisory = new ForecastHisory();
                        forecastHisory.TimeStamp = forecastHistoryDto.HistoryName;
                        forecastHisory.Year = forecastHistoryDto.ForecastUpdateHistoryDtos[0].Year;
                        //forecastHisory.Forecasts = forecastsPrevious;
                        forecastHisory.CreatedBy = session["userName"].ToString();
                        forecastHisory.CreatedDate = DateTime.Now;
                        var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate);
                    }                    
                }
            }    
            return Ok(returnedIdList);
        }


        [HttpDelete]
        [Route("api/utilities/ExcelDeleteAssignment/")]
        public IHttpActionResult DeleteAssignment_Excel(int[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (var item in ids)
                {
                    employeeAssignmentBLL.RemoveAssignment(item);
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
            if (forecastHistoryDto.ForecastUpdateHistoryDtos.Count>0)
            {
                foreach (var item in forecastHistoryDto.ForecastUpdateHistoryDtos)
                {
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(Convert.ToInt32(item.AssignmentId),user.LoginTime);
                    //var compareResult = TimeSpan.Compare(user.LoginTime.TimeOfDay,result.CreatedDate.TimeOfDay);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    //if (compareDate>=0)
                    //{
                        if (compareDate >= 0)
                        {
                            if (user.UserName==result.CreatedBy)
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
                    var compareDate = DateTime.Compare(result.CreatedDate,user.LoginTime);
                    if (compareDate>=0)
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
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(Convert.ToInt32(item.AssignmentId),user.LoginTime);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    if (compareDate >= 0)
                    {
                        if (user.UserName==result.CreatedBy)
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
        [Route("api/utilities/DuplicateForecastYear/")]
        public IHttpActionResult DuplicateForecastYear(int copyYear, int insertYear)
        {
            var result = forecastBLL.DuplicateForecastYear(copyYear, insertYear);
            return Ok(result);
        }

        [HttpGet]
        [Route("api/utilities/GetHistoriesByTimeStampId/")]
        public IHttpActionResult GetHistoriesByTimeStampId(int timeStampId)
        {
            List<object> forecastHistoryList = new List<object>();
            List<Forecast> historyList =  forecastBLL.GetHistoriesByTimeStampId(timeStampId);
            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            if (distinctAssignmentId.Count>0)
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
                        CreatedBy= historyList[0].CreatedBy,
                        OctPoints = octP == octPOriginal ? "": "("+ octP.ToString("0.0") + ") "+ octPOriginal.ToString("0.0"),
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
            List<Forecast> historyList = forecastBLL.GetAssignmentHistoriesByTimeStampId(timeStampId);

            List<int> distinctAssignmentId = historyList.Select(h => h.EmployeeAssignmentId).Distinct().ToList();
            if (distinctAssignmentId.Count > 0)
            {
                //<tr><td>${element.CreatedBy}</td><td>${element.EmployeeName}</td><td>${forecastType}</td><td>${element.Remarks}</td><td>${element.SectionName}</td><td>${element.DepartmentName}</td><td>${element.InChargeName}</td><td>${element.RoleName}</td><td><lable>${element.ExplanationName}</label></td><td>${element.CompanyName}</td><td>${element.GradePoints}</td><td>${element.UnitPrice}</td><td>${element.OctPoints}</td><td>${element.NovPoints}</td><td>${element.DecPoints}</td><td>${element.JanPoints}</td><td>${element.FebPoints}</td><td>${element.MarPoints}</td><td>${element.AprPoints}</td><td>${element.MayPoints}</td><td>${element.JunPoints}</td><td>${element.JulPoints}</td><td>${element.AugPoints}</td><td>${element.SepPoints}</td></tr>`
                foreach (var item in distinctAssignmentId)
                {
                    AssignmentHistoryViewModal _assignmentHistoryViewModal = new AssignmentHistoryViewModal();
                    AssignmentHistoryViewModal _objOriginalForecastedData = new AssignmentHistoryViewModal();
                    _assignmentHistoryViewModal = forecastBLL.GetAssignmentNamesForHistory(item, timeStampId);

                    //var employeeName = employeeBLL.GetEmployeeNameByAssignmentId(item);
                    var employeeName = _assignmentHistoryViewModal.EmployeeName;
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

                    _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

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

                    if (isUpdate)
                    {
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,                            
                            IsUpdate = isUpdate,
                            //EmployeeName = employeeName == _objOriginalForecastedData.EmployeeName ? "" : "(" + employeeName + ") " + _objOriginalForecastedData.EmployeeName,
                            SectionName = sectionName == _objOriginalForecastedData.SectionName ? "" : "(" + sectionName + ") " + _objOriginalForecastedData.SectionName,
                            DepartmentName = departmentName == _objOriginalForecastedData.DepartmentName ? "" : "(" + departmentName + ") " + _objOriginalForecastedData.DepartmentName,
                            InChargeName = inChargeName == _objOriginalForecastedData.InChargeName ? "" : "(" + inChargeName + ") " + _objOriginalForecastedData.InChargeName,
                            RoleName = roleName == _objOriginalForecastedData.RoleName ? "" : "(" + roleName + ") " + _objOriginalForecastedData.RoleName,
                            ExplanationName = explanationName == _objOriginalForecastedData.ExplanationName ? "" : "(" + explanationName + ") " + _objOriginalForecastedData.ExplanationName,
                            CompanyName = companyName == _objOriginalForecastedData.CompanyName ? "" : "(" + companyName + ") " + _objOriginalForecastedData.CompanyName,
                            GradePoints = gradePoints == _objOriginalForecastedData.GradePoints ? "" : "(" + gradePoints + ") " + _objOriginalForecastedData.GradePoints,
                            //UnitPrice = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice,
                            UnitPrice = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + Convert.ToInt32(unitPrice).ToString("N0") + ") " + Convert.ToInt32(_objOriginalForecastedData.UnitPrice).ToString("N0"),
                            Remarks = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType= "Updated",
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
                        //insert data udpate
                        forecastHistoryList.Add(new
                        {
                            EmployeeName = employeeName,
                            IsUpdate = isUpdate,
                            SectionName = sectionName == ""? "" : sectionName,                                                         
                            DepartmentName = departmentName == "" ? "" : departmentName,
                            InChargeName = inChargeName == "" ? "" : inChargeName,
                            RoleName = roleName == "" ? "" : roleName,
                            ExplanationName = explanationName == "" ? "" : explanationName,
                            CompanyName = companyName == "" ? "" : companyName,                             
                            GradePoints = gradePoints == "0" ? "" : gradePoints,
                            UnitPrice = unitPrice == "0" ? "" : Convert.ToInt32(unitPrice).ToString("N0"),
                            Remarks = remarks == "" ? "" : remarks,
                            CreatedBy = historyList[0].CreatedBy,
                            OperationType = "Inserted",
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

                    _objOriginalForecastedData = forecastBLL.GetOriginalForecastedData(item);

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
                            OperationType="Add Employee",
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
                            OperationType = "Delete Employee",
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
                        var cellWiseOriginalData = forecastBLL.GetCellWiseUpdateOriginalData(item,timeStampId);

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
                            oct_Cell = forecastBLL.GetApproveForecastCellData(11, octPrevious, octOrg, approvedCells);
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
                            nov_Cell = forecastBLL.GetApproveForecastCellData(12, novPrevious, novOrg, approvedCells);
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
                            dec_Cell = forecastBLL.GetApproveForecastCellData(13, decPrevious, decOrg, approvedCells);
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
                            jan_Cell = forecastBLL.GetApproveForecastCellData(14, janPrevious, janOrg, approvedCells);
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
                            feb_Cell = forecastBLL.GetApproveForecastCellData(15, febPrevious, febOrg, approvedCells);
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
                            mar_Cell = forecastBLL.GetApproveForecastCellData(16, marPrevious, marOrg, approvedCells);
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
                            apr_Cell = forecastBLL.GetApproveForecastCellData(17, aprPrevious, aprOrg, approvedCells);
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
                            may_Cell = forecastBLL.GetApproveForecastCellData(18, mayPrevious, mayOrg, approvedCells);
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
                            jun_Cell = forecastBLL.GetApproveForecastCellData(19, junPrevious, junOrg, approvedCells);
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
                            jul_Cell = forecastBLL.GetApproveForecastCellData(20, julPrevious, julOrg, approvedCells);
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
                            aug_Cell = forecastBLL.GetApproveForecastCellData(21, augPrevious, augOrg, approvedCells);
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
                            sep_Cell = forecastBLL.GetApproveForecastCellData(22, sepPrevious, sepOrg, approvedCells);
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
                            OperationType = "Cell Update",
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
                    var actualCost = actualCosts.Where(ac=>ac.AssignmentId==item.Id).SingleOrDefault();
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
        public IHttpActionResult GetActualCostConfirmData(int year,int monthId)
        {
            List<ActualCostViewModel> actualCostViewModels = new List<ActualCostViewModel>();

            //List<EmployeeAssignmentViewModel> employeeAssignments = employeeAssignmentBLL.GetAssignmentsByYear(year);

            List<EmployeeAssignmentViewModel> employeeAssignments = employeeAssignmentBLL.GetSpecificAssignmentDataData(year,monthId);
            foreach (var item in employeeAssignments)
            {
                var actualCostList = actualCostBLL.GetActualCostsByYear_AssignmentId(year,item.Id);
                if (actualCostList.Count>0)
                {
                    if (monthId==10)
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

            //List<ActualCost> actualCosts = actualCostBLL.GetActualCostsByYear(year);

            //if (actualCosts.Count > 0)
            //{
            //    foreach (var item in employeeAssignments)
            //    {
            //        var actualCost = actualCosts.Where(ac => ac.AssignmentId == item.Id).SingleOrDefault();
            //        if (actualCost == null)
            //        {
            //            actualCostViewModels.Add(MergeAssignmentWithActualCost(item, null));
            //        }
            //        else
            //        {
            //            actualCostViewModels.Add(MergeAssignmentWithActualCost(item, actualCost));
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (var item in employeeAssignments)
            //    {
            //        actualCostViewModels.Add(MergeAssignmentWithActualCost(item, null));

            //    }
            //}

            //var totalCount = actualCostViewModels.Count;

            //actualCostViewModels.Add(new ActualCostViewModel
            //{
            //    AssignmentId = 0,
            //    OctCost = $@"=SUM(J{1}:J{totalCount})",
            //    NovCost = $@"=SUM(K{1}:K{totalCount})",
            //    DecCost = $@"=SUM(L{1}:L{totalCount})",
            //    JanCost = $@"=SUM(M{1}:M{totalCount})",
            //    FebCost = $@"=SUM(N{1}:N{totalCount})",
            //    MarCost = $@"=SUM(O{1}:O{totalCount})",
            //    AprCost = $@"=SUM(P{1}:P{totalCount})",
            //    MayCost = $@"=SUM(Q{1}:Q{totalCount})",
            //    JunCost = $@"=SUM(R{1}:R{totalCount})",
            //    JulCost = $@"=SUM(S{1}:S{totalCount})",
            //    AugCost = $@"=SUM(T{1}:T{totalCount})",
            //    SepCost = $@"=SUM(U{1}:U{totalCount})",
            //});
            return Ok(employeeAssignments);
        }

        [HttpPost]
        [Route("api/utilities/CreateActualCost/")]
        public IHttpActionResult CreateActualCost(ActualCostDto actualCostDto)
        {
            string costColumnName = "";
            string pointColumnName = "";
            var session = System.Web.HttpContext.Current.Session;
            if (actualCostDto.ActualCosts.Count>0)
            {
                foreach (var item in actualCostDto.ActualCosts)
                {
                    if (item.AssignmentId==0)
                    {
                        continue;
                    }
                    
                    var flag = actualCostBLL.CheckAssignmentId(item.AssignmentId, actualCostDto.Year);
                    if (flag)
                    {
                        if (actualCostDto.Month==10)
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
                        actualCostBLL.UpdateActualCost(actualCostDto.Year, item.AssignmentId, costColumnName, pointColumnName,item.ActualCostAmount,item.ManHour, item.UpdatedBy,item.UpdatedDate);
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

            if (actualCost!=null)
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
                        if (item.DepartmentId=="8")
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
            if (apportionment.Apportionments.Count>0)
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
        public IHttpActionResult GetSukeyWithQA(int year=2023)
        {
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();

            List<SukeyDto> sukeys = actualCostBLL.GetAllSukeyData(year);

            //var sukeyList = actualCostBLL.GetAllSukeyData(year);

            var singleHinsho = sukeys.Where(s => s.DepartmentId == "8").FirstOrDefault();

            var apportionmentList =  actualCostBLL.GetAllApportionmentData(year);
            if (apportionmentList.Count>0)
            {
                foreach (var item in sukeys)
                {
                    if (item.DepartmentId=="8")
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
            //List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);
            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetAllAssignmentData(employeeAssignment);
            List<ForecastAssignmentViewModel> _forecsatEmployeeAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            return Ok(forecsatEmployeeAssignmentViewModels);
        }

        [HttpGet]
        [Route("api/utilities/UpdateApprovedData/")]
        public IHttpActionResult UpdateApprovedData(string assignmentYear,string historyName,string approvalCellsWithAssignmentId,string approvedRows)
        {
            int results = 0;
            int updateResults = 0;

            //approve history: start
            var session = System.Web.HttpContext.Current.Session;
            string createdBy = session["userName"].ToString();
            DateTime createdDate = DateTime.Now;
            int approveTimeStampId = 0;

            List<AssignmentHistory> _assignmentHistories_Add = new List<AssignmentHistory>();
            //_assignmentHistories_Add = forecastBLL.GetAddEmployeeApprovedData(Convert.ToInt32(assignmentYear));

            List<AssignmentHistory> _assignmentHistorys_Delete = new List<AssignmentHistory>();
            //_assignmentHistorys_Delete = forecastBLL.GetDeleteEmployeeApprovedData(Convert.ToInt32(assignmentYear));

            //row wise update: start          
            if (!string.IsNullOrEmpty(approvedRows))
            {
                var arrApprovalRowIds = approvedRows.Split(',');
                foreach (var approvedRowId in arrApprovalRowIds)
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

            string approvedCellAssignmentId = "";
            string approvedCellNo = "";
            List<AssignmentHistory> _assignmentHistorys_CellWise = new List<AssignmentHistory>();
            //_assignmentHistorys_CellWise = forecastBLL.GetCellWiseEmployeeApprovedData(Convert.ToInt32(assignmentYear));
            
            //update cells: start            
            if (!string.IsNullOrEmpty(approvalCellsWithAssignmentId))
            {
                var arrCellWithAssignmentids = approvalCellsWithAssignmentId.Split(',');
                foreach(var cellAndAssignmentIdItem in arrCellWithAssignmentids)
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
                            foreach(var item in arrPendingCells)
                            {
                                if(item != arrCellAndAssignmentId[1])
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

            if (_assignmentHistories_Add.Count>0 || _assignmentHistorys_Delete.Count > 0 || _assignmentHistorys_CellWise.Count > 0)
            {
                approveTimeStampId = forecastBLL.CreateApproveTimeStamp(historyName, Convert.ToInt32(assignmentYear), createdBy, createdDate);
                if(approveTimeStampId > 0)
                {
                    int approveResults = forecastBLL.CreateApprovetHistory(approveTimeStampId, Convert.ToInt32(assignmentYear), createdBy,_assignmentHistories_Add,_assignmentHistorys_Delete,_assignmentHistorys_CellWise);
                }
                //store approved row for download excel
                if(_assignmentHistories_Add.Count > 0)
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
                            foreach(var approvedCellItem in arrApprovedCells)
                            {
                                if (string.IsNullOrEmpty(storeApprovedCells))
                                {
                                    storeApprovedCells = approvedCellItem;
                                }
                                else
                                {
                                    var arrCheckIfTheCellsAlreadyExists = storeApprovedCells.Split(',');
                                    foreach(var indexItem in arrCheckIfTheCellsAlreadyExists)
                                    {
                                        if(indexItem != approvedCellItem)
                                        {
                                            storeApprovedCells = storeApprovedCells+","+approvedCellItem;
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
                insertResults = forecastBLL.InsertApprovedForecastedDataByYear(approveTimeStampId,Convert.ToInt32(assignmentYear), session["userName"].ToString());
                if (_assignmentHistories_Add.Count > 0 || _assignmentHistorys_Delete.Count > 0 || _assignmentHistorys_CellWise.Count > 0)
                {
                    //approved row
                    if (_assignmentHistories_Add.Count > 0)
                    {
                        foreach (var addRowItem in _assignmentHistories_Add)
                        {
                            int updateEmployeeAssignmentApprovedCellsResults = forecastBLL.UpdateApprovedData_AddRow(addRowItem,approveTimeStampId,assignmentYear);
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
                            if (!string.IsNullOrEmpty(arrCellWithId[0].ToString())) {
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

                                int tempResults = forecastBLL.UpdateApprovedCells(assignmentId, tempApprovedCells, approveTimeStampId,Convert.ToInt32(assignmentYear));
                            }
                        }
                    }
                    
                    cleanResults = forecastBLL.CleanPreviousApprovedDeletedRows(Convert.ToInt32(assignmentYear),approveTimeStampId);
                }
            }

            if (results > 0 || updateResults > 0 || cleanResults>0)
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

        //            sheet.Cells["C1"].Value = "Operation Type";
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
                if (item.DepartmentName== "品証")
                {
                    continue;
                }
                departments.Add(item);
            }

            return Ok(departments);
        }
    }
}
