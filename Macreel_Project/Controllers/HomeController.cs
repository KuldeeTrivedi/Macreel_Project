using Macreel_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Infosoft.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

        static HomeController()
        {
            RenewProduct obj = new RenewProduct();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

    }
}