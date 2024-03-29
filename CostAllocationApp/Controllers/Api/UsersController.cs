﻿using System;
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
        /*
         Description: check user.
         Type: POST
        */
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
                        foreach (var item in loggedInUsers)
                        {
                            _userBLL.RemoveUser(item.UserName);
                        }
                        userToken = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                        _userBLL.CreateUserLog(user.UserName, userToken);
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
