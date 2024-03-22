using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Macreel_Project.Models.Bussiness;
using System.Configuration;
using System.Web.Mvc;
using System.Web;

namespace Macreel_Project.Services
{
    public class ApprovedTaskListAdminController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_Task()
        {
            List<TaskManage> task_list = new List<TaskManage>();

            con.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
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
            con.Close();


            return Ok(task_list);
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult ViewById(string id)
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
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.assigned_emp = sdr["assign_emp"].ToString();
                    //task.assigned_emp = sdr["assigned_emp"].ToString();
                }
            }
            con.Close();
            return Ok(task);
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult UpdateTaskStatus(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            int row = 0;
            try
            {
                con.Open();
                var obj = new Models.Bussiness.TaskManage
                {
                    id= httpRequest.Form.Get("id"),
                    task_status= httpRequest.Form.Get("task_status"),
                };
                cmd = new SqlCommand("[sp_TaskManage]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "update_taskStatus");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@task_status", obj.task_status);
                
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
    }
}
