using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CostAllocationApp.BLL
{
    public class ExportExcelFileBLL
    {
        public ExcelWorksheet ExportEachPerson()
        {
            //var package = new ExcelPackage();
            ExcelPackage package = new ExcelPackage();
            var eachPersonSheet = package.Workbook.Worksheets.Add("Download(Each person)");

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
    }
}