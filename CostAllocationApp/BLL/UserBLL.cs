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
        public int CreateUserLog(string userName)
        {
            return userDAL.CreateUserLog(userName);
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
    }
}