using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;
using CostAllocationApp.ViewModels;
using CostAllocationApp.Dtos;
using System.Globalization;


namespace CostAllocationApp.DAL
{
    public class UploadExcelDAL : DbContext
    {

        public int? GetSectionIdByName(string sectionName)
        {
            int? sectionId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(sectionName))
            {
                where += $" Name =N'{sectionName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Sections
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
                                sectionId = Convert.ToInt32(rdr["Id"]);
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
            return sectionId;
        }

        public int? GetDepartmentIdByName(string departmentName)
        {
            int? departmentId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(departmentName))
            {
                where += $" Name =N'{departmentName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Departments
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
                                departmentId = Convert.ToInt32(rdr["Id"]);
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

            return departmentId;
        }

        public int? GetInchargeIdByName(string inchargeName)
        {
            int? inchargeId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(inchargeName))
            {
                where += $" Name =N'{inchargeName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM InCharges
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
                                inchargeId = Convert.ToInt32(rdr["Id"]);
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

            return inchargeId;
        }

        public int? GetRoleIdByName(string roleName)
        {
            int? roleId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(roleName))
            {
                where += $" Name =N'{roleName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Roles
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
                                roleId = Convert.ToInt32(rdr["Id"]);
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

            return roleId;
        }

        public int? GetExplanationIdByName(string explanationName)
        {
            int? explanationId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(explanationName))
            {
                where += $" Name =N'{explanationName}'";
                where += " AND IsActive=1 ";

                query = $@"select Id,Name FROM Explanations
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
                                explanationId = Convert.ToInt32(rdr["Id"]);
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

            return explanationId;
        }

        public int? GetCompanyIdByName(string companyName)
        {
            int? companyId = null;
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

        //get grade id by grade name
        public int? GetGradeIdByGradeName(string gradePoints)
        {
            int? gradeId = null;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(gradePoints))
            {
                where += $" gradepoints =N'{gradePoints}'";
                where += " AND IsActive=1 ";

                query = $@"select Id, gradepoints, gradelowpoints from Grades
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
                                gradeId = Convert.ToInt32(rdr["Id"]);
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

            return gradeId;
        }
        
        //get unit pirce by grade name
        public double GetUnitPriceByGradeName(string gradePoints)
        {
            double unitPrice = 0;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(gradePoints))
            {
                where += $" gradepoints =N'{gradePoints}'";
                where += " AND IsActive=1 ";

                query = $@"select Id, gradepoints, gradelowpoints from Grades
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
                                unitPrice = Convert.ToDouble(rdr["gradelowpoints"]);
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

            return unitPrice;
        }

    }
}