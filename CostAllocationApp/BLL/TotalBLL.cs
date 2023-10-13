using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;

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
        public string GetTotalTableHeaderPart(string main_header,string sub_header,string detial_header,string tableTitle,string year)
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
    }
}