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
        public EmployeeAssignmentBLL()
        {
            employeeAssignmentDAL = new EmployeeAssignmentDAL();
            explanationsBLL = new ExplanationsBLL();
            actualCostDAL = new ActualCostDAL();
        }
        public int CreateAssignment(EmployeeAssignment employeeAssignment)
        {
            return employeeAssignmentDAL.CreateAssignment(employeeAssignment);
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

                            forecastAssignment.OctTotal = forecastAssignment.forecasts.Where(f => f.Month == 10).SingleOrDefault().Total;
                            forecastAssignment.NovTotal = forecastAssignment.forecasts.Where(f => f.Month == 11).SingleOrDefault().Total;
                            forecastAssignment.DecTotal = forecastAssignment.forecasts.Where(f => f.Month == 12).SingleOrDefault().Total;
                            forecastAssignment.JanTotal = forecastAssignment.forecasts.Where(f => f.Month == 1).SingleOrDefault().Total;
                            forecastAssignment.FebTotal = forecastAssignment.forecasts.Where(f => f.Month == 2).SingleOrDefault().Total;
                            forecastAssignment.MarTotal = forecastAssignment.forecasts.Where(f => f.Month == 3).SingleOrDefault().Total;
                            forecastAssignment.AprTotal = forecastAssignment.forecasts.Where(f => f.Month == 4).SingleOrDefault().Total;
                            forecastAssignment.MayTotal = forecastAssignment.forecasts.Where(f => f.Month == 5).SingleOrDefault().Total;
                            forecastAssignment.JunTotal = forecastAssignment.forecasts.Where(f => f.Month == 6).SingleOrDefault().Total;
                            forecastAssignment.JulTotal = forecastAssignment.forecasts.Where(f => f.Month == 7).SingleOrDefault().Total;
                            forecastAssignment.AugTotal = forecastAssignment.forecasts.Where(f => f.Month == 8).SingleOrDefault().Total;
                            forecastAssignment.SepTotal = forecastAssignment.forecasts.Where(f => f.Month == 9).SingleOrDefault().Total;

                    }
                    forecastAssignment.ActualCosts = actualCostDAL.GetActualCostsByYear_AssignmentId(year,forecastAssignment.Id);
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

                List<ForecastAssignmentViewModel> redMarkedForecastAssignments =  this.MarkedAsRedForForecast(employees);
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
                employees = employees.OrderBy(e => e.EmployeeName).GroupBy(e=>e.EmployeeName).SelectMany(e=>e).ToList();
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
                    if (item.forecasts!=null && item.forecasts.Count>0)
                    {

                        if(item.forecasts.Where(f => f.Month == 10).SingleOrDefault().Points > 0)
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
                    if (item.EmployeeName.ToLower() == "head count" || item.EmployeeName.ToLower()=="total")
                    {
                        continue;
                    }
                    if (item.forecasts.Count==0)
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
                    if (item.forecasts.Count>0)
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
                        item.JanPoints ="0";
                        item.FebPoints ="0";
                        item.MarPoints ="0";
                        item.AprPoints ="0";
                        item.MayPoints ="0";
                        item.JunPoints ="0";
                        item.JulPoints ="0";
                        item.AugPoints ="0";
                        item.SepPoints ="0";

                        item.OctTotal = "";
                        item.NovTotal = "";
                        item.DecTotal = "";
                        item.JanTotal ="";
                        item.FebTotal ="";
                        item.MarTotal ="";
                        item.AprTotal ="";
                        item.MayTotal ="";
                        item.JunTotal ="";
                        item.JulTotal ="";
                        item.AugTotal ="";
                        item.SepTotal ="";

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
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName+"$"+singleEmployee.EmployeeName +" "+ singleEmployee.SubCode + " (" + singleEmployee.Remarks + ")";
                            }
                            else
                            {
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName+"$"+singleEmployee.EmployeeName + " " + singleEmployee.SubCode;
                            }

                        }
                    }



                    EmployeeAssignmentViewModel employeeAssignmentViewModelFirst = viewModels.Where(vm => vm.SubCode == 1).FirstOrDefault();
                    if (employeeAssignmentViewModelFirst==null)
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
                        if (singleEmployee!=null)
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
                                singleEmployee.EmployeeNameWithCodeRemarks = singleEmployee.EmployeeName+"$"+ singleEmployee.EmployeeName + " " + singleEmployee.SubCode + " (" + singleEmployee.Remarks + ")";
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
        public int UpdateBCYRCellByAssignmentId(int assignmentId, string cell)
        {
            return employeeAssignmentDAL.UpdateBCYRCellByAssignmentId(assignmentId, cell);
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
        public int UpdateCellsByAssignmentid(string updatedApprovedCells, string updatePendingCells,int assignmentId)
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
        public bool CheckForUnApprovedRow(string assignementId,bool isDeletedRow)
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
        public List<ForecastAssignmentViewModel> GetAllOriginalDataForDownloadFiles(EmployeeAssignmentForecast employeeAssignment,int approvedTimestampid)
        {
            var employees = employeeAssignmentDAL.GetAllOriginalDataForDownloadFiles(employeeAssignment,approvedTimestampid);

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


        public int CreateApprovedAssignmentByTimestampId(EmployeeAssignment employeeAssignment,int approvedTimestampId)
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
        public int UpdateOriginalAssignment(AssignmentHistory _assignmentHistory,string columnValue, string columnName)
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
        public int CheckMonthIdExistsForOrgForecast(int assignmentId,int monthId)
        {
            return employeeAssignmentDAL.CheckMonthIdExistsForOrgForecast(assignmentId, monthId);
        }

        public int RemoveApprovedDataFromOriginalTable(int assignmentId,int cellNo)
        {
            int results = 0;
            if(cellNo == 2)
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
            else if(cellNo == 11)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId,10);
            }
            else if (cellNo == 12)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 11);
            }
            else if (cellNo == 13)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 12);
            }
            else if (cellNo == 14)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 1);
            }
            else if (cellNo == 15)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 2);
            }
            else if (cellNo == 16)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 3);
            }
            else if (cellNo == 17)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 4);
            }
            else if (cellNo == 18)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 5);
            }
            else if (cellNo == 19)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 6);
            }
            else if (cellNo == 20)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 7);
            }
            else if (cellNo == 21)
            {
                employeeAssignmentDAL.RemoveForecastedDataFromOrgTable(assignmentId, 8);
            }
            else if (cellNo == 22)
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
            return employeeAssignmentDAL.GetMonthWiseOriginalForecastData(assignmentId,dbColumnName);
        }
        public bool CheckForValidOriginalData(string pendingCellNumbers,string cellNumber,string changedCells)
        {
            bool isValidRequest = true;
            var arrPendingCells = pendingCellNumbers.Split(',');
            foreach(var cellItem in arrPendingCells)
            {
                if (cellItem == cellNumber)
                {
                    isValidRequest = false;
                }
            }
            var arrChangedCels = changedCells.Split(',');
            foreach(var changedItem in arrChangedCels) {
                if (changedItem == cellNumber)
                {
                    isValidRequest = false;
                }
            }
            return isValidRequest;
        }
        public bool CheckForBudgetYearIsExists(int selected_year,int select_budget_type)
        {
            return employeeAssignmentDAL.CheckForBudgetYearIsExists(selected_year, select_budget_type);
        }
        public int FinalizeBudgetAssignment(int selected_year,int select_budget_type)
        {
            return employeeAssignmentDAL.FinalizeBudgetAssignment(selected_year, select_budget_type);
        }
        public List<EmployeeBudget> GetFinalizedBudgetData(int selected_year, int select_budget_type)
        {
            return employeeAssignmentDAL.GetFinalizedBudgetData(selected_year, select_budget_type);
        }
        public bool CheckYearIfFinalize(int year,int requestType)
        {
            return employeeAssignmentDAL.CheckYearIfFinalize(year, requestType);
        }
        public void DeleteAssignment_PreviousFinalizeData(int year)
        {
            employeeAssignmentDAL.DeleteAssignment_PreviousFinalizeData(year);
        }
    }
}