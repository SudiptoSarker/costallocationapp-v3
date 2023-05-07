using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class UserBLL
    {
        UserDAL userDAL = null;

        public UserBLL()
        {
            userDAL = new UserDAL();
        }

        public bool CheckUser(string userName, string password)
        {
            return userDAL.CheckUser(userName, password);
        }
        public int CreateUserLog(string userName,string token)
        {
            return userDAL.CreateUserLog(userName,token);
        }
        public List<User> GetUserLogs()
        {
            return userDAL.GetUserLogs();
        }
        public int CreateUserName(User user)
        {
            return userDAL.CreateUserName(user);
        }
        public List<User> GetAllUsers()
        {
            return userDAL.GetAllUsers();
        }
        public int RemoveUser(string userName)
        {
            return userDAL.RemoveUser(userName);
        }
        public List<User> GetAllUserLogs()
        {
            return userDAL.GetAllUserLogs();
        }
        public User GetUserLog(string userName)
        {
            return userDAL.GetUserLog(userName);
        }
        public static bool GetUserLogByToken(string token)
        {
            UserDAL user_dal = new UserDAL();
            return user_dal.GetUserLogByToken(token);
        }

        public User GetUserByUserName(string userName)
        {
            return userDAL.GetUserByUserName(userName);
        }

        public List<UserPermission> GetUserPermissionsByUserId(int userId)
        {
            return userDAL.GetUserPermissionsByUserId(userId);
        }
        public int UpdateUserName(User user)
        {
            return userDAL.UpdateUserName(user);
        }
    }
}