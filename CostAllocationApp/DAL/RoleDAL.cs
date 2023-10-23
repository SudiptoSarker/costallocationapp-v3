using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class RoleDAL : DbContext
    {
        public int CreateRole(Role role)
        {
            int result = 0;
            string query = $@"insert into Roles(Name,CreatedBy,CreatedDate,IsActive) values(@roleName,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@roleName", role.RoleName);
                cmd.Parameters.AddWithValue("@createdBy", role.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", role.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", role.IsActive);
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
        public int UpdateRole(Role role)
        {
            int result = 0;
            string query = "";
            query = query + "UPDATE Roles ";
            query = query + "SET Name = N'" + role.RoleName + "',UpdatedBy=N'" + role.UpdatedBy + "',UpdatedDate='" + role.UpdatedDate + "',IsActive=1";
            query = query + "WHERE Id = " + role.Id + " ";

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
        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            string query = "";
            query = "SELECT * FROM Roles WHERE IsActive=1 ";
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
                            Role role = new Role();
                            role.Id = Convert.ToInt32(rdr["Id"]);
                            role.RoleName = rdr["Name"].ToString();
                            role.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            role.CreatedBy = rdr["CreatedBy"].ToString();

                            roles.Add(role);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return roles;
            }
        }
        public int RemoveRoles(int roleIds)
        {
            int result = 0;            
            string query = $@"DELETE FROM Roles WHERE id = @id ";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", roleIds);
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

        public bool CheckRole(string roleName)
        {
            string query = "select * from Roles where Name=N'" + roleName + "'";
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
        public int GetRoleCountWithEmployeeAsignment(int roleId)
        {
            string query = "select * from EmployeesAssignments where RoleId=" + roleId;
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

        public Role GetRoleByRoleId(int roleId)
        {
            Role role = null;
            string query = "select * from Roles where Id = " + roleId;
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
                            role = new Role();
                            role.RoleName = rdr["Name"].ToString();
                            role.Id = Convert.ToInt32(rdr["Id"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    role = null;
                }

                return role;
            }
        }
        public int GetRoleIdByName(string role)
        {
            int roleId = 0;
            string where = "";
            string query = "";
            if (!string.IsNullOrEmpty(role))
            {
                where += $" Name =N'{role}'";
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
    }
}