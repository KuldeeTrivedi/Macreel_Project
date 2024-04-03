using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Services
{
    public class UpdateTaskStatusByReportingManagerController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        [HttpPost]
        public IHttpActionResult Update(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            var task = new TaskManage()
            {
                task_status = httpRequest.Form.Get("task_status")
            };
            SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@task_status", task.task_status);
            cmd.Parameters.AddWithValue("@Action", "update_taskStatus");
            if (con.State == ConnectionState.Closed)
                con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();

            return Ok("success");
        }
    }
}
