using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Dtos;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;

namespace CostAllocationApp.BLL
{
    public class EmployeeAssignmentBLL
    {
        EmployeeAssignmentDAL employeeAssignmentDAL = null;
        ExplanationsBLL explanationsBLL = null;
        ActualCostDAL actualCostDAL = null;
        TotalDAL totalDAL = null;
        public EmployeeAssignmentBLL()
        {
            employeeAssignmentDAL = new EmployeeAssignmentDAL();
            explanationsBLL = new ExplanationsBLL();
            actualCostDAL = new ActualCostDAL();
            totalDAL = new TotalDAL();
        }
        public int CreateAssignment(EmployeeAssignment employeeAssignment)
        {
            return employeeAssignmentDAL.CreateAssignment(employeeAssignment);
        }
        public int CreateFinalBudgetAssignment(EmployeeAssignment employeeAssignment)
        {
            return employeeAssignmentDAL.CreateFinalBudgetAssignment(employeeAssignment);
        }
        public int CreateBudgets(EmployeeBudget employeeAssignment)
        {
            return employeeAssignmentDAL.CreateBudgets(employeeAssignment);
        }
        public int UpdateAssignment(EmployeeAssignment employeeAssignment)
        {
            return employeeAssignmentDAL.UpdateAssignment(employeeAssignment);
        }
        public int UpdateBudgetAssignment(EmployeeAssignment employeeAssignment)
        {
            return employeeAssignmentDAL.UpdateBudgetAssignment(employeeAssignment);
        }
        public List<EmployeeAssignmentViewModel> SearchAssignment(EmployeeAssignment employeeAssignment)
        {
            var employees = employeeAssignmentDAL.SearchAssignment(employeeAssignment);
            if (employees.Count > 0)
            {
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                }
                if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    employees = employees.Where(emp => emp.ExplanationId == employeeAssignment.ExplanationId && emp.ExplanationId != "0").ToList();
                }
            }
            return employees;
            //return employeeAssignmentDAL.SearchAssignment(employeeAssignment);
        }

        public EmployeeAssignmentViewModel GetAssignmentById(int assignmentId)
        {
            var assignment = employeeAssignmentDAL.GetAssignmentById(assignmentId);
            if (string.IsNullOrEmpty(assignment.ExplanationId))
            {
                assignment.ExplanationId = "0";
                assignment.ExplanationName = "n/a";
            }
            else
            {
                assignment.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(assignment.ExplanationId)).ExplanationName;
            }
            return assignment;
        }

        public List<EmployeeAssignmentViewModel> GetEmployeesBySearchFilter(EmployeeAssignment employeeAssignment)
        {
            var employees = employeeAssignmentDAL.GetEmployeesBySearchFilter(employeeAssignment);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                {
                    employees = employees.Where(emp => emp.ExplanationId == employeeAssignment.ExplanationId && emp.ExplanationId != "0").ToList();
                }

                //this.MarkedAsRed(employees);
                this.MarkedAsRedForAddName(employees);
            }

            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower();
            }
            return employees;
        }

        public List<ForecastAssignmentViewModel> GetEmployeesBudgetByDepartments_Company(int departmentId, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> budgetAssignments = employeeAssignmentDAL.GetEmployeesBudgetByDepartments_Company(departmentId, companyIds, year);

            if (budgetAssignments.Count > 0)
            {
                foreach (var budgetAssignment in budgetAssignments)
                {
                    budgetAssignment.forecasts = employeeAssignmentDAL.GetBudgetByAssignmentId(budgetAssignment.Id, year.ToString());
                    if (budgetAssignment.forecasts.Count > 0)
                    {
                        budgetAssignment.OctPoints = budgetAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        budgetAssignment.NovPoints = budgetAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        budgetAssignment.DecPoints = budgetAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        budgetAssignment.JanPoints = budgetAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        budgetAssignment.FebPoints = budgetAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        budgetAssignment.MarPoints = budgetAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        budgetAssignment.AprPoints = budgetAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        budgetAssignment.MayPoints = budgetAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        budgetAssignment.JunPoints = budgetAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        budgetAssignment.JulPoints = budgetAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        budgetAssignment.AugPoints = budgetAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        budgetAssignment.SepPoints = budgetAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();
                    }
                }
            }
            return budgetAssignments;
        }


        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(string departmentIds, string companyIds, int year,string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetEmployeesForecastByDepartments_Company(departmentIds, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(int departmentId, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetEmployeesForecastByDepartments_Company(departmentId, companyIds, year);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.Id);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetBudgetManmonthByIncharge(string inchargeIds, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = totalDAL.GetBudgetCostByCompanyAndInchargeId(inchargeIds, companyIds, year);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetBudgetManMonthByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.Id);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }


        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company_Timestamps(int departmentId, string companyIds, int year, string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetEmployeesForecastByDepartments_Company_Timestamps(departmentId, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastsByTimestampAssignmentId(forecastAssignment.AssignmentTimeStampId, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.Id);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByIncharge_Company(string inchargeIds, string companyIds, int year,string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetEmployeesForecastByIncharge_Company(inchargeIds, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByIncharge_Company(string inchargeIds, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetEmployeesForecastByIncharge_Company(inchargeIds, companyIds, year);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.Id);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        //public List<ForecastAssignmentViewModel> GetEmployeesForecastBySearchFilter(EmployeeAssignment employeeAssignment)
        public List<ForecastAssignmentViewModel> GetEmployeesForecastBySearchFilter(EmployeeAssignmentForecast employeeAssignment)
        {
            var employees = employeeAssignmentDAL.GetEmployeesForecastBySearchFilter(employeeAssignment);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                //if (!String.IsNullOrEmpty(employeeAssignment.ExplanationId))
                //{
                //    employees = employees.Where(emp => emp.ExplanationId == employeeAssignment.ExplanationId && emp.ExplanationId != "0").ToList();
                //}

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        item.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(item.Id, employeeAssignment.Year);
                    }
                }

            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            //if (employees.Count > 0)
            //{
            //    string previousEmployeeName = "";
            //    int count = 1;
            //    foreach (var item in employees)
            //    {
            //        if (previousEmployeeName!=item.EmployeeName)
            //        {
            //            previousEmployeeName = item.EmployeeName;
            //            count = 1;
            //            //item.EmployeeName = item.EmployeeName + " (" + count + ")";
            //            item.EmployeeName = item.EmployeeName;
            //            count++;

            //        }
            //        else
            //        {
            //            item.EmployeeName = item.EmployeeName + " (" + count + ")";
            //            count++;
            //        }
            //    }
            //}

            // head count...
            if (employees.Count > 0)
            {
                List<int> OctHeadCount = new List<int>();
                List<int> NovHeadCount = new List<int>();
                List<int> DecHeadCount = new List<int>();
                List<int> JanHeadCount = new List<int>();
                List<int> FebHeadCount = new List<int>();
                List<int> MarHeadCount = new List<int>();
                List<int> AprHeadCount = new List<int>();
                List<int> MayHeadCount = new List<int>();
                List<int> JunHeadCount = new List<int>();
                List<int> JulHeadCount = new List<int>();
                List<int> AugHeadCount = new List<int>();
                List<int> SepHeadCount = new List<int>();

                foreach (var item in employees.ToList())
                {
                    if (item.forecasts != null && item.forecasts.Count > 0)
                    {

                        if (item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points > 0)
                        {
                            if (!OctHeadCount.Contains(item.EmployeeId))
                            {
                                OctHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points > 0)
                        {
                            if (!NovHeadCount.Contains(item.EmployeeId))
                            {
                                NovHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points > 0)
                        {
                            if (!DecHeadCount.Contains(item.EmployeeId))
                            {
                                DecHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points > 0)
                        {
                            if (!JanHeadCount.Contains(item.EmployeeId))
                            {
                                JanHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points > 0)
                        {
                            if (!FebHeadCount.Contains(item.EmployeeId))
                            {
                                FebHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points > 0)
                        {
                            if (!MarHeadCount.Contains(item.EmployeeId))
                            {
                                MarHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points > 0)
                        {
                            if (!AprHeadCount.Contains(item.EmployeeId))
                            {
                                AprHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points > 0)
                        {
                            if (!MayHeadCount.Contains(item.EmployeeId))
                            {
                                MayHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points > 0)
                        {
                            if (!JunHeadCount.Contains(item.EmployeeId))
                            {
                                JunHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points > 0)
                        {
                            if (!JulHeadCount.Contains(item.EmployeeId))
                            {
                                JulHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points > 0)
                        {
                            if (!AugHeadCount.Contains(item.EmployeeId))
                            {
                                AugHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points > 0)
                        {
                            if (!SepHeadCount.Contains(item.EmployeeId))
                            {
                                SepHeadCount.Add(item.EmployeeId);
                            }

                        }
                    }
                }

                employees.Add(new ForecastAssignmentViewModel
                {
                    EmployeeName = "Head Count",
                    OctPoints = OctHeadCount.Count().ToString(),
                    NovPoints = NovHeadCount.Count().ToString(),
                    DecPoints = DecHeadCount.Count().ToString(),
                    JanPoints = JanHeadCount.Count().ToString(),
                    FebPoints = FebHeadCount.Count().ToString(),
                    MarPoints = MarHeadCount.Count().ToString(),
                    AprPoints = AprHeadCount.Count().ToString(),
                    MayPoints = MayHeadCount.Count().ToString(),
                    JunPoints = JunHeadCount.Count().ToString(),
                    JulPoints = JulHeadCount.Count().ToString(),
                    AugPoints = AugHeadCount.Count().ToString(),
                    SepPoints = SepHeadCount.Count().ToString(),
                });

            }

            // calculate total...
            if (employees.Count > 0)
            {

                var countedRow = employees.Count - 1;
                employees.Add(new ForecastAssignmentViewModel
                {
                    EmployeeName = "Total",

                    OctPoints = $@"=SUM(K1:K{countedRow})",
                    NovPoints = $@"=SUM(L1:L{countedRow})",
                    DecPoints = $@"=SUM(M1:M{countedRow})",
                    JanPoints = $@"=SUM(N1:N{countedRow})",
                    FebPoints = $@"=SUM(O1:O{countedRow})",
                    MarPoints = $@"=SUM(P1:P{countedRow})",
                    AprPoints = $@"=SUM(Q1:Q{countedRow})",
                    MayPoints = $@"=SUM(R1:R{countedRow})",
                    JunPoints = $@"=SUM(S1:S{countedRow})",
                    JulPoints = $@"=SUM(T1:T{countedRow})",
                    AugPoints = $@"=SUM(U1:U{countedRow})",
                    SepPoints = $@"=SUM(V1:V{countedRow})",
                });
            }



            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }
                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        if (innerCount == 1)
                        {
                            forecast.Total = $"=K{count}*L{count}";
                        }
                        if (innerCount == 2)
                        {
                            forecast.Total = $"=K{count}*M{count}";
                        }
                        if (innerCount == 3)
                        {
                            forecast.Total = $"=K{count}*N{count}";
                        }
                        if (innerCount == 4)
                        {
                            forecast.Total = $"=K{count}*O{count}";
                        }
                        if (innerCount == 5)
                        {
                            forecast.Total = $"=K{count}*P{count}";
                        }
                        if (innerCount == 6)
                        {
                            forecast.Total = $"=K{count}*Q{count}";
                        }
                        if (innerCount == 7)
                        {
                            forecast.Total = $"=K{count}*R{count}";
                        }
                        if (innerCount == 8)
                        {
                            forecast.Total = $"=K{count}*S{count}";
                        }
                        if (innerCount == 9)
                        {
                            forecast.Total = $"=K{count}*T{count}";
                        }
                        if (innerCount == 10)
                        {
                            forecast.Total = $"=K{count}*U{count}";
                        }
                        if (innerCount == 11)
                        {
                            forecast.Total = $"=K{count}*V{count}";
                        }
                        if (innerCount == 12)
                        {
                            forecast.Total = $"=K{count}*W{count}";
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                        item.forecasts = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";

                        item.forecasts = null;
                    }


                }

            }


            return employees;
        }

        public int RemoveAssignment(int rowId)
        {
            return employeeAssignmentDAL.RemoveAssignment(rowId);
        }

        public List<EmployeeAssignmentViewModel> GetEmployeesByName(string employeeName)
        {
            var employees = employeeAssignmentDAL.GetEmployeesByName(employeeName);
            if (employees.Count > 0)
            {
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                }
            }
            return employees;
        }

        public List<EmployeeAssignmentViewModel> GetEmployeesBySearchFilterForMultipleSearch(EmployeeAssignmentDTO employeeAssignment)
        {
            List<EmployeeAssignmentViewModel> employeesWithIn = new List<EmployeeAssignmentViewModel>();
            var employees = employeeAssignmentDAL.GetEmployeesBySearchFilterForMultipleSearch(employeeAssignment);
            if (employees.Count > 0)
            {
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                }
                if (employeeAssignment.Explanations != null)
                {
                    if (employeeAssignment.Explanations.Length > 0)
                    {
                        foreach (var item in employeeAssignment.Explanations)
                        {
                            var employeesInTemp = employees.Where(emp => emp.ExplanationId.Contains(item) && emp.ExplanationId != "0").ToList();
                            employeesWithIn.AddRange(employeesInTemp);
                        }
                        employees = employeesWithIn;
                    }
                }
                this.MarkedAsRed(employees);
            }

            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower();
            }
            return employees;
        }

        public List<EmployeeAssignmentViewModel> MarkedAsRed(List<EmployeeAssignmentViewModel> employees)
        {
            List<EmployeeAssignmentViewModel> viewModels = new List<EmployeeAssignmentViewModel>();
            List<string> names = new List<string>();

            names = (from x in employees
                     select x.EmployeeName).ToList();
            names = names.Select(n => n).Distinct().ToList();

            foreach (var name in names)
            {
                viewModels = employees.Where(emp => emp.EmployeeName == name).ToList();
                if (viewModels.Count > 1)
                {
                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode;
                            }

                        }
                    }



                    EmployeeAssignmentViewModel employeeAssignmentViewModelFirst = viewModels.Where(vm => vm.SubCode == 1).FirstOrDefault();
                    if (employeeAssignmentViewModelFirst == null)
                    {
                        continue;
                    }
                    viewModels.Remove(employeeAssignmentViewModelFirst);
                    foreach (var filteredAssignment in viewModels)
                    {
                        if (!string.IsNullOrEmpty(employeeAssignmentViewModelFirst.UnitPrice))
                        {
                            if (filteredAssignment.UnitPrice != employeeAssignmentViewModelFirst.UnitPrice)
                            {
                                employees.Where(emp => emp.Id == filteredAssignment.Id).FirstOrDefault().MarkedAsRed = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName;
                            }

                        }
                    }
                }
            }


            return employees;
        }

        public List<ForecastAssignmentViewModel> MarkedAsRedForForecast(List<ForecastAssignmentViewModel> employees)
        {
            List<ForecastAssignmentViewModel> viewModels = new List<ForecastAssignmentViewModel>();
            List<string> names = new List<string>();

            names = (from x in employees
                     select x.EmployeeName).ToList();
            names = names.Select(n => n).Distinct().ToList();

            foreach (var name in names)
            {
                viewModels = employees.Where(emp => emp.EmployeeName == name).ToList();
                if (viewModels.Count > 1)
                {

                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode;
                            }

                        }
                    }

                    ForecastAssignmentViewModel forecastEmployeeAssignmentViewModelFirst = viewModels.Where(vm => vm.SubCode == 1).FirstOrDefault();
                    if (forecastEmployeeAssignmentViewModelFirst == null)
                    {
                        continue;
                    }
                    viewModels.Remove(forecastEmployeeAssignmentViewModelFirst);
                    foreach (var filteredAssignment in viewModels)
                    {
                        if (filteredAssignment.UnitPrice != forecastEmployeeAssignmentViewModelFirst.UnitPrice)
                        {
                            employees.Where(emp => emp.Id == filteredAssignment.Id).FirstOrDefault().MarkedAsRed = true;

                        }
                    }
                }
                else
                {
                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName;
                            }

                        }
                    }
                }
            }


            return employees;
        }

        public bool CheckEmployeeName(string employeeName)
        {
            return employeeAssignmentDAL.CheckEmployeeName(employeeName);
        }

        public List<EmployeeAssignmentViewModel> MarkedAsRedForAddName(List<EmployeeAssignmentViewModel> employees)
        {
            List<EmployeeAssignmentViewModel> viewModels = new List<EmployeeAssignmentViewModel>();
            List<string> names = new List<string>();

            names = (from x in employees
                     select x.EmployeeName).ToList();
            names = names.Select(n => n).Distinct().ToList();

            foreach (var name in names)
            {
                viewModels = employees.Where(emp => emp.EmployeeName == name).ToList();
                if (viewModels.Count > 1)
                {
                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " " + singleEmployee.SubCode;
                            }

                        }
                    }



                    EmployeeAssignmentViewModel employeeAssignmentViewModelFirst = viewModels.Where(vm => vm.SubCode == 1).FirstOrDefault();
                    if (employeeAssignmentViewModelFirst == null)
                    {
                        continue;
                    }
                    viewModels.Remove(employeeAssignmentViewModelFirst);
                    foreach (var filteredAssignment in viewModels)
                    {
                        if (!string.IsNullOrEmpty(employeeAssignmentViewModelFirst.UnitPrice))
                        {
                            if (filteredAssignment.UnitPrice != employeeAssignmentViewModelFirst.UnitPrice)
                            {
                                employees.Where(emp => emp.Id == filteredAssignment.Id).FirstOrDefault().MarkedAsRed = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in viewModels)
                    {
                        var singleEmployee = employees.Where(emp => emp.Id == item.Id).FirstOrDefault();
                        if (singleEmployee != null)
                        {
                            //singleEmployee.SubCode = -1;
                            if (!String.IsNullOrEmpty(singleEmployee.Remarks))
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName + "$" + singleEmployee.EmployeeName;
                            }

                            singleEmployee.AddNameSubCode = "";
                        }
                    }
                }
            }


            return employees;
        }

        public int GetLastId()
        {
            return employeeAssignmentDAL.GetLastId();
        }
        public int GetFinalBudgetLastId()
        {
            return employeeAssignmentDAL.GetFinalBudgetLastId();
        }
        public int GetBudgetLastId()
        {
            return employeeAssignmentDAL.GetBudgetLastId();
        }
        public void DeleteAssignment_Excel(int assignmentId)
        {
            employeeAssignmentDAL.DeleteAssignment_Excel(assignmentId);
        }
        public List<EmployeeAssignmentViewModel> GetAssignmentsByYear(int year)
        {
            return employeeAssignmentDAL.GetAssignmentsByYear(year);
        }
        public string GetBCYRCellByAssignmentId(int assignmentId)
        {
            return employeeAssignmentDAL.GetBCYRCellByAssignmentId(assignmentId);
        }
        public EmployeeAssignment GetBCYRCellAndPendingCellsByAssignmentId(int assignmentId)
        {
            return employeeAssignmentDAL.GetBCYRCellAndPendingCellsByAssignmentId(assignmentId);
        }
        public int UpdateBCYRCellByAssignmentId(int assignmentId, string cell)
        {
            return employeeAssignmentDAL.UpdateBCYRCellByAssignmentId(assignmentId, cell);
        }
        public int UpdateBCYRCellBCYRPendingCellByAssignmentId(int assignmentId, string cell, string pendingCells)
        {
            return employeeAssignmentDAL.UpdateBCYRCellBCYRPendingCellByAssignmentId(assignmentId, cell, pendingCells);
        }
        public int ApproveAssignement(string approvedAssignementId)
        {
            return employeeAssignmentDAL.ApproveAssignement(approvedAssignementId);
        }
        public int UnApproveAssignement(string approvedAssignementId)
        {
            return employeeAssignmentDAL.UnApproveAssignement(approvedAssignementId);
        }
        public int ApproveDeletedRow(string approvedAssignementId)
        {
            return employeeAssignmentDAL.ApproveDeletedRow(approvedAssignementId);
        }

        //un-approve delete data
        public int UnApproveDeletedRow(string approvedAssignementId)
        {
            return employeeAssignmentDAL.UnApproveDeletedRow(approvedAssignementId);
        }

        //get year wise all data for approval employee
        public List<ForecastAssignmentViewModel> GetApprovalEmployeesBySearchFilter(EmployeeAssignmentForecast employeeAssignment)
        {
            var employees = employeeAssignmentDAL.GetApprovalEmployeesBySearchFilter(employeeAssignment);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        item.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(item.Id, employeeAssignment.Year);
                    }
                }

            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            // head count...
            if (employees.Count > 0)
            {
                List<int> OctHeadCount = new List<int>();
                List<int> NovHeadCount = new List<int>();
                List<int> DecHeadCount = new List<int>();
                List<int> JanHeadCount = new List<int>();
                List<int> FebHeadCount = new List<int>();
                List<int> MarHeadCount = new List<int>();
                List<int> AprHeadCount = new List<int>();
                List<int> MayHeadCount = new List<int>();
                List<int> JunHeadCount = new List<int>();
                List<int> JulHeadCount = new List<int>();
                List<int> AugHeadCount = new List<int>();
                List<int> SepHeadCount = new List<int>();

                foreach (var item in employees.ToList())
                {
                    if (item.forecasts != null && item.forecasts.Count > 0)
                    {

                        if (item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points > 0)
                        {
                            if (!OctHeadCount.Contains(item.EmployeeId))
                            {
                                OctHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points > 0)
                        {
                            if (!NovHeadCount.Contains(item.EmployeeId))
                            {
                                NovHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points > 0)
                        {
                            if (!DecHeadCount.Contains(item.EmployeeId))
                            {
                                DecHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points > 0)
                        {
                            if (!JanHeadCount.Contains(item.EmployeeId))
                            {
                                JanHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points > 0)
                        {
                            if (!FebHeadCount.Contains(item.EmployeeId))
                            {
                                FebHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points > 0)
                        {
                            if (!MarHeadCount.Contains(item.EmployeeId))
                            {
                                MarHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points > 0)
                        {
                            if (!AprHeadCount.Contains(item.EmployeeId))
                            {
                                AprHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points > 0)
                        {
                            if (!MayHeadCount.Contains(item.EmployeeId))
                            {
                                MayHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points > 0)
                        {
                            if (!JunHeadCount.Contains(item.EmployeeId))
                            {
                                JunHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points > 0)
                        {
                            if (!JulHeadCount.Contains(item.EmployeeId))
                            {
                                JulHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points > 0)
                        {
                            if (!AugHeadCount.Contains(item.EmployeeId))
                            {
                                AugHeadCount.Add(item.EmployeeId);
                            }

                        }
                        if (item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points > 0)
                        {
                            if (!SepHeadCount.Contains(item.EmployeeId))
                            {
                                SepHeadCount.Add(item.EmployeeId);
                            }

                        }
                    }
                }
            }
            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }
                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        if (innerCount == 1)
                        {
                            forecast.Total = $"=K{count}*L{count}";
                        }
                        if (innerCount == 2)
                        {
                            forecast.Total = $"=K{count}*M{count}";
                        }
                        if (innerCount == 3)
                        {
                            forecast.Total = $"=K{count}*N{count}";
                        }
                        if (innerCount == 4)
                        {
                            forecast.Total = $"=K{count}*O{count}";
                        }
                        if (innerCount == 5)
                        {
                            forecast.Total = $"=K{count}*P{count}";
                        }
                        if (innerCount == 6)
                        {
                            forecast.Total = $"=K{count}*Q{count}";
                        }
                        if (innerCount == 7)
                        {
                            forecast.Total = $"=K{count}*R{count}";
                        }
                        if (innerCount == 8)
                        {
                            forecast.Total = $"=K{count}*S{count}";
                        }
                        if (innerCount == 9)
                        {
                            forecast.Total = $"=K{count}*T{count}";
                        }
                        if (innerCount == 10)
                        {
                            forecast.Total = $"=K{count}*U{count}";
                        }
                        if (innerCount == 11)
                        {
                            forecast.Total = $"=K{count}*V{count}";
                        }
                        if (innerCount == 12)
                        {
                            forecast.Total = $"=K{count}*W{count}";
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                        item.forecasts = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";

                        item.forecasts = null;
                    }
                }
            }
            return employees;
        }
        public int UpdateApprovedData(string assignmentYear)
        {
            return employeeAssignmentDAL.UpdateApprovedData(assignmentYear);
        }
        public int UpdateApprovedDataForDeleteRows(string assignmentYear)
        {
            return employeeAssignmentDAL.UpdateApprovedDataForDeleteRows(assignmentYear);
        }
        public EmployeeAssignment GetPreviousApprovedCells(string assignementId)
        {
            return employeeAssignmentDAL.GetPreviousApprovedCells(assignementId);
        }
        public int UpdateBYCRCells(string assignementId, string bCYRCellApproved, string storeBYCRCells)
        {
            return employeeAssignmentDAL.UpdateBYCRCells(assignementId, bCYRCellApproved, storeBYCRCells);
        }
        public int UpdateCellWiseApprovdData(string assignmentYear)
        {
            return employeeAssignmentDAL.UpdateCellWiseApprovdData(assignmentYear);
        }
        public List<EmployeeAssignment> GetPendingCells(string assignmentYear)
        {
            return employeeAssignmentDAL.GetPendingCells(assignmentYear);
        }
        public int UpdatePendingCells(EmployeeAssignment employeeAssignments)
        {
            return employeeAssignmentDAL.UpdatePendingCells(employeeAssignments);
        }
        public int UpdateCellsByAssignmentid(string updatedApprovedCells, string updatePendingCells, int assignmentId)
        {
            return employeeAssignmentDAL.UpdateCellsByAssignmentid(updatedApprovedCells, updatePendingCells, assignmentId);
        }

        public List<EmployeeAssignment> GetPendingDeleteRows(string assignmentYear)
        {
            return employeeAssignmentDAL.GetPendingDeleteRows(assignmentYear);
        }
        public List<EmployeeAssignment> GetPendingAddEmployee(string assignmentYear)
        {
            return employeeAssignmentDAL.GetPendingAddEmployee(assignmentYear);
        }
        public int UpdatePendingDeleteRows(EmployeeAssignment employeeAssignments)
        {
            return employeeAssignmentDAL.UpdatePendingDeleteRows(employeeAssignments);
        }
        public int UpdatePendingAddEmployee(EmployeeAssignment employeeAssignments)
        {
            return employeeAssignmentDAL.UpdatePendingAddEmployee(employeeAssignments);
        }

        public List<EmployeeAssignmentViewModel> GetSpecificAssignmentDataData(int year, int monthId)
        {
            return employeeAssignmentDAL.GetSpecificAssignmentDataData(year, monthId);
        }
        public int UpdateUnapprovedData(int year)
        {
            return employeeAssignmentDAL.UpdateUnapprovedData(year);
        }
        public bool CheckForUnApprovedCells(string assignementId, string selectedCells)
        {
            return employeeAssignmentDAL.CheckForUnApprovedCells(assignementId, selectedCells);
        }
        public int CheckForApprovedCells(string assignementId, string selectedCells)
        {
            return employeeAssignmentDAL.CheckForApprovedCells(assignementId, selectedCells);
        }
        public bool CheckForUnApprovedRow(string assignementId, bool isDeletedRow)
        {
            return employeeAssignmentDAL.CheckForUnApprovedRow(assignementId, isDeletedRow);
        }
        public EmployeeAssignment GetEmployeeAssignmentForCheckApproval(string assignementId)
        {
            return employeeAssignmentDAL.GetEmployeeAssignmentForCheckApproval(assignementId);
        }
        public int UpdateApprovedRowByAssignmentId(int assignmentId)
        {
            return employeeAssignmentDAL.UpdateApprovedRowByAssignmentId(assignmentId);
        }
        public int UpdateDeletedRowByAssignmentId(int assignmentId)
        {
            return employeeAssignmentDAL.UpdateDeletedRowByAssignmentId(assignmentId);
        }
        public int UpdateUnapprovedPendingRows(int year)
        {
            return employeeAssignmentDAL.UpdateUnapprovedPendingRows(year);
        }
        public int UpdateUnapprovedPendingDeleteRows(int year)
        {
            return employeeAssignmentDAL.UpdateUnapprovedPendingDeleteRows(year);
        }

        //Get All the data from assignment table with forecast
        public List<ForecastAssignmentViewModel> GetAllOriginalDataForDownloadFiles(EmployeeAssignmentForecast employeeAssignment, int approvedTimestampid)
        {
            var employees = employeeAssignmentDAL.GetAllOriginalDataForDownloadFiles(employeeAssignment, approvedTimestampid);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        //item.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(item.Id, employeeAssignment.Year);
                        item.forecasts = employeeAssignmentDAL.GetApprovedForecastdData(item.Id, employeeAssignment.Year);
                    }
                }

            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }
                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        if (innerCount == 1)
                        {
                            forecast.Total = $"=K{count}*L{count}";
                        }
                        if (innerCount == 2)
                        {
                            forecast.Total = $"=K{count}*M{count}";
                        }
                        if (innerCount == 3)
                        {
                            forecast.Total = $"=K{count}*N{count}";
                        }
                        if (innerCount == 4)
                        {
                            forecast.Total = $"=K{count}*O{count}";
                        }
                        if (innerCount == 5)
                        {
                            forecast.Total = $"=K{count}*P{count}";
                        }
                        if (innerCount == 6)
                        {
                            forecast.Total = $"=K{count}*Q{count}";
                        }
                        if (innerCount == 7)
                        {
                            forecast.Total = $"=K{count}*R{count}";
                        }
                        if (innerCount == 8)
                        {
                            forecast.Total = $"=K{count}*S{count}";
                        }
                        if (innerCount == 9)
                        {
                            forecast.Total = $"=K{count}*T{count}";
                        }
                        if (innerCount == 10)
                        {
                            forecast.Total = $"=K{count}*U{count}";
                        }
                        if (innerCount == 11)
                        {
                            forecast.Total = $"=K{count}*V{count}";
                        }
                        if (innerCount == 12)
                        {
                            forecast.Total = $"=K{count}*W{count}";
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                        item.forecasts = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";

                        item.forecasts = null;
                    }


                }

            }


            return employees;
        }

        public List<ExcelAssignmentDto> GetAllOriginalDataForReplciateBudget(string year, int approvedTimestampid) {
            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();
            excelAssignmentDtos = employeeAssignmentDAL.GetAllOriginalDataForReplciateBudget(year, approvedTimestampid);
            return excelAssignmentDtos;
        }
        public List<Forecast> GetApprovedForecastdDataForReplicateBudget(int assignmentId, string year)
        {
            List<Forecast> forecasts = new List<Forecast>();
            forecasts = employeeAssignmentDAL.GetApprovedForecastdDataForReplicateBudget(assignmentId, year);
            return forecasts;
        }
        public bool IsApprovedCellsForDownloadExcel(string cellNumber, string approvedCells)
        {
            bool isApprovedCell = false;

            var arrApprovedCells = approvedCells.Split(',');
            foreach (var cellItem in arrApprovedCells)
            {
                if (cellItem == cellNumber)
                {
                    isApprovedCell = true;
                }
            }

            return isApprovedCell;
        }
        public List<ForecastDistributdViewModal> GetQCAssignemntsPercentage(int assignmentId)
        {
            return employeeAssignmentDAL.GetQCAssignemntsPercentage(assignmentId);
        }

        public List<ForecastDistributdViewModal> GetQCAssignemntsPercentageByEmployeeIdAndYear(int employeeId, int year)
        {
            return employeeAssignmentDAL.GetQCAssignemntsPercentageByEmployeeIdAndYear(employeeId, year);
        }


        public List<ForecastAssignmentViewModel> GetAllAssignmentData(EmployeeAssignmentForecast employeeAssignment)
        {
            var employees = employeeAssignmentDAL.GetAllAssignmentData(employeeAssignment);

            // pull data from actual cost table...
            List<ActualCost> actualCosts = actualCostDAL.GetActualCostsByYear(Convert.ToInt32(employeeAssignment.Year));

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        item.forecasts = employeeAssignmentDAL.GetForecastsByAssignmentId(item.Id, employeeAssignment.Year);
                    }
                }
            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }

                    ActualCost actualCost = actualCosts.Where(ac => ac.AssignmentId == item.Id).SingleOrDefault();

                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        string manmonthSum = "";
                        manmonthSum = $"=Q{count}+R{count}+S{count}+T{count}+U{count}+V{count}+W{count}+X{count}+Y{count}+Z{count}+AA{count}+AB{count}";
                        string costTotal = $"=AD{count}+AE{count}+AF{count}+AG{count}+AH{count}+AI{count}+AJ{count}+AK{count}+AL{count}+AM{count}+AN{count}+AO{count}";
                        if (innerCount == 1)
                        {
                            if (actualCost==null)
                            {
                                forecast.Total = $"=K{count}*Q{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.OctCost > 0)
                                {
                                    forecast.Total = actualCost.OctCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.OctPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*Q{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                           

                        }
                        if (innerCount == 2)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*R{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.NovCost > 0)
                                {
                                    forecast.Total = actualCost.NovCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.NovPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*R{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                         
                        }
                        if (innerCount == 3)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*S{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.DecCost > 0)
                                {
                                    forecast.Total = actualCost.DecCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.DecPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*S{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        if (innerCount == 4)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*T{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.JanCost > 0)
                                {
                                    forecast.Total = actualCost.JanCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.JanPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*T{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                           
                        }
                        if (innerCount == 5)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*U{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.FebCost > 0)
                                {
                                    forecast.Total = actualCost.FebCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.FebPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*U{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        if (innerCount == 6)
                        {
                            if (actualCost==null)
                            {
                                forecast.Total = $"=K{count}*V{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.MarCost > 0)
                                {
                                    forecast.Total = actualCost.MarCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.MarPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*V{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                           
                        }
                        if (innerCount == 7)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*V{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.AprCost > 0)
                                {
                                    forecast.Total = actualCost.AprCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.AprPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*V{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                        }
                        if (innerCount == 8)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*X{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.MayCost > 0)
                                {
                                    forecast.Total = actualCost.MayCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.MayPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*X{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        if (innerCount == 9)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*Y{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.JunCost > 0)
                                {
                                    forecast.Total = actualCost.JunCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.JunPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*Y{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        if (innerCount == 10)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*Z{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.JulCost > 0)
                                {
                                    forecast.Total = actualCost.JulCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.JulPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*Z{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                          
                        }
                        if (innerCount == 11)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*AA{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.AugCost > 0)
                                {
                                    forecast.Total = actualCost.AugCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.AugPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*AA{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        if (innerCount == 12)
                        {
                            if (actualCost == null)
                            {
                                forecast.Total = $"=K{count}*AB{count}";
                                forecast.TotalPoints = manmonthSum;
                                forecast.TotalCosts = costTotal;
                            }
                            else
                            {
                                if (actualCost.SepCost > 0)
                                {
                                    forecast.Total = actualCost.SepCost.ToString();
                                    //forecast.Points = Convert.ToDecimal(actualCost.SepPoint);
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                                else
                                {
                                    forecast.Total = $"=K{count}*AB{count}";
                                    forecast.TotalPoints = manmonthSum;
                                    forecast.TotalCosts = costTotal;
                                }
                            }
                            
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;
                        
                        item.TotalManMonth = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().TotalPoints;
                        item.TotalCost = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().TotalCosts;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().TotalPoints;
                        //item.TotalManMonth = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";
                        item.TotalManMonth = "";
                        item.TotalCost = "";

                        item.forecasts = null;
                    }
                }

            }


            return employees;
        }

        public List<ForecastAssignmentViewModel> GetAllBudgetData(EmployeeBudgetAssignment employeeAssignment)
        {
            var employees = employeeAssignmentDAL.GetAllBudgetData(employeeAssignment);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        item.forecasts = employeeAssignmentDAL.GetBudgetForecastById(item.Id, employeeAssignment.Year);
                    }
                }
            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }                    
                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        string manmonthSum = "";
                        manmonthSum = $"=Q{count}+R{count}+S{count}+T{count}+U{count}+V{count}+W{count}+X{count}+Y{count}+Z{count}+AA{count}+AB{count}";
                        string costTotal = $"=AD{count}+AE{count}+AF{count}+AG{count}+AH{count}+AI{count}+AJ{count}+AK{count}+AL{count}+AM{count}+AN{count}+AO{count}";
                        if (innerCount == 1)
                        {
                            //l-w
                            forecast.Total = $"=K{count}*Q{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;

                        }
                        if (innerCount == 2)
                        {
                            forecast.Total = $"=K{count}*R{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 3)
                        {
                            forecast.Total = $"=K{count}*S{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 4)
                        {
                            forecast.Total = $"=K{count}*T{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 5)
                        {
                            forecast.Total = $"=K{count}*U{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 6)
                        {
                            forecast.Total = $"=K{count}*V{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 7)
                        {
                            forecast.Total = $"=K{count}*W{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 8)
                        {
                            forecast.Total = $"=K{count}*X{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 9)
                        {
                            forecast.Total = $"=K{count}*Y{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 10)
                        {
                            forecast.Total = $"=K{count}*Z{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 11)
                        {
                            forecast.Total = $"=K{count}*AA{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        if (innerCount == 12)
                        {
                            forecast.Total = $"=K{count}*AB{count}";
                            forecast.TotalPoints = manmonthSum;
                            forecast.TotalCosts = costTotal;
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                        item.TotalManMonth = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().TotalPoints;
                        item.TotalCost = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().TotalCosts;

                        item.forecasts = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";
                        item.TotalManMonth = "";
                        item.TotalCost = "";

                        item.forecasts = null;
                    }
                }

            }


            return employees;
        }


        public int CreateApprovedAssignmentByTimestampId(EmployeeAssignment employeeAssignment, int approvedTimestampId)
        {
            return employeeAssignmentDAL.CreateApprovedAssignmentByTimestampId(employeeAssignment, approvedTimestampId);
        }
        public int GetApprovedAssignmentLastId()
        {
            return employeeAssignmentDAL.GetApprovedAssignmentLastId();
        }
        public int CheckForOriginalAssignmentIsExists(int assignmentId)
        {
            return employeeAssignmentDAL.CheckForOriginalAssignmentIsExists(assignmentId);
        }
        public int UpdateOriginalAssignment(AssignmentHistory _assignmentHistory, string columnValue, string columnName)
        {
            return employeeAssignmentDAL.UpdateOriginalAssignment(_assignmentHistory, columnValue, columnName);
        }
        public int InsertOriginalAssignment(AssignmentHistory _assignmentHistory, string columnValue, string columnName)
        {
            return employeeAssignmentDAL.InsertOriginalAssignment(_assignmentHistory, columnValue, columnName);
        }
        public int CheckForOriginalForecastDataIsExists(int assignmentId)
        {
            return employeeAssignmentDAL.CheckForOriginalForecastDataIsExists(assignmentId);
        }
        public int CheckMonthIdExistsForOrgForecast(int assignmentId, int monthId)
        {
            return employeeAssignmentDAL.CheckMonthIdExistsForOrgForecast(assignmentId, monthId);
        }

        public int RemoveApprovedDataFromOriginalTable(int assignmentId, int cellNo)
        {
            int results = 0;
            if (cellNo == 2)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "Remarks");
            }
            else if (cellNo == 3)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "SectionId");
            }
            else if (cellNo == 4)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "DepartmentId");
            }
            else if (cellNo == 5)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "InChargeId");
            }
            else if (cellNo == 6)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "RoleId");
            }
            else if (cellNo == 7)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "ExplanationId");
            }
            else if (cellNo == 8)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "CompanyId");
            }
            else if (cellNo == 9)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "GradeId");
            }
            else if (cellNo == 10)
            {
                results = employeeAssignmentDAL.RemoveAssignmentDataFromOrgTable(assignmentId, "UnitPrice");
            }
            else if (cellNo == 16)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 10);
            }
            else if (cellNo == 17)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 11);
            }
            else if (cellNo == 18)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 12);
            }
            else if (cellNo == 19)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 1);
            }
            else if (cellNo == 20)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 2);
            }
            else if (cellNo == 21)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 3);
            }
            else if (cellNo == 22)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 4);
            }
            else if (cellNo == 23)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 5);
            }
            else if (cellNo == 24)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 6);
            }
            else if (cellNo == 25)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 7);
            }
            else if (cellNo == 26)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 8);
            }
            else if (cellNo == 27)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 9);
            }
            return results;
        }
        public string GetOriginalDataForPendingCells(int assignmentId, string dbColumnNameWithDbSchema, string dbColumnName)
        {
            return employeeAssignmentDAL.GetOriginalDataForPendingCells(assignmentId, dbColumnNameWithDbSchema, dbColumnName);
        }
        public decimal GetMonthWiseOriginalForecastData(int assignmentId, string dbColumnName)
        {
            return employeeAssignmentDAL.GetMonthWiseOriginalForecastData(assignmentId, dbColumnName);
        }
        public bool CheckForValidOriginalData(string pendingCellNumbers, string cellNumber, string changedCells)
        {
            bool isValidRequest = true;
            var arrPendingCells = pendingCellNumbers.Split(',');
            foreach (var cellItem in arrPendingCells)
            {
                if (cellItem == cellNumber)
                {
                    isValidRequest = false;
                }
            }
            var arrChangedCels = changedCells.Split(',');
            foreach (var changedItem in arrChangedCels)
            {
                if (changedItem == cellNumber)
                {
                    isValidRequest = false;
                }
            }
            return isValidRequest;
        }
        public bool CheckForBudgetYearIsExists(int selected_year, int select_budget_type)
        {
            return employeeAssignmentDAL.CheckForBudgetYearIsExists(selected_year, select_budget_type);
        }
        public int FinalizeBudgetAssignment(int selected_year, int select_budget_type)
        {
            return employeeAssignmentDAL.FinalizeBudgetAssignment(selected_year, select_budget_type);
        }
        public List<EmployeeBudget> GetFinalizedBudgetData(int selected_year, int select_budget_type)
        {
            return employeeAssignmentDAL.GetFinalizedBudgetData(selected_year, select_budget_type);
        }
        public bool CheckYearIfFinalize(int year, int requestType)
        {
            return employeeAssignmentDAL.CheckYearIfFinalize(year, requestType);
        }
        public void DeleteAssignment_PreviousFinalizeData(int year)
        {
            employeeAssignmentDAL.DeleteAssignment_PreviousFinalizeData(year);
        }
        public void DeletePreviousFinalBudgetData(int year)
        {
            employeeAssignmentDAL.DeletePreviousFinalBudgetData(year);
        }
        public bool CheckIsValidYearForImport(int year)
        {
            return employeeAssignmentDAL.CheckIsValidYearForImport(year);
        }

        //get the budget data by year and budget type
        public List<ForecastAssignmentViewModel> GetBudgetDataByYearAndType(int budgetYear, int budgetType)
        {
            var employees = employeeAssignmentDAL.GetBudgetDataByYearAndType(budgetYear, budgetType);

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (String.IsNullOrEmpty(item.ExplanationId))
                    {
                        item.ExplanationId = "0";
                        item.ExplanationName = "n/a";
                    }
                    else
                    {
                        item.ExplanationName = explanationsBLL.GetSingleExplanationByExplanationId(Int32.Parse(item.ExplanationId)).ExplanationName;
                    }
                    item.SerialNumber = count;
                    count++;
                }

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments = this.MarkedAsRedForForecast(employees);
                if (redMarkedForecastAssignments.Count > 0)
                {
                    foreach (var item in redMarkedForecastAssignments)
                    {
                        item.forecasts = employeeAssignmentDAL.GetBudgetForecastData(item.Id, budgetYear.ToString());
                    }
                }

            }
            foreach (var item in employees)
            {
                item.EmployeeNameWithCodeRemarks += "$" + item.MarkedAsRed.ToString().ToLower() + "$" + item.Id; ;
            }

            // order by and group by
            if (employees.Count > 0)
            {
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e => e.EmployeeName).SelectMany(e => e).ToList();
            }

            if (employees.Count > 0)
            {
                int count = 1;
                foreach (var item in employees)
                {
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower() == "total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count == 0)
                    {
                        item.forecasts = new List<ForecastDto>();

                        item.forecasts.Add(new ForecastDto { Month = 10, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 11, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 12, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 1, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 2, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 3, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 4, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 5, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 6, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 7, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 8, Points = 0, Total = "" });
                        item.forecasts.Add(new ForecastDto { Month = 9, Points = 0, Total = "" });

                    }
                    int innerCount = 1;
                    foreach (var forecast in item.forecasts)
                    {
                        if (innerCount == 1)
                        {
                            forecast.Total = $"=K{count}*L{count}";
                        }
                        if (innerCount == 2)
                        {
                            forecast.Total = $"=K{count}*M{count}";
                        }
                        if (innerCount == 3)
                        {
                            forecast.Total = $"=K{count}*N{count}";
                        }
                        if (innerCount == 4)
                        {
                            forecast.Total = $"=K{count}*O{count}";
                        }
                        if (innerCount == 5)
                        {
                            forecast.Total = $"=K{count}*P{count}";
                        }
                        if (innerCount == 6)
                        {
                            forecast.Total = $"=K{count}*Q{count}";
                        }
                        if (innerCount == 7)
                        {
                            forecast.Total = $"=K{count}*R{count}";
                        }
                        if (innerCount == 8)
                        {
                            forecast.Total = $"=K{count}*S{count}";
                        }
                        if (innerCount == 9)
                        {
                            forecast.Total = $"=K{count}*T{count}";
                        }
                        if (innerCount == 10)
                        {
                            forecast.Total = $"=K{count}*U{count}";
                        }
                        if (innerCount == 11)
                        {
                            forecast.Total = $"=K{count}*V{count}";
                        }
                        if (innerCount == 12)
                        {
                            forecast.Total = $"=K{count}*W{count}";
                        }
                        innerCount++;
                    }

                    count++;
                }
                foreach (var item in employees)
                {
                    if (item.forecasts.Count > 0)
                    {
                        item.OctPoints = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        item.NovPoints = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        item.DecPoints = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        item.JanPoints = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        item.FebPoints = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        item.MarPoints = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        item.AprPoints = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        item.MayPoints = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        item.JunPoints = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        item.JulPoints = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        item.AugPoints = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        item.SepPoints = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        item.OctTotal = item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                        item.NovTotal = item.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                        item.DecTotal = item.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                        item.JanTotal = item.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                        item.FebTotal = item.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                        item.MarTotal = item.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                        item.AprTotal = item.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                        item.MayTotal = item.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                        item.JunTotal = item.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                        item.JulTotal = item.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                        item.AugTotal = item.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                        item.SepTotal = item.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                        item.forecasts = null;
                    }
                    else
                    {
                        item.OctPoints = "0";
                        item.NovPoints = "0";
                        item.DecPoints = "0";
                        item.JanPoints = "0";
                        item.FebPoints = "0";
                        item.MarPoints = "0";
                        item.AprPoints = "0";
                        item.MayPoints = "0";
                        item.JunPoints = "0";
                        item.JulPoints = "0";
                        item.AugPoints = "0";
                        item.SepPoints = "0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal = "";
                        item.FebTotal = "";
                        item.MarTotal = "";
                        item.AprTotal = "";
                        item.MayTotal = "";
                        item.JunTotal = "";
                        item.JulTotal = "";
                        item.AugTotal = "";
                        item.SepTotal = "";

                        item.forecasts = null;
                    }


                }

            }


            return employees;
        }
        public List<EmployeeBudget> GetSecondHlafBudgetData(int year, int select_budget_type)
        {
            return employeeAssignmentDAL.GetSecondHlafBudgetData(year, select_budget_type);
        }
        public int IsBudgetMatchWithAssignmentData(EmployeeBudget _employeeBudget)
        {
            return employeeAssignmentDAL.IsBudgetMatchWithAssignmentData(_employeeBudget);
        }
        public List<ForecastDto> GettForecastDataForSecondHalfBudgetByAssignmentId(int assignmentId, int year)
        {
            return employeeAssignmentDAL.GettForecastDataForSecondHalfBudgetByAssignmentId(assignmentId, year);
        }
        public bool IsOriginalForecastData(int assignmentId, int year,int cellNo)
        {
            EmployeeAssignment employeeAssignment = employeeAssignmentDAL.GetAssignmentChangedAndPendingCellNo(assignmentId, year);

            bool isOriginalData = true;
            if (!string.IsNullOrEmpty(employeeAssignment.BCYRCell))
            {
                var arrBCYRCells = employeeAssignment.BCYRCell.Split(',');
                foreach(var cellItem in arrBCYRCells)
                {
                    if(Convert.ToInt32(cellItem) == cellNo)
                    {
                        isOriginalData = false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(employeeAssignment.BCYRCellPending))
            {
                var arrBCYRCellPending = employeeAssignment.BCYRCellPending.Split(',');
                foreach (var cellItem in arrBCYRCellPending)
                {
                    if (Convert.ToInt32(cellItem) == cellNo)
                    {
                        isOriginalData = false;
                    }
                }
            }
            return isOriginalData;
        }
        public decimal GetForecastOriginalPointsForBudget(int assignmentId, int monthId, int year)
        {
            return employeeAssignmentDAL.GetForecastOriginalPointsForBudget(assignmentId, monthId, year);
        }
        public ForecastTotalManMonthCostsViewModal GetTotalCalculationForManmonthAndCost(int year)
        {
            return employeeAssignmentDAL.GetTotalCalculationForManmonthAndCost(year);
        }
        public List<EmployeeAssignment> GetEmployeeNameForMenuChange(int year, int employeeId)
        {
            return employeeAssignmentDAL.GetEmployeeNameForMenuChange(year, employeeId);
        }
        public List<EmployeeAssignment> GetDeletedEmployeeCount(int year, int employeeId)
        {
            return employeeAssignmentDAL.GetDeletedEmployeeCount(year, employeeId);
        }
        public int RemoveBudgetAssignment(string budgetAssignmentId)
        {
            return employeeAssignmentDAL.RemoveBudgetAssignment(budgetAssignmentId);
        }
        public ForecastTotalManMonthCostsViewModal GetTotalManMonthAndCostForBudgetEdit(int year,int budgetType)
        {
            return employeeAssignmentDAL.GetTotalManMonthAndCostForBudgetEdit(year, budgetType);
        }
        public List<QaProportion> GetQAProportionsWithEmployee(string employeeId, string year)
        {
            return employeeAssignmentDAL.GetQAProportionsWithEmployee(employeeId,year);
        }
        public int InsertEmployeeAssignmentsForTimeStamps(EmployeeAssignment employeeAssignment, int timeStampId)
        {
            return employeeAssignmentDAL.InsertEmployeeAssignmentsForTimeStamps(employeeAssignment, timeStampId);
        }
        public int GetAssignmentTimeStampsLastId()
        {
            return employeeAssignmentDAL.GetAssignmentTimeStampsLastId();
        }

        public List<EmployeeAssignment> GetEmployeesAssignmentsByYear(int year,string strUpdatedAssignmentIds)
        {           
            return employeeAssignmentDAL.GetEmployeesAssignmentsByYear(year, strUpdatedAssignmentIds);
        }
        public List<Forecast> GetAssignmentForecastByYearAndAssignmentId(int assignmentId, int year)
        {
            return employeeAssignmentDAL.GetAssignmentForecastByYearAndAssignmentId(assignmentId, year);
        }

        /************
         * Difference Methods: Starts
        *************/
        public List<ForecastAssignmentViewModel> GetForecastedDataByTimestampId(string departmentIds, string companyIds, int year,string timestampsId)
        {            
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetForecastedAssignmentDataByTimestampId(departmentIds, companyIds, year, timestampsId);

            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastedDataByTimestampAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetCostByCompanyAndInchargeIds(string inchargeId, string companyIds, int year, string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetCostByCompanyAndInchargeIds(inchargeId, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastedDataByTimestampAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetManMonthForDifferenceByDepartments(string departmentId, string companyIds, int year,string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetForecastedAssignmentDataByTimestampId(departmentId, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastedDataByTimestampAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetBudgetManmonthByDepartments(string departmentIds, string companyIds, int year)
        {
            //List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetBudgetManmonthByDepartments(departmentIds, companyIds, year);
            List<ForecastAssignmentViewModel> forecastAssignments = totalDAL.GetBudgetCostByCompanyAndDepartmentId(departmentIds, companyIds, year);

            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetBudgetManMonthByAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.Id);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        public List<ForecastAssignmentViewModel> GetTotalManmonthForDifferenceByIncharge(string inchargeIds, string companyIds, int year, string timestampsId)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = employeeAssignmentDAL.GetCostByCompanyAndInchargeIds(inchargeIds, companyIds, year, timestampsId);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = employeeAssignmentDAL.GetForecastedDataByTimestampAssignmentId(forecastAssignment.Id, year.ToString());
                    if (forecastAssignment.forecasts.Count > 0)
                    {
                        forecastAssignment.OctPoints = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points.ToString();
                        forecastAssignment.NovPoints = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Points.ToString();
                        forecastAssignment.DecPoints = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Points.ToString();
                        forecastAssignment.JanPoints = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Points.ToString();
                        forecastAssignment.FebPoints = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Points.ToString();
                        forecastAssignment.MarPoints = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Points.ToString();
                        forecastAssignment.AprPoints = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Points.ToString();
                        forecastAssignment.MayPoints = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Points.ToString();
                        forecastAssignment.JunPoints = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Points.ToString();
                        forecastAssignment.JulPoints = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Points.ToString();
                        forecastAssignment.AugPoints = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Points.ToString();
                        forecastAssignment.SepPoints = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Points.ToString();

                        forecastAssignment.OctTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.OctPoints)).ToString();
                        forecastAssignment.NovTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.NovPoints)).ToString();
                        forecastAssignment.DecTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.DecPoints)).ToString();
                        forecastAssignment.JanTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JanPoints)).ToString();
                        forecastAssignment.FebTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.FebPoints)).ToString();
                        forecastAssignment.MarTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MarPoints)).ToString();
                        forecastAssignment.AprTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AprPoints)).ToString();
                        forecastAssignment.MayTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.MayPoints)).ToString();
                        forecastAssignment.JunTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JunPoints)).ToString();
                        forecastAssignment.JulTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.JulPoints)).ToString();
                        forecastAssignment.AugTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.AugPoints)).ToString();
                        forecastAssignment.SepTotal = (Convert.ToDouble(forecastAssignment.UnitPrice) * Convert.ToDouble(forecastAssignment.SepPoints)).ToString();

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year, forecastAssignment.AssignmentTimeStampId);
                    if (forecastAssignment.ActualCosts.Count == 0)
                    {
                        forecastAssignment.ActualCosts = new List<ActualCost>();
                        forecastAssignment.ActualCosts.Add(new ActualCost { });
                    }

                }
            }
            return forecastAssignments;
        }

        /************
         * Difference Methods: Ends
        *************/
    }
}