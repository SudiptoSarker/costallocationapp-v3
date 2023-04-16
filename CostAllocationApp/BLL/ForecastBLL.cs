using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class ForecastBLL
    {
        ForecastDAL forecastDAL = null;
        public ForecastBLL()
        {
            forecastDAL = new ForecastDAL();
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
        public List<ForecastHisory> GetTimeStamps_Year(int year)
        {
            return forecastDAL.GetTimeStamps_Year(year);
        }
        public List<Forecast> GetForecastHistories(int timeStampId)
        {
            return forecastDAL.GetForecastHistories(timeStampId);
        }
        public List<Forecast> GetForecastsByAssignmentId(int assignmentId)
        {
            return forecastDAL.GetForecastsByAssignmentId(assignmentId);
        }
        public bool MatchForecastHistoryByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.MatchForecastHistoryByAssignmentId(assignmentId,date);
        }
        public List<Forecast> GetMatchedForecastHistoryByAssignmentId(int assignmentId, DateTime date)
        {
            return forecastDAL.GetMatchedForecastHistoryByAssignmentId(assignmentId, date);
        }
    }
}