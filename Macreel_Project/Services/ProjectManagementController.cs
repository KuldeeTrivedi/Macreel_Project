using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectManagementController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        [System.Web.Http.HttpPost]
        public string Insert_Project(Project empobj)
        {
            string message = string.Empty;
            int count = 0;
            try
            {
                string Name = empobj.ProjectName.Substring(0, 3);
                Random rn = new Random();
                int N0 = 0;
                N0 = rn.Next(1000, 9999);
                empobj.ProjectCode = "Mac" + Name + "20" + N0;
                cmd = new SqlCommand("[Sp_Project]",con);
                con.Open();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@CompanyId", empobj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", empobj.CompanyName);
                cmd.Parameters.AddWithValue("@ClientName", empobj.ClientName);
                cmd.Parameters.AddWithValue("@ProjectName", empobj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectCode", empobj.ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectStartingDate", empobj.ProjectStartingDate);
                cmd.Parameters.AddWithValue("@CompletionDate", empobj.CompletionDate);
                cmd.Parameters.AddWithValue("@ProjectDeliveryDate", empobj.ProjectDeliveryDate);
                cmd.Parameters.AddWithValue("@TotalAmount", empobj.TotalAmount);
                cmd.Parameters.AddWithValue("@ProjectStatus", empobj.ProjectStatus);
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            if (count > 0)
            {
                message ="Data Inserted Successfully";
            }
            else
            {
                message="Failed to insert data";
            }
            return message;
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Update_Project(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var empobj = new Models.Bussiness.Project
                {
                    CompanyId = httpRequest.Form.Get("CompanyId"),
                    CompanyName = httpRequest.Form.Get("CompanyName"),
                    ClientName = httpRequest.Form.Get("ClientName"),
                    ProjectName = httpRequest.Form.Get("ProjectName"),
                    ProjectCode = httpRequest.Form.Get("ProjectCode"),
                    ProjectStartingDate = httpRequest.Form.Get("ProjectStartingDate"),
                    CompletionDate = httpRequest.Form.Get("CompletionDate"),
                    ProjectDeliveryDate = httpRequest.Form.Get("ProjectDeliveryDate"),
                    TotalAmount = Convert.ToDecimal(httpRequest.Form.Get("TotalAmount")),
                    ProjectStatus = httpRequest.Form.Get("ProjectStatus")
                };
                cmd = new SqlCommand("[Sp_Project]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@CompanyId", empobj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", empobj.CompanyName);
                cmd.Parameters.AddWithValue("@ClientName", empobj.ClientName);
                cmd.Parameters.AddWithValue("@ProjectName", empobj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectCode", empobj.ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectStartingDate", empobj.ProjectStartingDate);
                cmd.Parameters.AddWithValue("@CompletionDate", empobj.CompletionDate);
                cmd.Parameters.AddWithValue("@ProjectDeliveryDate", empobj.ProjectDeliveryDate);
                cmd.Parameters.AddWithValue("@TotalAmount", empobj.TotalAmount);
                cmd.Parameters.AddWithValue("@ProjectStatus", empobj.ProjectStatus);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return Ok("Updated Successfully");
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
        [System.Web.Http.HttpGet]
        public IHttpActionResult view_project()
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
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
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete_Project(string id)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
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
            return Ok("Deleted Succssfully");
        }
    }
}
