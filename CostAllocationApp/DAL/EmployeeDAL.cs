using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;


namespace CostAllocationApp.DAL
{
    public class EmployeeDAL : DbContext
    {
        public int GetLastId()
        {
            int result = 0;
            string query = $@"select max(Id) from Employees;";
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
        public int CreateEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"insert into Employees(FullName,CreatedBy,CreatedDate,IsActive) values(@fullName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@createdBy", employee.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", employee.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", employee.IsActive);
                try
                {
                    result = cmd.ExecuteNonQuery();
                    if (result>0)
                    {
                        result = GetLastId();
                    }
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }
        public int CheckForEmployeeName(string employeeName)
        {
            string query = "select Id,FullName from Employees where FullName=N'" + employeeName + "' AND Isactive=1";
            int result = 0;
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
                            result = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public List<Employee> GetAllEmployees(string orderby = "")
        {
            orderby = string.IsNullOrEmpty(orderby) ? "FullName" : orderby;
            List<Employee> employees = new List<Employee>();
            string query = "select * from employees where IsActive = 1 order by "+orderby+" asc";

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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                            employee.CreatedBy = rdr["CreatedBy"].ToString();
                            employee.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }

        public List<Employee> GetEmployeeListForBudgetEdit(int budgetYear,int budgetType)
        {
            List<Employee> employees = new List<Employee>();
            string query = "";
            if (budgetType == 1)
            {
                query = "EXEC SP_GetEmployeeListForFirstHalfBudget @year = "+ budgetYear + " ,@budgetType = "+ budgetType+" ";
            }
            else
            {
                query = "EXEC SP_GetEmployeeListForSecondHalfBudget @year = " + budgetYear + " ,@budgetType = " + budgetType + " ";
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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["EmployeeId"]);
                            employee.FullName = rdr["EmployeeName"].ToString();

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }
        public List<Employee> GetEmployeeListEmployeeAssignments(int assignmentYear)
        {
            List<Employee> employees = new List<Employee>();
            string query = "";
            query = "EXEC SP_GetEmployeeListForYearlyEdit @year = "+ assignmentYear + " ";

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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["EmployeeId"]);
                            employee.FullName = rdr["EmployeeName"].ToString();

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }
        public List<Employee> EmployeeListByNameFilter(string employeeName)
        {
            List<Employee> employees = new List<Employee>();
            string whereSql = "";
            if (employeeName.ToLower() == "all")
            {
                whereSql = "IsActive = 1";
            }
            else
            {
                whereSql = "FullName like N'%" + employeeName + "%' and IsActive = 1";
            }
            string query = "select * from employees where "+ whereSql + "  order by FullName asc";
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
                            Employee employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                            employee.CreatedBy = rdr["CreatedBy"].ToString();
                            employee.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

                            employees.Add(employee);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return employees;
            }
        }

        public int UpdateEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"update Employees set FullName=@fullName,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@fullName", employee.FullName);
                cmd.Parameters.AddWithValue("@updatedBy", employee.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employee.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employee.Id);
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

        public int InactiveEmployee(Employee employee)
        {
            int result = 0;
            string query = $@"update Employees set IsActive=@isActive,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@isActive", employee.IsActive);
                cmd.Parameters.AddWithValue("@updatedBy", employee.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", employee.UpdatedDate);
                cmd.Parameters.AddWithValue("@id", employee.Id);
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
        public bool CheckEmployeeDuplication(string employeeName)
        {
            string query = "select * from Employees where FullName=N'" + employeeName + "' And IsActive=1";
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

        public bool CheckUserNameDuplication(string userName)
        {
            string query = "select * from Users where UserName=N'" + userName + "'";
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
        public bool CheckUserEmailDuplication(string userEmail)
        {
            string query = "select * from Users where Email=N'" + userEmail + "'";
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

        public string GetEmployeeNameByAssignmentId(int assignmentId)
        {
            string fullName = "";
            string query = " select Employees.FullName from EmployeesAssignments join Employees on Employees.Id = EmployeesAssignments.EmployeeId where EmployeesAssignments.Id="+ assignmentId;

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
                            fullName = rdr["FullName"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return fullName;
            }
        }
        public int RemoveEmployees(int employeeId)
        {
            int result = 0;
            string query = $@"UPDATE Employees SET IsActive = 0 WHERE Id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", employeeId);
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
        public Employee GetEmployeeById(int employeeId)
        {
            Employee employee = null;
            string query = "select * from Employees where Id = " + employeeId;            
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
                            employee = new Employee();
                            employee.Id = Convert.ToInt32(rdr["Id"]);
                            employee.FullName = rdr["FullName"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    employee = null;
                }

                return employee;
            }
        }
    }
}