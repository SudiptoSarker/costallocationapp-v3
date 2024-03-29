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
        public int CreateFinalBudgetForecast(Forecast forecast)
        {
            return forecastDAL.CreateFinalBudgetForecast(forecast);
        }
        public int CreateBudgetForecast(Forecast forecast)
        {
            return forecastDAL.CreateBudgetForecast(forecast);
        }
        public bool CheckAssignmentId(int assignmentId,int year,int month)
        {
            return forecastDAL.CheckAssignmentId(assignmentId, year, month);
        }
        public bool CheckBudgetId(int assignmentId,int year,int month)
        {
            return forecastDAL.CheckBudgetId(assignmentId, year, month);
        }
        public int UpdateForecast(Forecast forecast)
        {
            return forecastDAL.UpdateForecast(forecast);
        }
        public int InsertForecastWithTimeStamp(Forecast forecast)
        {
            return forecastDAL.InsertForecastWithTimeStamp(forecast);
        }
        public int UpdateBudgetForecast(Forecast forecast)
        {
            return forecastDAL.UpdateBudgetForecast(forecast);
        }
        public int CreateTimeStamp(ForecastHisory forecastHisory)
        {
            return forecastDAL.CreateTimeStamp(forecastHisory);
        }
        public int CreateTimeStampsForYearlyEditData(ForecastHisory forecastHisory)
        {
            return forecastDAL.CreateTimeStampsForYearlyEditData(forecastHisory);
        }

        public int CreateAssignmentHistoryWithTimeStampId(List<AssignmentHistory> assignmentHistories, bool isUpdate, bool isDeleted,int timeStampId)
        {
            return forecastDAL.CreateAssignmentHistoryWithTimeStampId(assignmentHistories, isUpdate, isDeleted, timeStampId);
        }
        public int CreateAssignmenttHistory(AssignmentHistory assignmentHistory, int timeStampId,bool isUpdate,bool isDeleted,bool isOriginal)
        {
            return forecastDAL.CreateAssignmenttHistory(assignmentHistory, timeStampId, isUpdate, isDeleted, isOriginal);
        }
        public List<ForecastHisory> GetTimeStamps_Year(int year)
        {
            return forecastDAL.GetTimeStamps_Year(year);
        }
        public List<ForecastHisory> GetApprovalTimeStamps(int year)
        {
            return forecastDAL.GetApprovalTimeStamps(year);
        }

        public List<Forecast> GetForecastsByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetForecastsByAssignmentId(assignmentId);
        }
        public List<Forecast> GetForecastHitostyForApproval(int assignmentId)
        {
            return forecastDAL.GetForecastHitostyForApproval(assignmentId);
        }
        public List<Forecast> GetBudgetForecastsByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetBudgetForecastsByAssignmentId(assignmentId);
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
        public List<ForecastYear> GetBudgetYear()
        {
            return forecastDAL.GetBudgetYear();
        }
        public List<ForecastYear> GetBudgetFinalizeYear()
        {
            return forecastDAL.GetBudgetFinalizeYear();
        }
        public int GetLatestBudgetYear()
        {
            return forecastDAL.GetLatestBudgetYear();
        }

        public int GetReplicateYearForecastType(int replicateYear)
        {
            bool isSecondHalfBudgetFinalize = forecastDAL.GetReplicateYearForecastType(replicateYear);
            if (isSecondHalfBudgetFinalize)
            {
                return 2;
            }
            else
            {
                return 1;
            }            
        }
        public int DuplicateBudget(int copyYear, int insertYear,int budgetType,string approve_timestamp)
        {
            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();
            int replicateYearBudgetType = GetReplicateYearForecastType(copyYear);

            if (Convert.ToInt32(approve_timestamp) == copyYear)
            {
                excelAssignmentDtos = forecastDAL.GetEmployeesBudgetByYear(copyYear, replicateYearBudgetType);
            }
            else
            {
                excelAssignmentDtos = employeeAssignmentBLL.GetAllOriginalDataForReplciateBudget(copyYear.ToString(), Convert.ToInt32(approve_timestamp));                
            }


            int resultSave = 0;
            if (excelAssignmentDtos.Count > 0)
            {
                foreach (var item in excelAssignmentDtos)
                {
                    //insert forecast assignment here
                    EmployeeBudget employeeAssignment = new EmployeeBudget();
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
                    employeeAssignment.EmployeeModifiedName = item.EmployeeModifiedName;

                    employeeAssignment.DuplicateFrom = item.DuplicateFrom;
                    employeeAssignment.DuplicateCount = item.DuplicateCount;
                    employeeAssignment.RoleChanged = item.RoleChanged;
                    employeeAssignment.UnitPriceChanged = item.UnitPriceChanged;

                    if (!string.IsNullOrEmpty(budgetType.ToString()))
                    {
                        if (budgetType == 1)
                        {
                            employeeAssignment.FirstHalfBudget = true;
                            employeeAssignment.SecondHalfBudget = false;
                        }else if (budgetType == 2)
                        {
                            employeeAssignment.FirstHalfBudget = false;
                            employeeAssignment.SecondHalfBudget = true;
                        }
                        else
                        {
                            employeeAssignment.FirstHalfBudget = false;
                            employeeAssignment.SecondHalfBudget = false;
                        }
                    }
                    else
                    {
                        employeeAssignment.FirstHalfBudget = false;
                        employeeAssignment.SecondHalfBudget = false;
                    }                 
                    employeeAssignment.FinalizedBudget = false;

                    int result = employeeAssignmentBLL.CreateBudgets(employeeAssignment);

                    if (result == 1)
                    {
                        int employeeAssignmentLastId = employeeAssignmentBLL.GetBudgetLastId();
                        List<Forecast> forecasts = new List<Forecast>();
                      
                        if (Convert.ToInt32(approve_timestamp) == copyYear)
                        {
                            forecasts = forecastDAL.GetBudgetForecastDetails(employeeAssignment.Id, copyYear);
                        }
                        else
                        {
                            forecasts = employeeAssignmentBLL.GetApprovedForecastdDataForReplicateBudget(employeeAssignment.Id, copyYear.ToString());
                        }                        

                        foreach (var forecastItem in forecasts)
                        {
                            forecastItem.Year = insertYear;
                            forecastItem.EmployeeAssignmentId = employeeAssignmentLastId;
                            resultSave = forecastDAL.CreateBudgetForecast(forecastItem);
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
        public List<Forecast> GetAssignmentHistoriesByTimeStampId(int timeStampId,bool isOriginal)
        {
            return forecastDAL.GetAssignmentHistoriesByTimeStampId(timeStampId, isOriginal);
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
        public AssignmentHistory GetPreviousAssignmentDataById(int assignmentId)
        {
            return forecastDAL.GetPreviousAssignmentDataById(assignmentId);
        }        
        public AssignmentHistoryViewModal GetAssignmentNamesForHistory(int assignmentId, int timeStampId,bool isOriginal)
        {
            return forecastDAL.GetAssignmentNamesForHistory(assignmentId, timeStampId, isOriginal);
        }
        public ApprovalHistoryViewModal GetApprovalNamesForHistory(int assignmentId,int timeStampId)
        {
            return forecastDAL.GetApprovalNamesForHistory(assignmentId, timeStampId);
        }
        public AssignmentHistoryViewModal GetOriginalForecastedDataForApproval(int assignmentId,int timeStampId)
        {
            return forecastDAL.GetOriginalForecastedDataForApproval(assignmentId, timeStampId);
        }
        public int CreateApproveTimeStamp(string approveTimeStamp, int year, string createdBy, DateTime createdDate)
        {
            return forecastDAL.CreateApproveTimeStamp(approveTimeStamp, year, createdBy, createdDate);
        }
        public int CreateApprovetHistory(int approveTimeStampId, int year, string createdBy, List<AssignmentHistory> _assignmentHistories_Add, List<AssignmentHistory> _assignmentHistorys_Delete, List<AssignmentHistory> _assignmentHistorys_CellWise)
        {
            return forecastDAL.CreateApprovetHistory(approveTimeStampId, year, createdBy, _assignmentHistories_Add,_assignmentHistorys_Delete,_assignmentHistorys_CellWise);
        }
        public AssignmentHistory GetAddEmployeeApprovedData(int assignmentId)
        {
            return forecastDAL.GetAddEmployeeApprovedData(assignmentId);
        }
        public AssignmentHistory GetDeleteEmployeeApprovedData(int assignmentId)
        {
            return forecastDAL.GetDeleteEmployeeApprovedData(assignmentId);
        }
        public AssignmentHistory GetCellWiseEmployeeApprovedData(int assignmentId,int year,int cellNo)
        {
            return forecastDAL.GetCellWiseEmployeeApprovedData(assignmentId,year, cellNo);
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
                    if (cellNo == 10)
                    {
                        return Convert.ToInt32(tempValue).ToString("N0");
                    }
                    else
                    {
                        return tempValue;
                    }
                }
                else
                {
                    var tempCellValue = "";
                    if (cellNo == 10)
                    {
                        tempCellValue = previousCellName == originalCellName ? Convert.ToInt32(originalCellName).ToString("N0") : "(" + Convert.ToInt32(previousCellName).ToString("N0") + ") " + Convert.ToInt32(originalCellName).ToString("N0");
                    }
                    else
                    {
                        tempCellValue = previousCellName == originalCellName ? originalCellName : "(" + previousCellName + ") " + originalCellName;
                    }
                    
                    return tempCellValue;
                }
            }
            else
            {
                if (cellNo == Convert.ToInt32(approvedCells))
                {
                    if (cellNo == 10) {
                        return Convert.ToInt32(originalCellName).ToString("N0");
                    }
                    else
                    {
                        return originalCellName;
                    }                    
                }
                else
                {
                    var tempCellValue = "";
                    if (cellNo == 10)
                    {
                        tempCellValue = previousCellName == originalCellName ? Convert.ToInt32(originalCellName).ToString("N0") : "(" + Convert.ToInt32(previousCellName).ToString("N0") + ") " + Convert.ToInt32(originalCellName).ToString("N0");
                    }
                    else
                    {
                        tempCellValue = previousCellName == originalCellName ? originalCellName : "(" + previousCellName + ") " + originalCellName;
                    }                        
                    return tempCellValue;
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
                        tempValue = originalCellName.ToString("0.0");
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(tempValue))
                {
                    return tempValue;
                }
                else
                {
                    var cellManMonths = "";
                    return cellManMonths;
                }
            }
            else
            {
                if (cellNo == Convert.ToInt32(approvedCells))
                {
                    return originalCellName.ToString("0.0");
                }
                else
                {
                    var cellManMonths = "";
                    return cellManMonths;
                }

            }
        }

        public string GetApproveHistoryTimeStampName(int timeStampId)
        {
            return forecastDAL.GetApproveHistoryTimeStampName(timeStampId);
        }

        public int UpdateEmployeeAssignmentApprovedCellsByAssignmentId(AssignmentHistory assignmentHistory)
        {
            return forecastDAL.UpdateEmployeeAssignmentApprovedCellsByAssignmentId(assignmentHistory);
        }
        public string GetApprovedCellsByAssignmentId(string employeeAssignmentId)
        {
            return forecastDAL.GetApprovedCellsByAssignmentId(employeeAssignmentId);
        }
        public int UpdateEmployeeAssignmentApprovedRowByAssignmentId(AssignmentHistory assignmentHistory)
        {
            return forecastDAL.UpdateEmployeeAssignmentApprovedRowByAssignmentId(assignmentHistory); 
        }
        public int InsertApprovedForecastedDataByYear(int approvedTimestampId,int year,string userName,bool isApproval)
        {
            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();

            excelAssignmentDtos = forecastDAL.GetApprovedForecastedDataByYear(year);

            int resultSave = 0;
            if (excelAssignmentDtos.Count > 0)
            {
                foreach (var item in excelAssignmentDtos)
                {
                    if (!item.IsRowPending || !isApproval) {
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
                        employeeAssignment.EmployeeRootName = item.EmployeeRootName;
                        employeeAssignment.EmployeeModifiedName = item.EmployeeModifiedName;
                        employeeAssignment.CreatedBy = userName;
                        employeeAssignment.IsActive = item.IsActive.ToString();
                        employeeAssignment.IsDeleted = item.IsDeleted;
                        employeeAssignment.Year = item.Year;
                        employeeAssignment.BCYRCellPending = item.BCYRCellPending;
                        employeeAssignment.DuplicateFrom = item.DuplicateFrom;
                        employeeAssignment.DuplicateCount = item.DuplicateCount;
                        employeeAssignment.RoleChanged = item.RoleChanged;
                        employeeAssignment.UnitPriceChanged = item.UnitPriceChanged;

                        int result = employeeAssignmentBLL.CreateApprovedAssignmentByTimestampId(employeeAssignment, approvedTimestampId);

                        if (result == 1)
                        {
                            int approvedEmployeeAssignmentLastId = employeeAssignmentBLL.GetApprovedAssignmentLastId();
                            List<Forecast> forecasts = new List<Forecast>();
                            forecasts = forecastDAL.GetApprovedForecastedDataByAssignmentId(employeeAssignment.Id, year);

                            foreach (var forecastItem in forecasts)
                            {
                                forecastItem.Year = year;
                                forecastItem.EmployeeAssignmentId = approvedEmployeeAssignmentLastId;
                                resultSave = forecastDAL.CreateApprovedForecast(forecastItem);
                            }
                        }
                    }
                }
            }

            return resultSave;
        }
        public int UpdateApprovedData_AddRow(AssignmentHistory assignmentHistory,int approveTimeStampId, string assignmentYear)
        {
            return forecastDAL.UpdateApprovedData_AddRow(assignmentHistory, approveTimeStampId, assignmentYear);
        }
        public int UpdateApprovedData_DeleteRow(AssignmentHistory assignmentHistory,int approveTimeStampId, string assignmentYear)
        {
            return forecastDAL.UpdateApprovedData_DeleteRow(assignmentHistory, approveTimeStampId, assignmentYear);
        }

        public int UpdateApprovedCells(int assingmentId,string cellNo,int approvedTimestampId,int year)
        {
            return forecastDAL.UpdateApprovedCells(assingmentId,cellNo,approvedTimestampId,year);
        }
        public int CleanPreviousApprovedDeletedRows(int year,int approvedTimestampsId)
        {
            return forecastDAL.CleanPreviousApprovedDeletedRows(year, approvedTimestampsId);
        }
        public string GetApprovedCellsByTimestampId(int assingmentId, int cellNo, int approvedTimestampId, string year)
        {
            return forecastDAL.GetApprovedCellsByTimestampId(assingmentId, cellNo, approvedTimestampId, year);
        }
        public int UpdateOriginalForecast(Forecast forecast)
        {
            return forecastDAL.UpdateOriginalForecast(forecast);
        }
        public int InsertOriginalForecast(Forecast forecast)
        {
            return forecastDAL.InsertOriginalForecast(forecast); 
        }
        public int CreateForecastBudget(Forecast forecast)
        {
            return forecastDAL.CreateForecastBudget(forecast);
        }
    }
}