using Macreel_Project.Models;
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
    public class AssignProjectController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        Macreel_Project.Models.DataAccess db = new Macreel_Project.Models.DataAccess();

        [System.Web.Http.HttpPost]
        public string InsertEmployeeProject(AssignProject obj)
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
                Message = "Inserted successfully";
            }
            else
            {
                Message = "error";
            }
            return Message;
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_AssignProject() 
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            DataAccess dc = new DataAccess();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AssignBind");
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
                        //obj.AssignDate = rd["AssignDate"].ToString();
                        obj.AssignDate = rd["ProjectStartingDate"].ToString();
                        obj.EmployeeName = dc.ProjectEmployee(obj.ProjectCode);
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
        public string ViewAssignProjectByProjectCode(string id)
        {
            string Emp = "";
            string message = "";
            Emp = db.ProjectEmployee(id);
            string Descript = "";
            Descript = db.ProjectEmployeeDescription(id);
            List<Project> gtl = new List<Project>();
            gtl = db.getDocumentaion(id);
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
            list = db.ProjectStatusDetail(id);
            return message;
        }
        public IHttpActionResult Delete_AssignProject(string id)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteEmployee");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", id);
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
            return Ok(row);
        }
    }
}
