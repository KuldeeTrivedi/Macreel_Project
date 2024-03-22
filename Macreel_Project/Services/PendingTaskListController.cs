using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Services
{
    public class PendingTaskListController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        [HttpGet]
        public IHttpActionResult ViewPending(string id)
        {
            List<TaskManage> task_list = new List<TaskManage>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectPendingTask");
            cmd.Parameters.AddWithValue("@task_status",id);
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
}
