using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Http;
using CostAllocationApp.BLL;
using CostAllocationApp.Dtos;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using System.Globalization;

namespace CostAllocationApp.Controllers.Api
{
    public class DashController : ApiController
    {
        ActualCostBLL actualCostBLL = null;
        EmployeeAssignmentBLL employeeAssignmentBLL = null;
        DepartmentBLL departmentBLL = null;
        CompanyBLL companyBLL = null;
        TotalBLL totalBLL = null;

        public DashController()
        {
            actualCostBLL = new ActualCostBLL();
            employeeAssignmentBLL = new EmployeeAssignmentBLL();
            departmentBLL = new DepartmentBLL();
            companyBLL = new CompanyBLL();
            totalBLL = new TotalBLL();
        }

        [HttpGet]
        [Route("api/dash/GetTotalCost/")]
        public IHttpActionResult GetTotalCost()
        {
            string[] departmentListToShow = { "New BLEND", "導入", "移行", "自治体", "運用保守", "その他" };
            var year = 0;
            var companyIds = "";
            double totalCost = 0;

            double _octTotalCost = 0;
            double _novTotalCost = 0;
            double _decTotalCost = 0;
            double _janTotalCost = 0;
            double _febTotalCost = 0;
            double _marTotalCost = 0;
            double _aprTotalCost = 0;
            double _mayTotalCost = 0;
            double _junTotalCost = 0;
            double _julTotalCost = 0;
            double _augTotalCost = 0;
            double _sepTotalCost = 0;

            double _octActualCost = 0;
            double _novActualCost = 0;
            double _decActualCost = 0;
            double _janActualCost = 0;
            double _febActualCost = 0;
            double _marActualCost = 0;
            double _aprActualCost = 0;
            double _mayActualCost = 0;
            double _junActualCost = 0;
            double _julActualCost = 0;
            double _augActualCost = 0;
            double _sepActualCost = 0;

            var chartData = new ArrayList();
            var data = new ArrayList();
            var monthlyForecastData = new Dictionary<string, object>();
            var monthlyActualData = new Dictionary<string, object>();
            var response = new Dictionary<string, object>();

            data.Add("Departments");
            data.Add("Cost");
            chartData.Add(data);

            List<int> companyIDList = new List<int>();

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            //year = 2029;

            List<Department> departments = departmentBLL.GetAllDepartments();
            List<Company> companies = companyBLL.GetAllCompanies();

            double totaleOtherCost = 0;

            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);
            foreach (var department in departments)
            {
                data = new ArrayList();
                double totalDeptCost = 0;

                if (departmentListToShow.Contains(department.DepartmentName))
                {
                    data.Add(department.DepartmentName);

                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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

                        totalDeptCost += (_octActualCostTotal > 0) ? _octActualCostTotal : _octTotal;
                        _octTotalCost += _octTotal;
                        _octActualCost += _octActualCostTotal;

                        totalDeptCost += (_novActualCostTotal > 0) ? _novActualCostTotal : _novTotal;
                        _novTotalCost += _novTotal;
                        _novActualCost += _novActualCostTotal;

                        totalDeptCost += (_decActualCostTotal > 0) ? _decActualCostTotal : _decTotal;
                        _decTotalCost += _decTotal;
                        _decActualCost += _decActualCostTotal;

                        totalDeptCost += (_janActualCostTotal > 0) ? _janActualCostTotal : _janTotal;
                        _janTotalCost += _janTotal;
                        _janActualCost += _janActualCostTotal;

                        totalDeptCost += (_febActualCostTotal > 0) ? _febActualCostTotal : _febTotal;
                        _febTotalCost += _febTotal;
                        _febActualCost += _febActualCostTotal;

                        totalDeptCost += (_marActualCostTotal > 0) ? _marActualCostTotal : _marTotal;
                        _marTotalCost += _marTotal;
                        _marActualCost += _marActualCostTotal;

                        totalDeptCost += (_aprActualCostTotal > 0) ? _aprActualCostTotal : _aprTotal;
                        _aprTotalCost += _aprTotal;
                        _aprActualCost += _aprActualCostTotal;

                        totalDeptCost += (_mayActualCostTotal > 0) ? _mayActualCostTotal : _mayTotal;
                        _mayTotalCost += _mayTotal;
                        _mayActualCost += _mayActualCostTotal;

                        totalDeptCost += (_junActualCostTotal > 0) ? _junActualCostTotal : _junTotal;
                        _junTotalCost += _junTotal;
                        _junActualCost += _junActualCostTotal;

                        totalDeptCost += (_julActualCostTotal > 0) ? _julActualCostTotal : _julTotal;
                        _julTotalCost += _julTotal;
                        _julActualCost += _julActualCostTotal;

                        totalDeptCost += (_augActualCostTotal > 0) ? _augActualCostTotal : _augTotal;
                        _augTotalCost += _augTotal;
                        _augActualCost += _augActualCostTotal;

                        totalDeptCost += (_sepActualCostTotal > 0) ? _sepActualCostTotal : _sepTotal;
                        _sepTotalCost += _sepTotal;
                        _sepActualCost += _sepActualCostTotal;


                        totalCost += totalDeptCost;
                        data.Add(totalDeptCost);
                    }
                    else
                    {
                        totalCost += totalDeptCost;
                        data.Add(totalDeptCost);
                    }
                    chartData.Add(data);
                }
                else
                {
                    List<ForecastAssignmentViewModel> forecastAssignmentViewModels = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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

                        totaleOtherCost += (_octActualCostTotal > 0) ? _octActualCostTotal : _octTotal;
                        _octTotalCost += _octTotal;
                        _octActualCost += _octActualCostTotal;

                        totaleOtherCost += (_novActualCostTotal > 0) ? _novActualCostTotal : _novTotal;
                        _novTotalCost += _novTotal;
                        _novActualCost += _novActualCostTotal;

                        totaleOtherCost += (_decActualCostTotal > 0) ? _decActualCostTotal : _decTotal;
                        _decTotalCost += _decTotal;
                        _decActualCost += _decActualCostTotal;

                        totaleOtherCost += (_janActualCostTotal > 0) ? _janActualCostTotal : _janTotal;
                        _janTotalCost += _janTotal;
                        _janActualCost += _janActualCostTotal;

                        totaleOtherCost += (_febActualCostTotal > 0) ? _febActualCostTotal : _febTotal;
                        _febTotalCost += _febTotal;
                        _febActualCost += _febActualCostTotal;

                        totaleOtherCost += (_marActualCostTotal > 0) ? _marActualCostTotal : _marTotal;
                        _marTotalCost += _marTotal;
                        _marActualCost += _marActualCostTotal;

                        totaleOtherCost += (_aprActualCostTotal > 0) ? _aprActualCostTotal : _aprTotal;
                        _aprTotalCost += _aprTotal;
                        _aprActualCost += _aprActualCostTotal;

                        totaleOtherCost += (_mayActualCostTotal > 0) ? _mayActualCostTotal : _mayTotal;
                        _mayTotalCost += _mayTotal;
                        _mayActualCost += _mayActualCostTotal;

                        totaleOtherCost += (_junActualCostTotal > 0) ? _junActualCostTotal : _junTotal;
                        _junTotalCost += _junTotal;
                        _junActualCost += _junActualCostTotal;

                        totaleOtherCost += (_julActualCostTotal > 0) ? _julActualCostTotal : _julTotal;
                        _julTotalCost += _julTotal;
                        _julActualCost += _julActualCostTotal;

                        totaleOtherCost += (_augActualCostTotal > 0) ? _augActualCostTotal : _augTotal;
                        _augTotalCost += _augTotal;
                        _augActualCost += _augActualCostTotal;

                        totaleOtherCost += (_sepActualCostTotal > 0) ? _sepActualCostTotal : _sepTotal;
                        _sepTotalCost += _sepTotal;
                        _sepActualCost += _sepActualCostTotal;
                    }
                }
            }
            //Add other department costs
            data = new ArrayList();
            totalCost += totaleOtherCost;
            data.Add("その他");
            data.Add(totaleOtherCost);
            chartData.Add(data);

            //Prepare monthly forecast cost data
            monthlyForecastData.Add("10", _octTotalCost);
            monthlyForecastData.Add("11", _novTotalCost);
            monthlyForecastData.Add("12", _decTotalCost);
            monthlyForecastData.Add("1", _janTotalCost);
            monthlyForecastData.Add("2", _febTotalCost);
            monthlyForecastData.Add("3", _marTotalCost);
            monthlyForecastData.Add("4", _aprTotalCost);
            monthlyForecastData.Add("5", _mayTotalCost);
            monthlyForecastData.Add("6", _junTotalCost);
            monthlyForecastData.Add("7", _julTotalCost);
            monthlyForecastData.Add("8", _augTotalCost);
            monthlyForecastData.Add("9", _sepTotalCost);

            //prepare monthly actual cost data
            monthlyActualData.Add("10", _octActualCost);
            monthlyActualData.Add("11", _novActualCost);
            monthlyActualData.Add("12", _decActualCost);
            monthlyActualData.Add("1", _janActualCost);
            monthlyActualData.Add("2", _febActualCost);
            monthlyActualData.Add("3", _marActualCost);
            monthlyActualData.Add("4", _aprActualCost);
            monthlyActualData.Add("5", _mayActualCost);
            monthlyActualData.Add("6", _junActualCost);
            monthlyActualData.Add("7", _julActualCost);
            monthlyActualData.Add("8", _augActualCost);
            monthlyActualData.Add("9", _sepActualCost);

            //Prepare response data
            response.Add("chartData", chartData);
            response.Add("Year", year);
            response.Add("totalCost", string.Format("{0:N2}M", totalCost / 1000000));
            response.Add("forecastCost", monthlyForecastData);
            response.Add("actualCost", monthlyActualData);

            return Ok(response);
        }

        [HttpGet]
        [Route("api/dash/GetBudgetCost/")]
        public IHttpActionResult GetBudgetCost()
        {
            int year = 0;
            var companyIds = "";
            var response = new Dictionary<string, object>();
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
            double _octBudgetCost = 0;
            double _novBudgetCost = 0;
            double _decBudgetCost = 0;
            double _janBudgetCost = 0;
            double _febBudgetCost = 0;
            double _marBudgetCost = 0;
            double _aprBudgetCost = 0;
            double _mayBudgetCost = 0;
            double _junBudgetCost = 0;
            double _julBudgetCost = 0;
            double _augBudgetCost = 0;
            double _sepBudgetCost = 0;

            int forecastLeatestYear = actualCostBLL.GetLeatestForcastYear();
            year = forecastLeatestYear;
            //year = 2029;
            List<Department> departments = departmentBLL.GetAllDepartments();
            List<int> companyIDList = new List<int>();

            List<Company> companies = companyBLL.GetAllCompanies();
            foreach (var company in companies)
            {
                companyIDList.Add(company.Id);
            }
            companyIds = String.Join(",", companyIDList);


            Department qaDepartmentByName = departmentBLL.GetAllDepartments().Where(d => d.DepartmentName == "品証").SingleOrDefault();


            var hinsoData = employeeAssignmentBLL.GetEmployeesForecastByDepartments_Company(qaDepartmentByName.Id, companyIds, year);
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
                if (department.Id == qaDepartmentByName.Id)
                {
                    continue;
                }
                var apportionmentByDepartment = actualCostBLL.GetAllApportionmentData(year).Where(ap => ap.DepartmentId == department.Id).SingleOrDefault();
                if (apportionmentByDepartment == null)
                {
                    apportionmentByDepartment = new Apportionment();
                }

                List<ForecastAssignmentViewModel> forecastAssignmentViewModels = totalBLL.GetEmployeesForecastByDepartments_Company(department.Id, companyIds, year);
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

                    var _octCalculation = _octHinsho * (apportionmentByDepartment.OctPercentage / 100);
                    {
                        _octBudgetCost += _octTotal + _octCalculation;
                    }
                    var _novCalculation = _novHinsho * (apportionmentByDepartment.NovPercentage / 100);

                    {
                        _novBudgetCost += _novTotal + _novCalculation;
                    }
                    var _decCalculation = _decHinsho * (apportionmentByDepartment.DecPercentage / 100);
                    {
                        _decBudgetCost += _decTotal + _decCalculation;
                    }
                    var _janCalculation = _janHinsho * (apportionmentByDepartment.JanPercentage / 100);
                    {
                        _janBudgetCost += _janTotal + _janCalculation;
                    }
                    var _febCalculation = _febHinsho * (apportionmentByDepartment.FebPercentage / 100);
                    {
                        _febBudgetCost += _febTotal + _febCalculation;
                    }
                    var _marCalculation = _marHinsho * (apportionmentByDepartment.MarPercentage / 100);
                    {
                        _marBudgetCost += _marTotal + _marCalculation;
                    }
                    var _aprCalculation = _aprHinsho * (apportionmentByDepartment.AprPercentage / 100);
                    {
                        _aprBudgetCost += _aprTotal + _aprCalculation;
                    }
                    var _mayCalculation = _mayHinsho * (apportionmentByDepartment.MayPercentage / 100);
                    {
                        _mayBudgetCost += _mayTotal + _mayCalculation;
                    }
                    var _junCalculation = _junHinsho * (apportionmentByDepartment.JunPercentage / 100);
                    {
                        _junBudgetCost += _junTotal + _junCalculation;
                    }
                    var _julCalculation = _julHinsho * (apportionmentByDepartment.JulPercentage / 100);
                    {
                        _julBudgetCost += _julTotal + _julCalculation;
                    }
                    var _augCalculation = _augHinsho * (apportionmentByDepartment.AugPercentage / 100);
                    {
                        _augBudgetCost += _augTotal + _augCalculation;
                    }
                    var _sepCalculation = _sepHinsho * (apportionmentByDepartment.SepPercentage / 100);
                    {
                        _sepBudgetCost += _sepTotal + _sepCalculation;
                    }
                }

            }

            response.Add("10", _octBudgetCost);
            response.Add("11", _novBudgetCost);
            response.Add("12", _decBudgetCost);
            response.Add("1", _janBudgetCost);
            response.Add("2", _febBudgetCost);
            response.Add("3", _marBudgetCost);
            response.Add("4", _aprBudgetCost);
            response.Add("5", _mayBudgetCost);
            response.Add("6", _junBudgetCost);
            response.Add("7", _julBudgetCost);
            response.Add("8", _augBudgetCost);
            response.Add("9", _sepBudgetCost);

            return Ok(response);
        }
    }
}