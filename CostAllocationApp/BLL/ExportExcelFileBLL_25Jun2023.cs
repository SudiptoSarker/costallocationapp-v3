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

        public ExcelWorksheet ExportOriginalSheet(ExcelWorksheet sheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
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
                //else if (isDeleteRow)
                sheet.Cells["A" + count].Value = sectionName;
                sheet.Cells["A" + count].AutoFitColumns();
                if (isAddRow)
                {
                    sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                }
                else if (isDeleteRow)
                {
                    sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                }

                if (isAddRow)
                {
                    
                    
                    

                    sheet.Cells["B" + count].Value = departmentName;
                    sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["B" + count].AutoFitColumns();

                    sheet.Cells["C" + count].Value = inChargeName;
                    sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["C" + count].AutoFitColumns();

                    sheet.Cells["D" + count].Value = roleName;
                    sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["D" + count].AutoFitColumns();

                    sheet.Cells["E" + count].Value = explanationName;
                    sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["E" + count].AutoFitColumns();

                    if (!string.IsNullOrEmpty(employeeName))
                    {
                        sheet.Cells["F" + count].Value = employeeName;
                    }
                    else
                    {
                        sheet.Cells["F" + count].Value = rootEmployeeName;
                    }
                    sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["F" + count].AutoFitColumns();

                    sheet.Cells["G" + count].Value = remarks;
                    sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["G" + count].AutoFitColumns();

                    sheet.Cells["H" + count].Value = companyName;
                    sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["H" + count].AutoFitColumns();

                    sheet.Cells["I" + count].Value = gradePoints;
                    sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["I" + count].AutoFitColumns();

                    sheet.Cells["J" + count].Value = unitPrice;
                    sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["J" + count].AutoFitColumns();

                    sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                    sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["K" + count].AutoFitColumns();

                    sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                    sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["L" + count].AutoFitColumns();

                    sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                    sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["M" + count].AutoFitColumns();

                    sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                    sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["N" + count].AutoFitColumns();

                    sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                    sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["O" + count].AutoFitColumns();

                    sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                    sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["P" + count].AutoFitColumns();

                    sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                    sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["Q" + count].AutoFitColumns();

                    sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                    sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["R" + count].AutoFitColumns();

                    sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                    sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["S" + count].AutoFitColumns();

                    sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                    sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["T" + count].AutoFitColumns();

                    sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                    sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["U" + count].AutoFitColumns();

                    sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                    sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    sheet.Cells["V" + count].AutoFitColumns();
                }
                else if (isDeleteRow)
                {
                    sheet.Cells["A" + count].Value = sectionName;
                    sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["A" + count].AutoFitColumns();

                    sheet.Cells["B" + count].Value = departmentName;
                    sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["B" + count].AutoFitColumns();

                    sheet.Cells["C" + count].Value = inChargeName;
                    sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["C" + count].AutoFitColumns();

                    sheet.Cells["D" + count].Value = roleName;
                    sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["D" + count].AutoFitColumns();

                    sheet.Cells["E" + count].Value = explanationName;
                    sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["E" + count].AutoFitColumns();

                    if (!string.IsNullOrEmpty(employeeName))
                    {
                        sheet.Cells["F" + count].Value = employeeName;
                    }
                    else
                    {
                        sheet.Cells["F" + count].Value = rootEmployeeName;
                    }
                    sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["F" + count].AutoFitColumns();

                    sheet.Cells["G" + count].Value = remarks;
                    sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["G" + count].AutoFitColumns();

                    sheet.Cells["H" + count].Value = companyName;
                    sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["H" + count].AutoFitColumns();

                    sheet.Cells["I" + count].Value = gradePoints;
                    sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["I" + count].AutoFitColumns();

                    sheet.Cells["J" + count].Value = unitPrice;
                    sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["J" + count].AutoFitColumns();

                    sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                    sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["K" + count].AutoFitColumns();

                    sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                    sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["L" + count].AutoFitColumns();

                    sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                    sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["M" + count].AutoFitColumns();

                    sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                    sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["N" + count].AutoFitColumns();

                    sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                    sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["O" + count].AutoFitColumns();

                    sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                    sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["P" + count].AutoFitColumns();

                    sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                    sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["Q" + count].AutoFitColumns();

                    sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                    sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["R" + count].AutoFitColumns();

                    sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                    sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["S" + count].AutoFitColumns();

                    sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                    sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["T" + count].AutoFitColumns();

                    sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                    sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["U" + count].AutoFitColumns();

                    sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                    sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    sheet.Cells["V" + count].AutoFitColumns();
                }
                else if (!string.IsNullOrEmpty(approvedCells))
                {
                    bool isSectionApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("3", approvedCells);
                    if (isSectionApproved)
                    {
                        sheet.Cells["A" + count].Value = sectionName;
                        sheet.Cells["A" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["A" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["A" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("3") > 0)
                        {
                            string originalSectionName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "sec.Name", "Name");
                            if (!string.IsNullOrEmpty(originalSectionName))
                            {
                                sheet.Cells["A" + count].Value = originalSectionName;
                                sheet.Cells["A" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["A" + count].Value = sectionName;
                                sheet.Cells["A" + count].AutoFitColumns();
                            }
                        }
                        else
                        {
                            sheet.Cells["A" + count].Value = sectionName;
                            sheet.Cells["A" + count].AutoFitColumns();
                        }

                    }


                    bool isDeptApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("4", approvedCells);
                    if (isDeptApproved)
                    {
                        sheet.Cells["B" + count].Value = departmentName;
                        sheet.Cells["B" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["B" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["B" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("4") > 0)
                        {
                            string originalDepartmentName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "dep.Name", "Name");
                            if (!string.IsNullOrEmpty(originalDepartmentName))
                            {
                                sheet.Cells["B" + count].Value = originalDepartmentName;
                                sheet.Cells["B" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["B" + count].Value = departmentName;
                                sheet.Cells["B" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["B" + count].Value = departmentName;
                            sheet.Cells["B" + count].AutoFitColumns();
                        }
                    }

                    bool isInChargeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("5", approvedCells);
                    if (isInChargeApproved)
                    {
                        sheet.Cells["C" + count].Value = inChargeName;
                        sheet.Cells["C" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["C" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["C" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("5") > 0)
                        {
                            string originalInChargeName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "inc.Name", "Name");
                            if (!string.IsNullOrEmpty(originalInChargeName))
                            {
                                sheet.Cells["C" + count].Value = originalInChargeName;
                                sheet.Cells["C" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["C" + count].Value = inChargeName;
                                sheet.Cells["C" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["C" + count].Value = inChargeName;
                            sheet.Cells["C" + count].AutoFitColumns();
                        }
                    }

                    bool isRoleApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("6", approvedCells);
                    if (isRoleApproved)
                    {
                        sheet.Cells["D" + count].Value = roleName;
                        sheet.Cells["D" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["D" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["D" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("6") > 0)
                        {
                            string originalRoleName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "rl.Name", "Name");
                            if (!string.IsNullOrEmpty(originalRoleName))
                            {
                                sheet.Cells["D" + count].Value = originalRoleName;
                                sheet.Cells["D" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["D" + count].Value = roleName;
                                sheet.Cells["D" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["D" + count].Value = roleName;
                            sheet.Cells["D" + count].AutoFitColumns();
                        }

                    }


                    bool isExplanationApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("7", approvedCells);
                    if (isExplanationApproved)
                    {
                        sheet.Cells["E" + count].Value = explanationName;
                        sheet.Cells["E" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["E" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["E" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("7") > 0)
                        {
                            string originalExpName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "e.Name", "Name");
                            if (!string.IsNullOrEmpty(originalExpName))
                            {
                                sheet.Cells["E" + count].Value = originalExpName;
                                sheet.Cells["E" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["E" + count].Value = explanationName;
                                sheet.Cells["E" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["E" + count].Value = explanationName;
                            sheet.Cells["E" + count].AutoFitColumns();
                        }
                    }

                    bool isEmployeeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("1", approvedCells);
                    if (isEmployeeApproved)
                    {
                        if (!string.IsNullOrEmpty(employeeName))
                        {
                            sheet.Cells["F" + count].Value = employeeName;
                        }
                        else
                        {
                            sheet.Cells["F" + count].Value = rootEmployeeName;
                        }
                        sheet.Cells["F" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["F" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["F" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(employeeName))
                        {
                            sheet.Cells["F" + count].Value = employeeName;
                        }
                        else
                        {
                            sheet.Cells["F" + count].Value = rootEmployeeName;
                        }
                        sheet.Cells["F" + count].AutoFitColumns();
                    }

                    bool isRemarksApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("2", approvedCells);
                    if (isRemarksApproved)
                    {
                        sheet.Cells["G" + count].Value = remarks;
                        sheet.Cells["G" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["G" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["G" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("2") > 0)
                        {
                            string originalRemarksName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.Remarks", "Remarks");
                            if (!string.IsNullOrEmpty(originalRemarksName))
                            {
                                sheet.Cells["G" + count].Value = originalRemarksName;
                                sheet.Cells["G" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["G" + count].Value = remarks;
                                sheet.Cells["G" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["G" + count].Value = remarks;
                            sheet.Cells["G" + count].AutoFitColumns();
                        }
                    }


                    bool isCompanyApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("8", approvedCells);
                    if (isCompanyApproved)
                    {
                        sheet.Cells["H" + count].Value = companyName;
                        sheet.Cells["H" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["H" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["H" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("8") > 0)
                        {
                            string originalComName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "com.Name", "Name");
                            if (!string.IsNullOrEmpty(originalComName))
                            {
                                sheet.Cells["H" + count].Value = originalComName;
                                sheet.Cells["H" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["H" + count].Value = companyName;
                                sheet.Cells["H" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["H" + count].Value = companyName;
                            sheet.Cells["H" + count].AutoFitColumns();
                        }

                    }


                    bool isGradeApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("9", approvedCells);
                    if (isGradeApproved)
                    {
                        sheet.Cells["I" + count].Value = gradePoints;
                        sheet.Cells["I" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["I" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["I" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("9") > 0)
                        {
                            string originalGradePointsName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "gd.GradePoints", "GradePoints");
                            if (!string.IsNullOrEmpty(originalGradePointsName))
                            {
                                sheet.Cells["I" + count].Value = originalGradePointsName;
                                sheet.Cells["I" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["I" + count].Value = gradePoints;
                                sheet.Cells["I" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["I" + count].Value = gradePoints;
                            sheet.Cells["I" + count].AutoFitColumns();
                        }
                    }

                    bool isUnitApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("10", approvedCells);
                    if (isUnitApproved)
                    {
                        sheet.Cells["J" + count].Value = unitPrice;
                        sheet.Cells["J" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["J" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["J" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("10") > 0)
                        {
                            string originalUnitPrice = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.UnitPrice", "UnitPrice");
                            if (!string.IsNullOrEmpty(originalUnitPrice))
                            {
                                sheet.Cells["J" + count].Value = originalUnitPrice;
                                sheet.Cells["J" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["J" + count].Value = unitPrice;
                                sheet.Cells["J" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["J" + count].Value = unitPrice;
                            sheet.Cells["J" + count].AutoFitColumns();
                        }
                    }

                    bool isOctPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("11", approvedCells);
                    if (isOctPApproved)
                    {
                        sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                        sheet.Cells["K" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["K" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["K" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("11") > 0)
                        {
                            decimal octOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "10");
                            sheet.Cells["K" + count].Value = Convert.ToDecimal(octOriginalP).ToString("0.0");
                            sheet.Cells["K" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                            sheet.Cells["K" + count].AutoFitColumns();
                        }
                    }

                    bool isNovPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("12", approvedCells);
                    if (isNovPApproved)
                    {
                        sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                        sheet.Cells["L" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["L" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["L" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("12") > 0)
                        {
                            decimal novOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "11");
                            sheet.Cells["L" + count].Value = Convert.ToDecimal(novOriginalP).ToString("0.0");
                            sheet.Cells["L" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                            sheet.Cells["L" + count].AutoFitColumns();
                        }

                    }

                    bool isDecPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("13", approvedCells);
                    if (isDecPApproved)
                    {
                        sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                        sheet.Cells["M" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["M" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["M" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("13") > 0)
                        {
                            decimal decOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "12");
                            sheet.Cells["M" + count].Value = Convert.ToDecimal(decOriginalP).ToString("0.0");
                            sheet.Cells["M" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                            sheet.Cells["M" + count].AutoFitColumns();
                        }

                    }


                    bool isJanPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("14", approvedCells);
                    if (isJanPApproved)
                    {
                        sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                        sheet.Cells["N" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["N" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["N" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("14") > 0)
                        {
                            decimal janOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "1");
                            sheet.Cells["N" + count].Value = Convert.ToDecimal(janOriginalP).ToString("0.0");
                            sheet.Cells["N" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                            sheet.Cells["N" + count].AutoFitColumns();
                        }

                    }

                    bool isFebPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("15", approvedCells);
                    if (isFebPApproved)
                    {
                        sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                        sheet.Cells["O" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["O" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["O" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("15") > 0)
                        {
                            decimal febOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "2");
                            sheet.Cells["O" + count].Value = Convert.ToDecimal(febOriginalP).ToString("0.0");
                            sheet.Cells["O" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                            sheet.Cells["O" + count].AutoFitColumns();
                        }
                    }


                    bool isMarPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("16", approvedCells);
                    if (isMarPApproved)
                    {
                        sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                        sheet.Cells["P" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["P" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["P" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("16") > 0)
                        {
                            decimal marOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "3");
                            sheet.Cells["P" + count].Value = Convert.ToDecimal(marOriginalP).ToString("0.0");
                            sheet.Cells["P" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                            sheet.Cells["P" + count].AutoFitColumns();
                        }

                    }

                    bool isAprPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("17", approvedCells);
                    if (isAprPApproved)
                    {
                        sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                        sheet.Cells["Q" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["Q" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["Q" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("17") > 0)
                        {
                            decimal aprOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "4");
                            sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprOriginalP).ToString("0.0");
                            sheet.Cells["Q" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                            sheet.Cells["Q" + count].AutoFitColumns();
                        }

                    }

                    bool isMayPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("18", approvedCells);
                    if (isMayPApproved)
                    {
                        sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                        sheet.Cells["R" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["R" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["R" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("18") > 0)
                        {
                            decimal mayOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "5");
                            sheet.Cells["R" + count].Value = Convert.ToDecimal(mayOriginalP).ToString("0.0");
                            sheet.Cells["R" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                            sheet.Cells["R" + count].AutoFitColumns();
                        }

                    }


                    bool isJunPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("19", approvedCells);
                    if (isJunPApproved)
                    {
                        sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                        sheet.Cells["S" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["S" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["S" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("19") > 0)
                        {
                            decimal junOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "6");
                            sheet.Cells["S" + count].Value = Convert.ToDecimal(junOriginalP).ToString("0.0");
                            sheet.Cells["S" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                            sheet.Cells["S" + count].AutoFitColumns();
                        }

                    }

                    bool isJulPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("20", approvedCells);
                    if (isJulPApproved)
                    {
                        sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                        sheet.Cells["T" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["T" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["T" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("20") > 0)
                        {
                            decimal julOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "7");
                            sheet.Cells["T" + count].Value = Convert.ToDecimal(julOriginalP).ToString("0.0");
                            sheet.Cells["T" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                            sheet.Cells["T" + count].AutoFitColumns();
                        }

                    }

                    bool isAugPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("21", approvedCells);
                    if (isAugPApproved)
                    {
                        sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                        sheet.Cells["U" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["U" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["U" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("21") > 0)
                        {
                            decimal augOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "8");
                            sheet.Cells["U" + count].Value = Convert.ToDecimal(augOriginalP).ToString("0.0");
                            sheet.Cells["U" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                            sheet.Cells["U" + count].AutoFitColumns();
                        }

                    }

                    bool isSeptPApproved = employeeAssignmentBLL.IsApprovedCellsForDownloadExcel("22", approvedCells);
                    if (isSeptPApproved)
                    {
                        sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                        sheet.Cells["V" + count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells["V" + count].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                        sheet.Cells["V" + count].AutoFitColumns();
                    }
                    else
                    {
                        if (bCYRCellPending.IndexOf("22") > 0)
                        {
                            decimal sepOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "9");
                            sheet.Cells["V" + count].Value = Convert.ToDecimal(sepOriginalP).ToString("0.0");
                            sheet.Cells["V" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                            sheet.Cells["V" + count].AutoFitColumns();
                        }

                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(bCYRCellPending))
                    {
                        if (bCYRCellPending.IndexOf("3") > 0)
                        {
                            string originalSectionName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "sec.Name", "Name");
                            if (!string.IsNullOrEmpty(originalSectionName))
                            {
                                sheet.Cells["A" + count].Value = originalSectionName;
                                sheet.Cells["A" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["A" + count].Value = sectionName;
                                sheet.Cells["A" + count].AutoFitColumns();
                            }
                        }
                        else
                        {
                            sheet.Cells["A" + count].Value = sectionName;
                            sheet.Cells["A" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("4") > 0)
                        {
                            string originalDepartmentName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "dep.Name", "Name");
                            if (!string.IsNullOrEmpty(originalDepartmentName))
                            {
                                sheet.Cells["B" + count].Value = originalDepartmentName;
                                sheet.Cells["B" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["B" + count].Value = departmentName;
                                sheet.Cells["B" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["B" + count].Value = departmentName;
                            sheet.Cells["B" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("5") > 0)
                        {
                            string originalInChargeName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "inc.Name", "Name");
                            if (!string.IsNullOrEmpty(originalInChargeName))
                            {
                                sheet.Cells["C" + count].Value = originalInChargeName;
                                sheet.Cells["C" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["C" + count].Value = inChargeName;
                                sheet.Cells["C" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["C" + count].Value = inChargeName;
                            sheet.Cells["C" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("6") > 0)
                        {
                            string originalRoleName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "rl.Name", "Name");
                            if (!string.IsNullOrEmpty(originalRoleName))
                            {
                                sheet.Cells["D" + count].Value = originalRoleName;
                                sheet.Cells["D" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["D" + count].Value = roleName;
                                sheet.Cells["D" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["D" + count].Value = roleName;
                            sheet.Cells["D" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("7") > 0)
                        {
                            string originalExpName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "e.Name", "Name");
                            if (!string.IsNullOrEmpty(originalExpName))
                            {
                                sheet.Cells["E" + count].Value = originalExpName;
                                sheet.Cells["E" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["E" + count].Value = explanationName;
                                sheet.Cells["E" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["E" + count].Value = explanationName;
                            sheet.Cells["E" + count].AutoFitColumns();
                        }



                        if (!string.IsNullOrEmpty(employeeName))
                        {
                            sheet.Cells["F" + count].Value = employeeName;
                        }
                        else
                        {
                            sheet.Cells["F" + count].Value = rootEmployeeName;
                        }
                        sheet.Cells["F" + count].AutoFitColumns();

                        if (bCYRCellPending.IndexOf("2") > 0)
                        {
                            string originalRemarksName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.Remarks", "Remarks");
                            if (!string.IsNullOrEmpty(originalRemarksName))
                            {
                                sheet.Cells["G" + count].Value = originalRemarksName;
                                sheet.Cells["G" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["G" + count].Value = remarks;
                                sheet.Cells["G" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["G" + count].Value = remarks;
                            sheet.Cells["G" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("8") > 0)
                        {
                            string originalComName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "com.Name", "Name");
                            if (!string.IsNullOrEmpty(originalComName))
                            {
                                sheet.Cells["H" + count].Value = originalComName;
                                sheet.Cells["H" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["H" + count].Value = companyName;
                                sheet.Cells["H" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["H" + count].Value = companyName;
                            sheet.Cells["H" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("9") > 0)
                        {
                            string originalGradePointsName = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "gd.GradePoints", "GradePoints");
                            if (!string.IsNullOrEmpty(originalGradePointsName))
                            {
                                sheet.Cells["I" + count].Value = originalGradePointsName;
                                sheet.Cells["I" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["I" + count].Value = gradePoints;
                                sheet.Cells["I" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["I" + count].Value = gradePoints;
                            sheet.Cells["I" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("10") > 0)
                        {
                            string originalUnitPrice = employeeAssignmentBLL.GetOriginalDataForPendingCells(employeeAssignmentIdOrg, "ea.UnitPrice", "UnitPrice");
                            if (!string.IsNullOrEmpty(originalUnitPrice))
                            {
                                sheet.Cells["J" + count].Value = originalUnitPrice;
                                sheet.Cells["J" + count].AutoFitColumns();
                            }
                            else
                            {
                                sheet.Cells["J" + count].Value = unitPrice;
                                sheet.Cells["J" + count].AutoFitColumns();
                            }

                        }
                        else
                        {
                            sheet.Cells["J" + count].Value = unitPrice;
                            sheet.Cells["J" + count].AutoFitColumns();
                        }

                        if (bCYRCellPending.IndexOf("11") > 0)
                        {
                            decimal octOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "10");
                            sheet.Cells["K" + count].Value = Convert.ToDecimal(octOriginalP).ToString("0.0");
                            sheet.Cells["K" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                            sheet.Cells["K" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("12") > 0)
                        {
                            decimal novOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "11");
                            sheet.Cells["L" + count].Value = Convert.ToDecimal(novOriginalP).ToString("0.0");
                            sheet.Cells["L" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                            sheet.Cells["L" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("13") > 0)
                        {
                            decimal decOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "12");
                            sheet.Cells["M" + count].Value = Convert.ToDecimal(decOriginalP).ToString("0.0");
                            sheet.Cells["M" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                            sheet.Cells["M" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("14") > 0)
                        {
                            decimal janOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "1");
                            sheet.Cells["N" + count].Value = Convert.ToDecimal(janOriginalP).ToString("0.0");
                            sheet.Cells["N" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                            sheet.Cells["N" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("15") > 0)
                        {
                            decimal febOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "2");
                            sheet.Cells["O" + count].Value = Convert.ToDecimal(febOriginalP).ToString("0.0");
                            sheet.Cells["O" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                            sheet.Cells["O" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("16") > 0)
                        {
                            decimal marOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "3");
                            sheet.Cells["P" + count].Value = Convert.ToDecimal(marOriginalP).ToString("0.0");
                            sheet.Cells["P" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                            sheet.Cells["P" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("17") > 0)
                        {
                            decimal aprOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "4");
                            sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprOriginalP).ToString("0.0");
                            sheet.Cells["Q" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                            sheet.Cells["Q" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("18") > 0)
                        {
                            decimal mayOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "5");
                            sheet.Cells["R" + count].Value = Convert.ToDecimal(mayOriginalP).ToString("0.0");
                            sheet.Cells["R" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                            sheet.Cells["R" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("19") > 0)
                        {
                            decimal junOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "6");
                            sheet.Cells["S" + count].Value = Convert.ToDecimal(junOriginalP).ToString("0.0");
                            sheet.Cells["S" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                            sheet.Cells["S" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("20") > 0)
                        {
                            decimal julOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "7");
                            sheet.Cells["T" + count].Value = Convert.ToDecimal(julOriginalP).ToString("0.0");
                            sheet.Cells["T" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                            sheet.Cells["T" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("21") > 0)
                        {
                            decimal augOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "8");
                            sheet.Cells["U" + count].Value = Convert.ToDecimal(augOriginalP).ToString("0.0");
                            sheet.Cells["U" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                            sheet.Cells["U" + count].AutoFitColumns();
                        }
                        if (bCYRCellPending.IndexOf("22") > 0)
                        {
                            decimal sepOriginalP = employeeAssignmentBLL.GetMonthWiseOriginalForecastData(employeeAssignmentIdOrg, "9");
                            sheet.Cells["V" + count].Value = Convert.ToDecimal(sepOriginalP).ToString("0.0");
                            sheet.Cells["V" + count].AutoFitColumns();

                        }
                        else
                        {
                            sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                            sheet.Cells["V" + count].AutoFitColumns();
                        }
                    }
                    else
                    {
                        sheet.Cells["A" + count].Value = sectionName;
                        sheet.Cells["A" + count].AutoFitColumns();

                        sheet.Cells["B" + count].Value = departmentName;
                        sheet.Cells["B" + count].AutoFitColumns();

                        sheet.Cells["C" + count].Value = inChargeName;
                        sheet.Cells["C" + count].AutoFitColumns();

                        sheet.Cells["D" + count].Value = roleName;
                        sheet.Cells["D" + count].AutoFitColumns();

                        sheet.Cells["E" + count].Value = explanationName;
                        sheet.Cells["E" + count].AutoFitColumns();

                        if (!string.IsNullOrEmpty(employeeName))
                        {
                            sheet.Cells["F" + count].Value = employeeName;
                        }
                        else
                        {
                            sheet.Cells["F" + count].Value = rootEmployeeName;
                        }
                        sheet.Cells["F" + count].AutoFitColumns();

                        sheet.Cells["G" + count].Value = remarks;
                        sheet.Cells["G" + count].AutoFitColumns();

                        sheet.Cells["H" + count].Value = companyName;
                        sheet.Cells["H" + count].AutoFitColumns();

                        sheet.Cells["I" + count].Value = gradePoints;
                        sheet.Cells["I" + count].AutoFitColumns();

                        sheet.Cells["J" + count].Value = unitPrice;
                        sheet.Cells["J" + count].AutoFitColumns();

                        sheet.Cells["K" + count].Value = Convert.ToDecimal(octPOriginal).ToString("0.0");
                        sheet.Cells["L" + count].Value = Convert.ToDecimal(novPOriginal).ToString("0.0");
                        sheet.Cells["M" + count].Value = Convert.ToDecimal(decPOriginal).ToString("0.0");
                        sheet.Cells["N" + count].Value = Convert.ToDecimal(janPOriginal).ToString("0.0");
                        sheet.Cells["O" + count].Value = Convert.ToDecimal(febPOriginal).ToString("0.0");
                        sheet.Cells["P" + count].Value = Convert.ToDecimal(marPOriginal).ToString("0.0");
                        sheet.Cells["Q" + count].Value = Convert.ToDecimal(aprPOriginal).ToString("0.0");
                        sheet.Cells["R" + count].Value = Convert.ToDecimal(mayPOriginal).ToString("0.0");
                        sheet.Cells["S" + count].Value = Convert.ToDecimal(junPOriginal).ToString("0.0");
                        sheet.Cells["T" + count].Value = Convert.ToDecimal(julPOriginal).ToString("0.0");
                        sheet.Cells["U" + count].Value = Convert.ToDecimal(augPOriginal).ToString("0.0");
                        sheet.Cells["V" + count].Value = Convert.ToDecimal(sepPOriginal).ToString("0.0");
                    }
                }

                count++;
            }
            return sheet;
        }
        public ExcelWorksheet ExportEachPersonSheet(ExcelWorksheet eachPersonSheet,List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
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

        public ExcelWorksheet ExportPlanningDistributionSheet(ExcelWorksheet planningDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {
            planningDistributionSheet.Cells["A1"].Value = "氏名(Name)";
            planningDistributionSheet.Cells["A1"].Style.Font.Bold = true; ;
            planningDistributionSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["A1"].AutoFitColumns();

            planningDistributionSheet.Cells["B1"].Value = "区分(Sec)";
            planningDistributionSheet.Cells["B1"].Style.Font.Bold = true; ;
            planningDistributionSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["B1"].AutoFitColumns();

            planningDistributionSheet.Cells["C1"].Value = "企画/開発(Plan/Dev)";
            planningDistributionSheet.Cells["C1"].Style.Font.Bold = true; ;
            planningDistributionSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["C1"].AutoFitColumns();

            planningDistributionSheet.Cells["D1"].Value = "会社(Com)";
            planningDistributionSheet.Cells["D1"].Style.Font.Bold = true; ;
            planningDistributionSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["D1"].AutoFitColumns();

            planningDistributionSheet.Cells["E1"].Value = "グレード (Grade)";
            planningDistributionSheet.Cells["E1"].Style.Font.Bold = true; ;
            planningDistributionSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["E1"].AutoFitColumns();

            planningDistributionSheet.Cells["F1"].Value = "10";
            planningDistributionSheet.Cells["F1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["F1"].AutoFitColumns();

            planningDistributionSheet.Cells["G1"].Value = "11";
            planningDistributionSheet.Cells["G1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["G1"].AutoFitColumns();

            planningDistributionSheet.Cells["H1"].Value = "12";
            planningDistributionSheet.Cells["H1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["H1"].AutoFitColumns();

            planningDistributionSheet.Cells["I1"].Value = "1";
            planningDistributionSheet.Cells["I1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["I1"].AutoFitColumns();

            planningDistributionSheet.Cells["J1"].Value = "2";
            planningDistributionSheet.Cells["J1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["J1"].AutoFitColumns();

            planningDistributionSheet.Cells["K1"].Value = "3";
            planningDistributionSheet.Cells["K1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["K1"].AutoFitColumns();

            planningDistributionSheet.Cells["L1"].Value = "4";
            planningDistributionSheet.Cells["L1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["L1"].AutoFitColumns();

            planningDistributionSheet.Cells["M1"].Value = "5";
            planningDistributionSheet.Cells["M1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["M1"].AutoFitColumns();

            planningDistributionSheet.Cells["N1"].Value = "6";
            planningDistributionSheet.Cells["N1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["N1"].AutoFitColumns();

            planningDistributionSheet.Cells["O1"].Value = "7";
            planningDistributionSheet.Cells["O1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["O1"].AutoFitColumns();

            planningDistributionSheet.Cells["P1"].Value = "8";
            planningDistributionSheet.Cells["P1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["P1"].AutoFitColumns();

            planningDistributionSheet.Cells["Q1"].Value = "9";
            planningDistributionSheet.Cells["Q1"].Style.Font.Bold = true;
            planningDistributionSheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            planningDistributionSheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            planningDistributionSheet.Cells["Q1"].AutoFitColumns();

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
                        if (!string.IsNullOrEmpty(eachItem.CompanyName))
                        {
                            if (eachItem.CompanyName.ToLower() == "mw")
                            {
                                planningDistributionSheet.Cells["D" + eachPersonIndex].Value = "";
                            }
                            else
                            {
                                planningDistributionSheet.Cells["D" + eachPersonIndex].Value = eachItem.CompanyName;
                            }
                        }
                        else
                        {
                            planningDistributionSheet.Cells["D" + eachPersonIndex].Value = "";
                        }
                        if (!string.IsNullOrEmpty(eachItem.CompanyName))
                        {
                            if (eachItem.CompanyName.ToLower() == "mw")
                            {
                                planningDistributionSheet.Cells["E" + eachPersonIndex].Value = eachItem.GradePoint;
                            }
                        }

                        planningDistributionSheet.Cells["F" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                        planningDistributionSheet.Cells["G" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                        planningDistributionSheet.Cells["H" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                        planningDistributionSheet.Cells["I" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                        planningDistributionSheet.Cells["J" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                        planningDistributionSheet.Cells["K" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                        planningDistributionSheet.Cells["L" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                        planningDistributionSheet.Cells["M" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                        planningDistributionSheet.Cells["N" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                        planningDistributionSheet.Cells["O" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                        planningDistributionSheet.Cells["P" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                        planningDistributionSheet.Cells["Q" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

                        eachPersonIndex++;
                    }
                }
            }

            return planningDistributionSheet;
        }

        public ExcelWorksheet ExportDevDistributionSheet(ExcelWorksheet devDistributionSheet, List<ForecastAssignmentViewModel> forecastAssignmentViewModels)
        {
            devDistributionSheet.Cells["A1"].Value = "氏名(Name)";
            devDistributionSheet.Cells["A1"].Style.Font.Bold = true; ;
            devDistributionSheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["A1"].AutoFitColumns();

            devDistributionSheet.Cells["B1"].Value = "区分(Sec)";
            devDistributionSheet.Cells["B1"].Style.Font.Bold = true; ;
            devDistributionSheet.Cells["B1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["B1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["B1"].AutoFitColumns();

            devDistributionSheet.Cells["C1"].Value = "企画/開発(Plan/Dev)";
            devDistributionSheet.Cells["C1"].Style.Font.Bold = true; ;
            devDistributionSheet.Cells["C1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["C1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["C1"].AutoFitColumns();

            devDistributionSheet.Cells["D1"].Value = "会社(Com)";
            devDistributionSheet.Cells["D1"].Style.Font.Bold = true; ;
            devDistributionSheet.Cells["D1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["D1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["D1"].AutoFitColumns();

            devDistributionSheet.Cells["E1"].Value = "グレード (Grade)";
            devDistributionSheet.Cells["E1"].Style.Font.Bold = true; ;
            devDistributionSheet.Cells["E1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["E1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["E1"].AutoFitColumns();

            devDistributionSheet.Cells["F1"].Value = "10";
            devDistributionSheet.Cells["F1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["F1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["F1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["F1"].AutoFitColumns();

            devDistributionSheet.Cells["G1"].Value = "11";
            devDistributionSheet.Cells["G1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["G1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["G1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["G1"].AutoFitColumns();

            devDistributionSheet.Cells["H1"].Value = "12";
            devDistributionSheet.Cells["H1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["H1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["H1"].AutoFitColumns();

            devDistributionSheet.Cells["I1"].Value = "1";
            devDistributionSheet.Cells["I1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["I1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["I1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["I1"].AutoFitColumns();

            devDistributionSheet.Cells["J1"].Value = "2";
            devDistributionSheet.Cells["J1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["J1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["J1"].AutoFitColumns();

            devDistributionSheet.Cells["K1"].Value = "3";
            devDistributionSheet.Cells["K1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["K1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["K1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["K1"].AutoFitColumns();

            devDistributionSheet.Cells["L1"].Value = "4";
            devDistributionSheet.Cells["L1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["L1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["L1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["L1"].AutoFitColumns();

            devDistributionSheet.Cells["M1"].Value = "5";
            devDistributionSheet.Cells["M1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["M1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["M1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["M1"].AutoFitColumns();

            devDistributionSheet.Cells["N1"].Value = "6";
            devDistributionSheet.Cells["N1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["N1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["N1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["N1"].AutoFitColumns();

            devDistributionSheet.Cells["O1"].Value = "7";
            devDistributionSheet.Cells["O1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["O1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["O1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["O1"].AutoFitColumns();

            devDistributionSheet.Cells["P1"].Value = "8";
            devDistributionSheet.Cells["P1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["P1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["P1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["P1"].AutoFitColumns();

            devDistributionSheet.Cells["Q1"].Value = "9";
            devDistributionSheet.Cells["Q1"].Style.Font.Bold = true;
            devDistributionSheet.Cells["Q1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            devDistributionSheet.Cells["Q1"].Style.Fill.BackgroundColor.SetColor(Color.Orange);
            devDistributionSheet.Cells["Q1"].AutoFitColumns();

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

                            if (!string.IsNullOrEmpty(eachItem.CompanyName))
                            {
                                if (eachItem.CompanyName.ToLower() == "mw")
                                {
                                    devDistributionSheet.Cells["D" + eachPersonIndex].Value = "";
                                }
                                else
                                {
                                    devDistributionSheet.Cells["D" + eachPersonIndex].Value = eachItem.CompanyName;
                                }
                            }
                            else
                            {
                                devDistributionSheet.Cells["D" + eachPersonIndex].Value = "";
                            }

                            if (!string.IsNullOrEmpty(eachItem.CompanyName))
                            {
                                if (eachItem.CompanyName.ToLower() == "mw")
                                {
                                    devDistributionSheet.Cells["E" + eachPersonIndex].Value = eachItem.GradePoint;
                                }
                            }

                            devDistributionSheet.Cells["F" + eachPersonIndex].Value = eachItem.OctPoints.ToString("0.0");
                            devDistributionSheet.Cells["G" + eachPersonIndex].Value = eachItem.NovPoints.ToString("0.0");
                            devDistributionSheet.Cells["H" + eachPersonIndex].Value = eachItem.DecPoints.ToString("0.0");
                            devDistributionSheet.Cells["I" + eachPersonIndex].Value = eachItem.JanPoints.ToString("0.0");
                            devDistributionSheet.Cells["J" + eachPersonIndex].Value = eachItem.FebPoints.ToString("0.0");
                            devDistributionSheet.Cells["K" + eachPersonIndex].Value = eachItem.MarPoints.ToString("0.0");
                            devDistributionSheet.Cells["L" + eachPersonIndex].Value = eachItem.AprPoints.ToString("0.0");
                            devDistributionSheet.Cells["M" + eachPersonIndex].Value = eachItem.MayPoints.ToString("0.0");
                            devDistributionSheet.Cells["N" + eachPersonIndex].Value = eachItem.JunPoints.ToString("0.0");
                            devDistributionSheet.Cells["O" + eachPersonIndex].Value = eachItem.JulPoints.ToString("0.0");
                            devDistributionSheet.Cells["P" + eachPersonIndex].Value = eachItem.AugPoints.ToString("0.0");
                            devDistributionSheet.Cells["Q" + eachPersonIndex].Value = eachItem.SepPoints.ToString("0.0");

                            eachPersonIndex++;
                        }
                    }
                }
            }

            return devDistributionSheet;
        }

    }
}