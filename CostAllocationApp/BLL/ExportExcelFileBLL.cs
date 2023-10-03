using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using ExcelNumberFormat;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CostAllocationApp.BLL
{
    public class ExportExcelFileBLL
    {
        EmployeeAssignmentBLL employeeAssignmentBLL = new EmployeeAssignmentBLL();

        public ExcelWorksheet ExportOriginalExcelSheet(ExcelWorksheet sheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {

            sheet = GetOriginalExcelSheetHeader(sheet);

            int count = 2;
            foreach (var item in forecastAssignmentViewModels)
            {
                if (item.EmployeeAssignmentIdOrg == 112)
                {
                    //test
                }
                var assignmentId = item.Id;
                var employeeName = item.EmployeeName;
                var rootEmployeeName = item.RootEmployeeName;
                var employeeId = item.EmployeeId;
                var sectionName = item.SectionName;
                var departmentName = item.DepartmentName;
                var inChargeName = item.InchargeName;
                var roleName = item.RoleName;
                var explanationName = item.ExplanationName;
                var companyName = item.CompanyName;
                var gradePoints = item.GradePoint;
                var unitPrice = item.UnitPrice;
                var remarks = item.Remarks;

                var isDeleteRow = item.IsDeleteEmployee;
                var isAddRow = item.IsAddEmployee;
                var isUpdateCells = item.IsCellWiseUpdate;
                var approvedCells = item.ApprovedCells;
                var bCYRCellPending = item.BCYRCellPending;
                var employeeAssignmentIdOrg = item.EmployeeAssignmentIdOrg;

                var octPOriginal = item.OctPoints;
                var novPOriginal = item.NovPoints;
                var decPOriginal = item.DecPoints;
                var janPOriginal = item.JanPoints;
                var febPOriginal = item.FebPoints;
                var marPOriginal = item.MarPoints;
                var aprPOriginal = item.AprPoints;
                var mayPOriginal = item.MayPoints;
                var junPOriginal = item.JunPoints;
                var julPOriginal = item.JulPoints;
                var augPOriginal = item.AugPoints;
                var sepPOriginal = item.SepPoints;

                //deleted,added row and approval and not approval cells color
                
                string sectionNameReceived = GetApprovedOrOriginalVlaue("3", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, sectionName);
                sheet.Cells["A" + count].Value = sectionNameReceived;
                sheet.Cells["A" + count].AutoFitColumns();                

                string departmentNameReceived = GetApprovedOrOriginalVlaue("4", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, departmentName);
                sheet.Cells["B" + count].Value = departmentNameReceived;
                sheet.Cells["B" + count].AutoFitColumns();

                string inChargeNameReceived = GetApprovedOrOriginalVlaue("5", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, inChargeName);
                sheet.Cells["C" + count].Value = inChargeNameReceived;
                sheet.Cells["C" + count].AutoFitColumns();

                string roleNameReceived = GetApprovedOrOriginalVlaue("6", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, roleName);
                sheet.Cells["D" + count].Value = roleNameReceived;
                sheet.Cells["D" + count].AutoFitColumns();

                string explanationNameReceived = GetApprovedOrOriginalVlaue("7", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, explanationName);
                sheet.Cells["E" + count].Value = explanationNameReceived;
                sheet.Cells["E" + count].AutoFitColumns();

                sheet.Cells["F" + count].Value = employeeName is "" ? rootEmployeeName : employeeName;
                sheet.Cells["F" + count].AutoFitColumns();

                string remarksReceived = GetApprovedOrOriginalVlaue("2", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, remarks);
                sheet.Cells["G" + count].Value = remarksReceived;
                sheet.Cells["G" + count].AutoFitColumns();

                string companyNameReceived = GetApprovedOrOriginalVlaue("8", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, companyName);
                sheet.Cells["H" + count].Value = companyNameReceived;
                sheet.Cells["H" + count].AutoFitColumns();

                string gradePointsReceived = GetApprovedOrOriginalVlaue("9", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, gradePoints);
                sheet.Cells["I" + count].Value = gradePointsReceived;
                sheet.Cells["I" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                
                sheet.Cells["I" + count].AutoFitColumns();

                string unitPriceReceived = GetApprovedOrOriginalVlaue("10", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, unitPrice);                
                sheet.Cells["J" + count].Value = Convert.ToInt32(unitPriceReceived);
                sheet.Cells["J" + count].Style.Numberformat.Format = "#,#";
                sheet.Cells["J" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                sheet.Cells["J" + count].AutoFitColumns();

                string octPOriginalReceived = GetApprovedOrOriginalVlaue("11", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, octPOriginal);                
                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginalReceived);
                sheet.Cells["K" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["K" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                
                sheet.Cells["K" + count].AutoFitColumns();                

                string novPOriginalReceived = GetApprovedOrOriginalVlaue("12", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, novPOriginal);                
                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginalReceived);
                sheet.Cells["L" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["L" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                
                sheet.Cells["L" + count].AutoFitColumns();

                string decPOriginalReceived = GetApprovedOrOriginalVlaue("13", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, decPOriginal);                
                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginalReceived);
                sheet.Cells["M" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["M" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["M" + count].AutoFitColumns();

                string janPOriginalReceived = GetApprovedOrOriginalVlaue("14", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, janPOriginal);
                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginalReceived);
                sheet.Cells["N" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["N" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["N" + count].AutoFitColumns();

                string febPOriginalReceived = GetApprovedOrOriginalVlaue("15", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, febPOriginal);
                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginalReceived);
                sheet.Cells["O" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["O" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["O" + count].AutoFitColumns();

                string marPOriginalReceived = GetApprovedOrOriginalVlaue("16", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, marPOriginal);
                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginalReceived);
                sheet.Cells["P" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["P" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["P" + count].AutoFitColumns();

                string aprPOriginalReceived = GetApprovedOrOriginalVlaue("17", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, aprPOriginal);
                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginalReceived);
                sheet.Cells["Q" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["Q" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["Q" + count].AutoFitColumns();

                string mayPOriginalReceived = GetApprovedOrOriginalVlaue("18", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, mayPOriginal);
                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginalReceived);
                sheet.Cells["R" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["R" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["R" + count].AutoFitColumns();

                string junPOriginalReceived = GetApprovedOrOriginalVlaue("19", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, junPOriginal);
                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginalReceived);
                sheet.Cells["S" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["S" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["S" + count].AutoFitColumns();

                string julPOriginalReceived = GetApprovedOrOriginalVlaue("20", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, julPOriginal);
                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginalReceived);
                sheet.Cells["T" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["T" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["T" + count].AutoFitColumns();

                string augPOriginalReceived = GetApprovedOrOriginalVlaue("21", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, augPOriginal);
                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginalReceived);
                sheet.Cells["U" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["U" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["U" + count].AutoFitColumns();

                string sepPOriginalReceived = GetApprovedOrOriginalVlaue("22", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, sepPOriginal);
                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginalReceived);
                sheet.Cells["V" + count].Style.Numberformat.Format = "0.00";
                sheet.Cells["V" + count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["V" + count].AutoFitColumns();

                if (isAddRow)
                {
                    sheet = SetApprovalColor(sheet, count);                    
                }
                else if (isDeleteRow)
                {
                    sheet = SetDeleteRowColor(sheet, count);
                }
                else if (!string.IsNullOrEmpty(approvedCells))
                {
                    bool isSectionApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("3", approvedCells);
                    if (isSectionApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet,count,"A");
                    }                    

                    bool isDeptApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("4", approvedCells);
                    if (isDeptApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "B");
                    }                    

                    bool isInChargeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("5", approvedCells);
                    if (isInChargeApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "C");
                    }                    

                    bool isRoleApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("6", approvedCells);
                    if (isRoleApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "D");                        
                    }                    

                    bool isExplanationApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("7", approvedCells);
                    if (isExplanationApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "E");
                    }                    

                    bool isEmployeeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("1", approvedCells);
                    if (isEmployeeApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "F");                     
                    }                    

                    bool isRemarksApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("2", approvedCells);
                    if (isRemarksApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "G");
                    }
                   


                    bool isCompanyApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("8", approvedCells);
                    if (isCompanyApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "H");
                    }
                   

                    bool isGradeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("9", approvedCells);
                    if (isGradeApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "I");
                    }                    

                    bool isUnitApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("10", approvedCells);
                    if (isUnitApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "J");
                    }
                    
                    bool isOctPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("11", approvedCells);
                    if (isOctPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "K");
                    }                    

                    bool isNovPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("12", approvedCells);
                    if (isNovPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "L");
                    }                    

                    bool isDecPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("13", approvedCells);
                    if (isDecPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "M");
                    }

                    bool isJanPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("14", approvedCells);
                    if (isJanPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "N");
                    }

                    bool isFebPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("15", approvedCells);
                    if (isFebPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "O");
                    }

                    bool isMarPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("16", approvedCells);
                    if (isMarPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "P");
                    }

                    bool isAprPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("17", approvedCells);
                    if (isAprPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "Q");
                    }

                    bool isMayPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("18", approvedCells);
                    if (isMayPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "R");
                    }
                   
                    bool isJunPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("19", approvedCells);
                    if (isJunPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "S");
                    }

                    bool isJulPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("20", approvedCells);
                    if (isJulPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "T");
                    }
                    
                    bool isAugPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("21", approvedCells);
                    if (isAugPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "U");
                    }                   

                    bool isSeptPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("22", approvedCells);
                    if (isSeptPApproved)
                    {
                        sheet = SetCellWiseApprovalColor(sheet, count, "V");
                    }                    
                }                

                count++;
            }
            return sheet;
        }

        public ExcelWorksheet ExportEachPersonExcelSheet(ExcelWorksheet eachPersonSheet,List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {
            eachPersonSheet = GetEachPersonExcelSheetHeader(eachPersonSheet);

            int countEachPerson = 2;
            List<DownloadApproveHistoryViewModal> objEachPersonList = new List<DownloadApproveHistoryViewModal>();

            foreach (var item in forecastAssignmentViewModels)
            {
                var employeeName = item.EmployeeName;
                var rootEmployeeName = item.RootEmployeeName;
                var employeeId = item.EmployeeId;
                var sectionName = item.SectionName;
                var departmentName = item.DepartmentName;
                var inChargeName = item.InchargeName;
                var roleName = item.RoleName;
                var explanationName = item.ExplanationName;
                var companyName = item.CompanyName;
                var gradePoints = item.GradePoint;
                var unitPrice = item.UnitPrice;
                var remarks = item.Remarks;

                var isDeleteRow = item.IsDeleteEmployee;
                var isAddRow = item.IsAddEmployee;
                var isUpdateCells = item.IsCellWiseUpdate;

                var octPOriginal = item.OctPoints;
                var novPOriginal = item.NovPoints;
                var decPOriginal = item.DecPoints;
                var janPOriginal = item.JanPoints;
                var febPOriginal = item.FebPoints;
                var marPOriginal = item.MarPoints;
                var aprPOriginal = item.AprPoints;
                var mayPOriginal = item.MayPoints;
                var junPOriginal = item.JunPoints;
                var julPOriginal = item.JulPoints;
                var augPOriginal = item.AugPoints;
                var sepPOriginal = item.SepPoints;
                if (!item.IsDeleteEmployee)
                {
                    if (countEachPerson == 2)
                    {
                        DownloadApproveHistoryViewModal eachPerson = new DownloadApproveHistoryViewModal();

                        eachPerson.EmployeeName = rootEmployeeName;
                        eachPerson.EmployeeId = employeeId;
                        eachPerson.SectionName = sectionName;
                        eachPerson.DepartmentName = departmentName;
                        eachPerson.CompanyName = companyName;
                        eachPerson.GradePoint = gradePoints;

                        eachPerson.OctPoints = Convert.ToDecimal(octPOriginal);
                        eachPerson.NovPoints = Convert.ToDecimal(novPOriginal);
                        eachPerson.DecPoints = Convert.ToDecimal(decPOriginal);
                        eachPerson.JanPoints = Convert.ToDecimal(janPOriginal);
                        eachPerson.FebPoints = Convert.ToDecimal(febPOriginal);
                        eachPerson.MarPoints = Convert.ToDecimal(marPOriginal);
                        eachPerson.AprPoints = Convert.ToDecimal(aprPOriginal);
                        eachPerson.MayPoints = Convert.ToDecimal(mayPOriginal);
                        eachPerson.JunPoints = Convert.ToDecimal(junPOriginal);
                        eachPerson.JulPoints = Convert.ToDecimal(julPOriginal);
                        eachPerson.AugPoints = Convert.ToDecimal(augPOriginal);
                        eachPerson.SepPoints = Convert.ToDecimal(sepPOriginal);

                        objEachPersonList.Add(eachPerson);
                    }
                    else
                    {
                        bool isSamePersonForEachPerson = false;

                        foreach (var eachPersonFromItem in objEachPersonList)
                        {
                            if (eachPersonFromItem.EmployeeId == employeeId)
                            {
                                isSamePersonForEachPerson = true;
                                eachPersonFromItem.EmployeeName = rootEmployeeName;
                                eachPersonFromItem.EmployeeId = employeeId;
                                eachPersonFromItem.SectionName = sectionName;
                                eachPersonFromItem.DepartmentName = departmentName;
                                eachPersonFromItem.CompanyName = companyName;
                                if (!string.IsNullOrEmpty(gradePoints))
                                {
                                    if (!string.IsNullOrEmpty(eachPersonFromItem.GradePoint))
                                    {
                                        eachPersonFromItem.GradePoint = eachPersonFromItem.GradePoint + "," + gradePoints;
                                    }
                                    else
                                    {
                                        eachPersonFromItem.GradePoint = gradePoints;
                                    }
                                }

                                eachPersonFromItem.OctPoints = eachPersonFromItem.OctPoints + Convert.ToDecimal(octPOriginal);
                                eachPersonFromItem.NovPoints = eachPersonFromItem.NovPoints + Convert.ToDecimal(novPOriginal);
                                eachPersonFromItem.DecPoints = eachPersonFromItem.DecPoints + Convert.ToDecimal(decPOriginal);
                                eachPersonFromItem.JanPoints = eachPersonFromItem.JanPoints + Convert.ToDecimal(janPOriginal);
                                eachPersonFromItem.FebPoints = eachPersonFromItem.FebPoints + Convert.ToDecimal(febPOriginal);
                                eachPersonFromItem.MarPoints = eachPersonFromItem.MarPoints + Convert.ToDecimal(marPOriginal);
                                eachPersonFromItem.AprPoints = eachPersonFromItem.AprPoints + Convert.ToDecimal(aprPOriginal);
                                eachPersonFromItem.MayPoints = eachPersonFromItem.MayPoints + Convert.ToDecimal(mayPOriginal);
                                eachPersonFromItem.JunPoints = eachPersonFromItem.JunPoints + Convert.ToDecimal(junPOriginal);
                                eachPersonFromItem.JulPoints = eachPersonFromItem.JulPoints + Convert.ToDecimal(julPOriginal);
                                eachPersonFromItem.AugPoints = eachPersonFromItem.AugPoints + Convert.ToDecimal(augPOriginal);
                                eachPersonFromItem.SepPoints = eachPersonFromItem.SepPoints + Convert.ToDecimal(sepPOriginal);
                            }
                        }

                        if (!isSamePersonForEachPerson)
                        {
                            DownloadApproveHistoryViewModal eachPerson2 = new DownloadApproveHistoryViewModal();

                            eachPerson2.EmployeeName = rootEmployeeName;
                            eachPerson2.EmployeeId = employeeId;
                            eachPerson2.SectionName = sectionName;
                            eachPerson2.DepartmentName = departmentName;
                            eachPerson2.CompanyName = companyName;
                            eachPerson2.GradePoint = gradePoints;

                            eachPerson2.OctPoints = Convert.ToDecimal(octPOriginal);
                            eachPerson2.NovPoints = Convert.ToDecimal(novPOriginal);
                            eachPerson2.DecPoints = Convert.ToDecimal(decPOriginal);
                            eachPerson2.JanPoints = Convert.ToDecimal(janPOriginal);
                            eachPerson2.FebPoints = Convert.ToDecimal(febPOriginal);
                            eachPerson2.MarPoints = Convert.ToDecimal(marPOriginal);
                            eachPerson2.AprPoints = Convert.ToDecimal(aprPOriginal);
                            eachPerson2.MayPoints = Convert.ToDecimal(mayPOriginal);
                            eachPerson2.JunPoints = Convert.ToDecimal(junPOriginal);
                            eachPerson2.JulPoints = Convert.ToDecimal(julPOriginal);
                            eachPerson2.AugPoints = Convert.ToDecimal(augPOriginal);
                            eachPerson2.SepPoints = Convert.ToDecimal(sepPOriginal);

                            objEachPersonList.Add(eachPerson2);
                        }
                    }
                }
                countEachPerson++;
            }

            if (objEachPersonList.Count > 0)
            {
                int eachPersonIndex = 2;
                foreach (var eachItem in objEachPersonList)
                {
                    eachPersonSheet.Cells["A" + eachPersonIndex].Value = eachItem.EmployeeName;
                    if (!string.IsNullOrEmpty(eachItem.CompanyName))
                    {
                        if (eachItem.CompanyName.ToLower() == "mw")
                        {
                            eachPersonSheet.Cells["B" + eachPersonIndex].Value = "MW";
                        }
                        else
                        {
                            eachPersonSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                        }
                    }
                    else
                    {
                        eachPersonSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                    }
                    if (!string.IsNullOrEmpty(eachItem.SectionName))
                    {
                        if (eachItem.SectionName.ToLower() == "開発")
                        {
                            eachPersonSheet.Cells["C" + eachPersonIndex].Value = "開発";
                        }
                        else
                        {
                            eachPersonSheet.Cells["C" + eachPersonIndex].Value = "企画";
                        }
                    }
                    else
                    {
                        eachPersonSheet.Cells["C" + eachPersonIndex].Value = "企画";
                    }

                    if (!string.IsNullOrEmpty(eachItem.CompanyName))
                    {
                        if (eachItem.CompanyName.ToLower() == "mw")
                        {
                            eachPersonSheet.Cells["D" + eachPersonIndex].Value = "";
                        }
                        else
                        {
                            eachPersonSheet.Cells["D" + eachPersonIndex].Value = eachItem.CompanyName;
                        }
                    }
                    else
                    {
                        eachPersonSheet.Cells["D" + eachPersonIndex].Value = "";
                    }
                    if (!string.IsNullOrEmpty(eachItem.CompanyName))
                    {
                        if (eachItem.CompanyName.ToLower() == "mw")
                        {
                            eachPersonSheet.Cells["E" + eachPersonIndex].Value = eachItem.GradePoint;
                            eachPersonSheet.Cells["E" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            eachPersonSheet.Cells["E" + eachPersonIndex].AutoFitColumns();
                        }
                    }
                    
                    eachPersonSheet.Cells["F" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.OctPoints);
                    eachPersonSheet.Cells["F" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["F" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                    
                    eachPersonSheet.Cells["F" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["G" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.NovPoints);
                    eachPersonSheet.Cells["G" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["G" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                    
                    eachPersonSheet.Cells["G" + eachPersonIndex].AutoFitColumns();
                    
                    eachPersonSheet.Cells["H" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.DecPoints);
                    eachPersonSheet.Cells["H" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["H" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                    
                    eachPersonSheet.Cells["H" + eachPersonIndex].AutoFitColumns();
                    
                    eachPersonSheet.Cells["I" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JanPoints);
                    eachPersonSheet.Cells["I" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["I" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["I" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["J" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.FebPoints);
                    eachPersonSheet.Cells["J" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["J" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["J" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["K" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MarPoints);
                    eachPersonSheet.Cells["K" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["K" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                   
                    eachPersonSheet.Cells["K" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["L" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AprPoints);
                    eachPersonSheet.Cells["L" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["L" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["L" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["M" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MayPoints);
                    eachPersonSheet.Cells["M" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["M" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["M" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["N" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JunPoints);
                    eachPersonSheet.Cells["N" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["N" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["N" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["O" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JulPoints);
                    eachPersonSheet.Cells["O" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["O" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["O" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["P" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AugPoints);
                    eachPersonSheet.Cells["P" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["P" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;                    
                    eachPersonSheet.Cells["P" + eachPersonIndex].AutoFitColumns();

                    eachPersonSheet.Cells["Q" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.SepPoints);
                    eachPersonSheet.Cells["Q" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                    eachPersonSheet.Cells["Q" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    eachPersonSheet.Cells["Q" + eachPersonIndex].AutoFitColumns();

                    eachPersonIndex++;
                }
            }

            return eachPersonSheet;
        }

        public ExcelWorksheet ExportDistributedSheet(ExcelWorksheet distributedWorksheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels,string hid_selected_year)
        {
            //distributedWorksheet.Cells["A1"].Value = "区分(Section)	";
            //distributedWorksheet.Cells["A1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["A1"].AutoFitColumns();

            //distributedWorksheet.Cells["B1"].Value = "部署(Dept)";
            //distributedWorksheet.Cells["B1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["B1"].AutoFitColumns();

            //distributedWorksheet.Cells["C1"].Value = "担当作業(In chg)	";
            //distributedWorksheet.Cells["C1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["C1"].AutoFitColumns();

            //distributedWorksheet.Cells["D1"].Value = "役割(Role)";
            //distributedWorksheet.Cells["D1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["D1"].AutoFitColumns();

            //distributedWorksheet.Cells["E1"].Value = "説明(expl)";
            //distributedWorksheet.Cells["E1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["E1"].AutoFitColumns();

            distributedWorksheet.Cells["A1"].Value = "従業員名(Emp)";
            distributedWorksheet.Cells["A1"].Style.Font.Bold = true; ;
            distributedWorksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["A1"].AutoFitColumns();

            //distributedWorksheet.Cells["G1"].Value = "Remaks";
            //distributedWorksheet.Cells["G1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["G1"].AutoFitColumns();

            //distributedWorksheet.Cells["H1"].Value = "会社(Com)	";
            //distributedWorksheet.Cells["H1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["H1"].AutoFitColumns();

            //distributedWorksheet.Cells["I1"].Value = "グレード(Grade)";
            //distributedWorksheet.Cells["I1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["I1"].AutoFitColumns();

            //distributedWorksheet.Cells["J1"].Value = "単価(Unit Price)	";
            //distributedWorksheet.Cells["J1"].Style.Font.Bold = true;
            //distributedWorksheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            //distributedWorksheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            //distributedWorksheet.Cells["J1"].AutoFitColumns();


            distributedWorksheet.Cells["B1"].Value = "部署(Dept)";
            distributedWorksheet.Cells["B1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["B1"].AutoFitColumns();

            distributedWorksheet.Cells["C1"].Value = "10月";
            distributedWorksheet.Cells["C1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["C1"].AutoFitColumns();

            distributedWorksheet.Cells["D1"].Value = "11月";
            distributedWorksheet.Cells["D1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["D1"].AutoFitColumns();

            distributedWorksheet.Cells["E1"].Value = "12月";
            distributedWorksheet.Cells["E1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["E1"].AutoFitColumns();

            distributedWorksheet.Cells["F1"].Value = "1月";
            distributedWorksheet.Cells["F1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["F1"].AutoFitColumns();

            distributedWorksheet.Cells["G1"].Value = "2月";
            distributedWorksheet.Cells["G1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["G1"].AutoFitColumns();

            distributedWorksheet.Cells["H1"].Value = "3月";
            distributedWorksheet.Cells["H1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["H1"].AutoFitColumns();

            distributedWorksheet.Cells["I1"].Value = "4月";
            distributedWorksheet.Cells["I1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["I1"].AutoFitColumns();

            distributedWorksheet.Cells["J1"].Value = "5月";
            distributedWorksheet.Cells["J1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["J1"].AutoFitColumns();

            distributedWorksheet.Cells["K1"].Value = "6月";
            distributedWorksheet.Cells["K1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["K1"].AutoFitColumns();

            distributedWorksheet.Cells["L1"].Value = "7月";
            distributedWorksheet.Cells["L1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["L1"].AutoFitColumns();

            distributedWorksheet.Cells["M1"].Value = "8月";
            distributedWorksheet.Cells["M1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["M1"].AutoFitColumns();

            distributedWorksheet.Cells["N1"].Value = "9月";
            distributedWorksheet.Cells["N1"].Style.Font.Bold = true;
            distributedWorksheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            distributedWorksheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            distributedWorksheet.Cells["N1"].AutoFitColumns();

            List<ForecastAssignmentViewModel> forecastAssignmentViewModelsForDistinctEmployees = new List<ForecastAssignmentViewModel>();
            int distributedCount = 2;
            foreach (var item in forecastAssignmentViewModels)
            {
                if (!item.IsDeleteEmployee)
                {

                    //new logic: start
                    bool isSameDistributedEmployee = false;
                    bool isSameQCEmployee = false;

                    if (item.DepartmentName == "品証")
                    {
                        if (forecastAssignmentViewModelsForDistinctEmployees.Count > 0)
                        {
                            foreach (var distributedItem in forecastAssignmentViewModelsForDistinctEmployees)
                            {
                                if (distributedItem.EmployeeId == item.EmployeeId && distributedItem.DepartmentName == "品証")
                                {
                                    isSameQCEmployee = true;
                                    isSameDistributedEmployee = true;
                                    distributedItem.OctPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.OctPoints) + Convert.ToDecimal(item.OctPoints)).ToString();
                                    distributedItem.NovPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.NovPoints) + Convert.ToDecimal(item.NovPoints)).ToString();
                                    distributedItem.DecPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.DecPoints) + Convert.ToDecimal(item.DecPoints)).ToString();
                                    distributedItem.JanPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.JanPoints) + Convert.ToDecimal(item.JanPoints)).ToString();
                                    distributedItem.FebPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.FebPoints) + Convert.ToDecimal(item.FebPoints)).ToString();
                                    distributedItem.MarPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.MarPoints) + Convert.ToDecimal(item.MarPoints)).ToString();
                                    distributedItem.AprPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.AprPoints) + Convert.ToDecimal(item.AprPoints)).ToString();
                                    distributedItem.MayPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.MayPoints) + Convert.ToDecimal(item.MayPoints)).ToString();
                                    distributedItem.JunPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.JunPoints) + Convert.ToDecimal(item.JunPoints)).ToString();
                                    distributedItem.JulPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.JulPoints) + Convert.ToDecimal(item.JulPoints)).ToString();
                                    distributedItem.AugPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.AugPoints) + Convert.ToDecimal(item.AugPoints)).ToString();
                                    distributedItem.SepPoints = Convert.ToDecimal(Convert.ToDecimal(distributedItem.SepPoints) + Convert.ToDecimal(item.SepPoints)).ToString();
                                }
                            }

                            if (!isSameDistributedEmployee)
                            {
                                ForecastAssignmentViewModel forecastAssignmentViewModel2 = new ForecastAssignmentViewModel();

                                forecastAssignmentViewModel2.EmployeeName = item.EmployeeName;
                                forecastAssignmentViewModel2.RootEmployeeName = item.RootEmployeeName;
                                forecastAssignmentViewModel2.EmployeeId = item.EmployeeId;
                                forecastAssignmentViewModel2.SectionName = item.SectionName;
                                forecastAssignmentViewModel2.DepartmentName = item.DepartmentName;
                                forecastAssignmentViewModel2.InchargeName = item.InchargeName;
                                forecastAssignmentViewModel2.RoleName = item.RoleName;
                                forecastAssignmentViewModel2.ExplanationName = item.ExplanationName;
                                forecastAssignmentViewModel2.CompanyName = item.CompanyName;
                                forecastAssignmentViewModel2.GradePoint = item.GradePoint;
                                forecastAssignmentViewModel2.UnitPrice = item.UnitPrice;
                                forecastAssignmentViewModel2.Remarks = item.Remarks;
                                forecastAssignmentViewModel2.Year = item.Year;

                                forecastAssignmentViewModel2.IsDeleteEmployee = item.IsDeleteEmployee;
                                forecastAssignmentViewModel2.IsAddEmployee = item.IsAddEmployee;
                                forecastAssignmentViewModel2.IsCellWiseUpdate = item.IsCellWiseUpdate;
                                forecastAssignmentViewModel2.ApprovedCells = item.ApprovedCells;

                                forecastAssignmentViewModel2.OctPoints = item.OctPoints;
                                forecastAssignmentViewModel2.NovPoints = item.NovPoints;
                                forecastAssignmentViewModel2.DecPoints = item.DecPoints;
                                forecastAssignmentViewModel2.JanPoints = item.JanPoints;
                                forecastAssignmentViewModel2.FebPoints = item.FebPoints;
                                forecastAssignmentViewModel2.MarPoints = item.MarPoints;
                                forecastAssignmentViewModel2.AprPoints = item.AprPoints;
                                forecastAssignmentViewModel2.MayPoints = item.MayPoints;
                                forecastAssignmentViewModel2.JunPoints = item.JunPoints;
                                forecastAssignmentViewModel2.JulPoints = item.JulPoints;
                                forecastAssignmentViewModel2.AugPoints = item.AugPoints;
                                forecastAssignmentViewModel2.SepPoints = item.SepPoints;

                                forecastAssignmentViewModelsForDistinctEmployees.Add(forecastAssignmentViewModel2);
                            }
                        }
                        else
                        {
                            ForecastAssignmentViewModel forecastAssignmentViewModel2 = new ForecastAssignmentViewModel();

                            forecastAssignmentViewModel2.EmployeeName = item.EmployeeName;
                            forecastAssignmentViewModel2.RootEmployeeName = item.RootEmployeeName;
                            forecastAssignmentViewModel2.EmployeeId = item.EmployeeId;
                            forecastAssignmentViewModel2.SectionName = item.SectionName;
                            forecastAssignmentViewModel2.DepartmentName = item.DepartmentName;
                            forecastAssignmentViewModel2.InchargeName = item.InchargeName;
                            forecastAssignmentViewModel2.RoleName = item.RoleName;
                            forecastAssignmentViewModel2.ExplanationName = item.ExplanationName;
                            forecastAssignmentViewModel2.CompanyName = item.CompanyName;
                            forecastAssignmentViewModel2.GradePoint = item.GradePoint;
                            forecastAssignmentViewModel2.UnitPrice = item.UnitPrice;
                            forecastAssignmentViewModel2.Remarks = item.Remarks;
                            forecastAssignmentViewModel2.Year = item.Year;

                            forecastAssignmentViewModel2.IsDeleteEmployee = item.IsDeleteEmployee;
                            forecastAssignmentViewModel2.IsAddEmployee = item.IsAddEmployee;
                            forecastAssignmentViewModel2.IsCellWiseUpdate = item.IsCellWiseUpdate;
                            forecastAssignmentViewModel2.ApprovedCells = item.ApprovedCells;

                            forecastAssignmentViewModel2.OctPoints = item.OctPoints;
                            forecastAssignmentViewModel2.NovPoints = item.NovPoints;
                            forecastAssignmentViewModel2.DecPoints = item.DecPoints;
                            forecastAssignmentViewModel2.JanPoints = item.JanPoints;
                            forecastAssignmentViewModel2.FebPoints = item.FebPoints;
                            forecastAssignmentViewModel2.MarPoints = item.MarPoints;
                            forecastAssignmentViewModel2.AprPoints = item.AprPoints;
                            forecastAssignmentViewModel2.MayPoints = item.MayPoints;
                            forecastAssignmentViewModel2.JunPoints = item.JunPoints;
                            forecastAssignmentViewModel2.JulPoints = item.JulPoints;
                            forecastAssignmentViewModel2.AugPoints = item.AugPoints;
                            forecastAssignmentViewModel2.SepPoints = item.SepPoints;

                            forecastAssignmentViewModelsForDistinctEmployees.Add(forecastAssignmentViewModel2);
                        }
                    }
                    else
                    {
                        ForecastAssignmentViewModel forecastAssignmentViewModel2 = new ForecastAssignmentViewModel();

                        forecastAssignmentViewModel2.EmployeeName = item.EmployeeName;
                        forecastAssignmentViewModel2.RootEmployeeName = item.RootEmployeeName;
                        forecastAssignmentViewModel2.EmployeeId = item.EmployeeId;
                        forecastAssignmentViewModel2.SectionName = item.SectionName;
                        forecastAssignmentViewModel2.DepartmentName = item.DepartmentName;
                        forecastAssignmentViewModel2.InchargeName = item.InchargeName;
                        forecastAssignmentViewModel2.RoleName = item.RoleName;
                        forecastAssignmentViewModel2.ExplanationName = item.ExplanationName;
                        forecastAssignmentViewModel2.CompanyName = item.CompanyName;
                        forecastAssignmentViewModel2.GradePoint = item.GradePoint;
                        forecastAssignmentViewModel2.UnitPrice = item.UnitPrice;
                        forecastAssignmentViewModel2.Remarks = item.Remarks;
                        forecastAssignmentViewModel2.Year = item.Year;

                        forecastAssignmentViewModel2.IsDeleteEmployee = item.IsDeleteEmployee;
                        forecastAssignmentViewModel2.IsAddEmployee = item.IsAddEmployee;
                        forecastAssignmentViewModel2.IsCellWiseUpdate = item.IsCellWiseUpdate;
                        forecastAssignmentViewModel2.ApprovedCells = item.ApprovedCells;

                        forecastAssignmentViewModel2.OctPoints = item.OctPoints;
                        forecastAssignmentViewModel2.NovPoints = item.NovPoints;
                        forecastAssignmentViewModel2.DecPoints = item.DecPoints;
                        forecastAssignmentViewModel2.JanPoints = item.JanPoints;
                        forecastAssignmentViewModel2.FebPoints = item.FebPoints;
                        forecastAssignmentViewModel2.MarPoints = item.MarPoints;
                        forecastAssignmentViewModel2.AprPoints = item.AprPoints;
                        forecastAssignmentViewModel2.MayPoints = item.MayPoints;
                        forecastAssignmentViewModel2.JunPoints = item.JunPoints;
                        forecastAssignmentViewModel2.JulPoints = item.JulPoints;
                        forecastAssignmentViewModel2.AugPoints = item.AugPoints;
                        forecastAssignmentViewModel2.SepPoints = item.SepPoints;

                        forecastAssignmentViewModelsForDistinctEmployees.Add(forecastAssignmentViewModel2);
                    }
                    //new logic: end      
                }
            }

            if (forecastAssignmentViewModelsForDistinctEmployees.Count > 0)
            {
                int distributedCountDistinct = 2;
                foreach (var distributedItem in forecastAssignmentViewModelsForDistinctEmployees)
                {
                    if (!distributedItem.IsDeleteEmployee)
                    {
                        if (distributedItem.DepartmentName == "品証")
                        {
                            List<ForecastDistributdViewModal> forecastAssignmentViewModelsForQCPercentage = employeeAssignmentBLL.GetQCAssignemntsPercentageByEmployeeIdAndYear(distributedItem.EmployeeId, Convert.ToInt32(hid_selected_year));
                            if (forecastAssignmentViewModelsForQCPercentage.Count > 0)
                            {
                                foreach (var qcItem in forecastAssignmentViewModelsForQCPercentage)
                                {
                                    var employeeName = distributedItem.EmployeeName;
                                    var rootEmployeeName = distributedItem.RootEmployeeName;
                                    var employeeId = distributedItem.EmployeeId;
                                    var sectionName = distributedItem.SectionName;
                                    var departmentName = qcItem.DepartmentName;
                                    var inChargeName = distributedItem.InchargeName;
                                    var roleName = distributedItem.RoleName;
                                    var explanationName = distributedItem.ExplanationName;
                                    var companyName = distributedItem.CompanyName;
                                    var gradePoints = distributedItem.GradePoint;
                                    var unitPrice = distributedItem.UnitPrice;
                                    var remarks = distributedItem.Remarks;

                                    var isDeleteRow = distributedItem.IsDeleteEmployee;
                                    var isAddRow = distributedItem.IsAddEmployee;
                                    var isUpdateCells = distributedItem.IsCellWiseUpdate;
                                    var approvedCells = distributedItem.ApprovedCells;

                                    var octPOriginal = (Convert.ToDecimal(distributedItem.OctPoints) * qcItem.OctPercentage) / 100;
                                    var novPOriginal = (Convert.ToDecimal(distributedItem.NovPoints) * qcItem.NovPercentage) / 100;
                                    var decPOriginal = (Convert.ToDecimal(distributedItem.DecPoints) * qcItem.DecPercentage) / 100;
                                    var janPOriginal = (Convert.ToDecimal(distributedItem.JanPoints) * qcItem.JanPercentage) / 100;
                                    var febPOriginal = (Convert.ToDecimal(distributedItem.FebPoints) * qcItem.FebPercentage) / 100;
                                    var marPOriginal = (Convert.ToDecimal(distributedItem.MarPoints) * qcItem.MarPercentage) / 100;
                                    var aprPOriginal = (Convert.ToDecimal(distributedItem.AprPoints) * qcItem.AprPercentage) / 100;
                                    var mayPOriginal = (Convert.ToDecimal(distributedItem.MayPoints) * qcItem.Maypercentage) / 100;
                                    var junPOriginal = (Convert.ToDecimal(distributedItem.JunPoints) * qcItem.JunPercentage) / 100;
                                    var julPOriginal = (Convert.ToDecimal(distributedItem.JulPoints) * qcItem.JulPercentage) / 100;
                                    var augPOriginal = (Convert.ToDecimal(distributedItem.AugPoints) * qcItem.AugPercentage) / 100;
                                    var sepPOriginal = (Convert.ToDecimal(distributedItem.SepPoints) * qcItem.SepPercentage) / 100;

                                    //distributedWorksheet.Cells["A" + distributedCountDistinct].Value = sectionName;
                                    //distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["B" + distributedCountDistinct].Value = departmentName;
                                    //distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["C" + distributedCountDistinct].Value = inChargeName;
                                    //distributedWorksheet.Cells["C" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["D" + distributedCountDistinct].Value = roleName;
                                    //distributedWorksheet.Cells["D" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["E" + distributedCountDistinct].Value = explanationName;
                                    //distributedWorksheet.Cells["E" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["F" + distributedCountDistinct].Value = employeeName;
                                    //distributedWorksheet.Cells["F" + distributedCountDistinct].AutoFitColumns();
                                    distributedWorksheet.Cells["A" + distributedCountDistinct].Value = rootEmployeeName;
                                    distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["G" + distributedCountDistinct].Value = remarks;
                                    //distributedWorksheet.Cells["G" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["H" + distributedCountDistinct].Value = companyName;
                                    //distributedWorksheet.Cells["H" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["I" + distributedCountDistinct].Value = gradePoints;
                                    //distributedWorksheet.Cells["I" + distributedCountDistinct].AutoFitColumns();

                                    //distributedWorksheet.Cells["J" + distributedCountDistinct].Value = unitPrice;
                                    //distributedWorksheet.Cells["J" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["B" + distributedCountDistinct].Value = departmentName;
                                    distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["C" + distributedCountDistinct].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["C" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["C" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["D" + distributedCountDistinct].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["D" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["D" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["E" + distributedCountDistinct].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["E" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["E" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["F" + distributedCountDistinct].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["F" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["F" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["G" + distributedCountDistinct].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["G" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["G" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["H" + distributedCountDistinct].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["H" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["H" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["I" + distributedCountDistinct].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["I" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["I" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["J" + distributedCountDistinct].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["J" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["J" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["K" + distributedCountDistinct].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["K" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["K" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["L" + distributedCountDistinct].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["L" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["L" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["M" + distributedCountDistinct].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["M" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["M" + distributedCountDistinct].AutoFitColumns();

                                    distributedWorksheet.Cells["N" + distributedCountDistinct].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["N" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    distributedWorksheet.Cells["N" + distributedCountDistinct].AutoFitColumns();

                                    distributedCountDistinct++;
                                }
                            }
                            else
                            {
                                //distributedWorksheet.Cells["A" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                                //distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["B" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                                //distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["C" + distributedCountDistinct].Value = distributedItem.InchargeName;
                                //distributedWorksheet.Cells["C" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["D" + distributedCountDistinct].Value = distributedItem.RoleName;
                                //distributedWorksheet.Cells["D" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["E" + distributedCountDistinct].Value = distributedItem.ExplanationName;
                                //distributedWorksheet.Cells["E" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["A" + distributedCountDistinct].Value = distributedItem.RootEmployeeName;
                                distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["G" + distributedCountDistinct].Value = distributedItem.Remarks;
                                //distributedWorksheet.Cells["G" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["H" + distributedCountDistinct].Value = distributedItem.CompanyName;
                                //distributedWorksheet.Cells["H" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["I" + distributedCountDistinct].Value = distributedItem.GradePoint;
                                //distributedWorksheet.Cells["I" + distributedCountDistinct].AutoFitColumns();

                                //distributedWorksheet.Cells["J" + distributedCountDistinct].Value = distributedItem.UnitPrice;
                                //distributedWorksheet.Cells["J" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["B" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                                distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["C" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.OctPoints).ToString("0.0");
                                distributedWorksheet.Cells["C" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["C" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["D" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.NovPoints).ToString("0.0");
                                distributedWorksheet.Cells["D" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["D" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["E" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.DecPoints).ToString("0.0");
                                distributedWorksheet.Cells["E" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["E" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["F" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JanPoints).ToString("0.0");
                                distributedWorksheet.Cells["F" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["F" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["G" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.FebPoints).ToString("0.0");
                                distributedWorksheet.Cells["G" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["G" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["H" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.MarPoints).ToString("0.0");
                                distributedWorksheet.Cells["H" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["H" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["I" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.AprPoints).ToString("0.0");
                                distributedWorksheet.Cells["I" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["I" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["J" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.MayPoints).ToString("0.0");
                                distributedWorksheet.Cells["J" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["J" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["K" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JunPoints).ToString("0.0");
                                distributedWorksheet.Cells["K" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["K" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["L" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JulPoints).ToString("0.0");
                                distributedWorksheet.Cells["L" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["L" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["M" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.AugPoints).ToString("0.0");
                                distributedWorksheet.Cells["M" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["M" + distributedCountDistinct].AutoFitColumns();

                                distributedWorksheet.Cells["N" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.SepTotal).ToString("0.0");
                                distributedWorksheet.Cells["N" + distributedCountDistinct].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                distributedWorksheet.Cells["N" + distributedCountDistinct].AutoFitColumns();

                                distributedCountDistinct++;
                            }
                        }
                        else
                        {
                            //distributedWorksheet.Cells["A" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                            //distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["B" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                            //distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["C" + distributedCountDistinct].Value = distributedItem.InchargeName;
                            //distributedWorksheet.Cells["C" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["D" + distributedCountDistinct].Value = distributedItem.RoleName;
                            //distributedWorksheet.Cells["D" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["E" + distributedCountDistinct].Value = distributedItem.ExplanationName;
                            //distributedWorksheet.Cells["E" + distributedCountDistinct].AutoFitColumns();

                            distributedWorksheet.Cells["A" + distributedCountDistinct].Value = distributedItem.RootEmployeeName;
                            distributedWorksheet.Cells["A" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["G" + distributedCountDistinct].Value = distributedItem.Remarks;
                            //distributedWorksheet.Cells["G" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["H" + distributedCountDistinct].Value = distributedItem.CompanyName;
                            //distributedWorksheet.Cells["H" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["I" + distributedCountDistinct].Value = distributedItem.GradePoint;
                            //distributedWorksheet.Cells["I" + distributedCountDistinct].AutoFitColumns();

                            //distributedWorksheet.Cells["J" + distributedCountDistinct].Value = distributedItem.UnitPrice;
                            //distributedWorksheet.Cells["J" + distributedCountDistinct].AutoFitColumns();

                            distributedWorksheet.Cells["B" + distributedCountDistinct].Value = distributedItem.DepartmentName;
                            distributedWorksheet.Cells["B" + distributedCountDistinct].AutoFitColumns();

                            distributedWorksheet.Cells["C" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.OctPoints).ToString("0.0");
                            distributedWorksheet.Cells["D" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.NovPoints).ToString("0.0");
                            distributedWorksheet.Cells["E" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.DecPoints).ToString("0.0");
                            distributedWorksheet.Cells["F" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JanPoints).ToString("0.0");
                            distributedWorksheet.Cells["J" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.FebPoints).ToString("0.0");
                            distributedWorksheet.Cells["H" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.MarPoints).ToString("0.0");
                            distributedWorksheet.Cells["I" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.AprPoints).ToString("0.0");
                            distributedWorksheet.Cells["J" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.MayPoints).ToString("0.0");
                            distributedWorksheet.Cells["K" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JunPoints).ToString("0.0");
                            distributedWorksheet.Cells["L" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.JulPoints).ToString("0.0");
                            distributedWorksheet.Cells["M" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.AugPoints).ToString("0.0");
                            distributedWorksheet.Cells["N" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.SepTotal).ToString("0.0");

                            distributedCountDistinct++;
                        }
                    }
                }
            }
            return distributedWorksheet;
        }

        public ExcelWorksheet ExportPlanningDistributionExcelSheet(ExcelWorksheet planningDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels,string year)
        {
            planningDistributionSheet = GetPlanningAndDevelopmentDistributionExcelSheetHeader(planningDistributionSheet);

            int countEachPerson = 2;
            List<DownloadApproveHistoryViewModal> objEachPersonList = new List<DownloadApproveHistoryViewModal>();

            foreach (var item in forecastAssignmentViewModels)
            {
                var employeeName = item.EmployeeName;
                var rootEmployeeName = item.RootEmployeeName;
                var employeeId = item.EmployeeId;
                var sectionName = item.SectionName;
                var sectionId = item.SectionId;
                var departmentName = item.DepartmentName;
                var departmentId = item.DepartmentId;
                var inChargeName = item.InchargeName;
                var roleName = item.RoleName;
                var explanationName = item.ExplanationName;
                var companyName = item.CompanyName;
                var gradePoints = item.GradePoint;
                var unitPrice = item.UnitPrice;
                var remarks = item.Remarks;

                var isDeleteRow = item.IsDeleteEmployee;
                var isAddRow = item.IsAddEmployee;
                var isUpdateCells = item.IsCellWiseUpdate;

                var octPOriginal = item.OctPoints;
                var novPOriginal = item.NovPoints;
                var decPOriginal = item.DecPoints;
                var janPOriginal = item.JanPoints;
                var febPOriginal = item.FebPoints;
                var marPOriginal = item.MarPoints;
                var aprPOriginal = item.AprPoints;
                var mayPOriginal = item.MayPoints;
                var junPOriginal = item.JunPoints;
                var julPOriginal = item.JulPoints;
                var augPOriginal = item.AugPoints;
                var sepPOriginal = item.SepPoints;
                if (!item.IsDeleteEmployee)
                {
                    if (countEachPerson == 2)
                    {
                        DownloadApproveHistoryViewModal eachPerson = new DownloadApproveHistoryViewModal();

                        eachPerson.EmployeeName = rootEmployeeName;
                        eachPerson.EmployeeId = employeeId;
                        eachPerson.SectionName = sectionName;
                        eachPerson.SectionId = sectionId;
                        eachPerson.DepartmentName = departmentName;
                        eachPerson.DepartmentId = departmentId;
                        eachPerson.CompanyName = companyName;
                        eachPerson.GradePoint = gradePoints;

                        eachPerson.OctPoints = Convert.ToDecimal(octPOriginal);
                        eachPerson.NovPoints = Convert.ToDecimal(novPOriginal);
                        eachPerson.DecPoints = Convert.ToDecimal(decPOriginal);
                        eachPerson.JanPoints = Convert.ToDecimal(janPOriginal);
                        eachPerson.FebPoints = Convert.ToDecimal(febPOriginal);
                        eachPerson.MarPoints = Convert.ToDecimal(marPOriginal);
                        eachPerson.AprPoints = Convert.ToDecimal(aprPOriginal);
                        eachPerson.MayPoints = Convert.ToDecimal(mayPOriginal);
                        eachPerson.JunPoints = Convert.ToDecimal(junPOriginal);
                        eachPerson.JulPoints = Convert.ToDecimal(julPOriginal);
                        eachPerson.AugPoints = Convert.ToDecimal(augPOriginal);
                        eachPerson.SepPoints = Convert.ToDecimal(sepPOriginal);

                        objEachPersonList.Add(eachPerson);
                    }
                    else
                    {
                        bool isSamePersonForEachPerson = false;
                        bool isSameDepartment = false;
                        foreach (var eachPersonFromItem in objEachPersonList)
                        {
                            if (eachPersonFromItem.EmployeeId == employeeId)
                            {
                                isSamePersonForEachPerson = true;
                                
                                if (eachPersonFromItem.DepartmentId == departmentId && eachPersonFromItem.SectionId == sectionId)
                                {
                                    isSameDepartment = true;
                                    eachPersonFromItem.EmployeeName = rootEmployeeName;
                                    eachPersonFromItem.EmployeeId = employeeId;
                                    eachPersonFromItem.SectionName = sectionName;
                                    eachPersonFromItem.SectionId = sectionId;
                                    eachPersonFromItem.DepartmentName = departmentName;
                                    eachPersonFromItem.CompanyName = companyName;
                                    if (!string.IsNullOrEmpty(gradePoints))
                                    {
                                        if (!string.IsNullOrEmpty(eachPersonFromItem.GradePoint))
                                        {
                                            eachPersonFromItem.GradePoint = eachPersonFromItem.GradePoint + "," + gradePoints;
                                        }
                                        else
                                        {
                                            eachPersonFromItem.GradePoint = gradePoints;
                                        }
                                    }

                                    eachPersonFromItem.OctPoints = eachPersonFromItem.OctPoints + Convert.ToDecimal(octPOriginal);
                                    eachPersonFromItem.NovPoints = eachPersonFromItem.NovPoints + Convert.ToDecimal(novPOriginal);
                                    eachPersonFromItem.DecPoints = eachPersonFromItem.DecPoints + Convert.ToDecimal(decPOriginal);
                                    eachPersonFromItem.JanPoints = eachPersonFromItem.JanPoints + Convert.ToDecimal(janPOriginal);
                                    eachPersonFromItem.FebPoints = eachPersonFromItem.FebPoints + Convert.ToDecimal(febPOriginal);
                                    eachPersonFromItem.MarPoints = eachPersonFromItem.MarPoints + Convert.ToDecimal(marPOriginal);
                                    eachPersonFromItem.AprPoints = eachPersonFromItem.AprPoints + Convert.ToDecimal(aprPOriginal);
                                    eachPersonFromItem.MayPoints = eachPersonFromItem.MayPoints + Convert.ToDecimal(mayPOriginal);
                                    eachPersonFromItem.JunPoints = eachPersonFromItem.JunPoints + Convert.ToDecimal(junPOriginal);
                                    eachPersonFromItem.JulPoints = eachPersonFromItem.JulPoints + Convert.ToDecimal(julPOriginal);
                                    eachPersonFromItem.AugPoints = eachPersonFromItem.AugPoints + Convert.ToDecimal(augPOriginal);
                                    eachPersonFromItem.SepPoints = eachPersonFromItem.SepPoints + Convert.ToDecimal(sepPOriginal);
                                }                                
                            }
                        }

                        if (!isSamePersonForEachPerson || !isSameDepartment)
                        {
                            DownloadApproveHistoryViewModal eachPerson2 = new DownloadApproveHistoryViewModal();

                            eachPerson2.EmployeeName = rootEmployeeName;
                            eachPerson2.EmployeeId = employeeId;
                            eachPerson2.SectionName = sectionName;
                            eachPerson2.SectionId = sectionId;
                            eachPerson2.DepartmentName = departmentName;
                            eachPerson2.DepartmentId = departmentId;
                            eachPerson2.CompanyName = companyName;
                            eachPerson2.GradePoint = gradePoints;

                            eachPerson2.OctPoints = Convert.ToDecimal(octPOriginal);
                            eachPerson2.NovPoints = Convert.ToDecimal(novPOriginal);
                            eachPerson2.DecPoints = Convert.ToDecimal(decPOriginal);
                            eachPerson2.JanPoints = Convert.ToDecimal(janPOriginal);
                            eachPerson2.FebPoints = Convert.ToDecimal(febPOriginal);
                            eachPerson2.MarPoints = Convert.ToDecimal(marPOriginal);
                            eachPerson2.AprPoints = Convert.ToDecimal(aprPOriginal);
                            eachPerson2.MayPoints = Convert.ToDecimal(mayPOriginal);
                            eachPerson2.JunPoints = Convert.ToDecimal(junPOriginal);
                            eachPerson2.JulPoints = Convert.ToDecimal(julPOriginal);
                            eachPerson2.AugPoints = Convert.ToDecimal(augPOriginal);
                            eachPerson2.SepPoints = Convert.ToDecimal(sepPOriginal);

                            objEachPersonList.Add(eachPerson2);
                        }
                    }
                }
                countEachPerson++;
            }

            if (objEachPersonList.Count > 0)
            {
                //qc distribution plan : start
                List<DownloadApproveHistoryViewModal> objQCDistribution = new List<DownloadApproveHistoryViewModal>();
                foreach (var qcItem in objEachPersonList)
                {
                    var departmentName = qcItem.DepartmentName;
                    if (departmentName == "品証")
                    {
                        List<QaProportion> qaProportions = employeeAssignmentBLL.GetQAProportionsWithEmployee(qcItem.EmployeeId.ToString(), year);
                        if (qaProportions.Count > 0)
                        {
                            foreach (var qaProportionItem in qaProportions)
                            {
                                DownloadApproveHistoryViewModal downloadApproveHistoryViewModal = new DownloadApproveHistoryViewModal();
                                downloadApproveHistoryViewModal.EmployeeName = qcItem.EmployeeName;
                                downloadApproveHistoryViewModal.SectionName = qcItem.SectionName;
                                downloadApproveHistoryViewModal.DepartmentName = qaProportionItem.DepartmentName;
                                downloadApproveHistoryViewModal.CompanyName = qcItem.CompanyName;
                                downloadApproveHistoryViewModal.GradePoint = qcItem.GradePoint;

                                var tempOctPoints = (Convert.ToDouble(qcItem.OctPoints) * qaProportionItem.OctPercentage) / 100;
                                downloadApproveHistoryViewModal.OctPoints = Convert.ToDecimal(tempOctPoints);

                                var tempNovPoints = (Convert.ToDouble(qcItem.NovPoints) * qaProportionItem.NovPercentage) / 100;
                                downloadApproveHistoryViewModal.NovPoints = Convert.ToDecimal(tempNovPoints);

                                var tempDecPoints = (Convert.ToDouble(qcItem.DecPoints) * qaProportionItem.DecPercentage) / 100;
                                downloadApproveHistoryViewModal.DecPoints = Convert.ToDecimal(tempDecPoints);

                                var tempJanPoints = (Convert.ToDouble(qcItem.JanPoints) * qaProportionItem.JanPercentage) / 100;
                                downloadApproveHistoryViewModal.JanPoints = Convert.ToDecimal(tempJanPoints);

                                var tempFebPoints = (Convert.ToDouble(qcItem.FebPoints) * qaProportionItem.FebPercentage) / 100;
                                downloadApproveHistoryViewModal.FebPoints = Convert.ToDecimal(tempFebPoints);

                                var tempMarPoints = (Convert.ToDouble(qcItem.MarPoints) * qaProportionItem.MarPercentage) / 100;
                                downloadApproveHistoryViewModal.MarPoints = Convert.ToDecimal(tempMarPoints);

                                var tempAprPoints = (Convert.ToDouble(qcItem.AprPoints) * qaProportionItem.AprPercentage) / 100;
                                downloadApproveHistoryViewModal.AprPoints = Convert.ToDecimal(tempAprPoints);

                                var tempMayPoints = (Convert.ToDouble(qcItem.MayPoints) * qaProportionItem.MayPercentage) / 100;
                                downloadApproveHistoryViewModal.MayPoints = Convert.ToDecimal(tempMayPoints);

                                var tempJunPoints = (Convert.ToDouble(qcItem.JunPoints) * qaProportionItem.JunPercentage) / 100;
                                downloadApproveHistoryViewModal.JunPoints = Convert.ToDecimal(tempJunPoints);

                                var tempJulPoints = (Convert.ToDouble(qcItem.JulPoints) * qaProportionItem.JulPercentage) / 100;
                                downloadApproveHistoryViewModal.JulPoints = Convert.ToDecimal(tempJulPoints);

                                var tempAugPoints = (Convert.ToDouble(qcItem.AugPoints) * qaProportionItem.AugPercentage) / 100;
                                downloadApproveHistoryViewModal.AugPoints = Convert.ToDecimal(tempAugPoints);

                                var tempSepPoints = (Convert.ToDouble(qcItem.SepPoints) * qaProportionItem.SepPercentage) / 100;
                                downloadApproveHistoryViewModal.SepPoints = Convert.ToDecimal(tempSepPoints);

                                objQCDistribution.Add(downloadApproveHistoryViewModal);
                            }
                        }
                        else
                        {
                            objQCDistribution.Add(qcItem);
                        }
                    }
                    else
                    {
                        objQCDistribution.Add(qcItem);
                    }
                }
                //qc distribution plan: end

                int eachPersonIndex = 2;
                foreach (var eachItem in objQCDistribution)
                //foreach (var eachItem in objEachPersonList)
                {
                    //Planning Distribution
                    bool isPlanningDistribution = false;

                    if (string.IsNullOrEmpty(eachItem.SectionName))
                    {
                        isPlanningDistribution = true;                        
                    }
                    else
                    {
                        if (eachItem.SectionName.ToLower() != "開発")
                        {
                            isPlanningDistribution = true;
                        }
                    }

                    if (isPlanningDistribution)
                    {
                        planningDistributionSheet.Cells["A" + eachPersonIndex].Value = eachItem.EmployeeName;
                        if (!string.IsNullOrEmpty(eachItem.CompanyName))
                        {
                            if (eachItem.CompanyName.ToLower() == "mw")
                            {
                                planningDistributionSheet.Cells["B" + eachPersonIndex].Value = "MW";
                            }
                            else
                            {
                                planningDistributionSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                            }
                        }
                        else
                        {
                            planningDistributionSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                        }
                        planningDistributionSheet.Cells["C" + eachPersonIndex].Value = "企画";
                        planningDistributionSheet.Cells["D" + eachPersonIndex].Value = eachItem.DepartmentName;
                        if (!string.IsNullOrEmpty(eachItem.CompanyName))
                        {
                            if (eachItem.CompanyName.ToLower() == "mw")
                            {
                                planningDistributionSheet.Cells["E" + eachPersonIndex].Value = "";
                            }
                            else
                            {
                                planningDistributionSheet.Cells["E" + eachPersonIndex].Value = eachItem.CompanyName;
                            }
                        }
                        else
                        {
                            planningDistributionSheet.Cells["E" + eachPersonIndex].Value = "";
                        }
                        if (!string.IsNullOrEmpty(eachItem.CompanyName))
                        {
                            if (eachItem.CompanyName.ToLower() == "mw")
                            {
                                planningDistributionSheet.Cells["F" + eachPersonIndex].Value = eachItem.GradePoint;
                                planningDistributionSheet.Cells["F" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                planningDistributionSheet.Cells["F" + eachPersonIndex].AutoFitColumns();
                            }
                        }
                        
                        planningDistributionSheet.Cells["G" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.OctPoints);
                        planningDistributionSheet.Cells["G" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["G" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["G" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["H" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.NovPoints);
                        planningDistributionSheet.Cells["H" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["H" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["H" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["I" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.DecPoints);
                        planningDistributionSheet.Cells["I" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["I" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["I" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["J" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JanPoints);
                        planningDistributionSheet.Cells["J" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["J" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["J" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["K" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.FebPoints);
                        planningDistributionSheet.Cells["K" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["K" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["K" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["L" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MarPoints);
                        planningDistributionSheet.Cells["L" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["L" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["L" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["M" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AprPoints);
                        planningDistributionSheet.Cells["M" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["M" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["M" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["N" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MayPoints);
                        planningDistributionSheet.Cells["N" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["N" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["N" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["O" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JunPoints);
                        planningDistributionSheet.Cells["O" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["O" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["O" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["P" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JulPoints);
                        planningDistributionSheet.Cells["P" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["P" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["P" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["Q" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AugPoints);
                        planningDistributionSheet.Cells["Q" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["Q" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["Q" + eachPersonIndex].AutoFitColumns();

                        planningDistributionSheet.Cells["R" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.SepPoints);
                        planningDistributionSheet.Cells["R" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                        planningDistributionSheet.Cells["R" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        planningDistributionSheet.Cells["R" + eachPersonIndex].AutoFitColumns();

                        eachPersonIndex++;
                    }
                }
            }

            return planningDistributionSheet;
        }

        public ExcelWorksheet ExportDevDistributionExcelSheet(ExcelWorksheet devDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels,string year)
        {
            devDistributionSheet = GetPlanningAndDevelopmentDistributionExcelSheetHeader(devDistributionSheet);

            int countEachPerson = 2;
            List<DownloadApproveHistoryViewModal> objDevWiseDistributions = new List<DownloadApproveHistoryViewModal>();

            foreach (var item in forecastAssignmentViewModels)
            {
                var employeeName = item.EmployeeName;                
                var rootEmployeeName = item.RootEmployeeName;
                var employeeId = item.EmployeeId;
                var sectionName = item.SectionName;
                var sectionId = item.SectionId;
                var departmentName = item.DepartmentName;
                var departmentId = item.DepartmentId;
                var inChargeName = item.InchargeName;
                var roleName = item.RoleName;
                var explanationName = item.ExplanationName;
                var companyName = item.CompanyName;
                var gradePoints = item.GradePoint;
                var unitPrice = item.UnitPrice;
                var remarks = item.Remarks;

                var isDeleteRow = item.IsDeleteEmployee;
                var isAddRow = item.IsAddEmployee;
                var isUpdateCells = item.IsCellWiseUpdate;

                var octPOriginal = item.OctPoints;
                var novPOriginal = item.NovPoints;
                var decPOriginal = item.DecPoints;
                var janPOriginal = item.JanPoints;
                var febPOriginal = item.FebPoints;
                var marPOriginal = item.MarPoints;
                var aprPOriginal = item.AprPoints;
                var mayPOriginal = item.MayPoints;
                var junPOriginal = item.JunPoints;
                var julPOriginal = item.JulPoints;
                var augPOriginal = item.AugPoints;
                var sepPOriginal = item.SepPoints;
                if (!item.IsDeleteEmployee)
                {
                    if (countEachPerson == 2)
                    {
                        DownloadApproveHistoryViewModal eachPerson = new DownloadApproveHistoryViewModal();

                        eachPerson.EmployeeName = rootEmployeeName;
                        eachPerson.EmployeeId = employeeId;
                        eachPerson.SectionName = sectionName;
                        eachPerson.SectionId = sectionId;
                        eachPerson.DepartmentName = departmentName;
                        eachPerson.DepartmentId = departmentId;
                        eachPerson.CompanyName = companyName;
                        eachPerson.GradePoint = gradePoints;

                        eachPerson.OctPoints = Convert.ToDecimal(octPOriginal);
                        eachPerson.NovPoints = Convert.ToDecimal(novPOriginal);
                        eachPerson.DecPoints = Convert.ToDecimal(decPOriginal);
                        eachPerson.JanPoints = Convert.ToDecimal(janPOriginal);
                        eachPerson.FebPoints = Convert.ToDecimal(febPOriginal);
                        eachPerson.MarPoints = Convert.ToDecimal(marPOriginal);
                        eachPerson.AprPoints = Convert.ToDecimal(aprPOriginal);
                        eachPerson.MayPoints = Convert.ToDecimal(mayPOriginal);
                        eachPerson.JunPoints = Convert.ToDecimal(junPOriginal);
                        eachPerson.JulPoints = Convert.ToDecimal(julPOriginal);
                        eachPerson.AugPoints = Convert.ToDecimal(augPOriginal);
                        eachPerson.SepPoints = Convert.ToDecimal(sepPOriginal);

                        objDevWiseDistributions.Add(eachPerson);
                    }
                    else
                    {
                        bool isSamePersonForEachPerson = false;
                        bool isSameDepartment = false;
                        foreach (var eachPersonFromItem in objDevWiseDistributions)
                        {
                            if (eachPersonFromItem.EmployeeId == employeeId)
                            {
                                isSamePersonForEachPerson = true;                                
                                if (eachPersonFromItem.DepartmentId == departmentId && eachPersonFromItem.SectionId==sectionId)
                                {
                                    isSameDepartment = true;
                                    eachPersonFromItem.EmployeeName = rootEmployeeName;
                                    eachPersonFromItem.EmployeeId = employeeId;
                                    eachPersonFromItem.SectionName = sectionName;
                                    eachPersonFromItem.SectionId = sectionId;
                                    eachPersonFromItem.DepartmentName = departmentName;
                                    eachPersonFromItem.DepartmentId = departmentId;
                                    eachPersonFromItem.CompanyName = companyName;
                                    if (!string.IsNullOrEmpty(gradePoints))
                                    {
                                        if (!string.IsNullOrEmpty(eachPersonFromItem.GradePoint))
                                        {
                                            eachPersonFromItem.GradePoint = eachPersonFromItem.GradePoint + "," + gradePoints;
                                        }
                                        else
                                        {
                                            eachPersonFromItem.GradePoint = gradePoints;
                                        }
                                    }

                                    eachPersonFromItem.OctPoints = eachPersonFromItem.OctPoints + Convert.ToDecimal(octPOriginal);
                                    eachPersonFromItem.NovPoints = eachPersonFromItem.NovPoints + Convert.ToDecimal(novPOriginal);
                                    eachPersonFromItem.DecPoints = eachPersonFromItem.DecPoints + Convert.ToDecimal(decPOriginal);
                                    eachPersonFromItem.JanPoints = eachPersonFromItem.JanPoints + Convert.ToDecimal(janPOriginal);
                                    eachPersonFromItem.FebPoints = eachPersonFromItem.FebPoints + Convert.ToDecimal(febPOriginal);
                                    eachPersonFromItem.MarPoints = eachPersonFromItem.MarPoints + Convert.ToDecimal(marPOriginal);
                                    eachPersonFromItem.AprPoints = eachPersonFromItem.AprPoints + Convert.ToDecimal(aprPOriginal);
                                    eachPersonFromItem.MayPoints = eachPersonFromItem.MayPoints + Convert.ToDecimal(mayPOriginal);
                                    eachPersonFromItem.JunPoints = eachPersonFromItem.JunPoints + Convert.ToDecimal(junPOriginal);
                                    eachPersonFromItem.JulPoints = eachPersonFromItem.JulPoints + Convert.ToDecimal(julPOriginal);
                                    eachPersonFromItem.AugPoints = eachPersonFromItem.AugPoints + Convert.ToDecimal(augPOriginal);
                                    eachPersonFromItem.SepPoints = eachPersonFromItem.SepPoints + Convert.ToDecimal(sepPOriginal);
                                }                                                                                             
                            }
                        }

                        if (!isSamePersonForEachPerson || !isSameDepartment)
                        {
                            DownloadApproveHistoryViewModal eachPerson2 = new DownloadApproveHistoryViewModal();

                            eachPerson2.EmployeeName = rootEmployeeName;
                            eachPerson2.EmployeeId = employeeId;
                            eachPerson2.SectionName = sectionName;
                            eachPerson2.SectionId = sectionId;
                            eachPerson2.DepartmentName = departmentName;
                            eachPerson2.DepartmentId = departmentId;
                            eachPerson2.CompanyName = companyName;
                            eachPerson2.GradePoint = gradePoints;

                            eachPerson2.OctPoints = Convert.ToDecimal(octPOriginal);
                            eachPerson2.NovPoints = Convert.ToDecimal(novPOriginal);
                            eachPerson2.DecPoints = Convert.ToDecimal(decPOriginal);
                            eachPerson2.JanPoints = Convert.ToDecimal(janPOriginal);
                            eachPerson2.FebPoints = Convert.ToDecimal(febPOriginal);
                            eachPerson2.MarPoints = Convert.ToDecimal(marPOriginal);
                            eachPerson2.AprPoints = Convert.ToDecimal(aprPOriginal);
                            eachPerson2.MayPoints = Convert.ToDecimal(mayPOriginal);
                            eachPerson2.JunPoints = Convert.ToDecimal(junPOriginal);
                            eachPerson2.JulPoints = Convert.ToDecimal(julPOriginal);
                            eachPerson2.AugPoints = Convert.ToDecimal(augPOriginal);
                            eachPerson2.SepPoints = Convert.ToDecimal(sepPOriginal);

                            objDevWiseDistributions.Add(eachPerson2);
                        }
                    }
                }
                countEachPerson++;
            }


            if (objDevWiseDistributions.Count > 0)
            {
                //qc distribution : start
                List<DownloadApproveHistoryViewModal> objQCDistribution = new List<DownloadApproveHistoryViewModal>();                
                foreach (var qcItem in objDevWiseDistributions)
                {
                    var departmentName = qcItem.DepartmentName;
                    if(departmentName == "品証")
                    {
                        List<QaProportion> qaProportions = employeeAssignmentBLL.GetQAProportionsWithEmployee(qcItem.EmployeeId.ToString(), year);
                        if (qaProportions.Count > 0)
                        {
                            foreach(var qaProportionItem in qaProportions)
                            {
                                DownloadApproveHistoryViewModal downloadApproveHistoryViewModal = new DownloadApproveHistoryViewModal();
                                downloadApproveHistoryViewModal.EmployeeName = qcItem.EmployeeName;
                                downloadApproveHistoryViewModal.SectionName = qcItem.SectionName;
                                downloadApproveHistoryViewModal.DepartmentName = qaProportionItem.DepartmentName;
                                downloadApproveHistoryViewModal.CompanyName = qcItem.CompanyName;
                                downloadApproveHistoryViewModal.GradePoint = qcItem.GradePoint;

                                var tempOctPoints = (Convert.ToDouble(qcItem.OctPoints) * qaProportionItem.OctPercentage) / 100;
                                downloadApproveHistoryViewModal.OctPoints = Convert.ToDecimal(tempOctPoints);

                                var tempNovPoints = (Convert.ToDouble(qcItem.NovPoints) * qaProportionItem.NovPercentage) / 100;
                                downloadApproveHistoryViewModal.NovPoints = Convert.ToDecimal(tempNovPoints);

                                var tempDecPoints = (Convert.ToDouble(qcItem.DecPoints) * qaProportionItem.DecPercentage) / 100;
                                downloadApproveHistoryViewModal.DecPoints = Convert.ToDecimal(tempDecPoints);

                                var tempJanPoints = (Convert.ToDouble(qcItem.JanPoints) * qaProportionItem.JanPercentage) / 100;
                                downloadApproveHistoryViewModal.JanPoints = Convert.ToDecimal(tempJanPoints);

                                var tempFebPoints = (Convert.ToDouble(qcItem.FebPoints) * qaProportionItem.FebPercentage) / 100;
                                downloadApproveHistoryViewModal.FebPoints = Convert.ToDecimal(tempFebPoints);

                                var tempMarPoints = (Convert.ToDouble(qcItem.MarPoints) * qaProportionItem.MarPercentage) / 100;
                                downloadApproveHistoryViewModal.MarPoints = Convert.ToDecimal(tempMarPoints);

                                var tempAprPoints = (Convert.ToDouble(qcItem.AprPoints) * qaProportionItem.AprPercentage) / 100;
                                downloadApproveHistoryViewModal.AprPoints = Convert.ToDecimal(tempAprPoints);

                                var tempMayPoints = (Convert.ToDouble(qcItem.MayPoints) * qaProportionItem.MayPercentage) / 100;
                                downloadApproveHistoryViewModal.MayPoints = Convert.ToDecimal(tempMayPoints);

                                var tempJunPoints = (Convert.ToDouble(qcItem.JunPoints) * qaProportionItem.JunPercentage) / 100;
                                downloadApproveHistoryViewModal.JunPoints = Convert.ToDecimal(tempJunPoints);

                                var tempJulPoints = (Convert.ToDouble(qcItem.JulPoints) * qaProportionItem.JulPercentage) / 100;
                                downloadApproveHistoryViewModal.JulPoints = Convert.ToDecimal(tempJulPoints);

                                var tempAugPoints = (Convert.ToDouble(qcItem.AugPoints) * qaProportionItem.AugPercentage) / 100;
                                downloadApproveHistoryViewModal.AugPoints = Convert.ToDecimal(tempAugPoints);

                                var tempSepPoints = (Convert.ToDouble(qcItem.SepPoints) * qaProportionItem.SepPercentage) / 100;
                                downloadApproveHistoryViewModal.SepPoints = Convert.ToDecimal(tempSepPoints);

                                objQCDistribution.Add(downloadApproveHistoryViewModal);
                            }
                        }
                        else
                        {
                            objQCDistribution.Add(qcItem);
                        }
                    }
                    else
                    {
                        objQCDistribution.Add(qcItem);
                    }
                }
                //qc distribution: end

                int eachPersonIndex = 2;
                //foreach (var eachItem in objDevWiseDistributions)
                foreach (var eachItem in objQCDistribution)
                {
                    //Distributed sheet.
                    if (!string.IsNullOrEmpty(eachItem.SectionName)) { 
                        if (eachItem.SectionName.ToLower() == "開発")
                        {
                            devDistributionSheet.Cells["A" + eachPersonIndex].Value = eachItem.EmployeeName;
                            if (!string.IsNullOrEmpty(eachItem.CompanyName))
                            {
                                if (eachItem.CompanyName.ToLower() == "mw")
                                {
                                    devDistributionSheet.Cells["B" + eachPersonIndex].Value = "MW";
                                }
                                else
                                {
                                    devDistributionSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                                }
                            }
                            else
                            {
                                devDistributionSheet.Cells["B" + eachPersonIndex].Value = "業務委託(Outsourcing)";
                            }
                            devDistributionSheet.Cells["C" + eachPersonIndex].Value = "開発";

                            devDistributionSheet.Cells["D" + eachPersonIndex].Value = eachItem.DepartmentName;
                            if (!string.IsNullOrEmpty(eachItem.CompanyName))
                            {
                                if (eachItem.CompanyName.ToLower() == "mw")
                                {
                                    devDistributionSheet.Cells["E" + eachPersonIndex].Value = "";
                                }
                                else
                                {
                                    devDistributionSheet.Cells["E" + eachPersonIndex].Value = eachItem.CompanyName;
                                }
                            }
                            else
                            {
                                devDistributionSheet.Cells["E" + eachPersonIndex].Value = "";
                            }

                            if (!string.IsNullOrEmpty(eachItem.CompanyName))
                            {
                                if (eachItem.CompanyName.ToLower() == "mw")
                                {
                                    devDistributionSheet.Cells["F" + eachPersonIndex].Value = eachItem.GradePoint;
                                    devDistributionSheet.Cells["F" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    devDistributionSheet.Cells["F" + eachPersonIndex].AutoFitColumns();
                                }
                            }

                            devDistributionSheet.Cells["G" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.OctPoints);
                            devDistributionSheet.Cells["G" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["G" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["G" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["H" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.NovPoints);
                            devDistributionSheet.Cells["H" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["H" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["H" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["I" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.DecPoints);
                            devDistributionSheet.Cells["I" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["I" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["I" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["J" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JanPoints);
                            devDistributionSheet.Cells["J" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["J" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["J" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["K" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.FebPoints);
                            devDistributionSheet.Cells["K" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["K" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["K" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["L" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MarPoints);
                            devDistributionSheet.Cells["L" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["L" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["L" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["M" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AprPoints);
                            devDistributionSheet.Cells["M" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["M" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["M" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["N" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.MayPoints);
                            devDistributionSheet.Cells["N" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["N" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["N" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["O" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JunPoints);
                            devDistributionSheet.Cells["O" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["O" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["O" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["P" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.JulPoints);
                            devDistributionSheet.Cells["P" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["P" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["P" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["Q" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.AugPoints);
                            devDistributionSheet.Cells["Q" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["Q" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["Q" + eachPersonIndex].AutoFitColumns();

                            devDistributionSheet.Cells["R" + eachPersonIndex].Value = Convert.ToDecimal(eachItem.SepPoints);
                            devDistributionSheet.Cells["R" + eachPersonIndex].Style.Numberformat.Format = "0.00";
                            devDistributionSheet.Cells["R" + eachPersonIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            devDistributionSheet.Cells["R" + eachPersonIndex].AutoFitColumns();

                            eachPersonIndex++;
                        }
                    }
                }
            }

            return devDistributionSheet;
        }

        public string GetApprovedOrOriginalVlaue(string cellType, string approvedCells, string bCYRCellPending, int employeeAssignmentIdOrg, string cellValue)
        {
            string returnValue = "";
            bool isCellApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel(cellType, approvedCells);
            if (!isCellApproved)
            {
                if (cellType == "3")
                {
                    bool isSectionPending = IsCellPending("3", bCYRCellPending);
                    if (isSectionPending)
                    {
                        string originalSectionName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "sec.Name", "Name");
                        if (!string.IsNullOrEmpty(originalSectionName))
                        {
                            returnValue = originalSectionName;
                        }
                    }
                }
                else if (cellType == "4")
                {
                    bool isDepPending = IsCellPending("4", bCYRCellPending);
                    if (isDepPending)
                    {
                        string originalDepartmentName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "dep.Name", "Name");
                        if (!string.IsNullOrEmpty(originalDepartmentName))
                        {
                            returnValue = originalDepartmentName;
                        }
                    }
                }
                else if (cellType == "5")
                {
                    bool isInchargePending = IsCellPending("5", bCYRCellPending);
                    if (isInchargePending)
                    {
                        string originalInChargeName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "inc.Name", "Name");
                        if (!string.IsNullOrEmpty(originalInChargeName))
                        {
                            returnValue = originalInChargeName;
                        }
                    }
                }
                else if (cellType == "6")
                {
                    bool isRolePending = IsCellPending("6", bCYRCellPending);
                    if (isRolePending)
                    {
                        string originalRoleName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "rl.Name", "Name");
                        if (!string.IsNullOrEmpty(originalRoleName))
                        {
                            returnValue = originalRoleName;
                        }
                    }
                }
                else if (cellType == "7")
                {
                    bool isExplanationPending = IsCellPending("7", bCYRCellPending);
                    if (isExplanationPending)
                    {
                        string originalExpName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "e.Name", "Name");
                        if (!string.IsNullOrEmpty(originalExpName))
                        {
                            returnValue = originalExpName;
                        }
                    }
                }
                else if (cellType == "2")
                {
                    bool isRemarksPending = IsCellPending("2", bCYRCellPending);
                    if (isRemarksPending)
                    {
                        string originalRemarksName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.Remarks", "Remarks");
                        if (!string.IsNullOrEmpty(originalRemarksName))
                        {
                            returnValue = originalRemarksName;
                        }
                    }
                }
                else if (cellType == "8")
                {
                    bool isCompanyPending = IsCellPending("8", bCYRCellPending);
                    if (isCompanyPending)
                    {
                        string originalComName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "com.Name", "Name");
                        if (!string.IsNullOrEmpty(originalComName))
                        {
                            returnValue = originalComName;
                        }
                    }
                }
                else if (cellType == "9")
                {
                    bool isGradePending = IsCellPending("9", bCYRCellPending);
                    if (isGradePending)
                    {
                        string originalGradePointsName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "gd.GradePoints", "GradePoints");
                        if (!string.IsNullOrEmpty(originalGradePointsName))
                        {
                            returnValue = originalGradePointsName;
                        }
                    }
                }
                else if (cellType == "10")
                {
                    bool isUnitPending = IsCellPending("10", bCYRCellPending);
                    if (isUnitPending)
                    {
                        string originalUnitPrice = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.UnitPrice", "UnitPrice");
                        if (!string.IsNullOrEmpty(originalUnitPrice))
                        {
                            returnValue = originalUnitPrice;
                        }
                    }
                }
                else if (cellType == "11")
                {
                    bool isOctPending = IsCellPending("11", bCYRCellPending);
                    if (isOctPending)
                    {
                        decimal octOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "10");
                        returnValue = octOriginalP.ToString();
                    }
                }
                else if (cellType == "12")
                {
                    bool isNovPending = IsCellPending("12", bCYRCellPending);
                    if (isNovPending)
                    {
                        decimal novOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "11");
                        returnValue = novOriginalP.ToString();
                    }
                }
                else if (cellType == "13")
                {
                    bool isDecPending = IsCellPending("13", bCYRCellPending);
                    if (isDecPending)
                    {
                        decimal decOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "12");
                        returnValue = decOriginalP.ToString();
                    }
                }
                else if (cellType == "14")
                {
                    bool isJanPending = IsCellPending("14", bCYRCellPending);
                    if (isJanPending)
                    {
                        decimal janOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "1");
                        returnValue = janOriginalP.ToString();
                    }
                }
                else if (cellType == "15")
                {
                    bool isFebPending = IsCellPending("15", bCYRCellPending);
                    if (isFebPending)
                    {
                        decimal febOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "2");
                        returnValue = febOriginalP.ToString();
                    }
                }
                else if (cellType == "16")
                {
                    bool isMarPending = IsCellPending("16", bCYRCellPending);
                    if (isMarPending)
                    {
                        decimal marOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "3");
                        returnValue = marOriginalP.ToString();
                    }
                }
                else if (cellType == "17")
                {
                    bool isAprPending = IsCellPending("17", bCYRCellPending);
                    if (isAprPending)
                    {
                        decimal aprOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "4");
                        returnValue = aprOriginalP.ToString();
                    }
                }
                else if (cellType == "18")
                {
                    bool isMayPending = IsCellPending("18", bCYRCellPending);
                    if (isMayPending)
                    {
                        decimal mayOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "5");
                        returnValue = mayOriginalP.ToString();
                    }
                }
                else if (cellType == "19")
                {
                    bool isJunPending = IsCellPending("19", bCYRCellPending);
                    if (isJunPending)
                    {
                        decimal junOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "6");
                        returnValue = junOriginalP.ToString();
                    }
                }
                else if (cellType == "20")
                {
                    bool isJulPending = IsCellPending("20", bCYRCellPending);
                    if (isJulPending)
                    {
                        decimal julOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "7");
                        returnValue = julOriginalP.ToString();
                    }
                }
                else if (cellType == "21")
                {
                    bool isAugPending = IsCellPending("21", bCYRCellPending);
                    if (isAugPending)
                    {
                        decimal augOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "8");
                        returnValue = augOriginalP.ToString();
                    }
                }
                else if (cellType == "22")
                {
                    bool isSepPending = IsCellPending("21", bCYRCellPending);
                    if (isSepPending)
                    {
                        decimal sepOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "9");
                        returnValue = sepOriginalP.ToString();
                    }
                }
            }

            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = cellValue;
            }
            return returnValue;
        }
        public ExcelWorksheet SetApprovalColor(ExcelWorksheet sheet,int count)
        {
            sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            return sheet;
        }

        public ExcelWorksheet SetDeleteRowColor(ExcelWorksheet sheet,int count)
        {
            sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);

            return sheet;
        }
        public ExcelWorksheet SetCellWiseApprovalColor(ExcelWorksheet sheet,int count,string cellName)
        {
            sheet.Cells[""+ cellName + "" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["" + cellName + "" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

            return sheet;
        }
        public ExcelWorksheet GetOriginalExcelSheetHeader(ExcelWorksheet sheet)
        {
            sheet.Cells["A1"].Value = "区分(Section)	";
            sheet.Cells["A1"].Style.Font.Bold = true;
            sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["A1"].AutoFitColumns();

            sheet.Cells["B1"].Value = "部署(Dept)";
            sheet.Cells["B1"].Style.Font.Bold = true;
            sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["B1"].AutoFitColumns();

            sheet.Cells["C1"].Value = "担当作業(In chg)	";
            sheet.Cells["C1"].Style.Font.Bold = true;
            sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["C1"].AutoFitColumns();

            sheet.Cells["D1"].Value = "役割(Role)";
            sheet.Cells["D1"].Style.Font.Bold = true;
            sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["D1"].AutoFitColumns();

            sheet.Cells["E1"].Value = "説明(expl)";
            sheet.Cells["E1"].Style.Font.Bold = true;
            sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["E1"].AutoFitColumns();

            sheet.Cells["F1"].Value = "従業員名(Emp)";
            sheet.Cells["F1"].Style.Font.Bold = true; ;
            sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["F1"].AutoFitColumns();

            sheet.Cells["G1"].Value = "Remaks";
            sheet.Cells["G1"].Style.Font.Bold = true;
            sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["G1"].AutoFitColumns();

            sheet.Cells["H1"].Value = "会社(Com)	";
            sheet.Cells["H1"].Style.Font.Bold = true;
            sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["H1"].AutoFitColumns();

            sheet.Cells["I1"].Value = "グレード(Grade)";
            sheet.Cells["I1"].Style.Font.Bold = true;
            sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["I1"].AutoFitColumns();

            sheet.Cells["J1"].Value = "単価(Unit Price)	";
            sheet.Cells["J1"].Style.Font.Bold = true;
            sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["J1"].AutoFitColumns();


            sheet.Cells["K1"].Value = "10月";
            sheet.Cells["K1"].Style.Font.Bold = true;
            sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["K1"].AutoFitColumns();

            sheet.Cells["L1"].Value = "11月";
            sheet.Cells["L1"].Style.Font.Bold = true;
            sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["L1"].AutoFitColumns();

            sheet.Cells["M1"].Value = "12月";
            sheet.Cells["M1"].Style.Font.Bold = true;
            sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["M1"].AutoFitColumns();

            sheet.Cells["N1"].Value = "1月";
            sheet.Cells["N1"].Style.Font.Bold = true;
            sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["N1"].AutoFitColumns();

            sheet.Cells["O1"].Value = "2月";
            sheet.Cells["O1"].Style.Font.Bold = true;
            sheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["O1"].AutoFitColumns();

            sheet.Cells["P1"].Value = "3月";
            sheet.Cells["P1"].Style.Font.Bold = true;
            sheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["P1"].AutoFitColumns();

            sheet.Cells["Q1"].Value = "4月";
            sheet.Cells["Q1"].Style.Font.Bold = true;
            sheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["Q1"].AutoFitColumns();

            sheet.Cells["R1"].Value = "5月";
            sheet.Cells["R1"].Style.Font.Bold = true;
            sheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["R1"].AutoFitColumns();

            sheet.Cells["S1"].Value = "6月";
            sheet.Cells["S1"].Style.Font.Bold = true;
            sheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["S1"].AutoFitColumns();

            sheet.Cells["T1"].Value = "7月";
            sheet.Cells["T1"].Style.Font.Bold = true;
            sheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["T1"].AutoFitColumns();

            sheet.Cells["U1"].Value = "8月";
            sheet.Cells["U1"].Style.Font.Bold = true;
            sheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["U1"].AutoFitColumns();

            sheet.Cells["V1"].Value = "9月";
            sheet.Cells["V1"].Style.Font.Bold = true;
            sheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["V1"].AutoFitColumns();

            return sheet;
        }
        public ExcelWorksheet GetEachPersonExcelSheetHeader(ExcelWorksheet eachPersonSheet)
        {
            eachPersonSheet.Cells["A1"].Value = "氏名(Name)";
            eachPersonSheet.Cells["A1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["A1"].AutoFitColumns();

            eachPersonSheet.Cells["B1"].Value = "区分(Sec)";
            eachPersonSheet.Cells["B1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["B1"].AutoFitColumns();

            eachPersonSheet.Cells["C1"].Value = "企画/開発(Plan/Dev)";
            eachPersonSheet.Cells["C1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["C1"].AutoFitColumns();

            eachPersonSheet.Cells["D1"].Value = "会社(Com)";
            eachPersonSheet.Cells["D1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["D1"].AutoFitColumns();

            eachPersonSheet.Cells["E1"].Value = "グレード (Grade)";
            eachPersonSheet.Cells["E1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["E1"].AutoFitColumns();

            eachPersonSheet.Cells["F1"].Value = "10";
            eachPersonSheet.Cells["F1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["F1"].AutoFitColumns();

            eachPersonSheet.Cells["G1"].Value = "11";
            eachPersonSheet.Cells["G1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["G1"].AutoFitColumns();

            eachPersonSheet.Cells["H1"].Value = "12";
            eachPersonSheet.Cells["H1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["H1"].AutoFitColumns();

            eachPersonSheet.Cells["I1"].Value = "1";
            eachPersonSheet.Cells["I1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["I1"].AutoFitColumns();

            eachPersonSheet.Cells["J1"].Value = "2";
            eachPersonSheet.Cells["J1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["J1"].AutoFitColumns();

            eachPersonSheet.Cells["K1"].Value = "3";
            eachPersonSheet.Cells["K1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["K1"].AutoFitColumns();

            eachPersonSheet.Cells["L1"].Value = "4";
            eachPersonSheet.Cells["L1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["L1"].AutoFitColumns();

            eachPersonSheet.Cells["M1"].Value = "5";
            eachPersonSheet.Cells["M1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["M1"].AutoFitColumns();

            eachPersonSheet.Cells["N1"].Value = "6";
            eachPersonSheet.Cells["N1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["N1"].AutoFitColumns();

            eachPersonSheet.Cells["O1"].Value = "7";
            eachPersonSheet.Cells["O1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["O1"].AutoFitColumns();

            eachPersonSheet.Cells["P1"].Value = "8";
            eachPersonSheet.Cells["P1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["P1"].AutoFitColumns();

            eachPersonSheet.Cells["Q1"].Value = "9";
            eachPersonSheet.Cells["Q1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["Q1"].AutoFitColumns();

            return eachPersonSheet;
        }

        public ExcelWorksheet GetPlanningAndDevelopmentDistributionExcelSheetHeader(ExcelWorksheet eachPersonSheet)
        {
            eachPersonSheet.Cells["A1"].Value = "氏名(Name)";
            eachPersonSheet.Cells["A1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["A1"].AutoFitColumns();

            eachPersonSheet.Cells["B1"].Value = "区分(Sec)";
            eachPersonSheet.Cells["B1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["B1"].AutoFitColumns();

            eachPersonSheet.Cells["C1"].Value = "企画/開発(Plan/Dev)";
            eachPersonSheet.Cells["C1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["C1"].AutoFitColumns();

            eachPersonSheet.Cells["D1"].Value = "Department";
            eachPersonSheet.Cells["D1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["D1"].AutoFitColumns();

            eachPersonSheet.Cells["E1"].Value = "会社(Com)";
            eachPersonSheet.Cells["E1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["E1"].AutoFitColumns();

            eachPersonSheet.Cells["F1"].Value = "グレード (Grade)";
            eachPersonSheet.Cells["F1"].Style.Font.Bold = true; ;
            eachPersonSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["F1"].AutoFitColumns();

            eachPersonSheet.Cells["G1"].Value = "10";
            eachPersonSheet.Cells["G1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["G1"].AutoFitColumns();

            eachPersonSheet.Cells["H1"].Value = "11";
            eachPersonSheet.Cells["H1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["H1"].AutoFitColumns();

            eachPersonSheet.Cells["I1"].Value = "12";
            eachPersonSheet.Cells["I1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["I1"].AutoFitColumns();

            eachPersonSheet.Cells["J1"].Value = "1";
            eachPersonSheet.Cells["J1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["J1"].AutoFitColumns();

            eachPersonSheet.Cells["K1"].Value = "2";
            eachPersonSheet.Cells["K1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["K1"].AutoFitColumns();

            eachPersonSheet.Cells["L1"].Value = "3";
            eachPersonSheet.Cells["L1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["L1"].AutoFitColumns();

            eachPersonSheet.Cells["M1"].Value = "4";
            eachPersonSheet.Cells["M1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["M1"].AutoFitColumns();

            eachPersonSheet.Cells["N1"].Value = "5";
            eachPersonSheet.Cells["N1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["N1"].AutoFitColumns();

            eachPersonSheet.Cells["O1"].Value = "6";
            eachPersonSheet.Cells["O1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["O1"].AutoFitColumns();

            eachPersonSheet.Cells["P1"].Value = "7";
            eachPersonSheet.Cells["P1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["P1"].AutoFitColumns();

            eachPersonSheet.Cells["Q1"].Value = "8";
            eachPersonSheet.Cells["Q1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["Q1"].AutoFitColumns();

            eachPersonSheet.Cells["R1"].Value = "9";
            eachPersonSheet.Cells["R1"].Style.Font.Bold = true;
            eachPersonSheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            eachPersonSheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            eachPersonSheet.Cells["R1"].AutoFitColumns();

            return eachPersonSheet;
        }
        public bool IsCellPending(string cellNo,string bCYRCellPending)
        {
            bool returnValue = false;
            if (!string.IsNullOrEmpty(bCYRCellPending))
            {
                var arrCellPending = bCYRCellPending.Split(',');
                foreach(var pendingItem in arrCellPending)
                {
                    if (cellNo == pendingItem)
                    {
                        returnValue = true;
                    }
                }
            }            
            return returnValue;
        }
        public ExcelWorksheet ExportBudgetExcelSheet(ExcelWorksheet sheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {

            sheet = ExportBudgetExcelSheetHeader(sheet);

            int count = 2;
            foreach (var item in forecastAssignmentViewModels)
            {
                if (item.EmployeeAssignmentIdOrg == 112)
                {
                    //test
                }
                var assignmentId = item.Id;
                var employeeName = item.EmployeeName;
                var rootEmployeeName = item.RootEmployeeName;
                var employeeId = item.EmployeeId;
                var sectionName = item.SectionName;
                var departmentName = item.DepartmentName;
                var inChargeName = item.InchargeName;
                var roleName = item.RoleName;
                var explanationName = item.ExplanationName;
                var companyName = item.CompanyName;
                var gradePoints = item.GradePoint;
                var unitPrice = item.UnitPrice;
                var remarks = item.Remarks;                

                var octPOriginal = item.OctPoints;
                var novPOriginal = item.NovPoints;
                var decPOriginal = item.DecPoints;
                var janPOriginal = item.JanPoints;
                var febPOriginal = item.FebPoints;
                var marPOriginal = item.MarPoints;
                var aprPOriginal = item.AprPoints;
                var mayPOriginal = item.MayPoints;
                var junPOriginal = item.JunPoints;
                var julPOriginal = item.JulPoints;
                var augPOriginal = item.AugPoints;
                var sepPOriginal = item.SepPoints;

                var octTotal = Convert.ToDecimal(octPOriginal) * Convert.ToDecimal(unitPrice);//item.OctTotal;
                var novTotal = Convert.ToDecimal(novPOriginal) * Convert.ToDecimal(unitPrice);//item.NovTotal;
                var decTotal = Convert.ToDecimal(decPOriginal) * Convert.ToDecimal(unitPrice);//item.DecTotal;
                var janTotal = Convert.ToDecimal(janPOriginal) * Convert.ToDecimal(unitPrice);//item.JanTotal;
                var febTotal = Convert.ToDecimal(febPOriginal) * Convert.ToDecimal(unitPrice);//item.FebTotal;
                var marTotal = Convert.ToDecimal(marPOriginal) * Convert.ToDecimal(unitPrice);//item.MarTotal;
                var aprTotal = Convert.ToDecimal(aprPOriginal) * Convert.ToDecimal(unitPrice);//item.AprTotal;
                var mayTotal = Convert.ToDecimal(mayPOriginal) * Convert.ToDecimal(unitPrice);//item.MayTotal;
                var junTotal = Convert.ToDecimal(junPOriginal) * Convert.ToDecimal(unitPrice);//item.JunTotal;
                var julTotal = Convert.ToDecimal(julPOriginal) * Convert.ToDecimal(unitPrice);//item.JulTotal;
                var augTotal = Convert.ToDecimal(augPOriginal) * Convert.ToDecimal(unitPrice);//item.AugTotal;
                var septTotal = Convert.ToDecimal(sepPOriginal) * Convert.ToDecimal(unitPrice);//item.SepTotal;

                /********* Budget Assignment Data into excel sheet *********/
                sheet.Cells["A" + count].Value = employeeName;
                sheet.Cells["A" + count].AutoFitColumns();
                
                sheet.Cells["B" + count].Value = remarks;
                sheet.Cells["B" + count].AutoFitColumns();
                
                sheet.Cells["C" + count].Value = sectionName;
                sheet.Cells["C" + count].AutoFitColumns();
                
                sheet.Cells["D" + count].Value = departmentName;
                sheet.Cells["D" + count].AutoFitColumns();
                
                sheet.Cells["E" + count].Value = inChargeName;
                sheet.Cells["E" + count].AutoFitColumns();

                sheet.Cells["F" + count].Value = roleName;
                sheet.Cells["F" + count].AutoFitColumns();
                
                sheet.Cells["G" + count].Value = explanationName;
                sheet.Cells["G" + count].AutoFitColumns();
                
                sheet.Cells["H" + count].Value = companyName;
                sheet.Cells["H" + count].AutoFitColumns();
                
                sheet.Cells["I" + count].Value = gradePoints;
                sheet.Cells["I" + count].AutoFitColumns();
                
                sheet.Cells["J" + count].Value = unitPrice;
                sheet.Cells["J" + count].AutoFitColumns();

                /********* Budget Forecast Data into excel sheet *********/
                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                sheet.Cells["K" + count].AutoFitColumns();
                
                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                sheet.Cells["L" + count].AutoFitColumns();
                
                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                sheet.Cells["M" + count].AutoFitColumns();
                
                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                sheet.Cells["N" + count].AutoFitColumns();
                
                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                sheet.Cells["O" + count].AutoFitColumns();
                
                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                sheet.Cells["P" + count].AutoFitColumns();
                
                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                sheet.Cells["Q" + count].AutoFitColumns();
                
                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                sheet.Cells["R" + count].AutoFitColumns();
                
                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                sheet.Cells["S" + count].AutoFitColumns();

                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                sheet.Cells["T" + count].AutoFitColumns();

                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                sheet.Cells["U" + count].AutoFitColumns();

                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                sheet.Cells["V" + count].AutoFitColumns();

                /********* Budget Forecast costs into excel sheet *********/
                sheet.Cells["W" + count].Value = octTotal.ToString("N0"); ;                
                sheet.Cells["W" + count].AutoFitColumns();                

                sheet.Cells["X" + count].Value = novTotal.ToString("N0"); ;
                sheet.Cells["X" + count].AutoFitColumns();

                sheet.Cells["Y" + count].Value = decTotal.ToString("N0"); ;
                sheet.Cells["Y" + count].AutoFitColumns();

                sheet.Cells["Z" + count].Value = janTotal.ToString("N0"); ;
                sheet.Cells["Z" + count].AutoFitColumns();

                sheet.Cells["AA" + count].Value = febTotal.ToString("N0"); ;
                sheet.Cells["AA" + count].AutoFitColumns();

                sheet.Cells["AB" + count].Value = marTotal.ToString("N0"); ;
                sheet.Cells["AB" + count].AutoFitColumns();

                sheet.Cells["AC" + count].Value = aprTotal.ToString("N0"); ;
                sheet.Cells["AC" + count].AutoFitColumns();

                sheet.Cells["AD" + count].Value = mayTotal.ToString("N0"); ;
                sheet.Cells["AD" + count].AutoFitColumns();

                sheet.Cells["AE" + count].Value = junTotal.ToString("N0"); ;
                sheet.Cells["AE" + count].AutoFitColumns();

                sheet.Cells["AF" + count].Value = julTotal.ToString("N0"); ;
                sheet.Cells["AF" + count].AutoFitColumns();

                sheet.Cells["AG" + count].Value = augTotal.ToString("N0"); ;
                sheet.Cells["AG" + count].AutoFitColumns();

                sheet.Cells["AH" + count].Value = septTotal.ToString("N0"); ;
                sheet.Cells["AH" + count].AutoFitColumns();                

                count++;
            }
            return sheet;
        }
        public ExcelWorksheet ExportBudgetExcelSheetHeader(ExcelWorksheet sheet)
        {
            /******** Budget Assignment Header ********/
                sheet.Cells["A1"].Value = "要員(Employee)";
            sheet.Cells["A1"].Style.Font.Bold = true;
            sheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["A1"].AutoFitColumns();

            sheet.Cells["B1"].Value = "Remaks";
            sheet.Cells["B1"].Style.Font.Bold = true;
            sheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["B1"].AutoFitColumns();

            sheet.Cells["C1"].Value = "区分(Section)";
            sheet.Cells["C1"].Style.Font.Bold = true;
            sheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["C1"].AutoFitColumns();

            sheet.Cells["D1"].Value = "部署(Dept)";
            sheet.Cells["D1"].Style.Font.Bold = true;
            sheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["D1"].AutoFitColumns();

            sheet.Cells["E1"].Value = "担当作業(In chg)";
            sheet.Cells["E1"].Style.Font.Bold = true;
            sheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["E1"].AutoFitColumns();

            sheet.Cells["F1"].Value = "役割(Role)";
            sheet.Cells["F1"].Style.Font.Bold = true; ;
            sheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["F1"].AutoFitColumns();

            sheet.Cells["G1"].Value = "説明(Expl)";
            sheet.Cells["G1"].Style.Font.Bold = true;
            sheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["G1"].AutoFitColumns();

            sheet.Cells["H1"].Value = "会社(Com)	";
            sheet.Cells["H1"].Style.Font.Bold = true;
            sheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["H1"].AutoFitColumns();
            
            sheet.Cells["I1"].Value = "グレード(Grade)";
            sheet.Cells["I1"].Style.Font.Bold = true;
            sheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["I1"].AutoFitColumns();

            sheet.Cells["J1"].Value = "単価(Unit Price)	";
            sheet.Cells["J1"].Style.Font.Bold = true;
            sheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["J1"].AutoFitColumns();

            /******** Budget Man Month Header ********/
            sheet.Cells["K1"].Value = "10月";
            sheet.Cells["K1"].Style.Font.Bold = true;
            sheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["K1"].AutoFitColumns();

            sheet.Cells["L1"].Value = "11月";
            sheet.Cells["L1"].Style.Font.Bold = true;
            sheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["L1"].AutoFitColumns();

            sheet.Cells["M1"].Value = "12月";
            sheet.Cells["M1"].Style.Font.Bold = true;
            sheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["M1"].AutoFitColumns();

            sheet.Cells["N1"].Value = "1月";
            sheet.Cells["N1"].Style.Font.Bold = true;
            sheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["N1"].AutoFitColumns();

            sheet.Cells["O1"].Value = "2月";
            sheet.Cells["O1"].Style.Font.Bold = true;
            sheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["O1"].AutoFitColumns();

            sheet.Cells["P1"].Value = "3月";
            sheet.Cells["P1"].Style.Font.Bold = true;
            sheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["P1"].AutoFitColumns();

            sheet.Cells["Q1"].Value = "4月";
            sheet.Cells["Q1"].Style.Font.Bold = true;
            sheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["Q1"].AutoFitColumns();

            sheet.Cells["R1"].Value = "5月";
            sheet.Cells["R1"].Style.Font.Bold = true;
            sheet.Cells["R1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["R1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["R1"].AutoFitColumns();

            sheet.Cells["S1"].Value = "6月";
            sheet.Cells["S1"].Style.Font.Bold = true;
            sheet.Cells["S1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["S1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["S1"].AutoFitColumns();

            sheet.Cells["T1"].Value = "7月";
            sheet.Cells["T1"].Style.Font.Bold = true;
            sheet.Cells["T1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["T1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["T1"].AutoFitColumns();

            sheet.Cells["U1"].Value = "8月";
            sheet.Cells["U1"].Style.Font.Bold = true;
            sheet.Cells["U1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["U1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["U1"].AutoFitColumns();

            sheet.Cells["V1"].Value = "9月";
            sheet.Cells["V1"].Style.Font.Bold = true;
            sheet.Cells["V1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["V1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["V1"].AutoFitColumns();

            /******** Budget Costs Header ********/
            sheet.Cells["W1"].Value = "10月";
            sheet.Cells["W1"].Style.Font.Bold = true;
            sheet.Cells["W1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["W1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["W1"].AutoFitColumns();
            
            sheet.Cells["X1"].Value = "11月";
            sheet.Cells["X1"].Style.Font.Bold = true;
            sheet.Cells["X1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["X1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["X1"].AutoFitColumns();

            sheet.Cells["Y1"].Value = "12月";
            sheet.Cells["Y1"].Style.Font.Bold = true;
            sheet.Cells["Y1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Y1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["Y1"].AutoFitColumns();

            sheet.Cells["Z1"].Value = "1月";
            sheet.Cells["Z1"].Style.Font.Bold = true;
            sheet.Cells["Z1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["Z1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["Z1"].AutoFitColumns();

            sheet.Cells["AA1"].Value = "2月";
            sheet.Cells["AA1"].Style.Font.Bold = true;
            sheet.Cells["AA1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AA1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AA1"].AutoFitColumns();

            sheet.Cells["AB1"].Value = "3月";
            sheet.Cells["AB1"].Style.Font.Bold = true;
            sheet.Cells["AB1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AB1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AB1"].AutoFitColumns();

            sheet.Cells["AC1"].Value = "4月";
            sheet.Cells["AC1"].Style.Font.Bold = true;
            sheet.Cells["AC1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AC1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AC1"].AutoFitColumns();

            sheet.Cells["AD1"].Value = "5月";
            sheet.Cells["AD1"].Style.Font.Bold = true;
            sheet.Cells["AD1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AD1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AD1"].AutoFitColumns();

            sheet.Cells["AE1"].Value = "6月";
            sheet.Cells["AE1"].Style.Font.Bold = true;
            sheet.Cells["AE1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AE1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AE1"].AutoFitColumns();

            sheet.Cells["AF1"].Value = "7月";
            sheet.Cells["AF1"].Style.Font.Bold = true;
            sheet.Cells["AF1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AF1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AF1"].AutoFitColumns();

            sheet.Cells["AG1"].Value = "8月";
            sheet.Cells["AG1"].Style.Font.Bold = true;
            sheet.Cells["AG1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AG1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AG1"].AutoFitColumns();

            sheet.Cells["AH1"].Value = "9月";
            sheet.Cells["AH1"].Style.Font.Bold = true;
            sheet.Cells["AH1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells["AH1"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            sheet.Cells["AH1"].AutoFitColumns();
            return sheet;
        }

    }
}
 