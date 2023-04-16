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
    public class ForecastBLL
    {
        ForecastDAL forecastDAL = null;
        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        public ForecastBLL()
        {
            forecastDAL = new ForecastDAL();
            employeeAssignmentBLL = new EmployeeAssignmentBLL();
        }
        public int CreateForecast(Forecast forecast)
        {
            return forecastDAL.CreateForecast(forecast);
        }
        public bool CheckAssignmentId(int assignmentId,int year,int month)
        {
            return forecastDAL.CheckAssignmentId(assignmentId, year, month);
        }
        public int UpdateForecast(Forecast forecast)
        {
            return forecastDAL.UpdateForecast(forecast);
        }
        public int CreateTimeStamp(ForecastHisory forecastHisory)
        {
            return forecastDAL.CreateTimeStamp(forecastHisory);
        }
        public List<ForecastHisory> GetTimeStamps_Year(int year)
        {
            return forecastDAL.GetTimeStamps_Year(year);
        }
        public List<Forecast> GetForecastHistories(int timeStampId)
        {
            return forecastDAL.GetForecastHistories(timeStampId);
        }
        public List<Forecast> GetForecastsByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetForecastsByAssignmentId(assignmentId);
        }
        public bool MatchForecastHistoryByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.MatchForecastHistoryByAssignmentId(assignmentId,date);
        }
        public List<Forecast> GetMatchedForecastHistoryByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.GetMatchedForecastHistoryByAssignmentId(assignmentId, date);
        }
        public List<ForecastYear> GetForecastYear()
        {
            return forecastDAL.GetForecastYear();
        }
        public int DuplicateForecastYear(int copyYear,int insertYear)
        {
            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();
            excelAssignmentDtos = forecastDAL.GetEmployeesForecastByYear(copyYear);
            int resultSave = 0;
            if (excelAssignmentDtos.Count > 0)
            {
                foreach (var item in excelAssignmentDtos)
                {
                    //insert forecast assignment here
                    EmployeeAssignment employeeAssignment = new EmployeeAssignment();
                    employeeAssignment.Id = item.Id;
                    employeeAssignment.Remarks = item.Remarks;
                    employeeAssignment.UpdatedBy = "";
                    employeeAssignment.UpdatedDate = DateTime.Now;
                    employeeAssignment.EmployeeId = item.EmployeeId.ToString();                    
                    employeeAssignment.SectionId = Convert.ToInt32(item.SectionId);
                    employeeAssignment.DepartmentId = Convert.ToInt32(item.DepartmentId);
                    employeeAssignment.InchargeId = Convert.ToInt32(item.InchargeId);
                    employeeAssignment.RoleId = Convert.ToInt32(item.RoleId);
                    employeeAssignment.ExplanationId = item.ExplanationId.ToString();
                    employeeAssignment.CompanyId = Convert.ToInt32(item.CompanyId);
                    employeeAssignment.GradeId = Convert.ToInt32(item.GradeId);
                    employeeAssignment.UnitPrice = Convert.ToInt32(item.UnitPrice);
                    employeeAssignment.Year = insertYear.ToString();

                    int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
                    if (result == 1)
                    {
                        int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                        List<Forecast> forecasts = new List<Forecast>();

                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        foreach (var forecastItem in forecasts)
                        {
                            resultSave = forecastDAL.CreateForecast(forecastItem);
                        }
                    }
                }
            }
               
            return resultSave;
        }
    }
}