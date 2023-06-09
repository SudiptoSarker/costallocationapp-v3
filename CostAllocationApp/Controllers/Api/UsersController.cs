﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CostAllocationApp.BLL;
using CostAllocationApp.Models;
using System;

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
            string userToken = "invalid";
            if (!String.IsNullOrEmpty(user.UserName) && !String.IsNullOrEmpty(user.Password))
            {
                var result = _userBLL.CheckUser(user.UserName.ToString(), user.Password.ToString());
                if (result == true)
                {
                    var loggedInUsers = _userBLL.GetAllUserLogs().Where(u=>u.UserName== user.UserName).ToList();
                    if (loggedInUsers.Count>0)
                    {
                        userToken = "invalid-1";
                    }
                    else
                    {
                        userToken = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        _userBLL.CreateUserLog(user.UserName, userToken);
                       
                    }  
                }
            }

            return Ok(userToken);
        }
    }
}
