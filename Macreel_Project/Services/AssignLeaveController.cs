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
    public class AssignLeaveController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();
        [HttpPost]
        public IHttpActionResult Insert_Leave()
        {

            var httpRequest = HttpContext.Current.Request;
            string CurrentYear = DateTime.Now.Year.ToString();
            int row = 0;
            try
            {
                con.Open();
                var obj = new Models.Bussiness.Assignleave
                {
                    EmployeeId= httpRequest.Form.Get("EmployeeId"),
                    EmployeeName = httpRequest.Form.Get("EmployeeName"),
                    Type =httpRequest.Form.Get("LeaveType"),
                    Leave= httpRequest.Form.Get("NoOfLeave"),
                    Year= CurrentYear,
                };
                cmd = new SqlCommand("[Sp_LeaveManagement]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertAssignLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@LeaveType", obj.Type);
                cmd.Parameters.AddWithValue("@NoOfLeave", obj.Leave);
                cmd.Parameters.AddWithValue("@Year", CurrentYear);
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
        public IHttpActionResult GetUserDashboardLeaveForAdmins(string id)
        {
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UserDashboardLeaveAdmin");
                cmd.Parameters.AddWithValue("@EmployeeId", id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.EmployeeId = rd["EmployeeId"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["NoOfLeave"]);
                        if (rd["LeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        if (rd["RejectLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectLeaveCount"]);
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
}
