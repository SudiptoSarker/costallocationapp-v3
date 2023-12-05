using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace CostAllocationApp.Models
{
    public class Utility
    {

        //public string Address { get; set; } = "http://198.38.92.119:8090";
        //public string Address { get; set; } = "http://198.38.92.119:4545";
        public string Address { get; set; } = ConfigurationManager.AppSettings["url"];


        public bool CheckSession()
        {
            bool isLogedIn = true;
            var session = System.Web.HttpContext.Current.Session;
            if (session["token"] == null)
            {
                isLogedIn = false;
            }
            else
            {
                if (BLL.UserBLL.GetUserLogByToken(session["token"].ToString()) == false)
                {
                    isLogedIn = false;
                }
            }
            
            return isLogedIn;
        }
    }
}