using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.BLL;
using CostAllocationApp.Models;

namespace CostAllocationApp.Controllers.Api
{
    public class UsersController : ApiController
    {
        UserBLL _userBLL = null;
        public UsersController()
        {
            _userBLL = new UserBLL();
        }
        // GET: Users
        [HttpPost]
        public IHttpActionResult CheckUser(User user)
        {
            bool userCheck = false;
            if (!String.IsNullOrEmpty(user.UserName) && !String.IsNullOrEmpty(user.Password))
            {
                userCheck = _userBLL.CheckUser(user.UserName.ToString(), user.Password.ToString());
                if (userCheck==true)
                {
                    var result = _userBLL.CreateUserLog(user.UserName);
                }
            }

            return Ok(userCheck);
        }
    }
}
