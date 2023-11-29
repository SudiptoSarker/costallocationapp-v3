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
        public List<User> GetAllUsers(string orderBy = "", string orderType = "")
        {
            return userDAL.GetAllUsers(orderBy, orderType);
        }
        public List<User> GetSearchedUsers(string searchOption="", string searchBy="")
        {
            return userDAL.GetSearchedUsers(searchOption, searchBy);
        }
        public List<User> GetFilteredUsers(string filterRole = "", string filterTitle = "", string filterDept = "", string filterStatus = "")
        {
            return userDAL.GetFilteredUsers(filterRole, filterTitle, filterDept, filterStatus);
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
        public int UpdateUserStatus(string userName, string changeRoleId, bool userStatus, string updatedBy, DateTime updatedDate)
        {
            return userDAL.UpdateUserStatus(userName, changeRoleId, userStatus, updatedBy, updatedDate);
        }
        public int RemoveUserPermissions(int userId)
        {
            return userDAL.RemoveUserPermissions(userId);
        }
        public int CreateUserPermissions(string link, int userId)
        {
            return userDAL.CreateUserPermissions(link,userId);
        }
        public string GetUserRoleByUserName(string userName)
        {
            return userDAL.GetUserRoleByUserName(userName);
        }
    }
}