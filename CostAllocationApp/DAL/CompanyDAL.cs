using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class CompanyDAL : DbContext
    {
        public int CreateCompany(Company company)
        {
            int result = 0;
            string query = $@"insert into Companies(Name,CreatedBy,CreatedDate,IsActive) values(@companyName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@companyName", company.CompanyName);
                cmd.Parameters.AddWithValue("@createdBy", company.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", company.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", company.IsActive);
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

        public int UpdateCompany(Company company)
        {
            int result = 0;
            string query = "";
            query = query + "UPDATE Companies ";
            query = query + "SET Name = N'" + company.CompanyName + "',UpdatedBy=N'" + company.UpdatedBy + "',UpdatedDate='" + company.UpdatedDate + "',IsActive=1";
            query = query + "WHERE Id = " + company.Id + " ";

            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    return result = 0;
                }

                return result;
            }

        }

        public List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            string query = "";
            query = "SELECT * FROM Companies WHERE IsActive=1 ";
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
                            Company company = new Company();
                            company.Id = Convert.ToInt32(rdr["Id"]);
                            company.CompanyName = rdr["Name"].ToString();
                            company.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            company.CreatedBy = rdr["CreatedBy"].ToString();

                            companies.Add(company);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return companies;
            }
        }

        public int RemoveCompanies(int companyIds)
        {
            int result = 0;            
            string query = $@"DELETE FROM Companies WHERE id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", companyIds);
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

        public bool CheckCompany(string companyName)
        {
            string query = "select * from Companies where Name=N'" + companyName + "'";
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

        public int GetCompanyCountWithEmployeeAsignment(int companyId)
        {
            string query = "select * from EmployeesAssignments where CompanyId=" + companyId;
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
                            result++;
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return result;
        }

        public Company GetCompanyByCompanyId(int companyId)
        {
            Company company = null;
            string query = "select * from Companies where Id = " + companyId;
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
                        while (rdr.Read())
                        {
                            company = new Company();
                            company.CompanyName = rdr["Name"].ToString();
                            company.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    company = null;
                }

                return company;
            }
        }

        public int GetCompanyIdByName(string companyName)
        {
            int companyId = 0;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(companyName))
            {
                where += $" Name =N'{companyName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Companies
                            where {where}";

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
                                companyId = Convert.ToInt32(rdr["Id"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Response.Write(ex);
                        HttpContext.Current.Response.End();
                    }
                }

            }

            return companyId;
        }
    }
}