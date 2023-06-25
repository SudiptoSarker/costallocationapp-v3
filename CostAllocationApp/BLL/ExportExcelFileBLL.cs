using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
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
                sheet.Cells["I" + count].AutoFitColumns();

                string unitPriceReceived = GetApprovedOrOriginalVlaue("10", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, unitPrice);
                sheet.Cells["J" + count].Value = unitPriceReceived;
                sheet.Cells["J" + count].AutoFitColumns();

                string octPOriginalReceived = GetApprovedOrOriginalVlaue("11", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, octPOriginal);
                sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginalReceived).ToString("0.0");
                sheet.Cells["K" + count].AutoFitColumns();

                string novPOriginalReceived = GetApprovedOrOriginalVlaue("12", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, novPOriginal);
                sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginalReceived).ToString("0.0");
                sheet.Cells["L" + count].AutoFitColumns();

                string decPOriginalReceived = GetApprovedOrOriginalVlaue("13", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, decPOriginal);
                sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginalReceived).ToString("0.0");
                sheet.Cells["M" + count].AutoFitColumns();

                string janPOriginalReceived = GetApprovedOrOriginalVlaue("14", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, janPOriginal);
                sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginalReceived).ToString("0.0");
                sheet.Cells["N" + count].AutoFitColumns();

                string febPOriginalReceived = GetApprovedOrOriginalVlaue("15", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, febPOriginal);
                sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginalReceived).ToString("0.0");
                sheet.Cells["O" + count].AutoFitColumns();

                string marPOriginalReceived = GetApprovedOrOriginalVlaue("16", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, marPOriginal);
                sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginalReceived).ToString("0.0");
                sheet.Cells["P" + count].AutoFitColumns();

                string aprPOriginalReceived = GetApprovedOrOriginalVlaue("17", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, aprPOriginal);
                sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginalReceived).ToString("0.0");
                sheet.Cells["Q" + count].AutoFitColumns();

                string mayPOriginalReceived = GetApprovedOrOriginalVlaue("18", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, mayPOriginal);
                sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginalReceived).ToString("0.0");
                sheet.Cells["R" + count].AutoFitColumns();

                string junPOriginalReceived = GetApprovedOrOriginalVlaue("19", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, junPOriginal);
                sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginalReceived).ToString("0.0");
                sheet.Cells["S" + count].AutoFitColumns();

                string julPOriginalReceived = GetApprovedOrOriginalVlaue("20", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, julPOriginal);
                sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginalReceived).ToString("0.0");
                sheet.Cells["T" + count].AutoFitColumns();

                string augPOriginalReceived = GetApprovedOrOriginalVlaue("21", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, augPOriginal);
                sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginalReceived).ToString("0.0");
                sheet.Cells["U" + count].AutoFitColumns();

                string sepPOriginalReceived = GetApprovedOrOriginalVlaue("22", approvedCells, bCYRCellPending, employeeAssignmentIdOrg, sepPOriginal);
                sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginalReceived).ToString("0.0");
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
                        }
                    }

                    eachPersonSheet.Cells["F" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                    eachPersonSheet.Cells["G" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                    eachPersonSheet.Cells["H" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                    eachPersonSheet.Cells["I" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                    eachPersonSheet.Cells["J" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                    eachPersonSheet.Cells["K" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                    eachPersonSheet.Cells["L" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                    eachPersonSheet.Cells["M" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                    eachPersonSheet.Cells["N" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                    eachPersonSheet.Cells["O" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                    eachPersonSheet.Cells["P" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                    eachPersonSheet.Cells["Q" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

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
                                    distributedWorksheet.Cells["D" + distributedCountDistinct].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["E" + distributedCountDistinct].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["F" + distributedCountDistinct].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["G" + distributedCountDistinct].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["H" + distributedCountDistinct].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["I" + distributedCountDistinct].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["J" + distributedCountDistinct].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["K" + distributedCountDistinct].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["L" + distributedCountDistinct].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["M" + distributedCountDistinct].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                                    distributedWorksheet.Cells["N" + distributedCountDistinct].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");

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
                                distributedWorksheet.Cells["G" + distributedCountDistinct].Value = Convert.ToDecimal(distributedItem.FebPoints).ToString("0.0");
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

        public ExcelWorksheet ExportPlanningDistributionExcelSheet(ExcelWorksheet planningDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
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
                int eachPersonIndex = 2;
                foreach (var eachItem in objEachPersonList)
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
                            }
                        }

                        planningDistributionSheet.Cells["G" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                        planningDistributionSheet.Cells["H" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                        planningDistributionSheet.Cells["I" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                        planningDistributionSheet.Cells["J" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                        planningDistributionSheet.Cells["K" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                        planningDistributionSheet.Cells["L" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                        planningDistributionSheet.Cells["M" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                        planningDistributionSheet.Cells["N" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                        planningDistributionSheet.Cells["O" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                        planningDistributionSheet.Cells["P" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                        planningDistributionSheet.Cells["Q" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                        planningDistributionSheet.Cells["R" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

                        eachPersonIndex++;
                    }
                }
            }

            return planningDistributionSheet;
        }

        public ExcelWorksheet ExportDevDistributionExcelSheet(ExcelWorksheet devDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {
            devDistributionSheet = GetPlanningAndDevelopmentDistributionExcelSheetHeader(devDistributionSheet);

            int countEachPerson = 2;
            List<DownloadApproveHistoryViewModal> objDevWiseDistributions = new List<DownloadApproveHistoryViewModal>();

            foreach (var item in forecastAssignmentViewModels)
            {
                var employeeName = item.EmployeeName;
                if(employeeName== "sudipto test01")
                {
                    //test
                }
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
                int eachPersonIndex = 2;
                foreach (var eachItem in objDevWiseDistributions)
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
                                }
                            }

                            devDistributionSheet.Cells["G" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                            devDistributionSheet.Cells["H" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                            devDistributionSheet.Cells["I" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                            devDistributionSheet.Cells["J" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                            devDistributionSheet.Cells["K" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                            devDistributionSheet.Cells["L" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                            devDistributionSheet.Cells["M" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                            devDistributionSheet.Cells["N" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                            devDistributionSheet.Cells["O" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                            devDistributionSheet.Cells["P" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                            devDistributionSheet.Cells["Q" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                            devDistributionSheet.Cells["R" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

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
                    if (bCYRCellPending.IndexOf("3") > 0)
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
                    if (bCYRCellPending.IndexOf("4") > 0)
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
                    if (bCYRCellPending.IndexOf("5") > 0)
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
                    if (bCYRCellPending.IndexOf("6") > 0)
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
                    if (bCYRCellPending.IndexOf("7") > 0)
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
                    if (bCYRCellPending.IndexOf("2") > 0)
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
                    if (bCYRCellPending.IndexOf("8") > 0)
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
                    if (bCYRCellPending.IndexOf("9") > 0)
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
                    if (bCYRCellPending.IndexOf("10") > 0)
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
                    if (bCYRCellPending.IndexOf("11") > 0)
                    {
                        decimal octOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "10");
                        returnValue = octOriginalP.ToString();
                    }
                }
                else if (cellType == "12")
                {
                    if (bCYRCellPending.IndexOf("12") > 0)
                    {
                        decimal novOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "11");
                        returnValue = novOriginalP.ToString();
                    }
                }
                else if (cellType == "13")
                {
                    if (bCYRCellPending.IndexOf("13") > 0)
                    {
                        decimal decOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "12");
                        returnValue = decOriginalP.ToString();
                    }
                }
                else if (cellType == "14")
                {
                    if (bCYRCellPending.IndexOf("14") > 0)
                    {
                        decimal janOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "1");
                        returnValue = janOriginalP.ToString();
                    }
                }
                else if (cellType == "15")
                {
                    if (bCYRCellPending.IndexOf("15") > 0)
                    {
                        decimal febOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "2");
                        returnValue = febOriginalP.ToString();
                    }
                }
                else if (cellType == "16")
                {
                    if (bCYRCellPending.IndexOf("16") > 0)
                    {
                        decimal marOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "3");
                        returnValue = marOriginalP.ToString();
                    }
                }
                else if (cellType == "17")
                {
                    if (bCYRCellPending.IndexOf("17") > 0)
                    {
                        decimal aprOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "4");
                        returnValue = aprOriginalP.ToString();
                    }
                }
                else if (cellType == "18")
                {
                    if (bCYRCellPending.IndexOf("18") > 0)
                    {
                        decimal mayOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "5");
                        returnValue = mayOriginalP.ToString();
                    }
                }
                else if (cellType == "19")
                {
                    if (bCYRCellPending.IndexOf("19") > 0)
                    {
                        decimal junOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "6");
                        returnValue = junOriginalP.ToString();
                    }
                }
                else if (cellType == "20")
                {
                    if (bCYRCellPending.IndexOf("20") > 0)
                    {
                        decimal julOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "7");
                        returnValue = julOriginalP.ToString();
                    }
                }
                else if (cellType == "21")
                {
                    if (bCYRCellPending.IndexOf("21") > 0)
                    {
                        decimal augOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "8");
                        returnValue = augOriginalP.ToString();
                    }
                }
                else if (cellType == "22")
                {
                    if (bCYRCellPending.IndexOf("21") > 0)
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

    }
}