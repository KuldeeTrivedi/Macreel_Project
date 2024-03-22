using Macreel_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        //DataAccess db = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ClientDashBoard()
        {
            List<Project> list = new List<Project>();
            list = db.GetClientProjectList();
            ViewBag.gtlst = list;
            return View();
        }
        public ActionResult MyProfile(int Id = 0)
        {

            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            Client obj = new Client();
            obj = db.EditClient(Id);
            string StateId = obj.State;
            List<SelectListItem> City = new List<SelectListItem>();
            City = db.GetCity(StateId);
            ViewBag.City = City;
            return View(obj);
        }
        public ActionResult EditOrofile(int Id = 0)
        {
            Client obj = new Client();
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            if (Id != 0)
            {
                obj = db.EditClient(Id);
                string StateId = obj.State;
                List<SelectListItem> City = new List<SelectListItem>();
                City = db.GetCity(StateId);
                ViewBag.City = City;
            }
            return View(obj);
        }
        public ActionResult Invoice()
        {
            List<performa> LIST = new List<performa>();
            LIST = db.GetClientGanratedInvoiceList();
            ViewBag.List = LIST;
            return View();
        }
        public ActionResult TaxInvoicePreview(string InvoiceNo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<performa> product = new List<performa>();
            product = db.GetTaxProduct(InvoiceNo);
            ViewBag.product = product;
            performa obj = new performa();
            obj = db.GetInvoicePreview(InvoiceNo);
            double TR1 = Convert.ToDouble(obj.AfterTaxAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult SupportTicket()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            getlist = db.GetInboxForAdmin();
            ViewBag.EmailList = getlist;
            return View();
        }
    }
}