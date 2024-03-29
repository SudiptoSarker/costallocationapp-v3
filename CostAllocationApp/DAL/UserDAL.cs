﻿using System;
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
            string query = "select * from Users where UserName=N'" + userName + "'"+" and Password=N'"+password+ "' and Isactive=1 and UserRoleId !=0";
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
            string query = $@"update Users set UserName=@userName,Title=@title,DepartmentId=@departmentId,Email=@email,Password=@password,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate,UserRoleId=@userRoleId,IsActive=@isActive where Id=@id";
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
        public List<User> GetAllUsers(string orderBy = "", string orderType = "")
        {
            List<User> users = new List<User>();
            string query = "select u.Id,u.UserName,ur.Id as RoleId,ur.role,u.Title,u.DepartmentId,dpt.Name as DepartmentName,u.Email,u.Password,u.CreatedBy,u.CreatedDate,u.Isactive ";
            query = query + "from users u ";
            query = query + "left join Departments dpt on u.DepartmentId = dpt.id left join userroles ur on u.UserRoleId=ur.Id ";

            if(orderBy != "")
            {
                string orderQuery = "order by " + orderBy;
                orderQuery += orderType != "" ? " " + orderType : " asc";
                query += orderQuery;
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
                            User user = new User();
                            user.Id = Convert.ToInt32(rdr["Id"]);
                            user.UserName = rdr["UserName"].ToString();                            
                            user.UserTitle = rdr["Title"].ToString();
                            user.DepartmentId = rdr["DepartmentId"].ToString();
                            user.DepartmentName = rdr["DepartmentName"].ToString();
                            user.Email = rdr["Email"].ToString();
                            user.Password = rdr["Password"].ToString();
                            user.UserRoleName = rdr["role"].ToString();
                            user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            if (string.IsNullOrEmpty(rdr["RoleId"].ToString()))
                            {
                                user.UserRoleId = "0";
                                user.Status = "Invalid_" + user.IsActive;
                            }
                            else
                            {
                                user.UserRoleId = rdr["RoleId"].ToString();
                                user.Status = "Valid_" + user.IsActive;
                            }
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

        public List<User> GetSearchedUsers(string searchOption = "", string searchBy = "")
        {
            string whereQuery = "";
            if (searchOption != "" || searchBy != "")
            {
                whereQuery = "where ";
                whereQuery += (searchOption != "" && searchBy != "") ? searchOption + "='" + searchBy + "'" : "";
            }
            List<User> users = new List<User>();
            string query = "select u.Id,u.UserName,ur.Id as RoleId,ur.role,u.Title,u.DepartmentId,dpt.Name as DepartmentName,u.Email,u.Password,u.CreatedBy,u.CreatedDate,u.Isactive ";
            query = query + "from users u ";
            query = query + "left join Departments dpt on u.DepartmentId = dpt.id left join userroles ur on u.UserRoleId=ur.Id ";
            query = query + whereQuery;

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
                            user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            if (string.IsNullOrEmpty(rdr["RoleId"].ToString()))
                            {
                                user.UserRoleId = "0";
                                user.Status = "Invalid_" + user.IsActive;
                            }
                            else
                            {
                                user.UserRoleId = rdr["RoleId"].ToString();
                                user.Status = "Valid_" + user.IsActive;
                            }
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

        public List<User> GetFilteredUsers(string filterRole = "", string filterTitle = "", string filterDept = "", string filterStatus = "")
        {
            string whereQuery = "";
            List<string> queryList = new List<string>();
            if (filterRole != "" || filterTitle != "" || filterDept != "" || filterStatus != "")
            {
                whereQuery = "where ";
                if (filterRole != "") queryList.Add("UserRoleId="+filterRole);
                if (filterTitle != "") queryList.Add("Title='" + filterTitle + "'");
                if (filterDept != "") queryList.Add("DepartmentId=" + filterDept);
                if (filterStatus != "")
                {
                    string statusQuery = filterStatus == "3" ? "UserRoleId=0" : "u.Isactive=" + filterStatus;
                    queryList.Add(statusQuery);
                }
                string queryString = string.Join(" and ", queryList);
                whereQuery += queryString;
            }

            string query = "select u.Id,u.UserName,ur.Id as RoleId,ur.role,u.Title,u.DepartmentId,dpt.Name as DepartmentName,u.Email,u.Password,u.CreatedBy,u.CreatedDate,u.Isactive ";
            query = query + "from users u ";
            query = query + "left join Departments dpt on u.DepartmentId = dpt.id left join userroles ur on u.UserRoleId=ur.Id ";
            query = query + whereQuery;

            List<User> users = new List<User>();
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
                            user.IsActive = Convert.ToBoolean(rdr["IsActive"]);
                            if (string.IsNullOrEmpty(rdr["RoleId"].ToString()))
                            {
                                user.UserRoleId = "0";
                                user.Status = "Invalid_" + user.IsActive;
                            }
                            else
                            {
                                user.UserRoleId = rdr["RoleId"].ToString();
                                user.Status = "Valid_" + user.IsActive;
                            }
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

            string query = "select * from Users where username='" + userName + "'";

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
                            user.UserRoleId = rdr["UserRoleId"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                return user;
            }
        }

        public int UpdateUserStatus(string userName, string changeRoleId, bool userStatus, string updatedBy, DateTime updatedDate)
        {
            int result = 0;
            string query = $@"update Users  set UserRoleId=@changeRoleId,IsActive=@userStatus,UpdatedBy=@updatedBy,UpdatedDate=@updatedDate where UserName=@userName";

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
        public string GetUserRoleByUserName(string userName)
        {
            string userRoleType = "";

            string query = "";
            query = query + "SELECT u.UserName,ur.Role ";
            query = query + "FROM Users u ";
            query = query + "    INNER JOIN UserRoles ur ON u.UserRoleId = ur.Id ";
            query = query + "WHERE u.UserName=N'"+ userName + "' AND u.IsActive=1 ";

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
                            userRoleType = rdr["Role"].ToString().ToLower();                            
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return userRoleType;
        }
    }
}