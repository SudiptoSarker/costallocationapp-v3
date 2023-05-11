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

        public int CreateUserLog(string userName,string token)
        {
            int result = 0;
            string query = $@"insert into UserLogs(UserName,LoginTime,Token) values(@userName,@loginTime,@token)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@loginTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@token", token);
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
            string query = $@"insert into Users(UserName,Title,DepartmentId,Email,Password,CreatedBy,CreatedDate,IsActive,UserRoleId) values(@userName,@departmentId,@title,@email,@password,@createdBy,@createdDate,@isActive,@userRoleId)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@departmentId", user.UserTitle);
                cmd.Parameters.AddWithValue("@title", user.DepartmentId);
                cmd.Parameters.AddWithValue("@email", user.Email==null? "" : user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@createdBy", user.CreatedBy);
                cmd.Parameters.AddWithValue("@createdDate", user.CreatedDate);
                cmd.Parameters.AddWithValue("@isActive", user.IsActive);
                cmd.Parameters.AddWithValue("@userRoleId", user.UserRoleId);
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
        public int UpdateUserName(User user)
        {
            int result = 0;
            string query = $@"update Users set UserName=@userName,Title=@title,DepartmentId=@departmentId,Email=@email,Password=@password,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate,UserRoleId=@userRoleId where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", user.UserName);
                cmd.Parameters.AddWithValue("@title", user.UserTitle);
                cmd.Parameters.AddWithValue("@departmentId", user.DepartmentId);
                cmd.Parameters.AddWithValue("@email", user.Email == null ? "" : user.Email);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@updatedBy", user.UpdatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", user.UpdatedDate);
                cmd.Parameters.AddWithValue("@userRoleId", user.UserRoleId);
                cmd.Parameters.AddWithValue("@id", user.Id);
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
            string query = "select u.Id,u.UserName,ur.Id as RoleId,ur.role,u.Title,u.DepartmentId,dpt.Name as DepartmentName,u.Email,u.Password,u.CreatedBy,u.CreatedDate ";
            query = query+"from users u ";
            query = query + "left join Departments dpt on u.DepartmentId = dpt.id left join userroles ur on u.UserRoleId=ur.Id Where u.IsActive = 1";

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
                            user.Id = Convert.ToInt32(rdr["Id"]);
                            user.UserName = rdr["UserName"].ToString();
                            user.UserTitle = rdr["Title"].ToString();
                            user.DepartmentId = rdr["DepartmentId"].ToString();
                            user.DepartmentName = rdr["DepartmentName"].ToString();
                            user.Email = rdr["Email"].ToString();
                            user.Password = rdr["Password"].ToString();
                            user.UserRoleName = rdr["role"].ToString();
                            //user.CreatedBy = rdr["CreatedBy"].ToString();
                            //user.CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]);
                            user.UserRoleId = Convert.ToInt32(rdr["RoleId"]);

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

        public bool GetUserLogByToken(string token)
        {
            bool result = false;
            string query = "select * from UserLogs where token='" + token + "'";

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

        public User GetUserByUserName(string userName)
        {
            User user = new User();

            string query = "select * from Users where username='"+userName+"'";

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
                            user.Id = Convert.ToInt32(rdr["Id"]);
                            user.UserName = rdr["UserName"].ToString();
                            user.UserRoleId = Convert.ToInt32(rdr["UserRoleId"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return user;
            }
        }

        public List<UserPermission> GetUserPermissionsByUserId(int userId)
        {
            List<UserPermission> userPermissions = new List<UserPermission>();

            string query = "select * from UserPermissions where userid=" + userId;

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
                            UserPermission userPermission = new UserPermission();
                            userPermission.Id = Convert.ToInt32(rdr["Id"]);
                            userPermission.Link = rdr["Link"].ToString();
                            userPermission.UserId = Convert.ToInt32(rdr["UserId"]);

                            userPermissions.Add(userPermission);
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return userPermissions;
            }
        }
        public int UpdateUserStatus(string userName, string changeRoleId, bool userStatus, string updatedBy, DateTime updatedDate)
        {
            int result = 0;
            string query = $@"update Users  set UserRoleId=@changeRoleId,IsActive=@userStatus,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where UserName=@userName";

            //string query = $@"update Users set UserName=@userName,Title=@title,DepartmentId=@departmentId,Email=@email,Password=@password,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate,UserRoleId=@userRoleId where Id=@id";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@changeRoleId", changeRoleId);
                cmd.Parameters.AddWithValue("@userStatus", userStatus);
                cmd.Parameters.AddWithValue("@updatedBy", updatedBy);
                cmd.Parameters.AddWithValue("@updatedDate", updatedDate);

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

        public int RemoveUserPermissions(int userId)
        {
            int result = 0;
            string query = $@"delete from UserPermissions where userid=@userid";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@userid", userId);
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

        public int CreateUserPermissions(string link, int userId)
        {
            int result = 0;
            string query = $@"insert into UserPermissions(Link,UserId) values(@link,@userId)";
            using (SqlConnection sqlConnection = this.GetConnection())
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@link", link);
                cmd.Parameters.AddWithValue("@userId", userId);
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