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
        public int CreateTimeStampAndAssignmentHistory(ForecastHisory forecastHisory,List<AssignmentHistory> assignmentHistories,bool isUpdate)
        {
            return forecastDAL.CreateTimeStampAndAssignmentHistory(forecastHisory, assignmentHistories, isUpdate);
        }
        public int CreateAssignmenttHistory(AssignmentHistory assignmentHistory, int timeStampId,bool isUpdate)
        {
            return forecastDAL.CreateAssignmenttHistory(assignmentHistory, timeStampId, isUpdate);
        }
        public List<ForecastHisory> GetTimeStamps_Year(int year)
        {
            return forecastDAL.GetTimeStamps_Year(year);
        }
        public List<ForecastHisory> GetApprovalTimeStamps(int year)
        {
            return forecastDAL.GetApprovalTimeStamps(year);
        }
        public List<Forecast> GetForecastHistories(int timeStampId)
        {
            return forecastDAL.GetForecastHistories(timeStampId);
        }
        public List<Forecast> GetForecastsByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetForecastsByAssignmentId(assignmentId);
        }
        public List<Forecast> GetPreviousManMonth(string monthId_Points)
        {
            return forecastDAL.GetPreviousManMonth(monthId_Points);
        }
        public AssignmentHistoryViewModal GetCellWiseUpdatePreviousData(int assignmentId)
        {
            return forecastDAL.GetCellWiseUpdatePreviousData(assignmentId);
        }
        public AssignmentHistoryViewModal GetCellWiseUpdateOriginalData(int assignmentId,int timeStampId)
        {
            return forecastDAL.GetCellWiseUpdateOriginalData(assignmentId,timeStampId);
        }

        public Forecast MatchForecastHistoryByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.MatchForecastHistoryByAssignmentId(assignmentId, date);
        }
        public Forecast MatchForecastHistoryUsernamesByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.MatchForecastHistoryUsernamesByAssignmentId(assignmentId,date);
        }
        public List<Forecast> GetMatchedForecastHistoryByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetMatchedForecastHistoryByAssignmentId(assignmentId);
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
                    employeeAssignment.EmployeeName = item.EmployeeName;
                    employeeAssignment.BCYRCell = "";

                    int result = employeeAssignmentBLL.CreateAssignment(employeeAssignment);
                    if (result == 1)
                    {
                        int employeeAssignmentLastId = employeeAssignmentBLL.GetLastId();
                        List<Forecast> forecasts = new List<Forecast>();

                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.OctPoint, Month = 10, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.NovPoint, Month = 11, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.DecPoint, Month = 12, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JanPoint, Month = 1, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.FebPoint, Month = 2, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MarPoint, Month = 3, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AprPoint, Month = 4, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.MayPoint, Month = 5, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JunPoint, Month = 6, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.JulPoint, Month = 7, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.AugPoint, Month = 8, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        //forecasts.Add(new Forecast { EmployeeAssignmentId = employeeAssignmentLastId, Points = item.SepPoint, Month = 9, Total = 0, CreatedDate = DateTime.Now, CreatedBy = "", Year = Convert.ToInt32(insertYear) });
                        forecasts = forecastDAL.GetForecastDetails(employeeAssignment.Id,copyYear);
                        foreach (var forecastItem in forecasts)
                        {
                            forecastItem.Year = insertYear;
                            forecastItem.EmployeeAssignmentId = employeeAssignmentLastId;
                            resultSave = forecastDAL.CreateForecast(forecastItem);
                        }
                    }
                }
            }
               
            return resultSave;
        }

        public List<Forecast> GetHistoriesByTimeStampId(int timeStampId)
        {
            return forecastDAL.GetHistoriesByTimeStampId(timeStampId);
        }
        public List<Forecast> GetAssignmentHistoriesByTimeStampId(int timeStampId)
        {
            return forecastDAL.GetAssignmentHistoriesByTimeStampId(timeStampId);
        }
        public List<Forecast> GetApprovalHistoriesByTimeStampId(int timeStampId)
        {
            return forecastDAL.GetApprovalHistoriesByTimeStampId(timeStampId);
        }
        public List<int> GetYearFromHistory()
        {
            return forecastDAL.GetYearFromHistory();
        }
        public List<int> GetAssignmentYearList()
        {
            return forecastDAL.GetAssignmentYearList();
        }
        public List<int> GetApprovalAssignmentYearList()
        {
            return forecastDAL.GetApprovalAssignmentYearList();
        }
        public List<Forecast> GetForecastDetails(int assignmentId, int copyYear)
        {
            return forecastDAL.GetForecastDetails(assignmentId, copyYear);
        }
        public AssignmentHistory GetPreviousAssignmentDataById(int assignmentId)
        {
            return forecastDAL.GetPreviousAssignmentDataById(assignmentId);
        }
        public AssignmentHistoryViewModal GetAssignmentNamesForHistory(int assignmentId, int timeStampId)
        {
            return forecastDAL.GetAssignmentNamesForHistory(assignmentId, timeStampId);
        }
        public ApprovalHistoryViewModal GetApprovalNamesForHistory(int assignmentId,int timeStampId)
        {
            return forecastDAL.GetApprovalNamesForHistory(assignmentId, timeStampId);
        }
        public AssignmentHistoryViewModal GetOriginalForecastedData(int assignmentId)
        {
            return forecastDAL.GetOriginalForecastedData(assignmentId);
        }
        public int CreateApproveTimeStamp(string approveTimeStamp, int year, string createdBy, DateTime createdDate)
        {
            return forecastDAL.CreateApproveTimeStamp(approveTimeStamp, year, createdBy, createdDate);
        }
        public int CreateApprovetHistory(int approveTimeStampId, int year, string createdBy, List<AssignmentHistory> _assignmentHistories_Add, List<AssignmentHistory> _assignmentHistorys_Delete, List<AssignmentHistory> _assignmentHistorys_CellWise)
        {
            return forecastDAL.CreateApprovetHistory(approveTimeStampId, year, createdBy, _assignmentHistories_Add,_assignmentHistorys_Delete,_assignmentHistorys_CellWise);
        }
        public List<AssignmentHistory> GetAddEmployeeApprovedData(int year)
        {
            return forecastDAL.GetAddEmployeeApprovedData(year);
        }
        public List<AssignmentHistory> GetDeleteEmployeeApprovedData(int year)
        {
            return forecastDAL.GetDeleteEmployeeApprovedData(year);
        }
        public List<AssignmentHistory> GetCellWiseEmployeeApprovedData(int year)
        {
            return forecastDAL.GetCellWiseEmployeeApprovedData(year);
        }

        public EmployeeAssignment GetAssignmentDetailsById(int assignmentId, int year)
        {
            return forecastDAL.GetAssignmentDetailsById(assignmentId, year);
        }
        public List<EmployeeAssignment> GetAllUnapprovalDataForCells(int year)
        {
            return forecastDAL.GetAllUnapprovalDataForCells(year);
        }        

        public string GetApproveCellData(int cellNo, string previousCellName, string originalCellName,string approvedCells)
        {
            if (approvedCells.IndexOf(',') > 0)
            {
                //approved cell: 2,4

                var arrApprovedCells = approvedCells.Split(',');
                var tempValue = "";
                foreach (var approvedItems in arrApprovedCells)
                {
                    if (cellNo == Convert.ToInt32(approvedItems))
                    {
                        tempValue = originalCellName;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(tempValue))
                {
                    return tempValue;
                }
                else
                {
                    var tempCellValue = previousCellName == originalCellName ? "" : "(" + previousCellName + ") " + originalCellName;
                    return tempCellValue;
                    //return "(" + previousCellName + ")" + originalCellName;
                }
            }
            else
            {
                if (cellNo == Convert.ToInt32(approvedCells))
                {
                    return originalCellName;
                }
                else
                {
                    var tempCellValue = previousCellName == originalCellName ? "" : "(" + previousCellName + ") " + originalCellName;
                    return tempCellValue;
                    //return "(" + previousCellName + ")" + originalCellName;
                }
                
            }
        }
        public string GetApproveForecastCellData(int cellNo, decimal previousCellName, decimal originalCellName, string approvedCells)
        {
            if (approvedCells.IndexOf(',') > 0)
            {
                //approved cell: 2,4

                var arrApprovedCells = approvedCells.Split(',');
                var tempValue = "";
                foreach (var approvedItems in arrApprovedCells)
                {
                    if (cellNo == Convert.ToInt32(approvedItems))
                    {
                        tempValue = originalCellName.ToString();
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(tempValue))
                {
                    return tempValue;
                }
                else
                {
                    var cellManMonths = previousCellName == originalCellName ? "" : "(" + previousCellName.ToString("0.0") + ") " + originalCellName.ToString("0.0");
                    return cellManMonths;
                }
            }
            else
            {
                if (cellNo == Convert.ToInt32(approvedCells))
                {                    
                    return originalCellName.ToString();
                }
                else
                {
                    var cellManMonths = previousCellName == originalCellName ? "" : "(" + previousCellName.ToString("0.0") + ") " + originalCellName.ToString("0.0");
                    return cellManMonths;
                }

            }
        }
        public string GetHistoryTimeStampName(int timeStampId)
        {
            return forecastDAL.GetHistoryTimeStampName(timeStampId);
        }
    }
}