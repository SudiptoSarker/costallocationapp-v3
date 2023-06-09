﻿using System;
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
            //if (!String.IsNullOrEmpty(timeStampId))
            //{
            //    List<Forecast> forecastHistories = forecastBLL.GetForecastHistories(Convert.ToInt32(timeStampId));
            //    //List<int> forecastAssignmentIds = new List<int>();
            //    if (forecastHistories.Count > 0)
            //    {
            //        //forecastAssignmentIds = forecastHistories.Select(f => f.EmployeeAssignmentId).Distinct().ToList();
            //        ForecastAssignmentViewModel _x = null;
            //        int previousId = 0;
            //        foreach (var item in forecastHistories)
            //        {
                        
            //            if (previousId != item.EmployeeAssignmentId)
            //            {
            //                var x = forecsatEmployeeAssignmentViewModels.Where(a => a.Id == item.EmployeeAssignmentId).SingleOrDefault();
            //                previousId = item.EmployeeAssignmentId;
            //                _x = new ForecastAssignmentViewModel();

            //                _x.Id = x.Id;
            //                _x.EmployeeId = x.EmployeeId;
            //                _x.EmployeeName = x.EmployeeName+" (updated)";
            //                _x.CompanyId = x.CompanyId;
            //                _x.DepartmentId = x.DepartmentId;
            //                _x.SectionId = x.SectionId;
            //                _x.RoleId = x.RoleId;
            //                _x.InchargeId = x.InchargeId;
            //                _x.GradeId = x.GradeId;
            //                _x.UnitPrice = x.UnitPrice;

            //                _x.OctPoints = x.OctPoints;
            //                _x.NovPoints = x.NovPoints;
            //                _x.DecPoints = x.DecPoints;
            //                _x.JanPoints = x.JanPoints;
            //                _x.FebPoints = x.FebPoints;
            //                _x.MarPoints = x.MarPoints;
            //                _x.AprPoints = x.AprPoints;
            //                _x.MayPoints = x.MayPoints;
            //                _x.JunPoints = x.JunPoints;
            //                _x.JulPoints = x.JulPoints;
            //                _x.AugPoints = x.AugPoints;
            //                _x.SepPoints = x.SepPoints;

            //                forecsatEmployeeAssignmentViewModels.Add(_x);
            //            }
                       
            //            if (item.Month==10)
            //            {
            //                _x.OctPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 11)
            //            {
            //                _x.NovPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 12)
            //            {
            //                _x.DecPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 1)
            //            {
            //                _x.JanPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 2)
            //            {
            //                _x.FebPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 3)
            //            {
            //                _x.MarPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 4)
            //            {
            //                _x.AprPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 5)
            //            {
            //                _x.MayPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 6)
            //            {
            //                _x.JunPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 7)
            //            {
            //                _x.JulPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 8)
            //            {
            //                _x.AugPoints = item.Points.ToString();
            //            }
            //            if (item.Month == 9)
            //            {
            //                _x.SepPoints = item.Points.ToString();
            //            }
                        
            //        }

            //        //if (forecastAssignmentIds.Count > 0)
            //        //{
            //        //    foreach (var item in forecastAssignmentIds)
            //        //    {
            //        //        _forecsatEmployeeAssignmentViewModels.Add(forecsatEmployeeAssignmentViewModels.Where(f=>f.Id==item).SingleOrDefault());
            //        //    }
            //        //}
            //        var removableList = forecsatEmployeeAssignmentViewModels.Where(f => f.EmployeeId == 0).ToList();
            //        foreach (var item in removableList)
            //        {
            //            forecsatEmployeeAssignmentViewModels.Remove(item);
            //        }

            //        //forecsatEmployeeAssignmentViewModels.Where(f => f.EmployeeName.ToLower() == "total").SingleOrDefault().EmployeeId = maxEmployeeId + 2;

            //        forecsatEmployeeAssignmentViewModels = (from x in forecsatEmployeeAssignmentViewModels
            //                                                orderby x.EmployeeId ascending
            //                                                select x).ToList();
            //    }
            //}

            //if (_forecsatEmployeeAssignmentViewModels.Count>0)
            //{
            //    return Ok(_forecsatEmployeeAssignmentViewModels);
            //}
            //else
            //{
            //    return Ok(forecsatEmployeeAssignmentViewModels);
            //}

            //var maxEmployeeId = forecsatEmployeeAssignmentViewModels.Max(f => f.EmployeeId);
            

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

                        int updateResult = employeeAssignmentBLL.UpdateAssignment(employeeAssignment);

                        forecastsPrevious.AddRange(forecastBLL.GetForecastsByAssignmentId(item.AssignmentId));



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

                    //forecastHisory.UpdatedDate = DateTime.Now;
                    //forecastHisory.UpdatedBy = "";
                    //int resultEdit = forecastBLL.UpdateForecast(forecastHisory.Forecasts);
                    //int resultEdit = 0;
                    //foreach (var item in forecastHisory.Forecasts)
                    //{
                    //    var result = forecastBLL.CheckAssignmentId(item.EmployeeAssignmentId, item.Year,item.Month);
                    //    if (result == true)
                    //    {
                    //        resultEdit = forecastBLL.UpdateForecast(item);
                    //    }
                    //    else
                    //    {
                    //        int resultSave = forecastBLL.CreateForecast(item);
                    //    }                        
                    //}
                    var resultTimeStamp = forecastBLL.CreateTimeStamp(forecastHisory);

                    if (resultTimeStamp > 0)
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
                else
                {
                    user.IsActive = true;
                    user.CreatedBy = session["userName"].ToString();
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
        [Route("api/utilities/GetUserList/")]
        public IHttpActionResult GetUserList()
        {
            var session = System.Web.HttpContext.Current.Session;
            var user = userBLL.GetUserByUserName(session["userName"].ToString());
            List<User> filteredUsers = new List<User>();
            List<User> users = userBLL.GetAllUsers();
            if (user.UserRoleId==1)
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
                        if (item.UserName==user.UserName)
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
                    var session = System.Web.HttpContext.Current.Session;

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
                    employeeAssignment.CreatedBy = session["userName"].ToString();
                    employeeAssignment.CreatedDate = DateTime.Now;
                    employeeAssignment.Remarks = item.Remarks;


                    int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
                    if (result == 1)
                    {
                        int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                        List<Forecast> forecasts = new List<Forecast>();
                        
                        forecasts.Add(new Forecast { EmployeeAssignmentId=employeeAssignmentLastId ,Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "",Year= Convert.ToInt32(item.Year) });
                        foreach (var forecastItem in forecasts)
                        {
                            int resultSave = forecastBLL.CreateForecast(forecastItem);
                        }
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
                    employeeAssignmentBLL.DeleteAssignment_Excel(item);
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

        [HttpPost]
        [Route("api/utilities/CreateActualCost/")]
        public IHttpActionResult CreateActualCost(ActualCostDto actualCostDto)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (actualCostDto.ActualCosts.Count>0)
            {
                foreach (var item in actualCostDto.ActualCosts)
                {
                    if (item.AssignmentId==0)
                    {
                        continue;
                    }
                    item.Year = actualCostDto.Year;
                    var flag = actualCostBLL.CheckAssignmentId(item.AssignmentId, actualCostDto.Year);
                    if (flag)
                    {
                        item.UpdatedBy = session["userName"].ToString();
                        item.UpdatedDate = DateTime.Now;
                        actualCostBLL.UpdateActualCost(item);
                    }
                    else
                    {
                        item.CreatedBy = session["userName"].ToString();
                        item.CreatedDate = DateTime.Now;
                        actualCostBLL.CreateActualCost(item);
                    }
                }
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

    }
}
