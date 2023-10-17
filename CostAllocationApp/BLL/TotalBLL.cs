using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.BLL
{
    public class TotalBLL
    {
        TotalDAL totalDAL = null;
        DepartmentBLL departmentBLL = null;
        InChargeBLL inchargeBLL = null;
        CategoryBLL categoryBLL = null;
        SubCategoryBLL subCategoryBLL = null;
        DetailsItemBLL detailsItemBLL = null;
        public TotalBLL()
        {
            totalDAL = new TotalDAL();
            departmentBLL = new DepartmentBLL();
            inchargeBLL = new InChargeBLL();
            categoryBLL = new CategoryBLL();
            subCategoryBLL = new SubCategoryBLL();
            detailsItemBLL = new DetailsItemBLL();
        }

        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(int departmentId, string companyIds, int year)
        {
            List<ForecastAssignmentViewModel> forecastAssignments = totalDAL.GetEmployeesForecastByDepartments_Company(departmentId, companyIds, year);
            if (forecastAssignments.Count > 0)
            {
                foreach (var forecastAssignment in forecastAssignments)
                {
                    forecastAssignment.forecasts = totalDAL.GetForecastsByAssignmentId(forecastAssignment.Id, year.ToString());
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

                }
            }
            return forecastAssignments;
        }

        public int CreateDynamicTable(DynamicTable dynamicTable)
        {
            return totalDAL.CreateDynamicTable(dynamicTable);
        }
        public List<DynamicTable> GetAllDynamicTables()
        {
            return totalDAL.GetAllDynamicTables();
        }
        public int RemoveDynamicTable(DynamicTable dynamicTable)
        {
            List<Category> categories = categoryBLL.GetAllCategoriesByDynamicTableId(dynamicTable.Id);
            List<SubCategory> subCategories = new List<SubCategory>();
            List<DeatailsItem> detailsItems = new List<DeatailsItem>();


            if (categories.Count>0)
            {
                foreach (var category in categories)
                {
                    var subCategoryList = subCategoryBLL.GetSubCategoryByCategoryId(category.Id);
                    subCategories.AddRange(subCategoryList);
                }

                foreach (var category in categories)
                {
                    categoryBLL.RemoveCategory(category);
                }
            }

            if (subCategories.Count > 0)
            {
                foreach (var subCategory in subCategories)
                {
                    var deatailsItemsList = detailsItemBLL.GetDetailsItemBySubItemsId(subCategory.Id);
                    detailsItems.AddRange(deatailsItemsList);
                }

                foreach (var subCategory in subCategories)
                {
                    subCategoryBLL.RemoveSubCategory(subCategory);
                }
            }

            if (detailsItems.Count > 0)
            {
                foreach (var detailsItem in detailsItems)
                {
                    detailsItemBLL.RemoveDetailsItem(detailsItem);
                }
                
            }
            
            return totalDAL.RemoveDynamicTable(dynamicTable);
        }
        public int DeleteDynamicTableSettings(string dynamicTableid,string dynamicSettingsIds)
        {
            return totalDAL.DeleteDynamicTableSettings(dynamicTableid, dynamicSettingsIds);
        }
        public int UpdateDynamicTable(DynamicTable dynamicTable)
        {
            return totalDAL.UpdateDynamicTable(dynamicTable);
        }
        public int CreateDynamicSetting(DynamicSetting dynamicSetting)
        {
            return totalDAL.CreateDynamicSetting(dynamicSetting);
        }
        public DynamicTable GetDynamicTableById(int tableId)
        {
            return totalDAL.GetDynamicTableById(tableId);
        }

        public List<DynamicSetting> GetDynamicSettingsByDynamicTableId(int dynamicTableId)
        {
            List<DynamicSetting> dynamicSettings = totalDAL.GetDynamicSettingsByDynamicTableId(dynamicTableId);
            if (dynamicSettings.Count > 0)
            {
                foreach (var dynamicSetting in dynamicSettings)
                {
                    var dynamicMethodDefinition = DynamicMethodDefinition.GetMethods().Where(dmd => dmd.Id == Convert.ToInt32(dynamicSetting.MethodId)).SingleOrDefault();
                    dynamicSetting.MethodName = dynamicMethodDefinition.MethodName;
                    if (!String.IsNullOrEmpty(dynamicSetting.ParameterId))
                    {
                        string dependencyNames = "";
                        string[] parameterNames = dynamicSetting.ParameterId.Split(',');
                        if (dynamicMethodDefinition.Dependency=="dp")
                        {
                            foreach (var item in parameterNames)
                            {
                                dependencyNames += departmentBLL.GetDepartmentByDepartemntId(Convert.ToInt32(item)).DepartmentName + ", ";
                            }
                        }
                        if (dynamicMethodDefinition.Dependency == "in")
                        {
                            foreach (var item in parameterNames)
                            {
                                dependencyNames += inchargeBLL.GetInChargeByInChargeId(Convert.ToInt32(item)).InChargeName + ", ";
                            }
                        }
                        

                        dependencyNames.TrimEnd(',');
                        dynamicSetting.CommaSeperatedParameterName = dependencyNames;
                    }
                    
                }
            }

            return dynamicSettings;
        }

        public int UpdateDynamicTablesTitle(DynamicTable dynamicTable)
        {
            return totalDAL.UpdateDynamicTablesTitle(dynamicTable);
        }

        public bool IsNameAndPositionExists(string tableName, int tablePoisition, int tableId, string checkType)
        {
            return totalDAL.IsNameAndPositionExists(tableName, tablePoisition, tableId, checkType);
        }
        public int UpdateDynamicTableSettings(DynamicSetting dynamicSetting)
        {
            return totalDAL.UpdateDynamicTableSettings(dynamicSetting);
        }
        public string GetCostTableHeaderPart(string main_header,string sub_header,string detial_header,string tableTitle,string year)
        {
            string strTableHeader = "";            
            //strTableHeader = "<p class'font-weight-bold' id='p-total' style='margin-top:20px;'><u>" + tableTitle + ":</u></p>";

            strTableHeader = strTableHeader + "<thead>";
            strTableHeader = strTableHeader + "	<tr>";
            if (!string.IsNullOrEmpty(main_header))
            {
                strTableHeader = strTableHeader + "<th>"+ main_header + "</th>";
            }
            if (!string.IsNullOrEmpty(sub_header))
            {
                strTableHeader = strTableHeader + "<th>" + sub_header + "</th>";
            }
            if (!string.IsNullOrEmpty(detial_header))
            {
                strTableHeader = strTableHeader + "<th>" + detial_header + "</th>";
            }
            strTableHeader = strTableHeader + "		<th>10月</th>";
            strTableHeader = strTableHeader + "		<th>11月</th>";
            strTableHeader = strTableHeader + "		<th>12月</th>";
            strTableHeader = strTableHeader + "		<th>1月</th>";
            strTableHeader = strTableHeader + "		<th>2月</th>";
            strTableHeader = strTableHeader + "		<th>3月</th>";
            strTableHeader = strTableHeader + "		<th>4月</th>";
            strTableHeader = strTableHeader + "		<th>5月</th>";
            strTableHeader = strTableHeader + "		<th>6月</th>";
            strTableHeader = strTableHeader + "		<th>7月</th>";
            strTableHeader = strTableHeader + "		<th>8月</th>";
            strTableHeader = strTableHeader + "		<th>9月</th>";

            strTableHeader = strTableHeader + "		<th>FY$"+ year + "計</th>";
            strTableHeader = strTableHeader + "		<th>上期</th>";
            strTableHeader = strTableHeader + "		<th>下期 </th>";
            strTableHeader = strTableHeader + "	</tr>";
            strTableHeader = strTableHeader + "</thead>";

            return strTableHeader;
        }

        public string GetDynamicTableTitleByPosition(string tablePosition)
        {
            return totalDAL.GetDynamicTableTitleByPosition(tablePosition);
        }
        public string GetTotalCostTableBody(DynamicSetting settingItem, int totalTableIndexCount,string year,string timestampsId)
        {            
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";
            
            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                if (!string.IsNullOrEmpty(settingItem.MethodId))
                {
                    totalTableTd = totalTableTd + GetCostTableBodyWithCalculation(settingItem, year, timestampsId);
                }                
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }
        public string GetParameterIdsByMethodId(string tableId, string methodId)
        {
            string paramIds = "";

            return paramIds;
        }
        public string GetCostTableBodyWithCalculation(DynamicSetting settingItem,string year,string timestampsId)
        {
            string totalTableTd = "";
            //1: Cost for department without QA proration
            if (Convert.ToInt32(settingItem.MethodId) == 1)
            {

                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";

            }
            //2: Cost for department from QA prorationt
            else if (Convert.ToInt32(settingItem.MethodId) == 2)
            {

            }
            //3: Cost for department with QA proration
            else if (Convert.ToInt32(settingItem.MethodId) == 3)
            {

            }
            return totalTableTd;
        }
        public List<SukeyQADto> GetTotalCostTableByMethodId(int year,int )
        {
            int year = 0;
            double _octHinsho = 0;
            double _novHinsho = 0;
            double _decHinsho = 0;
            double _janHinsho = 0;
            double _febHinsho = 0;
            double _marHinsho = 0;
            double _aprHinsho = 0;
            double _mayHinsho = 0;
            double _junHinsho = 0;
            double _julHinsho = 0;
            double _augHinsho = 0;
            double _sepHinsho = 0;
            List<SukeyQADto> sukeyQADtos = new List<SukeyQADto>();
            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            //int actualCostLeatestYear = actualCostBLL.GetLeatestActualCostYear();
            year = forecastLeatestYear;
            List<Department> departments = departmentBLL.GetAllDepartments();
            Department qaDepartmentByName = departments.Where(d => d.DepartmentName == "品証").SingleOrDefault();
            if (qaDepartmentByName == null)
            {
                return NotFound();
            }
            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companiIds, year);
            if (hinsoData.Count > 0)
            {
                _octHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.OctTotal));
                _novHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.NovTotal));
                _decHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.DecTotal));
                _janHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JanTotal));
                _febHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.FebTotal));
                _marHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MarTotal));
                _aprHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AprTotal));
                _mayHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.MayTotal));
                _junHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JunTotal));
                _julHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.JulTotal));
                _augHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.AugTotal));
                _sepHinsho = hinsoData.Sum(fa => Convert.ToDouble(fa.SepTotal));
            }

            foreach (var department in departments)
            {
                double rowTotal = 0;
                double firstSlot = 0;
                SukeyQADto sukeyDto = new SukeyQADto();
                sukeyDto.DepartmentId = department.Id.ToString();
                sukeyDto.DepartmentName = department.DepartmentName;
                //if (department.Id==8)
                //{
                //    continue;
                //}
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }


                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companiIds, year);
                if (forecastAssignmentViewModels.Count > 0)
                {
                    double _octTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.OctTotal));
                    double _novTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.NovTotal));
                    double _decTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.DecTotal));
                    double _janTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JanTotal));
                    double _febTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.FebTotal));
                    double _marTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MarTotal));
                    double _aprTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AprTotal));
                    double _mayTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.MayTotal));
                    double _junTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JunTotal));
                    double _julTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.JulTotal));
                    double _augTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.AugTotal));
                    double _sepTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.SepTotal));

                    double _octActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].OctCost));
                    double _novActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].NovCost));
                    double _decActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].DecCost));
                    double _janActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JanCost));
                    double _febActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].FebCost));
                    double _marActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MarCost));
                    double _aprActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AprCost));
                    double _mayActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].MayCost));
                    double _junActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JunCost));
                    double _julActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].JulCost));
                    double _augActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].AugCost));
                    double _sepActualCostTotal = forecastAssignmentViewModels.Sum(fa => Convert.ToDouble(fa.ActualCosts[0].SepCost));

                    var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    if (_octActualCostTotal > 0)
                    {
                        sukeyDto.OctCost.Add(_octActualCostTotal + _octCalculation);
                        rowTotal += _octActualCostTotal + _octCalculation;
                    }
                    else
                    {
                        sukeyDto.OctCost.Add(_octTotal + _octCalculation);
                        rowTotal += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);
                    if (_novActualCostTotal > 0)
                    {
                        sukeyDto.NovCost.Add(_novActualCostTotal + _novCalculation);
                        rowTotal += _novActualCostTotal + _novCalculation;
                    }
                    else
                    {
                        sukeyDto.NovCost.Add(_novTotal + _novCalculation);
                        rowTotal += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    if (_decActualCostTotal > 0)
                    {
                        sukeyDto.DecCost.Add(_decActualCostTotal + _decCalculation);
                        rowTotal += _decActualCostTotal + _decCalculation;
                    }
                    else
                    {
                        sukeyDto.DecCost.Add(_decTotal + _decCalculation);
                        rowTotal += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    if (_janActualCostTotal > 0)
                    {
                        sukeyDto.JanCost.Add(_janActualCostTotal + _janCalculation);
                        rowTotal += _janActualCostTotal + _janCalculation;
                    }
                    else
                    {
                        sukeyDto.JanCost.Add(_janTotal + _janCalculation);
                        rowTotal += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    if (_febActualCostTotal > 0)
                    {
                        sukeyDto.FebCost.Add(_febActualCostTotal + _febCalculation);
                        rowTotal += _febActualCostTotal + _febCalculation;
                    }
                    else
                    {
                        sukeyDto.FebCost.Add(_febTotal + _febCalculation);
                        rowTotal += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    if (_marActualCostTotal > 0)
                    {
                        sukeyDto.MarCost.Add(_marActualCostTotal + _marCalculation);
                        rowTotal += _marActualCostTotal + _marCalculation;
                    }
                    else
                    {
                        sukeyDto.MarCost.Add(_marTotal + _marCalculation);
                        rowTotal += _marTotal + _marCalculation;
                    }
                    sukeyDto.FirstSlot.Add(rowTotal);
                    firstSlot = rowTotal;

                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    if (_aprActualCostTotal > 0)
                    {
                        sukeyDto.AprCost.Add(_aprActualCostTotal + _aprCalculation);
                        rowTotal += _aprActualCostTotal + _aprCalculation;
                    }
                    else
                    {
                        sukeyDto.AprCost.Add(_aprTotal + _aprCalculation);
                        rowTotal += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    if (_mayActualCostTotal > 0)
                    {
                        sukeyDto.MayCost.Add(_mayActualCostTotal + _mayCalculation);
                        rowTotal += _mayActualCostTotal + _mayCalculation;
                    }
                    else
                    {
                        sukeyDto.MayCost.Add(_mayTotal + _mayCalculation);
                        rowTotal += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    if (_junActualCostTotal > 0)
                    {
                        sukeyDto.JunCost.Add(_junActualCostTotal + _junCalculation);
                        rowTotal += _junActualCostTotal + _junCalculation;
                    }
                    else
                    {
                        sukeyDto.JunCost.Add(_junTotal + _junCalculation);
                        rowTotal += _junTotal + _junCalculation;
                    }
                    var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    if (_julActualCostTotal > 0)
                    {
                        sukeyDto.JulCost.Add(_julActualCostTotal + _julCalculation);
                        rowTotal += _julActualCostTotal + _julCalculation;
                    }
                    else
                    {
                        sukeyDto.JulCost.Add(_julTotal + _julCalculation);
                        rowTotal += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    if (_augActualCostTotal > 0)
                    {
                        sukeyDto.AugCost.Add(_augActualCostTotal + _augCalculation);
                        rowTotal += _augActualCostTotal + _augCalculation;
                    }
                    else
                    {
                        sukeyDto.AugCost.Add(_augTotal + _augCalculation);
                        rowTotal += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    if (_sepActualCostTotal > 0)
                    {
                        sukeyDto.SepCost.Add(_sepActualCostTotal + _sepCalculation);
                        rowTotal += _sepActualCostTotal + _sepCalculation;
                    }
                    else
                    {
                        sukeyDto.SepCost.Add(_sepTotal + _sepCalculation);
                        rowTotal += _sepTotal + _sepCalculation;
                    }
                    sukeyDto.RowTotal.Add(rowTotal);
                    sukeyDto.SecondSlot.Add(rowTotal - firstSlot);

                }
                else
                {
                    sukeyDto.OctCost.Add(0);
                    sukeyDto.NovCost.Add(0);
                    sukeyDto.DecCost.Add(0);
                    sukeyDto.JanCost.Add(0);
                    sukeyDto.FebCost.Add(0);
                    sukeyDto.MarCost.Add(0);
                    sukeyDto.AprCost.Add(0);
                    sukeyDto.MayCost.Add(0);
                    sukeyDto.JunCost.Add(0);
                    sukeyDto.JulCost.Add(0);
                    sukeyDto.AugCost.Add(0);
                    sukeyDto.SepCost.Add(0);
                    sukeyDto.RowTotal.Add(0);
                    sukeyDto.FirstSlot.Add(0);
                    sukeyDto.SecondSlot.Add(0);
                }



                sukeyQADtos.Add(sukeyDto);
            }
            return Ok(sukeyQADtos);
        }

        public string GetTotalTableBodyPart(DynamicSetting settingItem, int totalTableIndexCount)
        {
            string totalTableTrStart = "";
            totalTableTrStart = "<tr data-indentity='" + totalTableIndexCount + "'>";

            string totalTableTrEnd = "";
            totalTableTrEnd = "</tr>";

            string totalTableTd = "";
            string singleTotalBody = "";

            if (!string.IsNullOrEmpty(settingItem.CategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.CategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.CategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.SubCategoryName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.SubCategoryName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.SubCategoryName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(settingItem.DetailsItemName))
            {
                if (string.IsNullOrEmpty(totalTableTd))
                {
                    totalTableTd = "<td>" + settingItem.DetailsItemName + "</td>";
                }
                else
                {
                    totalTableTd = totalTableTd + "<td>" + settingItem.DetailsItemName + "</td>";
                }

            }
            if (!string.IsNullOrEmpty(totalTableTd))
            {
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
                totalTableTd = totalTableTd + "<td class='text-right'>0</td>";
            }
            singleTotalBody = totalTableTrStart + "" + totalTableTd + "" + totalTableTrEnd;

            return singleTotalBody;
        }
    }
}