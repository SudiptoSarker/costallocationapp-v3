using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CostAllocationApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["token"] == null)
            {
                return RedirectToAction("Login", "Registration");
            }
            if (BLL.UserBLL.GetUserLogByToken(Session["token"].ToString()) == false)
            {
                //Session["token"] = null;
                //Session["userName"] = null;
                Session.Abandon();
                return RedirectToAction("Login", "Registration");
            }
            return View();
        }
    }
}