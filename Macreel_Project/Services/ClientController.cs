using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Xml.Linq;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ClientController : ApiController
    {
        SqlConnection con;
        SqlCommand cmd;
        public ClientController()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Add_Client()
        {
            var httpRequest= HttpContext.Current.Request;
            int row = 0;
            try
            {
                con.Open();
                var obj = new Models.Bussiness.Client
                {
                    Id = Convert.ToInt32(httpRequest.Form.Get("Id")),
                    CompanyName = httpRequest.Form.Get("CompanyName"),
                    ContactPerson = httpRequest.Form.Get("ContactPerson"),
                    ContactNo = httpRequest.Form.Get("ContactNo"),
                    GSTNo = httpRequest.Form.Get("GSTNo"),
                    State = httpRequest.Form.Get("State"),
                    City = httpRequest.Form.Get("City"),
                    StateName = httpRequest.Form.Get("StateName"),
                    CityName = httpRequest.Form.Get("CityName"),
                    Address = httpRequest.Form.Get("Address"),
                    Statecode = httpRequest.Form.Get("Statecode"),
                    Designation = httpRequest.Form.Get("Designation"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    EmailId = httpRequest.Form.Get("EmailId")
                };
                cmd = new SqlCommand("[Sp_Client]",con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@StateName", obj.StateName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@Statecode", obj.Statecode);
                cmd.Parameters.AddWithValue("@Pincode", obj.Pincode);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
                row = cmd.ExecuteNonQuery();
                if (row > 0)
                {
                    return Ok("Success");
                }
                else
                {
                    return BadRequest("Data Not Inserted");
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
        [System.Web.Http.HttpPost]
        public IHttpActionResult Delete_client(string Id)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
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
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_ClientbyId(string Id)
        {
            con.Open();
            Models.Bussiness.Client obj = new Models.Bussiness.Client();
            try
            {
                SqlCommand cmd = new SqlCommand("[Sp_Client]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Models.Bussiness.Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.StateName = rd["StateName"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.Statecode = rd["Statecode"].ToString();
                        obj.Pincode = rd["Pincode"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.Designation = rd["Designation"].ToString();
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
            return Ok(obj);
        }
        // all client get
        [System.Web.Http.HttpGet]
        public IHttpActionResult View_Client()
        {
            con.Open();
            List<Models.Bussiness.Client> clnt = new List<Models.Bussiness.Client>();
            Models.Bussiness.Client obj = new Models.Bussiness.Client();
            try
            {
                SqlCommand cmd = new SqlCommand("[Sp_Client]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Models.Bussiness.Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.StateName = rd["StateName"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.Statecode = rd["Statecode"].ToString();
                        obj.Pincode = rd["Pincode"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        clnt.Add(obj);
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
            return Ok(clnt);
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult Update_Client(string Id)
        {
            var httpRequest = HttpContext.Current.Request;
            con.Open();
            try
            {
                var obj = new Models.Bussiness.Client
                {
                    Id = Convert.ToInt32(httpRequest.Form.Get("Id")),
                    CompanyName = httpRequest.Form.Get("CompanyName"),
                    ContactPerson = httpRequest.Form.Get("ContactPerson"),
                    ContactNo = httpRequest.Form.Get("ContactNo"),
                    GSTNo = httpRequest.Form.Get("GSTNo"),
                    State = httpRequest.Form.Get("State"),
                    City = httpRequest.Form.Get("City"),
                    StateName = httpRequest.Form.Get("StateName"),
                    CityName = httpRequest.Form.Get("CityName"),
                    Address = httpRequest.Form.Get("Address"),
                    Statecode = httpRequest.Form.Get("Statecode"),
                    Designation = httpRequest.Form.Get("Designation"),
                    Pincode = httpRequest.Form.Get("Pincode"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    EmailId = httpRequest.Form.Get("EmailId"),
                };
                SqlCommand cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@Id",Id);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@StateName", obj.StateName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@Statecode", obj.Statecode);
                cmd.Parameters.AddWithValue("@Pincode", obj.Pincode);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
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
    }
}
