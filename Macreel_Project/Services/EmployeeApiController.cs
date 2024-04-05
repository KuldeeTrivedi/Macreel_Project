using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using static Macreel_Project.Models.Bussiness;
using System.Web.Http.Cors;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmployeeApiController : ApiController
    {
        public class selectTaskByIdController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View_Task(string id)
            {
                Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
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
                con.Close();


                return Ok(task);
            }
        }
        public class ShowSerialNoController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [System.Web.Http.HttpGet]
            public IHttpActionResult View()
            {
                List<TaskManage> task = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_task_SNo");
                SqlDataReader sdr = cmd.ExecuteReader();
                //var ab = sdr["serial_no"].ToString();
                TaskManage pr;
                if (sdr.HasRows)
                {

                    while (sdr.Read())
                    {
                        pr = new TaskManage();
                        pr.s_no = sdr["serial_no"].ToString();
                        if (pr.s_no == "")
                        {
                            pr.s_no = "1";
                        }
                        task.Add(pr);
                    }
                }
                con.Close();
                return Ok(task);
            }
        }
        public class TaskAssignListEmpController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult ViewTastById(string id)
            {
                List<Macreel_Project.Models.Bussiness.TaskManage> list = new List<Macreel_Project.Models.Bussiness.TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_TaskToEmp");
                cmd.Parameters.AddWithValue("@userid", id);
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
                con.Close();
                return Ok(list);
            }
            [HttpPost]
            public IHttpActionResult update_task(string id)
            {
                var httpRequest = HttpContext.Current.Request;
                var curent_date = DateTime.Now.ToString("dd/MM/yyyy");
                var task_status = "Not Complete";
                if (httpRequest.Form.Get("task_status") != null)
                {
                    task_status = httpRequest.Form.Get("task_status");
                }
                var task = new TaskManage()
                {
                    updatedDateEmp = curent_date,
                    commentTask = httpRequest.Form.Get("commentTask"),
                    assigned_emp = httpRequest.Form.Get("assigned_emp"),
                    attachment1 = httpRequest.Form.Get("attachment1"),
                    attachment2 = httpRequest.Form.Get("attachment2"),
                    attachment3 = httpRequest.Form.Get("attachment3"),
                    attachment4 = httpRequest.Form.Get("attachment4"),
                    attachment5 = httpRequest.Form.Get("attachment5"),
                    task_status = task_status

                };


                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    task.attachment1 = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 1)
                {
                    var postedFile1 = httpRequest.Files[1];
                    string filePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile1.FileName);
                    postedFile1.SaveAs(filePath1);
                    task.attachment2 = "/tempimage/" + postedFile1.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 2)
                {
                    var attachment3 = httpRequest.Files[2];
                    string filePath2 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment3.FileName);
                    attachment3.SaveAs(filePath2);
                    task.attachment3 = "/tempimage/" + attachment3.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 3)
                {
                    var attachment4 = httpRequest.Files[3];
                    string filePath3 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment4.FileName);
                    attachment4.SaveAs(filePath3);
                    task.attachment4 = "/tempimage/" + attachment4.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 4)
                {
                    var attachment5 = httpRequest.Files[4];
                    string filePath4 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment5.FileName);
                    attachment5.SaveAs(filePath4);
                    task.attachment5 = "/tempimage/" + attachment5.FileName; // Save the file path in the database
                }
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@updatedDateEmp", curent_date);
                cmd.Parameters.AddWithValue("@commentTask", task.commentTask);
                cmd.Parameters.AddWithValue("@attachment1", task.attachment1);
                cmd.Parameters.AddWithValue("@attachment2", task.attachment2);
                cmd.Parameters.AddWithValue("@attachment3", task.attachment3);
                cmd.Parameters.AddWithValue("@attachment4", task.attachment4);
                cmd.Parameters.AddWithValue("@attachment5", task.attachment5);
                cmd.Parameters.AddWithValue("@task_status", task_status);
                cmd.Parameters.AddWithValue("@Action", "update_assignedTaskByEmp");
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();

                return Ok("Updated Successfully");
            }
        }
        public class TaskApiForEmpByReportingController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

            [HttpGet]
            public IHttpActionResult Get_taskList(string id, string s_no)
            {
                List<TaskManage> task = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_task1");
                cmd.Parameters.AddWithValue("@id", id);
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
                        pro.assigned_empId = sdr["assigned_employee"].ToString();
                        pro.task_status = sdr["task_status"].ToString();
                        pro.emp_status = sdr["emp_status"].ToString();
                        pro.commentTask = sdr["commentTask"].ToString();

                        task.Add(pro);
                    }
                }
                con.Close();

                return Ok(task);
            }
            [HttpGet]
            public IHttpActionResult Get_taskById(string id)
            {
                TaskManage task = new TaskManage();
                task = db.gettaskById(id);
                return Ok(task);
            }

            [HttpPost]
            public IHttpActionResult insert_task()
            {
                var httpRequest = HttpContext.Current.Request;

                var curent_date = DateTime.Now.ToString("yyyy-MM-dd");

                var title = httpRequest.Form.Get("title");
                var description = httpRequest.Form.Get("description");
                var assignedEmp = httpRequest.Form.Get("assigned_emp");
              var completeDate = httpRequest.Form.Get("complete_date");

                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(assignedEmp))
                {
                    return BadRequest("Title, description, and assigned employee are required.");
                }
               var task = new TaskManage()
                {
                    s_no = httpRequest.Form.Get("s_no"),
                    title = httpRequest.Form.Get("title"),
                    description = httpRequest.Form.Get("description"),
                    current_date = curent_date,
                    complete_date = httpRequest.Form.Get("complete_date"),
                    assigned_emp = httpRequest.Form.Get("assigned_emp"),
                    task_status = "Pending",
                };


                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    task.attachment1 = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 1)
                {
                    var postedFile1 = httpRequest.Files[1];
                    string filePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile1.FileName);
                    postedFile1.SaveAs(filePath1);
                    task.attachment2 = "/tempimage/" + postedFile1.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 2)
                {
                    var attachment3 = httpRequest.Files[2];
                    string filePath2 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment3.FileName);
                    attachment3.SaveAs(filePath2);
                    task.attachment3 = "/tempimage/" + attachment3.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 3)
                {
                    var attachment4 = httpRequest.Files[3];
                    string filePath3 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment4.FileName);
                    attachment4.SaveAs(filePath3);
                    task.attachment4 = "/tempimage/" + attachment4.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 4)
                {
                    var attachment5 = httpRequest.Files[4];
                    string filePath4 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment5.FileName);
                    attachment5.SaveAs(filePath4);
                    task.attachment5 = "/tempimage/" + attachment5.FileName; // Save the file path in the database
                }


                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", task.id);
                cmd.Parameters.AddWithValue("@S_no", task.s_no);
                cmd.Parameters.AddWithValue("@title", task.title);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@curent_date", curent_date);
                cmd.Parameters.AddWithValue("@completion_date", task.complete_date);
                cmd.Parameters.AddWithValue("@attachment1", task.attachment1);
                cmd.Parameters.AddWithValue("@attachment2", task.attachment2);
                cmd.Parameters.AddWithValue("@attachment3", task.attachment3);
                cmd.Parameters.AddWithValue("@attachment4", task.attachment4);
                cmd.Parameters.AddWithValue("@attachment5", task.attachment5);
                cmd.Parameters.AddWithValue("@assigned_employee", task.assigned_emp);
                cmd.Parameters.AddWithValue("@task_status", "Pending");
                cmd.Parameters.AddWithValue("@emp_status", "Pending");
                cmd.Parameters.AddWithValue("@Action", "Add_task");
                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();

                return Ok("success");
            }

            [HttpPost]
            public IHttpActionResult update_task(string id)
            {
                var httpRequest = HttpContext.Current.Request;

                var curent_date = DateTime.Now.ToString("yyyy-MM-dd");


                var task_status = "Pending";
                if (httpRequest.Form.Get("task_status") != null)
                {
                    task_status = httpRequest.Form.Get("task_status");
                }

                var emp_status = "Pending";
                if (httpRequest.Form.Get("emp_status") != null)
                {
                    emp_status = httpRequest.Form.Get("emp_status");
                }

                var task = new TaskManage()
                {
                    s_no = httpRequest.Form.Get("s_no"),
                    title = httpRequest.Form.Get("title"),
                    description = httpRequest.Form.Get("description"),
                    current_date = curent_date,
                    complete_date = httpRequest.Form.Get("complete_date"),
                    assigned_emp = httpRequest.Form.Get("assigned_emp"),
                    attachment1 = httpRequest.Form.Get("attachment1"),
                    attachment2 = httpRequest.Form.Get("attachment2"),
                    attachment3 = httpRequest.Form.Get("attachment3"),
                    attachment4 = httpRequest.Form.Get("attachment4"),
                    attachment5 = httpRequest.Form.Get("attachment5"),
                    updatedDateEmp = httpRequest.Form.Get("updatedDateEmp"),
                    task_status = task_status,
                    emp_status = emp_status,

                };


                if (httpRequest.Files.Count > 0)
                {
                    var postedFile = httpRequest.Files[0];
                    //string fileName = Path.GetFileName(emp.fileupload1.FileName);
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    task.attachment1 = "/tempimage/" + postedFile.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 1)
                {
                    var postedFile1 = httpRequest.Files[1];
                    string filePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), postedFile1.FileName);
                    postedFile1.SaveAs(filePath1);
                    task.attachment2 = "/tempimage/" + postedFile1.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 2)
                {
                    var attachment3 = httpRequest.Files[2];
                    string filePath2 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment3.FileName);
                    attachment3.SaveAs(filePath2);
                    task.attachment3 = "/tempimage/" + attachment3.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 3)
                {
                    var attachment4 = httpRequest.Files[3];
                    string filePath3 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment4.FileName);
                    attachment4.SaveAs(filePath3);
                    task.attachment4 = "/tempimage/" + attachment4.FileName; // Save the file path in the database
                }

                if (httpRequest.Files.Count > 4)
                {
                    var attachment5 = httpRequest.Files[4];
                    string filePath4 = Path.Combine(HttpContext.Current.Server.MapPath("~/tempimage/"), attachment5.FileName);
                    attachment5.SaveAs(filePath4);
                    task.attachment5 = "/tempimage/" + attachment5.FileName; // Save the file path in the database
                }
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@S_no", task.s_no);
                cmd.Parameters.AddWithValue("@title", task.title);
                cmd.Parameters.AddWithValue("@description", task.description);
                cmd.Parameters.AddWithValue("@curent_date", curent_date);
                cmd.Parameters.AddWithValue("@completion_date", task.complete_date);
                cmd.Parameters.AddWithValue("@attachment1", task.attachment1);
                cmd.Parameters.AddWithValue("@attachment2", task.attachment2);
                cmd.Parameters.AddWithValue("@attachment3", task.attachment3);
                cmd.Parameters.AddWithValue("@attachment4", task.attachment4);
                cmd.Parameters.AddWithValue("@attachment5", task.attachment5);
                cmd.Parameters.AddWithValue("@assigned_employee", task.assigned_emp);
                cmd.Parameters.AddWithValue("@task_status", task.task_status);
                cmd.Parameters.AddWithValue("@emp_status", task.emp_status);
                cmd.Parameters.AddWithValue("@Action", "update_task");


                if (con.State == ConnectionState.Closed)
                    con.Open();

                int i = cmd.ExecuteNonQuery();
                con.Close();

                return Ok("success");
            }

            [HttpPost]
            public IHttpActionResult delet_task(string id, string description)
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Action", "delete_task");
                cmd.Parameters.AddWithValue("@id", id);

                int i = cmd.ExecuteNonQuery();
                con.Close();

                return Ok("success");
            }
        }
        public class ViewPenCloseAndNotRespondTaskByReportiController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

            [HttpGet]
            public IHttpActionResult View(string status, string id)
            {
                List<TaskManage> task_list = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ViewPendingTaskByReporting");
                cmd.Parameters.AddWithValue("@task_status", status);
                cmd.Parameters.AddWithValue("@id", id);
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
                con.Close();
                return Ok(task_list);
            }
        }
        public class ViewApprovedTaskByReportingController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<TaskManage> task_list = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_approvaltask1");
                cmd.Parameters.AddWithValue("@id", id);
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
                con.Close();


                return Ok(task_list);
            }
        }
        public class ShowEmpForReportingManagerController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                TaskManage task = new TaskManage();
                List<emp_list> emp_list = new List<emp_list>();
                con.Open();
                SqlCommand cm = new SqlCommand("sp_TaskManage", con);
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.AddWithValue("@Action", "select_empList1");
                cm.Parameters.AddWithValue("@id", id);
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
                con.Close();
                return Ok(emp_list);
            }
        }
        public class CompleteTaskByEmpListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            [System.Web.Http.HttpGet]
            public IHttpActionResult CompleteTaskView(string id)
            {
                List<TaskManage> task_list = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "selectCompleteTask");
                cmd.Parameters.AddWithValue("@userid", id);
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
                        pro.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                        pro.emp_status = sdr["emp_status"].ToString();
                        pro.commentTask = sdr["commentTask"].ToString();
                        task_list.Add(pro);
                    }
                }
                con.Close();
                return Ok(task_list);
            }
            [HttpPost]
            public IHttpActionResult DeleteTask(string id ,string description)
            {
                int row = 0;
                try
                {
                    cmd = new SqlCommand("sp_TaskManage", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DeleteCompleteTask");
                    con.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok("Deleted Successfully");
            }

        }
        public class ViewCompTastByEmpByIdController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            [System.Web.Http.HttpGet]
            public IHttpActionResult View(string id)
            {
                Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();
                con.Open();
                cmd = new SqlCommand("sp_TaskManage", con);
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
                        //task.assigned_emp = sdr["assigned_emp"].ToString();

                    }
                }
                con.Close();
                return Ok(task);
            }
        }
        public class AppliedLeaveListEmpController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View_Leave(string id)
            {
                List<ApplyLeave> gtlst = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "SelectApplyEmployeeLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                            obj.Status = rd["Status"].ToString();
                            gtlst.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }
        }
        public class LeaveManagementEmpController : ApiController
        {
            SqlCommand cmd;  
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpPost]
            public IHttpActionResult InsertApplyLeave()
            {
                var httpRequest = HttpContext.Current.Request;
                int row = 0;
                try
                {
                    con.Open();
                    var obj = new Models.Bussiness.ApplyLeave
                    {
                        EmployeeId = httpRequest.Form.Get("EmployeeId"),
                        EmployeeName = httpRequest.Form.Get("EmployeeName"),
                        ToDate = httpRequest.Form.Get("ToDate"),
                        FromDate = httpRequest.Form.Get("FromDate"),
                        LeaveType = httpRequest.Form.Get("LeaveType"),
                        Description = httpRequest.Form.Get("Description")
                    };
                    cmd = new SqlCommand("[Sp_LeaveManagement]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "InsertApplyEmployeeLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                    cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
                    cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
                    cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return Ok("Success");
                    }
                    else
                    {
                        return BadRequest("Data Not Inserted");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            [HttpPost]
            public IHttpActionResult Update_Leave(string id)
            {
                var httpRequest = HttpContext.Current.Request;
                int row = 0;
                try
                {
                    con.Open();
                    var obj = new Models.Bussiness.ApplyLeave
                    {
                        EmployeeId = httpRequest.Form.Get("EmployeeId"),
                        EmployeeName = httpRequest.Form.Get("EmployeeName"),
                        ToDate = httpRequest.Form.Get("ToDate"),
                        FromDate = httpRequest.Form.Get("FromDate"),
                        LeaveType = httpRequest.Form.Get("LeaveType"),
                        Description = httpRequest.Form.Get("Description")
                    };
                    cmd = new SqlCommand("[Sp_LeaveManagement]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateApplyLeave");
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                    cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
                    cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
                    cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return Ok("Success");
                    }
                    else
                    {
                        return BadRequest("Data Not Inserted");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            [HttpGet]
            public IHttpActionResult BindLeaveById(int id)
            {
                ApplyLeave obj = new ApplyLeave();
                obj = db.EditApplyLeave(id);
                return Ok(obj);
            }
        }
        public class ViewCLListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<ApplyLeave> getlist = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "CLLeaveRemaining");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.ToDate = rd["FromDate"].ToString();
                            obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                            if (rd["ApprovedLeaveCount"] != DBNull.Value)
                            {
                                obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                            }
                            obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                            var rem = obj.Remain;
                            // HttpContext.Current.Session["CLRe"] = obj.Remain;
                            if (rd["RejectedLeaveCount"] != DBNull.Value)
                            {
                                obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                            }
                            else
                            {
                                obj.RejectLeaveCount = 0;
                            }
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }

                return Ok(getlist);
            }
        }
        public class ViewELListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<ApplyLeave> getlist = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ELLeaveRemaining");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                            if (rd["ApprovedLeaveCount"] != DBNull.Value)
                            {
                                obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                            }
                            obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                            // HttpContext.Current.Session["CLRe"] = obj.Remain;
                            if (rd["RejectedLeaveCount"] != DBNull.Value)
                            {
                                obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                            }
                            else
                            {
                                obj.RejectLeaveCount = 0;
                            }
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(getlist);
            }
        }
        public class ViewMLListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<ApplyLeave> getlist = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "MLLeaveRemaining");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                            if (rd["ApprovedLeaveCount"] != DBNull.Value)
                            {
                                obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                            }
                            obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                            // HttpContext.Current.Session["CLRe"] = obj.Remain;
                            if (rd["RejectedLeaveCount"] != DBNull.Value)
                            {
                                obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                            }
                            else
                            {
                                obj.RejectLeaveCount = 0;
                            }
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(getlist);
            }
        }
        public class ViewLWPController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<ApplyLeave> getlist = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "LWPLeaveRemaining");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                            if (rd["ApprovedLeaveCount"] != DBNull.Value)
                            {
                                obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                            }
                            obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                            // HttpContext.Current.Session["CLRe"] = obj.Remain;
                            if (rd["RejectedLeaveCount"] != DBNull.Value)
                            {
                                obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                            }
                            else
                            {
                                obj.RejectLeaveCount = 0;
                            }
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(getlist);
            }
        }
        public class ViewEmpLeaveController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult ViewLeave(string id)
            {
                List<ApplyLeave> gtlst = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "SelectApplyEmployeeLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                            obj.Status = rd["Status"].ToString();
                            gtlst.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }


        }
        public class PendinLeaveEmpController : ApiController
        {

            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult PendingApplyLeave(string id)
            {
                List<ApplyLeave> gtlst = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "PendingEmpLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                            obj.Status = rd["Status"].ToString();
                            gtlst.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }
            [HttpPost]
            public IHttpActionResult Delete(string id)
            {
                int row = 0;
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "deleteLeaveByemp");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok("Deleted Successfully");
            }
           
        }
        public class ApprovedLeaveListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult ApproveList(string id)
            {
                List<ApplyLeave> gtlst = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ApprovedEmpLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                            obj.Status = rd["Status"].ToString();
                            gtlst.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }
        }
        public class RejectLeaveListController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult Reject_Leave(string id)
            {
                List<ApplyLeave> gtlst = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "RejectEmpLeave");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                            obj.Status = rd["Status"].ToString();
                            gtlst.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }
        }
        public class AssignProjectListEmpController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult AssignProject(string id)
            {
                List<Project> gtlst = new List<Project>();
                Project obj = new Project();
                try
                {
                    cmd = new SqlCommand("Sp_Project", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "EmployeeProject");
                    cmd.Parameters.AddWithValue("@EmployeeId", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new Project();
                            obj.Id = Convert.ToInt32(rd["Id"]);
                            obj.ProjectName = rd["ProjectName"].ToString();
                            obj.CompanyName = rd["CompanyName"].ToString();
                            obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                            obj.CompletionDate = rd["CompletionDate"].ToString();
                            obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                            obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                            obj.ProjectCode = rd["ProjectCode"].ToString();
                            obj.ProjectStatus = rd["ProjectStatus"].ToString();
                            gtlst.Add(obj);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(gtlst);
            }
        }
        public class ViewAssProEmpByProjectCodeController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View(string id)
            {
                List<Project> getlist = new List<Project>();
                Project obj = new Project();
                try
                {
                    cmd = new SqlCommand("Sp_Project", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ViewEmpAssignById");
                    cmd.Parameters.AddWithValue("@ProjectCode", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new Project();
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.Description = rd[2].ToString();
                            obj.ProjectStatus = rd["ProjectStatus"].ToString();
                            obj.YourDescription = rd[4].ToString();
                            obj.ProjectStatus = rd["ProjectStatus"].ToString();
                            obj.Document = rd["DocumentPath"].ToString();
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(getlist);
            }
        }
        public class GetApplyListForReportingManagerController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult View_List(string id)
            {
                List<ApplyLeave> getlist = new List<ApplyLeave>();
                ApplyLeave obj = new ApplyLeave();
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "ApplyLeaveForReporting");
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            obj = new ApplyLeave();
                            obj.Id = Convert.ToInt32(rd["LeaveId"]);
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                            obj.ToDate = rd["ToDate"].ToString();
                            obj.FromDate = rd["FromDate"].ToString();
                            obj.AppliedDate = rd["AppliedDate"].ToString();
                            obj.LeaveType = rd["LeaveType"].ToString();
                            obj.Description = rd["Description"].ToString();
                            obj.AdminDescription = rd["AdminDescription"].ToString();
                            obj.Status = rd["Status"].ToString();
                            getlist.Add(obj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok(getlist);
            }
        }
        public class SelectOldPassController : ApiController
        {
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpGet]
            public IHttpActionResult GetEmployee(int id)
            {
                Employee emp = new Employee();

                SqlCommand cmd = new SqlCommand("[Sp_Login]", con);
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectPass");
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    sdr.Read();
                    emp.Id = Convert.ToInt32(sdr["Id"].ToString());
                    emp.Password = sdr["Password"].ToString();
                }

                sdr.Close();
                con.Close();

                return Ok(emp);
            }
        }
        public class UpdatePasswordController : ApiController
        {
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpPost]
            public string UpdatePass(Employee emp)
            {
                string Message = "";
                SqlCommand cmd = new SqlCommand("[Sp_Login]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@password", emp.Password);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@Action", "update_pass");
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i >= 1)
                {
                    Message = "Your Password Updated Successfully";
                }
                else
                {
                    Message = "Please Review Your Input Details!!";

                }
                return Message;
            }
        }
        public class UpdateProjectStatusByEmpController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpPost]
            public IHttpActionResult Update()
            {
                var httpRequest = HttpContext.Current.Request;
                int row = 0;
                try
                {
                    con.Open();
                    var obj = new Models.Bussiness.Project
                    {
                        EmployeeId = httpRequest.Form.Get("EmployeeId"),
                        EmployeeName = httpRequest.Form.Get("EmployeeName"),
                        ProjectCode = httpRequest.Form.Get("ProjectCode"),
                        ProjectName = httpRequest.Form.Get("ProjectName"),
                        ProjectStatus = httpRequest.Form.Get("ProjectStatus"),
                        Description = httpRequest.Form.Get("Description"),
                        Date = httpRequest.Form.Get("Date"),
                    };
                    cmd = new SqlCommand("[Sp_Project]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "InsertProjectStatus");
                    cmd.Parameters.AddWithValue("@EmployeeId",obj.EmployeeId);
                    cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                    cmd.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);
                    cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                    cmd.Parameters.AddWithValue("@ProjectStatus", obj.ProjectStatus);
                    cmd.Parameters.AddWithValue("@Description", obj.Description);
                    cmd.Parameters.AddWithValue("@Date", obj.Date);
                    row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return Ok("Status Updated Successfully");
                    }
                    else
                    {
                        return BadRequest("Status Not Updated");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            
           
           

        }
        public class UpdateLeaveStatusbyReportingController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
            [HttpPost]
            public IHttpActionResult UpdateStatus(string id)
            {
                var httpRequest = HttpContext.Current.Request;
                int row = 0;
                try
                {
                    con.Open();
                    var obj = new Models.Bussiness.ApplyLeave
                    {
                        Status = httpRequest.Form.Get("STATUS"),
                        AdminDescription = httpRequest.Form.Get("AdminDescription"),
                        LeaveCount = Convert.ToInt32(httpRequest.Form.Get("LeaveCount")),
                    };
                    cmd = new SqlCommand("[Sp_LeaveManagement]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateLeaveStatusbyReporting");
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@STATUS", obj.Status);
                    cmd.Parameters.AddWithValue("@AdminDescription", obj.AdminDescription);
                    cmd.Parameters.AddWithValue("@LeaveCount", obj.LeaveCount);
                    row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        return Ok("Status Updated Successfully");
                    }
                    else
                    {
                        return BadRequest("Status Not Updated");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            [HttpDelete]
            public IHttpActionResult DeleteLeave(string Id)
            {
                int row = 0;
                try
                {
                    cmd = new SqlCommand("Sp_LeaveManagement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DeleteApplyLeave");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    row = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    con.Close();
                    cmd.Dispose();
                }
                return Ok("Deleted Successfully");
            }
        }
    }
}
