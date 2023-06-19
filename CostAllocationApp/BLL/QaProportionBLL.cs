using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.ViewModels;
using CostAllocationApp.DAL;

namespace CostAllocationApp.BLL
{
    public class QaProportionBLL
    {
        QaProportionDAL proportionDAL = null;
        EmployeeDAL employeeDAL = null;
        EmployeeAssignmentDAL assignmentDAL = null;
        public QaProportionBLL()
        {
            proportionDAL = new QaProportionDAL();
            employeeDAL = new EmployeeDAL();
            assignmentDAL = new EmployeeAssignmentDAL();

        }
        public List<QaProportionViewModel> SearchAssignmentByYear_Department(int year, int departmentId)
        {
            List<QaProportionViewModel> qaProportionViewModels = new List<QaProportionViewModel>();
            var nameList =  proportionDAL.SearchAssignmentByYear_Department(year, departmentId).Distinct().ToList();
            foreach (var item in nameList)
            {
                QaProportionViewModel qaProportionViewModel = new QaProportionViewModel();
                qaProportionViewModel.EmployeeName = item;
                qaProportionViewModels.Add(qaProportionViewModel);
            }

            return qaProportionViewModels;
        }
        public List<object> SearchAssignmentByYear_Department_Data(int year, int departmentId)
        {
            double octTotal = 0, novTotal = 0, decTotal = 0, janTotal = 0, febTotal = 0, marTotal = 0, aprTotal = 0, mayTotal = 0, junTotal = 0, julTotal = 0, augTotal = 0, sepTotal = 0;

            List<object> list = new List<object>();
            List<int> countedEmployeeId = new List<int>();

            var employeeList = proportionDAL.SearchAssignmentByYear_Department_Data(year, departmentId);

            foreach (var item in employeeList)
            {
                if (!countedEmployeeId.Contains(item.EmployeeId))
                {
                    var filteredEmployeeList = employeeList.Where(e => e.EmployeeId == item.EmployeeId).ToList();
                    foreach (var item1 in filteredEmployeeList)
                    {
                        var unitPrice = proportionDAL.GetUnitPriceByAssignmentId(item1.AssignmentId);
                        var forecastData = assignmentDAL.GetForecastsByAssignmentId(item.AssignmentId, year.ToString());
                        octTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 10).SingleOrDefault().Points)* unitPrice;
                        novTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 11).SingleOrDefault().Points) * unitPrice;
                        decTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 12).SingleOrDefault().Points)*unitPrice;
                        janTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 1).SingleOrDefault().Points)*unitPrice;
                        febTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 2).SingleOrDefault().Points)*unitPrice;
                        marTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 3).SingleOrDefault().Points)*unitPrice;
                        aprTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 4).SingleOrDefault().Points)*unitPrice;
                        mayTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 5).SingleOrDefault().Points)*unitPrice;
                        junTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 6).SingleOrDefault().Points)*unitPrice;
                        julTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 7).SingleOrDefault().Points)*unitPrice;
                        augTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 8).SingleOrDefault().Points)*unitPrice;
                        sepTotal += Convert.ToDouble(forecastData.Where(f => f.Month == 9).SingleOrDefault().Points)*unitPrice;
                    }
                    list.Add(new {EmployeeName=item.EmployeeName, OctTotal= octTotal.ToString("N0"), NovTotal= novTotal.ToString("N0"), DecTotal= decTotal.ToString("N0"), JanTotal= janTotal.ToString("N0"), FebTotal=febTotal.ToString("N0"), MarTotal=marTotal.ToString("N0"), AprTotal= aprTotal.ToString("N0"), MayTotal=mayTotal.ToString("N0"), JunTotal=junTotal.ToString("N0"), JulTotal=julTotal.ToString("N0"), AugTotal=augTotal.ToString("N0"), SepTotal=sepTotal.ToString("N0") });
                    countedEmployeeId.Add(item.EmployeeId);
                }
                octTotal = 0; novTotal = 0; decTotal = 0; janTotal = 0; febTotal = 0; marTotal = 0; aprTotal = 0; mayTotal = 0; junTotal = 0; julTotal = 0; augTotal = 0; sepTotal = 0;

            }

            return list;
        }
        
    }
}