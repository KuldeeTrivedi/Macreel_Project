using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Macreel_Project.Models;
using static Macreel_Project.Models.Bussiness;
using System.IO;
//using SelectPdf;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Globalization;
using System.Text;
namespace Macreel_Infosoft.Controllers
{
    //[Filters]
    public class AdminController : Controller
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        //DataAccess db = new DataAccess();
        // GET: Admin
        public ActionResult Index()
        {
            int TotalEmp = 0;
            int TotalClient = 0;
            int TotalProject = 0;
            int AssignProject = 0;
            TotalEmp = db.CountEmployee();
            ViewBag.TotalEmp = TotalEmp;
            TotalClient = db.CountClient();
            ViewBag.TotalClient = TotalClient;
            TotalProject = db.CountProject();
            ViewBag.TotalProject = TotalProject;
            AssignProject = db.CountAssignProject();
            ViewBag.AssignProject = AssignProject;
            List<Employee> getlist = new List<Employee>();
            getlist = db.GetEmployyeList();
            ViewBag.Emp = getlist;
            List<Client> gtlist = new List<Client>();
            gtlist = db.BindClient();
            ViewBag.list = gtlist;
            List<Project> gtlst = new List<Project>();
            gtlst = db.GetProjectList();
            ViewBag.gtlst = gtlst;
            return View();
        }
        public ActionResult AddDepartment(int Id = 0, string Type = "")
        {
            Deprtment obj = new Deprtment();
            List<Deprtment> deptlist = new List<Deprtment>();
            deptlist = db.BindDepartment();
            ViewBag.deptlist = deptlist;
            List<designation> designationlist = new List<designation>();
            designationlist = db.BindDesignation();
            ViewBag.designationlist = designationlist;
            if (Type == "Dept")
            {
                obj = db.EditDepartment(Id);
            }
            else
            {
                obj = db.EditDesignation(Id);
            }
            return View(obj);
        }
        public ActionResult AddDesignation(int Id = 0, string Type = "")
        {
            Deprtment obj = new Deprtment();
            List<Deprtment> deptlist = new List<Deprtment>();
            deptlist = db.BindDepartment();
            ViewBag.deptlist = deptlist;
            List<designation> designationlist = new List<designation>();
            designationlist = db.BindDesignation();
            ViewBag.designationlist = designationlist;
            if (Type == "Dept")
            {
                obj = db.EditDepartment(Id);
            }
            else
            {
                obj = db.EditDesignation(Id);
            }
            return View(obj);
        }
        public ActionResult ViewDesignation()
        {
            return View();
        }
        public ActionResult ViewDepartment()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Department(Deprtment obj)
        {
            string Message = "";
            int row = 0;
            if (obj.DeptId != 0)
            {
                row = db.UpdateDepartment(obj);
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
                row = db.InsertDepartment(obj);
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
        [HttpPost]
        public JsonResult Designation(designation obj)
        {
            string Message = "";
            int row = 0;
            if (obj.DId != 0)
            {
                row = db.UpdateDesignation(obj);
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
                row = db.InsertDesignation(obj);
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
        [HttpPost]
        public JsonResult DeleteDepartment(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteDepartment(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteDesignation(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteDesignation(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEmployee(Employee obj)
        {
            int row = 0;
            string Message = "";
            string Name = obj.UserName.Substring(0, 3);
            Random rn = new Random();
            int N0 = 0;
            N0 = rn.Next(10, 99);
            obj.UserId = "Mac" + Name + "20" + N0;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    obj.Image = files[i];
                    string filename;
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = obj.Image.FileName.Split(new char[] { '\\' });
                        filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        filename = obj.Image.FileName;
                    }
                    string fname1 = Path.Combine(Server.MapPath("/Upload/"), filename);
                    obj.Image.SaveAs(fname1);
                    obj.ImagePath = "/Upload/" + filename;
                }
            }
            if (obj.Id != 0)
            {
                row = db.UpdateEmployee(obj);
                if (row > 0)
                {
                    Message = "update";
                }
                else
                {
                    Message = "failed";
                }
            }
            else
            {
                row = db.InsertEmployee(obj);
                string message = $@"
<html>
<head>
    <style>
        h3 {{ color: red; }}
        table {{
            border-collapse: collapse;
            width: 50%;
            margin: auto; /* Center the table */
        }}
        th, td {{
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }}
    </style>
</head>
<body>
    <p>Welcome To,</p>
    <h3>Macreel Infosoft PVT. LTD.</h3>
    <table>
        <tr>
            <th>Your Registration has been Succesfully</th>
            <th></th>
        </tr>
        <tr>
            <td>UserName:</td>
            <td><strong>{obj.UserName}</strong></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><strong>{obj.Password}</strong></td>
        </tr>
    </table>
    <p>From Macreel Infosoft Pvt Ltd..</p>
    <img src=''/>
</body>
</html>";
                db.sendemail(obj.EmailId, message);
                if (row > 0)
                {
                    Message = "success";
                }
                else
                {
                    Message = "failed";
                }
            }

            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckUserId(string UserName = "")
        {
            string Message = "";
            string UserId = "";
            UserId = db.CheckUserId(UserName);
            if (UserId != "")
            {
                Message = "True";
            }
            else
            {
                Message = "false";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllEmployeeList()
        {
            List<Employee> getlist = new List<Employee>();
            getlist = db.GetEmployyeList();
            ViewBag.Emp = getlist;
            return View();
        }
        [HttpPost]
        public JsonResult DeleteEmployee(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteEmployee(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmpView(int Id = 0)
        {
            Employee obj = new Employee();
            obj = db.GetEmployeeProfile(Id);

            return View(obj);
        }
        public ActionResult AddClient(int Id = 0)
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
        public ActionResult ClientList()
        {
            List<Client> getlist = new List<Client>();
            getlist = db.BindClient();
            ViewBag.list = getlist;
            return View();
        }
        public ActionResult ClientView(int Id = 0)
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
        [HttpPost]
        public JsonResult InsertClient(Client obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateClient(obj);
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
                row = db.InsertClient(obj);
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
        [HttpPost]
        public JsonResult DeleteClient(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteClient(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddProject(int Id = 0)
        {
            Project obj = new Project();
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;

            if (Id != 0)
            {
                obj = db.ProjectView(Id);
            }
            return View(obj);
        }
        [HttpPost]
        public JsonResult GetCompanyDetail(string ClientId = "")
        {
            Client obj = new Client();
            obj = db.GetClientDetails(ClientId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertProject(Project obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {

                if (obj.Documentation != null)
                {
                    int del = 0;
                    del = db.DeleteDocument(obj.ProjectCode);
                    foreach (var file in obj.Documentation)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var extention = Path.GetExtension(file.FileName);
                            var path = "/ProjectDocument/" + obj.ProjectName + obj.CompanyId + fileName;
                            file.SaveAs(Server.MapPath(path));
                            obj.Document = path;
                            db.InsertDocument(obj);
                        }
                    }
                }
                row = db.UpdateProject(obj);
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
                string Name = obj.ProjectName.Substring(0, 3);
                Random rn = new Random();
                int N0 = 0;
                N0 = rn.Next(1000, 9999);
                obj.ProjectCode = "Mac" + Name + "20" + N0;
                if (obj.Documentation != null)
                {
                    foreach (var file in obj.Documentation)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var extention = Path.GetExtension(file.FileName);
                            var path = "/ProjectDocument/" + obj.ProjectName + obj.CompanyId + fileName;
                            file.SaveAs(Server.MapPath(path));
                            obj.Document = path;
                            db.InsertDocument(obj);
                        }
                    }
                }

                row = db.InsertProject(obj);
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
        public ActionResult ViewService()
        { 
            return View();
        }
        public ActionResult ProjectList()
        {
            List<Project> gtlst = new List<Project>();
            gtlst = db.GetProjectList();
            ViewBag.gtlst = gtlst;
            return View();
        }
        [HttpPost]
        public JsonResult DeleteProject(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteProject(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ProjectView(int Id = 0)
        {
            Project obj = new Project();
            obj = db.ProjectView(Id);
            string ProjectCode = "";
            string Emp = "";
            ProjectCode = obj.ProjectCode;
            Emp = db.ProjectEmployee(ProjectCode);
            ViewBag.Emp = Emp;
            return View(obj);
        }
        public ActionResult AddLeaveType(int Id = 0)
        {
            List<LeaveTypes> list = new List<LeaveTypes>();
            list = db.bindlevetype();
            ViewBag.list = list;
            LeaveTypes obj = new LeaveTypes();
            if (Id != 0)
            {
                obj = db.Editlevetype(Id);
            }
            return View(obj);
        }
        [HttpPost]
        public JsonResult InsertLeaveType(LeaveTypes obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateLeaveType(obj);
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
                row = db.AddLeaveType(obj);
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
        [HttpPost]
        public JsonResult DeleteLeaveType(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.Deletelevetype(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssignLeave()
        {
            List<SelectListItem> GTLST = new List<SelectListItem>();
            GTLST = db.GetEmployyeListfORlEAVEaSSIGN();
            ViewBag.EMP = GTLST;
            List<LeaveTypes> list = new List<LeaveTypes>();
            list = db.bindlevetype();
            ViewBag.list = list;
            return View();
        }
        [HttpPost]
        public JsonResult GetEmployeeDetails(int Id = 0)
        {
            Employee obj = new Employee();
            obj = db.GetEmployeeProfile(Id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertAssignLeave(Assignleave obj)
        {
            string Message = "";
            int row = 0;
            obj.NoOfLeave = obj.Leave.Split(',');
            obj.LeaveType = obj.Type.Split(',');
            for (int i = 0; i < obj.LeaveType.Length; i++)
            {
                row = db.InsertAssignLeave(obj.EmployeeId, obj.EmployeeName, obj.NoOfLeave[i], obj.LeaveType[i]);
            }
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "failed";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UserDashboard()
        {
            int TotalPenginTask = 0;
            int TotalCompleteTask = 0;
            int TotalLeave = 0;
            int TotalPendingLeave = 0;
            int TotalApprovedLeave = 0;
            int TotalAssignProject = 0;
            TotalAssignProject = db.CountAssignProject1();
            ViewBag.TotalAssignProject = TotalAssignProject;
            int TotalAssignEmp = 0;
            TotalAssignEmp = db.CountAssignEmployee();
            ViewBag.TotalAssignEmp = TotalAssignEmp;
            int TotalRejectedLeave = 0;
            TotalRejectedLeave = db.CountTotalRejectedLeave();
            ViewBag.TotalRejectedLeave = TotalRejectedLeave;
            TotalApprovedLeave = db.CountTotalApprovedLeave();
            ViewBag.TotalApprovedLeave = TotalApprovedLeave;
            TotalPendingLeave = db.CountTotalPendingLeave();
            ViewBag.TotalPendingLeave = TotalPendingLeave;
            TotalLeave = db.CountTotalLeave();
            ViewBag.TotalLeave = TotalLeave;
            TotalPenginTask = db.CountPendingTask();
            ViewBag.TotalPengin= TotalPenginTask;
            TotalCompleteTask = db.CountCompleteTask();
            ViewBag.TotalComplete = TotalCompleteTask;
            if (Session["Login"] != null)
            {
                //var id = Session["userid"].ToString();
                List<ApplyLeave> obj = new List<ApplyLeave>();
                obj = db.GetUserDashboardLeave();
                ViewBag.Leave = obj;
                List<Lead> list = new List<Lead>();
                list = db.TodayLeadShowForEmp();
                List<Lead> list1 = new List<Lead>();
                ViewBag.today = list;
                list1 = db.TommorowLeadShowForEmp();           
                ViewBag.Tommorow = list1;
            }
            else
            {
                return RedirectToAction("Account", "Login");
            }
            return View();
        }
        public ActionResult ApplyLeaveForAdmin()
        {
            List<ApplyLeave> GetList = new List<ApplyLeave>();
            GetList = db.GetApplyListForAdmin();
            ViewBag.list = GetList;
            return View();
        }
        public ActionResult ApplyLeaveForReportingManager()
        {
            List<ApplyLeave> leave = new List<ApplyLeave>();
            leave = db.GetApplyListForReportingManager();
            ViewBag.leave = leave;
            return View();
        }
        public JsonResult UpdateLeaveStatus(int Id = 0, string Status = "", int LeaveCount = 0, string Description = "")
        {
            string Message = "";
            int row = 0;
            row = db.UpdateLeaveStatus(Id, Status, LeaveCount, Description);
            if (row > 0)
            {
                if (Status == "Approved")
                {
                    Message = "Successfully Approved,success";
                }
                else
                {
                    Message = "Successfully Reject,success";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        // update leave status by reporting manager
        public JsonResult UpdateLeaveStatusbyReporting(int Id = 0, string Status = "", int LeaveCount = 0, string Description = "")
        {
            string Message = "";
            int row = 0;
            row = db.UpdateLeaveStatusbyReporting(Id, Status, LeaveCount, Description);
            if (row > 0)
            {
                if (Status == "Approved")
                {
                    Message = "Successfully Approved,success";
                }
                else
                {
                    Message = "Successfully Reject,success";
                }

            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteApplyLeave(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteApplyLeave(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteApplyLeaveByEmp(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteApplyLeaveByEMP(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllEmployeeLeave()
        {
            List<Employee> getlist = new List<Employee>();
            getlist = db.GetEmployyeList();
            ViewBag.Emp = getlist;
            return View();
        }
        public ActionResult ViewEmpLeave(int Id = 0)
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetUserDashboardLeaveForAdmins(Id);
            ViewBag.Leave = obj;
            return View();
        }
        public ActionResult ViewEmpLeaveFor(int Id = 0)
        {
            List<ApplyLeave> obj = new List<ApplyLeave>();
            obj = db.GetUserDashboardLeaveForReportingManager(Id);
            ViewBag.Leave = obj;
            return View();
        }
        public ActionResult AssignProject()
        {
            AssignProject obj = new Bussiness.AssignProject();
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindProjectForAssignProject();
            ViewBag.Project = getlist;
            List<SelectListItem> empList = new List<SelectListItem>();
            empList = db.BindEmployyeForProject();
            ViewBag.emp = empList;
            return View(obj);
        }
        [HttpPost]
        public JsonResult GetProjectDetail(string ProjectCode = "")
        {
            Project obj = new Project();
            obj = db.GetProjectDetail(ProjectCode);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertProjectEmployee(AssignProject obj)
        {
            int row = 0;
            string Message = "";
            string[] EmpId;
            string[] EmpName;
            EmpId = obj.EmployeeId.Split(',');
            EmpName = obj.EmployeeName.Split(',');
            for (int i = 0; i < EmpId.Length; i++)
            {
                row = db.InsertEmployeeProject(EmpId[i], obj.ProjectCode, EmpName[i], obj.ProjectName, obj.Description, obj.AssignDate);
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
        public ActionResult AssignProjectList()
        {
            List<Project> gtlst = new List<Project>();
            gtlst = db.GetAssignProjectList();
            ViewBag.gtlst = gtlst;
            List<SelectListItem> empList = new List<SelectListItem>();
            empList = db.BindEmployyeForProject();
            ViewBag.emp = empList;
            return View();
        }
        public ActionResult ViewProjectAssignEmployee(string ProjectCode = "")
        {
            string Emp = "";
            string message = "";
            Emp = db.ProjectEmployee(ProjectCode);
            ViewBag.Emp = Emp;
            string Descript = "";
            Descript = db.ProjectEmployeeDescription(ProjectCode);
            ViewBag.desc = Descript;
            List<Project> gtl = new List<Project>();
            gtl = db.getDocumentaion(ProjectCode);
            ViewBag.Document = gtl;
            int row = 0;
            Project obj = new Project();
            row = db.UpdateProjectStatus(obj);
            if (row > 0)
            {
                message = "success";
            }
            else
            {
                message = "failed";
            }
            List<Project> list = new List<Project>();
            list = db.ProjectStatusDetail(ProjectCode);
            ViewBag.list = list;
            return View();
        }
        public ActionResult TaskSheetList()
        {
            List<SelectListItem> empList = new List<SelectListItem>();
            empList = db.BindEmployyeForProject();
            ViewBag.emp = empList;
            List<TaskSheets> list = new List<TaskSheets>();
            list = db.GetTasklist();
            ViewBag.list = list;
            return View();
        }
        public ActionResult Service(int Id = 0)
        {
            services obj = new services();
            if (Id != 0)
            {
                obj = db.EditServices(Id);
            }
            List<services> list = new List<services>();
            list = db.BindServices();
            ViewBag.list = list;
            return View(obj);
        }
        [HttpPost]
        public JsonResult AddService(services obj)
        {
            string message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateServices(obj);

                if (row > 0)
                {
                    message = "success";
                }
                else
                {
                    message = "failed";
                }
            }
            else
            {
                string Name = obj.Name.Substring(0, 3);
                Random rn = new Random();
                int N0 = 0;
                N0 = rn.Next(1000, 9999);
                int N02 = 0;
                N02 = rn.Next(10, 99);
                obj.Code = "Ser" + Name + N02 + N0;
                row = db.InsertServices(obj);
                if (row > 0)
                {
                    message = "update";
                }
                else
                {
                    message = "failed";
                }
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteServices(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteService(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenrateQuatation()
        {
            quatation obj = new quatation();
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            string No = "";
            No = db.GetMaxQuatationNo();
            obj.QuatationNo = "Mac" + "Qua" + 20 + No;
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            return View(obj);
        }
        [HttpPost]
        public JsonResult BindProjectForQuatation(string CompanyId = "")
        {
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            return Json(gtlist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindServices()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            return Json(getlist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DiscountPercentage(int Discount = 0, decimal Amount = 0.00M)
        {
            decimal FinalAmount = 0.00M;
            int VAL = 100;
            FinalAmount = Discount * Amount / VAL;
            return Json(FinalAmount, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult INSERTGenrateQuatation(quatation obj)
        {
            string Message = "";
            int row = 0;
            row = db.InsertQuatation(obj);
            if (row > 0)
            {
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 1; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.QuatationSoftwareIns(obj.QuatationNo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                    //if (count > 0)
                    //{
                    //    for (int i = 0; i < obj.SSAmount.Length; i++)
                    //    {
                    //        count = db.QuatationSoftwareServices(obj.QuatationNo, obj.SSServices[i], obj.SSServicesName[i], obj.SSDuration[i], obj.SSDurationTime[i], obj.SSAmount[i], obj.SSDescription[i]);
                    //    }
                    //}
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 1; i < obj.HardWareName.Length; i++)
                    {
                        count = db.QuatationHardwareIns(obj.QuatationNo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 1; i < obj.Amount.Length; i++)
                    {
                        count = db.QuatationProduct(obj.QuatationNo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Amount[i], obj.Description[i]);
                    }
                }
                if (count > 0)
                {
                    Message = "Quatation Successfully Ganrated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenrateQuatationList()
        {
            List<quatation> obj = new List<quatation>();
            obj = db.GetQuatationList();
            ViewBag.list = obj;
            return View();
        }
        [HttpPost]
        public JsonResult DeleteQuatation(string QuatationNo = "")
        {
            string Message = "";
            int row = 0;
            row = db.DeleteQuatation(QuatationNo);
            if (row > 0)
            {
                row = db.DeleteQuatationProduct(QuatationNo);
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditGenrateQuatation(string QuatationNo = "")
        {
            quatation obj = new quatation();
            obj = db.EditQuatation(QuatationNo);
            string CompanyId = "";
            CompanyId = obj.CompanyId;
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            List<quatation> list = new List<quatation>();
            list = db.EditQuatationProduct(QuatationNo);
            ViewBag.list = list;
            List<quatation> lists = new List<quatation>();
            lists = db.EditQuatationProductSoftware(QuatationNo);
            ViewBag.list = lists;
            List<quatation> listh = new List<quatation>();
            listh = db.EditQuatationProductHardware(QuatationNo);
            ViewBag.list = listh;
            return View(obj);
        }
        [HttpPost]
        public JsonResult UpdateGenrateQuatation(quatation obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdateQuatation(obj);
            if (row > 0)
            {
                int del = 0;
                del = db.DeleteQuatationProduct(obj.QuatationNo);
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 0; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.QuatationSoftware(obj.QuatationNo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 0; i < obj.HardWareName.Length; i++)
                    {
                        count = db.QuatationHardware(obj.QuatationNo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 0; i < obj.Amount.Length; i++)
                    {
                        count = db.QuatationProduct(obj.QuatationNo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Amount[i], obj.Description[i]);
                    }
                }
                if (count > 0)
                {
                    Message = "Quatation Successfully Updated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult QuatationPreview(string QuatationNo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<quatation> product = new List<quatation>();
            product = db.GetQuatationProduct(QuatationNo);
            List<quatation> product1 = new List<quatation>();
            product1 = db.GetQuatationProductsoftware(QuatationNo);
            List<quatation> Hproduct = new List<quatation>();
            Hproduct = db.GetQuatationProductHardware(QuatationNo);
            ViewBag.product = product;
            ViewBag.product2 = product1;
            ViewBag.Hproduct1 = Hproduct;
            quatation obj = new quatation();
            obj = db.GetQuatationPreview(QuatationNo);
            double TR1 = Convert.ToDouble(obj.AfterDiscountAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult AddPI(string QuatationNo = "")
        {
            quatation obj = new quatation();
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            string No = "";
            No = db.GetMaxQuatationNo();
            obj.QuatationNo = "Mac" + "PI" + 20 + No;
            return View(obj);
        }
        public ActionResult AddPIWithQuatation(string QuatationNo = "")
        {
            quatation obj = new quatation();
            obj = db.EditQuatation(QuatationNo);
            string CompanyId = "";
            CompanyId = obj.CompanyId;
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            List<quatation> list = new List<quatation>();
            list = db.EditQuatationProduct(QuatationNo);
            ViewBag.list = list;
            List<quatation> lists = new List<quatation>();
            lists = db.EditQuatationProductSoftware(QuatationNo);
            ViewBag.list = lists;
            List<quatation> listh = new List<quatation>();
            listh = db.EditQuatationProductHardware(QuatationNo);
            ViewBag.list = listh;
            string No = "";
            No = db.GetMaxPINo();
            obj.PINo = "Mac" + "PI" + 20 + No;
            return View(obj);
        }
        [HttpPost]
        public JsonResult GanrateQuatationToPI(quatation obj)
        {
            string Message = "";
            int row = 0;
            row = db.InsertPI(obj);
            if (row > 0)
            {
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 0; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.PISoftware(obj.PINo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 0; i < obj.HardWareName.Length; i++)
                    {
                        count = db.PIHardware(obj.PINo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 0; i < obj.Amount.Length; i++)
                    {
                        count = db.PIProduct(obj.PINo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "PI Successfully Ganrated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GanrateDPI(quatation obj)
        {
            string Message = "";
            int row = 0;
            row = db.InsertPI(obj);
            if (row > 0)
            {
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 1; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.PISoftware(obj.PINo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 1; i < obj.HardWareName.Length; i++)
                    {
                        count = db.PIHardware(obj.PINo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 1; i < obj.Amount.Length; i++)
                    {

                        count = db.PIProduct(obj.PINo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "PI Successfully Ganrated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PIList()
        {
            List<performa> LIST = new List<performa>();
            LIST = db.GetPIList();
            ViewBag.List = LIST;
            return View();
        }
        public ActionResult GanratePI()
        {
            performa obj = new performa();
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            string No = "";
            No = db.GetMaxPINo();
            obj.PINo = "Mac" + "PI" + 20 + No;
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            return View(obj);
        }
        [HttpPost]
        public JsonResult DeletePI(string PINo = "")
        {
            string Message = "";
            int row = 0;
            row = db.DeletePI(PINo);
            if (row > 0)
            {
                row = db.DeletePIProduct(PINo);
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PIPreview(string PINo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;

            List<quatation> product = new List<quatation>();
            List<quatation> product1 = new List<quatation>();
            List<quatation> hardware = new List<quatation>();
            product = db.GetPIProduct(PINo);
            ViewBag.product = product;
            product1 = db.GetPIProductSoftware(PINo);
            ViewBag.productsoftware = product1;
            hardware = db.GetPIProductHardware(PINo);
            ViewBag.Hardware = hardware;
            quatation obj = new quatation();
            obj = db.GetPIPreview(PINo);
            double TR1 = Convert.ToDouble(obj.AfterTaxAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult UpdatePI(string PINo = "")
        {
            quatation obj = new quatation();
            obj = db.EditPI(PINo);
            string CompanyId = "";
            CompanyId = obj.CompanyId;
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            List<quatation> list = new List<quatation>();
            list = db.GetPIProduct(PINo);
            ViewBag.list = list;

            return View(obj);
        }
        [HttpPost]
        public JsonResult UpdateGanratedPI(quatation obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdatePI(obj);
            if (row > 0)
            {
                int count = 0;
                int del = 0;
                del = db.DeletePIProduct(obj.PINo);
                if (obj.Type == "Software")
                {
                    for (int i = 0; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.PISoftware(obj.PINo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 0; i < obj.HardWareName.Length; i++)
                    {
                        count = db.PIHardware(obj.PINo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {
                    for (int i = 0; i < obj.Amount.Length; i++)
                    {
                        count = db.PIProduct(obj.PINo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "PI Successfully Updated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdatePerformaInvoice(performa obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdatePerformaInvoice(obj);
            if (row > 0)
            {
                if (obj.Status == 1)
                {
                    Message = "Successfully Approved,success";
                }
                else
                {
                    Message = "Successfully Rejected,success";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GanratedInvoice()
        {
            List<performa> LIST = new List<performa>();
            LIST = db.GetGanratedInvoiceList();
            ViewBag.List = LIST;
            return View();
        }
        [HttpPost]
        //public ActionResult QuatationPreview(FormCollection collection)
        //{

        //    //HtmlToPdf converter = new HtmlToPdf();
        //    //PdfDocument doc = converter.ConvertUrl(collection["TxtUrl"]);
        //    byte[] pdf = doc.Save();
        //    doc.Close();
        //    FileResult fileResult = new FileContentResult(pdf, "application/pdf");
        //    fileResult.FileDownloadName = "Company__Invoice.pdf";
        //    return fileResult;
        //}
        public ActionResult QuatationPdf(string QuatationNo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<quatation> product = new List<quatation>();
            product = db.GetQuatationProduct(QuatationNo);
            ViewBag.product = product;
            quatation obj = new quatation();
            obj = db.GetQuatationPreview(QuatationNo);
            double TR1 = Convert.ToDouble(obj.AfterDiscountAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult PerformaPdf(string PINo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<quatation> product = new List<quatation>();
            product = db.GetPIProduct(PINo);
            ViewBag.product = product;
            quatation obj = new quatation();
            obj = db.GetPIPreview(PINo);
            double TR1 = Convert.ToDouble(obj.AfterTaxAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult TaxInvoicePreview(string InvoiceNo = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<performa> product = new List<performa>();
            List<performa> products = new List<performa>();
            List<performa> productH = new List<performa>();
            product = db.GetTaxProduct(InvoiceNo);
            ViewBag.product = product;
            products = db.GetTaxProductSoftware(InvoiceNo);
            ViewBag.products = products;
            productH = db.GetTaxProductHardware(InvoiceNo);
            ViewBag.productH = productH;
            performa obj = new performa();
            obj = db.GetInvoicePreview(InvoiceNo);
            double TR1 = Convert.ToDouble(obj.AfterTaxAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult TaxPdf(string InvoiceNo = "")
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
        public ActionResult TaxInvoicePdf(string PINo = "")
        {
            string No = "";
            string TaxInvoiceNo = "";
            No = db.GetMaxTaxInvoiceNo();
            TaxInvoiceNo = "Mac" + "TI" + 20 + No;
            int row = 0;
            row = db.InsertTaxInvoice(PINo, TaxInvoiceNo);
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<quatation> product = new List<quatation>();
            product = db.GetPIProduct(PINo);
            ViewBag.product = product;
            performa obj = new performa();
            obj = db.TaxInvoiceDetails(PINo);
            double TR1 = Convert.ToDouble(obj.AfterTaxAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult PurchaseOrder(int Id = 0)
        {
            Purchase obj = new Purchase();
            if (Id != 0)
            {
                obj = db.EditPurchase(Id);
            }
            List<Purchase> gtlst = new List<Purchase>();
            gtlst = db.GetPurchaseList();
            ViewBag.list = gtlst;
            return View(obj);
        }
        [HttpPost]
        public JsonResult Purchase(Purchase obj)
        {
            string Message = "";
            int row = 0;
            var file = obj.UploadInvoice;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    obj.UploadInvoice = files[i];
                    string filename;
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = obj.UploadInvoice.FileName.Split(new char[] { '\\' });
                        filename = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        filename = obj.UploadInvoice.FileName;
                    }
                    string fname1 = Path.Combine(Server.MapPath("/Upload/"), filename);
                    obj.UploadInvoice.SaveAs(fname1);
                    obj.UploadInvoicePath = "/Upload/" + filename;
                }
            }
            if (obj.Id != 0)
            {
                row = db.UpdatePurchase(obj);
                if (row > 0)
                {
                    Message = "Successfully Updated,success";
                }
                else
                {
                    Message = "Something Wrong!,danger";
                }
            }
            else
            {
                row = db.InsertPurchase(obj);
                if (row > 0)
                {
                    Message = "Successfully Added,success";
                }
                else
                {
                    Message = "Something Wrong!,danger";
                }
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeletePurchase(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeletePurchase(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PurchaseView(int Id = 0)
        {
            Purchase obj = new Bussiness.Purchase();
            obj = db.EditPurchase(Id);
            return View(obj);
        }
        public ActionResult AddPayment()
        {
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            return View();
        }
        [HttpPost]
        public JsonResult GetPaymentorPayment(int CompanyId = 0)
        {
            Payments obj = new Payments();
            obj = db.GetPaymentsfORpAYMENT(CompanyId);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult InsertPayment(Payments obj)
        {
            string Message = "";
            int row = 0;
            row = db.AddPayment(obj);
            if (row > 0)
            {
                Message = "Payment Successfully Added!,success";
            }
            else
            {
                Message = "Something Wrong!,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Reports()
        {
            int TotalEmp = 0;
            int TotalClient = 0;
            int TotalProject = 0;
            int AssignProject = 0;
            int CountQuatations = 0;
            int CountPIS = 0;
            int Tax = 0;
            int Purchase = 0;
            int Task = 0;
            TotalEmp = db.CountEmployee();
            ViewBag.TotalEmp = TotalEmp;
            TotalClient = db.CountClient();
            ViewBag.TotalClient = TotalClient;
            TotalProject = db.CountProject();
            ViewBag.TotalProject = TotalProject;
            AssignProject = db.CountAssignProject();
            ViewBag.AssignProject = AssignProject;
            CountQuatations = db.CountQuatation();
            ViewBag.CountQuatations = CountQuatations;
            CountPIS = db.CountPI();
            ViewBag.CountPI = CountPIS;
            Tax = db.CountTax();
            ViewBag.Tax = Tax;
            Purchase = db.CountPurchase();
            ViewBag.Purchase = Purchase;
            Task = db.CountTaskSheet();
            ViewBag.Task = Task;
            return View();
        }
        public ActionResult PurchaseList()
        {
            Purchase obj = new Purchase();
            Purchase obj1 = new Purchase();
            string Before = "";
            string After = "";
            List<Purchase> gtlst = new List<Purchase>();
            gtlst = db.GetPurchaseList();
            ViewBag.list = gtlst;
            obj = db.TotalBeforeAmt();
            Before = obj.AmtBeforeTax;
            ViewBag.Before = Before;
            obj1 = db.TotalAftereAmt();
            After = obj1.AmtAfterTax;
            ViewBag.After = After;
            return View();
        }
        public ActionResult DateWisePurchase(string Date1 = "", string Date2 = "")
        {
            string Before = "";
            string After = "";

            List<Purchase> gtlst = new List<Purchase>();
            gtlst = db.DateWiseGetPurchaseList(Date1, Date2);
            ViewBag.list = gtlst;
            Before = db.TotalBeforeAmtDate(Date1, Date2);
            ViewBag.Before = Before;
            After = db.TotalAfterAmtDate(Date1, Date2);
            ViewBag.After = After;
            return View();
        }
        public ActionResult DuePayment()
        {
            List<Payments> list = new List<Payments>();
            list = db.GetDuePaymentList();
            ViewBag.list = list;
            return View();
        }
        public ActionResult LadgerAccount()
        {
            List<Client> list = new List<Client>();
            list = db.BindClient();
            ViewBag.list = list;
            return View();
        }

        public ActionResult DatewiseTaskSheet(string Date1 = "", string Date2 = "", string EmployeeId = "")
        {
            List<TaskSheets> list = new List<TaskSheets>();
            list = db.GetTasklistByDate(Date1, Date2, EmployeeId);
            ViewBag.list = list;
            Employee obj = new Employee();
            obj.Id = Convert.ToInt32(EmployeeId);
            obj = db.GetEmployeeProfile(obj.Id);
            return View(obj);
        }
        public ActionResult CreateLead(int Id = 0)
        {
            Lead obj = new Lead();
            if (Id != 0)
            {
                obj = db.EditLead(Id);
            }
            List<Lead> list = new List<Lead>();
            list = db.AllLead();
            ViewBag.list = list;
            return View(obj);
        }

        public ActionResult AllLead()
        {
            List<Lead> list = new List<Lead>();
            list = db.AllLead();
            ViewBag.list = list;
            return View();
        }
        public ActionResult AssignedLead()
        {
            List<SelectListItem> GTLST = new List<SelectListItem>();
            GTLST = db.GetEmployyeListfORlEAVEaSSIGN();
            ViewBag.EMP = GTLST;
            Lead obj = new Lead();
            List<Lead> list = new List<Lead>();
            list = db.AllNoAssignLead();
            ViewBag.list = list;
            if (list.Count == 0)
            {
                obj.Status = 0;
            }
            else
            {
                obj.Status = 1;
            }
            return View(obj);
        }
        public JsonResult InsertLead(Lead obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateLead(obj);
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
                row = db.InsertLead(obj);
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
        public JsonResult DeleteLead(int Id = 0)
        {
            string Message = "";
            int row = 0;
            row = db.DeleteLead(Id);
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewLead(int Id = 0)
        {
            Lead obj = new Lead();
            obj = db.EditLead(Id);
            return View(obj);
        }
        public JsonResult InsertAssignLead(Lead obj)
        {
            string Message = "";
            int row = 0;
            obj.Leads = obj.LeadId.Split(',');
            for (int i = 0; i < obj.Leads.Length; i++)
            {
                row = db.InsertAssignLead(obj.Leads[i], obj.EmployeeId, obj.EmployeeName, obj.AssignDate);
            }
            if (row > 0)
            {
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AllAssignLeave()
        {
            List<Lead> list = new List<Lead>();
            list = db.AllAssignLead();
            ViewBag.list = list;
            List<SelectListItem> GTLST = new List<SelectListItem>();
            GTLST = db.GetEmployyeListfORlEAVEaSSIGN();
            ViewBag.EMP = GTLST;
            return View();
        }
        public JsonResult DeleteAssignLead(int Id)
        {
            string mess = "";
            int row = 0;
            row = db.DeleteAssignLead(Id);
            if (row > 0)
            {
                mess = "success";
            }
            else
            {
                mess = "danger";
            }
            return Json(mess, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RenewProductList()
        {
            List<performa> list = new List<performa>();
            list = db.GetproductList();
            ViewBag.list = list;
            return View();
        }
        public ActionResult RenewPreview(string InvoiceNo = "", string CompanyId = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<performa> list = new List<performa>();
            list = db.GetproductListForInvoice(InvoiceNo);
            ViewBag.product = list;
            List<Client> cl = new List<Client>();
            cl = db.ClientDetal(CompanyId);
            ViewBag.client = cl;
            performa obj = new performa();
            obj = db.GetAmount(InvoiceNo);
            double TR1 = Convert.ToDouble(obj.TotalAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult RenewProductPdf(string InvoiceNo = "", string CompanyId = "")
        {
            List<Company> gtlst = new List<Company>();
            gtlst = db.GetCompanyDetail();
            ViewBag.company = gtlst;
            List<performa> list = new List<performa>();
            list = db.GetproductListForInvoice(InvoiceNo);
            ViewBag.product = list;
            List<Client> cl = new List<Client>();
            cl = db.ClientDetal(CompanyId);
            ViewBag.client = cl;
            performa obj = new performa();
            obj = db.GetAmount(InvoiceNo);
            double TR1 = Convert.ToDouble(obj.TotalAmount);
            obj.Amountword = Words.ConvertNumbertoWords(Convert.ToInt64(TR1));
            return View(obj);
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult PageNoTFound()
        {
            return View();
        }
        public JsonResult TotalAmountOfProduct(int Unit = 0, decimal Price = 0.00M)
        {
            decimal Total = 0.00M;
            Total = Price * Unit;
            return Json(Total, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindProduct()
        {
            List<SelectListItem> GetProductList = new List<SelectListItem>();
            GetProductList = db.BindProduct();
            return Json(GetProductList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BindHSNNo(int ProductId = 0)
        {
            string HSNNo = "";
            HSNNo = db.GetHSNNo(ProductId);
            return Json(HSNNo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GenratedInvoiceWithPI(string PINo = "")
        {
            performa obj = new performa();
            obj = db.EditPIThrowPI(PINo);
            string CompanyId = "";
            CompanyId = obj.CompanyId;
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            List<quatation> list = new List<quatation>();
            list = db.GetPIProduct(PINo);
            ViewBag.list = list;
            List<quatation> listS = new List<quatation>();
            listS = db.GetPIProductS(PINo);
            ViewBag.list = listS;
            List<quatation> listh = new List<quatation>();
            listh = db.GetPIProductH(PINo);
            ViewBag.list = listh;
            string No = "";
            No = db.GetMaxTaxInvoiceNo();
            obj.InvoiceNo = "Mac" + "TIN" + 20 + No;
            return View(obj);
        }
        [HttpPost]
        public JsonResult GanrateTaxInvoice(performa obj)
        {
            string Message = "";
            int row = 0;
            row = db.InsertTaxInvoice(obj);
            if (row > 0)
            {
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 0; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.TAXINVOICESoftware(obj.InvoiceNo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 0; i < obj.HardWareName.Length; i++)
                    {
                        count = db.InvoiceHardware(obj.InvoiceNo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 0; i < obj.Amount.Length; i++)
                    {
                        count = db.InvoiceProduct(obj.InvoiceNo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "Invoice Successfully Ganrated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GanrateTaxInvoice()
        {
            performa obj = new performa();
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            string No = "";
            No = db.GetMaxTaxInvoiceNo();
            obj.InvoiceNo = "Mac" + "TIN" + 20 + No;
            List<SelectListItem> STATE = new List<SelectListItem>();
            STATE = db.GetState();
            ViewBag.STATE = STATE;
            return View(obj);
        }
        [HttpPost]
        public JsonResult GanrateTaxInvoices(performa obj)
        {
            string Message = "";
            int row = 0;
            int count1 = 0;
            row = db.InsertTaxInvoice(obj);
            count1 = db.AddTaxInvoiceLadger(obj.CompanyId, obj.CompanyName, obj.AfterTaxAmount, "0", "0", obj.InvoiceNo);
            if (row > 0)
            {
                int count = 0;
                if (obj.Type == "Software")
                {
                    for (int i = 1; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.TAXINVOICESoftware(obj.InvoiceNo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 1; i < obj.HardWareName.Length; i++)
                    {
                        count = db.InvoiceHardware(obj.InvoiceNo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 1; i < obj.Amount.Length; i++)
                    {
                        count = db.InvoiceProduct(obj.InvoiceNo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "Invoice Successfully Ganrated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateInvoice(string InvoiceNo = "")
        {
            performa obj = new performa();
            obj = db.EditTax(InvoiceNo);
            string CompanyId = "";
            CompanyId = obj.CompanyId;
            List<SelectListItem> ClientList = new List<SelectListItem>();
            ClientList = db.BindClientForProject();
            ViewBag.client = ClientList;
            List<SelectListItem> getlist = new List<SelectListItem>();
            getlist = db.BindServicesForQuatation();
            ViewBag.service = getlist;
            List<SelectListItem> gtlist = new List<SelectListItem>();
            gtlist = db.BindProjectForQuatation(CompanyId);
            ViewBag.PROJECT = gtlist;
            List<performa> list = new List<performa>();
            list = db.GetTaxProduct(InvoiceNo);
            ViewBag.list = list;
            return View(obj);
        }
        [HttpPost]
        public JsonResult UpdateGanratedTaxInvoice(performa obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdateTaxInvoice(obj);
            if (row > 0)
            {
                int count = 0;
                int del = 0;
                del = db.DeleteTaxProduct(obj.InvoiceNo);
                if (obj.Type == "Software")
                {
                    for (int i = 0; i < obj.SoftwareName.Length; i++)
                    {
                        count = db.TAXINVOICESoftware(obj.InvoiceNo, obj.SoftwareName[i], obj.SAmount[i], obj.SUnit[i], obj.SDescription[i], obj.STotal[i]);
                    }
                }
                else if (obj.Type == "Hardware")
                {
                    for (int i = 0; i < obj.HardWareName.Length; i++)
                    {
                        count = db.InvoiceHardware(obj.InvoiceNo, obj.HardWareName[i], obj.Naration[i], obj.HAmount[i], obj.HUnit[i], obj.HTotal[i]);
                    }
                }
                else if (obj.Type == "Other")
                {

                    for (int i = 0; i < obj.Amount.Length; i++)
                    {
                        count = db.InvoiceProduct(obj.InvoiceNo, obj.Services[i], obj.ServicesName[i], obj.Duration[i], obj.DurationTime[i], obj.Date[i], obj.Amount[i], obj.Description[i], obj.CompanyId, obj.ProjectId);
                    }
                }
                if (count > 0)
                {
                    Message = "Invoice Successfully Updated!,success";
                }
                else
                {
                    Message = "Something Wrong,danger";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteInvoice(string InvoiceNo = "")
        {
            string Message = "";
            int row = 0;
            row = db.DeleteTaxInvoice(InvoiceNo);
            if (row > 0)
            {
                row = db.DeleteTaxProduct(InvoiceNo);
                Message = "success";
            }
            else
            {
                Message = "danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateAsssignLead(Lead obj)
        {
            string Message = "";
            int row = 0;
            row = db.ReassignLead(obj);
            if (row > 0)
            {
                Message = "Successfully Assign Lead,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewLeadStatusHistory(int LeadId = 0)
        {
            List<Lead> list = new List<Lead>();
            list = db.GetStatusHistory(LeadId);
            ViewBag.list = list;
            return View();
        }
        public JsonResult AddLadger(Payments obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Status == "Approved")
            {
                row = db.AddLadger(obj);
                int count = 0;
                count = db.AddPayment(obj);
            }
            if (row > 0)
            {
                Message = "Successfully Approved,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewLadger(int CompanyId)
        {
            Client obj = new Client();
            List<Payments> ladger = new List<Payments>();
            ladger = db.GetClientLadger(CompanyId);
            ViewBag.ladger = ladger;
            obj = db.EditClient(CompanyId);
            List<Payments> Payment = new List<Payments>();
            Payment = db.ClientPaymentForLadgerAccount(CompanyId);
            ViewBag.pay = Payment;
            Payments ss = new Payments();
            ss = db.GetPaymentsfORpAYMENT(CompanyId);
            ViewBag.Total = ss.TotalAmount;
            ViewBag.Payment = ss.Payment;
            ViewBag.remain = ss.RemainingAmount;
            return View(obj);
        }
        public ActionResult SupportTicket()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            getlist = db.GetInboxForAdmin();
            ViewBag.EmailList = getlist;
            return View();
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
        public JsonResult AddClientAccount(Client obj)
        {
            string Message = "";
            int row = 0;
            row = db.AddClientAccount(obj);
            if (row > 0)
            {
                Message = "Client Account Successfull Added,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateRenewProductStatus(performa obj)
        {
            string Message = "";
            int row = 0;
            row = db.UpdateRenewProduct(obj);
            if (row > 0)
            {
                if (obj.Status == 1)
                {
                    Message = "Successfully Approved,success";
                }
                else if (obj.Status == 2)
                {
                    Message = "Successfully Reject,success";
                }
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ReassignProject(AssignProject obj)
        {
            int row = 0;
            int count = 0;
            count = db.DeleteAssignEmployee(obj.ProjectCode);
            string Message = "";
            string[] EmpId;
            string[] EmpName;
            EmpId = obj.EmployeeId.Split(',');
            EmpName = obj.EmployeeName.Split(',');
            for (int i = 0; i < EmpId.Length; i++)
            {
                row = db.InsertEmployeeProject(EmpId[i], obj.ProjectCode, EmpName[i], obj.ProjectName, obj.Description, obj.AssignDate);
            }
            if (row > 0)
            {
                Message = "Successfully Re-Assigned,success";
            }
            else
            {
                Message = "Something Wrong,danger";
            }
            return Json(Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ApproveLeaveList()
        {
            List<ApplyLeave> blist = new List<ApplyLeave>();
            blist = db.ApprovedApplyLeave();
            ViewBag.ApplyLeave = blist;
            return View();
        }
        public ActionResult RejectLeaveList()
        {
            List<ApplyLeave> blist = new List<ApplyLeave>();
            blist = db.RejectApplyLeave();
            ViewBag.ApplyLeave = blist;
            return View();
        }
        public ActionResult PendingLeaveList()
        {
            List<ApplyLeave> blist = new List<ApplyLeave>();
            blist = db.PemdingApplyLeave();
            ViewBag.ApplyLeave = blist;
            return View();
        }

       
        #region Task Management
        public ActionResult Create_Task(string id)
        {
            ViewBag.NotificationTitle = "New Message";
            ViewBag.NotificationBody = "You have a new message!";

            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}

            ViewBag.Title = "Create_Task";
            var userid = Session["EmpId"];
            TaskManage task = new TaskManage();
            List<emp_list> emp_list = new List<emp_list>();
            connection.Open();
            SqlCommand cm = new SqlCommand("sp_TaskManage", connection);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Action", "select_empList");
            cm.Parameters.AddWithValue("@userid", userid);
            SqlDataReader sd = cm.ExecuteReader();
            emp_list pr;
            if (sd.HasRows)
            {
                while (sd.Read())
                {
                    pr = new emp_list();
                    pr.emp_id = sd["id"].ToString();
                    pr.emp_name = sd["EmployeeName"].ToString();
                    emp_list.Add(pr);
                }
            }
            task.emp_Lists = emp_list;
            connection.Close();
            if (id != null && id != "")
            {
                task = db.gettaskById(id);
            }
            else
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_task_SNo");
                SqlDataReader sdr = cmd.ExecuteReader();
                //var ab = sdr["serial_no"].ToString();
                if (sdr.HasRows)
                {
                    sdr.Read(); // Move to the first row
                    task.s_no = sdr["serial_no"].ToString();

                    if (task.s_no == "")
                    {
                        task.s_no = "1";
                    }
                }
                else
                {
                    task.s_no = "1";
                }


                connection.Close();
            }

            task.emp_Lists = emp_list;
            return View(task);
        }
        public ActionResult Create_TaskByReportingManager(string id)
        {
            ViewBag.NotificationTitle = "New Message";
            ViewBag.NotificationBody = "You have a new message!";
            TaskManage task = new TaskManage();
            List<emp_list> emp_list = new List<emp_list>();

            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}

            ViewBag.Title = "Create_Task";
            emp_list= db.InsertTaskByReportingManager();
            task.emp_Lists=emp_list;
            if (id != null && id != "")
            {
                task = db.gettaskById(id);
            }
            else
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_task_SNo");
                SqlDataReader sdr = cmd.ExecuteReader();
                //var ab = sdr["serial_no"].ToString();
                if (sdr.HasRows)
                {
                    sdr.Read(); // Move to the first row
                    task.s_no = sdr["serial_no"].ToString();

                    if (task.s_no == "")
                    {
                        task.s_no = "1";
                    }
                }
                else
                {
                    task.s_no = "1";
                }
                connection.Close();
            }
            task.emp_Lists = emp_list;
            return View(task);
        }
        [HttpPost]
        public ActionResult task_insert(TaskManage task)
        {
            var curent_date = DateTime.Now.ToString("yyyy-MM-dd");
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));
            // var path = Server.MapPath("~/tempimage/");
            HttpPostedFileBase file1 = Request.Files["fileupload1"];
            HttpPostedFileBase file2 = Request.Files["fileupload2"];
            HttpPostedFileBase file3 = Request.Files["fileupload3"];
            HttpPostedFileBase file4 = Request.Files["fileupload4"];
            HttpPostedFileBase file5 = Request.Files["fileupload5"];

            string attachment1 = "";
            string attachment2 = "";
            string attachment3 = "";
            string attachment4 = "";
            string attachment5 = "";

            List<TaskManage> tasks = new List<TaskManage>();
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase> { file1, file2, file3, file4, file5 };

            for (int attachmentNum = 1; attachmentNum <= 5; attachmentNum++)
            {
                string attachmentName = "attachment" + attachmentNum.ToString();
                string fileName = "file" + attachmentNum.ToString();
                if (task.GetType().GetProperty(attachmentName) != null)
                {
                    string attachmentValue = (string)task.GetType().GetProperty(attachmentName).GetValue(task, null);
                    if (!string.IsNullOrEmpty(attachmentValue))
                    {
                        // Save the attachment value if not null or empty
                        switch (attachmentNum)
                        {
                            case 1:
                                attachment1 = attachmentValue;
                                break;
                            case 2:
                                attachment2 = attachmentValue;
                                break;
                            case 3:
                                attachment3 = attachmentValue;
                                break;
                            case 4:
                                attachment4 = attachmentValue;
                                break;
                            case 5:
                                attachment5 = attachmentValue;
                                break;
                        }
                    }
                }

                // Now, check and process the file if it exists for this attachment
                HttpPostedFileBase file = files[attachmentNum - 1];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string uploadFileName = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file.FileName.Split('.')[1];

                    file.SaveAs(path + uploadFileName);

                    // Assign the upload filename to the appropriate attachment variable
                    switch (attachmentNum)
                    {
                        case 1:
                            attachment1 = "/tempimage/" + uploadFileName;
                            break;
                        case 2:
                            attachment2 = "/tempimage/" + uploadFileName;
                            break;
                        case 3:
                            attachment3 = "/tempimage/" + uploadFileName;
                            break;
                        case 4:
                            attachment4 = "/tempimage/" + uploadFileName;
                            break;
                        case 5:
                            attachment5 = "/tempimage/" + uploadFileName;
                            break;
                    }
                }
            }

            string[] idsArray = task.selectedValues.Split(',');

            int i = 0;
            var e = 0;

            foreach (var idString in idsArray)
            {
                var sno = Convert.ToInt32(task.s_no) + e;
                e++;

                SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", task.id);
                cmd.Parameters.AddWithValue("@S_no", sno);
                cmd.Parameters.AddWithValue("@title", task.title);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@curent_date", curent_date);
                cmd.Parameters.AddWithValue("@completion_date", task.complete_date);
                cmd.Parameters.AddWithValue("@attachment1", attachment1);
                cmd.Parameters.AddWithValue("@attachment2", attachment2);
                cmd.Parameters.AddWithValue("@attachment3", attachment3);
                cmd.Parameters.AddWithValue("@attachment4", attachment4);
                cmd.Parameters.AddWithValue("@attachment5", attachment5);
                //cmd.Parameters.AddWithValue("@assigned_employee", task.assigned_emp);
                cmd.Parameters.AddWithValue("@task_status", "Pending");
                cmd.Parameters.AddWithValue("@emp_status", "Pending");

                if (task.id != null && task.id != "")
                {
                    cmd.Parameters.AddWithValue("@Action", "update_task");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "Add_task");
                }
                cmd.Parameters.AddWithValue("@assigned_employee", idString);

                //var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                //hubContext.Clients.User(idString.ToString()).notify("New task assigned: ");


                //var notification = new Models.Admin.Notification
                //{
                //    Message = "You have a new task assigned.",
                //    EmployeeId = idString
                //};
                //string id = idString;
                //NotifyEmployee(id);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                i = cmd.ExecuteNonQuery();

            }
            connection.Close();

            if (i >= 1)
            {
                TempData["Message"] = "Task Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }
            return RedirectToAction("Create_Task");
        }
        [HttpPost]
        public ActionResult task_insertByReporintgM(TaskManage task)
        {
            var curent_date = DateTime.Now.ToString("yyyy-MM-dd");
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));
            // var path = Server.MapPath("~/tempimage/");
            HttpPostedFileBase file1 = Request.Files["fileupload1"];
            HttpPostedFileBase file2 = Request.Files["fileupload2"];
            HttpPostedFileBase file3 = Request.Files["fileupload3"];
            HttpPostedFileBase file4 = Request.Files["fileupload4"];
            HttpPostedFileBase file5 = Request.Files["fileupload5"];

            string attachment1 = "";
            string attachment2 = "";
            string attachment3 = "";
            string attachment4 = "";
            string attachment5 = "";

            List<TaskManage> tasks = new List<TaskManage>();
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase> { file1, file2, file3, file4, file5 };

            for (int attachmentNum = 1; attachmentNum <= 5; attachmentNum++)
            {
                string attachmentName = "attachment" + attachmentNum.ToString();
                string fileName = "file" + attachmentNum.ToString();
                if (task.GetType().GetProperty(attachmentName) != null)
                {
                    string attachmentValue = (string)task.GetType().GetProperty(attachmentName).GetValue(task, null);
                    if (!string.IsNullOrEmpty(attachmentValue))
                    {
                        // Save the attachment value if not null or empty
                        switch (attachmentNum)
                        {
                            case 1:
                                attachment1 = attachmentValue;
                                break;
                            case 2:
                                attachment2 = attachmentValue;
                                break;
                            case 3:
                                attachment3 = attachmentValue;
                                break;
                            case 4:
                                attachment4 = attachmentValue;
                                break;
                            case 5:
                                attachment5 = attachmentValue;
                                break;
                        }
                    }
                }

                // Now, check and process the file if it exists for this attachment
                HttpPostedFileBase file = files[attachmentNum - 1];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string uploadFileName = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file.FileName.Split('.')[1];

                    file.SaveAs(path + uploadFileName);

                    // Assign the upload filename to the appropriate attachment variable
                    switch (attachmentNum)
                    {
                        case 1:
                            attachment1 = "/tempimage/" + uploadFileName;
                            break;
                        case 2:
                            attachment2 = "/tempimage/" + uploadFileName;
                            break;
                        case 3:
                            attachment3 = "/tempimage/" + uploadFileName;
                            break;
                        case 4:
                            attachment4 = "/tempimage/" + uploadFileName;
                            break;
                        case 5:
                            attachment5 = "/tempimage/" + uploadFileName;
                            break;
                    }
                }
            }

            string[] idsArray = task.selectedValues.Split(',');

            int i = 0;
            var e = 0;

            foreach (var idString in idsArray)
            {
                var sno = Convert.ToInt32(task.s_no) + e;
                e++;

                SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", task.id);
                cmd.Parameters.AddWithValue("@S_no", sno);
                cmd.Parameters.AddWithValue("@title", task.title);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@curent_date", curent_date);
                cmd.Parameters.AddWithValue("@completion_date", task.complete_date);
                cmd.Parameters.AddWithValue("@attachment1", attachment1);
                cmd.Parameters.AddWithValue("@attachment2", attachment2);
                cmd.Parameters.AddWithValue("@attachment3", attachment3);
                cmd.Parameters.AddWithValue("@attachment4", attachment4);
                cmd.Parameters.AddWithValue("@attachment5", attachment5);
                //cmd.Parameters.AddWithValue("@assigned_employee", task.assigned_emp);
                cmd.Parameters.AddWithValue("@task_status", "Pending");
                cmd.Parameters.AddWithValue("@emp_status", "Pending");

                if (task.id != null && task.id != "")
                {
                    cmd.Parameters.AddWithValue("@Action", "update_task");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "Add_task");
                }
                cmd.Parameters.AddWithValue("@assigned_employee", idString);

                //var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                //hubContext.Clients.User(idString.ToString()).notify("New task assigned: ");


                //var notification = new Models.Admin.Notification
                //{
                //    Message = "You have a new task assigned.",
                //    EmployeeId = idString
                //};
                //string id = idString;
                //NotifyEmployee(id);

                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                i = cmd.ExecuteNonQuery();

            }
            connection.Close();

            if (i >= 1)
            {
                TempData["Message"] = "Task Submitted Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }
            return RedirectToAction("Create_TaskByReportingManager");
        }

        //private readonly stanley_india.Models.IUserConnectionManager _userConnectionManager;
        //public NotificationUserHub(stanley_india.Models.IUserConnectionManager userConnectionManager)
        //{
        //    _userConnectionManager = userConnectionManager;
        //}

        //public async override Task OnDisconnectedAsync(Exception exception)
        //{
        //    //get the connectionId
        //    var connectionId = Context.ConnectionId;
        //    _userConnectionManager.RemoveUserConnection(connectionId);
        //    var value = await Task.FromResult(0);
        //}

        //private void NotifyEmployee(string idString)
        //{
        //    // Use SignalR or WebSocket to push notification to the employee
        //    // Example: SignalR hub context
        //    var hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        //    hubContext.Clients.User(idString.ToString()).notify("New task assigned!");
        //}

        public ActionResult task_view()
        {
            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}

            List<TaskManage> task_list = new List<TaskManage>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_task");
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.assigned_emp = sdr["assign_emp"].ToString();
                    pro.task_status = sdr["task_status"].ToString();
                    pro.commentTask = sdr["commentTask"].ToString();
                    task_list.Add(pro);
                }
            }
            connection.Close();
            return View(task_list);
        }
        public ActionResult task_viewByReportingManager()
        {
            //Models.common_response Response = db.adminssioncheck("");
            //if (Response.success == false || Response.parameter != "admin")
            //{
            //    string url = Request.Url.PathAndQuery;
            //    return Redirect("/admin/login?url=" + HttpUtility.UrlEncode(url) + "");
            //}
            List<TaskManage> task_list = new List<TaskManage>();
            task_list = db.ViewTask();
            return View(task_list);
        }

        public ActionResult selectTaskViewById(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.task_status = sdr["task_status"].ToString();
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.assigned_emp = sdr["assign_emp"].ToString();
                    task.commentTask = sdr["commentTask"].ToString();
                    task.commentByOther = sdr["CommentBy"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();

                }
            }
            connection.Close();
            return View(task);

        }
        public ActionResult selectTaskViewByIdEmp(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.task_status = sdr["task_status"].ToString();
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.assigned_emp = sdr["assign_emp"].ToString();
                    task.commentTask = sdr["commentTask"].ToString();
                    task.commentByOther = sdr["CommentBy"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();

                }
            }
            connection.Close();
            return View(task);

        }
        public ActionResult TaskAssigned_list()
        {
            int userid = ((Login)HttpContext.Session["Login"]).EmpId;
           
            List<Macreel_Project.Models.Bussiness.TaskManage> list = new List<Macreel_Project.Models.Bussiness.TaskManage>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_TaskToEmp");
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader sdr = cmd.ExecuteReader();
            Macreel_Project.Models.Bussiness.TaskManage pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new Macreel_Project.Models.Bussiness.TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.task_status = sdr["task_status"].ToString();

                    list.Add(pro);
                }
            }
            connection.Close();
            return View(list);

        }
        public ActionResult ShowComment(string id)
        {
            List<Macreel_Project.Models.Bussiness.TaskManage> list = new List<Macreel_Project.Models.Bussiness.TaskManage>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "showComment");
            cmd.Parameters.AddWithValue("@task_id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            Macreel_Project.Models.Bussiness.TaskManage pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new Macreel_Project.Models.Bussiness.TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.commentByOther = sdr["commentByOther"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    list.Add(pro);
                }
            }
            connection.Close();
            return View(list);

        }

        public ActionResult selectTaskById(string id)
        {

            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.task_status = sdr["task_status"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();

                }
            }
            connection.Close();


            return View(task);

        }

        public ActionResult delet_task(string id)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "delete_task");
            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("task_view");
        }
       


        public ActionResult approved_task()
        {

            List<TaskManage> task_list = new List<TaskManage>();

            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_approvaltask");
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.assigned_emp = sdr["assign_emp"].ToString();
                    pro.task_status = sdr["task_status"].ToString();
                    pro.emp_status = sdr["emp_status"].ToString();
                    pro.updatedDateEmp = sdr["updatedDateEmp"].ToString();

                    task_list.Add(pro);
                }
            }
            connection.Close();


            return View(task_list);

        }
        public ActionResult approved_taskByReporting()
        {
            List<TaskManage> task_list = new List<TaskManage>();
            task_list = db.ViewApprovedTask();
            return View(task_list);
        }
        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        public ActionResult updateAssignedTaskById(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.task_status = sdr["task_status"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();
                }
            }
            connection.Close();
            return View(task);
        }
        public ActionResult updateTaskViewById(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.assigned_emp = sdr["assign_emp"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();
                }
            }
            connection.Close();
            return View(task);
        }
        public ActionResult updateTaskViewByIdForReportingManager(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "select_taskById");
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.assigned_emp = sdr["assign_emp"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();
                }
            }
            connection.Close();
            return View(task);
        }
        public ActionResult update_taskStatus(string id, string status)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "update_taskStatus");
            cmd.Parameters.AddWithValue("@id",id);
            cmd.Parameters.AddWithValue("@task_status", status);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("approved_task");
        }
        public ActionResult update_taskStatusByReportingManager(string id, string status)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Action", "update_taskStatus");
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@task_status", status);

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            return RedirectToAction("approved_taskByReporting");
        }
        [HttpPost]
        public ActionResult update_assignedTask(Macreel_Project.Models.Bussiness.TaskManage task)
        {
            var stats = "Not Complete";
            if (task.check == "1")
            {
                stats = "Completed";
            }
            Macreel_Project.Models.Bussiness.TaskManage  res = new Macreel_Project.Models.Bussiness.TaskManage();

            var curent_date = DateTime.Now.ToString("dd/MM/yyyy");
            var path = System.IO.Path.Combine(Server.MapPath("~/tempimage/"));

            // var path = Server.MapPath("~/tempimage/");

            HttpPostedFileBase file1 = Request.Files["fileupload1"];
            HttpPostedFileBase file2 = Request.Files["fileupload2"];
            HttpPostedFileBase file3 = Request.Files["fileupload3"];
            HttpPostedFileBase file4 = Request.Files["fileupload4"];
            HttpPostedFileBase file5 = Request.Files["fileupload5"];

            string attachment1 = "";
            string attachment2 = "";
            string attachment3 = "";
            string attachment4 = "";
            string attachment5 = "";

            List<TaskManage> tasks = new List<TaskManage>();
            List<HttpPostedFileBase> files = new List<HttpPostedFileBase> { file1, file2, file3, file4, file5 };

            for (int attachmentNum = 1; attachmentNum <= 5; attachmentNum++)
            {
                string attachmentName = "attachment" + attachmentNum.ToString();
                string fileName = "file" + attachmentNum.ToString();


                // Now, check and process the file if it exists for this attachment
                HttpPostedFileBase file = files[attachmentNum - 1];
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    string uploadFileName = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file.FileName.Split('.')[1];

                    file.SaveAs(path + uploadFileName);

                    // Assign the upload filename to the appropriate attachment variable
                    switch (attachmentNum)
                    {
                        case 1:
                            attachment1 = "/tempimage/" + uploadFileName;
                            break;
                        case 2:
                            attachment2 = "/tempimage/" + uploadFileName;
                            break;
                        case 3:
                            attachment3 = "/tempimage/" + uploadFileName;
                            break;
                        case 4:
                            attachment4 = "/tempimage/" + uploadFileName;
                            break;
                        case 5:
                            attachment5 = "/tempimage/" + uploadFileName;
                            break;
                    }
                }
            }


            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@leave_status", status);
            cmd.Parameters.AddWithValue("@id", task.id);
            cmd.Parameters.AddWithValue("@task_status", stats);
            cmd.Parameters.AddWithValue("@attachment1", attachment1);
            cmd.Parameters.AddWithValue("@attachment2", attachment2);
            cmd.Parameters.AddWithValue("@attachment3", attachment3);
            cmd.Parameters.AddWithValue("@attachment4", attachment4);
            cmd.Parameters.AddWithValue("@attachment5", attachment5);
            cmd.Parameters.AddWithValue("@updatedDateEmp", curent_date);
            cmd.Parameters.AddWithValue("@commentTask", task.commentTask);
            cmd.Parameters.AddWithValue("@Action", "update_assignedTaskByEmp");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                return View("Task Updated");
            }
            else
            {
                return View("Task Not Updated");
            }

        }
        [HttpPost]
        public ActionResult Add_Comment(Macreel_Project.Models.Bussiness.TaskManage task)
        {
            Macreel_Project.Models.Bussiness.TaskManage res = new Macreel_Project.Models.Bussiness.TaskManage();

            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@leave_status", status);
            //cmd.Parameters.AddWithValue("@id", task.id);
            cmd.Parameters.AddWithValue("@task_id", task.task_id);
            cmd.Parameters.AddWithValue("@commentByOther", task.commentByOther);
            cmd.Parameters.AddWithValue("@Action", "AddComment");

            if (connection.State == ConnectionState.Closed)
                connection.Open();

            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                return View("Add Successfully");
            }
            else
            {
                return View("Task Not Updated");
            }

        }


        #endregion

        //public ActionResult emp_PerformanceList(string perform_date)
        //{
        //    DateTime currentDate;
        //    if (perform_date == null || perform_date == "")
        //    {
        //        currentDate = DateTime.Now;
        //    }
        //    else
        //    {
        //        currentDate = Convert.ToDateTime(perform_date);
        //    }

        //    Session["perform_date"] = currentDate.ToString("yyyy-MM-dd");
        //    var dd = Session["perform_date"];

        //    List<Employee> add_employee = new List<Employee>();

        //    //add_employee = db.getemployeePerformance(currentDate);

        //    return View(add_employee);
        //}
        #region Employee Performance
        public ActionResult emp_PerformanceList(string perform_date)
        {
            DateTime currentDate;
            if (perform_date == null || perform_date == "")
            {
                currentDate = DateTime.Now;
            }
            else
            {
                currentDate = Convert.ToDateTime(perform_date);
            }
            Session["perform_date"] = currentDate.ToString("yyyy-MM-dd");
            var dd = Session["perform_date"];
            List<Employee> add_employee = new List<Employee>();
            add_employee = db.getemployeePerformance(currentDate);
            return View(add_employee);
        }
        public ActionResult Employee_PerformanceByID(string id, DateTime? date)
        {
            Employee add_employee = new Employee();
            if (date == null)
            {
                date = DateTime.Now;
            }

            if (id != null && id != "")
            {
                add_employee = db.getemployeeById(id, date);
            }

            //var dd = db.CalculatePerformanceWithDates(id);

            return View(add_employee);

        }
        #endregion

        #region password update

        public ActionResult Update_password()
        {
            int Id = ((Login)HttpContext.Session["Login"]).EmpId;
            Employee emp = new Employee();
            if (Id!=null)
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("[Sp_Login]", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectPass");
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emp.Id = Convert.ToInt32(sdr["Id"].ToString());
                    emp.Password = sdr["Password"].ToString();
                }
                sdr.Close();
                connection.Close();
            }
            return View(emp);

        }

        [HttpPost]
        public ActionResult Update_password(Employee emp)
        {
            SqlCommand cmd = new SqlCommand("[Sp_Login]", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@password", emp.Password);
            cmd.Parameters.AddWithValue("@Id", emp.Id);
            cmd.Parameters.AddWithValue("@Action", "update_pass");
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
            {
                TempData["Message"] = "Your Password Updated Successfully";
                TempData["para"] = "true";
            }
            else
            {
                TempData["Message"] = "Please Review Your Input Details!!";
                TempData["para"] = "false";
            }
            return RedirectToAction("Update_password");
        }

        #endregion
        #region TaskReport
        public ActionResult Task_reports(string status, filter_report filter_report)
        {
            TempData["emp_name"] = filter_report.emp_name;
            TempData["assigned_date"] = filter_report.assigned_date;
            TempData["status"] = filter_report.status;
            if (status != "" && status != null)
            {
                Session["Status"] = status;
            }
            var sts = Session["Status"];
            string conditions = "where task_status='" + sts + "'";
            if (filter_report.emp_name != null || filter_report.assigned_date != null)
            {
                if (filter_report.assigned_date != null && filter_report.assigned_date != "")
                {
                    try
                    {
                        if (conditions != "")
                        {
                            conditions = conditions + " and curent_date BETWEEN '" + filter_report.assigned_date.Split('/')[2] + "-" + filter_report.assigned_date.Split('/')[1] + "-" + filter_report.assigned_date.Split('/')[0] + "' and '" + filter_report.toassigned_date.Split('/')[2] + "-" + filter_report.toassigned_date.Split('/')[1] + "-" + filter_report.toassigned_date.Split('/')[0] + "'";
                        }
                        else
                        {
                            conditions = conditions + " assigned_date >='" + filter_report.assigned_date.Split('/')[2] + "-" + filter_report.assigned_date.Split('/')[1] + "-" + filter_report.assigned_date.Split('/')[0] + "'";
                        }
                    }
                    catch
                    {

                    }
                }
                if (filter_report.emp_name != null && filter_report.emp_name != "")
                {
                    if (conditions != "")
                    {
                        conditions = conditions + " and EmployeeName ='" + filter_report.emp_name.ToString().Replace("'", "").ToLower() + "'";
                    }
                    else
                    {
                        conditions = conditions + " EmployeeName ='" + filter_report.emp_name.ToString().Replace("'", "").ToLower() + "'";
                    }
                }
                if (conditions != "")
                {
                    conditions = "" + conditions + "";
                }
            }
            List<TaskManage> task_list = new List<TaskManage>();
            connection.Open();
            string sql = "SELECT emp.EmployeeName, emp.Id AS emp_id, task.* FROM tbl_taskManage AS task LEFT JOIN TblEmployee AS emp ON task.assigned_employee = emp.Id " + conditions + "";
            SqlCommand cmd = new SqlCommand(sql, connection);
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.assigned_emp = sdr["EmployeeName"].ToString();
                    //pro.userid = sdr["emp_id"].ToString();
                    pro.task_status = sdr["task_status"].ToString();
                    task_list.Add(pro);
                }
            }
            connection.Close();
            return View(task_list);
        }
        public ActionResult Task_reportsForReportingManager(string status, filter_report filter_report)
        {
            var userid = ((Login)HttpContext.Session["Login"]).EmpId;
            TempData["emp_name"] = filter_report.emp_name;
            TempData["assigned_date"] = filter_report.assigned_date;
            TempData["status"] = filter_report.status;

            if (!string.IsNullOrEmpty(status))
            {
                Session["Status"] = status;
            }

            var conditions = new StringBuilder("WHERE 1=1"); // Always true, simplifies condition building
            var parameters = new List<SqlParameter>();

            // Filter by task status if specified
            if (!string.IsNullOrEmpty(status))
            {
                conditions.Append(" AND task_status = @Status");
                parameters.Add(new SqlParameter("@Status", status));
            }

            // Filter by user id
            if (userid != 0)
            {
                conditions.Append(" AND emp.ReportingManager = @UserId");
                parameters.Add(new SqlParameter("@UserId", userid));
            }

            // Date filtering
            if (!string.IsNullOrEmpty(filter_report.assigned_date))
            {
                var startDate = DateTime.ParseExact(filter_report.assigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var endDate = DateTime.ParseExact(filter_report.toassigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                conditions.Append(" AND curent_date BETWEEN @StartDate AND @EndDate");
                parameters.Add(new SqlParameter("@StartDate", startDate));
                parameters.Add(new SqlParameter("@EndDate", endDate));
            }

            // Filter by employee name if specified
            if (!string.IsNullOrEmpty(filter_report.emp_name))
            {
                conditions.Append(" AND LOWER(EmployeeName) = LOWER(@EmpName)");
                parameters.Add(new SqlParameter("@EmpName", filter_report.emp_name.Replace("'", "''"))); // Properly escape single quotes
            }

            List<TaskManage> task_list = new List<TaskManage>();
            string sql = $@"SELECT emp.EmployeeName, emp.Id AS emp_id, task.* 
                    FROM tbl_taskManage AS task 
                    LEFT JOIN TblEmployee AS emp ON task.assigned_employee = emp.Id 
                    {conditions}";

                connection.Open();
                using (var cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddRange(parameters.ToArray());
                    using (var sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var pro = new TaskManage
                            {
                                id = sdr["id"].ToString(),
                                s_no = sdr["S_no"].ToString(),
                                title = sdr["title"].ToString(),
                                description = sdr["description"].ToString(),
                               complete_date = sdr["completion_date"].ToString(),
                               current_date = sdr["curent_date"].ToString(),
                        attachment1 = sdr["attachment1"].ToString(),
                        attachment2 = sdr["attachment2"].ToString(),
                        attachment3 = sdr["attachment3"].ToString(),
                       attachment4 = sdr["attachment4"].ToString(),
                       attachment5 = sdr["attachment5"].ToString(),
                        assigned_emp = sdr["EmployeeName"].ToString(),
                        //pro.userid = sdr["emp_id"].ToString();
                        task_status = sdr["task_status"].ToString()
                    };
                            task_list.Add(pro);
                        }
                    }
                
            }

            return View(task_list);
        }

        #region Task report Pdf Download
        public ActionResult task_pdfdownload(filter_report filter_report)
        {
            DataTable dt = db.download_report(filter_report);
            string html = " <html> <head> <meta charset='utf-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <link href='https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700;800&display=swap' rel='stylesheet'> <style>body *{font-family:'Montserrat';}table{border-collapse: collapse;}td, th{border: 1px solid #999;padding: 2px;text-align: left;font-size: 13px;text-transform: capitalize;}tbody tr:nth-child(odd){background: #eee;}</style> </head> <body><div> <img src='#' /><label style='text-align:center; color:blue; margin:5px; font-weight:bolder; font-size:30px;'>Macreel Infosoft Pvt Ltd</label></div><hr> <div> <table> <thead>";

            var col = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                col++;
                if (col == 20)
                {
                    break;
                }
                html = html + " <th>" + dc.ColumnName + "</th>";
            }
            html = html + "  </thead> <tbody>";
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                html = html + " <tr> ";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    html = html + "<td>" + dr[i].ToString() + "</td>";
                }
                html = html + " </tr>";
            }
            html = html + "  </tbody> </table> </div></body></html> ";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
            var pdfBytes = htmlToPdf.GeneratePdf(html);
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;filename=Task_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
            return RedirectToAction("Task_reports");

        }
        public ActionResult task_pdfdownloadByReportingManager(filter_report filter_report)
        {
            DataTable dt = db.download_reportForReportingManager(filter_report);
            string html = " <html> <head> <meta charset='utf-8'> <meta http-equiv='X-UA-Compatible' content='IE=edge'> <link href='https://fonts.googleapis.com/css2?family=Montserrat:wght@400;500;600;700;800&display=swap' rel='stylesheet'> <style>body *{font-family:'Montserrat';}table{border-collapse: collapse;}td, th{border: 1px solid #999;padding: 2px;text-align: left;font-size: 13px;text-transform: capitalize;}tbody tr:nth-child(odd){background: #eee;}</style> </head> <body><div> <img src='#' /><label style='text-align:center; color:blue; margin:5px; font-weight:bolder; font-size:30px;'>Macreel Infosoft Pvt Ltd</label></div><hr> <div> <table> <thead>";

            var col = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                col++;
                if (col == 20)
                {
                    break;
                }
                html = html + " <th>" + dc.ColumnName + "</th>";
            }
            html = html + "  </thead> <tbody>";
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                html = html + " <tr> ";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    html = html + "<td>" + dr[i].ToString() + "</td>";
                }
                html = html + " </tr>";
            }
            html = html + "  </tbody> </table> </div></body></html> ";

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            htmlToPdf.Orientation = NReco.PdfGenerator.PageOrientation.Landscape;
            var pdfBytes = htmlToPdf.GeneratePdf(html);
            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;filename=Task_Report_" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf");
            Response.BinaryWrite(pdfBytes);
            Response.End();
            return RedirectToAction("Task_reports");

        }
        #endregion
        #region order report Excel DownLoad
        public ActionResult taskreport_exceldownload(filter_report filter_report)
        {
            DataTable dt = db.download_report(filter_report);
            string attachment = "attachment; filename=order_report" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            string fullhtml = "";
            int datecolumn_index = 0;
            int datecolumn_dob_index = 0;
            int col = 0;

            foreach (DataColumn dc in dt.Columns)
            {
                col++;
                if (col == 20)
                {
                    break;
                }
                fullhtml = fullhtml + "<th style='border:1px solid black'>" + dc.ColumnName + "</th>";
            }
            fullhtml = "<thead><tr>" + fullhtml + "</tr></thead><tbody>";
            int i;
            string bodyhtml = "";
            foreach (DataRow dr in dt.Rows)
            {
                bodyhtml = bodyhtml + "<tr>";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == datecolumn_index || datecolumn_dob_index == i)
                    {
                        try
                        {
                            bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + Convert.ToDateTime(tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")).ToString("yyyy-MM-dd") + "</td>";
                        }
                        catch
                        {
                            bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + (tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")) + "</td>";
                        }
                    }
                    else
                    {
                        bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + (tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")) + "</td>";
                    }
                }
                bodyhtml = bodyhtml + "</tr>";
            }
            fullhtml = "<table style='border-collapse: collapse;'>" + fullhtml + bodyhtml + "</body></table>";
            Response.Write(fullhtml);
            Response.End();


            return RedirectToAction("Task_reports");
        }
        public ActionResult taskreport_exceldownloadByReportingManager(filter_report filter_report)
        {
            DataTable dt = db.download_reportByReportingManager(filter_report);
            string attachment = "attachment; filename=order_report" + DateTime.Now.ToString("dd_MM_yyyy") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            string fullhtml = "";
            int datecolumn_index = 0;
            int datecolumn_dob_index = 0;
            int col = 0;

            foreach (DataColumn dc in dt.Columns)
            {
                col++;
                if (col == 20)
                {
                    break;
                }
                fullhtml = fullhtml + "<th style='border:1px solid black'>" + dc.ColumnName + "</th>";
            }
            fullhtml = "<thead><tr>" + fullhtml + "</tr></thead><tbody>";
            int i;
            string bodyhtml = "";
            foreach (DataRow dr in dt.Rows)
            {
                bodyhtml = bodyhtml + "<tr>";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == datecolumn_index || datecolumn_dob_index == i)
                    {
                        try
                        {
                            bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + Convert.ToDateTime(tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")).ToString("yyyy-MM-dd") + "</td>";
                        }
                        catch
                        {
                            bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + (tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")) + "</td>";
                        }
                    }
                    else
                    {
                        bodyhtml = bodyhtml + "<td  style='border:1px solid black;vertical-align: top;text-align: left;'>" + (tab + dr[i].ToString().Replace("<p>", "").Replace("</p>", "").Replace("\n", "").Replace("\t", "")) + "</td>";
                    }
                }
                bodyhtml = bodyhtml + "</tr>";
            }
            fullhtml = "<table style='border-collapse: collapse;'>" + fullhtml + bodyhtml + "</body></table>";
            Response.Write(fullhtml);
            Response.End();


            return RedirectToAction("Task_reports");
        }
        #endregion

        #endregion


    }
}