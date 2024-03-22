using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Macreel_Project.Services
{
    public class ServiceController : ApiController
    {
        SqlConnection con;
        SqlCommand cmd;
        public ServiceController()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Add_Service()
        {
            con.Open();
            var httpRequest = HttpContext.Current.Request;
            int count = 0;
            try
            {
                var serve = new Macreel_Project.Models.Bussiness.services
                {
                Id=Convert.ToInt32(httpRequest.Form.Get("Id")),
                Code=httpRequest.Form.Get("Code"),
                Name=httpRequest.Form.Get("Name")
            };
                cmd = new SqlCommand("[Sp_Services]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@Code",serve.Code);
                cmd.Parameters.AddWithValue("@Name", serve.Name);
                count=cmd.ExecuteNonQuery();
                if(count>0)
                {
                    return Ok("Data Inserted Successfylly..");
                }
                else
                {
                    return Ok("Data Not Inserted..");
                };
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_Services()
        {
            con.Open();
            List<Macreel_Project.Models.Bussiness.services> list = new List<Models.Bussiness.services>();
            Macreel_Project.Models.Bussiness.services obj = new Models.Bussiness.services();
            try
            {
                SqlCommand cmd = new SqlCommand("[Sp_Services]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                cmd.Parameters.AddWithValue("@Id",obj.Id);
                cmd.Parameters.AddWithValue("@Name",obj.Name);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        new Models.Bussiness.services();
                        obj.Id = Convert.ToInt32(sdr["Id"].ToString());
                        obj.Name = sdr["Name"].ToString();
                        list.Add(obj);
                    }
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return Ok(list);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete_Service(string id)
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("[Sp_Services]",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                cmd.Parameters.AddWithValue("@Id", id);
                int row=cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    return Ok("Deleted successfully..");
                }
                else
                {
                    return Ok("Data Not Deleted...");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        [HttpPost]
        public IHttpActionResult Update_Service(string id)
        {
            con.Open();
            var httpRequest = HttpContext.Current.Request;
            int count = 0;
            try
            {
                var serve = new Macreel_Project.Models.Bussiness.services
                {
                    Id = Convert.ToInt32(httpRequest.Form.Get("Id")),
                    Code = httpRequest.Form.Get("Code"),
                    Name = httpRequest.Form.Get("Name")
                };
                cmd = new SqlCommand("[Sp_Services]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Code", serve.Code);
                cmd.Parameters.AddWithValue("@Name", serve.Name);
                count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return Ok("Data Updated Successfylly..");
                }
                else
                {
                    return Ok("Data Not Inserted..");
                };
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
