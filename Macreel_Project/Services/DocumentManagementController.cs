using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DocumentManagementController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        [System.Web.Http.HttpPost]
        public IHttpActionResult Insert_Document()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var empobj = new Models.Bussiness.Project
                {
                    ProjectCode = httpRequest.Form.Get("ProjectCode"),
                    Document = httpRequest.Form.Get("Document")
                };
                if (httpRequest.Files.Count > 0)
                {
                    var PostedFile = httpRequest.Files[0];
                    string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("/ProjectDocument/"), PostedFile.FileName);
                    PostedFile.SaveAs(FilePath);
                    empobj.Document = "/ProjectDocument/" + PostedFile.FileName;//save the filepath in the database
                }
                cmd = new SqlCommand("[Sp_Project]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertDocument");
                cmd.Parameters.AddWithValue("@ProjectCode", empobj.ProjectCode);
                cmd.Parameters.AddWithValue("@Document", empobj.Document);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return Ok("Inserted Successfully");
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
