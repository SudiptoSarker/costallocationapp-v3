﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Models;
using System.Data.SqlClient;
using System.Data;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.DAL
{
    public class ForecastDAL : DbContext
    {
        public int CreateForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"insert into Costs(Year,MonthId,Points,Total,EmployeeAssignmentsId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@total,@employeeAssignmentsId,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);
                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@createdBy", forecast.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", forecast.CreatedDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public int UpdateForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"update costs set Points = @points, Total= @total, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Year=@year and EmployeeAssignmentsId=@employeeAssignmentsId and MonthId=@monthId";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);


                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);

                cmd.Parameters.AddWithValue("@updatedBy", forecast.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", DateTime.Now);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }
        public int UpdateForecastWithAssignmentData(Forecast forecast)
        {
            int result = 0;
            string query = $@"update costs set Points = @points, Total= @total, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Year=@year and EmployeeAssignmentsId=@employeeAssignmentsId and MonthId=@monthId";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);


                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);

                cmd.Parameters.AddWithValue("@updatedBy", forecast.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", forecast.UpdatedDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }


        public bool CheckAssignmentId(int assignmentId, int year, int month)
        {
            string query = "select * from costs where EmployeeAssignmentsId=" + assignmentId + " and year = " + year + " and monthid=" + month;
            bool result = false;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        result = true;
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public int CreateTimeStamp(ForecastHisory forecastHisory)
        {
            int result = 0;
            string query = $@"insert into TimeStamps(TimeStamp,Year,CreatedBy,CreatedDate) values(@timeStamp,@year,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStamp", forecastHisory.TimeStamp);
                cmd.Parameters.AddWithValue("@year", forecastHisory.Year);
                cmd.Parameters.AddWithValue("@createdBy", forecastHisory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", forecastHisory.CreatedDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                if (result > 0)
                {
                    var lastId = GetLastId("TimeStamps");

                    foreach (var item in forecastHisory.Forecasts)
                    {
                        CreateForecastHistory(item, lastId);
                    }
                }
                return result;
            }
        }

        public int CreateForecastHistory(Forecast forecast, int timeStampId)
        {
            int result = 0;
            string query = $@"insert into CostHistories(Year,MonthId,Points,EmployeeAssignmentsId,TimeStampId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@employeeAssignmentsId,@timeStampId,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);
                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@timeStampId", timeStampId);
                cmd.Parameters.AddWithValue("@createdBy", forecast.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", forecast.CreatedDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public int GetLastId(string tableName)
        {
            int result = 0;
            string query = $@"select max(Id) from " + tableName;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);

                try
                {
                    result = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {

                }

                return result;
            }
        }

        public List<ForecastHisory> GetTimeStamps_Year(int year)
        {
            List<ForecastHisory> forecastHisories = new List<ForecastHisory>();
            string query = "";
            query = "SELECT * FROM TimeStamps WHERE year=" + year+" order by Id desc";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ForecastHisory forecastHisory = new ForecastHisory();
                            forecastHisory.Id = Convert.ToInt32(rdr["Id"]);
                            forecastHisory.TimeStamp = rdr["TimeStamp"].ToString();
                            forecastHisory.CreatedBy = rdr["CreatedBy"].ToString();

                            forecastHisories.Add(forecastHisory);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecastHisories;
            }
        }

        public List<Forecast> GetHistoriesByTimeStampId(int timeStampId)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM CostHistories WHERE TimeStampId=" + timeStampId;

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
                            forecast.CreatedBy = rdr["CreatedBy"].ToString();
                            forecasts.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecasts;
            }
        }

        public List<Forecast> GetForecastHistories(int timeStampId)
        {
            List<Forecast> forecastHisories = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM CostHistories WHERE TimeStampId=" + timeStampId;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Forecast forecastHisory = new Forecast();
                            forecastHisory.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecastHisory.Points = Convert.ToDecimal(rdr["Points"]);
                            forecastHisory.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
                            forecastHisories.Add(forecastHisory);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecastHisories;
            }
        }

        public List<Forecast> GetForecastsByAssignmentId(int assignmentId)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM Costs WHERE EmployeeAssignmentsId=" + assignmentId;
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
                            forecast.UpdatedBy = rdr["UpdatedBy"].ToString();
                            forecasts.Add(forecast);

                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecasts;
            }
        }

        public Forecast MatchForecastHistoryByAssignmentId(int assignmentId,DateTime date)
        {
            Forecast forecast = new Forecast();
            string query = "";
            query = "SELECT top 1 Id,EmployeeAssignmentsId,CreatedDate,CreatedBy FROM CostHistories WHERE EmployeeAssignmentsId=" + assignmentId +" order by Id desc";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            forecast.CreatedBy = rdr["CreatedBy"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecast;
            }
        }

        public Forecast MatchForecastHistoryUsernamesByAssignmentId(int assignmentId, DateTime date)
        {
            Forecast forecast = new Forecast();
            string query = "";
            query = "SELECT top 1 Id,EmployeeAssignmentsId,CreatedDate,CreatedBy FROM CostHistories WHERE EmployeeAssignmentsId=" + assignmentId + " order by Id desc";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.CreatedBy = rdr["CreatedBy"].ToString();
                            forecast.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                        }
                        
                    }
                }
                catch (Exception ex)
                {

                }

                return forecast;
            }
        }

        public List<Forecast> GetMatchedForecastHistoryByAssignmentId(int assignmentId)
        {
            List<Forecast> forecastHistories = new List<Forecast>();
            string query = "";
            query = "SELECT top 12 * FROM CostHistories WHERE EmployeeAssignmentsId=" + assignmentId + " order by ID desc";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while(rdr.Read())
                        {
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
                            forecast.CreatedBy = rdr["CreatedBy"].ToString();
                            forecastHistories.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecastHistories;
            }
        }
        public List<ForecastYear> GetForecastYear()
        {
            List<ForecastYear> forecastYears = new List<ForecastYear>();
            string query = "";
            query = "select distinct Year from EmployeesAssignments order by Year asc";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {                        
                        while (rdr.Read())
                        {
                            ForecastYear forecastYear = new ForecastYear();
                            forecastYear.Year = Convert.ToInt32(rdr["Year"]);
                            forecastYears.Add(forecastYear);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecastYears;
            }
        }

        public List<ExcelAssignmentDto> GetEmployeesForecastByYear(int year)
        {
            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,emp.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.SubCode, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId
                            from EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where ea.Year={year} and 1=1
                            order by emp.Id asc";

            List<ExcelAssignmentDto> excelAssignmentDtos = new List<ExcelAssignmentDto>();            

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            ExcelAssignmentDto excelAssignmentDto = new ExcelAssignmentDto();
                            excelAssignmentDto.Id = Convert.ToInt32(rdr["AssignmentId"]);                            
                            if (rdr["EmployeeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.EmployeeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.EmployeeId = rdr["EmployeeId"].ToString();
                            }
                            if (rdr["SectionId"] == DBNull.Value)
                            {
                                excelAssignmentDto.SectionId = null;
                            }
                            else
                            {
                                excelAssignmentDto.SectionId = Convert.ToInt32(rdr["SectionId"]);
                            }
                            if (rdr["DepartmentId"] == DBNull.Value)
                            {
                                excelAssignmentDto.DepartmentId = null;
                            }
                            else
                            {
                                excelAssignmentDto.DepartmentId = Convert.ToInt32(rdr["DepartmentId"]);
                            }
                            if (rdr["InchargeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.InchargeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.InchargeId = Convert.ToInt32(rdr["InchargeId"]);
                            }
                            if (rdr["RoleId"] == DBNull.Value)
                            {
                                excelAssignmentDto.RoleId = null;
                            }
                            else
                            {
                                excelAssignmentDto.RoleId = Convert.ToInt32(rdr["RoleId"]);
                            }

                            if (rdr["ExplanationId"] == DBNull.Value)
                            {
                                excelAssignmentDto.ExplanationId = null;
                            }
                            else
                            {
                                excelAssignmentDto.ExplanationId = Convert.ToInt32(rdr["ExplanationId"]);                                
                            }                            
                            if (rdr["CompanyId"] == DBNull.Value)
                            {
                                excelAssignmentDto.CompanyId = null;
                            }
                            else
                            {
                                excelAssignmentDto.CompanyId = Convert.ToInt32(rdr["CompanyId"]);
                            }
                            excelAssignmentDto.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]);
                            
                            if (rdr["GradeId"] == DBNull.Value)
                            {
                                excelAssignmentDto.GradeId = null;
                            }
                            else
                            {
                                excelAssignmentDto.GradeId = Convert.ToInt32(rdr["GradeId"]);
                            }
                            excelAssignmentDto.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            excelAssignmentDto.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();

                            excelAssignmentDtos.Add(excelAssignmentDto);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return excelAssignmentDtos;
        }
        public List<Forecast> GetForecastDetails(int assignmentId,int copyYear)
        {
            string query = "select Id,Year,MonthId,Points,Total,EmployeeAssignmentsId,CreatedBy,CreatedDate,UpdatedDate from Costs Where EmployeeAssignmentsId = "+assignmentId+ " and Year=" + copyYear;

            List<Forecast> forecasts = new List<Forecast>();

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Forecast forecast = new Forecast();
                            forecast.Id = Convert.ToInt32(rdr["Id"]);
                            if (rdr["EmployeeAssignmentsId"] == DBNull.Value)
                            {
                                forecast.EmployeeAssignmentId = 0;
                            }
                            else
                            {
                                forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);
                            }
                            if (rdr["Year"] == DBNull.Value)
                            {
                                forecast.Year = 0;
                            }
                            else
                            {
                                forecast.Year = Convert.ToInt32(rdr["Year"]);
                            }
                            if (rdr["MonthId"] == DBNull.Value)
                            {
                                forecast.Month = 0;
                            }
                            else
                            {
                                forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            }
                            if (rdr["Points"] == DBNull.Value)
                            {
                                forecast.Points = 0;
                            }
                            else
                            {
                                forecast.Points = Convert.ToInt32(rdr["Points"]);
                            }
                            if (rdr["Total"] == DBNull.Value)
                            {
                                forecast.Total = 0;
                            }
                            else
                            {
                                forecast.Total = Convert.ToInt32(rdr["Total"]);
                            }
                            if (rdr["CreatedBy"] == DBNull.Value)
                            {
                                forecast.CreatedBy = null;
                            }
                            else
                            {
                                forecast.CreatedBy = rdr["CreatedBy"].ToString();
                            }

                            if (rdr["CreatedDate"] == DBNull.Value)
                            {
                                forecast.CreatedDate = DateTime.Now;
                            }
                            else
                            {
                                forecast.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            }
                            if (rdr["UpdatedDate"] == DBNull.Value)
                            {
                                forecast.UpdatedDate = DateTime.Now; ;
                            }
                            else
                            {
                                forecast.UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]);
                            }

                            forecasts.Add(forecast);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecasts;
        }
        public List<int> GetYearFromHistory()
        {
            List<int> years = new List<int>();
            string query = "";

            query = "select distinct year from CostHistories where Year > 0 order by year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            var year = Convert.ToInt32(rdr["Year"]);
                            years.Add(year);

                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return years;
            }
        }

    }
}