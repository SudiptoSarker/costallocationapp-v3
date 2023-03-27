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
                }
                catch (Exception ex)
                {

                }

                return result;
            }

        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string query = "";
            query = "SELECT * ";
            query = query + "FROM Employees ";
            query = query + "WHERE Isactive=1 ";
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

    }
}