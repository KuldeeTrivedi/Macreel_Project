using Macreel_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.IO;
using static Macreel_Project.Models.Bussiness;


namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DepartmentAndDesignationApiController : ApiController
    {
        public class DepartmentandDesignationController : ApiController
        {
            SqlConnection con;
            SqlCommand cmd;
            DataAccess db = new DataAccess();
            public DepartmentandDesignationController()
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            }
            [System.Web.Http.HttpPost]
            public IHttpActionResult InsertDepartment()
            {
                var httpRequest = HttpContext.Current.Request;
                try
                {
                    var deptobj = new emp_list
                    {
                        department = httpRequest.Form.Get("Name"),
                    };
                    cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "InsertDepartment");
                    cmd.Parameters.AddWithValue("@Name", deptobj.department);
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return Ok("Data Inserted Successfully");
                    }
                    else
                    {
                        return Ok("Data Not inserted");
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
            [System.Web.Http.HttpGet]
            public IHttpActionResult Get_DePartment()
            {
                List<emp_list> list = new List<emp_list>();
                emp_list empobj = new emp_list();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "BindDepartment");
                    cmd.Parameters.AddWithValue("@Name", empobj.department);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            empobj = new emp_list();
                            empobj.emp_id = sdr["Id"].ToString();
                            empobj.department = sdr["Name"].ToString();
                            list.Add(empobj);
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
                }
                return Ok(list);
            }
            [HttpPost]
            public IHttpActionResult Delete_Department(string id,string Name)
            {
                int row = 0;
                try
                {
                    cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DeleteDepartment");
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
            [HttpPost]
            public IHttpActionResult update_department(string Id)
            {
                var httpRequest = HttpContext.Current.Request;
                try
                {
                    var regobj = new emp_list()
                    {
                        department = httpRequest.Form.Get("Name"),
                    };
                    //add other form fields as needed
                    SqlCommand cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateDepartment");
                    cmd.Parameters.AddWithValue("@Name", regobj.department);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return Ok("Updated Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest("Error Update Data:" + ex.Message);
                }
            }
            public class emp_list
            {
                public string emp_id { get; set; }
                public string emp_name { get; set; }
                public string designation { get; set; }
                public string department { get; set; }
            }
        }
        public class DesignationController : ApiController
        {
            SqlConnection con;
            SqlCommand cmd;
            DataAccess db = new DataAccess();
            public DesignationController()
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            }
            [System.Web.Http.HttpPost]
            public IHttpActionResult Designation()
            {
                var httpRequest = HttpContext.Current.Request;
                try
                {
                    var desiobj = new emp_list
                    {
                        designation = httpRequest.Form.Get("Name")
                    };
                    cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "InsertDesignation");
                    cmd.Parameters.AddWithValue("@Name", desiobj.designation);
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return Ok("Data Inserted Successfully");
                    }
                    else
                    {
                        return Ok("Data Not Inserted");
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
            [System.Web.Http.HttpGet]
            public IHttpActionResult Get_Designation()
            {
                List<emp_list> list = new List<emp_list>();
                emp_list empobj = new emp_list();
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "BindDesignation");
                    cmd.Parameters.AddWithValue("@Name", empobj.designation);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            empobj = new emp_list();
                            empobj.emp_id = sdr["Id"].ToString();
                            empobj.designation = sdr["Name"].ToString();
                            list.Add(empobj);
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
                }
                return Ok(list);
            }
            [System.Web.Http.HttpPost]
            public IHttpActionResult Delete_Designation(string Id,string emp_name)
            {
                int row = 0;
                try
                {
                    cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "DeleteDesignation");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    con.Open();
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
            [HttpPost]
            public IHttpActionResult update_designation(string Id)
            {
                var httpRequest = HttpContext.Current.Request;
                try
                {
                    var regobj = new emp_list()
                    {
                        designation = httpRequest.Form.Get("Name"),
                    };
                    //add other form fields as needed
                    SqlCommand cmd = new SqlCommand("[Sp_DeptAndDesignation]", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Action", "UpdateDesignation");
                    cmd.Parameters.AddWithValue("@Name", regobj.designation);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    if (con.State == ConnectionState.Closed)
                        con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    return Ok("Updated Successfully");
                }
                catch (Exception ex)
                {
                    return BadRequest("Error Update Data:" + ex.Message);
                }
            }
            public class emp_list
            {
                public string emp_id { get; set; }
                public string emp_name { get; set; }
                public string designation { get; set; }
                public string department { get; set; }
            }
        }
        public class TaskApiController : ApiController
        {
            SqlCommand cmd;
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

            [HttpGet]
            public IHttpActionResult Get_taskList()
            {
                List<TaskManage> task = new List<TaskManage>();
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
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

                var emp_status = "pending";
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
                cmd.Parameters.AddWithValue("@id",id);
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
            public IHttpActionResult delet_task(string id,string description)
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

    }
}
