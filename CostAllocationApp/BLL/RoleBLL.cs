using CostAllocationApp.DAL;
using CostAllocationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace CostAllocationApp.BLL
{
    public class RoleBLL
    {
        RoleDAL roleDAL = null;
        public RoleBLL()
        {
            roleDAL = new RoleDAL();
        }
        public int CreateRole(Role role)
        {
            return roleDAL.CreateRole(role);
        }
        public int UpdateRole(Role role)
        {
            return roleDAL.UpdateRole(role);
        }
        public List<Role> GetAllRoles()
        {
            return roleDAL.GetAllRoles();
        }
        public int RemoveRoles(int roleIds)
        {
            return roleDAL.RemoveRoles(roleIds);
        }
        public bool CheckRole(string roleName)
        {
            return roleDAL.CheckRole(roleName);
        }
        public int GetRoleCountWithEmployeeAsignment(int roleId)
        {
            return roleDAL.GetRoleCountWithEmployeeAsignment(roleId);
        }
        public Role GetRoleByRoleId(int roleId)
        {
            return roleDAL.GetRoleByRoleId(roleId);
        }
        public int RetrieveRoleIdByRoleName(string roleName, string userName)
        {
            Role objRole = new Role();
            int roleId = 0;

            if (!string.IsNullOrEmpty(roleName))
            {
                roleId = roleDAL.GetRoleIdByName(roleName);

                if (roleId > 0)
                {
                    return roleId;
                }
                else
                {
                    objRole.CreatedBy = userName;
                    objRole.CreatedDate = DateTime.Now;
                    objRole.IsActive = true;
                    objRole.RoleName = roleName;

                    int result = roleDAL.CreateRole(objRole);
                    roleId = roleDAL.GetRoleIdByName(roleName);
                    return roleId;
                }
            }
            else
            {
                return roleId;
            }

        }
    }
}