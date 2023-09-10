using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Models;
using CostAllocationApp.Dtos;

namespace CostAllocationApp.DAL
{
    public class TotalDAL: DbContext
    {
        public List<ForecastAssignmentViewModel> GetEmployeesForecastByDepartments_Company(int departmentId, string companyIds, int year)
        {

            string where = "";
            where += $" ea.DepartmentId = {departmentId} and ";

            string tempCompanyIds = "";
            string[] arrCompanyIds = companyIds.Split(new[] { "," }, StringSplitOptions.None);

            for (int i = 0; i < arrCompanyIds.Length; i++)
            {
                if (tempCompanyIds == "")
                {
                    tempCompanyIds = arrCompanyIds[i];
                }
                else
                {
                    tempCompanyIds = tempCompanyIds + "," + arrCompanyIds[i];
                }
            }
            where += $" ea.CompanyId In ({tempCompanyIds}) and ";
            where += $" ea.Year={year} and ";

            where += " 1=1";

            string query = $@"select ea.id as FinalBudgetId,emp.Id as EmployeeId,ea.EmployeeName,ea.SectionId, sec.Name as SectionName, ea.Remarks, ea.ExplanationId,
                            ea.DepartmentId, dep.Name as DepartmentName,ea.InChargeId, inc.Name as InchargeName,ea.RoleId,rl.Name as RoleName,ea.CompanyId, com.Name as CompanyName, ea.UnitPrice
                            ,gd.GradePoints,ea.GradeId
                            from EmployeeeFinalBudgets ea left join Sections sec on ea.SectionId = sec.Id
                            left join Departments dep on ea.DepartmentId = dep.Id
                            left join Companies com on ea.CompanyId = com.Id
                            left join Roles rl on ea.RoleId = rl.Id
                            left join InCharges inc on ea.InChargeId = inc.Id 
                            left join Grades gd on ea.GradeId = gd.Id
                            left join Employees emp on ea.EmployeeId = emp.Id
                            where {where}
                            order by emp.Id asc";


            List<ForecastAssignmentViewModel> forecastEmployeeAssignments = new List<ForecastAssignmentViewModel>();


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
                            ForecastAssignmentViewModel forecastEmployeeAssignmentViewModel = new ForecastAssignmentViewModel();
                            forecastEmployeeAssignmentViewModel.Id = Convert.ToInt32(rdr["FinalBudgetId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeId = rdr["EmployeeId"] is DBNull ? 0 : Convert.ToInt32(rdr["EmployeeId"]);
                            forecastEmployeeAssignmentViewModel.EmployeeName = rdr["EmployeeName"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionId = rdr["SectionId"].ToString();
                            forecastEmployeeAssignmentViewModel.SectionName = rdr["SectionName"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentId = rdr["DepartmentId"].ToString();
                            forecastEmployeeAssignmentViewModel.DepartmentName = rdr["DepartmentName"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeId = rdr["InchargeId"].ToString();
                            forecastEmployeeAssignmentViewModel.InchargeName = rdr["InchargeName"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleId = rdr["RoleId"].ToString();
                            forecastEmployeeAssignmentViewModel.RoleName = rdr["RoleName"].ToString();
                            forecastEmployeeAssignmentViewModel.ExplanationId = rdr["ExplanationId"] is DBNull ? "" : rdr["ExplanationId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyId = rdr["CompanyId"].ToString();
                            forecastEmployeeAssignmentViewModel.CompanyName = rdr["CompanyName"].ToString();
                            forecastEmployeeAssignmentViewModel.UnitPrice = Convert.ToInt32(rdr["UnitPrice"]).ToString();
                            forecastEmployeeAssignmentViewModel.GradeId = rdr["GradeId"].ToString();
                            forecastEmployeeAssignmentViewModel.GradePoint = rdr["GradePoints"].ToString();
                            forecastEmployeeAssignmentViewModel.Remarks = rdr["Remarks"] is DBNull ? "" : rdr["Remarks"].ToString();

                            forecastEmployeeAssignments.Add(forecastEmployeeAssignmentViewModel);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return forecastEmployeeAssignments;
        }


        public List<ForecastDto> GetForecastsByAssignmentId(int assignmentId, string year)
        {
            List<ForecastDto> forecasts = new List<ForecastDto>();
            string query = "select * from FinalBudgetCosts where EmployeeBudgetId=" + assignmentId + " and Year=" + year;
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
                            ForecastDto forecast = new ForecastDto();
                            forecast.ForecastId = Convert.ToInt32(rdr["Id"]);
                            forecast.Year = Convert.ToInt32(rdr["Year"]);
                            forecast.Month = Convert.ToInt32(rdr["MonthId"]);
                            forecast.Points = Convert.ToDecimal(rdr["Points"]);

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

        public int CreateDynamicTable(DynamicTable dynamicTable)
        {
            int result = 0;
            string query = $@"insert into DynamicTables(TableName,TableTitle,TablePosition,CreatedBy,CreatedDate,IsActive) values(@tableName,@tableTitle,@tablePosition,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@tableName", dynamicTable.TableName);
                cmd.Parameters.AddWithValue("@tableTitle", dynamicTable.TableTitle);
                cmd.Parameters.AddWithValue("@tablePosition", dynamicTable.TablePosition);
                cmd.Parameters.AddWithValue("@createdBy", dynamicTable.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", dynamicTable.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", dynamicTable.IsActive);
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

        public List<DynamicTable> GetAllDynamicTables()
        {
            List<DynamicTable> dynamicTables = new List<DynamicTable>();
            string query = "";
            query = "SELECT * FROM DynamicTables WHERE IsActive=1 ";
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
                            DynamicTable dynamicTable = new DynamicTable();
                            dynamicTable.Id = Convert.ToInt32(rdr["Id"]);
                            dynamicTable.TableName = rdr["TableName"].ToString();
                            dynamicTable.TableTitle = rdr["TableTitle"].ToString();
                            dynamicTable.TablePosition = Convert.ToInt32(rdr["TablePosition"]);
                            dynamicTable.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            dynamicTable.CreatedBy = rdr["CreatedBy"].ToString();

                            dynamicTables.Add(dynamicTable);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return dynamicTables;
            }
        }

        public int InactiveDynamicTable(DynamicTable dynamicTable)
        {
            int result = 0;
            string query = $@"update DynamicTables set IsActive=@isActive, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", dynamicTable.Id);
                cmd.Parameters.AddWithValue("@updatedBy", dynamicTable.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", dynamicTable.UpdatedDate);
                cmd.Parameters.AddWithValue("@isActive", dynamicTable.IsActive);
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

        public int UpdateDynamicTable(DynamicTable dynamicTable)
        {
            int result = 0;
            string query = $@"update DynamicTables set TableName = @tableName, TableTitle=@tableTitle,TablePosition=@tablePosition, UpdatedBy=@updatedBy, UpdatedDate=@updatedDate where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", dynamicTable.Id);
                cmd.Parameters.AddWithValue("@tableName", dynamicTable.TableName);
                cmd.Parameters.AddWithValue("@tableTitle", dynamicTable.TableTitle);
                cmd.Parameters.AddWithValue("@tablePosition", dynamicTable.TablePosition);
                cmd.Parameters.AddWithValue("@updatedBy", dynamicTable.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", dynamicTable.UpdatedDate);
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
        public DynamicTable GetAllDynamicTableById(string table_id)
        {
            DynamicTable dynamicTable = new DynamicTable ();
            string query = "";
            query = "SELECT * FROM DynamicTables WHERE IsActive=1 AND Id="+ table_id;
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
                            dynamicTable.Id = Convert.ToInt32(rdr["Id"]);
                            dynamicTable.TableName = rdr["TableName"].ToString();
                            dynamicTable.TableTitle = rdr["TableTitle"].ToString();
                            dynamicTable.TablePosition = Convert.ToInt32(rdr["TablePosition"]);
                            dynamicTable.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            dynamicTable.CreatedBy = rdr["CreatedBy"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return dynamicTable;
            }
        }
        public bool IsNameAndPositionExists(string tableName,int tablePoisition,int tableId,string checkType)
        {
            bool isExists = false;
            string query = "";
            if (checkType == "add")
            {
                query = "SELECT * FROM DynamicTables WHERE TableName= N'" + tableName + "' OR TablePosition=" + tablePoisition + " ";
            }
            else
            {
                query = "SELECT* FROM DynamicTables WHERE TableName = N'" + tableName + "' OR TablePosition = " + tablePoisition + " AND Id<>"+ tableId+" ";
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
                            isExists = true;
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return isExists;
            }
        }
    }
}