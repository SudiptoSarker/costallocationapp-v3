using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class UserRoleDAL: DbContext
    {
        public List<UserRole> GetAllUserRoles()
        {
            List<UserRole> userRoles = new List<UserRole>();
            string query = "select * from UserRoles";

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
                            UserRole userRole = new UserRole();
                            userRole.Id = Convert.ToInt32(rdr["Id"]);
                            userRole.Role = rdr["Role"].ToString();

                            userRoles.Add(userRole);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return userRoles;
            }
        }
    }
}