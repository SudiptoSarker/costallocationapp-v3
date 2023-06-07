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
        //public IHttpActionResult SearchForecastEmployee(EmployeeAssignmentDTO employeeAssignment)
        {
            ////employeeName = "";
            ////sectionId = "";
            ////departmentId = "";
            ////inchargeId = "";
            ////roleId = "";
            ////explanationId = "";
            ////companyId = "";
            ////status = "";
            ////year = "";

            //EmployeeAssignmentf employeeAssignment = new EmployeeAssignment();
            EmployeeAssignmentForecast employeeAssignment = new EmployeeAssignmentForecast();

            if (!string.IsNullOrEmpty(employeeName))
            {
                employeeAssignment.EmployeeName = employeeName.Trim();
            }
            else
            {
                employeeAssignment.EmployeeName = "";
            }
            //if (sectionId != null)
            //{
            //    if (sectionId.Length > 0)
            //    {
            //        string ids = "";
            //        foreach (var item in sectionId)
            //        {
            //            ids += $"{item},";
            //        }
            //        ids = ids.TrimEnd(',');

            //        //where += $" ea.SectionId in ({ids}) and ";
            //    }

            //}

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

        //[HttpGet]
        //public IHttpActionResult SearchForecastEmployee(EmployeeAssignmentDTO employeeAssignment)
        //{
        //    List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastBySearchFilter(employeeAssignment);

        //    return Ok(forecsatEmployeeAssignmentViewModels);
        //}


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
                        employeeAssignment.Id = item.AssignmentId;
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

                        forecasts.Add(ExtraxctToForecast(item.AssignmentId,item.Year,10,item.OctPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 11, item.NovPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 12, item.DecPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 1, item.JanPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 2, item.FebPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 3, item.MarPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 4, item.AprPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 5, item.MayPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 6, item.JunPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 7, item.JulPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 8, item.AugPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 9, item.SepPoint));


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
                        employeeAssignment.Id = item.AssignmentId;
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

                        //AssignmentHistory
                        //var resultTimeStamp = forecastBLL.CreateTimeStampAndAssignmentHistory(forecastHisory);
                        AssignmentHistory _assignmentHistory = new AssignmentHistory();
                        _assignmentHistory = forecastBLL.GetPreviousAssignmentDataById(employeeAssignment.Id);
                        
                        _assignmentHistory.CreatedBy = session["userName"].ToString();
                        _assignmentHistory.CreatedDate = DateTime.Now;

                        int updateResult = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

                        forecastsPrevious.AddRange(forecastBLL.GetForecastsByAssignmentId(item.AssignmentId));
                        assignmentHistories.Add(_assignmentHistory);


                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 10, item.OctPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 11, item.NovPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 12, item.DecPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 1, item.JanPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 2, item.FebPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 3, item.MarPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 4, item.AprPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 5, item.MayPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 6, item.JunPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 7, item.JulPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 8, item.AugPoint));
                        forecasts.Add(ExtraxctToForecast(item.AssignmentId, item.Year, 9, item.SepPoint));

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
                            var itemData = item.Split('_');
                            string result = employeeAssignmentBLL.GetBCYRCellByAssignmentId(Convert.ToInt32(itemData[0]));
                            if (String.IsNullOrEmpty(result))
                            {
                                result += itemData[1];
                            }
                            else
                            {
                                result += "," + itemData[1];
                            }
                            employeeAssignmentBLL.UpdateBCYRCellByAssignmentId(Convert.ToInt32(itemData[0]), result);
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
            int results;
            if (!isDeletedRow)
            {
                results = employeeAssignmentBLL.UnApproveDeletedRow(assignementId);
            }
            else
            {
                results = employeeAssignmentBLL.UnApproveAssignement(assignementId);
            }
            return Ok(results);
        }

        [HttpGet]
        public IHttpActionResult ApprovedCellData(string assignementId, string selectedCells)
        {
            EmployeeAssignment _employeeAssignment = new EmployeeAssignment();
            bool isUpdateData = false;

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
                if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCell))
                {
                    var arrBYCRCells = _employeeAssignment.BCYRCell.Split(',');                
                    foreach (var bycrCell in arrBYCRCells)
                    {
                        if (bycrCell != selectedCells)
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
                                    if(cellIteam == bycrCell)
                                    {                                        
                                        isCellExists = true;
                                        break;
                                    }
                                }
                                if (!isCellExists) { 
                                    storeBYCRCells = storeBYCRCells+","+bycrCell;
                                }
                            }
                        }
                    }
                }

                int results = employeeAssignmentBLL.UpdateBYCRCells(assignementId, _employeeAssignment.BCYRCellApproved,storeBYCRCells);
                return Ok(results);
            }
            else
            {
                return Ok(0); ;
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
                            approvedBCYRCellList = approvedBCYRCellList+","+bycrCellApproved;
                        }
                    }
                }
            }

            string bCYRCellList = "";
            //unapproved: start
            if (!string.IsNullOrEmpty(_employeeAssignment.BCYRCell))
            {
                isUpdateData = true;
                bCYRCellList = selectedCells+","+_employeeAssignment.BCYRCell;

                //var arrBCYRCells = _employeeAssignment.BCYRCell.Split(',');
                //foreach (var bycrCellItem in arrBCYRCells)
                //{
                //    isUpdateData = true;
                //    if (bycrCellItem != selectedCells)
                //    {
                //        if(bCYRCellList =="")
                //        {
                //            bCYRCellList = selectedCells;
                //        }
                //        else
                //        {
                //            bCYRCellList = bCYRCellList + "," + selectedCells;
                //        }
                //    }
                //    else
                //    {
                //        var arrCheckForCellValueIsExists = bCYRCellList.Split(',');
                //        bool isCellExists = false;
                //        foreach (var cellIteam in arrCheckForCellValueIsExists)
                //        {
                //            if (cellIteam == bycrCellItem)
                //            {
                //                isCellExists = true;
                //                break;
                //            }
                //        }
                //        if (!isCellExists)
                //        {
                //            if (bCYRCellList == "")
                //            {
                //                bCYRCellList = bycrCellItem;
                //            }
                //            else
                //            {
                //                bCYRCellList = bCYRCellList + "," + bycrCellItem;
                //            }
                //        }
                //    }
                //}
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
                        employeeAssignment.UnitPrice = item.UnitPrice;
                        employeeAssignment.GradeId = item.GradeId;
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
            return Ok("Data Inserted Successfully!!!");
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
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(item.AssignmentId,user.LoginTime);
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
                    var result = forecastBLL.MatchForecastHistoryUsernamesByAssignmentId(item.AssignmentId, user.LoginTime);
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
                    var result = forecastBLL.MatchForecastHistoryByAssignmentId(item.AssignmentId,user.LoginTime);
                    var compareDate = DateTime.Compare(result.CreatedDate, user.LoginTime);
                    if (compareDate >= 0)
                    {
                        if (user.UserName==result.CreatedBy)
                        {
                            continue;
                        }
                        // latest assignment history
                        var resultList = forecastBLL.GetMatchedForecastHistoryByAssignmentId(item.AssignmentId);
                    
                        var singleForecastList = forecastBLL.GetForecastsByAssignmentId(item.AssignmentId);


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
                            UnitPrice = unitPrice == _objOriginalForecastedData.UnitPrice ? "" : "(" + unitPrice + ") " + _objOriginalForecastedData.UnitPrice,
                            Remarks = remarks == _objOriginalForecastedData.Remarks ? "" : "(" + remarks + ") " + _objOriginalForecastedData.Remarks,
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
                            UnitPrice = unitPrice == "0" ? "" : unitPrice,
                            Remarks = remarks == "" ? "" : remarks,
                            CreatedBy = historyList[0].CreatedBy,                            
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
                            UnitPrice = unitPrice == "0" ? "" : unitPrice,
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
                            UnitPrice = unitPrice == "0" ? "" : unitPrice,
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

                        var employeeName_Cells = _approvalHistoryViewModal.EmployeeName;

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
            var apportionmentsC = 0;
            if (apportionmentsC > 0)
            {
                var count = 1;
                foreach (var item in distinctDepartmentIds)
                {
                    var department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item));
                    apportionmentList.Add(new
                    {
                        DepartmentId = department.Id,//a
                        DepartmentName = department.DepartmentName, //b

                        OctActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().OctCost,//c
                        OctPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().OctPercentage,//d
                        OctResult = $@"=C{count}+((D{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().OctCost})",//e

                        NovActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().NovCost,//f
                        NovPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().NovPercentage,//g
                        NovResult = $@"=F{count}+(G{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().NovCost}",//h

                        DecActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().DecCost,//i
                        DecPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().DecPercentage,//j
                        DecResult = $@"=I{count}+(J{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().DecCost}",//k

                        JanActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JanCost,//l
                        JanPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JanPercentage,//m
                        JanResult = $@"=L{count}+(M{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JanCost}",//n

                        FebActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().FebCost,//o
                        FebPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().FebPercentage,//p
                        FebResult = $@"=O{count}+(P{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().FebCost}",//q

                        MarActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MarCost,//r
                        MarPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().MarPercentage,//s
                        MarResult = $@"=R{count}+(S{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MarCost}",//t

                        AprActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AprCost,//u
                        AprPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().AprPercentage,//v
                        AprResult = $@"=U{count}+(V{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AprCost}",//w

                        MayActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MayCost,//x
                        MayPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().MayPercentage,//y
                        MayResult = $@"=X{count}+(Y{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MayCost}",//z

                        JunActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JunCost,//aa
                        JunPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JunPercentage,//ab
                        JunResult = $@"=AA{count}+(AB{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JunCost}",//ac

                        JulActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JulCost,//ad
                        JulPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().JulPercentage,//ae
                        JulResult = $@"=AD{count}+(AE{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JulCost}",//af

                        AugActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AugCost,//ag
                        AugPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().AugPercentage,//ah
                        AugResult = $@"=AG{count}+(AH{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AugCost}",//ai

                        SepActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().SepCost,//aj
                        SepPercentage = apportionments.Where(a => a.DepartmentId.ToString() == item).FirstOrDefault().SepPercentage,//ak
                        SepResult = $@"=AJ{count}+(AK{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().SepCost}",//al
                    });
                    count++;
                }
            }
            else
            {
                var count = 1;
                foreach (var item in distinctDepartmentIds)
                {
                    var department = departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item));
                    apportionmentList.Add(new {
                        DepartmentId= department.Id,//a
                        DepartmentName = department.DepartmentName, //b

                        //OctActualCost= sukeyList.Where(s=>s.DepartmentId== item).FirstOrDefault().OctCost,//c
                        OctPercentage = 0,//d
                        //OctResult=$@"=C{count}+((D{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().OctCost})",//e

                        //NovActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().NovCost,//f
                        NovPercentage = 0,//g
                        //NovResult = $@"=F{count}+(G{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().NovCost}",//h

                        //DecActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().DecCost,//i
                        DecPercentage = 0,//j
                        //DecResult = $@"=I{count}+(J{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().DecCost}",//k

                        //JanActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JanCost,//l
                        JanPercentage = 0,//m
                        //JanResult = $@"=L{count}+(M{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JanCost}",//n

                        //FebActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().FebCost,//o
                        FebPercentage = 0,//p
                        //FebResult = $@"=O{count}+(P{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().FebCost}",//q

                        //MarActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MarCost,//r
                        MarPercentage = 0,//s
                        //MarResult = $@"=R{count}+(S{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MarCost}",//t

                        //AprActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AprCost,//u
                        AprPercentage = 0,//v
                        //AprResult = $@"=U{count}+(V{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AprCost}",//w

                        //MayActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().MayCost,//x
                        MayPercentage = 0,//y
                        //MayResult = $@"=X{count}+(Y{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().MayCost}",//z

                        //JunActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JunCost,//aa
                        JunPercentage = 0,//ab
                        //JunResult = $@"=AA{count}+(AB{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JunCost}",//ac

                        //JulActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().JulCost,//ad
                        JulPercentage = 0,//ae
                        //JulResult = $@"=AD{count}+(AE{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().JulCost}",//af

                        //AugActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().AugCost,//ag
                        AugPercentage = 0,//ah
                        //AugResult = $@"=AG{count}+(AH{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().AugCost}",//ai

                        //SepActualCost = sukeyList.Where(s => s.DepartmentId == item).FirstOrDefault().SepCost,//aj
                        SepPercentage = 0,//ak
                        //SepResult = $@"=AJ{count}+(AK{count}/100)*{sukeyList.Where(s => s.DepartmentId == "8").FirstOrDefault().SepCost}",//al
                    });
                    count++;
                }
            }


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
            List<ForecastAssignmentViewModel> forecsatEmployeeAssignmentViewModels = employeeAssignmentBLL.GetApprovalEmployeesBySearchFilter(employeeAssignment);
            List<ForecastAssignmentViewModel> _forecsatEmployeeAssignmentViewModels = new List<ForecastAssignmentViewModel>();

            return Ok(forecsatEmployeeAssignmentViewModels);
        }

        [HttpGet]
        [Route("api/utilities/UpdateApprovedData/")]
        public IHttpActionResult UpdateApprovedData(string assignmentYear,string historyName)
        {
            int results = 0;
            //approve history: start
            var session = System.Web.HttpContext.Current.Session;
            string createdBy = session["userName"].ToString();
            DateTime createdDate = DateTime.Now;


            List<AssignmentHistory> _assignmentHistories_Add = new List<AssignmentHistory>();
            _assignmentHistories_Add = forecastBLL.GetAddEmployeeApprovedData(Convert.ToInt32(assignmentYear));

            List<AssignmentHistory> _assignmentHistorys_Delete = new List<AssignmentHistory>();
            _assignmentHistorys_Delete = forecastBLL.GetDeleteEmployeeApprovedData(Convert.ToInt32(assignmentYear));

            List<AssignmentHistory> _assignmentHistorys_CellWise = new List<AssignmentHistory>();
            _assignmentHistorys_CellWise = forecastBLL.GetCellWiseEmployeeApprovedData(Convert.ToInt32(assignmentYear));

            if(_assignmentHistories_Add.Count>0 || _assignmentHistorys_Delete.Count > 0 || _assignmentHistorys_CellWise.Count > 0)
            {
                int approveTimeStamp = forecastBLL.CreateApproveTimeStamp(historyName, Convert.ToInt32(assignmentYear), createdBy, createdDate);
                if(approveTimeStamp> 0)
                {
                    int approveResults = forecastBLL.CreateApprovetHistory(approveTimeStamp, Convert.ToInt32(assignmentYear), createdBy,_assignmentHistories_Add,_assignmentHistorys_Delete,_assignmentHistorys_CellWise);
                }
            }                            
            //approve history: end
            
            int results2 = employeeAssignmentBLL.UpdateApprovedData(assignmentYear);
            int results3 = employeeAssignmentBLL.UpdateApprovedDataForDeleteRows(assignmentYear);
            int results4 = employeeAssignmentBLL.UpdateCellWiseApprovdData(assignmentYear);

            if (results2 > 0 || results3 > 0 || results4 > 0)
            {
                //int results5 = employeeAssignmentBLL.UpdateUnapprovedData(Convert.ToInt32(assignmentYear));
                results = 1;
            }
            else
            {
                results = 0;
            }
            return Ok(results);
        }
    }
}
