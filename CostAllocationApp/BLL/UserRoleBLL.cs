using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CostAllocationApp.DAL;
using CostAllocationApp.Models;

namespace CostAllocationApp.BLL
{
    public class UserRoleBLL
    {
        UserRoleDAL roleDAL = null;
        public UserRoleBLL()
        {
            roleDAL = new UserRoleDAL();
        }
        public List<UserRole> GetAllUserRoles()
        {
            return roleDAL.GetAllUserRoles();
        }        
    }
}