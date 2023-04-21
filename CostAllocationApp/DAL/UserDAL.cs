using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using CostAllocationApp.Models;

namespace CostAllocationApp.DAL
{
    public class UserDAL : DbContext
    {
        public bool CheckUser(string userName, string password)
        {
            string query = "select * from Users where UserName=N'" + userName + "'"+" and Password=N'"+password+"'";
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

        public int CreateUserLog(string userName)
        {
            int result = 0;
            string query = $@"insert into UserLogs(UserName,LoginTime) values(@userName,@loginTime)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@loginTime", DateTime.Now);
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

        public List<User> GetUserLogs()
        {
            List<User> users = new List<User>();
            string query = "select * from UserLogs";
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
                            User user = new User();
                            user.UserName = rdr["UserName"].ToString();
                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return users;
            }
        }
        public int CreateUserName(User user)
        {
            int result = 0;
            string query = $@"insert into Users(UserName,Title,DepartmentId,Email,Password,CreatedBy,CreatedDate,IsActive) values(@userName,@departmentId,@title,@email,@password,@createdBy,@createdDate,@isActive)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@departmentId", user.UserTitle);
                cmd.Parameters.AddWithValue("@title", user.DepartmentId);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@createdBy", user.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", user.IsActive);
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
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            string query = "select u.UserName,u.Title,u.DepartmentId,dpt.Name as DepartmentName,u.Email,u.Password,u.CreatedBy,u.CreatedDate ";
            query = query+"from users u ";
            query = query + "left join Departments dpt on u.DepartmentId = dpt.id Where u.IsActive = 1";

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
                            User user = new User();                            
                            user.UserName = rdr["UserName"].ToString();
                            user.UserTitle = rdr["Title"].ToString();
                            user.DepartmentId = rdr["DepartmentId"].ToString();
                            user.DepartmentName = rdr["DepartmentName"].ToString();
                            user.Email = rdr["Email"].ToString();
                            user.Password = rdr["Password"].ToString();

                            //user.CreatedBy = rdr["CreatedBy"].ToString();
                            //user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);

                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return users;
            }
        }

        public int RemoveUser(string userName)
        {
            int result = 0;
            string query = $@"delete from UserLogs where UserName=@userName";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", userName);
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

        public List<User> GetAllUserLogs()
        {
            List<User> users = new List<User>();
            string query = "select * from UserLogs";

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
                            User user = new User();
                            user.UserName = rdr["UserName"].ToString();

                            users.Add(user);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return users;
            }
        }

        public User GetUserLog(string userName)
        {
            User user = new User();
            string query = "select * from UserLogs where username='"+userName+"'";

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
                            
                            user.UserName = rdr["UserName"].ToString();
                            user.LoginTime = Convert.ToDateTime(rdr["LoginTime"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return user;
            }
        }
    }
}