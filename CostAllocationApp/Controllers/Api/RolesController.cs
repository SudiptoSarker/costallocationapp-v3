using System;
using System.Collections.Generic;
using System.Web.Http;
using CostAllocationApp.Models;
using CostAllocationApp.BLL;

namespace CostAllocationApp.Controllers.Api
{
    public class RolesController : ApiController
    {
        RoleBLL roleBLL = null;
        public RolesController()
        {
            roleBLL = new RoleBLL();
        }

        [HttpPost]
        public IHttpActionResult CreateRole(Role role)
        {
            var session = System.Web.HttpContext.Current.Session;
            if (String.IsNullOrEmpty(role.RoleName))
            {
                return BadRequest("Role Name Required");
            }
            else
            {
                if (roleBLL.CheckRole(role.RoleName))
                {
                    return BadRequest("役割は登録済みです!!!");
                }
                else
                {
                    int result =0;
                    if (role.IsUpdate)
                    {
                        role.UpdatedBy = session["userName"].ToString();
                        role.UpdatedDate = DateTime.Now;
                        role.IsActive = true;
                        result = roleBLL.UpdateRole(role);

                   }
                    else
                    {
                        role.CreatedBy = session["userName"].ToString();
                        role.CreatedDate = DateTime.Now;
                        role.IsActive = true;
                        result = roleBLL.CreateRole(role);
                    }

                    if (result > 0)
                    {
                        return Ok("データが保存されました!");
                    }
                    else
                    {
                        return BadRequest("Something Went Wrong!!!");
                    }
                }
            }
        }
        [HttpGet]
        public IHttpActionResult Roles()
        {
            List<Role> roles = roleBLL.GetAllRoles();
            return Ok(roles);
        }

        [HttpDelete]
        public IHttpActionResult RemoveRoles([FromUri] string roleIds)
        {
            int result = 0;


            if (!String.IsNullOrEmpty(roleIds))
            {
                string[] ids = roleIds.Split(',');

                foreach (var item in ids)
                {
                    result += roleBLL.RemoveRoles(Convert.ToInt32(item));
                }

                if (result == ids.Length)
                {
                    return Ok("正常に削除がされました!");
                }
                else
                {
                    return BadRequest("Something Went Wrong!!!");
                }
            }
            else
            {
                return BadRequest("Select InCharge Id!");
            }

        }
    }
}