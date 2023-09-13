using CostAllocationApp.Dtos;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using System;
using System.Collections.Generic;
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
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
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

        public int CreateFinalBudgetForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"insert into FinalBudgetCosts(Year,MonthId,Points,Total,EmployeeBudgetId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@total,@employeeBudgetId,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);
                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@employeeBudgetId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@createdBy", forecast.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
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
        public int CreateBudgetForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"insert into BudgetCosts(Year,MonthId,Points,Total,EmployeeBudgetId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@total,@employeeBudgetId,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);
                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@employeeBudgetId", forecast.EmployeeAssignmentId);
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
        public int UpdateBudgetForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"update BudgetCosts set Points = @points, Total= @total, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Year=@year and EmployeeBudgetId=@employeeBudgetId and MonthId=@monthId";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);


                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@employeeBudgetId", forecast.EmployeeAssignmentId);
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

        public bool CheckBudgetId(int assignmentId, int year, int month)
        {
            string query = "select * from BudgetCosts where EmployeeBudgetId=" + assignmentId + " and year = " + year + " and monthid=" + month;
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

        public int CreateTimeStampAndAssignmentHistory(ForecastHisory forecastHisory, List<AssignmentHistory> assignmentHistories,bool isUpdate,bool isDeleted)
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

                    foreach (var item in assignmentHistories)
                    {
                        CreateAssignmenttHistory(item, lastId, isUpdate, isDeleted,false);
                    }
                    result = lastId;
                }
                return result;
            }
        }

        public int CreateAssignmenttHistory(AssignmentHistory assignmentHistory, int timeStampId,bool isUpdate,bool isDeleted,bool isOriginal)
        {
            int result = 0;

            string query = $@"insert into EmployeesAssignmentsWithCostsHistory(TimeStampId,Year,EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,EmployeeAssignmentId,MonthId_Points,CreatedBy,CreatedDate,IsUpdate,IsDeleted,IsOriginal) values(@timeStampId,@year,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@employeeAssignmentId,@monthId_Points,@createdBy,@createdDate,@isUpdate,@isDeleted,@isOriginal)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStampId", timeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);
                cmd.Parameters.AddWithValue("@employeeId", assignmentHistory.EmployeeId);
                cmd.Parameters.AddWithValue("@sectionId", assignmentHistory.SectionId);
                cmd.Parameters.AddWithValue("@departmentId", assignmentHistory.DepartmentId);
                cmd.Parameters.AddWithValue("@inChargeId", assignmentHistory.InChargeId);
                cmd.Parameters.AddWithValue("@roleId", assignmentHistory.RoleId);
                cmd.Parameters.AddWithValue("@explanationId", assignmentHistory.ExplanationId);
                cmd.Parameters.AddWithValue("@companyId", assignmentHistory.CompanyId);
                cmd.Parameters.AddWithValue("@unitPrice", assignmentHistory.UnitPrice);
                cmd.Parameters.AddWithValue("@gradeId", assignmentHistory.GradeId);
                cmd.Parameters.AddWithValue("@employeeAssignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId_Points", assignmentHistory.MonthId_Points);
                cmd.Parameters.AddWithValue("@createdBy", assignmentHistory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", assignmentHistory.CreatedDate);
                cmd.Parameters.AddWithValue("@isUpdate", isUpdate);
                cmd.Parameters.AddWithValue("@isDeleted", isDeleted);
                cmd.Parameters.AddWithValue("@isOriginal", isOriginal);

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

        public AssignmentHistory GetPreviousAssignmentDataById(int assignmentId)
        {
            AssignmentHistory assignmentHistories = new AssignmentHistory();
            string query = "";
            query = "SELECT * FROM EmployeesAssignments WHERE id=" + assignmentId;

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
                            assignmentHistories.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistories.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            assignmentHistories.SectionId = rdr["SectionId"] is DBNull ? "" : rdr["SectionId"].ToString();
                            assignmentHistories.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            assignmentHistories.InChargeId = rdr["InChargeId"] is DBNull ? "" : rdr["InChargeId"].ToString();
                            assignmentHistories.RoleId = rdr["RoleId"] is DBNull ? "" : rdr["RoleId"].ToString();
                            assignmentHistories.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            assignmentHistories.CompanyId = rdr["CompanyId"] is DBNull ? "" : rdr["CompanyId"].ToString();
                            assignmentHistories.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]).ToString();
                            assignmentHistories.GradeId = rdr["GradeId"] is DBNull ? "" : rdr["GradeId"].ToString();
                            assignmentHistories.CreatedBy = rdr["CreatedBy"] is DBNull ? "" : rdr["CreatedBy"].ToString();
                            assignmentHistories.UpdatedBy = rdr["UpdatedBy"] is DBNull ? "" : rdr["UpdatedBy"].ToString();
                            assignmentHistories.EmployeeAssignmentId = rdr["Id"] is DBNull ? "" : rdr["Id"].ToString();
                            assignmentHistories.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();
                            assignmentHistories.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            assignmentHistories.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            assignmentHistories.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();

                            if (!string.IsNullOrEmpty(assignmentHistories.Id.ToString()))
                            {
                                assignmentHistories.MonthId_Points = GetForecastDataForHistory(assignmentHistories.Id);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistories;
            }
        }
        public string GetForecastDataForHistory(int assignmentId)
        {
            string returnValue = "";
            List<Forecast> _forecasts = new List<Forecast>();

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
                            Forecast _forecast = new Forecast();

                            _forecast.Id = Convert.ToInt32(rdr["Id"]);
                            _forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            _forecast.Points = Convert.ToDecimal(rdr["Points"]);

                            if (returnValue == "")
                            {
                                returnValue = _forecast.Month + "_" + _forecast.Points;
                            }
                            else
                            {
                                returnValue = returnValue + "," + _forecast.Month + "_" + _forecast.Points;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return returnValue;
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
            query = "SELECT * FROM TimeStamps WHERE year=" + year + " order by Id desc";

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

        public List<ForecastHisory> GetApprovalTimeStamps(int year)
        {
            List<ForecastHisory> forecastHisories = new List<ForecastHisory>();
            string query = "";
            query = "SELECT * FROM ApproveTimeStamps WHERE year=" + year + " order by Id desc";

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
                            forecastHisory.TimeStamp = rdr["ApproveTimeStamp"].ToString();
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
        public List<Forecast> GetAssignmentHistoriesByTimeStampId(int timeStampId,bool isOriginal)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            if (isOriginal)
            {
                query = "SELECT * FROM EmployeesAssignmentsWithCostsHistory WHERE TimeStampId=" + timeStampId + " AND IsOriginal=1";
            }
            else
            {
                query = "SELECT * FROM EmployeesAssignmentsWithCostsHistory WHERE TimeStampId=" + timeStampId + " AND (IsOriginal IS NULL OR IsOriginal=0)";
            }                        
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
                            //Forecast forecast2 = new Forecast();
                            //forecast2.Id = Convert.ToInt32(rdr["Id"]);
                            //forecast2.Month = Convert.ToInt32(rdr["MonthId"]);
                            //forecast2.Points = Convert.ToDecimal(rdr["Points"]);
                            //forecast2.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);

                            string monthsPoints = rdr["MonthId_Points"].ToString();
                            var arrMonthsPoints = monthsPoints.Split(',');
                            foreach(var item in arrMonthsPoints)
                            {
                                Forecast forecast = new Forecast();
                                string months_points = item;
                                if(months_points.IndexOf('_') > 0)
                                {
                                    var arrMonth_points = months_points.Split('_');
                                    forecast.Id = Convert.ToInt32(rdr["Id"]);
                                    forecast.Month = Convert.ToInt32(arrMonth_points[0]);
                                    forecast.Points = Convert.ToDecimal(arrMonth_points[1]);
                                    forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentId"]);
                                    forecast.CreatedBy = rdr["CreatedBy"].ToString();
                                    forecasts.Add(forecast);
                                }                                
                            }

                            //forecast2.CreatedBy = rdr["CreatedBy"].ToString();
                            //forecasts.Add(forecast2);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return forecasts;
            }
        }

        public List<Forecast> GetApprovalHistoriesByTimeStampId(int timeStampId)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM ApproveHistory WHERE TimeStampId=" + timeStampId;
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
                            //Forecast forecast2 = new Forecast();
                            //forecast2.Id = Convert.ToInt32(rdr["Id"]);
                            //forecast2.Month = Convert.ToInt32(rdr["MonthId"]);
                            //forecast2.Points = Convert.ToDecimal(rdr["Points"]);
                            //forecast2.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentsId"]);

                            string monthsPoints = rdr["MonthId_Points"].ToString();
                            var arrMonthsPoints = monthsPoints.Split(',');
                            foreach (var item in arrMonthsPoints)
                            {
                                Forecast forecast = new Forecast();
                                string months_points = item;
                                if (months_points.IndexOf('_') > 0)
                                {
                                    var arrMonth_points = months_points.Split('_');
                                    forecast.Id = Convert.ToInt32(rdr["Id"]);
                                    forecast.Month = Convert.ToInt32(arrMonth_points[0]);
                                    forecast.Points = Convert.ToDecimal(arrMonth_points[1]);
                                    forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeAssignmentId"]);
                                    forecast.CreatedBy = rdr["CreatedBy"].ToString();
                                    forecasts.Add(forecast);
                                }
                            }

                            //forecast2.CreatedBy = rdr["CreatedBy"].ToString();
                            //forecasts.Add(forecast2);
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
        public List<Forecast> GetPreviousManMonth(string pointsWithManMonths)
        {
            List<Forecast> forecasts = new List<Forecast>();
            if (!string.IsNullOrEmpty(pointsWithManMonths)) { 
                var arrPreviousManMonths = pointsWithManMonths.Split(',');
                foreach (var manMonthItem in arrPreviousManMonths)
                {
                    Forecast forecast = new Forecast();
                    var PreviousManMonthPointId = manMonthItem.Split('_');
                    forecast.Month = Convert.ToInt32(PreviousManMonthPointId[0]);
                    forecast.Points = Convert.ToDecimal(PreviousManMonthPointId[1]);
                    forecasts.Add(forecast);
                }
            }
            return forecasts;
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

        public List<Forecast> GetForecastHitostyForApproval(int assignmentId)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM ApprovedCosts WHERE ApprovedEmployeeAssignmentsId=" + assignmentId;
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
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["ApprovedEmployeeAssignmentsId"]);
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


        public List<Forecast> GetBudgetForecastsByAssignmentId(int assignmentId)
        {
            List<Forecast> forecasts = new List<Forecast>();
            string query = "";
            query = "SELECT * FROM BudgetCosts WHERE EmployeeBudgetId=" + assignmentId;
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
                            forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeBudgetId"]);
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

        public Forecast MatchForecastHistoryByAssignmentId(int assignmentId, DateTime date)
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
                        while (rdr.Read())
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

        public List<ForecastYear> GetBudgetYear()
        {
            List<ForecastYear> forecastYears = new List<ForecastYear>();
            DepartmentDAL _departmentDAL = new DepartmentDAL();

            string query = "";
            query = "select distinct Year from EmployeeeBudgets order by Year asc";
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
                            bool isInitialDataExists = _departmentDAL.CheckForBudgetInitialDataExists(forecastYear.Year);
                            bool isSecondHalfBudgetExists = _departmentDAL.CheckForBudgetSecondHalfDataExists(forecastYear.Year);                            
                            forecastYear.FirstHalfBudget = isInitialDataExists;                            
                            forecastYear.SecondHalfBudget = isSecondHalfBudgetExists;
                            //forecastYear.FinalizedBudget = rdr["FinalizedBudget"] is DBNull ? false : Convert.ToBoolean(rdr["FinalizedBudget"]);                            
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

        public List<ForecastYear> GetBudgetFinalizeYear()
        {
            List<ForecastYear> forecastYears = new List<ForecastYear>();
            string query = "";
            query = "SELECT DISTINCT Year FROM EmployeeeBudgets WHERE FinalizedBudget=1 ORDER BY Year ASC";
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
        public int GetLatestBudgetYear()
        {
            int latestBudgetYear = 0;
            string query = "";
            query = "SELECT MAX(eb.Year) 'Year' FROM EmployeeeBudgets eb ";
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
                            latestBudgetYear = Convert.ToInt32(rdr["Year"]);                            
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return latestBudgetYear;
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
                            if (rdr["FullName"] == DBNull.Value)
                            {
                                excelAssignmentDto.EmployeeName = null;
                            }
                            else
                            {
                                excelAssignmentDto.EmployeeName = rdr["FullName"].ToString();
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
        public List<ExcelAssignmentDto> GetEmployeesBudgetByYear(int year,int budgetType)
        {
            string strWhere = "";
            if(Convert.ToInt32(budgetType) == 2)
            {
                strWhere = "AND ea.SecondHalfBudget=1 AND ea.FinalizedBudget=1";
            }
            else
            {
                strWhere = "AND ea.FirstHalfBudget=1 AND ea.FinalizedBudget=1";
            }
            strWhere = "ea.Year="+ year+ " " + strWhere;
            string query = $@"select ea.id as AssignmentId,emp.Id as EmployeeId,emp.FullName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.IsActive,ea.GradeId,ea.EmployeeName 'DuplicateName'
                            from EmployeeeBudgets ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {strWhere} and 1=1
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
                            if (rdr["FullName"] == DBNull.Value)
                            {
                                excelAssignmentDto.EmployeeName = null;
                            }
                            else
                            {
                                excelAssignmentDto.EmployeeName = rdr["FullName"].ToString();
                            }
                            excelAssignmentDto.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            excelAssignmentDto.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            excelAssignmentDto.EmployeeModifiedName = rdr["DuplicateName"] is DBNull ? "" : rdr["DuplicateName"].ToString();

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

        public List<Forecast> GetForecastDetails(int assignmentId, int copyYear)
        {
            string query = "select Id,Year,MonthId,Points,Total,EmployeeAssignmentsId,CreatedBy,CreatedDate,UpdatedDate from Costs Where EmployeeAssignmentsId = " + assignmentId + " and Year=" + copyYear;

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
        public List<Forecast> GetBudgetForecastDetails(int assignmentId, int copyYear)
        {
            string query = "select Id,Year,MonthId,Points,Total,EmployeeBudgetId,CreatedBy,CreatedDate,UpdatedDate from BudgetCosts Where EmployeeBudgetId = " + assignmentId + " and Year=" + copyYear;

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
                            if (rdr["EmployeeBudgetId"] == DBNull.Value)
                            {
                                forecast.EmployeeAssignmentId = 0;
                            }
                            else
                            {
                                forecast.EmployeeAssignmentId = Convert.ToInt32(rdr["EmployeeBudgetId"]);
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

        public List<int> GetAssignmentYearList()
        {
            List<int> years = new List<int>();
            string query = "";

            query = "select distinct year from EmployeesAssignmentsWithCostsHistory where Year > 0 order by year";
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
        public List<int> GetApprovalAssignmentYearList()
        {
            List<int> years = new List<int>();
            string query = "";

            query = "select distinct Year from ApproveTimeStamps where Year > 0 order by year";
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
        public AssignmentHistoryViewModal GetAssignmentNamesForHistory(int assignmentId,int timeStampId,bool isOriginal)
        {
            AssignmentHistoryViewModal assignmentHistoryViewModal = new AssignmentHistoryViewModal();

            string query = "";
            query = "Select eh.Id,eh.TimeStampId,ea.EmployeeName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
            query = query + "   ,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName',g.GradePoints,eh.UnitPrice,ea.Remarks,eh.IsUpdate,eh.IsDeleted,eh.EmployeeId,e.FullName 'RootEmployeeName'   ";
            query = query + "From EmployeesAssignmentsWithCostsHistory eh  ";
            query = query + "    Left Join Employees e On eh.EmployeeId=e.Id ";
            query = query + "    Left Join Sections s On eh.SectionId = s.Id ";
            query = query + "    Left Join Departments d On eh.DepartmentId = d.Id ";
            query = query + "    Left Join InCharges i On eh.InChargeId = e.Id ";
            query = query + "    Left Join Roles r On eh.RoleId = r.Id ";
            query = query + "    Left Join Explanations ex On eh.ExplanationId = ex.Id ";
            query = query + "    Left Join Companies c On eh.CompanyId = c.Id ";
            query = query + "    Left Join Grades g On eh.GradeId = g.Id ";
            query = query + "    Left Join EmployeesAssignments ea On eh.EmployeeAssignmentId = ea.Id ";
            if (isOriginal)
            {
                query = query + "Where eh.EmployeeAssignmentId = " + assignmentId + " and eh.TimeStampId=" + timeStampId + " AND eh.IsOriginal=1";
            }
            else
            {
                query = query + "Where eh.EmployeeAssignmentId = " + assignmentId + " and eh.TimeStampId=" + timeStampId+ " AND (eh.IsOriginal IS NULL OR eh.IsOriginal=0) ";
            }            
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
                            assignmentHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);                            
                            assignmentHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            assignmentHistoryViewModal.RootEmployeeName = rdr["RootEmployeeName"] is DBNull ? "" : rdr["RootEmployeeName"].ToString();
                            assignmentHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            assignmentHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            assignmentHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            assignmentHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            assignmentHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            assignmentHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            assignmentHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            assignmentHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            assignmentHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            assignmentHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);
                            assignmentHistoryViewModal.IsDeleted = rdr["IsDeleted"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleted"]);
                            assignmentHistoryViewModal.MonthId_Points = rdr["MonthId_Points"] is DBNull ? "" : rdr["MonthId_Points"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistoryViewModal;
            }
        }

        public ApprovalHistoryViewModal GetApprovalNamesForHistory(int assignmentId, int timeStampId)
        {
            ApprovalHistoryViewModal _approvalHistoryViewModal = new ApprovalHistoryViewModal();

            string query = "";
            query = "Select eh.Id,eh.TimeStampId,ea.EmployeeName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
            query = query + "   ,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName',g.GradePoints,eh.UnitPrice,ea.Remarks,eh.IsUpdate   ";
            query = query + "   ,eh.IsAddEmployee,eh.IsDeleteEmployee,eh.IsCellWiseUpdate,e.Id 'EmployeeId',e.FullName ";
            query = query + "From ApproveHistory eh  ";
            query = query + "    Left Join Employees e On eh.EmployeeId=e.Id ";
            query = query + "    Left Join Sections s On eh.SectionId = s.Id ";
            query = query + "    Left Join Departments d On eh.DepartmentId = d.Id ";
            query = query + "    Left Join InCharges i On eh.InChargeId = e.Id ";
            query = query + "    Left Join Roles r On eh.RoleId = r.Id ";
            query = query + "    Left Join Explanations ex On eh.ExplanationId = ex.Id ";
            query = query + "    Left Join Companies c On eh.CompanyId = c.Id ";
            query = query + "    Left Join Grades g On eh.GradeId = g.Id ";
            //query = query + "    Left Join EmployeesAssignments ea On eh.EmployeeAssignmentId = ea.Id ";
            query = query + "    Left Join ApprovedEmployeesAssignments ea On eh.EmployeeAssignmentId = ea.AssignmentId And ea.ApprovedTimeStampId = " + timeStampId;
            query = query + "Where eh.EmployeeAssignmentId = " + assignmentId + " and eh.TimeStampId=" + timeStampId;
            query = query + " Order By ea.EmployeeName asc";

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
                            _approvalHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);
                            _approvalHistoryViewModal.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                            _approvalHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            _approvalHistoryViewModal.RootEmployeeName = rdr["FullName"] is DBNull ? "" : rdr["FullName"].ToString();
                            _approvalHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            _approvalHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            _approvalHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            _approvalHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            _approvalHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            _approvalHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            _approvalHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            _approvalHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            _approvalHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            _approvalHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);

                            _approvalHistoryViewModal.IsAddEmployee = rdr["IsAddEmployee"] is DBNull ? false : Convert.ToBoolean(rdr["IsAddEmployee"]);
                            _approvalHistoryViewModal.IsDeleteEmployee = rdr["IsDeleteEmployee"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleteEmployee"]);
                            _approvalHistoryViewModal.IsCellWiseUpdate = rdr["IsCellWiseUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsCellWiseUpdate"]);

                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return _approvalHistoryViewModal;
            }
        }

        public AssignmentHistoryViewModal GetOriginalForecastedData(int assignmentId)
        {
            AssignmentHistoryViewModal assignmentHistoryViewModal = new AssignmentHistoryViewModal();

            string query = "";
            query = query + " Select ea.Id,e.FullName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
	        query = query + "     ,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName',g.GradePoints,ea.UnitPrice,ea.Remarks ";
            query = query + " From EmployeesAssignments ea ";
            query = query + "     Left Join Employees e On ea.EmployeeId=e.Id ";
            query = query + "     Left Join Sections s On ea.SectionId = s.Id ";
            query = query + "     Left Join Departments d On ea.DepartmentId = d.Id ";
            query = query + "     Left Join InCharges i On ea.InChargeId = e.Id ";
            query = query + "     Left Join Roles r On ea.RoleId = r.Id ";
            query = query + "     Left Join Explanations ex On ea.ExplanationId = ex.Id ";
            query = query + "     Left Join Companies c On ea.CompanyId = c.Id ";
            query = query + "     Left Join Grades g On ea.GradeId = g.Id	";
            query = query + " Where ea.Id ="+assignmentId;

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
                            assignmentHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);                            
                            assignmentHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            assignmentHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            assignmentHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            assignmentHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            assignmentHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            assignmentHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            assignmentHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            assignmentHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            assignmentHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            assignmentHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            assignmentHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistoryViewModal;
            }
        }
        public AssignmentHistoryViewModal GetOriginalForecastedDataForApproval(int assignmentId,int timeStampId)
        {
            AssignmentHistoryViewModal assignmentHistoryViewModal = new AssignmentHistoryViewModal();

            string query = "";
            query = query + " Select ea.Id,e.FullName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
            query = query + "     ,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName',g.GradePoints,ea.UnitPrice,ea.Remarks ";
            query = query + " From ApprovedEmployeesAssignments ea ";
            query = query + "     Left Join Employees e On ea.EmployeeId=e.Id ";
            query = query + "     Left Join Sections s On ea.SectionId = s.Id ";
            query = query + "     Left Join Departments d On ea.DepartmentId = d.Id ";
            query = query + "     Left Join InCharges i On ea.InChargeId = e.Id ";
            query = query + "     Left Join Roles r On ea.RoleId = r.Id ";
            query = query + "     Left Join Explanations ex On ea.ExplanationId = ex.Id ";
            query = query + "     Left Join Companies c On ea.CompanyId = c.Id ";
            query = query + "     Left Join Grades g On ea.GradeId = g.Id	";
            query = query + " where ea.ApprovedTimeStampId=" + timeStampId + " and ea.AssignmentId=" + assignmentId+ "";

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
                            assignmentHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            assignmentHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            assignmentHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            assignmentHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            assignmentHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            assignmentHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            assignmentHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            assignmentHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            assignmentHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            assignmentHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            //assignmentHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistoryViewModal;
            }
        }

        public int CreateApproveTimeStamp(string approveTimeStamp,int year,string createdBy,DateTime createdDate)
        {
            int result = 0;
            string query = $@"insert into ApproveTimeStamps(ApproveTimeStamp,Year,CreatedBy,CreatedDate) values(@timeStamp,@year,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStamp", approveTimeStamp);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@createdBy", createdBy);
                cmd.Parameters.AddWithValue("@createdDate", createdDate);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }

                if (result > 0)
                {
                    var lastId = GetLastId("ApproveTimeStamps");
                    //CreateApprovetHistory(lastId);
                    //foreach (var item in assignmentHistories)
                    //{
                    //    CreateAssignmenttHistory(item, lastId, isUpdate);
                    //}
                    result = lastId;
                }
                return result;
            }
        }

        public int CreateApprovetHistory(int approveTimeStampId,int year,string createdBy, List<AssignmentHistory> _assignmentHistories_Add, List<AssignmentHistory> _assignmentHistorys_Delete, List<AssignmentHistory> _assignmentHistorys_CellWise)
        {
            int finalResults = 0;

            //add employee: approve history. note: there is not previous data to compare
            int results_Add = 0;
            //List<AssignmentHistory> _assignmentHistories_Add = new List<AssignmentHistory>();
            //_assignmentHistories_Add = GetAddEmployeeApprovedData(year);
            foreach(var addEmployeeItem in _assignmentHistories_Add)
            {
                addEmployeeItem.CreatedBy = createdBy;
                results_Add = ApproveHistory_AddEmployee(addEmployeeItem, approveTimeStampId, true);
            }

            //delete employee: approve history. note: there is not previous data to compare
            int results_Delete = 0;
            //List<AssignmentHistory> _assignmentHistorys_Delete = new List<AssignmentHistory>();
            //_assignmentHistorys_Delete = GetDeleteEmployeeApprovedData(year);
            foreach (var deleteEmployeeItem in _assignmentHistorys_Delete)
            {
                deleteEmployeeItem.CreatedBy = createdBy;
                results_Delete = ApproveHistory_DeleteEmployee(deleteEmployeeItem, approveTimeStampId, true);
            }

            //cell wise approve: approve history. note: compare the cells with previous data
            int results_Cells = 0;
            //List<AssignmentHistory> _assignmentHistorys_CellWise = new List<AssignmentHistory>();
            //_assignmentHistorys_CellWise = GetCellWiseEmployeeApprovedData(year);
            foreach (var cellWiseEmployeeItem in _assignmentHistorys_CellWise)
            {
                cellWiseEmployeeItem.CreatedBy = createdBy;
                results_Cells = ApproveHistory_CellWise(cellWiseEmployeeItem, approveTimeStampId, true);
            } 

            if(results_Add >0 || results_Delete>0 || results_Cells > 0)
            {
                finalResults = 1;
            }

            return finalResults;            
        }

        public AssignmentHistory GetAddEmployeeApprovedData(int assignmentId)
        {
            AssignmentHistory assignmentHistorie = new AssignmentHistory();

            string query = "";
            query = "select* from EmployeesAssignments where Id="+ assignmentId;            

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
                            assignmentHistorie.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistorie.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            assignmentHistorie.SectionId = rdr["SectionId"] is DBNull ? "" : rdr["SectionId"].ToString();
                            assignmentHistorie.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            assignmentHistorie.InChargeId = rdr["InChargeId"] is DBNull ? "" : rdr["InChargeId"].ToString();
                            assignmentHistorie.RoleId = rdr["RoleId"] is DBNull ? "" : rdr["RoleId"].ToString();
                            assignmentHistorie.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            assignmentHistorie.CompanyId = rdr["CompanyId"] is DBNull ? "" : rdr["CompanyId"].ToString();
                            assignmentHistorie.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]).ToString();
                            assignmentHistorie.GradeId = rdr["GradeId"] is DBNull ? "" : rdr["GradeId"].ToString();
                            assignmentHistorie.CreatedBy = rdr["CreatedBy"] is DBNull ? "" : rdr["CreatedBy"].ToString();
                            assignmentHistorie.UpdatedBy = rdr["UpdatedBy"] is DBNull ? "" : rdr["UpdatedBy"].ToString();
                            assignmentHistorie.EmployeeAssignmentId = rdr["Id"] is DBNull ? "" : rdr["Id"].ToString();
                            assignmentHistorie.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();

                            if (!string.IsNullOrEmpty(assignmentHistorie.Id.ToString()))
                            {
                                assignmentHistorie.MonthId_Points = GetForecastDataForHistory(assignmentHistorie.Id);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return assignmentHistorie;
        }

        public int ApproveHistory_AddEmployee(AssignmentHistory assignmentHistory, int timeStampId, bool isAddEmployee)
        {
            int result = 0;

            string query = $@"insert into ApproveHistory(TimeStampId,Year,EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,EmployeeAssignmentId,MonthId_Points,CreatedBy,CreatedDate,IsAddEmployee) values(@timeStampId,@year,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@employeeAssignmentId,@monthId_Points,@createdBy,@createdDate,@isAddEmployee)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStampId", timeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);
                cmd.Parameters.AddWithValue("@employeeId", assignmentHistory.EmployeeId);
                cmd.Parameters.AddWithValue("@sectionId", assignmentHistory.SectionId);
                cmd.Parameters.AddWithValue("@departmentId", assignmentHistory.DepartmentId);
                cmd.Parameters.AddWithValue("@inChargeId", assignmentHistory.InChargeId);
                cmd.Parameters.AddWithValue("@roleId", assignmentHistory.RoleId);
                cmd.Parameters.AddWithValue("@explanationId", assignmentHistory.ExplanationId);
                cmd.Parameters.AddWithValue("@companyId", assignmentHistory.CompanyId);
                cmd.Parameters.AddWithValue("@unitPrice", assignmentHistory.UnitPrice);
                cmd.Parameters.AddWithValue("@gradeId", assignmentHistory.GradeId);
                cmd.Parameters.AddWithValue("@employeeAssignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId_Points", assignmentHistory.MonthId_Points);
                cmd.Parameters.AddWithValue("@createdBy", assignmentHistory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@isAddEmployee", isAddEmployee);

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

        public AssignmentHistory GetDeleteEmployeeApprovedData(int assignmentId)
        {
            AssignmentHistory assignmentHistorie = new AssignmentHistory();

            string query = "";
            query = "select* from EmployeesAssignments Where Id="+ assignmentId;

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
                            assignmentHistorie.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistorie.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            assignmentHistorie.SectionId = rdr["SectionId"] is DBNull ? "" : rdr["SectionId"].ToString();
                            assignmentHistorie.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            assignmentHistorie.InChargeId = rdr["InChargeId"] is DBNull ? "" : rdr["InChargeId"].ToString();
                            assignmentHistorie.RoleId = rdr["RoleId"] is DBNull ? "" : rdr["RoleId"].ToString();
                            assignmentHistorie.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            assignmentHistorie.CompanyId = rdr["CompanyId"] is DBNull ? "" : rdr["CompanyId"].ToString();
                            assignmentHistorie.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]).ToString();
                            assignmentHistorie.GradeId = rdr["GradeId"] is DBNull ? "" : rdr["GradeId"].ToString();
                            assignmentHistorie.CreatedBy = rdr["CreatedBy"] is DBNull ? "" : rdr["CreatedBy"].ToString();
                            assignmentHistorie.UpdatedBy = rdr["UpdatedBy"] is DBNull ? "" : rdr["UpdatedBy"].ToString();
                            assignmentHistorie.EmployeeAssignmentId = rdr["Id"] is DBNull ? "" : rdr["Id"].ToString();
                            assignmentHistorie.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();

                            if (!string.IsNullOrEmpty(assignmentHistorie.Id.ToString()))
                            {
                                assignmentHistorie.MonthId_Points = GetForecastDataForHistory(assignmentHistorie.Id);
                            }

                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return assignmentHistorie;
        }

        public int ApproveHistory_DeleteEmployee(AssignmentHistory assignmentHistory, int timeStampId, bool isDeleteEmployee)
        {
            int result = 0;

            string query = $@"insert into ApproveHistory(TimeStampId,Year,EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,EmployeeAssignmentId,MonthId_Points,CreatedBy,CreatedDate,IsDeleteEmployee) values(@timeStampId,@year,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@employeeAssignmentId,@monthId_Points,@createdBy,@createdDate,@isDeleteEmployee)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStampId", timeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);
                cmd.Parameters.AddWithValue("@employeeId", assignmentHistory.EmployeeId);
                cmd.Parameters.AddWithValue("@sectionId", assignmentHistory.SectionId);
                cmd.Parameters.AddWithValue("@departmentId", assignmentHistory.DepartmentId);
                cmd.Parameters.AddWithValue("@inChargeId", assignmentHistory.InChargeId);
                cmd.Parameters.AddWithValue("@roleId", assignmentHistory.RoleId);
                cmd.Parameters.AddWithValue("@explanationId", assignmentHistory.ExplanationId);
                cmd.Parameters.AddWithValue("@companyId", assignmentHistory.CompanyId);
                cmd.Parameters.AddWithValue("@unitPrice", assignmentHistory.UnitPrice);
                cmd.Parameters.AddWithValue("@gradeId", assignmentHistory.GradeId);
                cmd.Parameters.AddWithValue("@employeeAssignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId_Points", assignmentHistory.MonthId_Points);
                cmd.Parameters.AddWithValue("@createdBy", assignmentHistory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@isDeleteEmployee", isDeleteEmployee);

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

        public AssignmentHistory GetCellWiseEmployeeApprovedData(int assignmentId, int year, int cellNo)
        {
            AssignmentHistory assignmentHistorie = new AssignmentHistory();

            string query = "";
            query = "select* from EmployeesAssignments Where Id="+assignmentId +" and Year="+year;            

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
                            assignmentHistorie.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistorie.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            assignmentHistorie.SectionId = rdr["SectionId"] is DBNull ? "" : rdr["SectionId"].ToString();
                            assignmentHistorie.DepartmentId = rdr["DepartmentId"] is DBNull ? "" : rdr["DepartmentId"].ToString();
                            assignmentHistorie.InChargeId = rdr["InChargeId"] is DBNull ? "" : rdr["InChargeId"].ToString();
                            assignmentHistorie.RoleId = rdr["RoleId"] is DBNull ? "" : rdr["RoleId"].ToString();
                            assignmentHistorie.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            assignmentHistorie.CompanyId = rdr["CompanyId"] is DBNull ? "" : rdr["CompanyId"].ToString();
                            assignmentHistorie.UnitPrice = Convert.ToDecimal(rdr["UnitPrice"]).ToString();
                            assignmentHistorie.GradeId = rdr["GradeId"] is DBNull ? "" : rdr["GradeId"].ToString();
                            assignmentHistorie.CreatedBy = rdr["CreatedBy"] is DBNull ? "" : rdr["CreatedBy"].ToString();
                            assignmentHistorie.UpdatedBy = rdr["UpdatedBy"] is DBNull ? "" : rdr["UpdatedBy"].ToString();
                            assignmentHistorie.EmployeeAssignmentId = rdr["Id"] is DBNull ? "" : rdr["Id"].ToString();
                            assignmentHistorie.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();
                            assignmentHistorie.ApprovedCells = cellNo.ToString();

                            if (!string.IsNullOrEmpty(assignmentHistorie.Id.ToString()))
                            {
                                assignmentHistorie.MonthId_Points = GetForecastDataForHistory(assignmentHistorie.Id);
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return assignmentHistorie;
        }

        public EmployeeAssignment GetAssignmentDetailsById(int assignmentId,int year)
        {
            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

            string query = "";
            query = "select * from EmployeesAssignments where Id="+ assignmentId + " and year= "+year;            

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
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();


                            employeeAssignment.BCYR = rdr["BCYR"] is DBNull ? false : Convert.ToBoolean(rdr["BCYR"]);
                            employeeAssignment.IsActive = rdr["IsActive"] is DBNull ? "" : rdr["IsActive"].ToString();
                            employeeAssignment.IsDeleted = rdr["IsDeleted"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleted"]);

                            employeeAssignment.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
                            employeeAssignment.IsDeletePending = rdr["IsDeletePending"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeletePending"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return employeeAssignment;
        }

        public List<EmployeeAssignment> GetAllUnapprovalDataForCells(int year)
        {
            List<EmployeeAssignment> employeeAssignments = new List<EmployeeAssignment>();

            string query = "";
            query = "select * from EmployeesAssignments where(BCYRCell is not null and BCYRCell <> '') Or(BCYRCellPending is not null and BCYRCellPending <> '') and BCYR<> 1 and IsActive = 1 and Year =" +year;

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
                            EmployeeAssignment employeeAssignment = new EmployeeAssignment();

                            employeeAssignment.Id = Convert.ToInt32(rdr["Id"]);
                            employeeAssignment.BCYRCell = rdr["BCYRCell"] is DBNull ? "" : rdr["BCYRCell"].ToString();
                            employeeAssignment.BCYRCellApproved = rdr["BCYRCellApproved"] is DBNull ? "" : rdr["BCYRCellApproved"].ToString();
                            employeeAssignment.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();

                            employeeAssignments.Add(employeeAssignment);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return employeeAssignments;
        }

        public int ApproveHistory_CellWise(AssignmentHistory assignmentHistory, int timeStampId, bool isCellWiseHistory)
        {
            int result = 0;

            string query = $@"insert into ApproveHistory(TimeStampId,Year,EmployeeId,SectionId,DepartmentId,InChargeId,RoleId,ExplanationId,CompanyId,UnitPrice,GradeId,EmployeeAssignmentId,MonthId_Points,CreatedBy,CreatedDate,IsCellWiseUpdate,ApprovedCells) values(@timeStampId,@year,@employeeId,@sectionId,@departmentId,@inChargeId,@roleId,@explanationId,@companyId,@unitPrice,@gradeId,@employeeAssignmentId,@monthId_Points,@createdBy,@createdDate,@isCellWiseUpdate,@approvedCells)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@timeStampId", timeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);
                cmd.Parameters.AddWithValue("@employeeId", assignmentHistory.EmployeeId);
                cmd.Parameters.AddWithValue("@sectionId", assignmentHistory.SectionId);
                cmd.Parameters.AddWithValue("@departmentId", assignmentHistory.DepartmentId);
                cmd.Parameters.AddWithValue("@inChargeId", assignmentHistory.InChargeId);
                cmd.Parameters.AddWithValue("@roleId", assignmentHistory.RoleId);
                cmd.Parameters.AddWithValue("@explanationId", assignmentHistory.ExplanationId);
                cmd.Parameters.AddWithValue("@companyId", assignmentHistory.CompanyId);
                cmd.Parameters.AddWithValue("@unitPrice", assignmentHistory.UnitPrice);
                cmd.Parameters.AddWithValue("@gradeId", assignmentHistory.GradeId);
                cmd.Parameters.AddWithValue("@employeeAssignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId_Points", assignmentHistory.MonthId_Points);
                cmd.Parameters.AddWithValue("@createdBy", assignmentHistory.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@isCellWiseUpdate", isCellWiseHistory);
                cmd.Parameters.AddWithValue("@approvedCells", assignmentHistory.ApprovedCells);

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
        public AssignmentHistoryViewModal GetCellWiseUpdatePreviousData(int assignmentId)
        {
            AssignmentHistoryViewModal assignmentHistoryViewModal = new AssignmentHistoryViewModal();
            string query = "";
            //query = "select top 1 * from EmployeesAssignmentsWithCostsHistory where EmployeeAssignmentId = "+assignmentId+" Order by Id desc ";
            query = "Select eh.Id,eh.TimeStampId,ea.EmployeeName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
            query = query + "   ,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName',g.GradePoints,eh.UnitPrice,ea.Remarks,eh.IsUpdate,eh.MonthId_Points ";
            query = query + "From EmployeesAssignmentsWithCostsHistory eh  ";
            query = query + "    Left Join Employees e On eh.EmployeeId=e.Id ";
            query = query + "    Left Join Sections s On eh.SectionId = s.Id ";
            query = query + "    Left Join Departments d On eh.DepartmentId = d.Id ";
            query = query + "    Left Join InCharges i On eh.InChargeId = i.Id ";
            query = query + "    Left Join Roles r On eh.RoleId = r.Id ";
            query = query + "    Left Join Explanations ex On eh.ExplanationId = ex.Id ";
            query = query + "    Left Join Companies c On eh.CompanyId = c.Id ";
            query = query + "    Left Join Grades g On eh.GradeId = g.Id ";
            query = query + "    Left Join EmployeesAssignments ea On eh.EmployeeAssignmentId = ea.Id ";
            query = query + "Where eh.EmployeeAssignmentId = " + assignmentId + " and (eh.IsOriginal = 0 or eh.IsOriginal is null)  Order by Id desc ";

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
                            assignmentHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            assignmentHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            assignmentHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            assignmentHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            assignmentHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            assignmentHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            assignmentHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            assignmentHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            assignmentHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            assignmentHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            assignmentHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);
                            assignmentHistoryViewModal.MonthId_Points = rdr["MonthId_Points"] is DBNull ? "" : rdr["MonthId_Points"].ToString();
                            
                            //if (!string.IsNullOrEmpty(assignmentHistories.Id.ToString()))
                            //{
                            //    assignmentHistories.MonthId_Points = GetForecastDataForHistory(assignmentHistories.Id);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistoryViewModal;
            }
        }
        public AssignmentHistoryViewModal GetCellWiseUpdateOriginalData(int assignmentId,int timeStampId)
        {
            AssignmentHistoryViewModal assignmentHistoryViewModal = new AssignmentHistoryViewModal();
            string query = "";
            query = query + "Select ah.Id,ah.TimeStampId,ea.EmployeeName 'EmployeeName',s.Name 'SectionName',d.Name 'DepartmentName' ";
            query = query + "	,i.Name 'InChargeName',r.Name 'RoleName',ex.Name 'ExplanationName',c.Name 'CompanyName' ";
            query = query + "	,g.GradePoints,ah.UnitPrice,ea.Remarks,ah.IsUpdate,ah.MonthId_Points ,ah.ApprovedCells ";
            query = query + "From ApproveHistory ah "; 
            query = query + "	Left Join Employees e On ah.EmployeeId = e.Id ";
            query = query + "	Left Join Sections s On ah.SectionId = s.Id ";
            query = query + "	Left Join Departments d On ah.DepartmentId = d.Id ";
            query = query + "	Left Join InCharges i On ah.InChargeId = i.Id "; 
            query = query + "	Left Join Roles r On ah.RoleId = r.Id "; 
            query = query + "	Left Join Explanations ex On ah.ExplanationId = ex.Id ";
            query = query + "	Left Join Companies c On ah.CompanyId = c.Id "; 
            query = query + "	Left Join Grades g On ah.GradeId = g.Id ";
            query = query + "	Left Join EmployeesAssignments ea On ah.EmployeeAssignmentId = ea.Id ";
            query = query + "Where ah.EmployeeAssignmentId = "+assignmentId+" and TimeStampId="+timeStampId+" Order by Id desc ";

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
                            assignmentHistoryViewModal.Id = Convert.ToInt32(rdr["Id"]);
                            assignmentHistoryViewModal.EmployeeName = rdr["EmployeeName"] is DBNull ? "" : rdr["EmployeeName"].ToString();
                            assignmentHistoryViewModal.SectionName = rdr["SectionName"] is DBNull ? "" : rdr["SectionName"].ToString();
                            assignmentHistoryViewModal.DepartmentName = rdr["DepartmentName"] is DBNull ? "" : rdr["DepartmentName"].ToString();
                            assignmentHistoryViewModal.InChargeName = rdr["InChargeName"] is DBNull ? "" : rdr["InChargeName"].ToString();
                            assignmentHistoryViewModal.RoleName = rdr["RoleName"] is DBNull ? "" : rdr["RoleName"].ToString();
                            assignmentHistoryViewModal.ExplanationName = rdr["ExplanationName"] is DBNull ? "" : rdr["ExplanationName"].ToString();
                            assignmentHistoryViewModal.CompanyName = rdr["CompanyName"] is DBNull ? "" : rdr["CompanyName"].ToString();
                            assignmentHistoryViewModal.GradePoints = rdr["GradePoints"] is DBNull ? "" : rdr["GradePoints"].ToString();
                            assignmentHistoryViewModal.UnitPrice = rdr["UnitPrice"] is DBNull ? "" : rdr["UnitPrice"].ToString();
                            assignmentHistoryViewModal.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            assignmentHistoryViewModal.IsUpdate = rdr["IsUpdate"] is DBNull ? false : Convert.ToBoolean(rdr["IsUpdate"]);
                            assignmentHistoryViewModal.MonthId_Points = rdr["MonthId_Points"] is DBNull ? "" : rdr["MonthId_Points"].ToString();
                            assignmentHistoryViewModal.ApprovedCells = rdr["ApprovedCells"] is DBNull ? "" : rdr["ApprovedCells"].ToString();
                            //if (!string.IsNullOrEmpty(assignmentHistories.Id.ToString()))
                            //{
                            //    assignmentHistories.MonthId_Points = GetForecastDataForHistory(assignmentHistories.Id);
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return assignmentHistoryViewModal;
            }
        }
        public string GetHistoryTimeStampName( int timeStampId)
        {
            string query = "";
            query = query + "select Id,TimeStamp from TimeStamps where id="+ timeStampId;

            string timeStampName = "";
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
                            timeStampName = rdr["TimeStamp"] is DBNull ? "" : rdr["TimeStamp"].ToString();                            
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return timeStampName;
            }
        }
        public string GetApproveHistoryTimeStampName(int timeStampId)
        {
            string query = "";
            query = query + "select Id,ApproveTimeStamp from ApproveTimeStamps where id=" + timeStampId;

            string timeStampName = "";
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
                            timeStampName = rdr["ApproveTimeStamp"] is DBNull ? "" : rdr["ApproveTimeStamp"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return timeStampName;
            }
        }
        public int UpdateEmployeeAssignmentApprovedCellsByAssignmentId(AssignmentHistory assignmentHistory)
        {
            int result = 0;

            string query = $@"Update EmployeesAssignments Set ApprovedCells = @approvedCells Where Id=@assignmentId And Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@approvedCells", assignmentHistory.ApprovedCells);
                cmd.Parameters.AddWithValue("@assignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);
                
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
        public string GetApprovedCellsByAssignmentId(string employeeAssignmentId)
        {
            string query = "";
            query = query + "SELECT Id,ApprovedCells FROM EmployeesAssignments WHERE Id= "+ employeeAssignmentId;

            string approvedCells = "";
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
                            approvedCells = rdr["ApprovedCells"] is DBNull ? "" : rdr["ApprovedCells"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return approvedCells;
            }
        }
        public int UpdateEmployeeAssignmentApprovedRowByAssignmentId(AssignmentHistory assignmentHistory)
        {
            int result = 0;

            string query = $@"Update EmployeesAssignments Set IsAddEmployee = @isAddEmployee Where Id=@assignmentId And Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isAddEmployee", 1);
                cmd.Parameters.AddWithValue("@assignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@year", assignmentHistory.Year);

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
        public List<ExcelAssignmentDto> GetApprovedForecastedDataByYear(int year)
        {
            string query = $@"
                                SELECT	ea.Id as AssignmentId,ea.EmployeeId,ea.SectionId,ea.DepartmentId,ea.InChargeId,ea.RoleId,ea.ExplanationId,ea.CompanyId,ea.UnitPrice,ea.GradeId
		                                ,ea.CreatedBy,ea.UpdatedBy,ea.CreatedDate,ea.UpdatedDate,ea.IsActive, ea.Remarks,ea.SubCode,ea.Year,ea.EmployeeName 'EmployeeModifiedName',ea.IsDeleted
		                                ,emp.FullName 'EmployeeRootName',sec.Name as SectionName, dep.Name as DepartmentName, inc.Name as InchargeName,rl.Name as RoleName, com.Name as CompanyName,gd.GradePoints,ea.BCYRCellPending, ea.IsRowPending
                                FROM EmployeesAssignments ea left join Sections sec on ea.SectionId = sec.Id
	                                LEFT JOIN Departments dep on ea.DepartmentId = dep.Id
	                                LEFT JOIN Companies com on ea.CompanyId = com.Id
	                                LEFT JOIN Roles rl on ea.RoleId = rl.Id
	                                LEFT JOIN InCharges inc on ea.InChargeId = inc.Id 
	                                LEFT JOIN Grades gd on ea.GradeId = gd.Id
	                                LEFT JOIN Employees emp on ea.EmployeeId = emp.Id
                                WHERE ea.Year={year} and 1=1
                                ORDER BY emp.Id ASC
                            ";

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

                            excelAssignmentDto.Id = rdr["AssignmentId"] is DBNull ? 0 : Convert.ToInt32(rdr["AssignmentId"]);
                            excelAssignmentDto.EmployeeId = rdr["EmployeeId"] is DBNull ? "" : rdr["EmployeeId"].ToString();
                            excelAssignmentDto.SectionId = rdr["SectionId"] is DBNull ? 0 : Convert.ToInt32(rdr["SectionId"]);
                            excelAssignmentDto.DepartmentId = rdr["DepartmentId"] is DBNull ? 0 : Convert.ToInt32(rdr["DepartmentId"]);
                            excelAssignmentDto.InchargeId = rdr["InchargeId"] is DBNull ? 0 : Convert.ToInt32(rdr["InchargeId"]);
                            excelAssignmentDto.RoleId = rdr["RoleId"] is DBNull ? 0 : Convert.ToInt32(rdr["RoleId"]);
                            excelAssignmentDto.ExplanationId = rdr["ExplanationId"] is DBNull ? 0 : Convert.ToInt32(rdr["ExplanationId"]);
                            excelAssignmentDto.CompanyId = rdr["CompanyId"] is DBNull ? 0 : Convert.ToInt32(rdr["CompanyId"]);
                            excelAssignmentDto.UnitPrice = rdr["UnitPrice"] is DBNull ? 0 : Convert.ToDecimal(rdr["UnitPrice"]);
                            excelAssignmentDto.GradeId = rdr["GradeId"] is DBNull ? 0 : Convert.ToInt32(rdr["GradeId"]);
                            excelAssignmentDto.EmployeeRootName = rdr["EmployeeRootName"] is DBNull ? "" : rdr["EmployeeRootName"].ToString();
                            excelAssignmentDto.EmployeeModifiedName = rdr["EmployeeModifiedName"] is DBNull ? "" : rdr["EmployeeModifiedName"].ToString();
                            excelAssignmentDto.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();
                            excelAssignmentDto.IsActive = rdr["IsActive"] is DBNull ? false : Convert.ToBoolean(rdr["IsActive"]);
                            excelAssignmentDto.IsDeleted = rdr["IsDeleted"] is DBNull ? false : Convert.ToBoolean(rdr["IsDeleted"]);
                            excelAssignmentDto.Year = rdr["Year"] is DBNull ? "" : rdr["Year"].ToString();
                            excelAssignmentDto.BCYRCellPending = rdr["BCYRCellPending"] is DBNull ? "" : rdr["BCYRCellPending"].ToString();
                            excelAssignmentDto.IsRowPending = rdr["IsRowPending"] is DBNull ? false : Convert.ToBoolean(rdr["IsRowPending"]);
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
        public List<Forecast> GetApprovedForecastedDataByAssignmentId(int assignmentId,int year)
        {
            string query = "select Id,Year,MonthId,Points,Total,EmployeeAssignmentsId,CreatedBy,CreatedDate,UpdatedDate from Costs Where EmployeeAssignmentsId = " + assignmentId + " and Year=" + year;

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
                                forecast.Points = Convert.ToDecimal(rdr["Points"]);
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
        public int CreateApprovedForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"insert into ApprovedCosts(Year,MonthId,Points,Total,ApprovedEmployeeAssignmentsId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@total,@approvedEmployeeAssignmentsId,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);
                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@approvedEmployeeAssignmentsId", forecast.EmployeeAssignmentId);
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
        public int UpdateApprovedData_AddRow(AssignmentHistory assignmentHistory,int approveTimeStampId,string assignmentYear)
        {
            int result = 0;

            string query = $@"Update ApprovedEmployeesAssignments Set IsAddEmployee = @isAddEmployee Where AssignmentId=@assignmentId AND ApprovedTimeStampId=@approvedTimeStampId And Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isAddEmployee", 1);
                cmd.Parameters.AddWithValue("@assignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@approvedTimeStampId", approveTimeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentYear);

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
        public int UpdateApprovedData_DeleteRow(AssignmentHistory assignmentHistory,int approveTimeStampId,string assignmentYear)
        {
            int result = 0;

            string query = $@"Update ApprovedEmployeesAssignments Set IsDeleteEmployee = @isDeleteEmployee Where AssignmentId=@assignmentId AND ApprovedTimeStampId=@approvedTimeStampId And Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isDeleteEmployee", 1);
                cmd.Parameters.AddWithValue("@assignmentId", assignmentHistory.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@approvedTimeStampId", approveTimeStampId);
                cmd.Parameters.AddWithValue("@year", assignmentYear);

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
        public string GetPreviousApprovedCells(string employeeAssignmentId)
        {
            string query = "";
            query = query + "SELECT Id,ApprovedCells FROM ApprovedEmployeesAssignments WHERE Id= " + employeeAssignmentId;

            string approvedCells = "";
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
                            approvedCells = rdr["ApprovedCells"] is DBNull ? "" : rdr["ApprovedCells"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return approvedCells;
            }
        }
        public int UpdateApprovedCells(int assingmentId, string cellNo, int approvedTimestampId,int year)
        {
            int result = 0;

            string query = $@"Update ApprovedEmployeesAssignments Set ApprovedCells = @approvedCells Where AssignmentId=@assignmentId AND ApprovedTimeStampId=@approvedTimeStampId And Year=@year";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@approvedCells", cellNo);
                cmd.Parameters.AddWithValue("@assignmentId", assingmentId);
                cmd.Parameters.AddWithValue("@approvedTimeStampId", approvedTimestampId);
                cmd.Parameters.AddWithValue("@year", year);

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
        public string GetApprovedDeletedId(int year,int approvedTimestampId)
        {
            string deletedAssignmentIds = "";
            string query = "";
            query = query + "select * from ApprovedEmployeesAssignments where(IsActive is null OR IsActive = 0) And IsDeleted = 1 And(IsDeleteEmployee is null Or IsDeleteEmployee = 0) AND Year = "+year+" AND ApprovedTimeStampId = "+ approvedTimestampId;                           
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
                            if (string.IsNullOrEmpty(deletedAssignmentIds))
                            {
                                deletedAssignmentIds = rdr["Id"] is DBNull ? "" : rdr["Id"].ToString();
                            }
                            else
                            {
                                deletedAssignmentIds = deletedAssignmentIds +","+ (rdr["Id"] is DBNull ? "" : rdr["Id"].ToString());
                            }                            
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return deletedAssignmentIds;
            }
        }
        public int DeletePreviousDeletedRowFromApprovedData(string deletedRowIds)
        {
            int result = 0;

            string query = $@"DELETE FROM ApprovedEmployeesAssignments WHERE Id in (@id)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", deletedRowIds);

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
        public int DeletePreviousDeletedCostRowFromApprovedData(string deletedRowIds)
        {
            int result = 0;

            string query = $@"DELETE FROM ApprovedCosts WHERE EmployeeAssignmentsId in (@employeeAssignmentsId)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", deletedRowIds);

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
        public int CleanPreviousApprovedDeletedRows(int year,int approvedTimestampId)
        {
            string deletedRowIds = GetApprovedDeletedId(year,approvedTimestampId);
            int deleteAssignmentResults = DeletePreviousDeletedRowFromApprovedData(deletedRowIds);
            int deleteCostsResults = DeletePreviousDeletedCostRowFromApprovedData(deletedRowIds);
                    
            if(deleteAssignmentResults>0 || deleteCostsResults > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public string GetApprovedCellsByTimestampId(int assingmentId, int cellNo, int approvedTimestampId, string year)
        {
            string query = "";
            query = query + "SELECT ApprovedCells FROM ApprovedEmployeesAssignments Where AssignmentId="+ assingmentId + " AND ApprovedTimeStampId="+ approvedTimestampId + " And Year="+ year;

            string approvedCells = "";
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
                            approvedCells = rdr["ApprovedCells"] is DBNull ? "" : rdr["ApprovedCells"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return approvedCells;
            }
        }
        public int UpdateOriginalForecast(Forecast forecast)
        {
            int result = 0;
            string query = $@"update CostsOrg set Points = @points, Total= @total, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Year=@year and EmployeeAssignmentsId=@employeeAssignmentsId and MonthId=@monthId";
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
        public int InsertOriginalForecast(Forecast forecast)
        {
            int result = 0;                                
            string query = $@"insert into CostsOrg(EmployeeAssignmentsId,Year,MonthId,Points,Total,CreatedBy,CreatedDate) values(@employeeAssignmentsId,@year,@monthId,@points,@total,@createdBy,@createdDate)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);


                cmd.Parameters.AddWithValue("@points", forecast.Points);
                cmd.Parameters.AddWithValue("@total", forecast.Total);
                cmd.Parameters.AddWithValue("@year", forecast.Year);
                cmd.Parameters.AddWithValue("@employeeAssignmentsId", forecast.EmployeeAssignmentId);
                cmd.Parameters.AddWithValue("@monthId", forecast.Month);

                cmd.Parameters.AddWithValue("@createdBy", forecast.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
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
        public bool GetReplicateYearForecastType(int year)
        {
            bool results = false;
            string query = "";
            query = "SELECT * FROM EmployeeeBudgets WHERE Year="+ year + " AND SecondHalfBudget=1 AND FinalizedBudget=1";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        results= true;
                    }
                }
                catch (Exception ex)
                {

                }

                return results;
            }
        }
        public int CreateForecastBudget(Forecast forecast)
        {
            int result = 0;
            string query = $@"insert into BudgetCosts(Year,MonthId,Points,Total,EmployeeBudgetId,CreatedBy,CreatedDate) values(@year,@monthId,@points,@total,@employeeAssignmentsId,@createdBy,@createdDate)";
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
                cmd.Parameters.AddWithValue("@createdDate", DateTime.Now);
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
    }
}