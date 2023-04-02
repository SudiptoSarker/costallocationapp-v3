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

            if (!String.IsNullOrEmpty(timeStampId))
            {
                List<Forecast> forecastHistories = forecastBLL.GetForecastHistories(Convert.ToInt32(timeStampId));
                if (forecastHistories.Count > 0)
                {
                    foreach (var item in forecastHistories)
                    {
                        var x = forecsatEmployeeAssignmentViewModels.Where(a => a.Id == item.EmployeeAssignmentId).FirstOrDefault();
                        if (item.Month==10)
                        {
                            x.OctPoints = item.Points.ToString();
                        }
                        if (item.Month == 11)
                        {
                            x.NovPoints = item.Points.ToString();
                        }
                        if (item.Month == 12)
                        {
                            x.DecPoints = item.Points.ToString();
                        }
                        if (item.Month == 1)
                        {
                            x.JanPoints = item.Points.ToString();
                        }
                        if (item.Month == 2)
                        {
                            x.FebPoints = item.Points.ToString();
                        }
                        if (item.Month == 3)
                        {
                            x.MarPoints = item.Points.ToString();
                        }
                        if (item.Month == 4)
                        {
                            x.AprPoints = item.Points.ToString();
                        }
                        if (item.Month == 5)
                        {
                            x.MayPoints = item.Points.ToString();
                        }
                        if (item.Month == 6)
                        {
                            x.JunPoints = item.Points.ToString();
                        }
                        if (item.Month == 7)
                        {
                            x.JulPoints = item.Points.ToString();
                        }
                        if (item.Month == 8)
                        {
                            x.AugPoints = item.Points.ToString();
                        }
                        if (item.Month == 9)
                        {
                            x.SepPoints = item.Points.ToString();
                        }
                    }
                }
            }

            //List<ForecastAssignmentViewModel1> datas = new List<ForecastAssignmentViewModel1>();
            //foreach (var item in forecsatEmployeeAssignmentViewModels)
            //{
            //    ForecastAssignmentViewModel1 d = new ForecastAssignmentViewModel1();
            //    d.Id = item.Id;
            //    d.EmployeeName = item.EmployeeName;
            //    d.SectionId = item.SectionId;
            //    d.DepartmentId = item.DepartmentId;
            //    d.InchargeId = item.InchargeId;
            //    d.RoleId = item.RoleId;
            //    d.ExplanationId = item.ExplanationId;
            //    d.CompanyId = item.CompanyId;
            //    d.GradeId = item.GradeId;
            //    d.UnitPrice = item.UnitPrice;

            //    d.OctPoints = item.OctPoints;
            //    d.NovPoints = item.NovPoints;
            //    d.DecPoints = item.DecPoints;
            //    d.JanPoints = item.JanPoints;
            //    d.FebPoints = item.FebPoints;
            //    d.MarPoints = item.MarPoints;
            //    d.AprPoints = item.AprPoints;
            //    d.MayPoints = item.MayPoints;
            //    d.JunPoints = item.JunPoints;
            //    d.JulPoints = item.JulPoints;
            //    d.AugPoints = item.AugPoints;
            //    d.SepPoints = item.SepPoints;

            //    d.OctTotal = item.OctTotal;
            //    d.NovTotal = item.NovTotal;
            //    d.DecTotal = item.DecTotal;
            //    d.JanTotal = item.JanTotal;
            //    d.FebTotal = item.FebTotal;
            //    d.MarTotal = item.MarTotal;
            //    d.AprTotal = item.AprTotal;
            //    d.MayTotal = item.MayTotal;
            //    d.JunTotal = item.JunTotal;
            //    d.JulTotal = item.JulTotal;
            //    d.AugTotal = item.AugTotal;
            //    d.SepTotal = item.SepTotal;

            //    datas.Add(d);
            //}

            


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
        public IHttpActionResult CreateForecastHistory(List<ForecastUpdateHistoryDto> historyDtos)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string message = "Something went wrong!!!";
            if (historyDtos != null)
            {
                if (historyDtos.Count > 0)
                {
                    foreach (var item in historyDtos)
                    {
                        EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                        employeeAssignment.Id = item.AssignmentId;
                        employeeAssignment.Remarks = "";
                        employeeAssignment.UpdatedBy = "";
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
                    forecastHisory.TimeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    forecastHisory.Year = historyDtos[0].Year;
                    forecastHisory.Forecasts = forecasts;
                    forecastHisory.CreatedBy = "";
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

        public Forecast ExtraxctToForecast(int assignmentId,int year, int monthId,decimal point)
        {
            Forecast forecast = new Forecast();
            forecast.EmployeeAssignmentId = assignmentId;
            forecast.CreatedBy = "";
            forecast.CreatedDate = DateTime.Now;
            forecast.Year = year;
            forecast.Month = monthId;
            forecast.Points = point;

            return forecast;
        }


        [HttpPost]
        [Route("api/utilities/CreateEmployee/")]
        public IHttpActionResult CreateNewEmployee(Employee employee)
        {
            if (!String.IsNullOrEmpty(employee.FullName))
            {
                employee.IsActive = true;
                employee.CreatedBy = "";
                employee.CreatedDate = DateTime.Now;
                int result = employeeBLL.CreateEmployee(employee);
                if (result>0)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            else
            {
                return BadRequest("Invalid Employee Name");
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

        [HttpPut]
        [Route("api/utilities/UpdateEmployee/")]
        public IHttpActionResult UpdateEmployee(Employee employee)
        {
            if(!String.IsNullOrEmpty(employee.FullName))
            {
                employee.UpdatedBy = "";
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
            employee.IsActive = false;
            employee.UpdatedBy = "";
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
        public IHttpActionResult CreateAssignment_Excel(List<ExcelAssignmentDto> excelAssignmentDtos)
        {
            if (excelAssignmentDtos.Count>0)
            {
                foreach (var item in excelAssignmentDtos)
                {
                    EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                    if (item.EmployeeId=="" || item.EmployeeId==null)
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
                    employeeAssignment.Year = item.Year;
                    employeeAssignment.IsActive = true.ToString();
                    employeeAssignment.SubCode = 1;
                    employeeAssignment.CreatedBy = "";
                    employeeAssignment.CreatedDate = DateTime.Now;
                    employeeAssignment.Remarks = "";


                    int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
                    if (result == 1)
                    {
                        int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                        List<Forecast> forecasts = new List<Forecast>();
                        
                        forecasts.Add(new Forecast { EmployeeAssignmentId=employeeAssignmentLastId ,Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "" });
                        foreach (var forecastItem in forecasts)
                        {
                            int resultSave = forecastBLL.CreateForecast(forecastItem);
                        }
                    }                  
                }

            }           

            return Ok();
        }


        [HttpDelete]
        [Route("api/utilities/ExcelDeleteAssignment/")]
        public IHttpActionResult DeleteAssignment_Excel(int[] ids)
        {
            if (ids.Length > 0)
            {
                foreach (var item in ids)
                {
                    employeeAssignmentBLL.DeleteAssignment_Excel(item);
                }
            }

            return Ok("Operation Completed!");
        }



    }
}
