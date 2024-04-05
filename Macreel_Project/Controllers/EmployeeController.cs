using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Macreel_Project.Models;
using static Macreel_Project.Models.Bussiness;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
namespace Macreel_Infosoft.Controllers
{
    //[Filters]
    public class EmployeeController : Controller
    {
        // GET: Employee
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        //DataAccess db = new DataAccess();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddEmployee(int Id = 0)
        {
            List<SelectListItem> getdeprtlist = new List<SelectListItem>();
            getdeprtlist = db.GetDepartment();
            ViewBag.getdeprtlist = getdeprtlist;
            List<SelectListItem> getdeslist = new List<SelectListItem>();
            getdeslist = db.GetDesgnation();
            ViewBag.getdeslist = getdeslist;
            List<Login> getdata = new List<Login>();
            getdata = db.LoginData();
            ViewBag.login = getdata;
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            Employee obj = new Employee();
            if (Id != 0)
            {
                obj = db.GetEmployeeProfile(Id);
                string StateId = obj.StateId;
                List<SelectListItem> City = new List<SelectListItem>();
                City = db.GetCity(StateId);
                ViewBag.City = City;
            }

            return View(obj);
        }
        public ActionResult EmployeeList()
        {
            List<Employee> getlist = new List<Employee>();
            getlist = db.GetEmployyeListByRe();
            ViewBag.Emp = getlist;
            return View();
        }
        [HttpPost]
        public JsonResult City(string StateId)
        {
            List<SelectListItem> City = new List<SelectListItem>();
            City = db.GetCity(StateId);
            ViewBag.City = City;
            return Json(City, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditEmployeeProfile(int Id = 0)
        {
            List<SelectListItem> getdeprtlist = new List<SelectListItem>();
            getdeprtlist = db.GetDepartment();
            ViewBag.getdeprtlist = getdeprtlist;
            List<SelectListItem> getdeslist = new List<SelectListItem>();
            getdeslist = db.GetDesgnation();
            ViewBag.getdeslist = getdeslist;
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            Employee obj = new Employee();
            if (Id != 0)
            {
                obj = db.GetEmployeeProfile(Id);
                string StateId = obj.StateId;
                List<SelectListItem> City = new List<SelectListItem>();
                City = db.GetCity(StateId);
                ViewBag.City = City;
            }
            return View(obj);
        }
        public ActionResult ApplyLeave(int Id = 0)
        {
            int TotalLWP= 0;
            TotalLWP = db.TotalLWP();
            ViewBag.TotalLWP = TotalLWP;
            int TotalEL = 0;
            TotalEL = db.TotalEL();
            ViewBag.TotalEL = TotalEL;
            int TotalML = 0;
            TotalML = db.TotalML();
            ViewBag.TotalML = TotalML;
            int TotalCL = 0;
            TotalCL = db.TotalCL();
            ViewBag.TotalCL = TotalCL;
            int TotalApprovedLWPL = 0;
            TotalApprovedLWPL = db.TotalLWPApproved();
            ViewBag.TotalApprovedLWPL = TotalApprovedLWPL;
            int TotalApprovedEL = 0;
            TotalApprovedEL = db.TotalELApproved();
            ViewBag.TotalApprovedEL = TotalApprovedEL;
            int TotalApprovedML = 0;
            TotalApprovedML = db.TotalMLApproved();
            ViewBag.TotalApprovedML = TotalApprovedML;
            int TotalApprovedCL = 0;
            TotalApprovedCL = db.TotalCLApproved();
            ViewBag.TotalApprovedCL = TotalApprovedCL;
            int RemainCL = 0;
            RemainCL = db.CL();
            ViewBag.RemCL = RemainCL;
            int RemainML = 0;
            RemainML = db.ML();
            ViewBag.RemML = RemainML;
            int RemainEL = 0;
            RemainEL = db.EL();
            ViewBag.RemEL = RemainEL;
            int RemainLWP = 0;
            RemainLWP = db.LWP();
            ViewBag.RemLWP = RemainLWP;
            ApplyLeave obj = new ApplyLeave();
            List<SelectListItem> list = new List<SelectListItem>();
            list = db.BindLeveTypeForEmployee();
            ViewBag.list = list;
            List<ApplyLeave> blist = new List<ApplyLeave>();
            blist = db.BindApplyLeave();
            ViewBag.ApplyLeave = blist;
            if (Id != 0)
            {
                obj = db.EditApplyLeave(Id);
            }
            return View(obj);
        }
        [HttpPost]
        public JsonResult InsertApplyLeave(ApplyLeave obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateApplyLeave(obj);
                if (row > 0)
                {
                    Message = "update";
                }
                else
                {
                    Message = "error";
                }
            }
            else
            {
                row = db.InsertApplyLeave(obj);
                if (row > 0)
                {
                    Message = "success";
                }
                else
                {
                    Message = "error";
                }
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewEmpCL()
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetClEmp();
            ViewBag.Leave = obj;

            return View();
        }
        public ActionResult Comptask_view()
        {
            List<TaskManage> obj = new List<TaskManage>();
            obj = db.ViewCompleteTask();
            ViewBag.Leave = obj;
            return View(obj);
            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}

        }
        public ActionResult TotalLeave_view()
        {
            List<Assignleave> obj = new List<Assignleave>();
            obj = db.ViewTotalLeave();
            ViewBag.Leave1 = obj;
            return View(obj);
            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}

        }
        public ActionResult Compdelet_task(string id)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "DeleteCompleteTask");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("Comptask_view");
        }
        public ActionResult ViewEmpML()
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetMLEmp();
            ViewBag.Leave = obj;
            return View();
        }
        public ActionResult ViewEmpEL()
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetELEmp();
            ViewBag.Leave = obj;
            return View();
        }
        public ActionResult ViewEmpWPL()
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetWPLEmp();
            ViewBag.Leave = obj;
            return View();
        }
        public ActionResult ApplyLeaveView(int Id = 0)
        {
            ApplyLeave obj = new ApplyLeave();
            obj = db.EditApplyLeave(Id);
            List<SelectListItem> list = new List<SelectListItem>();
            list = db.BindLeveTypeForEmployee();
            ViewBag.list = list;
            return View(obj);
        }
        public ActionResult EmployeeAssignProject()
        {
            List<Project> gtlst = new List<Project>();
            gtlst = db.GetEmployeeProjectList();
            ViewBag.gtlst = gtlst;
            return View();
        }
        public ActionResult ViewProjectAssignEmployee(string ProjectCode = "")
        {
            string Emp = "";
            Emp = db.ProjectEmployee(ProjectCode);
            ViewBag.Emp = Emp;
            string Descript = "";
            Descript = db.ProjectEmployeeDescription(ProjectCode);
            ViewBag.desc = Descript;
            List<Project> gtl = new List<Project>();
            gtl = db.getDocumentaion(ProjectCode);
            ViewBag.Document = gtl;
            string YourDescript = "";
            YourDescript = db.ProjectYourEmployeeDescription(ProjectCode);
            ViewBag.YourDescript = YourDescript;
            return View();
        }
        public ActionResult TaskSheet()
        {
            TaskSheets obj = new TaskSheets();
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindProjectForTaskSheet();
            ViewBag.Project = getlist;
            return View(obj);
        }
        public JsonResult BindProject()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindProject();
            return Json(getlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertTask(TaskSheets obj)
        {
            string Message = "";
            int row = 0;
            for (int i = 0; i < obj.Date.Length; i++)
            {
                row = db.InsertTask(obj.Date[i], obj.Project[i], obj.Task[i], obj.Hour[i], obj.Description[i], obj.Status[i], obj.Remark[i]);
            }
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "error";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TaskSheetList()
        {
            List<TaskSheets> list = new List<TaskSheets>();
            list = db.GetTasklistForEmployee();
            ViewBag.list = list;
            return View();
        }
        public JsonResult UpdateProjectStatus(Project obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdateProjectStatus(obj);
            if (row > 0)
            {
                Message = "Status Successfully Added,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApplyLeaveList()
        {
            List<ApplyLeave> blist = new List<ApplyLeave>();
            blist = db.BindApplyLeave();
            ViewBag.ApplyLeave = blist;
            return View();
        }
        public ActionResult CreateLead(int Id = 0)
        {
            Lead obj = new Lead();
            if (Id != 0)
            {
                obj = db.EditLead(Id);
            }
            List<Lead> list = new List<Lead>();
            list = db.AllLeadForEmployee();
            ViewBag.list = list;
            return View(obj);
        }
        public ActionResult AllLeadofEmp()
        {
            List<Lead> list = new List<Lead>();
            list = db.AllLeadForEmployee();
            ViewBag.list = list;
            List<SelectListItem> GTLST = new List<SelectListItem>();
            GTLST = db.GetEmployyeListfORlEAVEaSSIGN();
            ViewBag.EMP = GTLST;
            return View();
        }
        public JsonResult InsertLead(Lead obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateEmpLead(obj);
                if (row > 0)
                {
                    Message = "Successfully Updated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                row = db.InsertEmpLead(obj);
                if (row > 0)
                {
                    Message = "Successfully Inserted!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateLeadStatus(Lead obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdateLeadStatus(obj);
            int count = 0;
            count = db.InsertEmpLeadStatus(obj);
            if (row > 0)
            {
                Message = "Successfully,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Mailer()
        {
            return View();
        }
        public ActionResult ComposeMail()
        {
            return View();
        }
        public JsonResult InsertClientProblem(TokenManagement obj)
        {
            string Message = "";
            int row = 0;
            string UserName = "";
            UserName = ((Login)HttpContext.Session["Login"]).UserName;
            string Name = UserName.Substring(0, 3);
            Random rn = new Random();
            int N0 = 0;
            N0 = rn.Next(1000, 9999);
            obj.TokenId = "MS-" + Name + "0" + N0;
            if (obj.File != null)
            {
                foreach (var file in obj.File)
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var extention = Path.GetExtension(file.FileName);
                        var path = "/ProjectDocument/" + UserName + obj.TokenId + fileName;
                        file.SaveAs(Server.MapPath(path));
                        obj.FilePath = path;
                        db.InsertAttachFile(obj);
                    }
                }
            }
            row = db.InsertClientProblem(obj);
            //string message = "Welcome To, <h3 style='color:red'>Macreel Infosoft PVT. LTD.</h3><br/> <br/> Your Subject is :  " + obj.subject + "<br/>Your Priority is :" + obj.Periority + "<br/><br/>We are happy to serve you.";
            //db.sendemail(obj.Subject, message);
            if (row > 0)
            {

                Message = "s";
            }
            else
            {
                Message = "f";
            }


            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Inbox()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            getlist = db.GetInbox();
            ViewBag.EmailList = getlist;
            return View();
        }
        public ActionResult ViewLeadStatusHistory(int LeadId = 0)
        {
            List<Lead> list = new List<Lead>();
            list = db.GetStatusHistory(LeadId);
            ViewBag.list = list;
            return View();
        }
        public ActionResult ViewLead(int Id = 0)
        {
            Lead obj = new Lead();
            obj = db.EditLead(Id);
            return View(obj);
        }
        public ActionResult InboxDetail(int Id = 0)
        {
            TokenManagement obj = new TokenManagement();
            obj = db.GetInboxDetail(Id);
            List<TokenManagement> list = new List<TokenManagement>();
            list = db.Getcomment(Id);
            ViewBag.list = list;
            return View(obj);
        }
        public JsonResult InsertComment(TokenManagement obj)
        {
            string Message = "";
            int row = 0;
            row = db.InsertComment(obj);
            if (row > 0)
            {
                Message = "Comment Successfully Added,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Transh()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            getlist = db.GetTransh();
            ViewBag.EmailList = getlist;
            return View();
        }
        [HttpPost]
        public JsonResult DeleteTicket(string Id)
        {
            string Message = "";
            int row = 0;
            string[] Ids;
            Ids = Id.Split(',');
            for (int i = 0; i < Ids.Length; i++)
            {
                row = db.DeleteToken(Ids[i]);
            }
            if (row > 0)
            {
                Message = "s";
            }
            else
            {
                Message = "f";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckTaskSheetDate(string Date = "")
        {
            string Message = "";
            List<TaskSheets> list = new List<TaskSheets>();
            list = db.CheckTaskSheetDate(Date);
            if (list.Count != 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateTask(string Date = "")
        {
            List<TaskSheets> list = new List<TaskSheets>();
            TaskSheets obj = new TaskSheets();
            list = db.EditTaskSheet(Date);
            ViewBag.list = list;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindProjectForTaskSheet();
            ViewBag.Project = getlist;
            return View(obj);
        }
        [HttpPost]
        public JsonResult UpdateTaskSheet(TaskSheets obj)
        {
            string Message = "";
            int row = 0;
            int del = 0;
            del = db.DeleteTask();
            for (int i = 0; i < obj.Date.Length; i++)
            {
                row = db.InsertTask(obj.Date[i], obj.Project[i], obj.Task[i], obj.Hour[i], obj.Description[i], obj.Status[i], obj.Remark[i]);
            }
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "error";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Soap()
        {
            return View();
        }
    }
}