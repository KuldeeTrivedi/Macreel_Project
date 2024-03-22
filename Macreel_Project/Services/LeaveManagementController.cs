using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LeaveManagementController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

        [System.Web.Http.HttpPost]
        public string InsertLeaveType(LeaveTypes obj)
        {
            string Message = "";
            int row = 0;
            if (obj.Id != 0)
            {
                row = db.UpdateLeaveType(obj);
                if (row > 0)
                {
                    Message = "Data Updated Successfully";
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

                    Message = "Data Inserted Successfully";
                }
                else
                {
                    Message = "error";
                }
            }
            return Message;
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_Leave()
        {
            List<LeaveTypes> list = new List<LeaveTypes>();
            LeaveTypes obj = new LeaveTypes();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectLeveType");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new LeaveTypes();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        list.Add(obj);
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
            return Ok(list);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult DeleteLeaveType(string id)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteLeveType");
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
            return Ok("Date Deleted Successfully");

        }
        

    }
}
