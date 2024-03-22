using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Macreel_Project.Models;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Infosoft.Controllers
{
    public class AccountController : Controller
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        //DataAccess db = new DataAccess();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LoginCheck(string UserName = "", string Password = "")
        {
            string Message = "";
            Login obj = new Login();
            obj = db.CheckLogin(UserName, Password);
            if (obj.UserName != null)
            {
                if (obj.Type == "Admin")
                {
                    Message = "A";
                    Session["Login"] = obj;
                }
                else if (obj.Type == "Emp")
                {
                    Message = "E";
                    Session["Login"] = obj;
                }
                else if (obj.Type == "Client")
                {
                    Message = "C";
                    Session["Login"] = obj;
                }
            }
            else
            {
                Message = "F";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LogOut()
        {
            string Msg = "";
            Msg = "s";
            Session.Abandon();
            Session.Clear();
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
    }
}