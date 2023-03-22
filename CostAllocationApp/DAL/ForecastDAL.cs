using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.Models;
using System.Data.SqlClient;


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


        public bool CheckAssignmentId(int assignmentId,int year,int month)
        {
            string query = "select * from costs where EmployeeAssignmentsId="+assignmentId+" and year = "+year+" and monthid="+month;
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

        public int CreateForecastHistory(Forecast forecast,int timeStampId)
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
            string query = $@"select max(Id) from "+tableName;
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
            query = "SELECT * FROM TimeStamps WHERE year="+year;
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
    }
}