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
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetCityByStateIdController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con= new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        [HttpGet]
        public IHttpActionResult View_City(string id)
        {
            List<city> list = new List<city>();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "City");
                cmd.Parameters.AddWithValue("@StateId", id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                city select;
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select = new city();
                        select.Id = Convert.ToInt32(rd["ID"].ToString());
                        select.City_Name = rd["Name"].ToString();
                        select.state_id = Convert.ToInt32(rd["StateID"].ToString());
                        list.Add(select);
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
    }
}
